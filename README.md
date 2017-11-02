# VRDeviceController v1.0.0
モバイルVRの単眼・双眼切り替え機能をモジュール化したスクリプトです。<br>
旧GvrSDK内のGvrMainと同じ挙動を目指しています。

# 使い方
#### 初期設定
PlayerSettingsでVirtualRealitySupportedにチェックを入れ、SDKsに**None**と**Cardboard**を追加します。<br>
この時**Cardboard**を上にした状態だと、起動時からVRモードが有効になった状態になります。<br>
起動直後にタッチ操作でUIをさせるシーンを想定している場合は**None**が上、**Cardboard**を下に設定してください。<br>
![Imgur](https://i.imgur.com/iKDlWpQ.png)

#### ヘッドトラッキングを有効にする
ヘッドトラッキングを有効化したいシーン内にPrefabs/VRDeviceControllerを追加します。<br>
同シーン内に存在するすべてのCameraのヘッドトラッキングが有効になることに注意してください。<br>
VRModeEnabledを操作(true、false)することで単眼・双眼の切り替えが可能です。<br>
VRDeviceControllerを配置していないシーンへ遷移した場合、自動的にカメラのヘッドトラッキングは無効となります。<br>

## ビルド環境
Unity 2017.2.0f3<br>
macOS High Sierra 10.13
