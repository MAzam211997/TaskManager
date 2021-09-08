import { CompletedAssignmentsComponent } from './Completed_Assignments/Completed_Assignments.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { ConfigurationRoutingModule } from "./configuration-routing.module";
import { SharedModule } from "src/app/shared/shared.module";
import { UsersComponent } from "./users/users.component";
import { DeliverablesComponent } from "./deliverables/deliverables.component";
import { FormGroup, ReactiveFormsModule, FormsModule } from "@angular/forms";
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { DeliverableAssignmentsComponent } from './deliverable_Assignments/deliverable-assignments.component';
import { NgxTypeaheadModule } from 'ngx-typeahead';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { LetterHeadComponent } from './letterhead/letterhead.component';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { ArchivedAssignmentsComponent } from './Archived_Assignments/Archived_Assignments.component';
import { DeliverableAssignmentsAdminComponent } from './deliverable-Assignments-Admin/deliverable-assignments-admin.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ConfigurationRoutingModule,
    SharedModule,
    FormsModule,
    CKEditorModule,
    NgxTypeaheadModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    TypeaheadModule.forRoot()
  ],
  declarations: [
    UsersComponent,
    DeliverablesComponent,
    DeliverableAssignmentsComponent,
    LetterHeadComponent,
    DeliverableAssignmentsAdminComponent,
    LetterHeadComponent,
    CompletedAssignmentsComponent,
    ArchivedAssignmentsComponent
  ],
  providers: []
})

export class ConfigurationModule {

}

