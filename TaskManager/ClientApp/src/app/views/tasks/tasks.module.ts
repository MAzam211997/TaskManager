import { NgModule } from '@angular/core';
import { TasksRoutingModule } from './tasks-routing.module';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TasksComponent } from './tasks.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [    
    TasksRoutingModule,
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
],
  declarations: [],
  providers: []
})
export class TasksModule { }
