using UnityEngine;
using System.Collections;

public class EnemyMoving1 : MonoBehaviour {
	private int speed = 5;	// 移動の速さ
	
	// Update is called once per frame
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
