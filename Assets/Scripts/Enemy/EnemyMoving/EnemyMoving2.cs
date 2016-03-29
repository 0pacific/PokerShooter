using UnityEngine;
using System.Collections;

public class EnemyMoving2 : MonoBehaviour {
	[SerializeField]
	private float period = 4;
	[SerializeField]
	private float width = 3;

	private Vector3 velocity;
	private bool isInitialized = false;
	private float timer = 0;
	private float turningPoint;


	// Use this for initialization
	void Start () {
		turningPoint = period / 4;
		velocity = transform.right * (4 * width / period);
	}

	// Update is called once per frame
	void Update () {
		// 横方向
		// period/2ごとに速度を反転(最初だけperiod/4)
		if (timer + Time.deltaTime <= turningPoint) {				
			transform.position += velocity * Time.deltaTime;
			timer += Time.deltaTime;
		} else {					// period/2以降は進行方向が反転
			transform.position += velocity * (turningPoint - timer);	// turningPointより超過分は無視
			velocity = -velocity;	// 速度を反転
			timer = 0;
			if (!isInitialized) {	// 最初は中央から始まる為、period/4で速度反転.以降はperiod/2ごと
				turningPoint = period / 2;
				isInitialized = true;
			}
		}
	}
}
