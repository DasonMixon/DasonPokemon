import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { PackSimulatorComponent } from './pack-simulator/pack-simulator.component';
import { DeckDraftingComponent } from './deck-drafting/deck-drafting.component';
import { TournamentComponent } from './tournament/tournament.component';
import { ExploreComponent } from './explore/explore.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PackSimulatorComponent,
    DeckDraftingComponent,
    TournamentComponent,
    ExploreComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
