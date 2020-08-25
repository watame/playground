# ［8/25］Cybozu Frontend Monthly #2

資料
https://gist.github.com/sakito21/d31529d0c43821ca9e03e2596762b295

気になった記事を各自説明する
* The Just in Case Mindset in CSS
  * 文字が長くなる場合に、アイコンがつぶれるとか
  * ボタンが並んだ場合に、次のボタンにマージン付けるとか
  * マインドセットの名の通り、覚えておいたほうが良いことを並べてくれている
* A Complete Guide to Dark Mode on the Web
  * ダークモードに対応させるためのTipsをまとめている
  * 実際のコードも入りつつ、実装が確認できる
  * ダークモードが
* You May Finally Use JSHint for Evil
  * JSHintがMITライセンスに変更された
  * メジャーバージョンアップではなく、マイナーバージョンで対応することにこだわった
    * メジャーバージョンアップで行うと、アップデートしない人もいるのでマイナーバージョンアップにこだわった
  * OSSはライセンス関係がややこしいので、利用したくても利用できないことが多々
  * JavaScript Weeklyでニュースを購読することができる
* React v17.0 Release Candidate: No New Features
  * Concurrent Modeに向けてバージョンアップを段階的にできるようになった
  * eventの内部実装で変更があった
    * Documentに対して全インベントを適用させているのが、RootElementにイベントが適用される
      * DOMの描画の前にイベントが走るようになっている
        * v16まではDocumentに対してイベントリスナを貼っていた
          * wheel, event, touch はパッシブイベントで持っていたのが、17では止められるようになっている
          * イベント回りは実装が大きく変わっているので、注意すること
    * Reactのeはブラウザのイベントではない
      * プールでReactのイベントを持つ必要があったが、モダンなブラウザでは必要なくなったので、プールが消えた
    * clean up イベントは非同期になっているので、同期的にやっている場合は注意する
* What's New In DevTools (Chrome 86)
  * Media Panelが追加
  * 特定のDOMのみのスクリーンショットが取れる
  * エミュレート機能の追加
    * ローカルフォントのエミュレート（ない場合の文字列も表示される）
    * 未ログインユーザのエミュレート
    * prefers-reduced-data のエミュレート
  * アクセシビリティ
    * ガイドラインを指定することで、アクセシビリティを考慮した色にすることができる
* Enabling Custom Control UI
  * selectなどのフォームコントロールをカスタマイズするのが難しい
    * カスタマイズする場合には、コントロールのテンプレートを厳密に設計する必要がある
    * cssの`part`という仮想ボタンを付与する等、段階を分で対応の方法をコードベースで記述されている
    * 詳細はブログにて
      * https://dackdive.hateblo.jp/entry/2020/08/19/080000
* Prettier 2.1: new --embedded-language-formatting option and new JavaScript/TypeScript features!
  * `html`などのタグを関数に渡して、フォーマットされたものを戻すようにしている、らしい
  * プラグイン界隈の変更は、mainteinerが居ないことがほとんどなのでイシューとかを挙げると良い