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
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthModule } from '@auth0/auth0-angular';
import { AuthButtonComponent } from './auth-button/auth-button.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PackSimulatorComponent,
    DeckDraftingComponent,
    TournamentComponent,
    ExploreComponent,
    AuthButtonComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AuthModule.forRoot({
      domain: 'das-pokemon.us.auth0.com',
      clientId: 'oRppRaGAm6nmgm7OCyagMVe3tkXBaGFx'
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
