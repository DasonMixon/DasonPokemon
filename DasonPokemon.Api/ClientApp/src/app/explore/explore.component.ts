import { Component, OnInit, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { identifierModuleUrl } from '@angular/compiler';

interface Set {
  id: string;
  name : string;
  image : string;
  releaseDate : Date;
  lastUpdated : Date;
}

interface Card {
  id: string;
  name : string;
  image : string;
}

@Component({
  selector: 'app-explore',
  templateUrl: './explore.component.html',
  styleUrls: ['./explore.component.scss']
})
@Injectable()
export class ExploreComponent implements OnInit {

  waitingForData : boolean = false;
  viewingSets : boolean = false;
  viewingCardsInSet : boolean = false;

  sets : Set[] = [];
  selectedSet : Set | null = null;

  cards : Card[] = [];

  httpClient : HttpClient;

  constructor(private http : HttpClient) {
    this.httpClient = http;

    this.waitingForData = true;
    this.httpClient.get("api/sets").subscribe((data: any) => {
      this.sets = data.map((item : any) => {
        return {
          id: item.id,
          name: item.name,
          image: item.images['logo'],
          releaseDate: item.releaseDate,
          lastUpdated: item.lastUpdated
        };
      });
      console.log(this.sets);
      this.viewingSets = true;
      this.viewingCardsInSet = false;
      this.waitingForData = false;
    });
  }

  ngOnInit(): void {
  }

  public loadCardForSet(set : Set) {
    this.waitingForData = true;
    this.cards = [];
    this.selectedSet = set;
    this.httpClient.get("api/cards/set/" + set.id).subscribe((data: any) => {
      this.cards = data.map((item : any) => {
        return {
          id: item.id,
          name: item.name,
          image: item.images['large']
        };
      });
      console.log(this.cards);
      this.viewingSets = false;
      this.viewingCardsInSet = true;
      this.waitingForData = false;
    });
  }

  public goToSets() {
    this.viewingCardsInSet = false;
    this.viewingSets = true;
    this.selectedSet = null;
  }

  public sortSetsBy(prop: string) {
    return this.sets.sort((a : any, b : any) => a[prop] > b[prop] ? -1 : a[prop] === b[prop] ? 0 : 1);
  }

  public sortCardsBy(prop: string) {
    return this.cards.sort((a : any, b : any) => a[prop] > b[prop] ? 1 : a[prop] === b[prop] ? 0 : -1);
  }
}
