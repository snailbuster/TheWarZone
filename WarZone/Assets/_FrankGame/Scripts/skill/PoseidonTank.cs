using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonTank : MonoBehaviour
{
    public EnemyTankControl poseidon;
    public Transform shellPos1;
    public Transform shellPos2;
    public GameObject shell;
    // Start is called before the first frame update
    void Start()
    {
        PoseidonTank pst = this.gameObject.GetComponent<PoseidonTank>();
        

    }

    // Update is called once per frame
    void Update()
    {
        OpenFire();
    }

    void OpenFire()
    {
        if (poseidon.poseidon_fire == true)
        {
            
            GameObject shellObj = Instantiate(shell, shellPos1.position, shellPos1.transform.rotation);
            Rigidbody shellRigidbody = shellObj.GetComponent<Rigidbody>();
            shellRigidbody.velocity = shellPos1.forward * poseidon.currentShellSpeed;

            GameObject shellObj2 = Instantiate(shell, shellPos2.position, shellPos2.transform.rotation);
            Rigidbody shellRigidbody2 = shellObj2.GetComponent<Rigidbody>();
            shellRigidbody2.velocity = shellPos2.forward * poseidon.currentShellSpeed;
            poseidon.poseidon_fire = false;

        }
    }
}
