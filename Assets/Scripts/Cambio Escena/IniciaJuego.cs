using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IniciaJuego : MonoBehaviour
{
    public string escena;

    public void Play()
    {
        SceneManager.LoadScene(escena);
    }
}
