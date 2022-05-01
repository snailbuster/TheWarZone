using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    //ȫ��bgm��Ч
    public AudioSource StartAudio;
    public AudioClip m_StartBgm;
    public Collider myCollider;
    public ParticleSystem shellExplosion; //������Ч

    public TankControl tk;
    public float SpeedBuff = 10f;


    private bool used = false; //ʹ�ñ��
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpAndDestroy();
    }


    void OnCollisionEnter(Collision collision)
    {

        //�ָ�Ѫ��
        if (collision.gameObject.tag == "Player" && used == false)
        {
            tk = collision.gameObject.GetComponent<TankControl>();
            tk.speed += SpeedBuff;

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
        //��������
        if (StartAudio.isPlaying == false && used == true)
        {
            Destroy(this.gameObject);
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
