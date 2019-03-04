import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HistoryService } from 'src/app/shared/services/history.service';
import { IPlayer } from 'src/app/shared/models/player.model';
import { GameInformation } from 'src/app/shared/models/game-info.model';


@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
  public players: IPlayer[];
  public userName ='';
  public games:GameInformation[] = [];

  constructor(private historyService: HistoryService, private router: Router) { }

  ngOnInit() {
    this.getAllPlayers();
  }

  public getAllPlayers():void {
    this.historyService.getUsers().subscribe(res => {
      this.players = res.playerViewModels;
    })
  }
  
  public getAllGames():void {
    let model: IPlayer = {
      name: this.userName
    }
    console.log(model.name)
    this.historyService.getAllPlayerGames(model).subscribe(res => {
      this.games = res.finishGameViewModels;
    })
  }

  public showGameMoves(id: string):void {
    this.router.navigate(
      ["history/gameRounds", id]
    );
  }
}
