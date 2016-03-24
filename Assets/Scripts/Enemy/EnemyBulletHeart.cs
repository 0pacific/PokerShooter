using UnityEngine;
using System.Collections;

public class EnemyBulletHeart : MonoBehaviour {
	[SerializeField]
	private int power = 5;		// 弾の威力
	[SerializeField]
	private int speed = 4;		// 直進方向の速さ
	[SerializeField]
	private float width = 5;	// 振動の幅
	[SerializeField]
	private float period = 1;	// 振動の周期
	private float timer = 0;	// 振動制御用のタイマー
	private float lifeTime = 5;	// 弾の寿命

	void Start () {
		transform.parent = GameObject.Find ("EnemyBullets").transform;
		Destroy (gameObject, lifeTime);			// lifeTime後には消す
	}

	// 弾の前方に速さspeedで移動
	void Update () {
		// 直進方向
		transform.position += transform.forward.normalized * speed * Time.deltaTime;
		// 横方向
		transform.position += transform.right.normalized * width * Mathf.Cos (2 * timer / period * Mathf.PI) * Time.deltaTime;
		timer += Time.deltaTime;
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
