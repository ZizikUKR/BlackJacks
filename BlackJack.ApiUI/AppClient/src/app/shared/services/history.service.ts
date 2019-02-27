import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPlayer } from '../models/player.model';
import { GameInformation } from '../models/game-info.model';
import { Move } from '../models/move.model';
import { environment } from 'src/app/environments/environment';


@Injectable()

export class HistoryService {
    constructor(private http: HttpClient) {}
    private historyApiUrl = '/api/History/';

    public getUsers() : Observable<IPlayer[]>{
         return this.http.get<IPlayer[]>(environment.apiUrl+this.historyApiUrl+'GetPlayers');
    }

    public getAllPlayerGames(body:IPlayer): Observable<GameInformation[]>{
        return this.http.post<GameInformation[]>(environment.apiUrl+this.historyApiUrl+ 'GetAllPlayerGames', body);
    }

    public getAllRounds(id:string):Observable<Move[]>{
        return this.http.get<Move[]>(environment.apiUrl+this.historyApiUrl+'GetAllMovesForCurrentGame/'+id);
    }
}