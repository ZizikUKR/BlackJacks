import { BrowserModule } from '@angular/platform-browser';
import { NgModule,  } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { GameService } from './shared/services/Game.service';
import { HttpClientModule } from '@angular/common/http';
import { GameComponent } from './components/game/game.component';
import {Routes, RouterModule} from '@angular/router';
import { HistoryComponent } from './components/history/history.component';
import { HistoryService } from './shared/services/history.service';
import { LoginComponent } from './components/login/login.component';
import { HistoryRoundsComponent } from './components/history-rounds/history-rounds.component';


const appRoutes: Routes = [
  {path:'',component: LoginComponent},
  {path:'game/:id',component: GameComponent},
  {path:'history',component: HistoryComponent},
  {path: 'history/gameRounds/:id',component: HistoryRoundsComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    GameComponent,
    LoginComponent,
    HistoryComponent,
    HistoryRoundsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [GameService, HistoryService],
  bootstrap: [AppComponent]
})
export class AppModule { }
