import { Component } from '@angular/core';
import { GameService } from './shared/services/Game.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent  {
  constructor (private testService: GameService) {
  }



}
