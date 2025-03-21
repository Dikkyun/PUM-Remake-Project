using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Slider gaugeSlider;
    public Text nameText;
    public Text levelText;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        gaugeSlider.minValue = 0;
        gaugeSlider.maxValue = 25;
        gaugeSlider.value = unit.currentGauge;
    }

    public void SetGauge(int gauge)
    {
        gaugeSlider.value = gauge;
    }
}
