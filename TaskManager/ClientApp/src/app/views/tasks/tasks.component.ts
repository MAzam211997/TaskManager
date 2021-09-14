import { Component, AfterViewInit, OnDestroy, ElementRef, ViewChild, OnInit } from '@angular/core';
import { BusinessServices } from 'src/app/services/singleton/business-services';
import { ResponseModel } from 'src/app/models/SharedModels/response.model';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from 'src/app/services/loader/LoaderService';
import { Globals } from 'src/environments/Globals';
import { ApplicationCodes, CommonConstant } from '../../shared/constent/applicationcodes';
import { SmartAppUtilities } from '../../models/common/getjsonfrom-controls-service-model';
import { tasks } from '../../models/tasks/tasks.model';
import { ActivatedRoute } from '@angular/router';
declare var $: any;

@Component({
  templateUrl: 'tasks.component.html'
})
export class TasksComponent implements OnInit {
  applicationCodes = new ApplicationCodes();
  rolesObject: any; // for Roles dropdown
  currentPage: number = 1;
  paginationPageSize = 10;
  totalRecord: number = 0;
  totalPages: number = 0;
  showingPage: string = "";
  pageInfo: string = "";
  searchText: string = "";
  usersObject: any;
  showMainContent: Boolean = true;
  model: tasks = new tasks();
  smartUtilities = new SmartAppUtilities(this.toastrService);
  @ViewChild('uploadFile', { static: false }) uploadFile: ElementRef;

  constructor(private businessService: BusinessServices, private toastrService: ToastrService,
    private loaderService: LoaderService, route: ActivatedRoute) {

  }

  ngOnInit() {
    this.loaderService.display(true);
    //this.businessService.commonService.Get(this.applicationCodes.Roles).subscribe
    //  ((data: any) => {
    //    this.loaderService.display(false);
    //    this.rolesObject = data.filter(x => x.tblName == this.applicationCodes.Roles);
    //  });
    //this.LoadParentGrid();
  }

  LoadParentGrid() {
    this.loaderService.display(true);
    this.businessService.taskService.GetAll(this.currentPage, this.paginationPageSize, this.searchText).subscribe(data => {
      try {
        if (data) {
          this.usersObject = data._obj;
          this.totalRecord = data._obj[0]["total"];
          this.totalPages = Math.ceil(this.totalRecord / this.paginationPageSize);
          setTimeout(x => {
            this.setPagingValues(this.currentPage);
          }, 400)
        }
        this.loaderService.display(false);
      } catch {
        this.loaderService.display(false);
      }
    });
  }

  onSearchkeyUp(e) {
    this.currentPage = 1;
    if (e.keyCode === 13 && e.target.value !== '') {
      //this.LoadParentGrid();
    }
    else if (e.target.value == '' || e.target.value == null) {
      //this.LoadParentGrid();
    }
  }


  ShowHideButton() {
    this.showMainContent = !this.showMainContent;
    setTimeout(() => {
      this.ClearFormControls();
    }, 300);
  }

  dispalyGrid() {
    this.ShowHideButton();
    ////this.LoadParentGrid();
    this.showMainContent = true;
  }

  ClearFormControls() {
    this.model = new tasks();
    this.model.IsActive = true;
    this.model.IsInserting = true;
  }

  nextPage() {
    this.currentPage++;
    //this.LoadParentGrid();
  }

  previousPage() {
    this.currentPage--;
    //this.LoadParentGrid();
  }

  setCurrentPage(cPage) {
    this.currentPage = cPage;
    //this.LoadParentGrid();
  }

  setPagingValues(cPage) {
    $("#btPrevious").removeClass("disabled");
    $("#btNext").removeClass("disabled");

    if (this.currentPage == 1) {
      $("#btPrevious").addClass("disabled");
      $("#btNext").removeClass("disabled");
    }

    if (this.currentPage == this.totalPages) {
      $("#btPrevious").removeClass("disabled");
      $("#btNext").addClass("disabled");
    }

    if (this.totalPages == 1) {
      $("#btPrevious").addClass("disabled");
      $("#btNext").addClass("disabled");
    }

    for (var i = 1; i <= this.totalPages; i++) {
      $("#page-" + i).removeClass("active");
    }

    setTimeout(() => {
      $("#page-" + this.currentPage).addClass("active");
    }, 100);
  }

  counter(i: number) {
    return new Array(i);
  }

  onDeletClick(task) {
    if (confirm("Are you sure to delete this record?")) {
      this.loaderService.display(true);
      this.businessService.taskService.DeleteRec(task.taskID).subscribe(data => {
        if (data._statusCode === 200) {
          this.toastrService.success(data._message);
          this.currentPage = 1;
          //this.LoadParentGrid();
        } else
          this.toastrService.error(data._message);

        this.loaderService.display(false);
      });
    }
  }

  onDetailClick(task) {
    this.model.TaskId = task.taskId;
    this.model.TaskTitle = task.taskTitle;
    this.model.TaskDescription = task.taskDescription;
    this.model.IsActive = task.isActive;
    this.model.IsInserting = false;
    this.showMainContent = !this.showMainContent;
  }

  addUpdateTask() {
    debugger
    this.loaderService.display(true);
    if (!this.smartUtilities.ValidateForm('#ContainerFormFields')) {
      this.loaderService.display(false);
      return;
    }
    this.model.IsInserting = true;

    if (this.model.TaskId > 0) {
      this.model.IsInserting = false;
    }

    this.businessService.taskService.SaveRec(this.model).subscribe(
      (data: ResponseModel) => {
        this.loaderService.display(false);
        if (data._statusCode == 200) {
          this.toastrService.success(data._message);
          this.dispalyGrid();
        }
        else {
          this.toastrService.error(data._message)
        }
      });

  }

}
