using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateSun", 3, 3);

    }
    
    /// <summary>
    /// 太阳花创建阳光
    /// </summary>
    private void CreateSun()
    {
        Sun sun = GameObject.Instantiate<GameObject>(GameManager.Instance.Prefab_Sun, transform.position, Quaternion.identity).GetComponent<Sun>();
        sun.transform.SetParent(transform);
        //让阳光进行跳跃动画
        sun.JumpAnimation();
    
    }




}
