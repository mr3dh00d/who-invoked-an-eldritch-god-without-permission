using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageEvent : MonoBehaviour
{

    public void StartMessageCoroutine(IEnumerator Routine)
    {
        StartCoroutine(Routine);
    }
}
