using UnityEngine;
using System.Collections;

public class EnemyBulletHeart : MonoBehaviour {
	[SerializeField]
	private int power = 5;		// 弾の威力
	[SerializeField]
	private int speed = 4;		// 直進方向の速さ
	[SerializeField]
	private float width = 1;	// 横方向移動の幅
	[SerializeField]
	private float period = 1;	// 横方向移動の周期
	private float horizontalSpeed;	// 横方向移動の速さ
	private float timer = 0;	// 横方向移動制御用のタイマー
	private float lifeTime = 5;	// 弾の寿命

	void Start () {
		transform.parent = GameObject.Find ("EnemyBullets").transform;
		Destroy (gameObject, lifeTime);			// lifeTime後には消す

		horizontalSpeed = 4 * width / period;
		transform.position -= transform.right.normalized * width;
	}

	// 弾の前方に速さspeedで移動
	void Update () {
		// 直進方向
		transform.position += transform.forward.normalized * speed * Time.deltaTime;
		// 横方向
		Vector3 horizontalV = transform.right.normalized * horizontalSpeed;
		if (timer + Time.deltaTime <= period/2) {				
			transform.position += horizontalV * Time.deltaTime;
			timer += Time.deltaTime;
		} else {											// period/2以降は進行方向が反転
			float diff = period - 2*timer - Time.deltaTime;	// 超過分を引いたもの(=Δ-2*(timer+Δ-period/2))
			transform.position += horizontalV * diff;
			horizontalSpeed = -horizontalSpeed;
			timer = 0;
		}

		/*		Cos使用の場合（ちょっと正確さに欠ける？）
		transform.position += transform.right.normalized * width * Mathf.Cos (2 * timer / period * Mathf.PI) * Time.deltaTime;
		timer += Time.deltaTime;*/
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
