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
    public float HP; //Ѫ��
    public Slider hpSlider;
    public bool HpProtect = false; //Ѫ������ true��ʱ���޷���Ѫ
    public GameObject Sheild;

    //̹�˱�ը
    public ParticleSystem tankExplosion;
    public AudioSource tankFire;

    //��Ч
    public AudioSource m_TankAudio;         
    public AudioClip m_EngineIdling;            
    public AudioClip m_EngineDriving;           
    public AudioSource m_TankShootAudio;
    public AudioClip m_TankFire;

    //��ʼ����־
    private bool InitDone = false;
    

    // Start is called before the first frame update
    void Start()
    {
        TankInitilization();
        AudioStart();


    }

    //���ܣ�̹�����Գ�ʼ��
    void TankInitilization()
    {
        print("̹�˳�ʼ��type"+tankType);
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        inputHorizontalStr = inputHorizontalStr + (int)tankType;
        inputVerticalStr = inputVerticalStr + (int)tankType;
        inputFireStr = inputFireStr + (int)tankType;

        

        this.HP = 100; //Ѫ��
        this.speed = 30; //�ٶ� 
        this.rotateSpeed = 50;//ת������

                               
        hpSlider.maxValue = HP;//��ʼ��slider
        hpSlider.value = HP;
        hpSlider.minValue = 0;

        this.MaxShellSpeed = 30; //�ӵ�����ٶ�
        this.currentShellSpeed = 10; //��ǰ�ӵ��ٶ�
        this.shellSpeedChange = 5;//�ӵ������ٶ�
        this.shellDamage = 30; //������300 ��ͨ30
        this.fireInterval = 0.2f; //��������Сֵ


        this.InitDone = true; //��ʼ������


    }

    // Update is called once per frame
    //Horizontal1 ad
    //Vertical1  sw
    void Update()
    {
        TankMove(); //̹���ƶ�
        currentFireInterval -= Time.deltaTime; //�����ʱ��

        //��Ч��
        HpProtectEffect();


    }

    //���ܣ�̹���ƶ�
    private void TankMove()
    {
        h_Value = Input.GetAxis(inputHorizontalStr);
        v_Value = Input.GetAxis(inputVerticalStr);
        EngineAudio(h_Value, v_Value); //�ƶ���Ч

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

    //���ܣ�̹�˿���
    void OpenFire(float shellSpeed)
    {
        if (currentFireInterval < 0)
        {
            currentFireInterval = fireInterval;
        }
        else
        {
            print("cdʣ��"+(fireInterval - currentFireInterval));
            return;
        }
        GameObject shellObj = Instantiate(shell, shellPos.position, shellPos.transform.rotation);
        Rigidbody shellRigidbody = shellObj.GetComponent<Rigidbody>();
        if (shellRigidbody != null)
        {
            TankFireAudio();//������Ч
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

    //������ʼ��
    public void AudioStart()
    {
        m_TankAudio.clip = m_EngineDriving;
        m_TankAudio.spatialBlend = 0;
        m_TankAudio.pitch = 0.9f;
        m_TankAudio.loop = true;
        m_TankAudio.volume = 0;
        m_TankAudio.Play();

   
    }

    //�ƶ���Ч
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

    //������Ч
    private void TankFireAudio()
    {
        AudioSource.PlayClipAtPoint(m_TankFire, Camera.main.transform.position);
    }

    //��ȡ��ʼ��״̬
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
