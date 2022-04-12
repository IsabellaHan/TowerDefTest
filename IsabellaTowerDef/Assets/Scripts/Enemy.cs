using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health;
    public float speed;
    public Color color;

    private Transform target;
    private int currentWPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer ren = GetComponent<MeshRenderer>();
        ren.material.color = color;

        target = Waypoints.waypoints[0];

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint() {
        if (currentWPoint >= Waypoints.waypoints.Length - 1)
        {
            Player.lives--;
            Destroy(gameObject);
        }

        currentWPoint++;
        target = Waypoints.waypoints[currentWPoint];
    }
}
