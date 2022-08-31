# 1.脱IE時代のフロントエンド開発（吉田）
IEのサポートが終了！
- 2022-06に終了した！

↓ 終了したことによって使えるJS/CSSはこんなにいっぱいあるよー
- https://the-world-after-ie-left.vercel.app/?view=css
- https://github.com/progfay/benefit-from-end-of-ie

全部チェックするのはきついから、使いたかったけど使えなかったものをピックアップ！

## 表現が豊かになる系
- CSS
    - background-clip
        - 背景をクリップする範囲を指定できる
        - 中抜きなどもできるので非常に便利
    - filter
        - 要素にフィルター効果を当てられる
        - drop-shadowが便利
            - 文字や図形に対して影を落とすことができる
            - 要素の矩形に対してだけでないので、表現力が高い

## 実装が簡単になる系
- CSS
    - object-fit
        - 画像のトリミングをしてくれる
    - aspect-ratio
        - アスペクト比を指定できる
    - fit-content
        - ブロック要素だけど幅はコンテントに合わせて表示できる
        - `width: fit-content` とするだけ

object-fit, aspect-ratioを合わせることで、トリミングとアスペクト比を保った状態で表示できる
↓こんな感じ
```css
#cover img {
    object-fit: cover;
    aspect-ratio: 16 / 9;
}
```

# 効率よく書ける系
- CSS
    - :is()
    - :where()
        - 複数の要素をまとめて、合致する場合にCSSを適用する

# 2.ES2022の新機能紹介（岩佐）

- Class
    - private, staticが定義できるようになった
    - private
        - `#val` で private変数を定義する必要がある
            - `private xxxx` は public 扱いになるっぽいので注意
            - TSのトランスパイル設定で対応できる？

- トップレベル await
    - awaitキーワードを非同期関数の外でも使用可能になる
    - トップレベル関数として利用できるので、ネストが少なくなる

- 実装状況
  - https://ics.media/entry/220610/

# 3.Vue2系のEOL時期判明。3系への乗り換え準備はお早めに
Vue3系がデフォルトバージョンになった。
それに伴い、2系のサポートが2023年末になっている。
-> Vuetifyは3系に対応していない。。。

Vuetifyなど強依存のライブラリがなければ、移行ガイドに従えば気合を入れてやればいけそう！

# 資料
- https://hackmd.io/w_8q2TLqRq2YeQCHD7fcGw
