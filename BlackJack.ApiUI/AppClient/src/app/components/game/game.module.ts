import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { GameComponent } from './game.component';
import { GameRoutingModule } from './game-routing.module';


@NgModule({
    declarations: [
        GameComponent
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        GameRoutingModule
    ]
})
export class GameModule {}