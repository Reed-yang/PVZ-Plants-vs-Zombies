using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 植物的基类
/// </summary>
public abstract class PlantBase : MonoBehaviour
{
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    /// <summary>
    /// 当前植物所在的网格
    /// </summary>
    protected Grid currGrid;
    private float hp;
    public float Hp
    {
        get => hp; protected set
        {
            hp = value;
            // 做生命值发生变化瞬间要做的事情
            //HpUpdateEvent();
        }
    }

    /// <summary>
    /// 查找自身相关组件
    /// </summary>
    protected void Find()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 创建时的初始化
    /// </summary>
    public void InitForCreate(bool inGrid)
    {
        Find();
        animator.speed = 0;
        if (inGrid)
        {
            spriteRenderer.sortingOrder = -1;
            spriteRenderer.color = new Color(1, 1, 1, 0.6f);
        }
        else
        {
            spriteRenderer.sortingOrder = 1;
        }
    }

    public void InitForPlace(Grid grid)
    {
        currGrid = grid;
        currGrid.CurrPlantBase = this;
        transform.position = grid.Position;
        animator.speed = 1;
        spriteRenderer.sortingOrder = 0;
        OnInitForPlace();
    }
    /// <summary>
    /// 受伤方法，被攻击时调用
    /// </summary>
    /// <param name="hurtvalue"></param>
    public void Hurt(float hurtvalue)
    {
        Hp -= hurtvalue;
        //发光效果
        StartCoroutine(ColorEF(0.2f, new Color(0.5f, 0.5f, 0.5f), 0.05f, null));
        if (Hp <= 0)
        {
            //死亡
            Dead();
        }
    }

    /// <summary>
    /// 颜色变化效果
    /// </summary>
    /// <returns></returns>
    protected IEnumerator ColorEF(float wantTime, Color targetColor, float delayTime, UnityAction fun)
    {
        float currTime = 0;
        float lerp;
        while (currTime < wantTime)
        {
            yield return new WaitForSeconds(delayTime);
            lerp = currTime / wantTime;
            currTime += delayTime;
            spriteRenderer.color = Color.Lerp(Color.white, targetColor, lerp);
        }
        spriteRenderer.color = Color.white;
        if (fun != null) fun();
    }

    private void Dead()
    {
        currGrid.CurrPlantBase = null;
        Destroy(gameObject);
    }
    protected virtual void OnInitForPlace() { }
}
