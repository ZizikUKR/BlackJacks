import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/shared/services/game.service';
import { Router } from '@angular/router';
import { IStartGame } from 'src/app/shared/models/startgame.model';
import { IPlayer } from 'src/app/shared/models/player.model';
import { RegisterService } from 'src/app/shared/services/register.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {


  private users: IPlayer[];
  private countOfBots: string;
  private userName: string;
  private idGame: string;

  constructor(private gameService: GameService, private router: Router, private registerService: RegisterService) {
    this.users = [];
  }

  ngOnInit(): void {
    this.getUsers();
  }

  public getUsers(): void {
    this.registerService.getUsers().subscribe((res: IPlayer[]) => {
      this.users = res
    })
  }

  public registration(): void {
    let model : IStartGame = {
      userName : this.userName,
      countOfBots : parseInt(this.countOfBots)
    }

    this.registerService.startGame(model).subscribe((res: any) => {   
      this.router.navigate(
        ['/game', res]
      );
    })
  }
 

}
