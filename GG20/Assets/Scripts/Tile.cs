using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public void setPos(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    public void setHeight(float height)
    {
        transform.localScale = new Vector3(1, height, 1);
    }

    public void setColor(Color clr)
    {
        Transform cube = transform.Find("Cube");
        cube.GetComponent<Renderer>().material.SetColor("_Color", clr);
    }

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
