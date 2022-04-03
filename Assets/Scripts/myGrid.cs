using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 网格
/// </summary>
public class Grid
{
    /// <summary>
    /// 坐标点
    /// </summary>
    public Vector2 Point;
    /// <summary>
    /// 世界坐标
    /// </summary>
    public Vector2 Position;


    public bool HavePlant;

    private PlantBase currPlantBase;
    public Grid(Vector2 point, Vector2 position, bool haveplant)
    {
        Point = point;
        Position = position;
        HavePlant = haveplant;
    }

    public PlantBase CurrPlantBase { get => currPlantBase; 
    set{
            currPlantBase = value;
            if(currPlantBase == null)
            {
                HavePlant = false;
            }
            else
            {
                HavePlant = true;
            }
        }
    }
}
