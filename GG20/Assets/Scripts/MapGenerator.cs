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


    public void updateTiles(List<SerList> lst, Color clrPos, Color clrNeg)
    {
        //float heightMult = (lst.Count + lst[0].L.Count) / 2 * heightScale;

        for (int x = 0; x < lst.Count; x++)
        {

            for (int y = 0; y < lst[x].L.Count; y++)
            {
                Tile currTile = tileList[x][y];

                //Tile tileHandler = currTile.GetComponent<Tile>();

                float currVal = lst[x].L[y];

                //float currHeight = mapHeight(currVal);
                Debug.Log("2gen tile " + x + "," + y + " val: " + currVal);

                currTile.setHeight(currVal);
                //currTile.setColor(currClr);
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
                tileHandler.posClr = clrPos;
                tileHandler.negClr = clrNeg;
                tileHandler.heightScale = (lst.Count + lst[0].L.Count) / 2 * heightScale;
                tileHandler.animTime = animTimeSec;
                tileHandler.clrMin = clrMin;
                tileHandler.mergeVal = mergeVal;
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
