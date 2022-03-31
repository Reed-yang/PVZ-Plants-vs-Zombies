using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UIPantCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    //遮罩图片的img组件
    private Image maskImg;

    //冷却时间，几秒钟可以放置一次植物
    public float CDTime;

    //当前时间，用于冷却时间的计算
    private float currTimeForCD;

    //是否可以放置植物
    private bool canPlace;

    public bool CanPlace
    {get => canPlace;
        set
        {
            canPlace = value;
            //如果不能放置
            if (!canPlace)
            {
                maskImg.fillAmount = 1;
                //开始冷却
                CDEnter();
            }
            else
            {
                maskImg.fillAmount = 0;
            }
        }
    
    }

    void Start()
    {
        maskImg = transform.Find("Mask").GetComponent<Image>();
        CanPlace = false;
    }

    /// <summary>
    /// 进入CD
    /// </summary>
    private void CDEnter()
    {

        StartCoroutine(CalCD());
    }

    /// <summary>
    /// 计算冷却时间
    /// </summary>
    /// <returns></returns>
    IEnumerator CalCD() {

        float calCD = (1 / CDTime) * 0.1f;
        currTimeForCD = CDTime;
        while (currTimeForCD >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            maskImg.fillAmount -= calCD;
            currTimeForCD -= 0.1f;
        }
        //冷却时间已到
        
        CanPlace = true;
    }

    //鼠标移入
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!CanPlace) return;
        transform.localScale = new Vector2(1.05f, 1.05f);
    }

    //鼠标移除
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!CanPlace) return;
        transform.localScale = new Vector2(1f, 1f);
    }

    //鼠标点击时的效果，放置植物
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CanPlace) return;
        Debug.Log("放置植物");
    }
}
