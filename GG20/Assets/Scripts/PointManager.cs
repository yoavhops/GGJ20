using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager singleton;

    private int points = 0;
    private int score = 0;

    private UnityEngine.UI.Text cptxt;
    private UnityEngine.UI.Text scoretxt;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        cptxt = transform.Find("pntsTxt").GetComponent<UnityEngine.UI.Text>();
        scoretxt = transform.Find("scoreTxt").GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPoints(int pnts)
    {
        points = pnts;
        cptxt.text = "Carbon Points: " + pnts;

        scoretxt.text = "Score: " + score;
    }

    public void addPoints(int pnts)
    {
        if (pnts > 0)
        {
            score += pnts * Random.Range(1000, 1500);
        }
        setPoints(points + pnts);
    }

    public int getPoints()
    {
        return points;
    }
}
