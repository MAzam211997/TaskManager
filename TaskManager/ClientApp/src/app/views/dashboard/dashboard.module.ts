import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  imports: [    
    DashboardRoutingModule,    
    CommonModule,
    SharedModule
],
  declarations: [DashboardComponent ]
})
export class DashboardModule { }
