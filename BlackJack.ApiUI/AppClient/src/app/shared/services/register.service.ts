import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IStartGame } from 'src/app/shared/models/start-game.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Players } from '../models/players.model';

@Injectable()

export class RegisterService {
    constructor(private http: HttpClient) { }

    private registerApiUrl = '/api/Register/';

    public getUsers(): Observable<Players> {
        return this.http.get<Players>(environment.apiUrl +this.registerApiUrl+ 'GetAllUser');
    }
    public startGame(model: IStartGame): Observable<Object> {
        return this.http.post(environment.apiUrl +this.registerApiUrl+ 'StartGame', model);
    }
}