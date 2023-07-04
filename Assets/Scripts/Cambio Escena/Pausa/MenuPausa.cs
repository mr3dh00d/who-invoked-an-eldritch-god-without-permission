using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject opciones;
    public GameObject controles;

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        opciones.SetActive(false);
        controles.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void Opciones()
    {
        pauseMenuUI.SetActive(false);
        opciones.SetActive(true);
    }

    public void Controles()
    {
        pauseMenuUI.SetActive(false);
        controles.SetActive(true);
    }

}