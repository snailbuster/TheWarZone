using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    //全局bgm音效
    public AudioSource StartAudio;
    public AudioClip m_StartBgm;
    public Collider myCollider;
    public ParticleSystem shellExplosion; //粒子特效

    public TankControl tk;
    public float SpeedBuff = 10f;


    private bool used = false; //使用标记
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

        //恢复血量
        if (collision.gameObject.tag == "Player" && used == false)
        {
            tk = collision.gameObject.GetComponent<TankControl>();
            tk.speed += SpeedBuff;

            ItemEnd();
            //音效、使用标记和碰撞
            MusicStart();

        }

    }

    void UpAndDestroy()
    {
        //道具升起
        if (used == true)
        {
            Vector3 moveItem = this.gameObject.transform.position;
            moveItem.y += 0.03f;
            this.gameObject.transform.position = moveItem;
        }
        //道具销毁
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

    //播放特效，删除碰撞，添加使用标记
    void ItemEnd()
    {
        shellExplosion.Play();
        used = true;
        myCollider.enabled = false;
    }


}
