using UnityEngine;
using System.Collections;

public class EnemyBulletHeart : MonoBehaviour {
	[SerializeField]
	private int power = 5;		// 弾の威力
	[SerializeField]
	private int forwardSpeed = 3;	// 直進方向の速さ
	[SerializeField]
	private float width = 3;	// 横方向移動の幅
	[SerializeField]
	private float period = 4;	// 横方向移動の周期
	private Vector3 forwardV;	// 直進方向の速度
	private Vector3 sideV;		// 横方向の速度

	private float timer = 0;	// 横方向移動制御用のタイマー
	private float lifeTime = 10;	// 弾の寿命
/*	private float turningPoint;	// 横移動の速度反転のタイミング (ジグザグ用)
	private bool isLoop = true;// 横移動の周期に入ったか否か(最初のperiod/4は特別) (ジグザグ用)	*/

	[SerializeField]
	private GameObject explosionPref;	// 爆発パーティクルのプレハブ

	void Start () {
		Destroy (gameObject, lifeTime);			// lifeTime後には消す

		forwardV = transform.forward.normalized * forwardSpeed;
		float sideSpeed = width;
/*		turningPoint = period / 4;				// (ジグザグ用)
		float sideSpeed = 4 * width / period;	// 横方向移動の速さ(ジグザグ用)	*/
		sideV = transform.right.normalized * sideSpeed;
	}

	// 弾の前方に速さspeedで移動
	void Update () {
		// 直進方向
		transform.position += forwardV * Time.deltaTime;
		// 横方向
		// Cos使用の場合（ちょっと正確さに欠ける？）
		float x = 2 * Mathf.PI * (timer / period);		// Cos()引数
		transform.position += sideV * Mathf.Cos (x) * Time.deltaTime;
		timer += Time.deltaTime;

/*		ジグザグパターン
		// period/2ごとに速度を反転(最初だけperiod/4)
		if (timer + Time.deltaTime <= turningPoint) {				
			transform.position += sideV * Time.deltaTime;
			timer += Time.deltaTime;
		} else {									// period/2以降は進行方向が反転
			transform.position += sideV * (turningPoint - timer);	// turningPointより超過分は無視
			sideV = -sideV;							// 速度を反転
			timer = 0;
			if (isLoop) {	// 最初は中央から始まる為、period/4で速度反転.以降はperiod/2ごと
				turningPoint = period / 2;
				isLoop = false;
			}
		}*/

		// 現在、カメラの都合上、横方向の制限を設けていない	
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
