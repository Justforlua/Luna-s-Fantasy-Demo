using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;//单例
    public Image hpMaskImage;
    public Image mpMaskImage;
    private float OriginalHPSize;//血条原始宽度参数
    private float OriginalMPSiza;//蓝条原始宽度参数
    public GameObject battlePanelGo;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        OriginalHPSize = hpMaskImage.rectTransform.rect.width;
        OriginalMPSiza = mpMaskImage.rectTransform.rect.width;
        SetHPValue(1f);
        SetMPValue(1f);

    }
/// <summary>
/// 血条UI填充显示
/// </summary>
/// <param name="FillPercent">填充百分比</param>
    public void SetHPValue(float FillPercent)
    {
        hpMaskImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,FillPercent*OriginalHPSize);
    }

    public void SetMPValue(float FillPercent)
    {
        mpMaskImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,FillPercent*OriginalMPSiza);
    }

    public void ShowOrHideBattlePanel(bool show)
    {
        battlePanelGo.SetActive(show);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
