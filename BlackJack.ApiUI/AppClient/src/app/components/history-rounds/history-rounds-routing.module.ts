import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HistoryRoundsComponent } from './history-rounds.component';




const routes: Routes = [{
  path: '',
  component: HistoryRoundsComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HistoryRoundsRoutingModule {}