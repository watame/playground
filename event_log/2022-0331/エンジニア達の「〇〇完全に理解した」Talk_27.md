# Tauri完全に理解した @kfurumiya
## TAURIとは
* Rustで実装されたGUIアプリ開発フレームワーク
  * フロントエンドの技術を利用する
  * OS標準のWebViewで表示をする
    * ライブラリもめちゃくちゃコンパクトでビルドしても3MBくらいになる
    * electronの**80分の1**
  * OS側の機能が使いやすい
    * configで有効化するだけ
    * WebViewの脆弱性がある時にOS側でアップデートするだけで対応できたりする
  * 今どきのウェブアプリなら特に問題なく動く
### 使いやすい
* npmで簡単にインストールできる
### 欠点
* ビルド遅い
* クロスコンパイル厳しい
  * GitHub Actionsで各環境ごとにビルドするしかない
* 現在はリモートURLを直で開けない？
  * ローカルのHTMLがエントリポイントになっている
  * 0秒リダイレクトで乗り切る
### その他
* 将来的にiOS/Android対応
* Rust拡張可能

# フロントエンドテストの為のMSW（Mock Service Worker） @Hirorou
## 前提
* フロントエンドエンジニアはReactやVueなどで開発している人を想定している
## 背景
* 機能開発で開発者一人がAPIとUIの両方を担当している
  * 将来を見据えてバックエンドとフロントエンドを分ける開発体制の準備を進めたい
## 開発環境要件と解決策
* 開発環境要件
    * REST APIで作成
    * OpenAPIを設計書がわりにFEとBEで別々で開発
    * FEはOpenAPIで設計したValidationやMockデータを使用したい
* 解決策
  * Prism
    * MockAPIのサーバーを立ててくれるプロキシサーバー
    * 毎回Dockerを起動してサーバーを立てていた
* 課題
  * PrismではErrorObjectを返却出来なかった
    * JSやTSのObjectを返却してくれるMockAPI環境が欲しい
## 新たな開発要件と解決策
* 開発環境要件
    * REST APIで作成
    * OpenAPIを設計書がわりにFEとBEで別々で開発
    * FEはOpenAPIで設計したValidationやMockデータを使用したい
    * JSやTSのObjectを返却してくれるMockAPI環境が欲しい
* 解決策
  * Mock Service Workerを使用する
    * Service Worker APIを活用して実際のRequestを仲介するMockAPIサーバー
## MSWの利点と懸念点
### 利点
* TypeScriptがサポートされている
* Service Workerを経由するのでサーバー構築が必要ない
  * ブラウザの裏側で起動してくれる
* ロジックも記述できるので柔軟なテストの幅が増える
* ブラウザのNerworkタブにも反映される
  * 実際のテストと同じように扱える
### 懸念
* Mock Service Workerの挙動自体はFEが実装しないとダメなので、仕様の書き起こしミスが発生する可能性がある
## 今後
* GraphQLにも対応しているので、試していきたい

# AssetPostprocessor完全に理解した @anns
※UnityやVRチャットでアバターを作成している人にしかわからない話題かも
## Unityを使うときに知っておくと便利な機能
* Unityのエディター拡張を使おう
  * 各項目の設定値を設定できるツールがある
    * AssetPostprocessor
## AssetPostprocessorとは
* アセットインポート時の処理をフックする
  * OnPreprocess~
    * アセットをインポートする前に呼び出される
      * 正直ここであまり利用されない
  * OnPostprocess~
    * アセットがインポートされた後に呼び出される
      * インポート後のデータを変更したいときに利用する
※FBXは3Dモデルを保存する形式の1つ
### 具体的には。。。
以下のようにプロセスが流れていく。
[PNG] -> [OnPreprocessAsset] -> [OnPreprocess Texture] -> [OnPostprocess Texture] -> [OnPostpprocessAllAssets]（1アセット内のデータが全てインポートされた後に呼ばれる）

# LTアフタートークタイム

# ちょこっと深堀り！！

# 振り返りトークタイム
