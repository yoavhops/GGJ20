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
    public List<SerList> Grid;
    public HashSet<Vector2Int> Sources;
    public List<Vector2Int> WaterSources;
    public List<Vector2Int> MountainSources;
    
    void Awake()
    {
        Singleton = this;
        Grid = new List<SerList>();
        for (var x = 0; x < Width; x++)
        {
            Grid.Add(new SerList());
            for (var y = 0; y < Height; y++)
            {
                Grid[x].L.Add(0);
            }
        }
        Sources = new HashSet<Vector2Int>();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        foreach (var source in WaterSources)
        {
            Grid[source.x].L[source.y] = -1;
            Sources.Add(source);
        }

        foreach (var source in MountainSources)
        {
            Grid[source.x].L[source.y] = 1;
            Sources.Add(source);
        }

        for (int i = 0; i < 50; i++)
        {
            DiffuseFromUp();
            DiffuseFromDown();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DiffuseFromUp()
    {
        for (var x = 0; x < Width; x++)
        for (var y = 0; y < Height; y++)
        {
            Diffuse(x, y);
        }
    }

    void DiffuseFromDown()
    {
        for (var x = Width - 1; x >= 0; x--)
        for (var y = Height - 1; y >= 0; y--)
        {
            Diffuse(x, y);
        }
    }

    void Diffuse(int x, int y)
    {
        if (Sources.Contains(new Vector2Int(x, y)))
            return;

        var avg = 0f;
        foreach (var niePositions in GetNiePositions(x, y))
        {
            avg += GetGridValue(niePositions.x, niePositions.y);
        }
        avg = avg / GetNiePositions(x, y).Count();
        Grid[x].L[y] = avg;
    }


    float GetGridValue(int x, int y)
    {
        if (!InRange(x,y))
        {
            return 0;
        }

        return Grid[x].L[y];
    }

    private bool InRange(int x, int y)
    {
        return !(x < 0 || x >= Width || y < 0 || y >= Height);
    }


    IEnumerable<Vector2Int> GetNiePositions(int x, int y)
    {
        foreach (var offset in GetNieOffsets())
        {
            if (!InRange(x, y))
            {
                continue;
            }

            yield return new Vector2Int(offset.x + x, offset.y + y);
        }
    }

    IEnumerable<Vector2Int> GetNieOffsets()
    {
        yield return new Vector2Int(0, 1);
        yield return new Vector2Int(0, -1);
        yield return new Vector2Int(1, 0);
        yield return new Vector2Int(-1, 0);
    }


}
