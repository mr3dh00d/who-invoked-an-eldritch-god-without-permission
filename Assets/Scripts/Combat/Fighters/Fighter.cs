using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public Stats stats;

    public void Start() {
        stats.setStats(25, 10, 5, 1);
    }

    public void Update() {
        if(Input.GetKeyDown("space")){
            stats.setHealth(stats.getHealth() - 2f);
        }
    }

}