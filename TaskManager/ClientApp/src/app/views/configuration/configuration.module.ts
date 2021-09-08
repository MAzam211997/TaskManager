import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { ConfigurationRoutingModule } from "./configuration-routing.module";
import { SharedModule } from "src/app/shared/shared.module";
import { UsersComponent } from "./users/users.component";
import { FormGroup, ReactiveFormsModule, FormsModule } from "@angular/forms";
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { NgxTypeaheadModule } from 'ngx-typeahead';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';

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
    UsersComponent
  ],
  providers: []
})

export class ConfigurationModule {

}

