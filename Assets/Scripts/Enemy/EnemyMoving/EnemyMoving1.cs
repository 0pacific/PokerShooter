using UnityEngine;
using System.Collections;

public class EnemyMoving1 : MonoBehaviour {
	private int speed = 1;

	void Update () {
		Move ();
	}

	private void Move(){
		transform.position += Vector3.right * speed * Time.deltaTime;
	}

	public void SetSpeed(int newSpeed){
		speed = newSpeed;
	}
}
