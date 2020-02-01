using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temperature : DiffuseAble
{
    public Temperature(List<Vector2Int> positiveSources, List<Vector2Int> negativeSources) :
       base(positiveSources, negativeSources)
    {
    }
}
