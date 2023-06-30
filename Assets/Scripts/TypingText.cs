using System.Collections;
using UnityEngine;
using TMPro;

public class TypingText : MonoBehaviour
{
    [SerializeField] private GameObject Imagen1;
    [SerializeField] private GameObject Imagen2;
    [SerializeField] private GameObject Imagen3;
    [SerializeField] private GameObject Imagen4;
    [SerializeField] private GameObject Imagen5;
    [SerializeField] private GameObject Imagen6;
    [SerializeField] private GameObject Imagen7;
    [SerializeField] private GameObject Imagen8;
    [SerializeField] private GameObject PanelTexto;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    
    private bool didDialogueStart;
    private float typingTime = 0.05f;
    private int lineIndex;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(!didDialogueStart)
            {
                StartDialogue();
            }
            else if(DialogueText.text == dialogueLines[lineIndex] && Input.GetKey("z"))
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                DialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        Imagen1.SetActive(true);
        PanelTexto.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }
    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            switch(lineIndex)
            {
                case 1:
                    Imagen1.SetActive(false);
                    Imagen2.SetActive(true);
                    StartCoroutine(ShowLine());
                    break;
                case 2:
                    Imagen2.SetActive(false);
                    Imagen3.SetActive(true);
                    StartCoroutine(ShowLine());
                    break;
                case 3:
                    Imagen3.SetActive(false);
                    Imagen4.SetActive(true);
                    StartCoroutine(ShowLine());
                    break;
                case 4:
                    Imagen4.SetActive(false);
                    Imagen5.SetActive(true);
                    StartCoroutine(ShowLine());
                    break;
                case 5:
                    Imagen5.SetActive(false);
                    Imagen6.SetActive(true);
                    StartCoroutine(ShowLine());
                    break;
                case 6:
                    Imagen6.SetActive(false);
                    Imagen7.SetActive(true);
                    StartCoroutine(ShowLine());
                    break;
                case 7:
                    Imagen7.SetActive(false);
                    Imagen8.SetActive(true);
                    StartCoroutine(ShowLine());
                    break;
            }
            
        }
        else
        {
            didDialogueStart = false;
            Imagen8.SetActive(false);
            PanelTexto.SetActive(false);
        }
    }

    private IEnumerator ShowLine()
    {
        DialogueText.text = string.Empty;
        foreach(char ch in dialogueLines[lineIndex])
        {
            DialogueText.text +=ch;
            yield return new WaitForSeconds(typingTime);
        }
    }
}
