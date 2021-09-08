import { Injectable } from "@angular/core";

import { CanDeactivate } from "@angular/router";
import { Globals } from "src/environments/Globals";
import { CommonConstant } from "src/app/shared/constent/applicationcodes";

@Injectable(
    {
        providedIn: "root"
    }
)
export class CanDeactivateGuard implements CanDeactivate<any> {
    commonConst = new CommonConstant();
    constructor(private globals: Globals) { }

    canDeactivate(component: any): boolean {
        this.globals.RemoveDataFromLocalStorage(
            this.commonConst.Email)
        return true;
    }
}
