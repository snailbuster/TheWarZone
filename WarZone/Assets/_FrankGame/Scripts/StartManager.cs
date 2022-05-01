using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    //ȫ��bgm��Ч
    public AudioSource StartBgmAudio;
    public AudioClip m_StartBgm;
    // Start is called before the first frame update
    void Start()
    {
        BgmStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BgmStart()
    {
        StartBgmAudio.clip = m_StartBgm;
        StartBgmAudio.loop = true;
        StartBgmAudio.volume = 0.2f;
        StartBgmAudio.Play();
    }
}
