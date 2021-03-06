﻿using UnityEngine;
using System.Collections;

// 敵の弾の挙動
public class EnemyBullet : MonoBehaviour {
	[SerializeField]
	private int power = 5;		// 弾の威力
	[SerializeField]
	private int speed = 5;		// 直進方向の速さ
	private float lifeTime = 10;	// 弾の寿命

	[SerializeField]
	private GameObject explosionPref;	// 爆発パーティクルのプレハブ

	void Start () {
		Destroy (gameObject, lifeTime);			// lifeTime後には消す
	}
	
	// 弾の前方に速さspeedで移動
	void Update () {
		transform.position += transform.forward.normalized * speed * Time.deltaTime;

		/*	現在、カメラの都合上、横方向の制限を設けていない		*/
		if ((transform.position.z > 20) || (transform.position.z < -10)) {
			Destroy (gameObject);
		}
	}

	// Playerと当たったらダメージを与える
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			other.GetComponent<PlayerManager> ().Damage (power);
			GameObject exp = (GameObject)Instantiate (explosionPref, transform.position, Quaternion.identity);
			float scale = power * 0.1f;
			exp.transform.localScale = new Vector3 (scale, scale, scale);	// 威力によって爆発の大きさを変える
			Destroy (this.gameObject);
		}
	}
}
