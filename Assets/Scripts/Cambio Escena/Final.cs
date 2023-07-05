using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    [SerializeField] private AnimationClip animacionFinal;

    private void Start()
    {
        
    }

    private void Update()
    {
        StartCoroutine(CambiarEscena());
    }

    IEnumerator CambiarEscena()
    {
        yield return new WaitForSeconds(animacionFinal.length);
        SceneManager.LoadScene("Inicio");
    }
}
