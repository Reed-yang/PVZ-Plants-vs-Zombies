using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 植物的基类
/// </summary>
public abstract class PlantBase : MonoBehaviour
{
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

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
    }

    public void InitForPlace()
    {
        animator.speed = 1;
        spriteRenderer.sortingOrder = 0;
        OnInitForPlace();
    }

    protected virtual void OnInitForPlace() { }
}
