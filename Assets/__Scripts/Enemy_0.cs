using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoundsCheck))]
public class Enemy_0 : Enemy {

    private Transform firePoint;

    public GameObject ProjectilePrefab;
    public float shotsPerSecond;

    private int count=50;
    private int fireTime;
    private void Start()
    {
        firePoint = transform.GetChild(0).GetChild(7);
        fireTime = (int)(50/shotsPerSecond);
    }

    void FixedUpdate()
    {
        Move();

        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown))
        {
            // We're off the bottom, so destroy this GameObject
            Destroy(gameObject);
        }
        count--;
        if(count==0)
        {
            Fire();
            count = fireTime;
        }

    }
    void Fire()
    {
        GameObject projectile = Instantiate(ProjectilePrefab);
        projectile.transform.position = firePoint.position;
        projectile.GetComponent<Rigidbody>().velocity = new Vector3(0, -20, 0);
    }

    
}