using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TankType
{
    Tank_One = 1,
    Tank_Two = 2,
    Tank_Enemy = 3,
}

public class TankControl : MonoBehaviour
{

    public Rigidbody _rigidbody;
    public float h_Value;
    public float v_Value;
    public float speed;
    public float rotateSpeed;

    public TankType tankType = TankType.Tank_One;
    

    public string inputHorizontalStr;
    public string inputVerticalStr;
    public string inputFireStr;


    //Fire
    public GameObject shell;
    public Transform shellPos;
    public float MaxShellSpeed;
    public float currentShellSpeed;
    public float shellSpeedChange;
    public float shellDamage;
    public float currentFireInterval;
    public float fireInterval;

    //HP
    public float HP; //血量
    public Slider hpSlider;
    public bool HpProtect = false; //血量保护 true的时候无法掉血
    public GameObject Sheild;

    //坦克爆炸
    public ParticleSystem tankExplosion;
    public AudioSource tankFire;

    //音效
    public AudioSource m_TankAudio;         
    public AudioClip m_EngineIdling;            
    public AudioClip m_EngineDriving;           
    public AudioSource m_TankShootAudio;
    public AudioClip m_TankFire;

    //初始化标志
    private bool InitDone = false;
    

    // Start is called before the first frame update
    void Start()
    {
        TankInitilization();
        AudioStart();


    }

    //功能：坦克属性初始化
    void TankInitilization()
    {
        print("坦克初始化type"+tankType);
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        inputHorizontalStr = inputHorizontalStr + (int)tankType;
        inputVerticalStr = inputVerticalStr + (int)tankType;
        inputFireStr = inputFireStr + (int)tankType;

        

        this.HP = 100; //血量
        this.speed = 30; //速度 
        this.rotateSpeed = 50;//转身速率

                               
        hpSlider.maxValue = HP;//初始化slider
        hpSlider.value = HP;
        hpSlider.minValue = 0;

        this.MaxShellSpeed = 30; //子弹最大速度
        this.currentShellSpeed = 10; //当前子弹速度
        this.shellSpeedChange = 5;//子弹蓄力速度
        this.shellDamage = 30; //测试用300 普通30
        this.fireInterval = 0.2f; //开火间隔最小值


        this.InitDone = true; //初始化结束


    }

    // Update is called once per frame
    //Horizontal1 ad
    //Vertical1  sw
    void Update()
    {
        TankMove(); //坦克移动
        currentFireInterval -= Time.deltaTime; //开火计时器

        //特效组
        HpProtectEffect();


    }

    //功能：坦克移动
    private void TankMove()
    {
        h_Value = Input.GetAxis(inputHorizontalStr);
        v_Value = Input.GetAxis(inputVerticalStr);
        EngineAudio(h_Value, v_Value); //移动音效

        //move
        if (v_Value != 0)
        {
            _rigidbody.MovePosition(this.transform.position + v_Value * this.transform.forward * speed * Time.deltaTime);
        }
        if (h_Value != 0)
        {
            if (v_Value < 0)
            {
                h_Value = -h_Value;
            }
            this.gameObject.transform.Rotate(Vector3.up * h_Value * rotateSpeed * Time.deltaTime);
        }


        if (Input.GetButton(inputFireStr))
        {
            currentShellSpeed += shellSpeedChange * Time.deltaTime;
            if (currentShellSpeed >= MaxShellSpeed)
            {
                currentShellSpeed = MaxShellSpeed;
            }
        }

        if (Input.GetButtonUp(inputFireStr))
        {
            OpenFire(currentShellSpeed);
            currentShellSpeed = 10;
        }
    }

    //功能：坦克开火
    void OpenFire(float shellSpeed)
    {
        if (currentFireInterval < 0)
        {
            currentFireInterval = fireInterval;
        }
        else
        {
            print("cd剩余"+(fireInterval - currentFireInterval));
            return;
        }
        GameObject shellObj = Instantiate(shell, shellPos.position, shellPos.transform.rotation);
        Rigidbody shellRigidbody = shellObj.GetComponent<Rigidbody>();
        if (shellRigidbody != null)
        {
            TankFireAudio();//开火音效
            shellRigidbody.velocity = shellPos.forward * shellSpeed;
        }

    }

    public void ShellDamage(float damage)
    {
        if(HpProtect == true)
        {
            return;
        }
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
            this.gameObject.SetActive(false);
        }

    }

    //声音初始化
    public void AudioStart()
    {
        m_TankAudio.clip = m_EngineDriving;
        m_TankAudio.spatialBlend = 0;
        m_TankAudio.pitch = 0.9f;
        m_TankAudio.loop = true;
        m_TankAudio.volume = 0;
        m_TankAudio.Play();

   
    }

    //移动音效
    private void EngineAudio(float h_value, float v_value)
    {
        if (Mathf.Abs(h_value) > 0.1f || Mathf.Abs(v_value) > 0.1f)
        {
            m_TankAudio.volume = 0.1f;
        }
        else
        {
            m_TankAudio.volume -= 0.1f;
            m_TankAudio.volume = Mathf.Max(0, m_TankAudio.volume);
        }
        
    }

    //开火音效
    private void TankFireAudio()
    {
        AudioSource.PlayClipAtPoint(m_TankFire, Camera.main.transform.position);
    }

    //获取初始化状态
    public bool IsInit()
    {
        return this.InitDone;
    }

    public void HpProtectEffect()
    {
        if(this.HpProtect == true)
        {
            this.Sheild.SetActive(true);
        }else
        {
            this.Sheild.SetActive(false);
        }
    }

}
