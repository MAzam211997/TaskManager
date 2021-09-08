import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

//import { AdminLayoutComponent } from "../layouts/admin";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        RouterModule,
    ],
    exports: [
        FormsModule,
        //AdminLayoutComponent,
        HttpClientModule,
        CommonModule
    ],

    declarations: [
       // AdminLayoutComponent
    ],
    entryComponents: []
})
export class SharedModule { }
