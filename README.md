# Unity MsgBox

## 概要

Unityで使えるダイアログ機能です。

## 呼び出し方

MsgBoxプレハブをCanvasの手前に置きます。その上で、下記を実行。

    MsgBox.Show().Message("てすとてすとてすとてすと\nてすとてすとてすと")
    	.Button(MsgBoxButton.Positive, "はい", () => {
        Debug.Log("はい");
      }).Button(MsgBoxButton.Negative, "いいえ", () => {
        Debug.Log("いいえ");
      });

デザインやアニメーションを実装して下さい。
