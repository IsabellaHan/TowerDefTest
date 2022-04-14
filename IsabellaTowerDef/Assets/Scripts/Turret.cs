using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    AudioSource audio;
    public GameObject bullet;
    private Transform target;
    public string enemyTag = "Enemy";
    public float turnSpeed = 5.0f;
    public Transform turretHead;
    public Transform pointOfShot;
    public enum TurretColor { 
    Green, Gray
    }
    public TurretColor color;

    public enum DamageType { 
    Piercing,
    Splash
    }
    public DamageType dmgType;

    public enum Damage
    {
        Low,
        High 
    }
    public Damage damage;

    public enum Range
    {
        Low,
        High 
    }

    public Range range;
    public enum Speed
    {
        medium,
        fast
    }
    public Speed spd;

    float shotCounter = 0f;
    float timeBetweenShots= 2f;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        MeshRenderer[] ren = GetComponentsInChildren<MeshRenderer>();//GetComponent<MeshRenderer>();
        if (color == TurretColor.Gray) {
            foreach (MeshRenderer r in ren) {
                r.material.color = Color.gray;
            }
        } else if(color == TurretColor.Green){
            foreach (MeshRenderer r in ren)
            {
                r.material.color = Color.green;
            }
        }

        InvokeRepeating("UpdateTarget", 0f, 1.0f);
    }

    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject g in enemies)
        {
            float distancetoEnemy = Vector3.Distance(transform.position, g.transform.position);
            if (distancetoEnemy < shortestDistance) {
                shortestDistance = distancetoEnemy;
                nearestEnemy = g;
            }
        }

        if (nearestEnemy != null && shortestDistance <= GetRange(range)) {
            target = nearestEnemy.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) { return; }

        Vector3 dir = target.position - transform.position;
        Quaternion headRot = Quaternion.LookRotation(dir);
        Vector3 rot = Quaternion.Lerp(turretHead.rotation, headRot, Time.deltaTime * turnSpeed).eulerAngles;
        turretHead.rotation = Quaternion.Euler(0f, rot.y, 0f);

        if (shotCounter <= 0) {
            Shoot();
            shotCounter = timeBetweenShots;
        }

        shotCounter -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, GetRange(range));
    }

    public float GetDamage(Damage dam) {
        switch (dam)
        {
            case Damage.Low:
                return 1;
            case Damage.High:
                return 2;
        }
        return 0;
    }
    public float GetRange(Range ran)
    {
        switch (ran)
        {
            case Range.Low:
                return 15;
            case Range.High:
                return 25;
        }
        return 0;
    }
    public float GetSpeed(Speed s)
    {
        switch (s)
        {
            case Speed.medium:
                return 10;
            case Speed.fast:
                return 15;
        }
        return 0;
    }

    public DamageType GetDamageType()
    {
        return dmgType;
    }
    void Shoot() {

        GameObject go = (GameObject)Instantiate(bullet, pointOfShot.transform.position, pointOfShot.rotation);
        Projectile p = go.GetComponent<Projectile>();
        audio.Play(0);

        if (p != null) {
            p.FindTarget(target);
            p.shotTurret = this;
        }
    }
}
