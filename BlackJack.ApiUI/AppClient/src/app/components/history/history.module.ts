import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HistoryComponent } from './history.component';
import { HistoryRoutingModule } from './history-routing.module';

@NgModule({
    declarations: [
        HistoryComponent
    ],
    imports: [
        FormsModule,
        CommonModule,
        ReactiveFormsModule,
        HistoryRoutingModule
    ]
})
export class HistoryModule {}