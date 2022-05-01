using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTankControl : MonoBehaviour
{
    //基础属性
    public float HP;
    public float speed;
    public float rotateSpeed;
    public bool DontMove = false;
    //子弹
    public float MaxShellSpeed;
    public float currentShellSpeed;
    public float shellDamage;

    public Rigidbody _rigidbody;
    int is_rotate;
    int is_forward;
    public GameObject shell;
    public Transform shellPos;

    //特效
    public ParticleSystem tankExplosion;
    public Slider hpSlider;

    //私有
    private float no_change_time = 1f;
    private bool change_direct = true;

    //初始化标志
    public bool InitDone = false;

    //poseidon 
    public bool poseidon_fire = false;



    public LayerMask building;

    // Start is called before the first frame update
    void Start()
    {
        TankInit();
        InvokeRepeating("ChangeHead", 0.1f, 2.0f);
        InvokeRepeating("CollidRepair", 0.1f, Time.deltaTime);
        InvokeRepeating("OpenFire", 0.1f, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(DontMove == false)
        {
            TankMove();
        }
        Destroy_Tank_Judge();
    }
    void TankInit()
    {
        this.speed = 5;
        this.HP = 1; //100
        this.rotateSpeed = 120;


        this.MaxShellSpeed = 30;
        this.currentShellSpeed = 20;
        this.shellDamage = 30;


        hpSlider.maxValue = HP;//初始化slider
        hpSlider.value = HP;
        hpSlider.minValue = 0;

        this.InitDone = true;
    }

    void TankMove()
    {
        
        if (is_forward != 0)
        {
            _rigidbody.MovePosition(this.transform.position + is_forward * this.transform.forward * speed * Time.deltaTime);
        }
        if (is_rotate != 0)
        {
            if (is_forward < 0)
            {
                is_rotate = -is_rotate;
            }
            this.gameObject.transform.Rotate(Vector3.up * is_rotate * rotateSpeed * Time.deltaTime);
        }
       
    }

    void ChangeHead()
    {
        while (change_direct)
        {
            is_rotate = Random.Range(-1, 2); //是否转向 0不转 -1左 1右
            is_forward = Random.Range(-1, 4); //是否直行 0不走 1直行 -1后退
            is_forward = Mathf.Min(is_forward, 1);
            if (is_rotate != 0 || is_forward != 0)
            { 
                break;
            }
        }

    }
    void CollidRepair()
    {
        Collider[] coliders = Physics.OverlapSphere(this.transform.position, 2, building);
        if (coliders.Length > 0 && no_change_time > 0.99)
        {
            this.is_forward = -this.is_forward;
            change_direct = false;
            no_change_time -= Time.deltaTime; //1秒不改变方向
        }
        else if (coliders.Length > 0)
        {
            no_change_time -= Time.deltaTime * 1.2f; //1秒不改变方向
        }else
        {
            change_direct = true;
            no_change_time = 1f;
        }
        return;
    }

    void OpenFire()
    {
        GameObject shellObj = Instantiate(shell, shellPos.position, shellPos.transform.rotation);
        Rigidbody shellRigidbody = shellObj.GetComponent<Rigidbody>();
        if (shellRigidbody != null)
        {
            shellRigidbody.velocity = shellPos.forward * currentShellSpeed;
        }

        poseidon_fire = true;//开火标记

    }

    public void ShellDamage(float damage)
    {
        if (HP > 0)
        {
            HP -= damage;
            hpSlider.value = HP;
        }
        if (HP <= 0)
        {
            //AudioManager._audioManagerInstance.tankExplosionAudioPlay();
            if (tankExplosion != null)
            {
                tankExplosion.transform.parent = null;
                tankExplosion.Play();
                Destroy(tankExplosion.gameObject, tankExplosion.main.duration);

            }
            //this.gameObject.SetActive(false);
            
        }

    }

    public void Destroy_Tank_Judge()
    {
        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    

}
