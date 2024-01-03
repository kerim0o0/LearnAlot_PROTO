using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class itemTrigger : MonoBehaviour
{
    private TextMeshProUGUI rewardCount;
    private int count;

    void Start()
    {
        rewardCount = GameObject.Find("StatsCanvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void increaseCount() {
        count = System.Convert.ToInt32(rewardCount.text + "");
        count++;
        rewardCount.text = count + "";
    }

   
}
