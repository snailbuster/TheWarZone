using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeTank : MonoBehaviour
{
    public EnemyTankControl mytank;
    public GameObject split_tank;//分裂内容
    private int split_times = 1;//分裂次数
    private int split_nums = 3; //分裂数量
    private float x;
    private float y;
    private float z;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TankSplit();
    }

    void TankSplit()
    {
        if (mytank.HP < 0 && split_times >0 )
        {
            print("橙子技能发动");

            split_times -= 1;
            for (int i =0;i <= split_nums; i++)
            {
                float random_num = Random.Range(-10, 10);
                x = this.gameObject.transform.position.x + random_num;
                y = this.gameObject.transform.position.y;
                z = this.gameObject.transform.position.z + random_num;
                Vector3 position = new Vector3(x, y, z);
                GameObject tankOne = Instantiate(split_tank, position, this.gameObject.transform.rotation);
            }

        }
    }
}
