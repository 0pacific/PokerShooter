using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	[SerializeField]
	private GameObject enemyPref;		// Enemyプレハブ
	[SerializeField]
	private GameObject[] bulletPrefs;	// enemyBulletプレハブ（spade, heart, dia, club)
	[SerializeField]
	private Transform[] shootTrans;		// shootPointのプレハブ群
	[SerializeField]
	private Sprite[] cardSprites;		// カードの画像

	private int[,] cardProbArr = new int[4, 14]{
		{130, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10},	// spadeの確率(0:合計,1~13:各カードの確率)
		{130, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10},	// heartの確率(0:合計,1~13:各カードの確率)
		{130, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10},	// diamondの確率(0:合計,1~13:各カードの確率)
		{130, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10}	// clubの確率(0:合計,1~13:各カードの確率)
	};
	private int probSum = 520;										// カード(Joker以外)の確率合計

	// テスト用のStart()
	void Start () {
		ResetCardProb ();
		Spawn (new Vector3(0, 0, 10), new int[]{0}, 0);
		StartCoroutine ("Closs");
	}
	// Spawn使用テスト用
	IEnumerator Closs(){
		yield return new WaitForSeconds (3.0f);
		Spawn (new Vector3(-3, 0, 10), new int[]{ 1, 2 }, 2);
		GameObject enemy = Spawn (new Vector3(3, 0, 10), new int[]{ 1, 2 }, 2);
		enemy.GetComponent<EnemyMoving1> ().SetSpeed (-1);
	}

	// Enemyを生成
	public GameObject Spawn(Vector3 spawnP, int[] shoots, int moving){
		int type = 0;
		int no = 1;
		int card = Random.Range (0, probSum + 1);
		if (card == 0) {
			// joker
		} else {
			int accumulation = 0;
			for (int i = 0; i < 4; i++) {
				if (card <= accumulation + cardProbArr [i, 0]) {
					// 各スートの累積からスートを求める
					type = i;
					for (int j = 1; j < 14; j++) {
						// 累積を計算していき、ナンバーを求める
						accumulation += cardProbArr [i, j];
						if (card <= accumulation) {
							no = j;
							break;
						}
					}
					break;
				}
				accumulation += cardProbArr [i, 0];
			}
		}
		SetCardProb (type, no, 0);
		GameObject enemy = (GameObject)GameObject.Instantiate (enemyPref, spawnP, Quaternion.identity);
		Enemy eneCom = enemy.GetComponent<Enemy> ();
		eneCom.frontRenderer.sprite = cardSprites [type];
		eneCom.Initialize (type, no);
		Transform[] shootPoint = new Transform[shoots.Length];
		for (int i = 0; i < shoots.Length; i++) {
			Transform p = (Transform)GameObject.Instantiate (shootTrans [shoots [i]]);
			p.position += enemy.transform.position;
			p.parent = enemy.transform;
			shootPoint [i] = p;
		}
		eneCom.SetShoot (bulletPrefs [type], shootPoint, 1f);
		eneCom.SetMoving (moving);

		return enemy;
	}

//////////////////////////
//	カードの確率制御関連	//
//////////////////////////

	// カードの確率変更(出現時に同じものが出ないようにする)
	private void SetCardProb(int suit, int num, int prob){
		int diff = prob - cardProbArr [suit, num];
		probSum += diff;
		cardProbArr [suit, 0] += diff;
		cardProbArr [suit, num] = prob;
	}
	// 引数のカード周囲に重み付け
	public void WeightCardProb(int suit, int num){
		// 同じ番号周りに重み付け
		for (int i = 0; i < 4; i++) {
			Weight (i, num - 1, 5);
			Weight (i, num, 10);
			Weight (i, num + 1, 5);	
		}
		// 同じスートに重み付け
		for (int i = 1; i < 14; i++) {
			Weight (suit, i, 5);
		}
	}
	// 引数のカードに重み付け
	private void Weight(int suit, int num, int w){
		if (num > 0 && num < 14) {
			if (cardProbArr [suit, num] != 0) {	// 0なら出現中か手札にあるので重み付けしない
				probSum += w;					// カード全部の合計に加算
				cardProbArr [suit, 0] += w;		// 引数カードのスートの合計に加算
				cardProbArr [suit, num] += w;	// 引数カードそのものに加算
			}
		} else if (num <= 0) {					// KとAで繋ぐ
			Weight (suit, 13 + num, w);
		} else if (num >= 14) {
			Weight (suit, num - 13, w);
		}
	}

	// 引数のカード1枚を初期化(敵撃破や手札解放時など)
	public void ResetCardProb(int suit, int num){
		int diff = 10 - cardProbArr [suit, num];
		probSum += diff;
		cardProbArr [suit, 0] += diff;
		cardProbArr [suit, num] = 10;
	}
	// 引数なしなら0以外のもの全てを初期化(手札完成時に出現中のもの以外)
	public void ResetCardProb(){
		probSum = 520;
		for (int i = 0; i < 4; i++) {
			cardProbArr [i, 0] = 130;
			for (int j = 1; j <= 13; j++) {
				if (cardProbArr [i, j] == 0) {
					cardProbArr [i, 0] -= 10;
					probSum -= 10;
				} else {
					cardProbArr [i, j] = 10;
				}
			}
		}
	}
}
