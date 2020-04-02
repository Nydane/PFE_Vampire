using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxBlood (int blood)
    {
        slider.maxValue = blood;
        slider.value = blood;
    }
    public void SetBlood (int blood)
    {
        slider.value = blood;
    }
}
