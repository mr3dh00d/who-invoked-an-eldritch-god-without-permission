using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[System.Serializable] public class StatsPanel
{
    public GameObject Slider;
    public Image Image;
    public TextMeshProUGUI HealthLabel;
    public TextMeshProUGUI LevelLabel;

    public void setLevelUI(int level)
    {
        LevelLabel.text = $"Nv. {level}";
    }

    public void setHealthUI(float currentHealth, float maxHealth)
    {
        string color = ColorTypes.healthGreen;
        if (currentHealth == 0f)
        {
            color = ColorTypes.healthNone;
            Image.GetComponent<Image>().color = ColorUtility.TryParseHtmlString("#FFFFFFA0", out Color nc) ? nc : Color.black;
            Animator animator = Image.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }
        }
        else if (currentHealth < maxHealth * 0.2f)
        {
            color = ColorTypes.healthRed;
        }
        else if (currentHealth < maxHealth * 0.4f)
        {
            color = ColorTypes.healthYellow;
        }
        Slider.GetComponent<Slider>().value = currentHealth / maxHealth;
        Slider.transform.GetChild(1).GetComponentInChildren<Image>().color = ColorUtility.TryParseHtmlString(color, out Color newColor) ? newColor : Color.black;
        HealthLabel.text = $"{currentHealth} / {maxHealth}";
    }

}
