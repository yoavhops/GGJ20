using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

[Serializable]
public class DiffuseAble
{
    public List<SerList> Grid;
    public HashSet<Vector2Int> Sources;
    public List<Vector2Int> PositiveSources;
    public List<Vector2Int> NegativeSources;
    public float Damp = 1.7f;

    public List<Effect> Effects = new List<Effect>();

    protected DiffuseAble(List<Vector2Int> positiveSources, List<Vector2Int> negativeSources)
    {
        Debug.Log("Is parent first?");

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
            if (source.x < GridManager.Singleton.Width && source.y < GridManager.Singleton.Height)
            {
                Grid[source.x].L[source.y] = UnityEngine.Random.Range(0.2f, 1);
                Sources.Add(source);
            }
        }

        foreach (var source in NegativeSources)
        {
            if (source.x < GridManager.Singleton.Width && source.y < GridManager.Singleton.Height)
            {
                Grid[source.x].L[source.y] = -UnityEngine.Random.Range(0.2f, 1); ;
                Sources.Add(source);
            }
        }
    }

    public void SetValue(int x, int y, float value)
    {
        value = Mathf.Clamp(value, -1f, 1f);
        Grid[x].L[y] = value;
    }

    public void AddEffect(Effect effect)
    {
        Effects.Add(effect);
    }

    public float GetValueWithOutEffects(int x, int y)
    {
        return Grid[x].L[y];
    }

    public float GetValueWithEffects(int x, int y)
    {
        var ans = Grid[x].L[y];
        var origValue = ans;

        Effects.RemoveAll(effect =>
        {
            if (effect.IsDone())
            {
                GameObject.Destroy(effect.gameObject);
                return true;
            }

            return false;
        });

        foreach (var effects in Effects)
        {
            ans += effects.GetEffectValue(x, y, origValue);
        }

        return Math.Min(ans, 1);
    }

    public void AddEffectValue(Vector2Int pos, float delta)
    {
        if (Sources.Contains(pos))
            return;

        Grid[pos.x].L[pos.y] = Mathf.Clamp(Grid[pos.x].L[pos.y] + delta, -1, 1);
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
        var avg = 0f;
        var gave = 0f;
        foreach (var niePositions in GetNiePositions(x, y))
        {
            var sideDelta = Grid[x].L[y] - GetGridValue(niePositions.x, niePositions.y);
            sideDelta /= 4f;
            sideDelta *= Damp;
            gave += sideDelta;

            if (Sources.Contains(new Vector2Int(niePositions.x, niePositions.y)))
                continue;

            Grid[niePositions.x].L[niePositions.y] += sideDelta;
        }

        if (Sources.Contains(new Vector2Int(x, y)))
            return;

        Grid[x].L[y] -= gave;
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
