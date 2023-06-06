using System.Collections;
using UnityEngine;

public class Mercader : MonoBehaviour
{
    private bool isPlayerinRange;
    private Animator MercaderAnimation;
    // Update is called once per frame
    void Update()
    {
        MercaderAnimation = GetComponent<Animator>();
        if(isPlayerinRange && Input.GetKey("z"))
        {
            MercaderAnimation.SetBool("Active", true);
            MercaderAnimation.SetBool("Termine", false);
        }
        if(isPlayerinRange && Input.GetKey("x"))
        {
            MercaderAnimation.SetBool("Active", false);
            MercaderAnimation.SetBool("Termine", false);
        }
        if(!isPlayerinRange)
        {
            MercaderAnimation.SetBool("Active", false);
            MercaderAnimation.SetBool("Termine", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerinRange = true;
            Debug.Log("si se pudo burro");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerinRange = false;
            Debug.Log("te chingaste perro");
        }
    }
}
