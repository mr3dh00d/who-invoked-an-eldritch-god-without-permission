using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public Fighter[] heroes;
    public Fighter[] villains;
    public ActionsManager actionsManager;
    public MessagesManager messagesManager;

    // Start is called before the first frame update
    void Start()
    {
        actionsManager.iniciar();
        messagesManager.sayBattleIntro();
    }

    // Update is called once per frame
    void Update()
    {
        if(!messagesManager.getIsTyping())
        {
            if(Input.GetKeyDown("z") && !actionsManager.actionsPanel.activeSelf)
            {
                messagesManager.messagesPanel.SetActive(false);
                actionsManager.actionsPanel.SetActive(true);
                // string[] actions = new string[4]{"Attack", "Magic", "Item", "Run"};
                actionsManager.setActions(new string[]{
                    "Pollito Hojita",
                    "Pollito Miope",
                    "Pollito Lloron",
                    "Pollito Payaso"
                });
                GetComponent<ActionEvent>().enabled = true;
            }
        }
    }

    
}
