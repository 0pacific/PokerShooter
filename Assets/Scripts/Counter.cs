using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour {

  public GameObject[] icons;

  public void UpdateIcons (int count)
  {
    for(int i = 0;i < icons.Length;i++)
    {
      if(i < count)
        icons[i].SetActive(true);
      else
        icons[i].SetActive(false);
    }
  }
}
