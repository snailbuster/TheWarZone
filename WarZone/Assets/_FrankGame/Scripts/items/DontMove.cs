using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontMove : MonoBehaviour
{
    //ȫ��bgm��Ч
    public AudioSource StartAudio;
    public AudioClip m_StartBgm;
    public Collider myCollider;
    public ParticleSystem shellExplosion; //������Ч

    public TankControl tk;
    public float ControlTime = 10f;


    private bool used = false; //ʹ�ñ��
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�������Ч��ʹ����ʼ��ʱ
        if (used == true)
        {
            ControlTime -= Time.deltaTime;
            if (ControlTime < 0)
            {
                EnemyTankControl[] enemytanks = GameObject.FindObjectsOfType<EnemyTankControl>();
                for (int i = 0; i < enemytanks.Length; i++)
                {
                    enemytanks[i].DontMove = false;
                }
                print("���ܽ���");
                Destroy(this.gameObject);
            }
        }
        UpAndDestroy();
    }


    void OnCollisionEnter(Collision collision)
    {

        //�ָ�Ѫ��
        if (collision.gameObject.tag == "Player" && used == false)
        {
            EnemyTankControl[] enemytanks = GameObject.FindObjectsOfType<EnemyTankControl>();
            for(int i = 0;i < enemytanks.Length;i++)
            {
                enemytanks[i].DontMove = true;
            }
            

            ItemEnd();
            //��Ч��ʹ�ñ�Ǻ���ײ
            MusicStart();

        }

    }

    void UpAndDestroy()
    {
        //��������
        if (used == true)
        {
            Vector3 moveItem = this.gameObject.transform.position;
            moveItem.y += 0.03f;
            this.gameObject.transform.position = moveItem;
        }

    }

    void MusicStart()
    {
        StartAudio.clip = m_StartBgm;
        StartAudio.loop = false;
        StartAudio.volume = 0.4f;
        StartAudio.Play();
    }

    //������Ч��ɾ����ײ�����ʹ�ñ��
    void ItemEnd()
    {
        shellExplosion.Play();
        used = true;
        myCollider.enabled = false;
    }


}