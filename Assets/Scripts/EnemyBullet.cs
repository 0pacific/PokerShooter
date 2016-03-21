using UnityEngine;
using System.Collections;

// 敵の弾の挙動
public class EnemyBullet : MonoBehaviour {
	private int bulletSpeed = 10;
	private float lifeTime = 5;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward.normalized * bulletSpeed * Time.deltaTime;
	}
}
