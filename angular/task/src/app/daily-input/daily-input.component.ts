import { Component, OnInit } from "@angular/core";
import { FlatpickrOptions } from "ng2-flatpickr";
import Japanese from "flatpickr/dist/l10n/ja.js";

@Component({
  selector: "app-daily-input",
  templateUrl: "./daily-input.component.html",
  styleUrls: ["./daily-input.component.css"]
})
export class DailyInputComponent implements OnInit {
  /**
   * カレンダー用オプション
   * @type {FlatpickrOptions}
   */
  cal_options: FlatpickrOptions = {
    // 日本語環境
    locale: Japanese.ja
  };

  /**
   * デートピッカー用オプション
   * @type {FlatpickrOptions}
   */
  time_options: FlatpickrOptions = {
    // カレンダー表示
    noCalendar: true,
    // 時間入力
    enableTime: true,
    // 24時間表示
    time_24hr: true
  };

  constructor() {}

  ngOnInit() {}
}
