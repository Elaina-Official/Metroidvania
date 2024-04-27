using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    //��Ҫ��ʾ�����Ե�����
    [SerializeField] private string statName;
    //UI����ʾ�����Ե�ֵ���ı�
    [SerializeField] TextMeshProUGUI statValueText;
    //UI����ʾ�����Ե����ֵ��ı�
    [SerializeField] TextMeshProUGUI statNameText;

    private void OnValidate()
    {
        gameObject.name = "Stat  " + statName;

        if(statValueText != null )
            statValueText.text = statName;
    }
}
