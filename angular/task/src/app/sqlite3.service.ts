import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class Sqlite3Service {
  constructor() {}

  test() {
    console.log("this is test!");
    const sqlite = require("sqlite3").verbose();
    const database = new sqlite.Database("test.db");
    database.serialize(() => {
      // テーブルがなければ作成する
      database.run("CREATE TABLE IF NOT EXISTS user (name TEXT, age INTEGER)");

      // Prepared Statement でデータを挿入する
      const stmt = database.prepare("INSERT INTO user VALUES (?, ?)");
      stmt.run(["Foo", 25]);
      stmt.run(["Bar", 39]);
      stmt.run(["Baz", 31]);

      // prepare() で取得した Prepared Statement オブジェクトをクローズする。これをコールしないとエラーになる
      stmt.finalize();
    });
    database.close();
  }
}
