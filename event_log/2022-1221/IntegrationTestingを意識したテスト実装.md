# 出前館アプリ
- React Nativeで作られている

# ReactNativeについての質問
- flutterと比較してどう？

# クライアント開発環境
- Next.js / React
- BFF
  - GraphQL / Apollo

# テスト環境
- UI
  - Autify
- Service
  - Snapshot Testing
- Unit
  - Unit
  - Integration testing

# テストの基本方針
- リファクタリングした画面にテストを書く
  - 大規模なフリファクタリング/ リプレイスの最中
  - 優先度：テストを実装すること < リファクタリングすること
- 重要なロジックは関数に切り出し、UnitTestを実装
  - BFFへのロジック集約する際にTestを決めている
と、いいつつカチッとしているわけではない

# The Testing Trophy
- Write tests. Not too many,. Mostly integration
- Not too many.
  - カバレッジが高すぎるコード はいい状態とイコールではない
- Mostly integration
  - テストピラミッドが上に行くほど、各テスト形式の信頼度が高くなることはあまり知られていない
  - E２Eテストは信頼性とスピードコストのバランスが良い

Unit test / Integration Testingに意識を集中している

# どうやってテストする？
1. Hook部分をMock化する
  - テスト実装のコストが最も低い
  - 壊れにくいテストではある
  - ほぼ単体テスト
2. ApolloClientのMockProviderをしよう
  - 実際に利用するApolloClientを利用するので実際の利用シーンに合致する
3. 通信部分をMock化
  - ユースケース自体をテストできる

出前館では 1, 2を実装している。
テストとスピード感の両立を目指して開発を行う
