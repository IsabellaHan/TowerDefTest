using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    AudioSource audio;
    public bool isPlaced = false;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") {
            GameManager.instance.lives--;
            GameManager.instance.enemyCounter++;
            audio.Play(0);
            Destroy(other.gameObject);
        }
       
        
    }

}
