using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tienda : MonoBehaviour
{
    private TextMeshProUGUI DineroLabel;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Semillas = Helpers.FindChildWithName(gameObject, "Semillas");
        if(Semillas != null)
        {
            DineroLabel = Helpers.FindChildWithName(Semillas, "Dinero").GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(DineroLabel != null)
        {
            DineroLabel.text = GameManager.instance.semillas.ToString();
        }
    }
}
