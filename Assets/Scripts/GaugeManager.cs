using UnityEngine;
using System.Collections;

public class GaugeManager : MonoBehaviour {

  public GameObject gauge;

  RectTransform gaugeRect;
  float maxGaugeSize;
  float gaugeHeight;
  float ratio = 1.0f;

  void Start ()
  {
    // ゲージの最大サイズ取得
    gaugeRect = gauge.GetComponent<RectTransform>();
    maxGaugeSize = gaugeRect.sizeDelta.x;
    gaugeHeight = gaugeRect.sizeDelta.y;
  }


  public void UpdateGauge (int max,int value)
  {
    if(value < 0)
    {
      Debug.Log("Warning[GaugeManager] : Value is lower than 0.");
      return; 
    }

    if(value / max >= 1)
      ratio = 1.0f;
    else
      ratio = (float)value / (float)max;

    gaugeRect.sizeDelta = new Vector2(maxGaugeSize * ratio,gaugeHeight);
  }
}
