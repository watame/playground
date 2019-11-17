import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppRoutingModule } from "./app-routing.module";

// FlexLayout
import { FlexLayoutModule } from "@angular/flex-layout";

// DateTimePicker
import { Ng2FlatpickrModule } from "ng2-flatpickr";

// Component
import { AppComponent } from "./app.component";
import { DailyInputComponent } from "./daily-input/daily-input.component";

// Material
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatCardModule } from "@angular/material/card";

@NgModule({
  declarations: [AppComponent, DailyInputComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,

    // FlexLayout
    FlexLayoutModule,

    // DatetimePicker
    Ng2FlatpickrModule,

    // Material
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatCardModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
