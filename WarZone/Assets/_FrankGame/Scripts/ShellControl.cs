using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellControl : MonoBehaviour
{
    public ParticleSystem shellExplosion;

    public float explosionRadius;
    public float explosionForce;
    public string belong;
    public LayerMask tankMask; //目标类型


    //音效
    public AudioClip m_ShellExploAudio;

    // Start is called before the first frame update
    void Start()
    {
        ShellInit();
    }

    //shell初始化
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
        
        if(collision.gameObject.layer == this.gameObject.layer) //打到自己
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
                    float boom_position_buff = (this.transform.position - tankRigidbody.position).magnitude - 1;//爆炸点到目标点距离带来的伤害增益
                    float currentDamage = boom_position_buff * tankControl.currentShellSpeed / tankControl.MaxShellSpeed * tankControl.shellDamage; //伤害公式：当前速度比例*伤害*爆炸点到目标距离
                    if (tankControl != null)
                    {
                        tankControl.ShellDamage(currentDamage);
                    }
                    print("坦克对敌人造成伤害" + currentDamage);
                }
                else if(belong == "Player")
                {

                    var tankControl = tankColliders[i].gameObject.GetComponent<EnemyTankControl>();
                    float boom_position_buff = (this.transform.position - tankRigidbody.position).magnitude - 1;//爆炸点到目标点距离带来的伤害增益
                    float currentDamage = boom_position_buff * tankControl.currentShellSpeed / tankControl.MaxShellSpeed * tankControl.shellDamage; //伤害公式：当前速度比例*伤害*爆炸点到目标距离
                    print("shell damage" + tankControl.shellDamage);
                    if (tankControl != null)
                    {
                        tankControl.ShellDamage(currentDamage);
                    }
                    print("敌人对坦克造成伤害" + currentDamage);
                }
                
                
            }
            
        }
        
        shellExplosion.transform.parent = null;
        if (shellExplosion != null)
        {
            ShellExplosionAudio();//音效
            shellExplosion.Play();
            Destroy(shellExplosion.gameObject, shellExplosion.main.duration);
          
        }
        Destroy(this.gameObject);
    }

    //子弹爆炸音效
    private void ShellExplosionAudio()
    {
        AudioSource.PlayClipAtPoint(m_ShellExploAudio, Camera.main.transform.position);
    }
}