using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
  // 敵の出現ポイント

  public int restrictionNum;
  public int restrictionMoving;
  public int restrictionAttack;
  EnemyManager enemyManager;

	// Use this for initialization
	void Start () {

    enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();

    GameObject enemy;

    if(restrictionNum == (int)GameController.rNum.normal)
    {
      enemy = enemyManager.Spawn(
        transform.position,
        true,
        (int)GameController.rMoving.normal);
    }

    Destroy(this.gameObject);
	
	}

  void OnDrawGizmos ()
  {
    Vector3 offset = new Vector3(0, 0.5f, 0);

    Gizmos.color = new Color(1, 0, 0, 0.5f);
    Gizmos.DrawSphere(transform.position + offset, 0.5f);


  }
}
