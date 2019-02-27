import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HistoryComponent } from './history.component';


const routes: Routes = [
    {
        path: '',
        component: HistoryComponent
    },
    {
        path: 'gameRounds/:id',
        loadChildren: '../history-rounds/history-rounds.module#HistoryRoundsModule'
    }];

@NgModule({
    imports: [ RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class HistoryRoutingModule { }