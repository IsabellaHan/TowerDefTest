using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public Turret shotTurret;
    private Transform target;
    public GameObject impactObj;
    public float dmg;

    private void Start()
    {
        dmg = shotTurret.GetDamage(shotTurret.damage);
        //Debug.Log(dmg);
    }
    public void FindTarget(Transform t) {
        target = t;
    }

    private void Update()
    {
        
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distance = shotTurret.GetSpeed(shotTurret.spd) * Time.deltaTime;

        if (dir.magnitude <= distance) {
            GameObject fxObj = Instantiate(impactObj,transform.position, transform.rotation);
            Destroy(fxObj, 1.5f);
            Destroy(gameObject);
            return;
        }

        transform.Translate ( dir.normalized * distance, Space.World);
    }


    public void AOEDamage() { 
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5.0f);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Enemy") {
                Enemy e = hitCollider.gameObject.GetComponent<Enemy>();
                e.health = e.health - dmg;
            }
            Debug.Log(hitCollider);
        }
    }

}
