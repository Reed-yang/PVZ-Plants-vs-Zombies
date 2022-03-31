using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private List<Vector2> pointList = new List<Vector2>();
    private List<Grid> GridList = new List<Grid>();
    
    void Start()
    {
        CreatGridsBaseGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
     //       Debug.Log(GetGridPointbyMouse());
        }
    }
    /*
        private void CreatGridsBaseColl()
        {
            //创建一个预制体网格
            GameObject prefabGrid = new GameObject();
            prefabGrid.AddComponent<BoxCollider2D>().size = new Vector2(1, 1.5f);
            prefabGrid.transform.SetParent(transform);
            prefabGrid.transform.position = transform.position;
            prefabGrid.name = 0 + "-" + 0;

            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    GameObject grid = GameObject.Instantiate<GameObject>(prefabGrid, transform.position + new Vector3(1.4f * i, 1.55f * j, 0), Quaternion.identity, transform);
                    grid.name = i + "-" + j;
                }
            }
        }
   
    /// <summary>
    /// 基于坐标List形式创建网络
    /// </summary>
    private void CreatGridsBasePointList()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                pointList.Add(transform.position + new Vector3(1.4f * i, 1.55f * j, 0));
            }
        }
    }
 */
    
    /// <summary>
    /// 基于Grid脚本的形式创建网络
    /// </summary>
    private void CreatGridsBaseGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GridList.Add(new Grid(new Vector2(i, j), transform.position + new Vector3(1.4f * i, 1.55f * j, 0), false));
            }
        }
    }

    /// <summary>
    /// 通过鼠标获取网格坐标点
    /// </summary>
    /// <returns></returns>
    public Vector2 GetGridPointbyMouse()
    {
        float dis = 1000000;
        Vector2 point = new Vector2();
        for(int i = 0; i < GridList.Count; i++)
        {
            if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), GridList[i].Position) < dis)
            {
                dis = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), GridList[i].Position);
                point = GridList[i].Position;
            }
        }
        return point;
    }
}
