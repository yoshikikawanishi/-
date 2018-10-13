using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public int bulletKind;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //衝突したら弾を消す
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "EnemyTag") {
            Destroy(gameObject);
        }
    }



}
