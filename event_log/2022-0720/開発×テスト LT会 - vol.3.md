# 途中から参加

# インシデントゼロを支える技術
- https://speakerdeck.com/yug1224/20220315-devtestlt

# 腐らない腐らないAPIドキュメントを作るテスト
- https://www.slideshare.net/naoyatakahashi17/apipdf-252250475

# TurborepoとVitestでテスト/CI高速化
発表者: Toyb0x

### Turborepo
- vercelが作ったモノレポツール
- CIでも利用可能
- ビルドキャッシュが有名
- テスト結果もキャッシュ可能

#### テスト/CI高速化のTIPS
- パッケージは分割しよう
  - キャッシュはパッケージ(package.json)単位で行われる
- RemoteCache
  - そこそこ高い
  - 150ドル/月は超える
- GithubActionsのactions/cache
  - 無料
  - 10ギガを超えた場合は、古いものから消える

#### キャッシュの仕組み
- turbo.json
- .gitignore

#### パッケージ分割
- NXという管理ツールのようにするのが良いです

### Vitest
- テスト早い
- TSプロジェクトでセットアップが早い
  - 設定が楽

#### 注意点
- テスト時に型チェックは行われない
- CIなどで別途型チェックする必要がある
  - `tsc --noEmit` を別途発表することが必要

# Atomic Design と テストの◯◯な話
発表者: @takfjp

## コンポーネントの分け方
- Atomic Design
  - Atomsという単位から始まって、単位ごとにパーツが組み合わさって最終的にページとなる

### テストの仕方
- Atom, Molecules, Organismsに対してテスト
- コンポーネント以外のAPI通信、カスタムフック、汎用ロジックに対してテスト
- カバレッジは `Codecov` を使う
- Componentを追加して、前回のコミットよりもカバレッジが下がっている
  - 追加されたコンポーネントにテストが書かれていない
  - テストが不十分
- 不十分な場合にはReviewDogで該当箇所を指摘

#### Pagesのテストどうする？
- Organisms以下のコンポーネントの組み合わせでできているので、テストは基本不要という判断
- ただ、Pages独自のロジックはテストできない
  - テストが必要となるロジックをPagesに盛り込まない
  - カスタムフック、汎用ロジックは分離させておく

### Atomic Designとコンポーネントの教訓
- Molecules 以下のテストを充実させてカバレッジを担保する
- Pages にロジックを持たない
- Pages の動作にはエンジニアがユーザー目線でQAを行う
  - エンジニアがQAへの意識を持つ

### 辛くなったら。。。
- Atomic Designを脱してコンポーネント構成を見直す
  - 既存のデザインを崩すのは、かなりコストがかかる
- テストをペアプロで作っていく
  - テストへの解像度を上げるのに有効（知見の共有）
- jest 以外に E2E テストを組み込んでテスト自体の堅牢性を上げる

# テスト駆動開発入門
発表者: MakotoNakai

## 開発とテストの関係は。。。？
- 開発 → テスト
  - これが通常の開発
- テスト → 開発
  - これがテスト駆動開発

## テスト駆動開発のメリット・デメリット
- メリット
  - バグの早期発見ができる
- デメリット
  - なれるまでに時間がかかる
  - タスク達成までに時間がかかる

## 手順
1. 通らないテストを書く
2. テストに通る実装をする
   - 固定値でも良いので、正しい出力になる実装をする
   - これがテスト通過のための実装
3. 実装を改善する
   - 実装の修正を行う
4. 1-3を繰り返す

## まとめ
- テスト駆動開発はバグの早期発見・デバッグ時間の短縮などの利点がある
- テスト駆動開発はテストコードの実装・テスト通過のための実装・実装の修正をひたすら繰り返す

## 資料
- https://speakerdeck.com/makotonakai/tesutoqu-dong-kai-fa-ru-men

# Enzyme から React Native Testing Library に移行した理由
発表者: @tamago3keran
- 用語
  - RNTL(React Native Testing Library)

## Enzyme
- Reactのコードをテストしやすくするテストユーティリティのテスティングライブラリ
- テスト内でstateを利用することも可能

## RNTL(React Native Testing Library)
- React Nativeのコードをテストしやすくするテストユーティリティのテスティングライブラリ
- エンドユーザーと同じようにコンポーネントを利用するテストが可能

## Enzymeを利用していたら何が起きたか
- React Hooksが登場
  - クラスコンポーネントベースの開発から、FC + Hooksに以降
  - stateにアクセスするテストが軒並み死んだ

## なんでRNTLを採用した
- 設計指針がより壊れにくいテストが作成可能
  - ユーザーが操作しているようなテストにすることで、高い信頼性を獲得
- メリット
  - 壊れにくいテスト
  - 機能開発に集中できる
  - React公式が推奨している

# jestのspyOnでmockする際に詰まった話
発表者: @Riku_0202_

## jest.spyOn の使い方
- 引数で、モジュールとモックしたい関数名を文字列として与える
- モックしてくれる

## テストが動かない原因はインポートの違い
- import * as React from "react" <- 単一インポート
  - トランスパイル
    - _react.useRef
- import React from "react" <- デフォルトインポート
  - トランスパイル
    - _react["default"].useRef

- spyOnは `_react["default"].useRef` をMockする

## まとめ
- importによってテストが動かなかったりする
- トランスパイルがどうされるかを意識する必要がある

# TypeScriptでexhaustive check
発表者: hata6502

## 静的解析もテスト
- ESlint, TypeScript
- コードを書いたその場でエラーを教えてくれる

## コードの改修に強い列挙型
- 列挙型に新しい値を加えたとき、列挙型を扱う処理の修正を忘れることがある
  - 列挙に新たなメンバーを追加した場合に、既存の関数を変更するのを忘れる

## exhaustive check = 網羅チェック
TypeScriptには網羅チェックしてくれる機能がある
-> `exhaustiveCheck` (列挙型をすべて回して確認するのが必要)
