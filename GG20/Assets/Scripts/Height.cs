using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Height : DiffuseAble
{

    public Height(List<Vector2Int> positiveSources, List<Vector2Int> negativeSources):
        base(positiveSources, negativeSources)
    {
        
    }

}
