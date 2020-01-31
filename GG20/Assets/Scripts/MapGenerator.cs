using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject tile;

    private float Width = 20;
    private float Height = 20;

    public float heightScale = 0.1f;
    public float clrMin = 0.4f;

    public float mergeVal = 0.05f;

    public float animTimeSec = 1f;

    private List<List<Tile>> tileList = null;

    List<SerList> currLst; //current heights
    List<SerList> targetLst; //heights to shift to

    private Color clrByVal(float val, Color clrPos, Color clrNeg)
    {
        Color clr;
        // clrPos * (currVal) + clrNeg*(1-currVal);
        if (Mathf.Abs(val) < mergeVal)
        {
            //between 
            clr = clrPos * (val/mergeVal * (1 - clrMin) + clrMin) + clrNeg * (val/mergeVal * -1 * (1 - clrMin) + clrMin);
            clr = clr / 2;
        }
        else if (val > 0)
        {
            clr = clrPos * (val * (1-clrMin) + clrMin);
        } else
        {
            clr = clrNeg * (val * -1 * (1 - clrMin) + clrMin);
        }

        return clr;
    }

    private float mapHeight(float height)
    {
        float logBase = 10;
        float newHeight = Mathf.Log(Mathf.Abs(height) * (logBase-1) + 1, logBase) * Mathf.Sign(height);
        newHeight = (newHeight + Mathf.Log(Mathf.Abs(1) * (logBase - 1) + 1, logBase)) / 2;

        //float newHeight = Mathf.Pow(height, 3);

        if (newHeight == 0)
        {
            newHeight = 0.001f;
        }

        return newHeight;
    }

    public void updateTiles(List<SerList> lst, Color clrPos, Color clrNeg)
    {
        float heightMult = (lst.Count + lst[0].L.Count) / 2 * heightScale;

        for (int x = 0; x < lst.Count; x++)
        {

            for (int y = 0; y < lst[x].L.Count; y++)
            {
                Tile currTile = tileList[x][y];

                //Tile tileHandler = currTile.GetComponent<Tile>();

                float currVal = lst[x].L[y];

                Color currClr = clrByVal(currVal, clrPos, clrNeg);

                float currHeight = mapHeight(currVal);


                currClr.a = 1;
                Debug.Log("2gen tile " + x + "," + y + " h: " + currHeight + " clr: " + currClr);

                currTile.setHeight(currHeight * heightMult);
                currTile.setColor(currClr);
            }
        }
    }

    public void genTiles(List<SerList> lst, Color clrPos, Color clrNeg)
    {
        tileList = new List<List<Tile>>();
        for (int x=0; x<lst.Count; x++)
        {
            tileList.Add(new List<Tile>());
            for (int y=0; y<lst[x].L.Count; y++)
            {
                GameObject currTile = Instantiate(tile);
                Tile tileHandler = currTile.GetComponent<Tile>();
                currTile.transform.parent = transform;
                tileHandler.setPos(new Vector3(x, 0, y));
                tileList[x].Add(tileHandler);
            }
        }
    }

    public void setTiles(List<SerList> lst, Color clrPos, Color clrNeg)
    {
        if (tileList == null)
        {
            genTiles(lst, clrPos, clrNeg);
        }

        updateTiles(lst, clrPos, clrNeg);
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        List<List<float>> tiles = new List<List<float>>();
        for (var x = 0; x < Width; x++)
        {
            tiles.Add(new List<float>());
            for (var y = 0; y < Height; y++)
            {
                tiles[x].Add(Random.Range(-1f, 1f));
            }
        }
        */
        
        //genTiles(GridManager.Singleton.Grid, new Color(0.8f, 0.2f, 0.1f), new Color(0,0,1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
