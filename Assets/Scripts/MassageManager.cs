using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MassageManager : MonoBehaviour {

    //表示する用のパネル
    GameObject messagePanel;

    //表示するメッセージ
    public class TextClass{
        public string name;
        public string serifu;
    }
    public TextClass[] message = new TextClass[3];
   
   
    //メッセージUI
    private Text messageText;

    //初めののメッセージ番号(配列番号)
    int k = 0;
    //一連のメッセージ数
    int n = 3;
    //今のメッセージを表示しきったかどうか
    private bool isOneMessage = false;

    //今見ている文字番号
    private int i = 0;
    //行の左端からの現在の文字数
    private int textLength = 0;
    //１行に表示できる文字数
    private int maxLineText = 25;

   

    //テキストスピード
    [SerializeField]
    private float textSpeed = 0.08f;
    //経過時間
    private float t = 0f;
    
    //メッセージをすべて表示したかどうか
    private bool isEndMessage = true;
    
	
    // Use this for initialization
	void Start () {

        for(int l = 0; l < n; l++) {
            message[l] = new TextClass();
        }
        //テキストを代入
        MessageText();
    }


    // Update is called once per frame
    void Update () {

        //パネルとテキストの生成、取得、メッセージ表示の開始
        if (Input.GetKeyDown(KeyCode.Z) && isEndMessage && i == 0) {
            //パネルの生成、メッセージ表示開始
            StartMessage();
            isEndMessage = false;
        }

        //一連ののセリフを表示しきっていない
        if (!isEndMessage) {

            //一つのセリフを表示
            if (!isOneMessage) {
                PrintText();
            }
            //一つのセリフを表示しきったとき
            if (isOneMessage == true) {
                if (Input.GetKeyDown(KeyCode.Z)) {
                    k++;
                    TextReset();
                    i = 0;
                    isOneMessage = false;
                    //一連のセリフを表示しきった時
                    if(k >= n) {
                        isEndMessage = true;
                        i = -1;
                    }
                }
            }
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


    //メッセージ表示の開始
    void StartMessage() {
        //パネルとテキストを生成
        messagePanel = GameObject.Instantiate(Resources.Load("Prefabs/Panel")) as GameObject;
        messagePanel.transform.SetParent(gameObject.transform, false);
        //テキストを取得
        messageText = GetComponentInChildren<Text>();
        messageText.text = "";
    }


    //テキストのリセット
    void TextReset() {
        messageText.text = "";
        textLength = 0;
        t = 0f;
    }

    //テキスト欄に一文字ずつ表示
    void PrintText() {
        if (i < message[k].serifu.Length) {
            if (t >= textSpeed) {
                messageText.text += message[k].serifu[i];
                t = 0f;
                textLength++;

                //１行に表示しきる、または改行文字のとき現在の行数を足す
                if (textLength >= maxLineText || message[k].serifu[i] == '\n') {
                    textLength = 0;
                }
                i++;
            }
            t += Time.deltaTime;
        }
        else if(!isOneMessage){
            isOneMessage = true;
        }
    }


    //表示するテキスト
    void MessageText() {
        /*
       message[0].name = "";
       message[0].serifu = "";
      */

                             /* 横全角25文字で１行分、４行以上になるとダメ */

  //-----------------------------------------ここからテキスト----------------------------------
        message[0].name = "魔理沙";
        message[0].serifu = "弾幕はパワー";
        message[1].name = "霊夢;";
        message[1].serifu = "お賽銭";
        message[2].name = "ああああ";
        message[2].serifu = "あああああああ\nいいいいいいいい";

    }


}
