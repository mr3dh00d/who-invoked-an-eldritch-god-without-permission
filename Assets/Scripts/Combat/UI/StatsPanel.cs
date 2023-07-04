using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[System.Serializable] public class StatsPanel
{
    private GameObject Health;
    private Image Avatar;
    private TextMeshProUGUI Name;
    private TextMeshProUGUI HealthLabel;
    private TextMeshProUGUI LevelLabel;
    private bool showHealthNumber = true;
    private bool isUIset = false;

    public void setUI(GameObject Panel)
    {
        Health = Helpers.FindChildWithName(Panel, "Health").gameObject;
        Avatar = Helpers.FindChildWithName(Panel, "Avatar").GetComponent<Image>();
        Name = Helpers.FindChildWithName(Panel, "Name").GetComponent<TextMeshProUGUI>();
        HealthLabel = Helpers.FindChildWithName(Panel, "HealthLabel").GetComponent<TextMeshProUGUI>();
        LevelLabel = Helpers.FindChildWithName(Panel, "LevelLabel").GetComponent<TextMeshProUGUI>();
        isUIset = true;
    }

    public bool getIsUIset()
    {
        return isUIset;
    }

    public void setName(string name)
    {
        Name.text = name;
    }

    public void setLevelUI(int level)
    {
        LevelLabel.text = $"Nv. {level}";
    }

    public void setLevelUI(string level)
    {
        LevelLabel.text = $"Nv. {level}";
    }

    public void setHealthUI(float currentHealth, float maxHealth)
    {
        string color = ColorTypes.healthGreen;
        if (currentHealth == 0f)
        {
            color = ColorTypes.healthNone;
            Avatar.GetComponent<Image>().color = ColorUtility.TryParseHtmlString("#FFFFFFA0", out Color nc) ? nc : Color.black;
            Animator animator = Avatar.GetComponent<Animator>();
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
        Health.GetComponent<Slider>().value = currentHealth / maxHealth;
        Health.transform.GetChild(1).GetComponentInChildren<Image>().color = ColorUtility.TryParseHtmlString(color, out Color newColor) ? newColor : Color.black;
        if(showHealthNumber) HealthLabel.text = $"{currentHealth} / {maxHealth}";
    }

    public void disabledHealthNumber()
    {
        showHealthNumber = false;
    }

    public void enabledHealthNumber()
    {
        showHealthNumber = true;
    }

    public Image getAvatar()
    {
        return Avatar;
    }

}
