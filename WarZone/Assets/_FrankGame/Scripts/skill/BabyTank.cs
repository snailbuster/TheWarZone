using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyTank : MonoBehaviour
{
    float skill_time = 7f; //技能发动周期
    private float tmp_skill_time;
    public EnemyTankControl etc;
    public Animator animator;
    private bool ani_play;
    // Start is called before the first frame update
    void Start()
    {
        this.tmp_skill_time = skill_time;
        animator.SetBool("PowerUp", false);

    }

    // Update is called once per frame
    void Update()
    {
        PowerUp();
    }

    void PowerUp()
    {
        tmp_skill_time -= Time.deltaTime;
        if (tmp_skill_time < 0)
        {
            
            etc.HP += 5;
            etc.shellDamage *= 1.1f;
            etc.speed *= 1.1f;
            etc.rotateSpeed *= 1.1f;

            tmp_skill_time = skill_time;
            //播放升级动画
            ani_play = true;
            animator.SetBool("PowerUp", true);
            
        } else if (tmp_skill_time < skill_time -1  && ani_play == true)
        {
            ani_play = false;
            animator.SetBool("PowerUp", false);
        }
        
    }
}
