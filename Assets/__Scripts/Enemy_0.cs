using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoundsCheck))]
public class Enemy_0 : Enemy {

    private Transform firePoint;

    public GameObject ProjectilePrefab;

    private int count = 0;
    private void Start()
    {


        firePoint = transform.GetChild(0).GetChild(7);





    }

    void FixedUpdate()
    {
        Move();

        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown))
        {
            // We're off the bottom, so destroy this GameObject
            Destroy(gameObject);
        }
        count++;
        if(count==100)
        {
            Fire();
            count = 1;
        }

    }
    void Fire()
    {
        GameObject projectile = Instantiate(ProjectilePrefab);
        projectile.transform.position = firePoint.position;
        projectile.GetComponent<Rigidbody>().velocity = new Vector3(0, -20, 0);
    }

    
}
