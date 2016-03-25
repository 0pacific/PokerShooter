using UnityEngine;
using System.Collections;

public class EnemyBulletClub : MonoBehaviour {
	[SerializeField]
	private int power = 5;		// 弾の威力
	[SerializeField]
	private int speed = 7;		// 直進方向の速さ
	[SerializeField]
	private int dividedSpeed = 4;	// 分裂後の速さ
	private float lifeTime = 5;	// 弾の寿命

	[SerializeField]
	private Transform divisionBullet;	// 分裂弾のプレハブ
	[SerializeField]
	private float divisionTime = 1;		// 分裂するタイミング
	private float divisionAngle = 45;	// 分裂弾の角度

	void Start () {
		transform.parent = GameObject.Find ("EnemyBullets").transform;
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
		trans.Rotate (0, divisionAngle, 0);
		trans = (Transform)Instantiate (divisionBullet, transform.position, transform.rotation);
		trans.Rotate (0, -divisionAngle, 0);
		speed = dividedSpeed;
	}

	// Playerと当たったらダメージを与える
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			other.GetComponent<PlayerManager> ().Damage (power);
			Destroy (this.gameObject);
		}
	}
}
