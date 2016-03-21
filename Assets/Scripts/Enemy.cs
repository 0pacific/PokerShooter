using UnityEngine;
using System.Collections;

// 
public class Enemy : MonoBehaviour {
	private int enemyType = 0;			// マークの種類
	private int enemyNo = 0;			// カードのナンバー

	private int enemyHealth = 100;		// 敵のHP
	private int dyingLine;				// 捕獲可能になる
	private int speed = 5;				// 敵の移動スピード

	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private Transform[] shootPoint;		// 弾の発射位置	
	private float shootDuration = 0.2f;	// 弾の発射間隔

	private bool isReverse = false;		// 裏返しの状態か否か
	private bool isDying = false;		// 瀕死状態か否か

	// 敵の配置時、初期化
	public void Initialize(int type, int no, int health, Vector3 pos){
		enemyType = type;
		enemyNo = no;
		enemyHealth = health;
		transform.position = pos;

		dyingLine = (int)(enemyHealth * 0.1f);
	}

	// テスト用の初期化
	void Start () {
		Initialize (0, 7, 100, new Vector3 (0, 0, 15));
		StartCoroutine ("Shoot");
		Invoke ("RevTes", 1);
		Invoke ("Dead", 5);
	}

	void Update () {
		Move ();
	}

	// shootDurationごとに弾をshootPointから発射
	IEnumerator Shoot(){
		while (true) {
			for (int i = 0; i < shootPoint.Length; i++) {
				GameObject.Instantiate (bulletPrefab, shootPoint[i].position, shootPoint[i].rotation);
			}
			yield return new WaitForSeconds (shootDuration);
		}
	}

	// 前進のみの移動
	private void Move(){
		GetComponent<Rigidbody> ().velocity = Vector3.back.normalized * speed;
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
		enemyHealth -= damege;
		if (enemyHealth <= 0) {
			Dead ();
		} else if (enemyHealth <= dyingLine) {
			isDying = true;
		}
	}

	// enemyを消します
	private void Dead(){

		Destroy (gameObject);
	}
}
