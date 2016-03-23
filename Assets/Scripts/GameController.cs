using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

  const int MaximumEnergy = 100;

  public GaugeManager energyGauge;

  int energy = MaximumEnergy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    energyGauge.UpdateGauge(MaximumEnergy,energy);
	
	}

  public bool ConsumeBall ()
  {
    if(energy < 10)
      return false;

    energy -= 10;

    return true;
  }
}
