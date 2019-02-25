import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/shared/services/Game.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


  private users: string[];
  private countOfBots: string;
  private userName: string;
  private idGame: string;

  constructor(private testService: GameService, private router: Router) {

  }

  ngOnInit() {
    this.getUsers();
  }

  public getUsers() {

    this.testService.getUsers().subscribe((res: any) => {
      this.users = res
    })
  }

  public registration() {
    const body = {
      username: this.userName,
      countOfBots: this.countOfBots
    }
    this.testService.startGame(body).subscribe((res: any) => {
      console.log(res)
      this.router.navigate(
        ['/game', res]
      );
    })
  }
 

}
