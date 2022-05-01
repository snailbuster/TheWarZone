using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    public GameObject eventObj;
    public Button btnA;
    public Button btnB;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        //GameObject.DontDestroyOnLoad(this.eventObj);

        btnA.onClick.AddListener(LoadSceneA);
        btnB.onClick.AddListener(LoadSceneB);
    }
    private void LoadSceneA()
    {
        print("press a");
        //调用协程
        StartCoroutine(LoadScene(2));
        Button[] buttons = this.gameObject.GetComponentsInChildren<Button>();
        for (int i = 0;i<buttons.Length;i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        GameObject image = this.transform.Find("Image2").gameObject;
        image.gameObject.SetActive(false);
        

    }

    private void LoadSceneB()
    {
        StartCoroutine(LoadScene(1));
        GameObject.DontDestroyOnLoad(this.gameObject);
        GameObject.DontDestroyOnLoad(this.eventObj);
    }

    IEnumerator LoadScene(int index)
    {
        animator.SetBool("FadeIn",true);
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



    // Update is called once per frame
    void Update()
    {
        
    }
}
