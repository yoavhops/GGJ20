using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutrients : DiffuseAble
{

    public Nutrients(List<Vector2Int> positiveSources, List<Vector2Int> negativeSources) :
        base(positiveSources, negativeSources)
    {

    }

}
