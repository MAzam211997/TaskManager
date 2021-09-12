import { NgModule } from '@angular/core';
import { TasksRoutingModule } from './tasks-routing.module';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TasksComponent } from './tasks.component';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { NgxTypeaheadModule } from 'ngx-typeahead';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [    
    TasksRoutingModule,
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    CKEditorModule,
    NgxTypeaheadModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    TypeaheadModule.forRoot()
  ],
  declarations: [TasksComponent]
})
export class TasksModule { }
