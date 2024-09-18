using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CoinFlip : MonoBehaviour
{
    public GameObject coin;
    public GameObject side1;
    public GameObject side2;
    public void DoFlip()
    {
        //coin.transform.DORotate(new Vector3(0, 180, 0), 2);
        coin.transform.DOPunchRotation(new Vector3(0, 180, 0), 2).OnComplete(DoRotate);
    }
    public void DoRotate()
    {
        int r = Random.Range(0, 2);
        if (r == 1)
        {
            coin.transform.DORotate(new Vector3(0, 180, 0), 0.3f);
            side1.SetActive(false);
            side2.SetActive(true);
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 200);
            PlayerPrefs.Save();
            UIManager.instance.ShowMoney(PlayerPrefs.GetInt("Money").ToString());
        }
    }
}
