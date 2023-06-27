using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[System.Serializable] public class MessagesManager
{
    private float typingSpeed = 0.03f;
    public GameObject messagesPanel;
    public MessageEvent messageEvent;

    private bool isTyping = false;


    public void sayBattleIntro()
    {
        displayMessage("Un grupo de enemigos ha aparecido!");
    }

    public void displayMessage(string message)
    {
        isTyping = true;
        Helpers.FindChildWithName(messagesPanel, "Image").SetActive(false);
        messageEvent.StartCoroutine(ShowMessage(message));

    }

    private IEnumerator ShowMessage(string message)
    {
        TextMeshProUGUI label = messagesPanel.GetComponentInChildren<TextMeshProUGUI>();
        label.text = string.Empty;

        foreach(char letter in message.ToCharArray())
        {
            label.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        isTyping = false;
        Helpers.FindChildWithName(messagesPanel, "Image").SetActive(true);
    }

    public bool getIsTyping()
    {
        return isTyping;
    }

    // public void setAttackMessage(string attacker, string attack, string defender, float damage)
    // {
    //     displayMessage($"{attacker} ha usado {attack} contra {defender} y ha hecho {damage} de daño!");
    // }

}
