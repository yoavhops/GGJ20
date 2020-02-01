using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltLevels : DiffuseAble
{
    public SaltLevels(List<Vector2Int> positiveSources, List<Vector2Int> negativeSources) :
    base(positiveSources, negativeSources)
    {
        var height = GridManager.Singleton.TypeToDiffuse[typeof(Height)];
        for (var x = 0; x < GridManager.Singleton.Width; x++)
        {
            for (var y = 0; y < GridManager.Singleton.Height; y++)
            {
                var heightValue = height.GetGridValue(x, y);
                if(heightValue < 0)
                {
                    negativeSources.Add(new Vector2Int(x, y));
                }

                if (heightValue > 0.5)
                {
                    positiveSources.Add(new Vector2Int(x, y));
                }
            }
        }

    }

    protected override void Diffuse(int x, int y)
    {
        base.Diffuse(x, y);
    }
}
