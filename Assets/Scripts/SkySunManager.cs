using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySunManager : MonoBehaviour
{


    //创建阳光时的坐标Y
    private float createSunPosY = 6;
    //创建阳光时的坐标X范围
    private float createSunMaxPosX = 3;
    private float createSunMinPosX = -5;

    private float sunDownMaxPosY = 2.2F;
    private float sunDownMinPosY = -3.5F;

    void Start()
    {
        
        InvokeRepeating("CreateSun", 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //从天空中生成阳光
    void CreateSun()
    {
        Sun sun = GameObject.Instantiate<GameObject>(GameManager.Instance.GameConf.Sun, Vector3.zero, Quaternion.identity, transform).GetComponent<Sun>();
        float downY = Random.Range(sunDownMinPosY, sunDownMaxPosY);
        float creatX = Random.Range(createSunMinPosX, createSunMaxPosX);
        sun.InitForSky(downY, creatX, createSunPosY);
    }
}
