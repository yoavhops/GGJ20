﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydration : DiffuseAble
{
    public Hydration(List<Vector2Int> positiveSources, List<Vector2Int> negativeSources) :
        base(positiveSources, negativeSources)
    {
        var height = GridManager.Singleton.TypeToDiffuse[typeof(Height)];
        for (var x = 0; x < GridManager.Singleton.Width; x++)
        {
            for (var y = 0; y < GridManager.Singleton.Height; y++)
            {
                var heightValue = height.GetGridValue(x, y);
                if (heightValue < 0)
                {
                    SetValue(x, y, 0.8f);
                }

                if (heightValue > 0.8)
                {
                    SetValue(x, y, -0.8f);
                }
            }
        }
    }
}
