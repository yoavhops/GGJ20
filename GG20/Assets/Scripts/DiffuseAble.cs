using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class DiffuseAble
{
    public List<SerList> Grid;
    public HashSet<Vector2Int> Sources;
    public List<Vector2Int> PositiveSources;
    public List<Vector2Int> NegativeSources;

    protected DiffuseAble(List<Vector2Int> positiveSources, List<Vector2Int> negativeSources)
    {
        PositiveSources = positiveSources;
        NegativeSources = negativeSources;

        Sources = new HashSet<Vector2Int>();
        Grid = new List<SerList>();
        for (var x = 0; x < GridManager.Singleton.Width; x++)
        {
            Grid.Add(new SerList());
            for (var y = 0; y < GridManager.Singleton.Height; y++)
            {
                Grid[x].L.Add(0);
            }
        }

        foreach (var source in PositiveSources)
        {
            Grid[source.x].L[source.y] = -1;
            Sources.Add(source);
        }

        foreach (var source in NegativeSources)
        {
            Grid[source.x].L[source.y] = 1;
            Sources.Add(source);
        }
    }

    public void FullDiffuse()
    {
        DiffuseFromUp();
        DiffuseFromDown();
    }

    void DiffuseFromUp()
    {
        for (var x = 0; x < GridManager.Singleton.Width; x++)
        for (var y = 0; y < GridManager.Singleton.Height; y++)
        {
            Diffuse(x, y);
        }
    }

    void DiffuseFromDown()
    {
        for (var x = GridManager.Singleton.Width - 1; x >= 0; x--)
        for (var y = GridManager.Singleton.Height - 1; y >= 0; y--)
        {
            Diffuse(x, y);
        }
    }

    protected virtual void Diffuse(int x, int y)
    {
        if (Sources.Contains(new Vector2Int(x, y)))
            return;

        var avg = 0f;
        foreach (var niePositions in GetNiePositions(x, y))
        {
            avg += GetGridValue(niePositions.x, niePositions.y);
        }
        avg = avg / GetNiePositions(x, y).Count();
        //avg *= DampFactor;
        Grid[x].L[y] = avg;
    }


    public float GetGridValue(int x, int y)
    {
        return Grid[x].L[y];
    }

    protected IEnumerable<Vector2Int> GetNiePositions(int x, int y)
    {
        foreach (var offset in GetNieOffsets())
        {
            if (!GridManager.Singleton.InRange(offset.x + x, offset.y + y))
            {
                continue;
            }

            yield return new Vector2Int(offset.x + x, offset.y + y);
        }
    }

    protected IEnumerable<Vector2Int> GetNieOffsets()
    {
        yield return new Vector2Int(0, 1);
        yield return new Vector2Int(0, -1);
        yield return new Vector2Int(1, 0);
        yield return new Vector2Int(-1, 0);
    }

}
