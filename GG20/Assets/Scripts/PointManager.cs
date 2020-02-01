using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager singleton;

    private int points = 0;

    private UnityEngine.UI.Text txt;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        txt = transform.Find("Text").GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPoints(int pnts)
    {
        points = pnts;
        txt.text = "Points: " + pnts;
    }

    public void addPoints(int pnts)
    {
        setPoints(points + pnts);
    }

    public int getPoints()
    {
        return points;
    }
}
