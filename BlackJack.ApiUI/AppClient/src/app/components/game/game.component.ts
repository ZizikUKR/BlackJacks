import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/shared/services/Game.service';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})

export class GameComponent implements OnInit {
  public users = [];
  public moves: object[];
  public gameId: string = '';
  public isOver = false;
  public status = '';

  constructor(private testService: GameService, private activatedRoute: ActivatedRoute) {

  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.gameId = params['id'];
    });
    this.getMoves(this.gameId);
  }


  public getMoves(id: any) {
    this.testService.showMoves(id).subscribe((res: any) => {
      this.moves = res.Rounds

    })
  }

  public getCard(id:any){
   this.testService.nextMove(id).subscribe((res:any)=>{      
      this.getMoves(id);
      this.isOver = res;
      this.showResult(this.isOver);
   })
  }

  public getRestOfCards(id:any){
    this.testService.dealRestOfCards(id).subscribe((res:any)=>{
      this.isOver = res;
      this.getMoves(id);
      this.showResult(this.isOver);
    })  
  }

  public showResult(isFinish:boolean){
    if(isFinish===true){
      this.testService.getGameInfo(this.gameId).subscribe((res:any)=>{
        this.status = res.Status;
      })
    }                                                                                
  }

}
