using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    //��Ҫ��ʾ�����Ե����֣�Hierarchy�ڶ�������֣�Ҳ��һ����ֵ��UI����ʾ�ĸ���ֵ���֣���statNameText
    [SerializeField] private string statName;

    //UI����ʾ�����Ե����ֵ��ı������˽ű�ʹ�����µ�Name��ʾ�����븳ֵ�˱���
    [SerializeField] TextMeshProUGUI statNameText;
    //UI����ʾ�����Ե�ֵ���ı�����Unity��ʹ�ô˽ű��Ķ���ͬʱҲ��Value����ʾ�ߣ���Ҫ���Լ����븳ֵ�˱���
    [SerializeField] TextMeshProUGUI statValueText;

    //�˱�����Unity�ڿ����ֶ�ѡȡ������enum StatType�ڵĸ����ݣ���ͨ��GetValueOfStatType������ȡ�����ݶ�Ӧ������ֵ
    [SerializeField] StatType statType;

    private void OnValidate()
    {
        gameObject.name = "Stat  " + statName;

        if(statNameText != null )
            statNameText.text = statName;
    }

    private void Start()
    {
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
}
