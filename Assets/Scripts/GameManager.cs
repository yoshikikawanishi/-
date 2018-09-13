using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //シングルトン用
    public static GameManager instance;

    //スクリプト
    private MessageManager messageManager;

    //オープニングシーン開始
    public bool startOP = false;
    //オープニングシーン終了
    public bool endOP = false;
    //ゲームシーン開始
    public bool[] startGameSceneMV = {false, false, false, false, false, false };
    //ゲームシーン終了
    public bool[] endGameSceneMV = { false, false, false, false, false, false };

    //プレイヤーが操作可能かどうか
    public bool isPlayable = false;

    void Awake() {
        //シングルトン
        if (instance != null) {
            Destroy(this.gameObject);
        }
        else if (instance == null) {
            instance = this;
        }
        //シーンを遷移してもオブジェクトを消さない
        DontDestroyOnLoad(gameObject);
    }


    // Use this for initialization
    void Start () {

        //スクリプトの取得
        messageManager = GetComponent<MessageManager>();
	}
	
	// Update is called once per frame
	void Update () {

        //オープニングの終了、ゲームシーンの開始
        if (SceneManager.GetActiveScene().name == "OPScene") {
            if (messageManager.isEndMessage) {
                messageManager.isEndMessage = false;
                endOP = true;
                startGameSceneMV[0] = true;
            }
        }

        //ゲームシーンのムービー1の終了
        if(SceneManager.GetActiveScene().name == "GameScene") {
            if (messageManager.isEndMessage) {
                messageManager.isEndMessage = false;
                endGameSceneMV[0] = true;
                isPlayable = true;
            }
        }

    }







    //スタートボタン押下時に呼ばれる関数
    //オープニングの開始
    public void GameStart() {
        SceneManager.LoadScene("OPScene");
        startOP = true;
    }

}
