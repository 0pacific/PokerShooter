using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	[SerializeField]
	private GameObject enemyPref;		// Enemyプレハブ
	[SerializeField]
	private GameObject[] bulletPrefs;	// enemyBulletプレハブ（spade, heart, dia, club)
	[SerializeField]
	private Transform[] shootTrans;		// 

	private Transform[] shootPoint;		// 

	public Vector3 spawnPoint = new Vector3 (0, 0, 15);

	// Use this for initialization
	void Start () {
		StartCoroutine ("Spawn");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Spawn(){
		while (true) {
			int type = Random.Range (0, 3);
			GameObject enemy = (GameObject)GameObject.Instantiate (enemyPref, spawnPoint, Quaternion.identity);
			enemy.GetComponent<Enemy> ().Initialize (type, 0, 50, enemy.transform.position);
			shootPoint = new Transform[3];
			for (int i = 0; i < 3; i++) {
				Transform p = (Transform)GameObject.Instantiate (shootTrans [i]);
				p.position += enemy.transform.position;
				p.parent = enemy.transform;
				shootPoint [i] = p;
			}
			enemy.GetComponent<Enemy> ().SetShoot (bulletPrefs [type], shootPoint, 0.5f);
			yield return new WaitForSeconds (2.0f);
		}
	}
}
