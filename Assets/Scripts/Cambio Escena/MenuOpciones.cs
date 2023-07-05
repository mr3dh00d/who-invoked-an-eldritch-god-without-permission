using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuOpciones : MonoBehaviour
{
    public void Update()
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("Volumen", AudioManager.Instance.volumen);
        }
    }

    [SerializeField] private AudioMixer audioMixer;

    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void CambiarVolumen(float volumen)
    {
        AudioManager.Instance.volumen = volumen;
    }
}
