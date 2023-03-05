using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoundsCheck))]
public class Enemy_0 : Enemy {


    [Header("Dynamic")]
    [SerializeField]
    static public Transform PROJECTILE_ANCHOR;
    private eWeaponType _type = eWeaponType.blaster;
    public WeaponDefinition def;
    public GameObject collar;
    public float nextShotTime; // Time last shot was fired
    public GameObject weaponModel;
    private Transform shotPointTransform;


    private void Start()
    {
        //collar = transform.Find("Collar").gameObject;
        //collarRend = collar.GetComponent<Renderer>();

        // Dynamically create an anchor for all Projectiles
        if (PROJECTILE_ANCHOR == null)
        {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }

        shotPointTransform = transform.GetChild(0);  //There is only one child and it is what we need

        


        
    }

    void FixedUpdate(){
        //Vector3 vel = Vector3.down * def.velocity;
        ProjectileHero p = MakeProjectile();
        p.vel = new Vector3(0,0,-20);
    }
     public eWeaponType type
    {
        get
        {
            return (_type);
        }
        set
        {
            SetType(value);
        }
    }

    public void SetType(eWeaponType wt)
    {
        _type = wt;
        if (type == eWeaponType.none)
        {
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }

        //Get the weapon def from main
        def = Main.GET_WEAPON_DEFINITION(_type);

        //Destroy any old model and then attach a model for this weapon type
        if (weaponModel != null) Destroy(weaponModel);
        weaponModel = Instantiate<GameObject>(def.weaponModelPrefab, transform);
        weaponModel.transform.localPosition = Vector3.zero;
        weaponModel.transform.localScale = Vector3.one;

        nextShotTime = 0; // You can fire immediately after _type is set.
    }


    public ProjectileHero MakeProjectile()
    {
        GameObject go = Instantiate<GameObject>(def.weaponModelPrefab, PROJECTILE_ANCHOR);
        ProjectileHero p = go.GetComponent<ProjectileHero>();

        Vector3 pos = shotPointTransform.position;
        pos.z = 0;                                                           
        p.transform.position = pos;

        p.type = type;
        nextShotTime = Time.time + def.delayBetweenShots;                    
        return (p);
    }
}
