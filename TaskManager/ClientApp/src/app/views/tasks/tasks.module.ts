import { NgModule } from '@angular/core';
import { TasksRoutingModule } from './tasks-routing.module';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TasksComponent } from './tasks.component';

@NgModule({
  imports: [    
    TasksRoutingModule,
    CommonModule,
    SharedModule
  ],
  declarations: [TasksComponent]
})
export class TasksModule { }
