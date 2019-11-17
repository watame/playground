### 概要

特定のパターンに沿ってデータが格納されているバイナリファイルの読み込みを行うサンプルです。

### 前提

- バイナリファイルのデータ仕様が判明している
- バイナリファイルを読み込むデータ領域が明示されている
- 処理は IntelCPU を積んだ Linux マシンで行う

### データ仕様

バイナリファイルは特定のデータ領域については以下のように定義されているものとします。
※ 適当に決めた格納定義なので効率的ではないです（仕組みを説明するためだけの仮定義です）

#### バイナリのデータ形式

- リトルエンディアンで書き込まれている

#### Header 情報

| byte size | type | description   |
| --------- | ---- | ------------- |
| 2         | num  | comment_count |
| 2         | bit  | option        |
| ?         | char | comment       |

※ `comment`フィールドは`option`の`comment_exist`が`1`の場合のみ格納される
※ `comment`フィールドは`comment_count`分繰り返し格納される(0 の場合は`comment_exist=0`となり、`comment`自体が格納されない)

#### comment フィールド情報

| byte size | type | description |
| --------- | ---- | ----------- |
| 4         | num  | comment_id  |
| 256       | char | comment     |

#### option フィールド情報

| field_range | description   |
| ----------- | ------------- |
| 0-14        | reserved      |
| 15          | comment_exist |

※ `reserved`は未使用の領域

### 処理流れ

1. 指定されたサイズのデータ領域を fread()で読みこみ、char ポインタに格納
2. 上記 char ポインタを構造体毎の型で取得し、データを読み込む

### 参考サイト

http://www7b.biglobe.ne.jp/~robe/cpphtml/html03/cpp03014.html
