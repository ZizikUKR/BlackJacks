import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPlayer } from '../models/player.model';
import { environment } from 'src/environments/environment';
import { Moves } from '../models/moves.model';


@Injectable()

export class GameService {

  constructor(private http: HttpClient) { }

  private gameApiUrl = '/api/Game/';
  
  public showMoves(id: string): Observable<Moves> {
  
    return this.http.get<Moves>(environment.apiUrl+this.gameApiUrl+'GetFirstTwoMoves/' + id)
  }

  public nextMove(id: string) : Observable<boolean> {
    return this.http.get<boolean>(environment.apiUrl+this.gameApiUrl+'NextRoundForPlayer/' + id)
  }

  public dealRestOfCards(id: string) : Observable<boolean> {
    return this.http.get<boolean>(environment.apiUrl+this.gameApiUrl+'DealRestOfCards/' + id)
  }

  public getGameInfo(id: string): Observable<IPlayer> {
    return this.http.get<IPlayer>(environment.apiUrl+this.gameApiUrl+'GameResult/' + id)
  }

}
