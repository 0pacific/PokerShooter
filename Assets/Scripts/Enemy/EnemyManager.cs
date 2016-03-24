using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	[SerializeField]
	private GameObject enemyPref;		// Enemyプレハブ
	[SerializeField]
	private GameObject[] bulletPrefs;	// enemyBulletプレハブ（spade, heart, dia, club)
	[SerializeField]
	private Transform[] shootTrans;		// shootPointのプレハブ群

	public Transform[] spawnPoint;

	// テスト用のStart()
	void Start () {
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

	// Update is called once per frame
	void Update () {
	
	}

	public GameObject Spawn(Vector3 spawnP, int[] shoots, int moving){
		int type = Random.Range (0, 4);
		GameObject enemy = (GameObject)GameObject.Instantiate (enemyPref, spawnP, Quaternion.identity);
		Enemy eneCom = enemy.GetComponent<Enemy> ();
		eneCom.Initialize (type, 0);
		Transform[] shootPoint = new Transform[shoots.Length];
		for (int i = 0; i < shoots.Length; i++) {
			Transform p = (Transform)GameObject.Instantiate (shootTrans [shoots [i]]);
			p.position += enemy.transform.position;
			p.parent = enemy.transform;
			shootPoint [i] = p;
		}
		eneCom.SetShoot (bulletPrefs [type], shootPoint, 0.5f);
		eneCom.SetMoving (moving);

		return enemy;
	}
}
