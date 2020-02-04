using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public GameObject TreePrefab;

    public void OnClick()
    {
        if(PointManager.singleton.getPoints() > 100)
        {
            Instantiate(TreePrefab);
        }
 
    }
}
