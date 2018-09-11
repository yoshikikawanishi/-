using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaTest : MonoBehaviour {

    //リジッドボディー取得用
    private Rigidbody2D _rigidbody;
    //スクロールする背景
    private GameObject scrollBackGround;
    //アニメーションコンポーネント
    private Animator anim;

    //移動速度
    private float v = 10f;
    private float up = 7f;
    //画面スクロールの速度
    private float scrollVelocity = 0.2f;

    //向き
    private Vector3 scale;

    //現在のモード
    private bool isDashMode = true;
    //前進のアニメーション中かどうか
    private bool isGoForward = false;
    //背景をスクロールさせるか時期を移動させるか
    private bool isScrollForward = false;
    private bool isScrollBack = false;

	// Use this for initialization
	void Start () {
        //リジッドボディー取得
        _rigidbody = this.GetComponent<Rigidbody2D>();
        //アニメーションコンポーネントの取得
        anim = GetComponent<Animator>();
        //スクロールする背景の取得
        scrollBackGround = GameObject.Find("ScrollBackGround");

        //大きさと向きの変数
        scale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {

        //背景をスクロールさせるかどうかの判別
        JudgeScroll();

        //移動
        Transition();
        //アニメーション
        Animation();

        //モードの切り替え
        if (Input.GetKeyDown(KeyCode.X)) {
            if (isDashMode) {
                isDashMode = false;
                v = 7f;
                scrollVelocity = 0.14f;
            }
            else {
                isDashMode = true;
                v = 10f;
                scrollVelocity = 0.2f;
            }
        }
        
       
    }

    //背景をスクロールさせるかどうかの判別
    void JudgeScroll() {
        if (this.transform.position.x > 0.5f && !isScrollForward) {
            isScrollForward = true;
        }
        else if (this.transform.position.x <= 0.5f && isScrollForward) {
            isScrollForward = false;
        }
        if (this.transform.position.x < -0.5f && !isScrollBack) {
            isScrollBack = true;
        }
        else if (this.transform.position.x >= -0.5f && isScrollBack) {
            isScrollBack = false;
        }
    }


    //移動と向き
    void Transition() {
        
        //移動と向き
        if (Input.GetKey(KeyCode.RightArrow)) {
            if (!isScrollForward) {
                _rigidbody.velocity = new Vector2(v, _rigidbody.velocity.y);
            }
            else {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                scrollBackGround.transform.position -= new Vector3(scrollVelocity, 0, 0);
            }
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            _rigidbody.velocity = new Vector2(0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (!isScrollBack) {
                _rigidbody.velocity = new Vector2(-v, _rigidbody.velocity.y);
            }
            else {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                scrollBackGround.transform.position += new Vector3(scrollVelocity, 0, 0);
            }
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            _rigidbody.velocity = new Vector2(0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, up);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            _rigidbody.velocity = new Vector2(0, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -up);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            _rigidbody.velocity = new Vector2(0, 0);
        }
    }

    //アニメーション
    void Animation() {
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && !isGoForward) {
            anim.SetBool("GoForwardBool", true);
            isGoForward = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) {
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
