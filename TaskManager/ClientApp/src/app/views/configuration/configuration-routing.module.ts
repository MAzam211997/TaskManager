import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { UsersComponent } from './users/users.component';
import { AuthGuard } from "../authentication/authentication-helpers/auth.guard";


export const ConfigurationRoutes: Routes = [
    {
        path: '',
        children: [
          {
            path: 'Users',
            canActivate: [AuthGuard],
            component: UsersComponent,
            data: {
              breadcrumb: 'Users',
              description: "Users"
            }
          }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(ConfigurationRoutes)],
    exports: [RouterModule]
})

export class ConfigurationRoutingModule { }
