import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPlayer } from '../models/player.model';


@Injectable()

export class HistoryService {
  constructor(private http: HttpClient) { }


  public getUsers(): Observable<IPlayer[]> {
    return this.http.get<IPlayer[]>('http://localhost:61994/api/History/GetPlayers');
  }

  public getAllPlayerGames(body: IPlayer) {
    return this.http.post('http://localhost:61994/api/History/GetAllPlayerGames', body);
  }

  public getAllRounds(id: any) {
    return this.http.get('http://localhost:61994/api/History/GetAllMovesForCurrentGame/' + id);
  }
}
