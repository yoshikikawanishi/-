using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rumia : MonoBehaviour {

    //段階
    private int PhaseNumber = 0;
    
    //2π
    private float oneCycle;
    //弾の生成スパン
    private float time = 0f;
    private float span = 3.0f;

    //交差弾用
    //生成時の半径
    private float radius = 0.2f;
    //1つの円の弾数
    private int circle = 10;
    //広がるときの弾速
    private float spreadV = 3.0f;
    




    
	// Use this for initialization
	void Start () {
        
        //2π
        oneCycle = 2.0f * Mathf.PI;

        PhaseNumber = 1;
	}
	
	// Update is called once per frame
	void Update () {

        switch (PhaseNumber) {
            //1段階目
            case 1: Phase1(); break;

        }
	}


    //1段階目
    private void Phase1() {

        //交差弾
        if (time < span) {
            time += Time.deltaTime;
        }
        else {
            time = 0;
            for (int i = 0; i < circle; i++) {
                //時計回りする緑弾
                //生成
                var point = ((float)i / circle) * oneCycle;
                var x = Mathf.Cos(point) * radius;
                var y = Mathf.Sin(point) * radius;
                var position = gameObject.transform.position + new Vector3(x, y);
                GameObject greenBullet = GameObject.Instantiate(Resources.Load("Prefabs/EnemyBullet/交差弾1")) as GameObject;
                greenBullet.transform.position = position;
                //広げる
                greenBullet.transform.LookAt2D(this.transform, Vector2.right);
                greenBullet.GetComponent<Rigidbody2D>().velocity = greenBullet.transform.right * spreadV;

                //反時計回りする黄色弾
                point = ((float)i / circle) * oneCycle + 0.1f * Mathf.PI;
                x = Mathf.Cos(point) * radius;
                y = Mathf.Sin(point) * radius;
                position = gameObject.transform.position + new Vector3(x, y);
                GameObject yellowBullet = GameObject.Instantiate(Resources.Load("Prefabs/EnemyBullet/交差弾2")) as GameObject;
                yellowBullet.transform.position = position;
                //広げる
                yellowBullet.transform.LookAt2D(this.transform, Vector2.right);
                yellowBullet.GetComponent<Rigidbody2D>().velocity = yellowBullet.transform.right * spreadV;

            }
        }
    }
}
