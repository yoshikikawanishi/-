using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieManager : MonoBehaviour {
    
    //ゲームマネージャー
    GameManager gameManager;
    //メッセージマネージャー
    MessageManager messageManager;
 

    // Use this for initialization
    void Start () {
        //スクリプトの取得
        gameManager = GetComponent<GameManager>();
        messageManager = GetComponent<MessageManager>();
            
	}
	
	// Update is called once per frame
	void Update () {

        //タイトルシーン開始
        if (gameManager.isOpeningScene) {
            gameManager.isOpeningScene = false;
            Invoke("OPScene", 2.0f);
        }
    }

    public void OPScene() {
        messageManager.k = 2;
        messageManager.n = 2;
        messageManager.startMessageTrigger = true;
    }
}
