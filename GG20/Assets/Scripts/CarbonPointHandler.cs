using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarbonPointHandler : MonoBehaviour
{
    private void onClick()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        ClickManager.singleton.registerClickHandler(gameObject, onClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
