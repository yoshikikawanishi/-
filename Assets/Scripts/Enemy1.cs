using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1面のザコ敵管理

public class Enemy1 : MonoBehaviour {

    //自機
    GameObject player;

    //向き
    private Vector3 scale;
    private int direction = 1;

    //赤妖精：０　青妖精：１
    public int enemyKind;
    private int hp = 0;

    //弾の速度
    private float bulletSpeed = 0;
    //弾の生成速度
    private float time = 0;
    private float span = 5.0f;
    //加速度
    private float a = 0;
    //移動を開始する自機のx座標
    public float startTrigger_Left = 0;
    public float startTrigger_Right = 0;
    private float trigger_x = 0;

    //移動開始の判定
    private bool startAct = false;

	// Use this for initialization
	void Start () {

        //自機の取得
        player = GameObject.Find("Marisa");
        
        //初期設定
        switch (enemyKind) {
            //赤妖精
            case 0:
                hp = 2;
                bulletSpeed = 3.0f;
                span = 5.0f;
                break;
            //青妖精
            case 1:
                hp = 3;
                bulletSpeed = 3.0f;
                span = 2.0f;
                a = 0.001f;
                break;
        }
        time = Random.Range(0, span - 0.1f);

        //向き
        scale = transform.localScale;
        //自機が右からくるとき
        if(player.transform.position.x > transform.position.x) {
            //反転させる
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            //弾の向き
            direction = -1;
            //トリガーになる時期の位置
            trigger_x = startTrigger_Right;
        }
        //自機が左からくるとき
        else {
            trigger_x = startTrigger_Left;
        }

    }


    // Update is called once per frame
    void Update () {

        //自機が所定の位置についた時スタート
        if (StartTrigger()) {
            startAct = true;
        }

        if (startAct) {
            switch (enemyKind) {
                case 0:
                    RedFairy();
                    break;
                case 1:
                    BlueFairy();
                    break;
            }
        }

    }


    //赤妖精
    void RedFairy() {
        //移動
        gameObject.transform.position -= new Vector3(0.05f * direction, 0, 0);
        Invoke("LeaveUp", 5.0f);
        //ショット
        if(time < span) {
            time += Time.deltaTime;
        }
        else {
            time = 0f;
            //弾を生成
            GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/EnemyBullet/弾1")) as GameObject;
            bullet.transform.position = transform.position;
            //弾の向きを変える
            bullet.transform.Rotate(new Vector3(0, 0, Random.Range(-20f, 20f)));
            //弾を発射
            bullet.GetComponent<Rigidbody2D>().velocity = -bullet.transform.right * bulletSpeed * direction;
            Destroy(bullet, 10f);
        }
        
    }

    //青妖精
    void BlueFairy() {

        //自機がの方向を向く
        if (player.transform.position.x > transform.position.x) {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
        else {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }

        //下から出現
        if (a <= 0.1f) {
            gameObject.transform.position += new Vector3(0, 0.1f - a, 0);
            a += 0.001f;
        }
        //下にはける
        Invoke("LeaveDown", 6.0f);
        
            //ショット
        if (time < span) {
            time += Time.deltaTime;
        }
        else {
            time = 0f;
            //弾を生成
            GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/EnemyBullet/弾1")) as GameObject;
            bullet.transform.position = transform.position;
            //弾の向きを自機に向ける
            bullet.transform.LookAt2D(player.transform, Vector2.right);
            //弾を発射
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;
            Destroy(bullet, 10f);
        }
    }


    //自機の位置を判別
    bool StartTrigger() {
        float x = trigger_x;
        if (x - 0.5f < player.transform.position.x && player.transform.position.x < x + 0.5f) {
            return true;
        }
        return false;

    }



    //画面上にはける
    void LeaveUp() {
        gameObject.transform.position += new Vector3(0, 0.1f, 0);
        if(gameObject.transform.position.y > 6f) {
            Destroy(gameObject);
        }
    }

    //画面下にはける
    void LeaveDown() {
        gameObject.transform.position -= new Vector3(0, 0.1f, 0);
        if (gameObject.transform.position.y < -6f) {
            Destroy(gameObject);
        }
    }

    //弾が当たった時
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "BulletTag") {
            hp--;
        }
        //hpが0になったとき
        if (hp <= 0) {
            DeleteEnemy();
        }

    }

     //消滅時の処理
    private void DeleteEnemy() {
        Destroy(gameObject);
    }

}
