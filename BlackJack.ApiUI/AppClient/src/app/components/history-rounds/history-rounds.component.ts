import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HistoryService } from 'src/app/shared/services/history.service';

@Component({
  selector: 'app-history-rounds',
  templateUrl: './history-rounds.component.html',
  styleUrls: ['./history-rounds.component.css']
})
export class HistoryRoundsComponent implements OnInit {

  public rounds=[];
  public gameId = '';
  constructor(private router: Router, private activatedRoute: ActivatedRoute, private historyService:HistoryService) 
  { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params=>{
      this.gameId = params['id'];
      this.loadRounds(this.gameId);
    })
  }

  public loadRounds(id:string){
    this.historyService.getAllRounds(id).subscribe((res:any)=>{
      console.log(res);
      console.log(res);
      console.log(res);
      this.rounds = res;
    })
  }
  
}