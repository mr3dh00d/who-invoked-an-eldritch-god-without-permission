using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objetos : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI nombreObjeto;
    [SerializeField] public TextMeshProUGUI precioItem;
    [SerializeField] public GameObject errorItem;
    private TextMeshProUGUI errorMessage;

    public void Start()
    {
        errorMessage = Helpers.FindChildWithName(errorItem, "Error").GetComponent<TextMeshProUGUI>();
        errorItem.SetActive(false);
    }
    
    public void ObtenerInformacion()
    {
        if(GameManager.instance.inventario.Count > 7){
            errorMessage.text = "Inventario lleno";
            errorItem.SetActive(true);
            return;
        }

        int precio = int.Parse(precioItem.text);

        if(GameManager.instance.semillas < precio){
            errorMessage.text = "No tienes suficientes semillas";
            errorItem.SetActive(true);
            return;
        }

        errorItem.SetActive(false);

        GameManager.instance.semillas -= precio;
        switch (nombreObjeto.text)
        {
            case "Agua con Ajo":
                GameManager.instance.inventario.Add(new Item(nombreObjeto.text, 10));
                break;
            case "Gusanos":
                GameManager.instance.inventario.Add(new Item(nombreObjeto.text, 5));
                break;
            case "Huevitos":
                GameManager.instance.inventario.Add(new Item(nombreObjeto.text, -10));
                break;
            default:
                break;
        }
        // Debug.Log(nombreObjeto.text);
        // Debug.Log(precioItem.text);
    }
}
