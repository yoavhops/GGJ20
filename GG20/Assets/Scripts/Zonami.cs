using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Zonami : Effect
{
    private const float MaxHeight = 0.2f;
    private const float EffectTime = 3f;
    private const float EffectDistance = 10f;
    private const float EffectRingSize = 2.5f;
    private const float EffectValue = 0.3f;

    public readonly Vector2Int OrigPos;

    public float timeWhenEffectStart;

    public Zonami(int x, int y)
    {
        OrigPos = new Vector2Int(x,y);
        timeWhenEffectStart = Time.time;
    }

    public override bool IsDone()
    {
        return (Time.time - timeWhenEffectStart) > EffectTime;
    }

    public override float GetEffectValueForColor(int x, int y, float origValue)
    {
        if (origValue > MaxHeight)
            return 0;

        var diffTimeNormalized  = (Time.time - timeWhenEffectStart) / EffectTime;

        var bestDistance = diffTimeNormalized * EffectDistance;

        var currentDistance = Vector2Int.Distance(new Vector2Int(x, y), OrigPos);

        if (Math.Abs(bestDistance - currentDistance) < EffectRingSize)
        {
            return EffectValue;
        }

        return 0;
    }
}
