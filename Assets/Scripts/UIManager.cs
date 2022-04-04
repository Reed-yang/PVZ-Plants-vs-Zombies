using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private Text sunNumText;
    private GameObject Panel;
    // 当前的植物卡片
    private UIPlantCard currCard;
    private LVInfoPanel LVInfoPanel;
    //private SetPanel SetPanel;
    //private OverPanel OverPanel;
    public UIPlantCard CurrCard
    {
        get => currCard;
        set
        {
            if (currCard == value) return;
            // 置空上一个卡片的状态
            if (currCard != null)
            {
                currCard.WantPlace = false;
            }
            currCard = value;
        }
    }

    private void Awake()
    {
        Instance = this;
        Panel = transform.Find("Panel").gameObject;
        sunNumText = transform.Find("Panel/SunNumText").GetComponent<Text>();
        LVInfoPanel = transform.Find("LVInfoPanel").GetComponent<LVInfoPanel>();
        //SetPanel = transform.Find("SetPanel").GetComponent<SetPanel>();
        //SetPanel.gameObject.SetActive(false);
        //OverPanel = transform.Find("OverPanel").GetComponent<OverPanel>();
    }


    /// <summary>
    /// 更新阳光的数字
    /// </summary>
    public void UpdateSunNum(int num)
    {
        sunNumText.text = num.ToString();
    }

    /// <summary>
    /// 获取阳光数量Text的坐标
    /// </summary>
    public Vector3 GetSunNumTextPos()
    {
        return sunNumText.transform.position;
    }

    /// <summary>
    /// 设置主面板的显示
    /// </summary>
    /// <param name="isShow"></param>
    public void SetMainPanelActive(bool isShow)
    {
        Panel.SetActive(isShow);
    }
    public void UpdateDayNum(int day)
    {
        LVInfoPanel.UpdateDayNum(day);
    }
    public void UpdateStageNum(int stage)
    {
        LVInfoPanel.UpdateStageNum(stage);
    }
    public void ShowSetPanel()
    {
        //AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.Pause);
        //SetPanel.Show(true);
    }

    public void GameOver()
    {
        //OverPanel.Over();
    }
}
