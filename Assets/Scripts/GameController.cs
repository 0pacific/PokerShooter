using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

  const int MaxBallSize = 3;

  public Counter ballCounter;

  int ballCount = MaxBallSize;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    ballCounter.UpdateIcons(ballCount);
	
	}

  public bool ConsumeBall ()
  {
    if(ballCount <= 0)
      return false;

    ballCount--;

    return true;
  }
}
