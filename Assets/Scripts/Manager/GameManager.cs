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

    //自機
    private GameObject player;

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
        if (nowScene == "OPScene2" && movieManager.endOPMV) {
            movieManager.endOPMV = false;
            SceneManager.LoadScene("霧の湖1");
        }

        //1面の開始
        if (nowScene == "霧の湖1" && nowScene != oldScene) {
            oldScene = "霧の湖1";
            isPlayable = true;
            //自機の取得
            player = GameObject.Find("Marisa");
        }

        //1面中ボスムービー開始
        if (nowScene == "霧の湖1") {
            if (player.transform.position.x > 108f && isPlayable) {
                isPlayable = false;
                StartCoroutine("SceneChange","1面中ボス");
            }
        }
        if(nowScene == "1面中ボス" && nowScene != oldScene) {
            oldScene = nowScene;
            movieManager.startGameSceneMV[0] = true;
        }

        //1面中ボス戦闘開始
        if(nowScene == "1面中ボス" && movieManager.endGameSceneMV[0]) {
            movieManager.endGameSceneMV[0] = false;
            isPlayable = true;
        }
        
    }







    //スタートボタン押下時に呼ばれる関数
    //オープニングシーンに遷移
    public void GameStart() {
        SceneManager.LoadScene("OPScene");
    }

    //シーン遷移時の関数
    private IEnumerator SceneChange(string nextScene) {
        SceneEffect sceneEfect = GameObject.Find("BlackOut").GetComponent<SceneEffect>();
        sceneEfect.startSceneChange = true;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(nextScene);

    }

}
