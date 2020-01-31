using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Singleton;
    public int Width = 100;
    public int Height = 100;
    public float[,] Grid;

    void Awake()
    {
        Singleton = this;
        Grid = new float[Width, Height];

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
