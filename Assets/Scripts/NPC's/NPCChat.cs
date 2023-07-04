using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NPCChat : MonoBehaviour
{
    [SerializeField] private bool AutoStart;
    [SerializeField] private GameObject actionMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private float typingSpeed = 0.03f;
    [SerializeField] public List<NPCDialogue> dialogueLines;
    private NPCDialogue actualDialogue;
    private Image dialogueImage;
    private TextMeshProUGUI dialogueName;
    private TextMeshProUGUI dialogueText;
    private GameObject player;
    private bool isPlayerInRange;
    private bool isDialogueActive;
    private bool didDialogueStarted;
    private int lineIndex;
    // private Sprite mercaderSprite;
    // Update is called once per frame

    void Start()
    {
        dialogueImage = Helpers.FindChildWithName(dialoguePanel, "DialogueImage").GetComponent<Image>();
        dialogueText = Helpers.FindChildWithName(dialoguePanel, "DialogueText").GetComponent<TextMeshProUGUI>();
        dialogueName = Helpers.FindChildWithName(dialoguePanel, "DialogueName").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(isPlayerInRange)
        {
            if(Input.GetKeyDown("z")){
                if(isDialogueActive)
                {
                    if(!didDialogueStarted && !AutoStart)
                    {
                        StartDialogue();
                    }
                    if(didDialogueStarted)
                    {
                        if (dialogueText.text == actualDialogue.GetDialogue())
                        {
                            NextLine();
                        }
                        
                    }

                }
            }
            if (Input.GetKeyDown("x")){
                    if (dialogueText.text != actualDialogue.GetDialogue())
                    {
                        StopAllCoroutines();
                        dialogueText.text = actualDialogue.GetDialogue();
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
            if(AutoStart)
            {
                StartDialogue();
            }
            if(actionMark != null)
            {
                actionMark.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if(actionMark != null)
            {
                actionMark.SetActive(false);
            }
        }
    }
    private void StartDialogue()
    {
        didDialogueStarted = true;
        lineIndex = 0;
        actualDialogue = dialogueLines[lineIndex];
        dialogueName.text = actualDialogue.GetName();
        dialogueImage.sprite = actualDialogue.GetAvatar();
        dialoguePanel.SetActive(true);
        if(actionMark != null)
        {
            actionMark.SetActive(false);   
        }
        player.GetComponent<Movement>().DisableMovement();
        player.GetComponent<Movement>().StopMovement();
        StartCoroutine(ShowLine());
        // dialogueText.text = dialogueLines[0];
    }
    private void NextLine()
    {
        if(lineIndex < dialogueLines.Count - 1)
        {
            lineIndex++;
            actualDialogue = dialogueLines[lineIndex];
            dialogueName.text = actualDialogue.GetName();
            dialogueImage.sprite = actualDialogue.GetAvatar();
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStarted = false;
            isDialogueActive = false;
            dialoguePanel.SetActive(false);
            didDialogueStarted = false;
            shopPanel.SetActive(true);
            player.GetComponent<Movement>().EnableMovement();
        }
    }
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach(char letter in actualDialogue.GetDialogue().ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    public bool GetIsDialogueActive()
    {
        return isDialogueActive;
    }
    
}
