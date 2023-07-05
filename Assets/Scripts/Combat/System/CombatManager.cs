using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CombatManager : MonoBehaviour
{
    private List<Fighter> heros;
    public GameObject herosPanel;
    public List<Fighter> villains;
    public GameObject villainsPanel;
    private List<Item> items;
    public ActionsManager actionsManager;
    public MessagesManager messagesManager;
    private string[] actionsLabel = new string[] { 
        ActionTypes.attack, 
        ActionTypes.defense, 
        ActionTypes.item,
    };
    private List<Fighter> herosOptions;
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
    public GameObject Fade;
    private bool playerWin;
    public string LoseScene;
    public string ExitScene;

    private enum landOptions
    {
        forest,
        land,
        mountain
    }
    [SerializeField]
    private landOptions selectLandLevel;



    // private Dictionary<Fighter, Action> Actions;

    // Start is called before the first frame update
    void Start()
    {
        heros = GameManager.instance.heros;
        items = GameManager.instance.inventario;
        foreach (Hero hero in heros)
        {
            hero.stats.statsPanel.setUI(Helpers.FindChildWithName(herosPanel, hero.name));
            hero.attacks = hero.attacks.ConvertAll(attack => {
                Attack newAttack = attack.Clone();
                newAttack.uses = 0;
                return newAttack;
            });
            hero.setLevel(hero.stats.level);

        }
        foreach (Enemy villain in villains)
        {
            villain.stats.statsPanel.setUI(Helpers.FindChildWithName(villainsPanel, villain.name));
            switch (selectLandLevel)
            {
                case landOptions.forest:
                    villain.setLevel(GameManager.instance.leveOfForest);
                    break;
                case landOptions.land:
                    villain.setLevel(GameManager.instance.leveOfLand);
                    if(villain is Vultur)
                    {
                        villain.getStats().statsPanel.setName(GameManager.instance.MainEnemyLandName());
                    }
                    break;
                case landOptions.mountain:
                    villain.setLevel(1);
                    villain.getStats().statsPanel.setLevelUI("???");
                    break;
                default:
                    break;
            }
        }
        actionsManager.iniciar();
        messagesManager.sayBattleIntro();
        actions = new Queue<Action>();
        events = new Queue<string>();
        fighting = true;
        playersTurn = true;
    }

    private void setUpPlayerTurn()
    {
        playersTurn = true;
        doingActions = false;
        messagesManager.messagesPanel.SetActive(false);
        actionsManager.actionsPanel.SetActive(true);
        fighterSelected = false;
        actionSelected = false;
        itemSelected = false;
        finishAction = false;
        doingActions = false;
        actions = new Queue<Action>();
        events = new Queue<string>();
        // Actions = new Dictionary<Fighter, Action>();
        herosOptions = heros.FindAll(hero => {
            if(hero.getIsDefending())
            {
                hero.setIsDefending(false);
            }
            return hero.stats.getHealth() > 0;
        });
        villainsOptions = villains.FindAll(villain => {
            Image image = villain.stats.statsPanel.getAvatar();
            if(image){
                image.GetComponent<Animator>().SetBool("Turno", false);
            }
            return villain.stats.getHealth() > 0;
        });
        actionsManager.setActions(herosOptions.ConvertAll(hero => $"Pollito {hero.name}").ToArray());
        GetComponent<ActionEvent>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!fighting)
        {
            if (Input.GetKeyDown("z"))
            {
                StartCoroutine(SceneLoad());        
            }
        }
        if (fighting)
        {
            if(!messagesManager.getIsTyping())
            {
                if (Input.GetKeyDown("z"))
                {
                    if(messagesManager.messagesPanel.activeSelf)
                    {
                        if(playersTurn){
                            if(!doingActions){
                                setUpPlayerTurn();
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
                                else
                                {
                                    EnemyTurn();
                                    doingActions = true;
                                    playersTurn = false;
                                    events.Enqueue("Los enemigos se preparan para atacar.");
                                    doEvents();
                                }
                            }
                        }
                        else 
                        {
                            if(!doingActions)
                            {
                                EnemyTurn();
                                doingActions = true;
                                playersTurn = false;
                                events.Enqueue("Los enemigos se preparan para atacar.");
                                doEvents();
                            }
                            else
                            {
                                int herosAlive = heros.FindAll(hero => hero.stats.getHealth() > 0).Count;
                                int villainsAlive = villains.FindAll(villain => villain.stats.getHealth() > 0).Count;
                                if(herosAlive == 0 || villainsAlive == 0)
                                {
                                    if(herosAlive == 0)
                                    {
                                        playerWin = false;
                                        messagesManager.displayMessage("Has sido derrotado.");
                                    }
                                    else if(villainsAlive == 0)
                                    {
                                        playerWin = true;
                                        int reward = 0;
                                        switch (selectLandLevel)
                                        {
                                            case landOptions.forest:
                                                GameManager.instance.defeatedEnemiesInForest.Add(
                                                    villains[GameManager.instance.defeatedEnemiesInForest.Count].name
                                                );
                                                GameManager.instance.leveOfForest++;
                                                reward = Random.Range(15, 25) * villains.Count;
                                                break;
                                            case landOptions.land:
                                                GameManager.instance.defeatedEnemiesInLand.Add(
                                                    GameManager.instance.MainEnemyLandName()
                                                );
                                                GameManager.instance.leveOfLand++;
                                                reward = Random.Range(20, 30) * villains.Count;
                                                break;
                                            default:
                                                break;
                                        }
                                        messagesManager.displayMessage("Has derrotado a los enemigos.\nHas ganado " + reward + " semillas.");
                                        GameManager.instance.semillas += reward;
                                    }
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
                                } else {
                                    setUpPlayerTurn();
                                }
                            }
                        }
                    }
                    else if (actionsManager.actionsPanel.activeSelf)
                    {
                        if(!fighterSelected)
                        {
                            selectedFighter = herosOptions[actionsManager.actionSelected];
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
                                        attackOptions = selectedFighter.attacks.FindAll(attack => attack.uses < attack.maxUses || attack.maxUses == Mathf.Infinity);
                                        actionsManager.setActions(attackOptions.ConvertAll(attack => $"{attack.name} ({attack.damage} de daño)").ToArray());
                                        break;
                                    case ActionTypes.defense:
                                        actions.Enqueue(new Action(selectedAction, selectedFighter));
                                        finishAction = true;
                                        break;
                                    case ActionTypes.item:
                                        if(items.Count > 0)
                                        {
                                            actionsManager.setActions(items.ConvertAll(item => $"{item.name}").ToArray());
                                        }
                                        else
                                        {
                                            actionsManager.setActions(new string[] { "No tienes objetos" });
                                        }
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
                                            selectedAttack = attackOptions[actionsManager.actionSelected];
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
                                        if (items.Count > 0)
                                        {
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
                            herosOptions.Remove(selectedFighter);
                            items.Remove(selectedItem);
                            actionsManager.setActions(herosOptions.ConvertAll(hero => $"Pollito {hero.name}").ToArray());
                            if (herosOptions.Count == 0)
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
                                actionsManager.setActions(herosOptions.ConvertAll(hero => $"Pollito {hero.name}").ToArray());
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
        if(actions.Count > 0)
        {
            Action action = actions.Dequeue();
            bool isDead;
            switch (action.type)
            {
                case ActionTypes.attack:
                    // Caso especial de pollito Payaso
                    isDead = action.target.stats.getHealth() <= 0;
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
                    Debug.Log($"{action.target.name} tiene {action.target.stats.getHealth()} de vida");
                    if (!isDead && action.target.stats.getHealth() <= 0)
                    {
                        Debug.Log($"{action.target.name} ha sido derrotado.");
                        events.Enqueue($"{action.target.name} ha sido derrotado.");
                        if(action.attacker.GetType() == typeof(Hero))
                        {
                            int newLevel = action.attacker.stats.level + 1;
                            if (newLevel > 6)
                                events.Enqueue($"{action.attacker.name} ya ha alcanzado el nivel máximo.");
                            else
                            {
                                ((Hero)action.attacker).setLevel(newLevel);
                                events.Enqueue($"{action.attacker.name} ha subido de nivel.");
                            }
                        }
                    }
                    if(action.attack.level > 1)
                    {
                        if(action.attacker is Hero){
                            action.attack.uses++;
                        }
                        events.Enqueue($"{action.attacker.name} ha usado {action.attack.name}.\nLe quedan {action.attack.maxUses - action.attack.uses} usos.");
                    }
                    if(action.attacker is Enemy)
                    {
                        Image image = action.attacker.stats.statsPanel.getAvatar();
                        if(image){
                            image.GetComponent<Animator>().SetBool("Turno", true);
                        }
                    }
                    break;
                case ActionTypes.defense:
                    action.attacker.setIsDefending(true);
                    messagesManager.displayMessage(
                        DefenseTypes.defenseMessage(action.attacker)
                    );
                    break;
                case ActionTypes.item:
                    isDead = action.target.stats.getHealth() <= 0;
                    action.target.stats.setHealth(action.target.stats.getHealth() + action.item.effect);
                    messagesManager.displayMessage(
                        action.item.effect < 0
                        ? $"{PollitoTypes.pollito} {action.attacker.name} le lanzo un huevazo a {action.target.name} y recibe {-1*action.item.effect} de daño."
                        : $"{action.target.name} ha recuperado {action.item.effect} de salud por usar {action.item.name}."
                    );
                    if (!isDead && action.target.stats.getHealth() <= 0)
                    {
                        events.Enqueue($"{action.target.name} ha sido derrotado.");
                        if(action.attacker.GetType() == typeof(Hero))
                        {
                            int newLevel = action.attacker.stats.level + 1;
                            if (newLevel > 6)
                                events.Enqueue($"{action.attacker.name} ya ha alcanzado el nivel máximo.");
                            else
                            {
                                ((Hero)action.attacker).setLevel(newLevel);
                                events.Enqueue($"{action.attacker.name} ha subido de nivel.");
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

        }
        if(actions.Count == 0 && events.Count == 0)
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
                if(villain is Poseido)
                {
                    villain.setIsDefending(false);
                    List<Fighter> targets = heros.FindAll(hero => hero.stats.getHealth() > 0);
                    List<Attack> attackOptions = villain.attacks.FindAll(attack => attack.uses < attack.maxUses || attack.maxUses == Mathf.Infinity);
                    Attack selectedAttack = attackOptions[Random.Range(0, attackOptions.Count)];
                    selectedAttack.uses++;
                    actions.Enqueue(
                        new Action(
                            ActionTypes.attack,
                            villain,
                            selectedAttack,
                            targets[Random.Range(0, targets.Count)]
                        )
                    );
                }
                else if (villain is Vultur)
                {
                    villain.setIsDefending(false);
                    List<Fighter> targets = heros.FindAll(hero => hero.stats.getHealth() > 0);
                    for (int i = 0; i < 2; i++)
                    {
                        List<Attack> attackOptions = villain.attacks.FindAll(attack => attack.uses < attack.maxUses || attack.maxUses == Mathf.Infinity);
                        Attack selectedAttack = attackOptions[Random.Range(0, attackOptions.Count)];
                        selectedAttack.uses++;
                        actions.Enqueue(
                            new Action(
                                ActionTypes.attack,
                                villain,
                                selectedAttack,
                                targets[Random.Range(0, targets.Count)]
                            )
                        );
                    }   
                }
                else if(villain is CluckNThulu)
                {
                    villain.setIsDefending(false);
                    List<Fighter> targets = heros.FindAll(hero => hero.stats.getHealth() > 0);
                    foreach (Hero target in targets)
                    {
                        List<Attack> attackOptions = villain.attacks.FindAll(attack => attack.uses < attack.maxUses || attack.maxUses == Mathf.Infinity);
                        Debug.Log("Posibles ataques: " + attackOptions.Count);
                        Attack selectedAttack = attackOptions[Random.Range(0, attackOptions.Count)];
                        selectedAttack.uses++;
                        actions.Enqueue(
                            new Action(
                                ActionTypes.attack,
                                villain,
                                selectedAttack,
                                target
                            )
                        );
                    }
                }
            }
        }
    }

    public IEnumerator SceneLoad()
    {
        Fade.SetActive(true);
        Animator fadeAnimator = Fade.GetComponent<Animator>();
        if(fadeAnimator != null) fadeAnimator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(
            playerWin ? ExitScene : LoseScene
        );
    }

    
}
