using UnityEngine;
using System.Collections;

// 敵の弾の挙動
public class EnemyBullet : MonoBehaviour {
	[SerializeField]
	private int power = 5;
	[SerializeField]
	private int speed = 10;
	private float lifeTime = 5;

	void Start () {
		transform.parent = GameObject.Find ("EnemyBullets").transform;
		Destroy (gameObject, lifeTime);			// lifeTime後には消す
	}
	
	// 弾の前方に速さspeedで移動
	void Update () {
		transform.position += transform.forward.normalized * speed * Time.deltaTime;
/*		カメラの都合上、横方向の消滅が不自然なため保留
  		if (Mathf.Abs (transform.position.x) > GameController.windowWidth + 1
			||	transform.position.z > 20
			||	transform.position.z < -15) {
			Destroy (gameObject);
		}*/
	}

	// Playerと当たったらダメージを与える
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			other.GetComponent<PlayerManager> ().Damage (power);
			Destroy (this.gameObject);
		}
	}
}
