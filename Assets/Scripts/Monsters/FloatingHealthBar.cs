using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public void SetHealth(float health, string name)
    {
        
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
        else
        {
            Debug.LogError("Health Slider component not set on " + gameObject.name);
        }
    }

    public void SetMaxHealth(float maxHealth)
    {
        if (maxHealth <= 0)
        {
            return;
        }
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
        }
        else
        {
            Debug.LogError("Health Slider component not set on " + gameObject.name);
        }
    }
}
