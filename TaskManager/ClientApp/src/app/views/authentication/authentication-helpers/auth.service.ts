import { Injectable } from '@angular/core';    
import { Router } from '@angular/router';
import { BusinessServices } from 'src/app/services/singleton/business-services';
    
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private router: Router, private businessSerivce: BusinessServices) {

  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/Login']);
  }

  resetServerSessionTime(): void {
    this.businessSerivce.commonService.RestartSessionTime().subscribe(data => {
      console.log("Session Re-Activated");
    });

  }
}
