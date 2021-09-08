import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { Login } from "../../../models/Login/Login.model";
import { BusinessServices } from "../../../services/singleton/business-services";
import { LoaderService } from "src/app/services/loader/LoaderService";
import { ToastrService } from 'ngx-toastr';
import { Globals } from "src/environments/Globals";
import { formatDate } from '@angular/common';
import { AuthService } from "../authentication-helpers/auth.service";
import { ApplicationConstants, CommonConstant } from "../../../shared/constent/applicationcodes";


declare var $: any;

@Component({
  selector: 'app-login',
  templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit {
  commonconstant = new CommonConstant();
  login = new Login();
  currentDate: string = "";
  counter: number = 0;
  showMainContent: Boolean = true;
  nameForget: string = "";

  constructor(private loaderService: LoaderService, private route: Router,
    private loginBusinessServices: BusinessServices, private activatedRoute: ActivatedRoute,
    private touster: ToastrService, private authService: AuthService, private globals: Globals) {
    this.currentDate = formatDate(new Date(), 'dd/MM/yyyy HH:mm', 'en');
  }
  ngOnInit() {

    if (localStorage.getItem('IsLoggingOut') === "true") {
      this.touster.error("Session has been expired");
      this.authService.logout();
    }
    $("body").addClass("loginbody");
    $("html").removeAttr("dir");

  }
  ShowHideButton() {
    this.showMainContent = !this.showMainContent;
  }

  dispalyGrid() {
    this.showMainContent = true;
  }

  onSubmit() {
    this.loaderService.display(true);
    try {
      this.loginBusinessServices.loginService.Authentication(this.login).subscribe((data: any) => {
        this.loaderService.display(false);
        if (data && data._obj) {
          console.log(data);
          if (data._statusCode === 200) {
            this.globals.resetSessionTimeOut();
            this.globals.resetServerSessionTimeOut();
            this.touster.success(data._message);
            localStorage.setItem('isLoggedIn', "true");
            this.globals.setDataInLocalStorage(this.commonconstant.MobileNo, data._obj.mobile);
            this.globals.setDataInLocalStorage(this.commonconstant.UserRoleID, data._obj.roleID);
            this.globals.setDataInLocalStorage(this.commonconstant.IsSigningPartner, data._obj.isSigningPartner);
            this.globals.setDataInLocalStorage(this.commonconstant.UserRoleName, data._obj.roleName);
            this.globals.setDataInLocalStorage(this.commonconstant.FullName, data._obj.fullName);
            this.globals.setDataInLocalStorage(this.commonconstant.Initial, data._obj.initial);
            this.globals.setDataInLocalStorage(this.commonconstant.UserID, data._obj.userID);

            this.touster.success(data._message);
            var userRoleID = localStorage.getItem('userRoleID');

            if (userRoleID == "1") {
              this.route.navigate(['Configuration/Users']);
            } else {
              this.route.navigate(['Configuration/Deliverable_Assignments']);
            }

            //if (data._obj.UserInformation.DefaultPage)
            //  this.route.navigate([data._obj.UserInformation.DefaultPage]);
            //else
            //  this.route.navigate(['Dashboard']);


          } else {

            this.touster.error(data._message);
          }
        } else {

          this.touster.error(data._message);
        }


      });
    } catch (error) {
      console.log(error);
      this.touster.error(error);
    }
  }


  ForgotPassword() {
    this.loaderService.display(true)
    this.login.EmailAddress = this.nameForget;
    this.loginBusinessServices.loginService.AuthenticateForGotPasswordUser(this.login).subscribe((data: any) => {
      this.loaderService.display(false);

      if (data && data._statusCode === 200) {
        this.touster.success(data._message);
      }
      else {
        this.touster.error(data._message);
      }

    })
  }
}


