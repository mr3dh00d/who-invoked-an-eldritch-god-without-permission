using System.Collections;
using UnityEngine;

public class Mercader : MonoBehaviour
{
    private Animator MercaderAnimation;
    private bool isEntering = false;
    private Coroutine exitCoroutine;

    void Start()
    {
        MercaderAnimation = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isEntering && exitCoroutine == null)
            {
                isEntering = true;
                MercaderAnimation.SetBool("Active", true);
                StartCoroutine(EnterAnimationCoroutine());
            }
            else if (isEntering && exitCoroutine != null)
            {
                StopCoroutine(exitCoroutine);
                exitCoroutine = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isEntering)
            {
                isEntering = false;
                exitCoroutine = StartCoroutine(ExitAnimationCoroutine());
            }
        }
    }

    private IEnumerator EnterAnimationCoroutine()
    {
        yield return new WaitForSeconds(MercaderAnimation.GetCurrentAnimatorStateInfo(0).length);
        isEntering = false;
    }

    private IEnumerator ExitAnimationCoroutine()
    {
        yield return new WaitForSeconds(0.5f); // Tiempo de espera antes de desactivar la animación
        MercaderAnimation.SetBool("Active", false);
        exitCoroutine = null;
    }
}
