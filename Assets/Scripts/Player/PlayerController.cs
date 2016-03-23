using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
  // 自機の動作を管理するクラス

  Vector3 moveVec = Vector3.zero; //自機の移動ベクトル

  public float speed; // 移動速度
  public float shootDuration; // 弾丸の発射間隔
  public GameObject bulletPrefab; // 弾丸のプレファブ
  public GameObject ballPrefab; // ボールのプレファブ
  public GameController controller; // ゲームコントローラー
  public PlayerManager manager;
  public GameObject balls; // ボールの管理オブジェクト
  public GameObject bullets; // 弾の管理オブジェクト

	// Use this for initialization
	void Start () {

    // コルーチンで通常弾を常に発射させる
    StartCoroutine(Shoot());
	
	}
	
	// Update is called once per frame
	void Update () {

    // 移動関連
    moveVec = Vector3.zero;

    if(Input.GetKey("right"))
      MoveRight();
    else if(Input.GetKey("left"))
      MoveLeft();

    // ボール発射
    if(Input.GetKeyDown("space"))
      ShootBall();

    // 移動実行
    transform.position = transform.position + moveVec;
  
	}

  void MoveRight ()
  {
    if(transform.position.x > GameController.windowWidth)
      return;
    
    moveVec.x = speed * Time.deltaTime;
  }

  void MoveLeft ()
  {
    if(transform.position.x < -1 * GameController.windowWidth)
      return;
    
    moveVec.x = -1 * speed * Time.deltaTime;
  }

  void ShootBall ()
  {
    // ボールが残っていれば消費して発射
    if(manager.ConsumeBall()) {
      GameObject ball = (GameObject)Instantiate(
        ballPrefab,
        transform.position,
        Quaternion.identity
      );
      ball.transform.SetParent(balls.transform,false);
    }
  }

  IEnumerator Shoot ()
  {
    while(true)
    {
      // shootDuration毎に弾丸を発射
      yield return new WaitForSeconds(shootDuration);
      GameObject bullet = (GameObject)Instantiate(
        bulletPrefab,
        transform.position,
        Quaternion.identity
      );
      bullet.transform.SetParent(bullets.transform,false);
    }
  }
}
