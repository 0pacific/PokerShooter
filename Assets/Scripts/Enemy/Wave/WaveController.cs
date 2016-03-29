using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
  // waveの管理クラス

  public GameObject[] waves;

  GameObject currentWave;
  GameObject nextWave;
  float left = 0.0f;
  bool isCleared;

	// Use this for initialization
	void Start () {

    currentWave = (GameObject)Instantiate(
      waves[Random.Range(0, waves.Length)],
      transform.position,
      Quaternion.identity
    );
    left = currentWave.GetComponent<Wave>().duration;
    currentWave.transform.SetParent(transform);

    nextWave = (GameObject)Instantiate(
      waves[Random.Range(0, waves.Length)],
      transform.position,
      Quaternion.identity
    );
    nextWave.SetActive(false);
    nextWave.transform.SetParent(transform);
	
	}
	
  void Update () {

    left -= Time.deltaTime;
    Debug.Log(left);
    if(left <= 0.0f)
    {
      Destroy(currentWave.gameObject);
      currentWave = nextWave;
      currentWave.SetActive(true);
      left = currentWave.GetComponent<Wave>().duration;

      nextWave = (GameObject)Instantiate(
        waves[Random.Range(0, waves.Length)],
        transform.position,
        Quaternion.identity
      );
      nextWave.SetActive(false);
    }
  }
}
