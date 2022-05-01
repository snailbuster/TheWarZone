using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTank : MonoBehaviour
{
    private float skill_cd = 1; 
    private float skill_time = 5;
    private float tmp_skill_cd;
    private float tmp_skill_time;
    private EnemyTankControl ghostTank;
    private GameObject tankchassis;
    public MeshRenderer mr1;
    public MeshRenderer mr2;
    public MeshRenderer mr3;
    public MeshRenderer mr4;
    // Start is called before the first frame update
    void Start()
    {
        //ghostTank = this.gameObject.GetComponent<EnemyTankControl>();
        this.tmp_skill_cd = skill_cd;
        this.tmp_skill_time = skill_time;

    }

    // Update is called once per frame
    void Update()
    {
        
        Invisible();
    }

    void Invisible()
    {
        if(tmp_skill_cd > 0)
        {
            tmp_skill_cd -= Time.deltaTime;
        }
        
        if (tmp_skill_cd < 0)
        {
            print("¼¼ÄÜÆô¶¯");
            mr1.enabled = false;
            mr2.enabled = false;
            mr3.enabled = false;
            mr4.enabled = false;
            tmp_skill_time -= Time.deltaTime;
            if(tmp_skill_time < 0)
            {
                tmp_skill_cd = skill_cd;
                tmp_skill_time = skill_time;
                mr1.enabled = true;
                mr2.enabled = true;
                mr3.enabled = true;
                mr4.enabled = true;

            }
        }
        
        
    }
}
