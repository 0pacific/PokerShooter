using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
  // ボールの移動、爆発など

  public float speed; //ボールの速度
  public float blastDistance; //ボールが爆発する距離(Z軸方向)
  public GameObject effectPrefab;

  // Use this for initialization
  void Start () {
  }

  // Update is called once per frame
  void Update () {

    //ボールの移動
    Vector3 moveVec = new Vector3(0.0f, 0.0f, speed * Time.deltaTime);

    transform.position += moveVec;

    //ボールの爆発
    if(transform.position.z > blastDistance)
      Blast();
  }

  void Blast ()
  {


    // 爆発用のエフェクトを起動
    Instantiate(
      effectPrefab,
      transform.position,
      Quaternion.identity
    );
    Destroy(this.gameObject);
  }
}
