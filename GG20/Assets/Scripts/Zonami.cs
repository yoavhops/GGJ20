using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Zonami : Effect
{
    private const float MaxHeight = 0.1f;
    private const float EffectTime = 3f;
    private const int EffectDistance = 10;
    private const float EffectRingSize = 2.5f;
    private const float EffectValue = -0.3f;

    public Vector2Int OrigPos;

    public float timeWhenEffectStart;

    public List<Vector2Int> Offsets = new List<Vector2Int>();

    private float lastDistance = 0;

    public void Init(int x, int y)
    {
        OrigPos = new Vector2Int(x,y);
        timeWhenEffectStart = Time.time;
        transform.position = new Vector3(OrigPos.x, 0 ,OrigPos.y);
        gameObject.name = (this).name;
        
        for (int xd = -EffectDistance; xd <= EffectDistance; xd++)
        for (int yd = -EffectDistance; yd <= EffectDistance; yd++)
        {
            Offsets.Add(new Vector2Int(xd, yd));
        }

        Offsets = Offsets.OrderBy(item => Vector2Int.Distance(Vector2Int.zero, item)).ToList();

    }

    public override bool IsDone()
    {
        return (Time.time - timeWhenEffectStart) > EffectTime;
    }

    void Update()
    {
        var diffTimeNormalized = (Time.time - timeWhenEffectStart) / EffectTime;
        var bestDistance = diffTimeNormalized * EffectDistance;

        int i = 0;
        for (; i < Offsets.Count; i++)
        {
            var pos = new Vector2Int(OrigPos.x + Offsets[i].x, OrigPos.y + Offsets[i].y);
            var currentDistance = Vector2Int.Distance(pos, OrigPos);

            if (currentDistance > bestDistance)
                break;

            Act(pos);
        }

        if (i > 0)
            Offsets.RemoveRange(0, i);

        /*
        if (Math.Abs(bestDistance - currentDistance) < EffectRingSize)
        {
            return EffectValue;
        }*/
    }

    private void Act(Vector2Int pos)
    {
        GridManager.Singleton.TypeToDiffuse[]
    }


    public override float GetEffectValue(int x, int y, float origValue)
    {
        return 0;
    }
}
