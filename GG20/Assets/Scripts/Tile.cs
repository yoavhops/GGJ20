using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private float timeSinceSet = 0;

    private float prevHeight = 0;
    private float? targetHeight = null;

    public Color posClr, negClr;

    public float heightScale;

    public float animTime = 1;

    public float clrMin = 0.4f;

    public float mergeVal = 0.05f;

    public void setPos(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    private float mapHeight(float height)
    {
        //float logBase = 10;
        //float newHeight = Mathf.Log(Mathf.Abs(height) * (logBase - 1) + 1, logBase) * Mathf.Sign(height);
        //newHeight = (newHeight + Mathf.Log(Mathf.Abs(1) * (logBase - 1) + 1, logBase)) / 2;
        //float newHeight = Mathf.Log(Mathf.Abs(height) * 1024, 2) / 10 * Mathf.Sign(height);

        float newHeight = (1 - Mathf.Pow(1 - Mathf.Abs(height), 3)) * Mathf.Sign(height);
        newHeight = (newHeight + 1) / 2;

        //float newHeight = Mathf.Pow(height, 3);
        //float newHeight = height;
        newHeight *= heightScale;

        if (newHeight < 0.001f)
        {
            newHeight = 0.001f;
        }

        return newHeight;
    }

    public void setHeight(float height)
    {
        timeSinceSet = 0;
        targetHeight = height;
    }

    private void setHeightNow(float height)
    {
        transform.localScale = new Vector3(1, mapHeight(height), 1);
        //transform.localPosition = new Vector3(transform.localPosition.x, mapHeight(height), transform.localPosition.z);
        updateClrByHeight(height);
    }

    public void setColor(Color clr)
    {
        Transform cube = transform.Find("Cube");
        cube.GetComponent<Renderer>().material.SetColor("_Color", clr);
    }

    
    void Start()
    {
        
    }

    private Color clrByVal(float val)
    {
        Color clr;
        // clrPos * (currVal) + clrNeg*(1-currVal);
        if (Mathf.Abs(val) < mergeVal)
        {
            //between 
            clr = posClr * (val / mergeVal * (1 - clrMin) + clrMin) + negClr * (val / mergeVal * -1 * (1 - clrMin) + clrMin);
            clr = clr / 2;
        }
        else if (val > 0)
        {
            clr = posClr * (val * (1 - clrMin) + clrMin);
        }
        else
        {
            clr = negClr * (val * -1 * (1 - clrMin) + clrMin);
        }

        return clr;
    }

    private void updateClrByHeight(float val)
    {
        Color currClr = clrByVal(val);
        currClr.a = 1;
        setColor(currClr);
    }
    
    void Update()
    {
        float dt = Time.deltaTime;
        timeSinceSet += dt;
        if (targetHeight != null)
        {
            if (timeSinceSet >= animTime)
            {
                setHeightNow(targetHeight.Value);
                prevHeight = targetHeight.Value;
                targetHeight = null;
            }
            else
            {
                float percent = timeSinceSet / animTime;
                setHeightNow(prevHeight + (targetHeight.Value-prevHeight)*percent);
            }
        }
    }
}
