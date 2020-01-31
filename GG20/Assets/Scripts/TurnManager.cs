using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public float TreeOffset = 1f;
    public float TreeTime = 1f;
    private float currentTreeTime = 1f;

    void Start()
    {
        currentTreeTime = TreeTime + TreeOffset;
    }
    // Update is called once per frame
    void Update()
    {
        currentTreeTime -= Time.deltaTime;

        if (currentTreeTime <= 0)
        {
            currentTreeTime += TreeTime;
            GridManager.Singleton.TypeToDiffuse[typeof(Tree)].FullDiffuse();
        }

    }
}
