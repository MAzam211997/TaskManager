import { Component, AfterViewInit, OnDestroy, ElementRef, ViewChild } from '@angular/core';
import { BusinessServices } from 'src/app/services/singleton/business-services';
import { ResponseModel } from 'src/app/models/SharedModels/response.model';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from 'src/app/services/loader/LoaderService';
import { Globals } from 'src/environments/Globals';
import { CommonConstant } from '../../shared/constent/applicationcodes';
declare var $: any;

@Component({
    templateUrl: 'dashboard.component.html'
})
export class DashboardComponent implements AfterViewInit, OnDestroy {
   
    commonconstant = new CommonConstant();
    constructor(private businessService: BusinessServices, private loaderService: LoaderService, private toastrService: ToastrService, private globals: Globals) {
                
    }
    ngOnDestroy(): void {
        $("body").removeClass("dashboard-body");
    }

    canvas: any;
    ctx: any;

    ngAfterViewInit(): void {

    }
}
