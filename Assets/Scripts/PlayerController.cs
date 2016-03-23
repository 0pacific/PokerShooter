using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
  // 自機の動きなど

  Vector3 moveVec = Vector3.zero; //自機の移動ベクトル
  int hp;

  public float speed; // 移動速度
  public float shootDuration; // 弾丸の発射間隔
  public GameObject bulletPrefab; // 弾丸のプレファブ
  public GameObject ballPrefab; // ボールのプレファブ
  public GameController controller; // ゲームコントローラー

	// Use this for initialization
	void Start () {

    hp = GameController.maxPlayerHP;

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
    if(controller.ConsumeBall()) {
      Instantiate(
        ballPrefab,
        transform.position,
        Quaternion.identity
      );
    }
  }

  IEnumerator Shoot ()
  {
    while(true)
    {
      // shootDuration毎に弾丸を発射
      yield return new WaitForSeconds(shootDuration);
      Instantiate(
        bulletPrefab,
        transform.position,
        Quaternion.identity
      );
    }
  }

  public void Damage (int damage)
  {
    // ダメージ処理
    hp -= damage;
    if(hp < 0)
      controller.GameOver();
    else if(hp > GameController.maxPlayerHP)
      hp = GameController.maxPlayerHP;
  }
}
