import { BrowserModule } from '@angular/platform-browser';
import { NgModule, } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { GameService } from './shared/services/game.service';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { HistoryService } from './shared/services/history.service';
import { LoginComponent } from './components/login/login.component';
import { RegisterService } from './shared/services/register.service';
import { ErrorPageComponent } from './components/error-page/error-page.component';


const appRoutes: Routes = [
  {
    path: '',
    component: LoginComponent
  },
  {
    path: 'game/:id',
    loadChildren: './components/game/game.module#GameModule'
  },
  {
    path: 'history',
    loadChildren: './components/history/history.module#HistoryModule'
  },
  {
    path:'error',
    component: ErrorPageComponent
  }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ErrorPageComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [GameService, HistoryService, RegisterService],
  bootstrap: [AppComponent]
})
export class AppModule { }
