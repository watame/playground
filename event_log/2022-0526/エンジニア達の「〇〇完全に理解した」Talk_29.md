# ライブ配信サーバー完全に理解した
- 発表者：mainyaa

## ライブ配信の技術
配信が盛り上がっていて、かかるお金もすごい。。。
ストリーマーになりたくてもなれない、だが、僕らには技術がある！
-> Twitch.tvを見ると.m3u8, .tsをやっている
  HLSを利用している見たい

## HLS
HTTPを経由した低遅延の動画配信できる
- Appleが作った
- OS
  - iOSはHLSにしか対応していない

### 特徴
- HTTP/HTTPSで動画を配信できる
- アダプティブストリーミング
  - 視聴者の帯域幅にあわせて画質を変えることが出来る
  - モバイルでうれしい  
- 3-6秒の遅延がある
  - ラグがあるとコミュニケーションするにはちょっと辛い

### 仕組み
#### 配信側
- 動画をセグメントに分割
  - 各セグメントは10秒くらい
  - 分割したものは`ts`ファイルとなる
- ビデオセグメントのURLと順序はプレイリストとして保存される
  - m3u8ファイルとしてプレイリストが作成される

#### 視聴側
- HLS準拠のプレーヤーはm3u8ファイルをダウンロードして、動画を再生する
- プレーヤーは定期的にm3u8ファイルをポーリングして、都度プレイリストを更新する

### 嬉しいところ
- HTTPで通信するからセキュリティでブロックされない
- HTTPサーバーを使っているからスケールやりやすい
- 視聴さが増えてもメディアサーバーへの負荷が変わらない
- アダプティブストリーミングで複数のビットレートで配信できる
- DevToolで中身見れる

### 嬉しくないところ
- 遅延が大きい
  - 原理的に3秒未満に出来ない
- 使えるエンコードが決まってる
  - H.264, H.265のみ
- DRMがAppleFairPlayのみになっている

## アダプティブストリーミングとは
- ビットレートが異なる複数のストリームを配信することで、ブラウザが自分の帯域に合わせて自動的に画質を調整してくれる仕組み
- マスターのm3u8を作り、それを基にビットレートが違う子m3u8を参照する
  - 配信自体は全ビットレートのファイルを配信する
    - 配信の帯域に合わせて、取得するファイルを切り替える

## ファイルを見ると。。。
以下のようにsrcのアドレスにファイルを設定するだけ
```
<body>
  <video src="xxxxx.m3u8">
</body>
```

## 資料URL
- https://speakerdeck.com/mainyaa/raibupei-xin-sabawan-quan-nili-jie-sita

## サーバーを立てる
- alfg/niginx-rtmp
  - 上記のDokcerイメージを使うと一瞬で立てられる
  - `OBS`の設定を行えば普通に配信できる

# dbt 完全に理解したい
- 発表者：dach

## dbt(data build tool)とは
Select分を利用するだけでデータを変換して抽出できる仕組み

### 利用できる基盤
#### DIKWモデル
  - データ分析のフレームワーク(1から順に分析する)
    1. Data
       - 販売情報、地域別気象情報
    2. Information
       - 集約・整形された販売情報
    3. Knowledge
       - 分析できるように整理された情報
    4. Wisdom
       - 雨の日にスマホ決済5%Offキャンペーンをやれば、売り上げが見込める！
##### Layer
###### DataSource
以下に分類される、データの源泉となるもの
- 構造データ
  - 基幹システム、ECサイト、など
- 非構造データ
  - ファイル、センサーデータ、位置データ、など
###### Collecting Layer
- DataSourceからの取り込みを行う
###### Processing Layer
- CollectingLayerのデータ整形を行う
  - 抽出、クレンジング、変換、など
###### Storage Layer
- CollectingLayer, Processing Layerのデータを保存する
- 1,2,3の順番でデータがブラッシュアップされていく
  1. DataLake
    - 生データを保存する
  2. DataWareHouse
    - 組織横断的なデータが保存される
  3. DataMart
    - UseCaseに沿って分析可能なデータ

### dbtを完全に理解したい
ハンズオン

### dbtvaultを完全に理解しよう
データの管理などを良い感じに解決できるやつ
- https://belonginc.dev/members/ttyfky/posts/dbtvault-bigquery-demo-published
- https://zenn.dev/k0120/books/1a4c210fc736ab

# 技術選定完全に理解した
- 発表者：unsoluble_sugar

## 技術選定してる？
### 選定のタイミング
- 新規開発
- 機能追加
- リプレース
- 事業ピポット
### 選定の勘所
#### 大前提
要件の整理

- 何を実現したいか
  - 目指すゴールの定義
    - 何が達成できれば良いか
      - ユーザー体験
      - パフォーマンス
- 運用のしやすさ
  - 利用者
  - メンテナンス
- Design Doc等があると良さそう

#### 評価基準の明確化
- 開発期間
  - 短期、中長期
  - 事前調査、検証時間が取れるか
- 予算
  - 導入費用、工数
- 人的リソース
  - メンバーアサイン

#### 開発チーム体制
- メンバーの技術スタック
  - スキル領域
  - 前提知識の有無
  - **雑談相手がいるか**
- キャッチアップ速度

#### 候補選定と検証
- 要件を満たしているか
  - 大前提
- 拡張性
  - カスタマイズの有無
  - いじれないものを導入しても困る。。。
- 制限事項
  - アップデート阻害の可能性

#### 導入する際のチェックポイント
- ライセンス形態
- 実績、導入事例
  - 例：OSSの場合
    - コントリビューター数
    - コミット、PR数
    - スター数
- **サンプルプログラムの品質**
- **ドキュメントの充実性**

#### 運用・保守
- セキュリティ
  - 第三者評価
  - サポート体制
- アップデート頻度
  - Issue, プルリク対応の様子
  - 脆弱性への対応速度
- **利用ユーザーの母数**
  - 人員補充の際にすごく大事

#### ランニングコスト
- サブスクリプション型
- 従量課金制
  - API
    - 時間単位のCall数
    - トラフィック量
  - SaaS
    - ストレージ仕様容量
    - セッション数
    - インスタンス起動時間

#### スイッチングコスト
- 依存関係
- 類似ライブラリの状況調査
- 流行り廃りも定期的にウォッチする必要ある
  - ココは特に気を付けないと、ちゃんと選定していないんじゃないかって言われるので、大事にしよう

## 資料
- https://speakerdeck.com/unsoluble_sugar/technology-selection-completely-understood
