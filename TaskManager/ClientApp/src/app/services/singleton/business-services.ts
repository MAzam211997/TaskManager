import { DeliverableCommentsRepository } from './../../repositories/deliverable-comments-repository/DeliverableCommentsRepository';
import { DeliverableAssignmentRepository } from './../../repositories/deliverable-assignment-repository/DeliverableAssignmentRepository';
import { Injectable, Injector } from "@angular/core";
import { UsersRepository } from "src/app/repositories/users-repository/UsersRepository";
import { CommonRepository } from "../../repositories/common-repository/CommonRepository";
import { DeliverableRepository } from "../../repositories/deliverable-repository/DeliverableRepository";
import { LoginRepository } from "../../repositories/login-repository/LoginRepository";
import { LetterheadRepository } from '../../repositories/letterhead-repository/LetterheadRepository';
import { DeliverableAssignmentAdminRepository } from '../../repositories/deliverable-assignment-admin-repository/DeliverableAssignmentAdminRepository';

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

  private _deliverableService: DeliverableRepository;
  public get deliveralbeService(): DeliverableRepository {
    if (!this._deliverableService) {
      this._deliverableService = this.injector.get(DeliverableRepository);
    }
    return this._deliverableService;
  }

  private _deliverableAssignmentService: DeliverableAssignmentRepository;
  public get deliverableAssignmentService(): DeliverableAssignmentRepository {
    if (!this._deliverableAssignmentService) {
      this._deliverableAssignmentService = this.injector.get(DeliverableAssignmentRepository);
    }
    return this._deliverableAssignmentService;
  }

  private _deliverableCommentsService: DeliverableCommentsRepository;
  public get deliverableCommentsService(): DeliverableCommentsRepository {
    if (!this._deliverableCommentsService) {
      this._deliverableCommentsService = this.injector.get(DeliverableCommentsRepository);
    }
    return this._deliverableCommentsService;
  }

  private _letterheadService: LetterheadRepository;
  public get letterheadService(): LetterheadRepository {
    if (!this._letterheadService) {
      this._letterheadService = this.injector.get(LetterheadRepository);
    }
    return this._letterheadService;
  }

  private _deliverableAssignmentAdminService: DeliverableAssignmentAdminRepository;
  public get deliverableAssignmentAdminService(): DeliverableAssignmentAdminRepository {
    if (!this._deliverableAssignmentAdminService) {
      this._deliverableAssignmentAdminService = this.injector.get(DeliverableAssignmentAdminRepository);
    }
    return this._deliverableAssignmentAdminService;
  }
}
