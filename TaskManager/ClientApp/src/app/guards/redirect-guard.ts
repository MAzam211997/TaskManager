import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RedirectGuard implements CanActivate {
    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
      // debugger;
      //   // window.location.href = next.data['StoreURL'];
      //   console.log(next.data['StoreURL']);
      //   window.open(window.location.origin + "/ecom", "_self");
        return true;
    }
}
