using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    //阳光的数量
    private int sunNum;
    public int SunNum
    {
        get => sunNum;
        set
        {
            sunNum = value;
            UIManager.Instance.UpdateSunNum(sunNum);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SunNum = 100;
    }
}
