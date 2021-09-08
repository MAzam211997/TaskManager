import { Injectable } from '@angular/core'
import { formatDate } from '@angular/common';
import { CommonConstant } from 'src/app/shared/constent/applicationcodes';

declare var $: any;
Injectable()
export class Globals {

    public cultureCode: string;
    public isRTL: boolean;
    public dateSaveFormat: string;
    public sessionTime:number;
    // private datePipe: DateFormatterPipe
    // private datePipe:any; 

    constructor() {
         
        let cachedCultureCode: string = localStorage.getItem("cultureCode");
        this.cultureCode = cachedCultureCode ? cachedCultureCode : 'en-US';

        let cachedIsRTLValue: string = localStorage.getItem("isRTL");
        this.isRTL = cachedIsRTLValue ? (cachedIsRTLValue.toLowerCase() === 'true') : false;

        this.dateSaveFormat = "yyyyMMdd";
        this.sessionTime = 60 * 60; 
    }
    /**
     * GetCurrent
     */
    public getCurrentDate(): string {
        return formatDate(new Date(), 'dd/MM/yyyy', 'en-US');
    }

    public getCurrentDateFull(): string {
        return formatDate(new Date(), 'ddMMyyyyHHmmss', 'en-US');
    }
    public getMonthStartDate(Year, month): string {
        var firstDay = new Date(Year, month, 1);
        return formatDate(firstDay, 'dd/MM/yyyy', 'en-US');
    }

    public getMonthEndDate(Year, month): string {
        var firstDay = new Date(Year, month + 1, 0);
        return formatDate(firstDay, 'dd/MM/yyyy', 'en-US');
    }
    public GetMonthsArray(): any[] {
        return [
            { text: "324", value: "01" },
            { text: "337", value: "02" },
            { text: "338", value: "03" },
            { text: "339", value: "04" },
            { text: "340", value: "05" },
            { text: "341", value: "06" },
            { text: "342", value: "07" },
            { text: "343", value: "08" },
            { text: "344", value: "09" },
            { text: "345", value: "10" },
            { text: "346", value: "11" },
            { text: "347", value: "12" }
        ]
    }
    //#region Local Storage Related Events

    public SetCultureCode(cultureCode: string): void {

        localStorage.removeItem("cultureCode");
        localStorage.setItem("cultureCode", cultureCode);
        this.cultureCode = cultureCode;
    }

    public SetTextDirection(isRTL: boolean): void {

        localStorage.removeItem("isRTL");
        localStorage.setItem("isRTL", isRTL.toString());
        this.isRTL = isRTL;
    }

    public SetTextDirectionFromCultureCode(): void {
        let isRTL = this.cultureCode == "en-US" ? false : true;
        localStorage.removeItem("isRTL");
        localStorage.setItem("isRTL", isRTL.toString());
        this.isRTL = isRTL;
    }
    public setDataInLocalStorage(key: string, value: any): void {
        localStorage.removeItem(key);
        if (value)
            localStorage.setItem(key, value.toString())
        else {
            localStorage.setItem(key, value);
        }
    }

    public getDataFromLocalStorage(key: string): any {

        let storageData = localStorage.getItem(key)
        return storageData ? storageData : null;
    }

    public getBoolFromLocalStorage(key: string): boolean {

        let storageData = localStorage.getItem(key)
        if (storageData !== null && storageData.toString().toLowerCase() === 'true')
            return true
        else
            return false
    }
    /*
    * Comma Separated Key to delete specific item from LocalStorage
    */
    public RemoveDataFromLocalStorage(key: string): void {
        key.split(',').forEach(element => {
            localStorage.removeItem(element);
        });
    }


    //#endregion

    //#region  
    public HideCollapseForCrud() {

        if ($("#divHome").hasClass('show')) {
            $('[href="#divHome"]').trigger('click');
        }
        if ($("#divWork").hasClass('show')) {
            $('[href="#divWork"]').trigger('click');
        }
        if ($("#payTermsPanel").hasClass('show')) {
            $('[href="##payTermsPanel"]').trigger('click');
        }
    }
    HideCollapseForNavigation(smartUtilities: any) {

        localStorage.removeItem("HomeAddress");
        localStorage.removeItem("WorkAddress");
        smartUtilities.ClearForm(".HomeAddress");
        smartUtilities.ClearForm(".WorkAddress");

        if ($('#divHome').is(':visible'))
            $('[href="#divHome"]').trigger('click');

        if ($('#divWork').is(':visible'))
            $('[href="#divWork"]').trigger('click');

        if ($('#saleTgtPanel').is(':visible'))
            $('[href="#saleTgtPanel"]').trigger('click');

        //if($('#saleTgtPanel').is(':visible')){
        // $('#saleTgtPanel').collapse('hide', true);
        // if($('#clsIcon').hasClass('os-icon-ui-23')) {
        //   $('#clsIcon').addClass('os-icon-ui-22');
        // }

    }
    HideCollapseForNavigationBuyer(smartUtilities: any) {

        localStorage.removeItem("HomeAddress");
        localStorage.removeItem("WorkAddress");
        smartUtilities.ClearForm(".HomeAddress");
        smartUtilities.ClearForm(".WorkAddress");

        if ($('#headingOne').is(':visible'))
            $('[href="#collapseOne"]').trigger('click');

        if ($('#heading11').is(':visible'))
            $('[href="#collapse11"]').trigger('click');

        //if ($('#divWork').is(':visible'))
        //  $('[href="#divWork"]').trigger('click');

        //if ($('#saleTgtPanel').is(':visible'))
        //  $('[href="#saleTgtPanel"]').trigger('click');  

        //if($('#saleTgtPanel').is(':visible')){
        // $('#saleTgtPanel').collapse('hide', true);
        // if($('#clsIcon').hasClass('os-icon-ui-23')) {
        //   $('#clsIcon').addClass('os-icon-ui-22');
        // }

    }
    ClearaddressFields() {
        localStorage.removeItem("HomeAddress");
        localStorage.removeItem("WorkAddress");
    }

