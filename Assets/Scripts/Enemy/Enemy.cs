using UnityEngine;
using System.Collections;

// 各スートの順番
public enum Suits {spade, heart, diamond, club};

// 敵の基本的挙動（移動以外）
public class Enemy : MonoBehaviour {
	private int suit = 0;			// マークの種類
	private int num = 0;			// カードのナンバー

	private int health = 100;		// 敵のHP
	private int captureLine = 10;	// 捕獲可能になる値(ボールの威力)

	[SerializeField]
	private GameObject bullet;		// 弾のプレハブ
	[SerializeField]
	private Transform[] shootPoint;		// 弾の発射位置	
	private float shootDuration = 0.5f;	// 弾の発射間隔

	[SerializeField]
	private Animator frontAnimator;		// カード表面用のアニメーションコンポーネント

	private bool isReverse = false;		// 裏返しの状態か否か
	private bool isCapture = false;		// 捕獲可能状態か否か

	// 敵の初期化
	public void Initialize(int type, int no, int hp, Vector3 pos){
		suit = type;
		num = no;
		health = hp;
		transform.position = pos;
	}

	// 敵の弾、発射位置の初期化
	public void SetShoot(GameObject bulletPref, Transform[] shootTrans, float duration){
		bullet = bulletPref;
		shootPoint = shootTrans;
		shootDuration = duration;
	}

	// テスト用の初期化
	void Start () {
		Initialize (1, 7, 100, new Vector3 (0, 0, 15));
		if (shootPoint.Length != 0) {	// 発射位置がなければ、コルーチンを呼び出さない。
			StartCoroutine ("Shoot");
		}
		Invoke ("RevTes", 1);
		Invoke ("Dead", 5);
		Damage (90);
	}

	void Update () {
		
	}

	// shootDurationごとに弾をshootPointから発射
	IEnumerator Shoot(){
		while (true) {
			for (int i = 0; i < shootPoint.Length; i++) {
				GameObject.Instantiate (bullet, shootPoint[i].position, shootPoint[i].rotation);
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
			transform.rotation = Quaternion.Euler(Vector3.up * 180 * (timer / reverseTime));
			timer += Time.deltaTime;
			yield return null;
		}
		isReverse = !isReverse;
		if (isReverse) {
			transform.rotation = Quaternion.Euler (0, 180, 0);
		} else {
			transform.rotation = Quaternion.identity;
		}
	}

	// 弾とか当たったらダメージを与える
	public void Damage(int damege){
		health -= damege;
		if (health <= 0) {
			Dead ();
		} else if (health <= captureLine) {
			isCapture = true;
			frontAnimator.SetBool ("isDying", true);
		}
	}

	// enemyを消します
	private void Dead(){

		Destroy (gameObject);
	}
}
