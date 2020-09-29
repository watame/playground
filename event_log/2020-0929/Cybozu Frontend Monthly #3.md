# ［9/29］Cybozu Frontend Monthly #2

資料
https://cybozu.github.io/frontend-monthly/posts/2020-09

気になった記事を各自説明する
* Introducing the New JSX Transform
  * Reactのdefault importをしなくても利用できるようになった
  * babel経由でJSX解釈をすることができるようになったよ
* Vueのv3がリリースされた
  * Vue3.0がリリースされた
  * 移行についても細かく説明されているので、移行するときはそれに沿う事
* MDN Browser Compatibility Report 2020
  * ものすごい長い課題収集アンケート
  * デザイン関連に課題あり
  * WebAPIへの不満も多かった
  * 最も課題の多いブラウザとしてIE,旧Edge,Safariが肉薄している
  * 生の声とかも記載されているので面白い記事になっている
* Working with Media - Designing in the Browser
  * YouTubeの連載
  * 最近連載が復活して、アクセシビリティについてをずっとやっている
  * alt属性についての説明
    * alt属性は検索にもアクセシビリティも重要
    * 画像だけであれば、画像の物体だけではなく情景も細かく書く必要がある
    * コンテンツを見て、alt属性を追加するのが大事
      * 全ての画像に対してalt属性は不要（細かく説明されているなら）
    * figureタグを利用すれば、画像の下部に説明を付けることも出来る
    * 動画の字幕も変えられるから、コンテンツに対応して変更しようね
* Supporting ESLint's dependencies
  * ESLintに対して寄付を募っているよという話
  * メンテナーとパッケージ開発者に対して寄付を還元するよ
* ReadSpecWith.us
  * EcmaScriptの仕様を見ながら解説してくれる動画
  * 1本7-8分で見やすい
  * EcmaScriptの仕様の解読にも良い
* DevTools architecture refresh: Migrating to JavaScript modules
  * ChromeのDevToolsの依存関係解決をESModuleに移行した話
  * Moduelがない時代からの独自モジュール利用の問題等、歴史もある
  * 移行期間の概算、フェーズ分けをして進捗管理もオープンにしつつ対応
  * 7か月くらいかかった
  * 方針としては自動変換スクリプトを作成し、フォルダ毎にスクリプトを展開する
* Introducing Source Order Viewer in the Microsoft Edge DevTools - Microsoft Edge Blog
  * Edgeの小ネタ
  * HTMLドキュメント上の順番をビジュアライズしてくれる機能
    * タブオーダーが確認できる
    * 見た目とドキュメント上の順序がばらつく場合が大きくなっている
    * 読み上げでは順序が大事なのでアクセシビリティにもかかわる
  * 表示してくれるのは、選択した要素の子要素のオーダー
  * FireFoxでは全部のオーダー出るっぽいからFireFoxが上位互換？
    * Edgeの方はソース上の順序を可視化するもの（タブとソースの順序が一致していない場合に役立つ）
* Workers Durable Objects Beta: A New Approach to Stateful Serverless
  * CloudFlareの新機能
  * CloudFlare Workers : CDNのエッジで動くサーバサイドのJavaScript
    * エッジ内でデータを保存できる
      * 保存できるのはクラス
        * 世界に一個にしかないクラス
        * このクラスにローカルにデータがある
      * controller.storage -> 永続化する先
    * Worker間通信が可能になる
      * fetchでworkerで通信を受けて、putで書き込み
    * この中でのみ動くアプリも作れるよ（すごい）
* The State of Nuxt
  * Vue3対応に合わせていろいろ変わるよっていう話
  * Nuxt使う人は見とくと良いです