    //#endregion 

    //#region
    public GetHoursArray(): any[] {
        return [
            { text: "00", value: "00" },
            { text: "01", value: "01" },
            { text: "02", value: "02" },
            { text: "03", value: "03" },
            { text: "04", value: "04" },
            { text: "05", value: "05" },
            { text: "06", value: "06" },
            { text: "07", value: "07" },
            { text: "08", value: "08" },
            { text: "09", value: "09" },
            { text: "10", value: "10" },
            { text: "11", value: "11" },
            { text: "12", value: "12" },
            { text: "13", value: "13" },
            { text: "14", value: "14" },
            { text: "15", value: "15" },
            { text: "16", value: "16" },
            { text: "17", value: "17" },
            { text: "18", value: "18" },
            { text: "19", value: "19" },
            { text: "20", value: "20" },
            { text: "21", value: "21" },
            { text: "22", value: "22" },
            { text: "23", value: "23" }

        ]
    }

    public GetMintsArray(): any[] {
        return [
            { text: "00", value: "00" },
            { text: "01", value: "01" },
            { text: "02", value: "02" },
            { text: "03", value: "03" },
            { text: "04", value: "04" },
            { text: "05", value: "05" },
            { text: "06", value: "06" },
            { text: "07", value: "07" },
            { text: "08", value: "08" },
            { text: "09", value: "09" },
            { text: "10", value: "10" },
            { text: "11", value: "11" },
            { text: "12", value: "12" },
            { text: "13", value: "13" },
            { text: "14", value: "14" },
            { text: "15", value: "15" },
            { text: "16", value: "16" },
            { text: "17", value: "17" },
            { text: "18", value: "18" },
            { text: "19", value: "19" },
            { text: "20", value: "20" },
            { text: "21", value: "21" },
            { text: "22", value: "22" },
            { text: "23", value: "23" },
            { text: "24", value: "24" },
            { text: "25", value: "25" },
            { text: "26", value: "26" },
            { text: "27", value: "27" },
            { text: "28", value: "28" },
            { text: "29", value: "29" },
            { text: "30", value: "30" },
            { text: "31", value: "31" },
            { text: "32", value: "32" },
            { text: "33", value: "33" },
            { text: "34", value: "34" },
            { text: "35", value: "35" },
            { text: "36", value: "36" },
            { text: "37", value: "37" },
            { text: "38", value: "38" },
            { text: "39", value: "39" },
            { text: "40", value: "40" },
            { text: "41", value: "41" },
            { text: "42", value: "42" },
            { text: "43", value: "43" },
            { text: "44", value: "44" },
            { text: "45", value: "45" },
            { text: "46", value: "46" },
            { text: "47", value: "47" },
            { text: "48", value: "48" },
            { text: "49", value: "49" },
            { text: "50", value: "50" },
            { text: "51", value: "51" },
            { text: "52", value: "52" },
            { text: "53", value: "53" },
            { text: "54", value: "54" },
            { text: "55", value: "55" },
            { text: "56", value: "56" },
            { text: "57", value: "57" },
            { text: "58", value: "58" },
            { text: "59", value: "59" },


        ]
    }

    //#endregion  

    constant = new CommonConstant()

    resetSessionTimeOut() {
        localStorage.setItem(this.constant.SESSION_TIME, this.sessionTime.toString())
    }        

    resetServerSessionTimeOut() {
        localStorage.setItem(this.constant.SERVER_SESSION_TIME, this.sessionTime.toString())
    } 

    getSessionTime(): number {
        let timeLeft = Number(localStorage.getItem(this.constant.SESSION_TIME)) - 30
        localStorage.setItem(this.constant.SESSION_TIME, timeLeft.toString())
        return timeLeft;
    }

    getServerSessionTime(): number {
        let timeLeft = Number(localStorage.getItem(this.constant.SERVER_SESSION_TIME)) - 30
        localStorage.setItem(this.constant.SERVER_SESSION_TIME, timeLeft.toString())
        return timeLeft;
    }

}
export enum ScreenType {
    StandardForm = 1,
    GridForm = 2,
    UpdateOnly = 3,
    MasterScreens = 4,
    TreeViewForm = 5
}