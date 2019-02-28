import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IStartGame } from '../models/startgame.model';
import { Observable } from 'rxjs';
import { IPlayer } from '../models/player.model';
import { Move } from '../models/move.model';
import { GameInformation } from '../models/game-info.model';
import { environment } from 'src/app/environments/environment';


@Injectable()

export class GameService {

  constructor(private http: HttpClient) { }

  private gameApiUrl = '/api/Game/';
  
  public showMoves(id: string): Observable<Move[]> {
  
    return this.http.get<Move[]>(environment.apiUrl+this.gameApiUrl+'GetFirsTwoMoves/' + id)
  }

  public nextMove(id: string) : Observable<boolean> {
    return this.http.get<boolean>(environment.apiUrl+this.gameApiUrl+'NextRoundForPlayer/' + id)
  }

  public dealRestOfCards(id: string) : Observable<boolean> {
    return this.http.get<boolean>(environment.apiUrl+this.gameApiUrl+'DealRestOfCards/' + id)
  }

  public getGameInfo(id: string) {
    return this.http.get(environment.apiUrl+this.gameApiUrl+'GameResult/' + id)
  }

}
