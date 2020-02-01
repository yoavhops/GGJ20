using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SerList
{
    public List<float> L = new List<float>();
}

public class GridManager : MonoBehaviour
{
    public static GridManager Singleton;
    public int Width = 100;
    public int Height = 100;
    public Dictionary<Type, DiffuseAble> TypeToDiffuse = new Dictionary<Type, DiffuseAble>();
    public List<DiffuseAble> DebugDiffuseAble = new List<DiffuseAble>();
    public GameObject WaterSources;
    public GameObject MountainSources;
    public List<Vector2Int> TreeSources;
    public List<Vector2Int> CitySources;


    public int StartIteration = 50;
    //public float DampFactor = 0.9f;

    private MapGenerator mapGen;
    
    void Awake()
    {
        Singleton = this;
        TypeToDiffuse[typeof(Height)] = new Height(GetPositions(WaterSources).ToList(), GetPositions(MountainSources).ToList());
        DebugDiffuseAble.Add(TypeToDiffuse[typeof(Height)]);
        TypeToDiffuse[typeof(Tree)] = new Tree(CitySources, TreeSources);
        DebugDiffuseAble.Add(TypeToDiffuse[typeof(Tree)]);
    }

    private IEnumerable<Vector2Int> GetPositions(GameObject parant)
    {
        for (int i = 0 ; i < parant.transform.childCount; i++)
        {
            var child = parant.transform.GetChild(i);
            yield return new Vector2Int(Mathf.RoundToInt(child.localPosition.x), Mathf.RoundToInt(child.localPosition.z));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < StartIteration; i++)
        {
            TypeToDiffuse[typeof(Height)].FullDiffuse();
        }

        mapGen = GameObject.Find("/Map").GetComponent<MapGenerator>();
        mapGen.updateMap(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool InRange(int x, int y)
    {
        return !(x < 0 || x >= GridManager.Singleton.Width || y < 0 || y >= GridManager.Singleton.Height);
    }

}
