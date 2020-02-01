using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private float timeSinceSet = 0;

    private float prevHeight = 0;
    private float? targetHeight = null;

    private float prevVal = 0;
    private float? targetVal = null;

    public Color hPosClr, hNegClr;
    public Color posClr, negClr;

    public float heightScale;

    public float animTime = 1;

    public float clrMin = 0.4f;

    public float mergeVal = 0.05f;

    private Transform cube;

    public void setPos(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    private float mapHeight(float height)
    {
        float newHeight;
        //float logBase = 10;
        //float newHeight = Mathf.Log(Mathf.Abs(height) * (logBase - 1) + 1, logBase) * Mathf.Sign(height);
        //newHeight = (newHeight + Mathf.Log(Mathf.Abs(1) * (logBase - 1) + 1, logBase)) / 2;
        //float newHeight = Mathf.Log(Mathf.Abs(height) * 1024, 2) / 10 * Mathf.Sign(height);
        if (height < 0)
        {
            newHeight = 0.5f;
        } else
        {
            newHeight = (1 - Mathf.Pow(1 - Mathf.Abs(height), 3)) * Mathf.Sign(height);
            newHeight = (newHeight + 1) / 2;

            //float newHeight = Mathf.Pow(height, 3);
            //float newHeight = height;
        }

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
        if (targetHeight != null)
        {
            prevHeight = targetHeight.Value;
        }
        targetHeight = height;
    }

    public void setVal(float val)
    {
        if (targetVal != null)
        {
            prevVal = targetVal.Value;
        }
        targetVal = val;
    }

    private void setHeightNow(float height)
    {
        transform.localScale = new Vector3(1, mapHeight(height), 1);
        //transform.localPosition = new Vector3(transform.localPosition.x, mapHeight(height), transform.localPosition.z);
        //updateClrByHeight(height);
    }

    private void setValNow(float val)
    {
       // updateClrByVal(val);
    }

    public void setColor(Color clr)
    {
        cube.GetComponent<Renderer>().material.SetColor("_Color", clr);
    }

    
    void Start()
    {
         cube = transform.Find("Cube");
    }

    private Color clrByValHeight(float val, float height)
    {
        Color clr = clrByVal(val, posClr, negClr);
        Color hclr = clrByVal(height, hPosClr, hNegClr);

        clr = (Mathf.Abs(val) * clr + (1 - Mathf.Abs(val)) * hclr) / 2;
        //if (targetVal != null && Mathf.Abs(targetVal.Value) < 0.2f)
        //{
        //    clr = hclr;
        //}

        return clr;
    }

    private Color clrByVal(float val, Color posClr, Color negClr)
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
            //clr = (clr * (1 - Mathf.Abs(val)) + (Mathf.Abs(val)) * hPosClr) / 2;
        }
        else
        {
            clr = negClr * (val * -1 * (1 - clrMin) + clrMin);
            //clr = (clr * (1-Mathf.Abs(val)) + (Mathf.Abs(val)) * hNegClr) / 2;
        }

        return clr;
    }

    private void updateClrByVal(float val, float height)
    {
        Color currClr = clrByValHeight(val, height);
        currClr.a = 1;
        setColor(currClr);
    }
    
    public void UpdateTile(float dt)
    {
        //float dt = Time.deltaTime;
        timeSinceSet += dt;
        float height = prevHeight;
        float val = prevVal;
        if (timeSinceSet >= animTime)
        {
            if (targetHeight != null)
            {
                setHeightNow(targetHeight.Value);
                prevHeight = targetHeight.Value;
                height = targetHeight.Value;
                targetHeight = null;
            }

            if (targetVal != null)
            {
                //setValNow(targetVal.Value);
                prevVal = targetVal.Value;
                val = targetVal.Value;
                targetVal = null;
            }
        }
        else
        {
            float percent = timeSinceSet / animTime;
            if (targetHeight != null)
            {
                height = prevHeight + (targetHeight.Value - prevHeight) * percent;
                setHeightNow(height);
            }

            if (targetVal != null)
            {
                val = prevVal + (targetVal.Value - prevVal) * percent;
                //setValNow(val);
            }
        }
        updateClrByVal(val, height);
    }
}
