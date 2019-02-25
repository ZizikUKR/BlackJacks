import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable()

export class GameService {

  constructor(private http: HttpClient) {}

  public getUsers() {
      return this.http.get('http://localhost:61994/api/Register/GetAllUser' );
  }
  public startGame(body: any) {
    return this.http.post('http://localhost:61994/api/Register/StartGame', [body.username, body.countOfBots] );
  }
  public showMoves(id:any){

    return this.http.get('http://localhost:61994/api/Game/GetFirsTwoMoves/' + id)
  }

  public nextMove(id:any){
    return this.http.get('http://localhost:61994/api/Game/NextRoundForPlayer/'+ id)
  }

  public dealRestOfCards(id:any){
    console.log(id)
      return this.http.get('http://localhost:61994/api/Game/DealRestOfCards/'+ id)
  }

  public getGameInfo(id:any){
    return this.http.get('http://localhost:61994/api/Game/GameResult/'+id)
  }

}
