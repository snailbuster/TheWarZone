using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


public class SteelTank : MonoBehaviour
{
    public EnemyTankControl steelTank;
    // Start is called before the first frame update
    void Start()
    {
        SteelBody();
        print(steelTank.HP);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void SteelBody()
    {
        steelTank.HP += 50;

    }

}
