using UnityEngine;
using System.Collections;

public class EnemyMovingCircle : MonoBehaviour {
	[SerializeField]
	private float period = 3;
	[SerializeField]
	private float radius = 1.5f;

	private Vector3 center;

	private float timer = 0;

	// Use this for initialization
	void Start () {
		center = transform.position - Vector3.forward * radius;
	}
	
	// Update is called once per frame
	void Update () {
		float t = 2 * Mathf.PI * timer / period;
		float x = radius * Mathf.Sin (t);
		float z = radius * Mathf.Cos (t);
		transform.position = new Vector3 (center.x + x, 0, center.z + z);
	}
}
