import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DeckDraftingComponent } from './deck-drafting/deck-drafting.component';
import { ExploreComponent } from './explore/explore.component';
import { HomeComponent } from './home/home.component';
import { PackSimulatorComponent } from './pack-simulator/pack-simulator.component';
import { TournamentComponent } from './tournament/tournament.component';

const routes: Routes = [
  { path: '',   redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'explore', component: ExploreComponent },
  { path: 'packsim', component: PackSimulatorComponent },
  { path: 'deckdraft', component: DeckDraftingComponent },
  { path: 'tournament', component: TournamentComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
