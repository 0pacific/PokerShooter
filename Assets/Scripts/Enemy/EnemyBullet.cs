using UnityEngine;
using System.Collections;

// 敵の弾の挙動
public class EnemyBullet : MonoBehaviour {
	[SerializeField]
	private int power = 5;
	[SerializeField]
	private int speed = 10;
	private float lifeTime = 5;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward.normalized * speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			other.GetComponent<PlayerController> ().Damage (power);
			Destroy (this.gameObject);
		}
	}
}
