using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellControl : MonoBehaviour
{
    public ParticleSystem shellExplosion;

    public float explosionRadius;
    public float explosionForce;
    public string belong;
    public LayerMask tankMask; //Ŀ������


    //��Ч
    public AudioClip m_ShellExploAudio;

    // Start is called before the first frame update
    void Start()
    {
        ShellInit();
    }

    //shell��ʼ��
    void ShellInit()
    {
        this.explosionForce = 150;
        this.explosionRadius = 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] tankColliders = Physics.OverlapSphere(this.transform.position, explosionRadius, tankMask);
        
        if(collision.gameObject.layer == this.gameObject.layer) //���Լ�
        {
            return;
        }


        for (int i = 0; i < tankColliders.Length; i++)
        {
            var tankRigidbody = tankColliders[i].gameObject.GetComponent<Rigidbody>();
            if (tankRigidbody != null)
            {
                tankRigidbody.AddExplosionForce(explosionForce, this.transform.position, explosionRadius);
                //float distance = (this.transform.position - tankRigidbody.position).magnitude;
                
                if(belong == "Enemy")
                {
                    var tankControl = tankColliders[i].gameObject.GetComponent<TankControl>();
                    float boom_position_buff = (this.transform.position - tankRigidbody.position).magnitude - 1;//��ը�㵽Ŀ������������˺�����
                    float currentDamage = boom_position_buff * tankControl.currentShellSpeed / tankControl.MaxShellSpeed * tankControl.shellDamage; //�˺���ʽ����ǰ�ٶȱ���*�˺�*��ը�㵽Ŀ�����
                    if (tankControl != null)
                    {
                        tankControl.ShellDamage(currentDamage);
                    }
                    print("̹�˶Ե�������˺�" + currentDamage);
                }
                else if(belong == "Player")
                {

                    var tankControl = tankColliders[i].gameObject.GetComponent<EnemyTankControl>();
                    float boom_position_buff = (this.transform.position - tankRigidbody.position).magnitude - 1;//��ը�㵽Ŀ������������˺�����
                    float currentDamage = boom_position_buff * tankControl.currentShellSpeed / tankControl.MaxShellSpeed * tankControl.shellDamage; //�˺���ʽ����ǰ�ٶȱ���*�˺�*��ը�㵽Ŀ�����
                    print("shell damage" + tankControl.shellDamage);
                    if (tankControl != null)
                    {
                        tankControl.ShellDamage(currentDamage);
                    }
                    print("���˶�̹������˺�" + currentDamage);
                }
                
                
            }
            
        }
        
        shellExplosion.transform.parent = null;
        if (shellExplosion != null)
        {
            ShellExplosionAudio();//��Ч
            shellExplosion.Play();
            Destroy(shellExplosion.gameObject, shellExplosion.main.duration);
          
        }
        Destroy(this.gameObject);
    }

    //�ӵ���ը��Ч
    private void ShellExplosionAudio()
    {
        AudioSource.PlayClipAtPoint(m_ShellExploAudio, Camera.main.transform.position);
    }
}