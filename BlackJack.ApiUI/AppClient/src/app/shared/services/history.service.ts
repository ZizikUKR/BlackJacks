import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable()

export class HistoryService {
    constructor(private http: HttpClient) {}


    public getUsers(){
         return this.http.get('http://localhost:61994/api/History/GetPlayers');
    }

    public getAllPlayerGames(body: any){
        console.log(body)
        return this.http.post('http://localhost:61994/api/History/GetAllPlayerGames', body);
    }

    public getAllRounds(id:any){
        return this.http.get('http://localhost:61994/api/History/GetAllMovesForCurrentGame/'+id);
    }
}