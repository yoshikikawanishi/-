using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovieManager : MonoBehaviour {
    
    //ゲームマネージャー
    GameManager gameManager;
    //メッセージマネージャー
    MessageManager messageManager;
    //シーンエフェクト
    SceneEffect sceneEffect;

    //ムービーシーンの番号
    private int movieNumber = -1;

    //ムービーナンバー0の背景スクロールの速度
    private float scroll = 0.1f;

    //オープニングシーン開始
    public bool startOPMV = false;
    //オープニングシーン終了
    public bool endOPMV = false;
    //ゲームシーン中ムービー開始
    public bool[] startGameSceneMV = { false, false, false, false, false, false };
    //ゲームシーン中ムービー終了
    public bool[] endGameSceneMV = { false, false, false, false, false, false };

    //背景取得用
    GameObject backGround;

    // Use this for initialization
    void Start () {
        //スクリプトの取得
        gameManager = GetComponent<GameManager>();
        messageManager = GetComponent<MessageManager>();
        
    }
	
	// Update is called once per frame
	void Update () {


        //コルーチン

        //OPシーン開始
        if (startOPMV) {
            startOPMV = false;
            StartCoroutine("OPScene");
        }   



        //アップデート内の処理

        switch (movieNumber) {
            //オープニングシーン
            case 0:
                //背景の移動
                backGround.transform.position -= new Vector3(scroll, 0, 0);
                break;                

        }

    }


    //オープニングシーンのムービーのコルーチン
    private IEnumerator OPScene() {
        //明転開始
        sceneEffect = GameObject.Find("BlackOut").GetComponent<SceneEffect>();
        sceneEffect.startLightUp = true;
        yield return new WaitForSeconds(2.0f);
        //明転終了
        sceneEffect.startLightUp = false;

        //メッセージ表示
        messageManager.k = 0;
        messageManager.n = 1;
        messageManager.startMessageTrigger = true;
        //メッセージ表示終了
        yield return new WaitUntil(EndMessage);
        messageManager.isEndMessage = false;
        
        //暗転開始
        sceneEffect.startBlackOut = true;
        yield return new WaitForSeconds(2.0f);

        //シーン遷移
        SceneManager.LoadScene("OPScene2");

        //背景のスクロール
        yield return new WaitForSeconds(0.1f);
        backGround = GameObject.Find("BackGround");
        movieNumber = 0;
        yield return new WaitForSeconds(2.0f);

        //メッセージ表示
        messageManager.k = 2;
        messageManager.n = 2;
        messageManager.startMessageTrigger = true;
        //メッセージ表示終了
        yield return new WaitUntil(EndMessage);
        messageManager.isEndMessage = false;

        //爆発音とエフェクト
        yield return new WaitForSeconds(1.0f);
        
        
        //背景のスクロールの停止
        movieNumber = -1;

        //メッセージ表示
        messageManager.k = 3;
        messageManager.n = 4;
        messageManager.startMessageTrigger = true;
        //メッセージ表示終了
        yield return new WaitUntil(EndMessage);
        messageManager.isEndMessage = false;

        //暗転、背景のスクロール開始
        scroll = 0.3f;
        movieNumber = 0;
        sceneEffect = GameObject.Find("BlackOut").GetComponent<SceneEffect>();
        sceneEffect.startSceneChange = true;
        yield return new WaitForSeconds(1.0f);
        movieNumber = -1;

        //オープニングシーン終了
        endOPMV = true;
    }



    //メッセージ表示の終了
    private bool EndMessage() {
        return messageManager.isEndMessage;
    }
}
