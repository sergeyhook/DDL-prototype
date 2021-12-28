using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image image;
    // Start is called before the first frame update

    public void SetHealth(float Health)
    {
        slider.value = Health;
        image.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetMaxHealth(float Health)
    {
        gradient.Evaluate(1f);
        image.color = gradient.Evaluate(1f);
        slider.maxValue = Health;
        slider.value = Health;
    }
}
