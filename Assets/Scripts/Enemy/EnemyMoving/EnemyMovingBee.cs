using UnityEngine;
using System.Collections;

// 敵の８の字移動(EnemyMoving*との併用不可)
public class EnemyMovingBee : MonoBehaviour {
	[SerializeField]
	private float period = 4;	// ８の字運動の周期
	[SerializeField]
	private float width = 3;	// ８の字運動の幅(X軸)
	[SerializeField]
	private float depth = 2;	// ８の字運動の奥行き(Z軸)

	private Vector3 center;		// ８の字の中心

	private float timer = 0;	// 制御用のタイマー

	void Start () {
		center = transform.position;
	}

	void Update () {
		float radian = 2 * Mathf.PI * timer / period;			// 時間から角度計算
		float x = center.x + width * Mathf.Sin (radian);		// 新しいx軸座標
		float z = center.z + depth * Mathf.Sin (2 * radian);	// 新しいz軸座標
		transform.position = new Vector3 (x, 0, z);

		timer += Time.deltaTime;
		if (timer > period) {
			timer = 0;
		}
	}
}
