'use strict';

// Electron のモジュール
const electron = require("electron");

// アプリケーションをコントロールするモジュール
const app = electron.app;

// ウィンドウを作成するモジュール
const BrowserWindow = electron.BrowserWindow;

// dirname
const _dirname = "/Users/kanemarutomoya/workspace/electron/clock"

// メインウィンドウはGCされないようにグローバル宣言
let mainWindow;

// 全ウィンドウが閉じたら終了
app.on('window-all-closed', function(){
    if (process.platform != 'darwin'){
        app.quit();
    }
});

// Electronの初期化完了後に実行
app.on('ready', function(){
    // メイン画面の表示。ウィンドウの幅、高さが指定可能
    mainWindow = new BrowserWindow({width: 400, height: 150});
    mainWindow.loadURL('file://' + _dirname + '/index.html');
    // 最前面表示
    mainWindow.setAlwaysOnTop(true);
    // サイズ変更無効化
    mainWindow.setResizable(false);

    // ウィンドウが閉じたらアプリも終了
    mainWindow.on('closed', function(){
        mainWindow = null;
    });
});
