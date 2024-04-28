using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_InGame : MonoBehaviour
{
    private PlayerStats pStats;
    private UnityEngine.UI.Slider healthBarSlider;

    private void Start()
    {
        pStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        healthBarSlider = GetComponentInChildren<UnityEngine.UI.Slider>();

        //�¼��ĵ���
        if (pStats != null)
            pStats.onHealthChanged += UpdateHealthUI;

        //�����Start��������Ҫȷ���ȳ�ʼ��ʵ��Ѫ����Start��������ã�����UI����ʵ��Ѫ��������
        //�����������˳�򣬿���Project Settings��Scripts Execution Order���޸�
        //Debug.Log("UI_InGame Start() Func Called");
        //ʵ�����ɵ�ʱ�򣬵���һ��Ѫ��UI�ĸ���
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    //��onHealthChanged�¼����ӵ��ã����ϸ���ʵ��ĵ�ǰѪ�����Ա����뻬������
    {
        //��������ֵ����ʵ���������Ѫ��
        healthBarSlider.maxValue = pStats.GetFinalMaxHealth();
        //����ĵ�ǰֵ����ʵ��ĵ�ǰѪ��
        healthBarSlider.value = pStats.currentHealth;
    }
}
