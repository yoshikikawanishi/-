using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MassageManager : MonoBehaviour {

    //
    GameObject messagePanel;

    //メッセージUI
    private Text messageText;
    //表示するメッセージ
    private string message;
    
    //1回のメッセージの最大文字数
    [SerializeField]
    private int maxTextLength = 75;
    //今見ている文字番号
    private int i = 0;
    //行の左端からの現在の文字数
    private int textLength = 0;
    //１行に表示できる文字数
    private int maxLineText = 25;

    //メッセージの最大行数
    [SerializeField]
    private int maxLine = 3;
    //現在の行
    private int nowLine = 1;

    //テキストスピード
    [SerializeField]
    private float textSpeed = 0.08f;
    //経過時間
    private float t = 0f;
    
    //メッセージをすべて表示したかどうか
    private bool isEndMessage = true;

	
    // Use this for initialization
	void Start () {
       
        //表示するメッセージ
        SetMessage("aaaaaaaaa\n" +
            "ああああああああああああああああああああ\nいい" +
            "うううううううううううううううううううううううううううううううう\nえええええええええええええええ" +
            "おおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおえ"
        );
		
	}
	

	// Update is called once per frame
	void Update () {

        //パネルとテキストの生成、取得、メッセージ表示の開始
        if (Input.GetKeyDown(KeyCode.Z) && isEndMessage && i == 0) {
            StartMessage();
            isEndMessage = false;
        }

        //メッセージを表示しきっていない
        if (!isEndMessage) {
            //メッセージの表示
            PrintMessage();          
        }

        //メッセージを表示しきったかどうか
        if (i >= message.Length && !isEndMessage) {
            isEndMessage = true;
        }
        //メッセージを表示しきったときzでパネルを消去
        if(isEndMessage){
            if (Input.GetKeyDown(KeyCode.Z)) {
                TextReset();
                i = 0;
                Destroy(messagePanel);
                
            }
        }
       
    }


    

    //変数に文字列を代入
    void SetMessage(string message) {
        this.message = message;
    }

    //メッセージ表示の開始
    void StartMessage() {
        //パネルとテキストを生成
        messagePanel = GameObject.Instantiate(Resources.Load("Prefabs/Panel")) as GameObject;
        messagePanel.transform.SetParent(gameObject.transform, false);
        //テキストを取得
        messageText = GetComponentInChildren<Text>();
        messageText.text = "";
       }

    //メッセージの表示、表示中の処理
    void PrintMessage() {
        //３行目まで表示しきっていないとき
        if (nowLine <= maxLine) {
            //1文字づつ表示
            PrintText();
        }
        //３行目も表示しきった時、zボタンが押されると続きのメッセージから表示
        else {
            if (Input.GetKeyDown(KeyCode.Z)) {
                TextReset();
                i++;
            }
        }
        //zが押されたら表示を加速
        if (Input.GetKey(KeyCode.Z)) {
            textSpeed = 0.02f;
        }
        if (Input.GetKeyUp(KeyCode.Z)) {
            textSpeed = 0.08f;
        }
    }

    //テキストのリセット
    void TextReset() {
        messageText.text = "";
        nowLine = 1;
        textLength = 0;
        t = 0f;
    }

    //テキスト欄に一文字ずつ表示
    void PrintText() {
        if (t >= textSpeed) {
            messageText.text += message[i];
            t = 0f;
            textLength++;

            //１行に表示しきる、または改行文字のとき現在の行数を足す
            if (textLength >= maxLineText || message[i] == '\n') {
                textLength = 0;
                nowLine++;
            }
            i++;
        }
        t += Time.deltaTime;
    }

}
