using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEffect : MonoBehaviour {

    //スクリプト
    MessageManager messageManager;

    //オープニングシーンの明転開始
    public bool startLightUp = false;
    //オープニングシーンの暗転開始
    public bool startBlackOut = false;
    //ワールドマップへ
    public bool startSceneChange = false;

	// Use this for initialization
	void Start () {
        //スクリプトの取得
        messageManager = GameObject.Find("Scripts").GetComponent<MessageManager>();

       
        
    }
	
	// Update is called once per frame
	void Update () {

        //オープニングシーンでの明転用
        if (startLightUp) {
            gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.01f);
        }
        //オープニングシーンでの暗転用
        if(startBlackOut) {
            gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.01f);
        }

        //シーン遷移時
        if (startSceneChange) {
            gameObject.transform.position -= new Vector3(0.5f, 0, 0);
        }
	}
}
