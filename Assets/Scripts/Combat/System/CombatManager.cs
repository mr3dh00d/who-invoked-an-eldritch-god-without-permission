using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public List<Fighter> heroes;
    public List<Fighter> villains;
    public List<Item> items;
    public ActionsManager actionsManager;
    public MessagesManager messagesManager;
    private string[] actionsLabel = new string[] { 
        ActionTypes.attack, 
        ActionTypes.defense, 
        ActionTypes.item,
    };
    private List<Fighter> heroesOptions;
    private List<Fighter> villainsOptions;
    private bool fighterSelected;
    private Fighter selectedFighter;
    private bool actionSelected;
    private string selectedAction;

    private bool itemSelected;
    private Item selectedItem;
    private bool finishAction;
    private bool doingActions;

    private Queue<Action> actions;



    // private Dictionary<Fighter, Action> Actions;

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
            if (Input.GetKeyDown("z"))
            {
                if(messagesManager.messagesPanel.activeSelf)
                {
                    if(!doingActions){
                        messagesManager.messagesPanel.SetActive(false);
                        actionsManager.actionsPanel.SetActive(true);
                        fighterSelected = false;
                        actionSelected = false;
                        itemSelected = false;
                        finishAction = false;
                        doingActions = false;
                        actions = new Queue<Action>();
                        // Actions = new Dictionary<Fighter, Action>();
                        heroesOptions = new List<Fighter>();
                        foreach (Fighter hero in heroes)
                        {
                            if(hero.stats.getHealth() > 0){
                                heroesOptions.Add(hero);
                            }
                        }
                        villainsOptions = new List<Fighter>();
                        foreach (Fighter villain in villains)
                        {
                            if(villain.stats.getHealth() > 0){
                                villainsOptions.Add(villain);
                            }
                        }
                        actionsManager.setActions(heroesOptions.ConvertAll(hero => $"Pollito {hero.name}").ToArray());
                        GetComponent<ActionEvent>().enabled = true;
                    }
                    else if(doingActions){
                        if(actions.Count > 0)
                        {
                            doActions();
                        }
                        else
                        {
                            doingActions = false;
                            messagesManager.sayBattleIntro();
                        }
                    }
                }
                else if (actionsManager.actionsPanel.activeSelf)
                {
                    if(!fighterSelected)
                    {
                        selectedFighter = heroesOptions[actionsManager.actionSelected];
                        actionsManager.setActions(actionsLabel);
                        fighterSelected = true;
                    }
                    else if (fighterSelected)
                    {
                        if(!actionSelected){
                            selectedAction = actionsLabel[actionsManager.actionSelected];
                            actionSelected = true;
                            switch (selectedAction)
                            {
                                case ActionTypes.attack:
                                    actionsManager.setActions(villainsOptions.ConvertAll(villain => $"{villain.name}").ToArray());
                                    break;
                                case ActionTypes.defense:
                                    actions.Enqueue(new Action(selectedAction, selectedFighter));
                                    finishAction = true;
                                    break;
                                case ActionTypes.item:
                                    actionsManager.setActions(items.ConvertAll(item => $"{item.name}").ToArray());
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if(actionSelected)
                        {
                            switch (selectedAction)
                            {
                                case ActionTypes.attack:
                                    Fighter target = villainsOptions[actionsManager.actionSelected];
                                    actions.Enqueue(new Action(selectedAction, selectedFighter, target));
                                    finishAction = true;
                                    break;
                                case ActionTypes.item:
                                    if(!itemSelected)
                                    {
                                        selectedItem = items[actionsManager.actionSelected];
                                        itemSelected = true;
                                        if(selectedItem.effect < 0)
                                        {
                                            actionsManager.setActions(villainsOptions.ConvertAll(villain => $"{villain.name}").ToArray());
                                        }
                                        else
                                        {
                                            actions.Enqueue(new Action(selectedAction, selectedFighter, null, selectedItem));
                                            finishAction = true;
                                        }
                                    }
                                    else if (itemSelected)
                                    {
                                        Fighter targetItem = villainsOptions[actionsManager.actionSelected];
                                        actions.Enqueue(new Action(selectedAction, selectedFighter, targetItem, selectedItem));
                                        finishAction = true;
                                    }
                                    break;
                                default:
                                    break;
                            }

                        }
                    }

                    if(finishAction)
                    {
                        fighterSelected = false;
                        actionSelected = false;
                        itemSelected = false;
                        finishAction = false;
                        heroesOptions.Remove(selectedFighter);
                        actionsManager.setActions(heroesOptions.ConvertAll(hero => $"Pollito {hero.name}").ToArray());
                        if (heroesOptions.Count == 0)
                        {
                            actionsManager.actionsPanel.SetActive(false);
                            GetComponent<ActionEvent>().enabled = false;
                            doingActions = true;
                            doActions();
                        }
                    }
                }
                
            }
            else if (Input.GetKeyDown("x"))
            {
                if(actionsManager.actionsPanel.activeSelf)
                {
                    if(fighterSelected)
                    {
                        if(!actionSelected){
                            actionsManager.setActions(heroesOptions.ConvertAll(hero => $"Pollito {hero.name}").ToArray());
                            fighterSelected = false;
                        }
                        else if (actionSelected)
                        {
                            switch (selectedAction)
                            {
                                case ActionTypes.attack:
                                    actionsManager.setActions(actionsLabel);
                                    actionSelected = false;
                                    selectedAction = null;
                                    break;
                                case ActionTypes.item:
                                    if(itemSelected)
                                    {
                                        actionsManager.setActions(items.ConvertAll(item => $"{item.name}").ToArray());
                                        itemSelected = false;
                                    }
                                    else if(!itemSelected)
                                    {
                                        actionsManager.setActions(actionsLabel);
                                        actionSelected = false;
                                        selectedAction = null;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    public void doActions()
    {
        messagesManager.messagesPanel.SetActive(true);
        Action action = actions.Dequeue();
        switch (action.type)
        {
            case ActionTypes.attack:
                action.target.takeDamage(action.attacker.getAttack());
                messagesManager.displayMessage(
                    $"{action.attacker.name} atacó a {action.target.name} por {action.attacker.getAttack()} de daño"
                );
                break;
            default:
                break;
        }
    }

    
}
