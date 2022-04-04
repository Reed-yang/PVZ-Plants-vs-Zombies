using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySunManager : MonoBehaviour
{
    public static SkySunManager Instance;
    //创建阳光时的坐标Y
    private float createSunPosY = 6;
    //创建阳光时的坐标X范围
    private float createSunMaxPosX = 3;
    private float createSunMinPosX = -5;
    private float sunDownMaxPosY = 2.2F;
    private float sunDownMinPosY = -3.5F;
    private void Awake()
    {
        Instance = this;
    }
    public void StartCreatSun(float delay)
    {

        InvokeRepeating("CreateSun", delay, delay);
    }

    public void StopCreatSun()
    {
        CancelInvoke();
    }

    /// <summary>
    /// 从天空中生成阳光
    /// </summary>
    void CreateSun()
    {
        Sun sun = PoolManager.Instance.GetObj(GameManager.Instance.GameConf.Sun).GetComponent<Sun>();
        sun.transform.SetParent(transform);
        float downY = Random.Range(sunDownMinPosY, sunDownMaxPosY);
        float creatX = Random.Range(createSunMinPosX, createSunMaxPosX);
        sun.InitForSky(downY, creatX, createSunPosY);
    }
}
