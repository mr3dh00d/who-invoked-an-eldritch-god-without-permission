using System.Collections;
using UnityEngine;

public class Mercader : MonoBehaviour
{
    private Animator MercaderAnimation;
    private bool playerInRage = false;
    private bool isEntering = false;
    private bool shopOpened = false;
    private Coroutine exitCoroutine;
    private NPCChat chat;

    [SerializeField]
    private GameObject shopPanel;

    void Start()
    {
        MercaderAnimation = GetComponent<Animator>();
        chat = GetComponent<NPCChat>();
    }

    void Update()
    {
        if(playerInRage && !shopOpened && !chat.GetIsDialogueActive()){
            shopPanel.SetActive(true);
            shopOpened = true;
        }

        if (playerInRage && shopPanel.activeSelf && Input.GetKeyDown("x"))
        {
            shopPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRage = true;
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
            playerInRage = false;
            shopOpened = false;
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
