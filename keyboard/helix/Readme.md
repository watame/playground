### 概要

自作キーボード Helix の ファームウェア です。
[MakotoKurauchi](https://github.com/MakotoKurauchi)様が記載されている[ファームウェア](https://github.com/MakotoKurauchi/helix/blob/master/Doc/firmware_jp.md)を基に keymap を作成しました。

### 利用方法

このレポジトリに格納されている`watame`というファームウェアを書き込む場合の手順を記載します。

1. `/keyboards/helix/rev2/keymaps/` に`watame`ディレクトリを丸ごとコピーします
2. インストール済みの `qmk_firmware` ディレクトリに移動します
3. `make helix:watame` でビルドします
4. `qmk_firmware` ディレクトリに移動し、`.build`ディレクトリに`helix_rev2_watame.hex`が格納されていることを確認
5. [QMK_ToolBox](https://github.com/qmk/qmk_toolbox/releases)を利用し、ファームウェアを**左右のキーボードそれぞれに**書き込む
