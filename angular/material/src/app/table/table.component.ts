/**
 * Angular Material の公式サイトを基に機能検証コンポーネントを作成
 * https://material.angular.io/components/table/examples
 */
 
import { Component, OnInit } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";

export interface SirenCharacter {
  name: string;
  position: number;
  age: number;
  job: string;
}

/**
 * SIREN Wikipedia より登場人物情報を引用
 * https://ja.wikipedia.org/wiki/SIREN_(%E3%82%B2%E3%83%BC%E3%83%A0%E3%82%BD%E3%83%95%E3%83%88)#%E7%99%BB%E5%A0%B4%E4%BA%BA%E7%89%A9
 */
const ELEMENT_DATA: SirenCharacter[] = [
  { position: 1, name: "須田恭也", age: 16, job: "高校生" },
  { position: 2, name: "竹内多聞", age: 34, job: "民俗学者" },
  { position: 3, name: "宮田司郎", age: 27, job: "医師" },
  { position: 4, name: "牧野慶", age: 27, job: "求導師" },
  { position: 5, name: "恩田理沙", age: 21, job: "家事手伝い" },
  { position: 6, name: "四方田春海", age: 10, job: "小学生" },
  { position: 7, name: "高遠玲子", age: 29, job: "小学校教師" },
  { position: 8, name: "志村晃", age: 70, job: "猟師" },
  { position: 9, name: "美浜奈保子", age: 28, job: "TVレポーター" },
  { position: 10, name: "前田知子", age: 14, job: "中学生" }
];

@Component({
  selector: "app-table",
  templateUrl: "./table.component.html",
  styleUrls: ["./table.component.css"]
})
export class TableComponent implements OnInit {

  displayedColumns: string[] = ["position", "name", "age", "job"];
  dataSource = new MatTableDataSource(ELEMENT_DATA);

  constructor() {}

  ngOnInit() {
    /**
     * テーブルのフィルタとして利用する条件を指定
     * [Angular Material Table filterPredicate]
     * https://stackoverflow.com/questions/48470046/angular-material-table-filterpredicate
     */
    this.dataSource.filterPredicate = 
    // dataにはdataSourceの配列、filterには datasource.filter = xx として指定した文字列が入る
    (data, filter) => {
      // match関数は文字列が含まれていない場合にnullを戻すので、null以外の要素はフィルタ文字列を含んでいる
      return data.name.match(filter) !== null;
    }
  }

  /**
   * フィルタ適用処理
   * @param filterValue フィルタとして利用する文字列
   */
  applyFilter(filterValue: string) {
    // filterPredicate で保持する filterの値を指定してフィルタを適用する
    this.dataSource.filter = filterValue;
  }
}
