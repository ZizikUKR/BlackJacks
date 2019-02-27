import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HistoryRoundsComponent } from './history-rounds.component';
import { HistoryRoundsRoutingModule } from './history-rounds-routing.module';


@NgModule({
    declarations: [
        HistoryRoundsComponent
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        HistoryRoundsRoutingModule
    ]
})
export class HistoryRoundsModule {}