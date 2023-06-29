using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string escena;

    public void Jugar()
    {
        SceneManager.LoadScene(escena);
    }

    public void Salir()
    {
        Debug.Log("Saliste");
        Application.Quit();
    }
}
