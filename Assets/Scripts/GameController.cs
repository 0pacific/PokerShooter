using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

  // グローバル変数 //

  public static int bulletPower = 1; // プレイヤーの弾丸一発の威力
  public static int ballPower = 10; // ボールの威力
  public static float windowWidth = 4; // 画面幅（左右均等)
  public static int maxPlayerHP = 100;
  public static int maxEnergy = 100; // ボール使用の最大値
  public static int energyConsumption = 10;

  public static Dictionary<int,int> enemyHP = new Dictionary<int,int>(){
    {1,130},
    {2,30},
    {3,40},
    {4,50},
    {5,60},
    {6,70},
    {7,80},
    {8,90},
    {9,100},
    {10,130},
    {11,230},
    {12,330},
    {13,430},
  }; // 敵の数字とHPの対応

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
	
	// Update is called once per frame
	void Update () {

	
	}

  public void GameOver ()
  {
    
  }
}
