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
    private bool attackSelected;
    private Attack selectedAttack;
    private List<Attack> attackOptions;

    private bool itemSelected;
    private Item selectedItem;
    private bool finishAction;
    private bool doingActions;
    private bool playersTurn;
    private bool fighting;
    private Queue<Action> actions;
    private Queue<string> events;



    // private Dictionary<Fighter, Action> Actions;

    // Start is called before the first frame update
    void Start()
    {
        actionsManager.iniciar();
        messagesManager.sayBattleIntro();
        actions = new Queue<Action>();
        events = new Queue<string>();
        fighting = true;
        playersTurn = true;
        // foreach (Fighter hero in heroes)
        // {
        //     foreach (Attack attack in hero.attacks)
        //     {
        //         attack.uses = attack.maxUses;
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (fighting){
            if(!messagesManager.getIsTyping())
            {
                if (Input.GetKeyDown("z"))
                {
                    if(messagesManager.messagesPanel.activeSelf)
                    {
                        if(playersTurn){
                            if(!doingActions){
                                messagesManager.messagesPanel.SetActive(false);
                                actionsManager.actionsPanel.SetActive(true);
                                playersTurn = true;
                                fighterSelected = false;
                                actionSelected = false;
                                itemSelected = false;
                                finishAction = false;
                                doingActions = false;
                                actions = new Queue<Action>();
                                events = new Queue<string>();
                                // Actions = new Dictionary<Fighter, Action>();
                                heroesOptions = new List<Fighter>();
                                foreach (Fighter hero in heroes)
                                {
                                    if(hero.stats.getHealth() > 0){
                                        heroesOptions.Add(hero);
                                    }
                                    if(hero.getIsDefending())
                                    {
                                        hero.setIsDefending(false);
                                    }
                                }
                                villainsOptions = villains.FindAll(villain => villain.stats.getHealth() > 0);
                                actionsManager.setActions(heroesOptions.ConvertAll(hero => $"Pollito {hero.name}").ToArray());
                                GetComponent<ActionEvent>().enabled = true;
                            }
                            else if(doingActions)
                            {
                                if(events.Count > 0)
                                {
                                    doEvents();
                                }
                                else if(actions.Count > 0)
                                {
                                    doActions();
                                }
                            }
                        }
                        else 
                        {
                            if(!doingActions)
                            {
                                EnemyTurn();
                                doingActions = true;
                                events.Enqueue("Los enemigos se preparan para atacar.");
                                doEvents();
                            }
                            else
                            {
                                int heroesAlive = heroes.FindAll(hero => hero.stats.getHealth() > 0).Count;
                                int villainsAlive = villains.FindAll(villain => villain.stats.getHealth() > 0).Count;
                                if(heroesAlive == 0 || villainsAlive == 0)
                                {
                                    messagesManager.displayMessage(
                                        heroesAlive == 0 ? "Has sido derrotado." : "Has derrotado a los enemigos."
                                    );
                                    fighting = false;
                                    return;
                                }
                                if(events.Count > 0)
                                {
                                    doEvents();
                                }
                                else if(actions.Count > 0)
                                {
                                    doActions();
                                }
                                else
                                {
                                    playersTurn = true;
                                }
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
                            attackOptions = selectedFighter.attacks.FindAll(attack => attack.uses > 0);
                            if(!actionSelected){
                                selectedAction = actionsLabel[actionsManager.actionSelected];
                                actionSelected = true;
                                switch (selectedAction)
                                {
                                    case ActionTypes.attack:
                                        actionsManager.setActions(attackOptions.ConvertAll(attack => $"{attack.name} ({attack.damage} de daño)").ToArray());
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
                                        if(!attackSelected)
                                        {
                                            attackSelected = true;
                                            selectedAttack = selectedFighter.attacks[actionsManager.actionSelected];
                                            actionsManager.setActions(villainsOptions.ConvertAll(villain => $"{villain.name}").ToArray());
                                        }
                                        else if (attackSelected)
                                        {
                                            Fighter target = villainsOptions[actionsManager.actionSelected];
                                            actions.Enqueue(new Action(selectedAction, selectedFighter, selectedAttack, target));
                                            finishAction = true;
                                        }
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
                                                actions.Enqueue(new Action(selectedAction, selectedFighter, null, selectedFighter, selectedItem));
                                                finishAction = true;
                                            }
                                        }
                                        else if (itemSelected)
                                        {
                                            Fighter targetItem = villainsOptions[actionsManager.actionSelected];
                                            actions.Enqueue(new Action(selectedAction, selectedFighter, null, targetItem, selectedItem));
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
                            attackSelected = false;
                            heroesOptions.Remove(selectedFighter);
                            items.Remove(selectedItem);
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
                                        if(attackSelected)
                                        {
                                            actionsManager.setActions(attackOptions.ConvertAll(attack => $"{attack.name} ({attack.damage} de daño)").ToArray());
                                            attackSelected = false;
                                        }
                                        else if(!attackSelected)
                                        {
                                            actionsManager.setActions(actionsLabel);
                                            actionSelected = false;
                                            selectedAction = null;
                                        }
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
            if(messagesManager.getIsTyping())
            {
                if(Input.GetKeyDown("x")){
                    messagesManager.skipMessage();
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
                // Caso especial de pollito Payaso
                float damageTaken = 0;
                if(action.attacker.name == PollitoTypes.payaso && action.attack.level == 2)
                {
                    damageTaken = action.target.takeDamage(0);
                }
                else if(action.attacker.name == PollitoTypes.payaso && action.attack.level == 3)
                {
                    damageTaken = action.target.takeDamage(Random.Range(40, 60));
                }
                else
                {
                    damageTaken = action.target.takeDamage(action.attack.damage);
                }
                Attack attack = action.attack.Clone();
                attack.damage = damageTaken;
                messagesManager.displayMessage(
                    AttackTypes.attackMessage(action.attacker, attack, action.target)
                );
                if (action.target.stats.getHealth() <= 0)
                {
                    events.Enqueue($"{action.target.name} ha sido derrotado.");
                    if(action.attacker.GetType() == typeof(Hero))
                    {
                        int newLevel = action.attacker.stats.level + 1;
                        ((Hero)action.attacker).setLevel(newLevel);
                        if (newLevel >= 3)
                            events.Enqueue($"{action.attacker.name} ha alcanzado el nivel máximo.");
                        else
                            events.Enqueue($"{action.attacker.name} ha subido de nivel.");
                    }
                }
                if(action.attacker.GetType() == typeof(Hero) && action.attack.uses > 0 && action.attack.uses != Mathf.Infinity)
                {
                    action.attack.uses--;
                    events.Enqueue($"{action.attacker.name} ha usado {action.attack.name}.\nLe quedan {action.attack.uses} usos.");
                }
                break;
            case ActionTypes.defense:
                action.attacker.setIsDefending(true);
                messagesManager.displayMessage(
                    DefenseTypes.defenseMessage(action.attacker)
                );
                break;
            case ActionTypes.item:
                action.target.stats.setHealth(action.target.stats.getHealth() + action.item.effect);
                messagesManager.displayMessage(
                    action.item.effect < 0
                    ? $"{PollitoTypes.pollito} {action.attacker.name} le lanzo un huevazo a {action.target.name} y recibe {-1*action.item.effect} de daño."
                    : $"{action.target.name} ha recuperado {action.item.effect} de salud por usar {action.item.name}."
                );
                break;
            default:
                break;
        }
        if(actions.Count == 0)
        {
            doingActions = false;
            playersTurn = !playersTurn;
        }
    }

    public void doEvents()
    {
        messagesManager.messagesPanel.SetActive(true);
        string message = events.Dequeue();
        messagesManager.displayMessage(message);
    }

    public void EnemyTurn()
    {
        actionsManager.actionsPanel.SetActive(false);
        GetComponent<ActionEvent>().enabled = false;
        foreach (Fighter villain in villains)
        {
            if(villain.stats.getHealth() > 0){
                villain.setIsDefending(false);
                List<Fighter> targets = heroes.FindAll(hero => hero.stats.getHealth() > 0);
                actions.Enqueue(
                    new Action(
                        ActionTypes.attack,
                        villain,
                        villain.attacks[Random.Range(0, villain.attacks.Count)],
                        targets[Random.Range(0, targets.Count)]
                    )
                );
                // actions.Enqueue(new Action(ActionTypes.attack, villain, villain.attacks[0], heroes[Random.Range(0, heroes.Count)]));
            }
        }
    }

    
}
