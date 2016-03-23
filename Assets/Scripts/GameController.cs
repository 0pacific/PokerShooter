using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

  // グローバル変数 //

  public static int bulletPower = 1; // プレイヤーの弾丸一発の威力
  public static int ballPower = 10; // ボールの威力
  public static float windowWidth = 4; // 画面幅（左右均等)
  public static int maxPlayerHP = 100;
  public static int maxEnergy = 100;

  public static Dictionary<int,int> enemyHP = new Dictionary<int,int>(); // 敵の数字とHPの対応
  public enum rNum { // 敵出現ポイントにおける数字の制限
    normal
  };
  public enum rMoving { // 敵出現ポイントにおける動きの制限
    normal
  };
  public enum rAttack { // 敵出現ポイントにおける攻撃方法の制限
    normal
  };


  //////////////////

  void Awake () {
    enemyHP.Add(1, 130);
    enemyHP.Add(2, 30);
    enemyHP.Add(3, 40);
    enemyHP.Add(4, 50);
    enemyHP.Add(5, 60);
    enemyHP.Add(6, 70);
    enemyHP.Add(7, 80);
    enemyHP.Add(8, 90);
    enemyHP.Add(9, 100);
    enemyHP.Add(10, 230);
    enemyHP.Add(11, 330);
    enemyHP.Add(12, 430);
    enemyHP.Add(13, 530);
	}
	
	// Update is called once per frame
	void Update () {

	
	}

  public void GameOver ()
  {
    
  }
}
