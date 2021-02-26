using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Linq;
using System.Diagnostics;

namespace DasonPokemon.Core.Services
{
    public class DataRefreshService : IDataRefreshService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICardService _cardService;
        private readonly ISetService _setService;
        private readonly ILogger<DataRefreshService> _logger;
        private readonly IMapper _mapper;

        public DataRefreshService(IHttpClientFactory httpClientFactory, ICardService cardService, ISetService setService, ILogger<DataRefreshService> logger,
            IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _cardService = cardService;
            _setService = setService;
            _logger = logger;
            _mapper = mapper;
        }

        // TODO: After seeing what I had to do here, we need to switch to just using EF with a relational DB... Also I think it makes more sense to just own the data ourselves instead of getting from one API and trying to import it to this one just so that we can add a few more fields
        public async Task<bool> Refresh(string baseUrl, string apiKey)
        {
            try
            {
                var sw = new Stopwatch();
                sw.Start();

                _logger.LogInformation("Starting data refresh");

                var setIds = new Dictionary<string, Guid>();

                await RefreshData<ApiResponses.Set, Entities.Set>(baseUrl, apiKey, "sets", "id",
                    async (items) => {
                        // Get all sets from DB that have a matching ExternalId with the ones we just got from the API
                        var currentSets = await _setService.GetSetsWithMatchingExternalIds(items.Select(c => c.ExternalId));

                        // Map the existing Id values over the items we're about to upsert that way we don't regenerate them
                        var upsertingSets = (from newSet in items
                                             join exSet in currentSets on newSet.ExternalId equals exSet.ExternalId into matches
                                             from subSet in matches.DefaultIfEmpty()
                                             select new { NewSet = newSet, ExistingSet = subSet }).ToList();

                        foreach (var setGroup in upsertingSets)
                        {
                            if (setGroup.ExistingSet != null)
                            {
                                setGroup.NewSet.Id = setGroup.ExistingSet.Id;
                            }

                            // Add cusatom data here, like in images add the pack image
                            setGroup.NewSet.Images.Add("pack", SetPackImages.GetValueOrDefault(setGroup.NewSet.Name, "https://i.imgur.com/vMgkQHQ.jpg")); // TODO: Default image should be some sort of placeholder image or something that matches pack size

                            if (!setIds.ContainsKey(setGroup.NewSet.ExternalId))
                                setIds.Add(setGroup.NewSet.ExternalId, setGroup.NewSet.Id);
                        }

                        // Bulk upsert
                        await _setService.BulkUpsert(upsertingSets.Select(us => us.NewSet));
                    });

                // Make API calls to dev api to get all cards, all sets, all everything and use those to update data in mongo
                await RefreshData<ApiResponses.Card, Entities.Card>(baseUrl, apiKey, "cards", "id",
                    async (items) => {
                        // Get all cards from DB that have a matching ExternalId with the ones we just got from the API
                        var currentCards = await _cardService.GetCardsWithMatchingExternalIds(items.Select(c => c.ExternalId));

                        // Map the existing Id values over the items we're about to upsert that way we don't regenerate them
                        var upsertingCards = (from newCard in items
                                             join exCard in currentCards on newCard.ExternalId equals exCard.ExternalId into matches
                                             from subCard in matches.DefaultIfEmpty()
                                             select new { NewCard = newCard, ExistingCard = subCard }).ToList();

                        foreach (var cardGroup in upsertingCards)
                        {
                            if (cardGroup.ExistingCard != null)
                            {
                                cardGroup.NewCard.Id = cardGroup.ExistingCard.Id;
                                cardGroup.NewCard.Set.Id = cardGroup.ExistingCard.Set.Id;
                            }
                            else if (!setIds.ContainsKey(cardGroup.NewCard.Set.ExternalId))
                            {
                                // The set that this card says it belongs to doesn't exist in our db, so we must stop here
                                throw new ApplicationException($"Tried to add card '{cardGroup.NewCard.ExternalId}' but the set it belongs to does not exist in our system '{cardGroup.NewCard.Set.ExternalId}'");
                            } else
                            {
                                cardGroup.NewCard.Set.Id = setIds[cardGroup.NewCard.Set.ExternalId];
                            }

                            // Add cusatom data here, like in images add the pack image
                            cardGroup.NewCard.Images.Add("pack", SetPackImages.GetValueOrDefault(cardGroup.NewCard.Set.Name, "https://i.imgur.com/vMgkQHQ.jpg")); // TODO: Default image should be some sort of placeholder image or something that matches pack size
                        }

                        // Bulk upsert
                        await _cardService.BulkUpsert(upsertingCards.Select(uc => uc.NewCard));
                    });

                sw.Stop();

                _logger.LogInformation($"Completed data refresh, took {sw.ElapsedMilliseconds}ms");
                return true;
            } catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while refreshing data: {ex.Message}");
                return false;
            }
        }

        private async Task RefreshData<TApiModel, TEntityModel>(string baseUrl, string apiKey, string name, string orderBy, Func<IEnumerable<TEntityModel>, Task> processPage)
        {
            int returnedResultCount = 0;
            int currentPage = 1;
            var client = _httpClientFactory.CreateClient();

            do
            {
                var request = new HttpRequestMessage(HttpMethod.Get, Path.Combine(baseUrl, $"{name}?page={currentPage}&orderBy={orderBy}"));
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("X-Api-Key", apiKey);

                _logger.LogDebug($"Performing data refresh call. Method: {request.Method}, Request Uri: {request.RequestUri}");
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    var responseData = await JsonSerializer.DeserializeAsync<GetApiHttpResponse<TApiModel>>(responseStream);
                    returnedResultCount = responseData.Count;
                    currentPage++;

                    var pageResults = _mapper.Map<IEnumerable<TEntityModel>>(responseData.Data);
                    await processPage(pageResults);
                }
                else
                {
                    throw new ApplicationException($"Error occurred during data refresh call. Method: {request.Method}, Request Uri: {request.RequestUri}");
                }
            } while (returnedResultCount > 0);
        }

        private Dictionary<string, string> SetPackImages = new Dictionary<string, string>
        {
            { "Base", "https://i.imgur.com/pp7p56x.jpeg" },
            { "Jungle", "https://i.imgur.com/pp7p56x.jpeg" }
        };
    }
}
