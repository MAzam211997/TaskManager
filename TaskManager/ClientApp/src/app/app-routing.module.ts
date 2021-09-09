import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { P404Component } from './views/error/404.component';
import { AuthGuard } from './views/authentication/authentication-helpers/auth.guard';
import { P401Component } from './views/error/401.component';
// import { POSTailorLayoutComponent } from './layouts/postailor/postailor-layout.component';
import { LoginComponent } from './views/authentication/login/login.component';
import { CanDeactivateGuard } from './views/authentication/authentication-helpers/auth.can-deactivate-guard';
import { RedirectGuard } from './guards/redirect-guard';
import { TasksComponent } from './views/tasks/tasks.component';

export const AppRoutes: Routes = [
    { path: '', redirectTo: 'Login', pathMatch: 'full' },
    {
        path: 'Login',
        component: LoginComponent,
  },
  {
    path: 'Tasks',
    component: TasksComponent,
   // loadChildren: './views/dashboard/dashboard.module#DashboardModule',
    data: {
      breadcrumb: 'Tasks'
    }
  },
    { path: 'NoAccess', component: P401Component },
    { path: '**', component: P404Component }
];

@NgModule({
    imports: [RouterModule.forRoot(AppRoutes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
