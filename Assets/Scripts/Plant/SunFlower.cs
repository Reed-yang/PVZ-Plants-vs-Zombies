using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : PlantBase
{
    protected override void OnInitForPlace()
    {
        Hp = 300f;
        InvokeRepeating("CreateSun", 3, 3);
    }
    /// <summary>
    /// 太阳花创建阳光
    /// </summary>
    private void CreateSun()
    {
        Sun sun = GameObject.Instantiate<GameObject>(GameManager.Instance.GameConf.Sun, transform.position, Quaternion.identity).GetComponent<Sun>();
        sun.transform.SetParent(transform);
        //让阳光进行跳跃动画
        sun.JumpAnimation();
    }
}
