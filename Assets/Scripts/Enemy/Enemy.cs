using UnityEngine;
using System.Collections;

// 各スートの順番
public enum Suits {spade, heart, diamond, club};

// 敵の基本的挙動（移動以外）
public class Enemy : MonoBehaviour {
	private int suit = 0;			// マークの種類
	private int num = 0;			// カードのナンバー
	private int hp = 100;			// 敵のHP

	[SerializeField]
	private GameObject bullet;		// 弾のプレハブ
	[SerializeField]
	private Transform[] shootPoint;		// 弾の発射位置	
	private float shootDuration = 1f;	// 弾の発射間隔

	public GameObject enemyBullets;	// 敵の弾の管理オブジェクト
	[SerializeField]
	private GameObject enemyManager;	// EnemyManagerへの参照

	[SerializeField]
	private Component[] movings;		// アタッチされてるEnemyMovingコンポーネント
	[SerializeField]
	private Animator frontAnimator;		// カード表面用のAnimatorコンポーネント
	public SpriteRenderer frontRenderer;// カード表面のSpriteRendererコンポーネント

	private bool isReversed = false;		// 裏返しの状態か否か
	private bool isCapturable = false;		// 捕獲可能状態か否か

	// 敵の初期化
	public void Initialize(int type, int no){
		suit = type;
		num = no;

		hp = GameController.enemyHP[no];
	}

	// 敵の弾、発射位置の初期化
	public void SetShoot(GameObject bulletPref, Transform[] shootTrans, float duration){
		bullet = bulletPref;
		shootPoint = shootTrans;
		shootDuration = duration;
	}

	// テスト用の初期化
	void Start () {
		if (shootPoint.Length != 0) {	// 発射位置がなければ、コルーチンを呼び出さない。
			StartCoroutine ("Shoot");
		}

		Invoke ("RevTes", 1);
		Invoke ("RevTes", 4);
		Invoke ("Dead", 20);
	}

	void Update () {
		/*	現在、カメラの都合上、横方向の制限を設けていない		*/
		if ((transform.position.z > 20) || (transform.position.z < -10)) {
			Dead();
		}
	}

	// shootDurationごとに弾をshootPointから発射
	IEnumerator Shoot(){
		while (true) {
			if (Mathf.Abs (transform.eulerAngles.y) < 90) { 
				for (int i = 0; i < shootPoint.Length; i++) {
					GameObject b = (GameObject)Instantiate (bullet, shootPoint [i].position, shootPoint [i].rotation);
					b.transform.SetParent (enemyBullets.transform, true);
				}
			}
			yield return new WaitForSeconds (shootDuration);
		}
	}

	private void RevTes(){
		StartCoroutine (Reverse (1f));
	}

	// reverseTime秒で裏返す
	IEnumerator Reverse(float reverseTime){
		float timer = 0;
		while (timer < reverseTime) {
			transform.Rotate(0, 180 * (Time.deltaTime / reverseTime), 0);
			timer += Time.deltaTime;
			yield return null;
		}
		isReversed = !isReversed;
		if (isReversed) {
			transform.rotation = Quaternion.Euler (0, 180, 0);
		} else {
			transform.rotation = Quaternion.identity;
		}
	}

	// 弾とか当たったらダメージを与える
	public void Damage(int damege){
		hp -= damege;
		if (hp <= 0) {
			Dead ();
		} else if (hp <= GameController.ballPower) {
			isCapturable = true;
			frontAnimator.SetBool ("isDying", true);
		}
	}

	// ボールが当たった場合に呼び出す
	public void Capture(){
		if (isCapturable) {
			//	カードの登録などの処理	//

			Destroy (gameObject);
			//////////////////////////
		}
		hp -= GameController.ballPower;
	}

	// 敵を倒した時の処理
	private void Dead(){
		EnemyManager em = enemyManager.GetComponent<EnemyManager> ();
		em.ResetCardProb (suit, num);	// 再び同じカードが出現するようにする
		Destroy (gameObject);
	}

	// EnemyMoving系を切り替える
	public void SetMoving(int pattern){
		// 現在アタッチされてるものを削除
		for (int i = 0; i < movings.Length; i++) {
			if (movings [i] != null) {
				Destroy (movings [i]);
			}
		}

		switch (pattern) {
		case 0:
			System.Array.Resize(ref movings, 1);
			movings [0] = (EnemyMoving0)gameObject.AddComponent<EnemyMoving0> ();
			break;
		case 1:
			System.Array.Resize(ref movings, 1);
			movings [0] = (EnemyMoving1)gameObject.AddComponent<EnemyMoving1> ();
			break;
		case 2:
			System.Array.Resize(ref movings, 2);
			movings [0] = (EnemyMoving0)gameObject.AddComponent<EnemyMoving0> ();
			movings [1] = (EnemyMoving1)gameObject.AddComponent<EnemyMoving1> ();
			break;
		default :
			System.Array.Resize(ref movings, 1);
			movings [0] = (EnemyMoving0)gameObject.AddComponent<EnemyMoving0> ();
			break;
		}
	}
}
