using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{
    public int lev_scene_num;
    public int lev_start_num;
    //˽��

    private float endTime = 2f; //��Ϸ�����ȴ�ʱ��
    private float endTime2 = 3f; //������ʾ�������ĵȴ�ʱ��

    private float tmp_endTime; //��ʱ��
    private float tmp_endTime2;

    private int lev = 0; 
    private bool playing = true;

    

    void Start()
    {
        this.lev_scene_num = 3; 
        this.lev_start_num = 8;
        this.tmp_endTime = endTime;
        this.tmp_endTime2 = endTime2;
    }

    // Update is called once per frame
    void Update()
    {
        if(playing == true)
        {
            HasEnemy();
        }else
        {
            NextLev();
        }
    }

    //IEnumerator Timer()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(1.0f);
    //        timer += 1;
    //        Debug.Log(string.Format("Timer2 is up !!! time=${0}", Time.time));
    //    }
    //}


    //�о����
    void HasEnemy()
    {
        Scene scene = SceneManager.GetActiveScene();
        print("scene.name.IndexOf"+ scene.name.IndexOf("TankGame"));
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && scene.name.IndexOf("TankGame") >=0)  
        {
            tmp_endTime -= Time.deltaTime;
            if (tmp_endTime < 0 && playing == true)
            {
                
                print("û�е����ˣ��л�����" + lev);
                playing = false;
                AsyncOperation async_lev_start = SceneManager.LoadSceneAsync(lev_start_num+lev);

            }
       
        }
        
    }

    void NextLev()
    {
        tmp_endTime2 -= Time.deltaTime;
        if (tmp_endTime2 < 0 )
        {
            playing = true;
            lev += 1;
            print("�µ�һ�ؿ�ʼ" + lev);
            AsyncOperation async_lev = SceneManager.LoadSceneAsync(lev_scene_num + lev);
            tmp_endTime = endTime;  //ʱ������
            tmp_endTime2 = endTime2;
            
        }

    }

    
}
