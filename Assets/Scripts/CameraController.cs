using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //自機
    GameObject player;
    PlayerController playerController;

    //ステージの左端
    public float leftSide = 0;
    //右端
    public float rightSide = 100f;
    //自機とカメラの距離
    private float i = 0;
    //カメラの移動速度
    private float v = 0.1f;
    //向き変更時のカメラの位置
    private float d = 1.0f;

	// Use this for initialization
	void Start () {

        //自機
        player = GameObject.Find("Marisa");
        playerController = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

        //カメラを自機に追従
        if (playerController.isRight) {
            if (i < d) {
                transform.position = new Vector3(player.transform.position.x + i, 0, -10);
                i += v;
            }
            else {
                transform.position = new Vector3(player.transform.position.x + d, 0, -10);
            }
        }
        else {
            if (i > -d) {
                transform.position = new Vector3(player.transform.position.x +i, 0, -10);
                i -= v;
            }
            else {
                transform.position = new Vector3(player.transform.position.x - d, 0, -10f);
            }
        }
        //左端のときスクロールを止める
        if (transform.position.x < leftSide) {
            transform.position = new Vector3(leftSide, 0, -10);
        }
        //右端のときスクロールを止める
        if (transform.position.x >= rightSide) {
            transform.position = new Vector3(rightSide, 0, -10);
        }
    }
}
