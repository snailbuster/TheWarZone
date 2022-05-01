using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    //��ʼ̹��λ��
    public Transform posOne;
    public Transform posTwo;
    public GameObject tankPrefab;
    public Color tankOneColor;
    public Color tankTwoColor;

    public MyCamera camerControl;

    // �����������
    public int playerNum = 1;

    //ȫ��bgm��Ч
    public AudioSource TankBgmAudio;
    public AudioClip m_TankBgm;

    //˽��

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


    void TankSpawn_two()  //����tank��ʼ��tank
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

    //������Ϸbgm
    void BgmStart()
    {
        TankBgmAudio.clip = m_TankBgm;
        TankBgmAudio.loop = true;
        TankBgmAudio.volume = 0.2f;
        TankBgmAudio.Play();
    }

    
}