import { Injectable, Injector } from "@angular/core";
import { UsersRepository } from "src/app/repositories/UsersRepository/UsersRepository";
import { LoginRepository } from "src/app/repositories/LoginRepository/LoginRepository";
import { CommonRepository } from "src/app/repositories/CommonRepository/CommonRepository";
@Injectable()
export class BusinessServices {

  constructor(private injector: Injector) { }

  private _loginService: LoginRepository;
  public get loginService(): LoginRepository {
    if (!this._loginService) {
      this._loginService = this.injector.get(LoginRepository);
    }
    return this._loginService;
  }

  private _userService: UsersRepository;
  public get userService(): UsersRepository {
    if (!this._userService) {
      this._userService = this.injector.get(UsersRepository);
    }
    return this._userService;
  }

  private _commonService: CommonRepository;
  public get commonService(): CommonRepository {
    if (!this._commonService) {
      this._commonService = this.injector.get(CommonRepository);
    }
    return this._commonService;
  }

}
