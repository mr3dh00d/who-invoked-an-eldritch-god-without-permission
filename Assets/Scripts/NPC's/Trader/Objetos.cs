using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objetos : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI nombreObjeto;
    [SerializeField] public TextMeshProUGUI precioItem;
    
    public void ObtenerInformacion()
    {
        Debug.Log(nombreObjeto.text);
        Debug.Log(precioItem.text);
        
    }
}
