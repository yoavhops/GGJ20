﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Tree : DiffuseAble
{

    public Tree(List<Vector2Int> positiveSources, List<Vector2Int> negativeSources):
        base(positiveSources, negativeSources)
    {
        
    }

    protected override void Diffuse(int x, int y)
    {
        if (GridManager.Singleton.TypeToDiffuse[typeof(Height)].GetGridValue(x, y) <= 0)
        {
            return;
        }

        base.Diffuse(x, y);
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