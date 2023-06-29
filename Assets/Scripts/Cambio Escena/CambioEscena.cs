using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public string escena;
    private Animator transitionAnimator;

    void Start()
    {
        transitionAnimator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(SceneLoad());
    }

    public IEnumerator SceneLoad()
    {
        transitionAnimator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(escena);
    }
}
