using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient healthLevel;
    public Image fillIn;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fillIn.color = healthLevel.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fillIn.color = healthLevel.Evaluate(slider.normalizedValue);
    }
}