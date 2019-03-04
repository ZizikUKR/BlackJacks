import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IStartGame } from 'src/app/shared/models/start-game.model';
import { IPlayer } from 'src/app/shared/models/player.model';
import { RegisterService } from 'src/app/shared/services/register.service';
import { error } from 'util';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  
  private users: IPlayer[];
  private countOfBots: string;
  private userName: string;

  constructor(private router: Router, private registerService: RegisterService) {
    this.users = [];
  }

  ngOnInit(): void {
    this.getUsers();
  }

  public getUsers(): void {
    this.registerService.getUsers().subscribe(res => {
      this.users = res.playerViewModels
    })
  }

  public registration(): void {
    let model : IStartGame = {
      userName : this.userName,
      countOfBots : parseInt(this.countOfBots)
    }
    
    this.registerService.startGame(model).subscribe(res => {   
      this.router.navigate(
        ['/game', res]
      );
    })
  }
}
