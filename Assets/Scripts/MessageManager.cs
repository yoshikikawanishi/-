using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {

    //表示する用のパネル
    GameObject messagePanel;
    //キャンバス
    GameObject canvas;

    //表示するメッセージ
    public class TextClass {
        public string name;
        public string serifu;
    }
    public TextClass[] message = new TextClass[4];

    //メッセージ表示のテキストコンポーネント
    private Text messageText;
    //キャラ名表示のテキストコンポーネント
    private Text nameText;
    //キャラのアイコン表示用
    private Image charctorIcon;


    //今のメッセージを表示しきったかどうか
    private bool isOneMessage = false;
    //ほかのスクリプトから操作するメッセージ表示開始のトリガー
    public bool startMessageTrigger = false;

    //１連のセリフの初めののメッセージ番号(配列番号)
    public int k = 0;
    //１連のセリフの最後の配列番号
    public int n = 3;
    //一つのセリフ中の今見ている文字番号
    private int i = 0;
    //行の左端からの現在の文字数
    private int textLength = 0;
    //１行に表示できる文字数
    private int maxLineText = 23;

    //テキストスピード
    [SerializeField]
    private float textSpeed = 0.08f;
    //経過時間
    private float t = 0f;

    //メッセージをすべて表示したかどうか
    private bool isEndMessage = true;


    // Use this for initialization
    void Start() {

        for (int l = 0; l < 4; l++) {
            message[l] = new TextClass();
        }
        //テキストを代入
        MessageText();

        
    }


    // Update is called once per frame
    void Update() {

        //パネルとテキストの生成、取得、メッセージ表示の開始
        if (startMessageTrigger && isEndMessage && i == 0) {
            //パネルの生成、メッセージ表示開始
            StartMessage();
            isEndMessage = false;
            startMessageTrigger = false;
            
        }

        //一連ののセリフを表示しきっていない
        if (!isEndMessage) {
            //一つのセリフを表示
            if (!isOneMessage) {
                PrintText();
            }

            //一つのセリフを表示しきったとき
            if (isOneMessage == true) {
                //zが押されたらメッセージの配列番号をインクリメントし、テキストをリセットする
                if (Input.GetKeyDown(KeyCode.Z)) {
                    k++;
                    TextReset();
                    isOneMessage = false;
                    //一連のセリフを表示しきった時
                    if (k > n) {
                        isEndMessage = true;
                        i = -1;
                    }
                    else {
                        nameText.text = message[k].name;
                        DisplayIcon();
                    }
                }
            }
        }

        //1連のセリフを表示しきったときzでパネルを消去
        if (isEndMessage) {
            if (Input.GetKeyDown(KeyCode.Z)) {
                TextReset();
                Destroy(messagePanel);
            }
        }

    }


    //メッセージ表示の開始
    void StartMessage() {
        //パネルとテキストを生成
        messagePanel = GameObject.Instantiate(Resources.Load("Prefabs/Panel")) as GameObject;
        canvas = GameObject.Find("Canvas");
        messagePanel.transform.SetParent(canvas.transform, false);

        //テキストを取得
        messageText = messagePanel.transform.GetChild(0).GetComponent<Text>();
        messageText.text = "";
        //キャラ名表示のテキストを取得
        nameText = messagePanel.transform.GetChild(1).GetComponent<Text>();
        nameText.text = ""; 
        //アイコン表示用のパネルを取得
        charctorIcon = messagePanel.transform.GetChild(2).GetComponent<Image>();
        
        //キャラ名の表示
        nameText.text = message[k].name;
        //アイコンの表示
        DisplayIcon();
    }


    //テキストのリセット
    void TextReset() {
        messageText.text = "";
        nameText.text = "";
        textLength = 0;
        t = 0f;
        i = 0;
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
        else if (!isOneMessage) {
            isOneMessage = true;
        }
    }

    //--------------------------------------------------------------------------------------------
    
    //アイコンの表示
    void DisplayIcon() {
        /*
         アイコン用のスプライトの名前はキャラクターの名前にする
         新しくアイコンを付け足す時はここのスイッチ分の中に名前を入れる
         */
 
        switch (message[k].name) {
            case "魔理沙": charctorIcon.sprite = Resources.Load<Sprite>("Images/Icon/魔理沙"); break;
            case "勇儀": charctorIcon.sprite = Resources.Load<Sprite>("Images/Icon/勇儀"); break;
        }
    }

    //----------------------------------------------------------------------------------------------
    
    //表示するテキスト
    void MessageText() {
        /*
       message[0].name = "";
       message[0].serifu = "";
      */

        /* 横全角21文字で１行分、４行以上になるとダメ */
        /* 総メッセージ数をmessage配列の宣言、start関数内のforループの条件に入れるの注意 */

        //-----------------------------------------ここからテキスト----------------------------------
        message[0].name = "魔理沙";
        message[0].serifu = "弾幕はパワー";
        message[1].name = "勇儀";
        message[1].serifu = "お賽銭";

        message[2].name = "魔理沙";
        message[2].serifu = "ああああああああああああああああああああああああああ\nいいいいいいいい";

        message[3].name = "勇儀";
        message[3].serifu = "それはそれは残酷ですわw";
    }


}


