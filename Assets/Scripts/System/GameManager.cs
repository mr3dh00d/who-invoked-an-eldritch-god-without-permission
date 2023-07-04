using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int semillas;

    public List<Fighter> heros;

    public List<Item> inventario;

    public Vector2 lastPosition = Vector2.zero;
    public int leveOfForest = 1;
    public List<string> defeatedEnemiesInForest;
    public int leveOfLand = 1;
    public List<string> defeatedEnemiesInLand;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            foreach (Transform child in Helpers.FindChildWithName(gameObject, "Heros").transform)
            {
                heros.Add(child.GetComponent<Fighter>());
            }
            semillas = 300;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public string MainEnemyLandName (){
        switch (leveOfLand)
        {
            case 1:
                return "Ramphus";
            case 2:
                return "Gymno";
            case 3:
                return "Cora";       
            default:
                return "Vultur";
        }
    }

}
