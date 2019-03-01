import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPlayer } from 'src/app/shared/models/player.model';
import { environment } from 'src/environments/environment';
import { FinishGame } from 'src/app/shared/models/finish-game';
import { Players } from '../models/players.model';
import { Moves } from '../models/moves.model';


@Injectable()

export class HistoryService {
    constructor(private http: HttpClient) {}
    private historyApiUrl = '/api/History/';

    public getUsers() : Observable<Players>{
         return this.http.get<Players>(environment.apiUrl+this.historyApiUrl+'GetPlayers');
    }

    public getAllPlayerGames(body:IPlayer): Observable<FinishGame>{
        console.log(body);
        return this.http.post<FinishGame>(environment.apiUrl+this.historyApiUrl+ 'GetAllPlayerGames', body);
    }

    public getAllRounds(id:string):Observable<Moves>{
        return this.http.get<Moves>(environment.apiUrl+this.historyApiUrl+'GetAllMovesForCurrentGame/'+id);
    }
}