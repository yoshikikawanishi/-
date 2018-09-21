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
    public TextClass[] message = new TextClass[100];

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
    private float textSpeed = 0.05f;
    //経過時間
    private float t = 0f;

    //メッセージをすべて表示したかどうか
    public bool isEndMessage = false;
    private bool onMessage = false;


    // Use this for initialization
    void Start() {

        for (int l = 0; l <= 99; l++) {
            message[l] = new TextClass();
        }
        //テキストを代入
        MessageText();

    }


    // Update is called once per frame
    void Update() {

        //表示開始
        if (startMessageTrigger) {
            startMessageTrigger = false;
            StartMessage();
            onMessage = true;
        }

        //表示
        if (onMessage) {
            PrintText();
        }

        
        //一つのセリフを表示しきった時
        if (isOneMessage) {
            if (Input.GetKeyDown(KeyCode.Z)) {
                isOneMessage = false;
                TextReset();
                if (k < n) {
                    k++;
                }
                else {
                    Destroy(messagePanel);
                    onMessage = false;
                    isEndMessage = true;
                }
            }
        }

        //表示速度
        if (Input.GetKeyDown(KeyCode.Z)) {
            textSpeed = 0.01f;
        }
        if (Input.GetKeyUp(KeyCode.Z)) {
            textSpeed = 0.05f;
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


    //セリフの表示
    public void PrintText() {

        //表示
        //名前
        nameText.text = message[k].name;
        //アイコン
        DisplayIcon();
        //セリフ
        if (t < textSpeed) {
            t += Time.deltaTime;
        }
        else {
            if (i < message[k].serifu.Length) {
                messageText.text += message[k].serifu[i];
                if (textLength >= maxLineText || message[k].serifu[i] == '\n') {
                    textLength = 0;
                }
                i++;
            }
            else {
                isOneMessage = true;
            }
            t = 0;
        }
        
    }


    private IEnumerator WaitSeconds(float s) {
        yield return new WaitForSeconds(s);
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
            case "にとり": charctorIcon.sprite = Resources.Load<Sprite>("Images/Icon/にとり"); break;
            case "爆発": break;
        }
    }

    //----------------------------------------------------------------------------------------------
    
    //表示するテキスト
    void MessageText() {
        int i = 0;
        /* 
       message[i].name = "";
       message[i].serifu = "";
       i++;
         */

        /*
         * kとnを設定して
         * messageManager.startMessageTrigger = true;
         * で表示開始
         * 終了時messageManager.isEndMessageがtrueになる
         * 
         */

        /* 横全角21文字で１行分、４行以上になるとダメ */
        /* 総メッセージ数をmessage配列の宣言、start関数内のforループの条件に入れるの注意 */

        //-----------------------------------------ここからテキスト----------------------------------
        message[i].name = "にとり";
        message[i].serifu = "...ついに...";

        i++;
        message[i].name = "にとり";
        message[i].serifu = "ついに出来たーーーーーーーーーーー！！！\n「異変製造機」君！！！";

        i++;
        message[i].name = "魔理沙";
        message[i].serifu = "今日も幻想郷は平和だz";

        i++;
        message[i].name = "魔理沙";
        message[i].serifu = "なに！？";

        i++;
        message[i].name = "魔理沙";
        message[i].serifu = "今の爆音は紅魔館の方か！\nいってみるぜ！";

        i = 5;
        message[i].name = "";
        message[i].serifu = "";

        i++;
        message[i].name = "";
        message[i].serifu = "";

        i++;
        message[i].name = "";
        message[i].serifu = "";

        i++;
        message[i].name = "";
        message[i].serifu = "";
    }


}


