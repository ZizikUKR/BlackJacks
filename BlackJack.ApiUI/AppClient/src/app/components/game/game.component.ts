import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/shared/services/game.service';
import { ActivatedRoute } from '@angular/router';
import { Move } from 'src/app/shared/models/move.model';
import { GameInformation } from 'src/app/shared/models/game-info.model';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})

export class GameComponent implements OnInit {
  public users = [];
  public moves: Move[];
  public gameId: string = '';
  public isOver = false;
  public status = '';

  constructor(private gameService: GameService, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.gameId = params['id'];
    });
    this.getMoves(this.gameId);
  }


  public getMoves(id: string): void {
    this.gameService.showMoves(id).subscribe((res: Move[]) => {
      this.moves = res;
      console.log(this.moves);
    })
  }

  public getCard(id: string): void {
    this.gameService.nextMove(id).subscribe(res => {
      console.log(res);
      this.getMoves(id);
      this.isOver = res;
      this.showResult(this.isOver);
    })
  }

  public getRestOfCards(id: string): void {
    this.gameService.dealRestOfCards(id).subscribe(res => {
      this.isOver = res;
      this.getMoves(id);
      this.showResult(this.isOver);
    })
  }

  public showResult(isFinish:boolean):void {
     if (isFinish) {
      this.gameService.getGameInfo(this.gameId).subscribe((res: any) => {
        this.status = res.Status     
         return
      })
     }
     this.status = '';
  }

}
