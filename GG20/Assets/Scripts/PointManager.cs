using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager singleton;

    private int points = 0;
    private int score = 0;

    public UnityEngine.UI.Text cptxt;
    public UnityEngine.UI.Text scoretxt;

    private void Awake()
    {
        singleton = this;
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
