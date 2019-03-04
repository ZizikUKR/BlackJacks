import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HistoryService } from 'src/app/shared/services/history.service';
import { Move } from 'src/app/shared/models/move.model';

@Component({
  selector: 'app-history-rounds',
  templateUrl: './history-rounds.component.html',
  styleUrls: ['./history-rounds.component.css']
})
export class HistoryRoundsComponent implements OnInit {

  public rounds:Move[];
  public gameId = '';
  constructor(private activatedRoute: ActivatedRoute, private historyService:HistoryService, private router: Router) 
  { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params=>{
      this.gameId = params['id'];
      this.loadRounds(this.gameId);
    })
  }

  public loadRounds(id:string){
    this.historyService.getAllRounds(id).subscribe(res=>{
      this.rounds = res.roundViewModels;
    })
  }
  
}
