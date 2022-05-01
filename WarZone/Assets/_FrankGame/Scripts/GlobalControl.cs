using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{
    public int lev_scene_num;
    public int lev_start_num;
    //私有

    private float endTime = 2f; //游戏结束等待时间
    private float endTime2 = 3f; //场景提示到场景的等待时间

    private float tmp_endTime; //计时器
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


    //敌军检测
    void HasEnemy()
    {
        Scene scene = SceneManager.GetActiveScene();
        print("scene.name.IndexOf"+ scene.name.IndexOf("TankGame"));
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && scene.name.IndexOf("TankGame") >=0)  
        {
            tmp_endTime -= Time.deltaTime;
            if (tmp_endTime < 0 && playing == true)
            {
                
                print("没有敌人了，切换场景" + lev);
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
            print("新的一关开始" + lev);
            AsyncOperation async_lev = SceneManager.LoadSceneAsync(lev_scene_num + lev);
            tmp_endTime = endTime;  //时间重置
            tmp_endTime2 = endTime2;
            
        }

    }

    
}
