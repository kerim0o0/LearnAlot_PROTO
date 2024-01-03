using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class statsController : MonoBehaviour
{
    public Slider xpSlider;
    public TextMeshProUGUI levelIndicator;
    public int xpPerLevel = 1000;

    private float xp;
    private int level;

    private void Start()
    {
        xp = 0;
        level = 1;
        UpdateUI();
    }

    public void IncreaseXP()
    {

            xp+= 0.75f;
            if (xp >= xpPerLevel)
            {
                xp -= xpPerLevel;
                level++;
            }
            UpdateUI();
    }

    public void DecreaseXP()
    {
        xp -= 0.75f;
        if (xp < 0)
            {
            xp += xpPerLevel;
            level--;
            }
            UpdateUI();
    }

    private void UpdateUI()
    {
        xpSlider.value = (float)xp / xpPerLevel;
        levelIndicator.text = level.ToString();
    }
}
