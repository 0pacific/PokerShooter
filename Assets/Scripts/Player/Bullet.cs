using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
  //弾丸の動きなど

  public float speed; //弾丸の速度
  public float vanishDistance; //弾丸が消滅する距離(Z軸方向)

  Transform target = null; // ホーミング対象

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    //弾丸の移動
    Vector3 targetVect;
    if(target == null)
    {
      targetVect = transform.position + new Vector3(0, 0, 10.0f);
    }
    else
    {
      targetVect = target.position;
    }
    transform.position = Vector3.MoveTowards(transform.position, targetVect, speed * Time.deltaTime);

    //弾丸の消去
    if(transform.position.z > vanishDistance)
      Destroy(this.gameObject);
	}

  public void SetTarget (Transform tar)
  {
    target = tar;
  }

  void OnTriggerEnter(Collider other)
  {
    if(other.tag == "Enemy")
    {
      other.GetComponent<Enemy>().Damage(GameController.bulletPower);
      Destroy(this.gameObject);
    }
  }
}
