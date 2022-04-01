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
    //是否需要放置
    private bool wantPlace;
    //用来创建的植物
    private PlantBase plant;
    //在网格中的植物，为透明的
    private PlantBase plantInGrid;
    //当前卡片所对应的植物类型
    public PlantType CardPlantType;
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

    public bool WantPlace { get => wantPlace;
        set
        {
            wantPlace = value;
            if (wantPlace)
            {
                GameObject prefab = PlantManager.Instance.GetPlantForType(CardPlantType);
                plant = GameObject.Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity, PlantManager.Instance.transform).GetComponent<PlantBase>();
                plant.InitForCreate(false);
            }
            else
            {
                if(plant != null)
                {
                    Destroy(plant.gameObject);
                    plant = null;
                }
            }
        }
    }

    void Start()
    {
        maskImg = transform.Find("Mask").GetComponent<Image>();
        CanPlace = false;
    }


    private void Update()
    {
        //如果需要放置植物，并且要放置的植物不为空
        if (WantPlace && plant!=null)
        {
            //让植物跟随我们的鼠标
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            plant.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0);

            //如果距离网格较近，网格上出现透明植物
            if (Vector2.Distance(mousePoint,GridManager.Instance.GetGridPointByMouse()) < 1.5f)
            {
                if(plantInGrid == null)
                {
                    plantInGrid = GameObject.Instantiate<GameObject>(plant.gameObject, GridManager.Instance.GetGridPointByMouse(), Quaternion.identity, PlantManager.Instance.transform).GetComponent<PlantBase>();
                    plantInGrid.InitForCreate(true);
                }
                else
                {
                    plantInGrid.transform.position = GridManager.Instance.GetGridPointByMouse();
                }

                //如果点击鼠标，即放置植物
                if (Input.GetMouseButtonDown(0))
                {
                    plant.transform.position = GridManager.Instance.GetGridPointByMouse();
                    plant.InitForPlace();
                    plant = null;
                    Destroy(plantInGrid.gameObject);
                    plantInGrid = null;
                    WantPlace = false;
                    CanPlace = false;
                }
            }
            else
            {
                if (plantInGrid != null)
                {
                    Destroy(plantInGrid.gameObject);
                    plantInGrid = null;
                }
            }
        }
        //如果右键取消放置
        if (Input.GetMouseButtonDown(1))
        {
            if (plant != null) Destroy(plant.gameObject);
            if (plantInGrid != null) Destroy(plantInGrid.gameObject);
            plant = null;
            plantInGrid = null;
            WantPlace = false;
            
        }
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
        if (!WantPlace)
        {
            WantPlace = true;
        }
    }
}
