using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaTest : MonoBehaviour {

    //移動速度
    private float v = 0.15f;
    private float back = -0.1f;
    private float up = 0.1f;

    //アニメーションコンポーネント
    private Animator anim;
    //現在のモード
    private bool isDashMode = true;
    //前進のアニメーション中かどうか
    private bool isGoForward = false;

	// Use this for initialization
	void Start () {
        //アニメーションコンポーネントの取得
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        //移動
        Transition();
        //アニメーション
        Animation();

        //モードの切り替え
        if (Input.GetKeyDown(KeyCode.X)) {
            if (isDashMode) {
                isDashMode = false;
                v = 0.1f;
            }
            else {
                isDashMode = true;
                v = 0.15f;
            }
        }
        
       
    }

    //移動
    void Transition() {
        
        //移動
        if (Input.GetKey(KeyCode.RightArrow)) {
            this.transform.Translate(v, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            this.transform.Translate(back, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            this.transform.Translate(0, up, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            this.transform.Translate(0, -up, 0);
        }
    }

    //アニメーション
    void Animation() {
        if (Input.GetKey(KeyCode.RightArrow) && !isGoForward) {
            anim.SetBool("GoForwardBool", true);
            isGoForward = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            anim.SetBool("GoForwardBool", false);
            isGoForward = false;
        }

        //モードの切り替え
        if (Input.GetKeyDown(KeyCode.X)) {
            if (isDashMode) {
                anim.SetBool("ShotModeBool", true);
                anim.SetBool("DashModeBool", false);
                anim.SetBool("GoForwardBool", false);
                isGoForward = false;

            }
            else {
                anim.SetBool("ShotModeBool", false);
                anim.SetBool("DashModeBool", true);
                anim.SetBool("GoForwardBool", false);
                isGoForward = false;
            }
        }
    }
}
