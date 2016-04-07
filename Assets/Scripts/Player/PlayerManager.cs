using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
  // 自機のHPなどの数値を管理するクラス

  public GaugeManager energyGauge;
  public GaugeManager HPGauge;
  public GameController controller;


  int energy = GameController.maxEnergy;
  int hp = GameController.maxPlayerHP;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {


    energyGauge.UpdateGauge(GameController.maxEnergy,energy);
	
	}

  public bool ConsumeBall ()
  {
    if(energy < 10)
      return false;

    energy -= 10;

    return true;
  }

  public void Damage (int damage)
  {
    // ダメージ処理
    hp -= damage;

    if(hp < 0)
    {
      controller.GameOver();
      hp = 0; 
    }
    else if(hp > GameController.maxPlayerHP)
      hp = GameController.maxPlayerHP;

    HPGauge.UpdateGauge(GameController.maxPlayerHP, hp);
  }
}
