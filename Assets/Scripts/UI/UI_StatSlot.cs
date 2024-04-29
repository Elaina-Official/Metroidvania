using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
//���ں����������࣬alt+enter��ѡ��ʵ�ֽӿڡ�����ʹ�ã���ֱ���һ��������
{
    //��ȡUI���
    private UI ui;

    #region SlotContent
    //��Ҫ��ʾ�����Ե����֣�Hierarchy�ڶ�������֣�Ҳ��һ����ֵ��UI����ʾ�ĸ���ֵ���֣���statNameText
    [SerializeField] private string statName;

    //UI����ʾ�����Ե����ֵ��ı������˽ű�ʹ�����µ�Name��ʾ�����븳ֵ�˱���
    [SerializeField] TextMeshProUGUI statNameText;
    //UI����ʾ�����Ե�ֵ���ı�����Unity��ʹ�ô˽ű��Ķ���ͬʱҲ��Value����ʾ�ߣ���Ҫ���Լ����븳ֵ�˱���
    [SerializeField] TextMeshProUGUI statValueText;

    //�˱�����Unity�ڿ����ֶ�ѡȡ������enum StatType�ڵĸ����ݣ���ͨ��GetValueOfStatType������ȡ�����ݶ�Ӧ������ֵ
    [SerializeField] StatType statType;
    #endregion

    #region ToolTip
    //�������slot�ľ���������Ϣ����Ҫ�ֶ����룻[TextArea]ʹ��������Hierarchy�ڿ��Էֶ������룬������ֻ��һ��
    [TextArea]
    [SerializeField] string statDescription;
    #endregion

    private void OnValidate()
    {
        gameObject.name = "Stat  " + statName;

        if(statNameText != null )
            statNameText.text = statName;
    }

    private void Start()
    {
        //��ʼʱ��ȡUI���
        ui = GetComponentInParent<UI>();

        //��ʼʱ����һ������ֵ
        UpdateStatValueSlotUI();
    }

    public void UpdateStatValueSlotUI()
    //ÿ����ֵ�����仯ʱ����Ҫ���ô˺�������һ��
    {
        //��ȡ����ͳ�����ݽű�
        PlayerStats pStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    
        if( pStats != null )
        {
            //ͨ���˺�����ȡѡȡ�����ݶ�Ӧ������ֵ����ת��Ϊ�ַ���
            statValueText.text = pStats.GetValueOfStatType(statType).ToString();
        }
    }

    #region PointerEnter&Exit
    public void OnPointerEnter(PointerEventData eventData)
    {
        //����Ĭ�����ɵ���䣬���ǲ������
        //throw new System.NotImplementedException();

        //�������ͣ�����slot��ʱ����ʾ�������
        ui.statToolTip.ShowStatToolTipAs(statDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //����Ĭ�����ɵ���䣬���ǲ������
        //throw new System.NotImplementedException();

        //�뿪ʱ���ر�
        ui.statToolTip.HideStatToolTip();
    }
    #endregion
}
