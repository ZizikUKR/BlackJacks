import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IStartGame } from 'src/app/shared/models/start-game.model';
import { Observable } from 'rxjs';
import { environment } from 'src/app/environments/environment';
import { PlayersList } from '../models/players-list.model';

@Injectable()

export class RegisterService {
    constructor(private http: HttpClient) { }

    private registerApiUrl = '/api/Register/';

    public getUsers(): Observable<PlayersList> {
        return this.http.get<PlayersList>(environment.apiUrl +this.registerApiUrl+ 'GetAllUser');
    }
    public startGame(model: IStartGame): Observable<Object> {
        return this.http.post(environment.apiUrl +this.registerApiUrl+ 'StartGame', model);
    }
}