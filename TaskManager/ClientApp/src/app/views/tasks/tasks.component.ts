import { Component, AfterViewInit, OnDestroy, ElementRef, ViewChild } from '@angular/core';
import { BusinessServices } from 'src/app/services/singleton/business-services';
import { ResponseModel } from 'src/app/models/SharedModels/response.model';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from 'src/app/services/loader/LoaderService';
import { Globals } from 'src/environments/Globals';
import { CommonConstant } from '../../shared/constent/applicationcodes';
declare var $: any;

@Component({
  templateUrl: 'tasks.component.html'
})
export class TasksComponent implements AfterViewInit, OnDestroy {
   
    commonconstant = new CommonConstant();
  constructor(private businessService: BusinessServices, private loaderService: LoaderService, private globals: Globals) {
    console.log("Called te cons")
    }
    ngOnDestroy(): void {
        $("body").removeClass("dashboard-body");
    }

    canvas: any;
    ctx: any;

    ngAfterViewInit(): void {

    }
}
