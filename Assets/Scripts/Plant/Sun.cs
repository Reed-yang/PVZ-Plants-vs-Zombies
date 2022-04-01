using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    //下落的目标点Y
    private float downTargetPosY;


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= downTargetPosY)
        {
            Invoke("DestorySun", 8);
            return;
        }
        transform.Translate(Vector3.down * Time.deltaTime);
    }

    /// <summary>
    /// 鼠标点击阳光时，增加UIManager中阳光数量；并销毁
    /// </summary>
    private void OnMouseDown()
    {
        PlayerManager.Instance.SunNum += 50;
        Vector3 sunNum = Camera.main.ScreenToWorldPoint(UIManager.Instance.GetSunNumTextPos());
        sunNum = new Vector3(sunNum.x, sunNum.y, 0);
        FlyAnimation(sunNum);
        
    }

    /// <summary>
    /// 让阳光进行跳跃动画
    /// </summary>
    public void JumpAnimation()
    {

        StartCoroutine(Dojump());
    }
    private IEnumerator Dojump()
    {
        
        bool isLeft = Random.Range(0, 2)==0;
        Vector3 startPos = transform.position;
        if (isLeft)
        {
            //向上跳
            while (transform.position.y <= startPos.y+1)
            {
                yield return new WaitForSeconds(0.005f);
                transform.Translate(new Vector3(-0.01f, 0.05f, 0));
            }
            //向下落
            while (transform.position.y >= startPos.y)
            {
                yield return new WaitForSeconds(0.005f);
                transform.Translate(new Vector3(-0.01f, -0.05f, 0));
            }
        }
        else//向右
        {
            while (transform.position.y <= startPos.y + 1)
            {
                yield return new WaitForSeconds(0.005f);
                transform.Translate(new Vector3(0.01f, 0.05f, 0));
            }
            while (transform.position.y >= startPos.y)
            {
                yield return new WaitForSeconds(0.005f);
                transform.Translate(new Vector3(0.01f, -0.05f, 0));
            }
        }
    }



    /// <summary>
    /// 飞行动画
    /// </summary>
    private void FlyAnimation(Vector3 pos)
    {
        StartCoroutine(DoFly(pos));
    }


    private IEnumerator DoFly(Vector3 pos)
    {
        Vector3 direction = (pos - transform.position).normalized;
        while (Vector3.Distance(pos, transform.position) > 0.5f )
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(direction);

        }
        DestorySun();
    }


    /// <summary>
    /// 销毁阳光
    /// </summary>
    private void DestorySun()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 阳光同天空中初始化的方法
    /// </summary>
    public void InitForSky(float downTargetPosY, float creatPosX, float creatPosY)
    {
        this.downTargetPosY = downTargetPosY;
        transform.position = new Vector2(creatPosX, creatPosY);

    }
}
