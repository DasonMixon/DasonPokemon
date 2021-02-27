import { Component, OnInit } from '@angular/core';

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
  flipped: boolean;
}

@Component({
  selector: 'app-pack-simulator',
  templateUrl: './pack-simulator.component.html',
  styleUrls: ['./pack-simulator.component.scss']
})
export class PackSimulatorComponent implements OnInit {

  state : PackSimState = PackSimState.PackList;

  packs : Pack[];

  selectedPack : Pack | null;
  waitingForPackResults : boolean = false;

  constructor() {

    // TODO: Should be getting pack list from backend
    this.packs = [
      { id: '1', name: 'Shining Fates', image : '/assets/shining_fates_pack.jpg', cards: [] },
      { id: '2', name: 'Vivid Voltage', image : '/assets/vivid_voltage_pack.jpg', cards: [] }
    ];

    this.selectedPack = null;
  }

  ngOnInit(): void {
  }

  public selectPack(pack : Pack) {
    this.selectedPack = pack;
    this.state = PackSimState.PackSelected;
  }

  public openPack() {
    this.waitingForPackResults = true;
    this.state = PackSimState.PackOpening;

    // Make call to backend and ask for a pack opening for the selected pack

    // For now, gonna just return some dummy data
    this.selectedPack!.cards = [
      { id: '1', name: 'someCard1', image: 'https://images.pokemontcg.io/base1/18_hires.png', flipped: false },
      { id: '2', name: 'someCard2', image: 'https://images.pokemontcg.io/base1/21_hires.png', flipped: false },
      { id: '3', name: 'someCard3', image: 'https://images.pokemontcg.io/base1/21_hires.png', flipped: false },
      { id: '4', name: 'someCard4', image: 'https://images.pokemontcg.io/base1/21_hires.png', flipped: false },
      { id: '5', name: 'someCard5', image: 'https://images.pokemontcg.io/base1/21_hires.png', flipped: false },
      { id: '6', name: 'someCard6', image: 'https://images.pokemontcg.io/base1/21_hires.png', flipped: false },
      { id: '7', name: 'someCard7', image: 'https://images.pokemontcg.io/base1/21_hires.png', flipped: false },
      { id: '8', name: 'someCard8', image: 'https://images.pokemontcg.io/base1/21_hires.png', flipped: false },
      { id: '9', name: 'someCard9', image: 'https://images.pokemontcg.io/base1/21_hires.png', flipped: false },
      { id: '10', name: 'someCard10', image: 'https://images.pokemontcg.io/base1/21_hires.png', flipped: false },
    ];

    setTimeout(() => {
      this.waitingForPackResults = false;
    }, 3000);

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
    console.log("flipping card");
  }
}
