using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Button btnA;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        btnA.onClick.AddListener(LoadSceneA);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void LoadSceneA()
    {
        print("点击");   
        //调用协程
        StartCoroutine(LoadScene(0));
    }


    IEnumerator LoadScene(int index)
    {
        animator.SetBool("FadeIn", true);
        animator.SetBool("FadeOut", false);
        yield return new WaitForSeconds(1);
        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.completed += OnLoadedScene;
    }

    private void OnLoadedScene(AsyncOperation obj)
    {
        animator.SetBool("FadeIn", false);
        animator.SetBool("FadeOut", true);
    }
}
