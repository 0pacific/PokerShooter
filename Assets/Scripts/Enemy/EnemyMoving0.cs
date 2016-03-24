using UnityEngine;
using System.Collections;

public class EnemyMoving0 : MonoBehaviour {
	private int speed = 3;	// 移動の速さ

	void Update () {
		Move ();
	}

	private void Move(){
		transform.position += Vector3.back * speed * Time.deltaTime;
	}

	public void SetSpeed(int newSpeed){
		speed = newSpeed;
	}
}
