import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IStartGame } from '../models/startgame.model';
import { Observable } from 'rxjs';
import { IPlayer } from '../models/player.model';
import { environment } from 'src/app/environments/environment';

@Injectable()

export class RegisterService {
    constructor(private http: HttpClient) { }

    private registerApiUrl = '/api/Register/';

    public getUsers(): Observable<IPlayer[]> {
        return this.http.get<IPlayer[]>(environment.apiUrl +this.registerApiUrl+ 'GetAllUser');
    }
    public startGame(model: IStartGame): Observable<Object> {
        return this.http.post(environment.apiUrl +this.registerApiUrl+ 'StartGame', model);
    }
}