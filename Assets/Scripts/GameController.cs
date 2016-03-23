using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

  // グローバル変数 //

  public static int bulletPower = 1; // プレイヤーの弾丸一発の威力
  public static int ballPower = 10; // ボールの威力
  public static float windowWidth = 4; // 画面幅（左右均等)
  public static int maxPlayerHP = 100;
  public static int maxEnergy = 100;

  //////////////////

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

  public void GameOver ()
  {
    
  }
}
