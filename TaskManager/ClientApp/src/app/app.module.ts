import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TasksRepository } from './repositories/TasksRepository/TasksRepository';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { UsersComponent } from './views/configuration/users/users.component';
import { CommonRepository } from './repositories/CommonRepository/CommonRepository';
import { BusinessServices } from './services/singleton/business-services';
import { TasksComponent } from './views/tasks/tasks.component';
import { SharedModule } from './shared/shared.module';
import { AppRoutingModule } from './app-routing.module';
import { LoginRepository } from './repositories/LoginRepository/LoginRepository';
import { LoaderService } from './services/loader/LoaderService';
import { Globals } from '../environments/Globals';
import { UsersRepository } from './repositories/UsersRepository/UsersRepository';
import { P404Component } from './views/error/404.component';
import { P401Component } from './views/error/401.component';
import { LoginComponent } from './views/authentication/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TasksComponent,
    P404Component,
    P401Component,
    LoginComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    SharedModule,
    BrowserAnimationsModule,
    TypeaheadModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 2000,
      positionClass: 'toast-top-center',
      preventDuplicates: true,
      closeButton: true,
      enableHtml: true
    }),
  ],
  providers: [  TasksRepository
              , CommonRepository
    , BusinessServices,
    CommonRepository,
    LoginRepository,
    UsersRepository,
    LoaderService,
    Globals,
             ],
  bootstrap: [AppComponent]
})
export class AppModule { }
