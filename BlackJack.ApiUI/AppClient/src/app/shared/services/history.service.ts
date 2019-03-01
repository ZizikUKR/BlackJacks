import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GameInformation } from '../models/game-info.model';
import { environment } from 'src/app/environments/environment';
import { IPlayer } from 'src/app/shared/models/player.model';
import { PlayersList } from '../models/players-list.model';
import { MoveList } from '../models/moves-list.model';


@Injectable()

export class HistoryService {
    constructor(private http: HttpClient) {}
    private historyApiUrl = '/api/History/';

    public getUsers() : Observable<PlayersList>{
         return this.http.get<PlayersList>(environment.apiUrl+this.historyApiUrl+'GetPlayers');
    }

    public getAllPlayerGames(body:IPlayer): Observable<GameInformation[]>{
        console.log(body);
        return this.http.post<GameInformation[]>(environment.apiUrl+this.historyApiUrl+ 'GetAllPlayerGames', body);
    }

    public getAllRounds(id:string):Observable<MoveList>{
        return this.http.get<MoveList>(environment.apiUrl+this.historyApiUrl+'GetAllMovesForCurrentGame/'+id);
    }
}