import { Component, OnInit, HostListener, ElementRef, ViewChild } from '@angular/core';
import { BusinessServices } from 'src/app/services/singleton/business-services';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from 'src/app/services/loader/LoaderService';
import { ResponseModel } from 'src/app/models/SharedModels/response.model';
import { ActivatedRoute } from '@angular/router';
import { Users } from '../../../models/Users/users.model';
import { SmartAppUtilities } from '../../../models/common/getjsonfrom-controls-service-model';
import { ApplicationCodes } from 'src/app/shared/constent/applicationcodes';

declare var $: any;
@Component({
  selector: 'app-users',
  templateUrl: './users.component.html'
})

export class UsersComponent implements OnInit {
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
  model: Users = new Users();
  smartUtilities = new SmartAppUtilities(this.toastrService);
  @ViewChild('uploadFile', { static: false }) uploadFile: ElementRef;

  constructor(private businessService: BusinessServices, private toastrService: ToastrService,
    private loaderService: LoaderService, route: ActivatedRoute) {

  }

  ngOnInit() {
    this.loaderService.display(true);
    this.businessService.commonService.Get(this.applicationCodes.Roles).subscribe
      ((data: any) => {
        this.loaderService.display(false);
        this.rolesObject = data.filter(x => x.tblName == this.applicationCodes.Roles);
      });
    this.LoadParentGrid();
  }

  LoadParentGrid() {
    this.loaderService.display(true);
    this.businessService.userService.GetAll(this.currentPage, this.paginationPageSize, this.searchText).subscribe(data => {
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
      this.LoadParentGrid();
    }
    else if (e.target.value == '' || e.target.value == null) {
      this.LoadParentGrid();
    }
  }

  SetInitial() {
    var str = this.model.FullName;
    var matches = str.match(/\b(\w)/g);
    var Initial = matches.join('');
    this.model.Initial = Initial;
  }

  ShowHideButton() {
    this.showMainContent = !this.showMainContent;
    setTimeout(() => {
      this.ClearFormControls();
    }, 300);
  }

  dispalyGrid() {
    this.ShowHideButton();
    this.LoadParentGrid();
    this.showMainContent = true;
  }

  ClearFormControls() {
    this.model = new Users();
    this.model.IsActive = true;
    this.model.IsInserting = true;
  }

  nextPage() {
    this.currentPage++;
    this.LoadParentGrid();
  }

  previousPage() {
    this.currentPage--;
    this.LoadParentGrid();
  }

  setCurrentPage(cPage) {
    this.currentPage = cPage;
    this.LoadParentGrid();
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

  onDeletClick(user) {
    if (confirm("Are you sure to delete this record?")) {
      this.loaderService.display(true);
      this.businessService.userService.DeleteRec(user.userID, user.signature).subscribe(data => {
        if (data._statusCode === 200) {
          this.toastrService.success(data._message);
          this.currentPage = 1;
          this.LoadParentGrid();
        } else
          this.toastrService.error(data._message);

        this.loaderService.display(false);
      });
    }
  }

  onDetailClick(user) {
    this.model.UserID = user.userID;
    this.model.EmailAddress = user.emailAddress;
    this.model.FullName = user.fullName;
    this.model.IsActive = user.isActive;
    this.model.IsInserting = false;
    this.model.Mobile = user.mobile;
    this.model.Password = user.password;
    this.model.CurrentPassword = user.password;
    this.model.RoleID = user.roleID;
    this.model.Initial = user.initial;
    this.model.OldFile = user.signature;
    this.showMainContent = !this.showMainContent;
    this.model.IsSigningPartner = user.isSigningPartner;
    this.model.Signature = user.signature;
    this.model.ArabicName = user.arabicName;
  }

  addUpdateUser() {
    this.loaderService.display(true);
    if (!this.smartUtilities.ValidateForm('#ContainerFormFields')) {
      this.loaderService.display(false);
      return;
    }
    if (!this.smartUtilities.ValidateEmail(this.model.EmailAddress)) {
      this.toastrService.error("Please correct Email Address.");
      this.loaderService.display(false);
      return;
    }
    this.model.IsInserting = true;

    if (this.model.UserID > 0) {
      this.model.IsInserting = false;
    }

    this.businessService.userService.SaveRec(this.model).subscribe(
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

  UploadSignature(table, columnName, files: FileList) {
    if (files.length) {
      this.model.IsInserting = false;
      this.model.Signature = files.item(0).name;

      var acceptableExt = ["png", "jpg", "jpeg", "PNG"];
      if (!acceptableExt.includes(this.model.Signature.split('.')[1])) {
        this.toastrService.error("Please Upload an image.");
        this.model.Signature = "";
        return;
      }

      this.model.Sign_File = files.item(0);
      this.loaderService.display(true);
      this.businessService.commonService.IsFieldValueDuplicated(table, columnName, this.model.Sign_File.name)
        .subscribe(data => {
          if (data._statusCode === 200) {
            this.toastrService.error("This file is already exist.");
            this.model.Sign_File = null;
            this.uploadFile.nativeElement.value = "";
          }
          if (data._message == "Value duplicate") {
            this.model.Signature = "";
          }

          this.loaderService.display(false);
        });
    } else {
      this.model.Signature = "";
    }
  }
}
