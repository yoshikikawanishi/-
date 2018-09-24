using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //シングルトン用
    public static GameManager instance;

    //スクリプト
    private MessageManager messageManager;
    private MovieManager movieManager;

    //現在のシーン
    private string nowScene = "TitleScene";
    //前のシーン
    private string oldScene = "TitleScene";

    //プレイヤーが操作可能かどうか
    public bool isPlayable = true;

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
        movieManager = GetComponent<MovieManager>();

	}
	
	// Update is called once per frame
	void Update () {

        //現在のシーン
        nowScene = SceneManager.GetActiveScene().name;
 
      
        //オープニングシーンの開始
        if(nowScene == "OPScene" && nowScene != oldScene) {
            oldScene = "OPScene";
            movieManager.startOPMV = true;
            isPlayable = false;
        }

        //オープニングムービーの終了
        if (movieManager.endOPMV) {
            movieManager.endOPMV = false;
            SceneManager.LoadScene("霧の湖");
        }

        //1面の開始
        if (nowScene == "霧の湖" && nowScene != oldScene) {
            oldScene = "霧の湖";
            isPlayable = true;
        }
    }







    //スタートボタン押下時に呼ばれる関数
    //オープニングシーンに遷移
    public void GameStart() {
        SceneManager.LoadScene("OPScene");
    }

}
