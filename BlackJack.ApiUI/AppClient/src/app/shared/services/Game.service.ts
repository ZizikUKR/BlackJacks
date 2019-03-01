import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/app/environments/environment';
import { MoveList } from '../models/moves-list.model';


@Injectable()

export class GameService {

  constructor(private http: HttpClient) { }

  private gameApiUrl = '/api/Game/';
  
  public showMoves(id: string): Observable<MoveList> {
  
    return this.http.get<MoveList>(environment.apiUrl+this.gameApiUrl+'GetFirstTwoMoves/' + id)
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
