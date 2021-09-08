import { HttpInterceptor, HttpRequest, HttpHandler } from '@angular/common/http';
import { Globals } from 'src/environments/Globals';


export class HttpRequestInterceptorService implements HttpInterceptor {  

  constructor(private globals:Globals) { }

  intercept(request: HttpRequest<any>, next: HttpHandler) {
    console.log('interceptor server time reset')
    this.globals.resetServerSessionTimeOut();
    const customReq = request.clone({});
    return next.handle(customReq);
  }
    

    // return next.handle(request).pipe(
    //   finalize(res => {
    //     this.totalRequests--;
    //     if (this.totalRequests === 0) {
          
    //     }
    //   }));

}