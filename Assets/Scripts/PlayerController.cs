using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //リジッドボディー取得用
    private Rigidbody2D _rigidbody;
    //スクロールする背景
    private GameObject scrollBackGround;
    //アニメーションコンポーネント
    private Animator anim;

    //移動速度
    private float v = 10f;
    private float up = 7f;
    private float dashVelocity = 10f;
    private float shotVelocity = 7f;
    //画面スクロールの速度
    private float scrollVelocity = 0.2f;
    private float upScrollVelocity = 0.14f;
    //弾速
    private float bulletSpeed = 20.0f;
    //弾の生成スパン
    private float span = 0.08f;
    private float time = 0;
    //弾の射程
    private float bulletRange = 0.5f;

    //向き
    private Vector3 scale;
    private Vector3 bulletScale;

    //現在のモード
    private bool isDashMode = true;
    //前進のアニメーション中かどうか
    private bool isGoForward = false;
    //背景をスクロールさせるか自機を移動させるか
    private bool isScrollForward = false;
    private bool isScrollBack = false;
  
    //スクリプト
    private GameManager gameManager;

    // Use this for initialization
    void Start() {
        //リジッドボディー取得
        _rigidbody = this.GetComponent<Rigidbody2D>();
        //アニメーションコンポーネントの取得
        anim = GetComponent<Animator>();
        //スクロールする背景の取得
        scrollBackGround = GameObject.Find("BackGround");
        //スクリプトの取得
        gameManager = GameObject.Find("Scripts").GetComponent<GameManager>();

        //大きさと向きの変数
        scale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update() {

        if (gameManager.isPlayable) {

            //移動
            Transition();
            //アニメーション
            Animation();

            //モードの切り替え
            if (Input.GetKeyDown(KeyCode.X)) {
                if (isDashMode) {
                    isDashMode = false;
                    v = shotVelocity;
                    scrollVelocity = 0.14f;
                }
                else {
                    isDashMode = true;
                    v = dashVelocity;
                    scrollVelocity = 0.2f;
                }
            }

            //ショット
            Shot();
            
        }

    }


    //背景をスクロールさせるかどうかの判別
    void JudgeScroll() {
        //右
        if (this.transform.position.x > 0.5f && !isScrollForward) {
            isScrollForward = true;
        }
        else if (this.transform.position.x <= 0.5f && isScrollForward) {
            isScrollForward = false;
        }
        //左
        if (this.transform.position.x < -0.5f && !isScrollBack) {
            isScrollBack = true;
        }
        else if (this.transform.position.x >= -0.5f && isScrollBack) {
            isScrollBack = false;
        }
       
    }


    //移動と向き
    void Transition() {

        //背景をスクロールさせるかどうかの判別
        JudgeScroll();

        //上移動
        if (Input.GetKey(KeyCode.UpArrow)) {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, up);           
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            _rigidbody.velocity = new Vector2(0, 0);
        }

        //下移動
        if (Input.GetKey(KeyCode.DownArrow)) {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -up);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            _rigidbody.velocity = new Vector2(0, 0);
        }

        //右移動
        if (Input.GetKey(KeyCode.RightArrow)) {
            //背景スクロール
            if (!isScrollForward) {
                _rigidbody.velocity = new Vector2(v, _rigidbody.velocity.y);
            }
            //自機自体が動く
            else {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                scrollBackGround.transform.position -= new Vector3(scrollVelocity, 0, 0);
            }
            //自機の方向変更
            if (!Input.GetKey(KeyCode.LeftShift)) {
                transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            _rigidbody.velocity = new Vector2(0, 0);
        }

        //左移動
        if (Input.GetKey(KeyCode.LeftArrow)) {
            //背景スクロール
            if (!isScrollBack) {
                _rigidbody.velocity = new Vector2(-v, _rigidbody.velocity.y);
            }
            //自機自体が動く
            else {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                scrollBackGround.transform.position += new Vector3(scrollVelocity, 0, 0);
            }
            //自機の方向変更
            if (!Input.GetKey(KeyCode.LeftShift)) {
                transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            _rigidbody.velocity = new Vector2(0, 0);
        }

        
    }

    //アニメーション
    void Animation() {
        //前進のアニメーション
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && !isGoForward) {
            anim.SetBool("GoForwardBool", true);
            isGoForward = true;
        }
        //その他のアニメーション
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


    //ショット
    void Shot() {
        if (Input.GetKey(KeyCode.Z) && !isDashMode) {
            //弾のスパンの計測
            if (time < span) {
                time += Time.deltaTime;
            }
            else {
                //弾の生成、発射
                time = 0f;
                GameObject Missile1 = GameObject.Instantiate(Resources.Load("Prefabs/Shot_Missile")) as GameObject;
                Destroy(Missile1, bulletRange);
                //向きの切り替え
                if (bulletSpeed < 0) {
                    Missile1.transform.localScale = new Vector3(-5, 5, 5);
                    Missile1.transform.position = this.transform.position + new Vector3(-0.2f, 0, 0);
                }
                else {
                    Missile1.transform.position = this.transform.position + new Vector3(0.2f, 0, 0);
                }
                Missile1.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            }
        }

        if (Input.GetKeyUp(KeyCode.Z)) {
            time = 0f;
        }

        //向きの切り替え
        if (!Input.GetKey(KeyCode.LeftShift)) {
            if (Input.GetKey(KeyCode.LeftArrow) && bulletSpeed > 0) {
                bulletSpeed = -20.0f;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && bulletSpeed < 0) {
                bulletSpeed = 20.0f;
            }
        }
    }
}
