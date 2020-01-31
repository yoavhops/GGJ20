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
    public List<Vector2Int> WaterSources;
    public List<Vector2Int> MountainSources;
    public List<Vector2Int> TreeSources;
    public List<Vector2Int> CitySources;


    public int StartIteration = 50;
    //public float DampFactor = 0.9f;

    private MapGenerator mapGen;
    
    void Awake()
    {
        Singleton = this;
        TypeToDiffuse[typeof(Height)] = new Height(WaterSources, MountainSources);
        DebugDiffuseAble.Add(TypeToDiffuse[typeof(Height)]);
        TypeToDiffuse[typeof(Tree)] = new Tree(CitySources, TreeSources);
        DebugDiffuseAble.Add(TypeToDiffuse[typeof(Tree)]);
    }
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < StartIteration; i++)
        {
            TypeToDiffuse[typeof(Height)].FullDiffuse();
        }

        mapGen = GameObject.Find("/Map").GetComponent<MapGenerator>();
        mapGen.updateMap();
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
