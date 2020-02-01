using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Tree : DiffuseAble
{
    public float TreeMaxHeight = 0.8f;
    public float TreeMinHeight = 0.0f;
    public float TreeMaxTemp = 0.65f;
    public float TreeMinTemp = -0.4f;
    public float TreeMaxSalt = 0.5f;
    public float TreeMaxHydration = 0.8f;
    public float TreeMinHydration = -0.6f;
    
    public float CityMaxHeight = 0.9f;
    public float CityMinHeight = 0f;
    public float CityMaxTemp = 0.7f;
    public float CityMinTemp = -0.2f;
    public float CityMaxSalt = 0.65f;
    public float CityMaxHydration = 0.85f;
    public float CityMinHydration = -0.7f;

    public static float GrowthRate = 0.001f;

    public Tree(List<Vector2Int> positiveSources, List<Vector2Int> negativeSources):
        base(positiveSources, negativeSources)
    {
        
    }

    protected override void Diffuse(int x, int y)
    {


        base.Diffuse(x, y);

        var saltValue = GridManager.Singleton.TypeToDiffuse[typeof(SaltLevels)].GetGridValue(x, y);
        var tempValue = GridManager.Singleton.TypeToDiffuse[typeof(Temperature)].GetGridValue(x, y);
        var hydrationValue = GridManager.Singleton.TypeToDiffuse[typeof(Hydration)].GetGridValue(x, y);

        if (Grid[x].L[y] > 0.2)
        {
            var nutrientsValue = GridManager.Singleton.TypeToDiffuse[typeof(Nutrients)].GetGridValue(x, y);

            if (CanTreesGrow(x,y))
            {
                Grid[x].L[y] = Math.Min(Grid[x].L[y] + nutrientsValue * GrowthRate, 1);
            }
            

        }
        else if (Grid[x].L[y] < -0.2)
        {
            var nutrientsValue = GridManager.Singleton.TypeToDiffuse[typeof(Nutrients)].GetGridValue(x, y);

            if (CanCityGrow(x,y))
            Grid[x].L[y] = Math.Max(Grid[x].L[y] - nutrientsValue * GrowthRate, -1);
        }
    }

    private bool CanCityGrow(int x, int y)
    {
        var heightValue = GridManager.Singleton.TypeToDiffuse[typeof(Height)].GetGridValue(x, y);
        var saltValue = GridManager.Singleton.TypeToDiffuse[typeof(SaltLevels)].GetGridValue(x, y);
        var tempValue = GridManager.Singleton.TypeToDiffuse[typeof(Temperature)].GetGridValue(x, y);
        var hydrationValue = GridManager.Singleton.TypeToDiffuse[typeof(Hydration)].GetGridValue(x, y);


        return (heightValue >= CityMinHeight && heightValue <= CityMaxHeight && saltValue <= CityMaxSalt &&
            tempValue <= CityMaxTemp && tempValue >= CityMinTemp && hydrationValue <= CityMaxHydration
            && hydrationValue >= CityMinHydration);
    }

    private bool CanTreesGrow(int x, int y)
    {
        var heightValue = GridManager.Singleton.TypeToDiffuse[typeof(Height)].GetGridValue(x, y);
        var saltValue = GridManager.Singleton.TypeToDiffuse[typeof(SaltLevels)].GetGridValue(x, y);
        var tempValue = GridManager.Singleton.TypeToDiffuse[typeof(Temperature)].GetGridValue(x, y);
        var hydrationValue = GridManager.Singleton.TypeToDiffuse[typeof(Hydration)].GetGridValue(x, y);


        return (heightValue >= TreeMinHeight && heightValue <= TreeMaxHeight && saltValue <= TreeMaxSalt &&
            tempValue <= TreeMaxTemp && tempValue >= TreeMinTemp && hydrationValue <= TreeMaxHydration
            && hydrationValue >= TreeMinHydration);
    }

    protected IEnumerable<Vector2Int> GetNiePositions(int x, int y)
    {
        foreach (var offset in GetNieOffsets())
        {
            
            if (!GridManager.Singleton.InRange(offset.x + x, offset.y + y) ||
                GridManager.Singleton.TypeToDiffuse[typeof(Height)].GetGridValue(offset.x + x, offset.y + y) <= 0)
            {
                continue;
            }

            yield return new Vector2Int(offset.x + x, offset.y + y);
        }
    }

}
