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

    public List<Vector2Int> ColdSources;
    public List<Vector2Int> HotSources;

    public List<Vector2Int> SaltSources;
    public List<Vector2Int> NoSaltSources;

    public List<Vector2Int> NutrientsSources;
    public List<Vector2Int> NoNutrientsSource;

    public List<Vector2Int> HydrationSources;
    public List<Vector2Int> DrySource;

    public int StartIteration = 50;
    //public float DampFactor = 0.9f;

    private MapGenerator mapGen;
    
    void Awake()
    {
        Singleton = this;
        TypeToDiffuse[typeof(Height)] = new Height(GetPositions(WaterSources).ToList(), GetPositions(MountainSources).ToList());
        DebugDiffuseAble.Add(TypeToDiffuse[typeof(Height)]);
        InitializeMap(TypeToDiffuse[typeof(Height)]);

        TypeToDiffuse[typeof(SaltLevels)] = new SaltLevels(NoSaltSources, SaltSources);
        DebugDiffuseAble.Add(TypeToDiffuse[typeof(SaltLevels)]);
        InitializeMap(TypeToDiffuse[typeof(SaltLevels)], 5);

        TypeToDiffuse[typeof(Temperature)] = new Temperature(HotSources, ColdSources);
        DebugDiffuseAble.Add(TypeToDiffuse[typeof(Temperature)]);
        InitializeMap(TypeToDiffuse[typeof(Temperature)]);

        TypeToDiffuse[typeof(Hydration)] = new Hydration(HydrationSources, DrySource);
        DebugDiffuseAble.Add(TypeToDiffuse[typeof(Hydration)]);
        InitializeMap(TypeToDiffuse[typeof(Hydration)]);

        TypeToDiffuse[typeof(Nutrients)] = new Nutrients(NutrientsSources, NoNutrientsSource);
        DebugDiffuseAble.Add(TypeToDiffuse[typeof(Nutrients)]);
        InitializeMap(TypeToDiffuse[typeof(Nutrients)]);

        TypeToDiffuse[typeof(Tree)] = new Tree(TreeSources, CitySources);
        DebugDiffuseAble.Add(TypeToDiffuse[typeof(Tree)]);
    }

    private void InitializeMap(DiffuseAble typeToInitialize, int iterations = 50)
    {
        for (int i = 0; i < iterations; i++)
        {
            typeToInitialize.FullDiffuse();
        }
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
            TypeToDiffuse[typeof(SaltLevels)].FullDiffuse();
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
