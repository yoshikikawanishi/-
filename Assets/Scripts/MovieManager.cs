using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovieManager : MonoBehaviour {
    
    //ゲームマネージャー
    GameManager gameManager;
    //メッセージマネージャー
    MessageManager messageManager;

    //ムービーシーンの番号
    public int movieNumber = 0;

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
        if (gameManager.startOP) {
            gameManager.startOP = false;
            StartCoroutine("OPScene");
        }
        //OPシーン終了
        if (gameManager.endOP) {
            gameManager.endOP = false;
            SceneManager.LoadScene("GameScene");
        }

        //ゲームシーンのムービー１開始
        if (gameManager.startGameSceneMV[0]) {
            gameManager.startGameSceneMV[0] = false; 
            StartCoroutine("GameSceneMV1");
        }

        //ゲームシーンのムービー1終了
        if (gameManager.endGameSceneMV[0]) {
            gameManager.endGameSceneMV[0] = false;
            movieNumber = 0;
        }

        //アップデート内の処理
        switch (movieNumber) {

            //ゲームシーンの冒頭
            case 1: backGround.transform.position -= new Vector3(0.1f, 0, 0);  break;

        }


    }


    //オープニングシーンのムービーのコルーチン
    private IEnumerator OPScene() {
        yield return new WaitForSeconds(2.0f);
        GameObject OPBackGround = GameObject.Find("BackGround");
        OPBackGround.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.7f);
        yield return new WaitForSeconds(3.0f);
        OPBackGround.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1.0f);
        messageManager.k = 0;
        messageManager.n = 3;
        messageManager.startMessageTrigger = true;

    }

    //ゲームシーンのムービー1のコルーチン
    private IEnumerator GameSceneMV1() {
        yield return new WaitForSeconds(0.1f);
        backGround = GameObject.Find("BackGround");
        movieNumber = 1;
        messageManager.k = 4;
        messageManager.n = 5;
        messageManager.startMessageTrigger = true;
        
    }

}
