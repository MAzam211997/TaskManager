import { NgModule } from '@angular/core';
import {
  Routes,
  RouterModule
} from '@angular/router';

import { TasksComponent } from './tasks.component';
/*import { AdminLayoutComponent } from 'src/app/layouts/admin';*/
import { AuthGuard } from '../authentication/authentication-helpers/auth.guard';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    component: TasksComponent,
    data: {
      title: 'Tasks'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TasksRoutingModule { }
