using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //シングルトン用
    public static GameManager instance;

    //オープニングシーンかどうか
    public bool isOpeningScene = false;


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
       
	}
	
	// Update is called once per frame
	void Update () {

		
	}







    //スタートボタン押下時に呼ばれる関数
    public void GameStart() {
        SceneManager.LoadScene("OPScene");
        isOpeningScene = true;
    }

}
