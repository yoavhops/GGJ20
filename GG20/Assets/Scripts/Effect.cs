using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Effect
{
    public virtual float GetEffectValueForColor(int x, int y, float origValue)
    {
        throw new NotImplementedException();
    }

    public virtual bool IsDone()
    {
        return true;
    }
}
