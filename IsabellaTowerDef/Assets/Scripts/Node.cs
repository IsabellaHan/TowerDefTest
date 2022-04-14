using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [HideInInspector]
    public Color hoverCol;
    [HideInInspector]
    public Color clickCol = Color.black;
    [HideInInspector]
    public Color startCol;
    [HideInInspector]
    public MeshRenderer ren;
    [HideInInspector]
    public GameObject currentTur;



    private void Start()
    {
        ren = GetComponent<MeshRenderer>();
        startCol = ren.material.color;

    }

 
    

    private void OnMouseDown()
    {
        if (currentTur != null)
        {
            return;
        }
        else
        {
            GameManager.instance.node = this;
            ren.material.color = clickCol;
        }
    }


    private void OnMouseEnter()
    {
       
        ren.material.color = hoverCol;
    }
    private void OnMouseExit()
    {
      ren.material.color = startCol;
    }
}
