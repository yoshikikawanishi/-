using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//霧の湖のザコ生成
public class EnemyGen1 : MonoBehaviour {

    //すでに生成済みかどうか
    private bool[] generated = new bool[20];
    
    //配列番号
    int n = 0;

    //生成を開始する自機のx座標
    private float g_Player;
    //自機と生成位置の距離
    private float distance;
    //生成位置
    private Vector3 g_Pos = new Vector3(0, 0, 0);
    //敵の種類
    private int g_Kind = 0;
    //数
    private int m = 0;

    //自機
    private GameObject player;

    //敵の生成情報
    public class GenInfo {
        //生成する位置
        public Vector3 pos;
        //敵の種類
        public int kind;
    }

	// Use this for initialization
	void Start () {
		
        //初期化
        for(int i = 0; i < 20; i++) {
            generated[i] = false;
        }

        //自機
        player = GameObject.Find("Marisa");

        n = 0;
        
        
	}
	

	// Update is called once per frame
	void Update () {

        Deploy();
        
	}


    //タイミングと座標と種類番号を引数で敵を生成
    void EnemyGenerate(GenInfo a) {
         
            GameObject enemy;
            //種類分け
            switch (a.kind) {
                case 0:
                    enemy = GameObject.Instantiate(Resources.Load("Prefabs/Enemy1/赤妖精")) as GameObject;
                    //座標
                    enemy.transform.position = a.pos;
                    break;
                case 1:
                    enemy = GameObject.Instantiate(Resources.Load("Prefabs/Enemy1/青妖精")) as GameObject;
                    //座標
                    enemy.transform.position = a.pos;
                    break;
                case 2:
                    break;

                case 3:
                    break;


            }
        
    }


    //自機が座標ｘにいるかどうか
    bool PlayerPosition(float x) {
        if(x - 0.5f < player.transform.position.x &&  player.transform.position.x < x + 0.5f) {
            return true;
        }
        return false;

    }

    //---------------------------------ここから敵の配置と生成タイミング-----------------------------------------

    void Deploy() {

        /*
         if (!generated[番号] && (PlayerPosition(戦闘のx座標 - 11f) || PlayerPosition(最後尾のx座標 + 11f))) {
            GenInfo enemy = new GenInfo { pos = new Vector3(生成座標), kind = 種類番号 };
            EnemyGenerate(enemy);
            generated[番号] = true;
        }       

         if (!generated[番号] && (PlayerPosition(先頭のx座標 - 11f) || PlayerPosition(最後尾のx座標 + 11f))) {
            GenInfo[] enemy = new GenInfo[数];
            for(int i = 0; i < 数; i++){
                enemy[i] = new GenInfo{ pos = new Vector3(生成座標), kind = 種類番号 };
            EnemyGenerate(enemy);
            }
            generated[番号] = true;
        }       
         */

        //赤
        if (!generated[0] && (PlayerPosition(15f - 10f) || PlayerPosition(15f + 10f))) {
            GenInfo redFaily = new GenInfo { pos = new Vector3(15f, 2f, 0), kind = 0 };
            EnemyGenerate(redFaily);
            generated[0] = true;
        }

        //赤３
        if (!generated[1] && (PlayerPosition(17f - 10f) || PlayerPosition(21f + 10f))) {
            GenInfo[] enemy = new GenInfo[3];
            for (int i = 0; i < 3; i++) {
                enemy[i] = new GenInfo { pos = new Vector3(17f + 2 * i, 2f, 0), kind = 0 };
                EnemyGenerate(enemy[i]);
            }
            generated[1] = true;
        }


    }

}
