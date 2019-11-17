import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { DailyInputComponent } from "./daily-input/daily-input.component";

const routes: Routes = [{ path: "daily", component: DailyInputComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
