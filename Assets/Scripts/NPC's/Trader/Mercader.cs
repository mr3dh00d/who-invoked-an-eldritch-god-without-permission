using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Mercader : MonoBehaviour
{
    [SerializeField] private GameObject actionMark;
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image dialogueImage;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    [SerializeField] private float typingSpeed = 0.03f;
    private GameObject player;
    private bool isPlayerInRange;
    private bool isDialogueActive;
    private bool didDialogueStarted;
    private int lineIndex;
    private Animator MercaderAnimation;
    private Sprite mercaderSprite;
    // Update is called once per frame

    void Start()
    {
        MercaderAnimation = GetComponent<Animator>();
        mercaderSprite = GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {
        if(isPlayerInRange)
        {
            if(Input.GetKey("z")){
                if(isDialogueActive)
                {
                    if(!didDialogueStarted)
                    {
                        StartDialogue();
                    }
                    if(didDialogueStarted)
                    {
                        if (dialogueText.text == dialogueLines[lineIndex])
                        {
                            NextLine();
                        }
                        // else if (dialogueText.text != dialogueLines[lineIndex])
                        // {
                        //     StopAllCoroutines();
                        //     dialogueText.text = dialogueLines[lineIndex];
                        // }
                    }

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            isDialogueActive = true;
            isPlayerInRange = true;
            actionMark.SetActive(true);
            MercaderAnimation.SetBool("Active", true);
            Debug.Log("si se pudo burro");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            actionMark.SetActive(false);
            MercaderAnimation.SetBool("Active", false);
            Debug.Log("te chingaste perro");
        }
    }
    private void StartDialogue()
    {
        didDialogueStarted = true;
        dialogueImage.sprite = mercaderSprite;
        dialoguePanel.SetActive(true);
        actionMark.SetActive(false);
        lineIndex = 0;
        player.GetComponent<Movement>().enabled = false;
        StartCoroutine(ShowLine());
        // dialogueText.text = dialogueLines[0];
    }
    private void NextLine()
    {
        if(lineIndex < dialogueLines.Length - 1)
        {
            lineIndex++;
            dialogueText.text = string.Empty;
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStarted = false;
            isDialogueActive = false;
            dialoguePanel.SetActive(false);
            didDialogueStarted = false;
            player.GetComponent<Movement>().enabled = true;
        }
    }
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        if(!(lineIndex%2==0))
        {
            dialogueImage.sprite = playerSprite;
        }
        else
        {
            dialogueImage.sprite = mercaderSprite;
        }

        foreach(char letter in dialogueLines[lineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }
    
}
