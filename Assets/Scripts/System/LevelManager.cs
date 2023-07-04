using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<EnemyInLevel> villains;
        private enum landOptions
    {
        forest,
        land,
        mountain
    }
    [SerializeField]
    private landOptions selectLandLevel;
    void Start()
    {
        List<string> enemies;
        switch (selectLandLevel)
        {
            case landOptions.forest:
                enemies = GameManager.instance.defeatedEnemiesInForest;
                break;
            case landOptions.land:
                enemies = GameManager.instance.defeatedEnemiesInLand;
                break;
            case landOptions.mountain:
                enemies = new List<string>();
                break;
            default:
                enemies = new List<string>();
                break;
        }
        foreach (string enemy in enemies)
        {
            EnemyInLevel enemyInLevel = villains.Find(x => x.key == enemy);
            if(enemyInLevel != null)
            {
                Object.Destroy(enemyInLevel.enemy);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
