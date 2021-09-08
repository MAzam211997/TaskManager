import { ArchivedAssignmentsComponent } from './Archived_Assignments/Archived_Assignments.component';
import { CompletedAssignmentsComponent } from './Completed_Assignments/Completed_Assignments.component';
import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { UsersComponent } from './users/users.component';
import { AuthGuard } from "../authentication/authentication-helpers/auth.guard";
import { DeliverablesComponent } from "./deliverables/deliverables.component";
import { DeliverableAssignmentsComponent } from "./deliverable_Assignments/deliverable-assignments.component";
import { LetterHeadComponent } from "./letterhead/letterhead.component";
import { DeliverableAssignmentsAdminComponent } from "./deliverable-Assignments-Admin/deliverable-assignments-admin.component";


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
          },
          {
            path: 'Deliverables',
            canActivate: [AuthGuard],
            component: DeliverablesComponent,
            data: {
              breadcrumb: 'Deliverables',
              description: "Deliverables"
            }
          },
          {
            path: 'Deliverable_Assignments',
            canActivate: [AuthGuard],
            component: DeliverableAssignmentsComponent,
            data: {
              breadcrumb: 'Deliverable Assignments',
              description: "Deliverable Assignments"
            }
          },

          {
            path: 'DeliverableAssignmentsAdmin',
            canActivate: [AuthGuard],
            component: DeliverableAssignmentsAdminComponent,
            data: {
              breadcrumb: 'Deliverable Assignments Admin',
              description: "Deliverable Assignments Admin"
            }
          },
          {
            path: 'Letterheads',
            canActivate: [AuthGuard],
            component: LetterHeadComponent,
            data: {
              breadcrumb: 'Letterheads',
              description: "Letterheads"
            }
          },
          {
            path: 'Completed_Assignments',
            canActivate: [AuthGuard],
            component: CompletedAssignmentsComponent,
            data: {
              breadcrumb: 'Completed_Assignments',
              description: "Completed_Assignments"
            }
          },
          {
            path: 'Archived_Assignments',
            canActivate: [AuthGuard],
            component: ArchivedAssignmentsComponent,
            data: {
              breadcrumb: 'Archived_Assignments',
              description: "Archived_Assignments"
            }
          },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(ConfigurationRoutes)],
    exports: [RouterModule]
})

export class ConfigurationRoutingModule { }
