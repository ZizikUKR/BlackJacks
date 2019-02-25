import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HistoryService } from 'src/app/shared/services/history.service';


@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
  public players= [];
  public playerName = '';
  public games= [];

  constructor(private historyService: HistoryService, private router: Router)
   { }

  ngOnInit() {
    this.getAllPlayers();
  }

  public getAllPlayers(){
    this.historyService.getUsers().subscribe((res:any)=>{
      this.players = res;
    })
  }

  public getAllGames(){

    const body = {
      username: this.playerName
    }

      this.historyService.getAllPlayerGames(body).subscribe((res:any)=>{
        this.games = res;
        console.log(res);
      })
  }
  
  public showGameMoves(id:any){
    this.router.navigate(
      ["history/gameRounds", id]
    );
  }
}
