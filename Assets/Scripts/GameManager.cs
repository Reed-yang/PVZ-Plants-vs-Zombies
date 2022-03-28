using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //阳光的数量
    private int sunNum;
    //预制体统一由GameManager管理
    //阳光预制体Prefab
    public GameObject Prefab_Sun { get; private set; }


    public int SunNum{
        get => sunNum;
        set{
            sunNum = value;
            UIManager.Instance.UpdateSunNum(sunNum);
        }
    }


    private void Awake()
    {
        Instance = this;
        Prefab_Sun = Resources.Load<GameObject>("Sun");
    }

    void Start()
    {
        SunNum = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
