using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrarEnCombate : MonoBehaviour {

    [SerializeField] private string escena;
    [SerializeField] private GameObject Fade;
    private NPCChat chat;
    private Animator animator;
    private Animator fadeAnimator;
    private bool haveToChangeScene = false;
    
    void Start()
    {
        chat = GetComponent<NPCChat>();
        animator = GetComponent<Animator>();
        fadeAnimator = Fade.GetComponent<Animator>();
    }

    private void Update()
    {
        if(haveToChangeScene)
        {
            if(!chat.GetIsDialogueActive())
            {
                LoadNextScene();
            }
        }

    }

     private void OnTriggerEnter2D(Collider2D collision)
     {
            if(collision.tag == "Player")
            {
                haveToChangeScene = true;
                GameManager.instance.lastPosition = collision.transform.position;
                if(animator != null) animator.SetBool("playerInRange", true);
            }
     }

    public void LoadNextScene()
    {
        StartCoroutine(SceneLoad());
    }

    public IEnumerator SceneLoad()
    {
        Fade.SetActive(true);
        if(fadeAnimator != null) fadeAnimator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(escena);
    }

}