using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEvent : MonoBehaviour
{
    public ActionsManager actionsManager;
    // Start is called before the first frame update
    void Start()
    {
        actionsManager = GetComponent<CombatManager>().actionsManager;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("up"))
        {
            if(actionsManager.actionSelected > 1)
            {
                actionsManager.selectAction(actionsManager.actionSelected - 2);
            }
        }
        if(Input.GetKeyDown("down"))
        {
            if(actionsManager.actionSelected < 6)
            {
                actionsManager.selectAction(actionsManager.actionSelected + 2);
            }
        }
        if(Input.GetKeyDown("left"))
        {
            if(actionsManager.actionSelected % 2 == 1)
            {
                actionsManager.selectAction(actionsManager.actionSelected - 1);
            }
        }
        if(Input.GetKeyDown("right"))
        {
            if(actionsManager.actionSelected % 2 == 0)
            {
                actionsManager.selectAction(actionsManager.actionSelected + 1);
            }
        }
        
    }
}
