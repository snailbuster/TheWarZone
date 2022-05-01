using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    //初始坦克位置
    public Transform posOne;
    public Transform posTwo;
    public GameObject tankPrefab;
    public Color tankOneColor;
    public Color tankTwoColor;

    public MyCamera camerControl;

    // 输入玩家数量
    public int playerNum = 1;

    //全局bgm音效
    public AudioSource TankBgmAudio;
    public AudioClip m_TankBgm;

    //私有

    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Destroy(GameObject.Find("CanvasMain"));
        BgmStart();
        if (playerNum == 1)
        {
            TankSpawn_one();
        }
        else
        {
            TankSpawn_two();
        }

        if (camerControl != null)
        {
            camerControl.tanks = GameObject.FindGameObjectsWithTag("Player");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TankSpawn()
    {
        GameObject tankOne = Instantiate(tankPrefab, posOne.position, posOne.transform.rotation);
        var tankOneControl =  tankOne.GetComponent<TankControl>();
        if (tankOneControl != null)
        {
            tankOneControl.tankType = TankType.Tank_One;
            MeshRenderer[] renderers = tankOne.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = tankOneColor;
            }

        }


        GameObject tankTwo = Instantiate(tankPrefab, posTwo.position, posTwo.transform.rotation);
        var tankTwoControl = tankTwo.GetComponent<TankControl>();
        if (tankTwoControl != null)
        {
            tankTwoControl.tankType = TankType.Tank_Two;
            MeshRenderer[] renderers = tankTwo.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = tankTwoColor;
            }
        }

    }


    void TankSpawn_two()  //生成tank初始化tank
    {
        GameObject tankOne = Instantiate(tankPrefab, posOne.position, posOne.transform.rotation);
        var tankOneControl = tankOne.GetComponent<TankControl>();
        if (tankOneControl != null)
        {
            tankOneControl.tankType = TankType.Tank_One;
            MeshRenderer[] renderers = tankOne.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = tankOneColor;
            }

        }


        GameObject tankTwo = Instantiate(tankPrefab, posTwo.position, posTwo.transform.rotation);
        var tankTwoControl = tankTwo.GetComponent<TankControl>();
        if (tankTwoControl != null)
        {
            tankTwoControl.tankType = TankType.Tank_Two;
            MeshRenderer[] renderers = tankTwo.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = tankTwoColor;
            }
        }
    }

    void TankSpawn_one()
    {
        GameObject tankOne = Instantiate(tankPrefab, posOne.position, posOne.transform.rotation);
        var tankOneControl = tankOne.GetComponent<TankControl>();
        if (tankOneControl != null)
        {
            tankOneControl.tankType = TankType.Tank_One;
            MeshRenderer[] renderers = tankOne.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = tankOneColor;
            }

        }

    }

    //播放游戏bgm
    void BgmStart()
    {
        TankBgmAudio.clip = m_TankBgm;
        TankBgmAudio.loop = true;
        TankBgmAudio.volume = 0.2f;
        TankBgmAudio.Play();
    }

    
}
