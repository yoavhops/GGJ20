using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public delegate void ClickDelegate();
 
    Dictionary<GameObject, ClickDelegate> clickHandlers = new Dictionary<GameObject, ClickDelegate>();

    public static ClickManager singleton;

    public void registerClickHandler(GameObject go, ClickDelegate handler)
    {
        //Debug.Log("registered" + go.name);
        clickHandlers.Add(go, handler);
        foreach (Transform child in go.transform)
        {
            registerClickHandler(child.gameObject, handler);
        }

    }

    void Start()
    {
        singleton = this;
    }

    void Update()
    {

        Vector3? clickPos = null;
        if (Input.GetMouseButtonDown(0))
        {
            clickPos = Input.mousePosition;
        }

        if (clickPos != null)
        {
            //Debug.Log("click");
            
            Ray ray = Camera.main.ScreenPointToRay(clickPos.Value);
            LayerMask mask = LayerMask.GetMask("carbonPoints");
            RaycastHit[] hits = Physics.RaycastAll(ray, 100.0f, mask);
            if (hits.Length > 0)
            {
                for (int i=0; i< hits.Length; i++)
                {
                    RaycastHit hit = hits[i];
                    GameObject hitGO = hit.transform.gameObject;
                    Debug.Log("hit " + hitGO.name);
                    if (clickHandlers.ContainsKey(hitGO))
                    {
                        clickHandlers[hitGO]();
                    }
                }
            }
        }
    }
}
