import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/shared/services/game.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Move } from 'src/app/shared/models/move.model';

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

  constructor(private gameService: GameService, private activatedRoute: ActivatedRoute,private router: Router) {
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.gameId = params['id'];
    });
    this.getMoves(this.gameId);
  }


  public getMoves(id: string): void {
    this.gameService.showMoves(id).subscribe(res => {
      this.moves = res.roundViewModels;
    },
    err =>{
      this.router.navigate(
        ["error"]
      );
    })
  }

  public getCard(id: string): void {
    this.gameService.nextMove(id).subscribe(res => {
      this.getMoves(id);
      this.isOver = res;
      this.showResult(this.isOver);
    },
    err =>{
      this.router.navigate(
        ["error"]
      );
    })
  }

  public getRestOfCards(id: string): void {
    this.gameService.dealRestOfCards(id).subscribe(res => {
      this.isOver = res;
      this.getMoves(id);
      this.showResult(this.isOver);
    },
    err =>{
      this.router.navigate(
        ["error"]
      );
    })
  }

  public showResult(isFinish:boolean):void {
     if (isFinish) {
      this.gameService.getGameInfo(this.gameId).subscribe(res => {
        console.log(res)
        this.status = res.status     
         return
      },
      err =>{
        this.router.navigate(
          ["error"]
        );
      })
     }
     this.status = '';
  }

}
