using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTilePlacer : MonoBehaviour
{
    private GameObject hitGO = null;

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Cube").GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 1, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = LayerMask.GetMask("tiles");
        RaycastHit[] hits = Physics.RaycastAll(ray, 100.0f, mask);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                hitGO = hit.transform.gameObject;
                transform.position = new Vector3(hitGO.transform.position.x, hitGO.transform.lossyScale.y, hitGO.transform.position.z);
            }
        }

        if (hitGO != null && Input.GetMouseButtonDown(0))
        {
            Vector2Int pos = new Vector2Int((int)hitGO.transform.parent.position.x, (int)hitGO.transform.parent.position.z);
            Debug.Log("Adding source at: " + pos);
            if (((Tree)GridManager.Singleton.TypeToDiffuse[typeof(Tree)]).CanTreesGrow(pos.x, pos.y))
            {
                ((Tree)GridManager.Singleton.TypeToDiffuse[typeof(Tree)]).addSource(pos);
                Destroy(gameObject);
            }
        }
    }
}
