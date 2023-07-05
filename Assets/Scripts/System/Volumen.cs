using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Volumen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Slider>().value = AudioManager.Instance.volumen;
    }
}
