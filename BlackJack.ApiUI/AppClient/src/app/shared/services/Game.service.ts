import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IStartGame } from '../models/startgame.model';
import { Observable } from 'rxjs';
import { IPlayer } from '../models/player.model';
import { Move } from '../models/move.model';
import { GameInformation } from '../models/game-info.model';


@Injectable()

export class GameService {

  constructor(private http: HttpClient) { }

  public getUsers(): Observable<IPlayer[]> {
    return this.http.get<IPlayer[]>('http://localhost:61994/api/Register/GetAllUser');
  }
  public startGame(model: IStartGame): Observable<Object> {
    return this.http.post('http://localhost:61994/api/Register/StartGame', model);
  }
  public showMoves(id: string): Observable<Move[]> {

    return this.http.get<Move[]>('http://localhost:61994/api/Game/GetFirsTwoMoves/' + id)
  }

  public nextMove(id: string): Observable<boolean> {
    return this.http.get<boolean>('http://localhost:61994/api/Game/NextRoundForPlayer/' + id)
  }

  public dealRestOfCards(id: string): Observable<boolean> {
    return this.http.get<boolean>('http://localhost:61994/api/Game/DealRestOfCards/' + id)
  }

  public getGameInfo(id: string): Observable<GameInformation> {
    return this.http.get<GameInformation>('http://localhost:61994/api/Game/GameResult/' + id)
  }

}
