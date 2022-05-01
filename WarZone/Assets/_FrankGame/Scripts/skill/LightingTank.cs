using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingTank : MonoBehaviour
{
    // Start is called before the first frame update

    public EnemyTankControl lightingTank;
    void Start()
    {
        lightingTank.speed = lightingTank.speed + lightingTank.speed * 1.5f;
        lightingTank.currentShellSpeed *= 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
