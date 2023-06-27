using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[System.Serializable] public class ActionsManager
{
    public GameObject actionsPanel;
    public int actionSelected = 0;
    private string[] actions;
    private TextMeshProUGUI[] actionsTexts;
    private GameObject selector;

    private List<Vector2> selectorPositions;
    public void iniciar() 
    {
        actionsTexts = actionsPanel.GetComponentsInChildren<TextMeshProUGUI>();
        selector = Helpers.FindChildWithName(actionsPanel, "Selector");
        selectorPositions = new List<Vector2>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int x = 56 + (j * 370);
                int y = -66 + (i * -50);
                selectorPositions.Add(new Vector2(x, y));
            }
        }
       

    }

    public void setActions(string[] actions)
    {
        this.actions = actions;
        for (int i = 0; i < actionsTexts.Length; i++)
        {
            actionsTexts[i].text = i < actions.Length ? actions[i] : "";
        }
        selectAction(0);
    }

    public void selectAction(int action)
    {
        if(action < 0 || action >= actions.Length)
        {
            return;
        }
        actionSelected = action;
        selector.GetComponent<RectTransform>().anchoredPosition = selectorPositions[action];
        // selector.transform.position = actionsTexts[action].transform.position - new Vector2(-5, 1);
    }

}