using UnityEngine;
using System.Collections;

public class EnemyBulletClub : MonoBehaviour {
	[SerializeField]
	private int power = 5;		// 弾の威力
	[SerializeField]
	private int speed = 5;		// 直進方向の速さ
	private float lifeTime = 10;	// 弾の寿命

	[SerializeField]
	private Transform divisionBullet;	// 分裂弾のプレハブ
	[SerializeField]
	private float divisionTime = 0.5f;	// 分裂するタイミング
	[SerializeField]
	private int divisionSpeed = 3;		// 分裂後の速さ
	[SerializeField]
	private float divisionScale = 0.5f;	// 分裂後の大きさ
	private float divisionAngle = 30;	// 分裂弾の角度

	[SerializeField]
	private GameObject explosionPref;	// 爆発パーティクルのプレハブ

	void Start () {
		Destroy (gameObject, lifeTime);			// lifeTime後には消す

		Invoke ("Divide", divisionTime);
	}

	// 弾の前方に速さspeedで移動
	void Update () {
		transform.position += transform.forward.normalized * speed * Time.deltaTime;

		/*	現在、カメラの都合上、横方向の制限を設けていない		*/
		if ((transform.position.z > 20) || (transform.position.z < -10)) {
			Destroy (gameObject);
		}
	}

	private void Divide(){	// 弾を３つに分裂
		Transform trans = (Transform)Instantiate (divisionBullet, transform.position, transform.rotation);
		trans.SetParent (transform.parent, true);
		trans.Rotate (0, divisionAngle, 0);
		trans = (Transform)Instantiate (divisionBullet, transform.position, transform.rotation);
		trans.SetParent (transform.parent, true);
		trans.Rotate (0, -divisionAngle, 0);
		transform.localScale = new Vector3 (divisionScale, divisionScale, divisionScale);
		speed = divisionSpeed;
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
