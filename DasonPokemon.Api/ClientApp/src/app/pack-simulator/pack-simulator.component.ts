import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

enum PackSimState {
  PackList = 1,
  PackSelected = 2,
  PackOpening = 3,
  PackOpened = 4
}

interface Pack {
  id : string;
  name : string;
  image: string;
  cards: Card[];
}

interface Card {
  id: string;
  name: string;
  image: string;
  nonRare: boolean;
  flipped: boolean;
}

@Component({
  selector: 'app-pack-simulator',
  templateUrl: './pack-simulator.component.html',
  styleUrls: ['./pack-simulator.component.scss']
})
export class PackSimulatorComponent implements OnInit {

  state : PackSimState = PackSimState.PackList;

  packs : Pack[] = [];

  selectedPack : Pack | null;
  selectedCard : Card | null;
  waitingForData : boolean = false;

  httpClient : HttpClient;

  constructor(private http : HttpClient) {
    this.httpClient = http;

    this.waitingForData = true;
    this.httpClient.get("api/packs").subscribe((data: any) => {
      this.packs = data.map((item : any) => {
        return {
          id: item.id,
          name: item.name,
          image: item.image
        };
      });
      console.log(this.packs);
      this.waitingForData = false;
    });

    this.selectedPack = null;
    this.selectedCard = null;
  }

  ngOnInit(): void {
  }

  public selectPack(pack : Pack) {
    this.selectedPack = pack;
    this.state = PackSimState.PackSelected;
  }

  public openPack() {
    this.waitingForData = true;
    this.state = PackSimState.PackOpening;

    // Make call to backend and ask for a pack opening for the selected pack
    this.httpClient.get("api/packs/generate/" + this.selectedPack!.id).subscribe((data: any) => {
      this.selectedPack!.cards = data.map((item : any) => {
        return {
          id: item.id,
          name: item.name,
          image: item.images['large'],
          flipped: false,
          nonRare: item.rarity === "Common" || item.rarity === "Uncommon"
        };
      });
      console.log(this.selectedPack!.cards);
      this.waitingForData = false;

      let currentTimeout = 250;
      for(let i = 0; i < this.selectedPack!.cards.length; i++) {
        if (this.selectedPack!.cards[i].nonRare) {
          setTimeout(() => {
            this.selectedPack!.cards[i].flipped = true;
          }, currentTimeout);
          currentTimeout = currentTimeout + 250;
        }
      }
    });
  }

  public goBack() {
    if (this.state == PackSimState.PackSelected) {
      // Clear selected pack and go back to initial state
      this.selectedPack = null;
      this.state = PackSimState.PackList;
    }
  }

  public flipCard(card : Card) {
    card.flipped = true;
  }

  public zoomCard(card : Card) {
    this.selectedCard = card;
  }
}
