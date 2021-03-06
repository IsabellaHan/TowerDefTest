using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    string projTag = "Projectile";
    public float health;
    public enum Speed
    {
        slow,
        fast
    }
    public Speed spd;

    public GameObject deathParticles;
    float deathParticleTimer = 0.7f;
    public enum EnemyColor
    {
        yellow, red
    }
    public EnemyColor color;

    private Transform target;
    private int currentWPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer ren = GetComponent<MeshRenderer>();
        if (color == EnemyColor.yellow) {
            ren.material.color = Color.yellow;
        }
        else if (color == EnemyColor.red) {
            ren.material.color = Color.red;
        }

        //if (Waypoints.waypoints.Length != 0) // if we were using waypoints
        //{
        //    target = Waypoints.waypoints[0];
        //}
        //else { 
        target = FindObjectOfType<Player>().transform; 
       // }

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * GetSpeed(spd) * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) {
            GetNextWayPoint();
        }

        if (health <= 0) {
            GameManager.instance.enemyCounter++;
            Destroy(gameObject);
        }

       // Debug.Log(health);
    }

    void GetNextWayPoint() {

        currentWPoint++;
        target = Waypoints.waypoints[currentWPoint];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag==projTag)
        {
            Turret tu = other.gameObject.GetComponent<Projectile>().shotTurret;
            Projectile po = other.gameObject.GetComponent<Projectile>();
            if (tu.tag == "Arrow")
            {
                health = health - other.gameObject.GetComponent<Projectile>().dmg;
            }
            else if (tu.tag == "Cannon") {
                po.AOEDamage();
            }

            GameObject fxObj = Instantiate(deathParticles, transform.position, transform.rotation);
            Destroy(fxObj, deathParticleTimer);

        }
    }

    public float GetSpeed(Speed s)
    {
        switch (s)
        {
            case Speed.slow:
                return 5;
            case Speed.fast:
                return 10;
        }
        return 0;
    }

    private void OnDestroy()
    {
        GameManager.instance.listOfEnemy.Remove(this.gameObject);
    }
}
