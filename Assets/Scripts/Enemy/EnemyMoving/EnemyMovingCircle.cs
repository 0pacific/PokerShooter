using UnityEngine;
using System.Collections;

// 敵の円運動(EnemyMoving*との併用不可)
public class EnemyMovingCircle : MonoBehaviour {
	[SerializeField]
	private float period = 3;		// 円運動の周期
	[SerializeField]
	private float radius = 3;	// 円運動の半径

	private Vector3 center;			// 円運動の中心

	private float timer = 0;		// 制御用のタイマー

	void Start () {
		center = transform.position - Vector3.forward * radius;
	}
	

	void Update () {
		float radian = 2 * Mathf.PI * timer / period;		// 時間から角度計算
		float x = center.x + radius * Mathf.Sin (radian);	// 新しいx軸座標
		float z = center.z + radius * Mathf.Cos (radian);	// 新しいz軸座標
		transform.position = new Vector3 (x, 0, z);

		timer += Time.deltaTime;
		if (timer > period) {
			timer = 0;
		}
	}
}
