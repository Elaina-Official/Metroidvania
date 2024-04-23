using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ע������������һ��UI��ص�
using UnityEngine.UI;


public class HealthBar_UI : MonoBehaviour
{
    #region Components
    //���ӵ�ʵ��Ľű�
    private Entity entity;
    //���ӵ�UI�����RectTransform����UIʹ�õ�Transform��
    private RectTransform myTransform;
    //����Ѫ���Ļ��飬��ʵ��Ѫ������
    private Slider slider;
    //���ӵ�ʵ���ͳ����Ϣ
    private EntityStats mySts;
    #endregion

    private void Start()
    {
        #region Components
        //���ӵ�Entity��ĸ�������ű�����Player.cs��Bringer.cs��
        entity = GetComponentInParent<Entity>();
        //���ӵ���Ѫ��UI��Transform
        myTransform = GetComponent<RectTransform>();
        //��ȡѪ��UI�Ļ���
        slider = GetComponentInChildren<Slider>();
        //���ӵ�ʵ���ͳ����Ϣ
        mySts = GetComponentInParent<EntityStats>();
        #endregion

        //ʹ��ʵ����ô˺���ʱ�������һ��FlipTheUI�������˴���+=����������أ�ʵ�ֺ��������
        entity.onFlipped += FlipTheUI;
    }

    private void Update()
    //���ڽ�ʡϵͳ���ܿ��ǣ����ѡ��ʹ��Update����������ʹ���¼�����ÿһ�ζ�����ֵ���и���ʱ���ܵ��˺���������һ�θ���UI�ĺ���
    {
        //����Ѫ��
        UpdateHealthUI();
    }

    //���ϸ���ʵ��ĵ�ǰѪ�����Ա����뻬������
    private void UpdateHealthUI()
    {
        //��������ֵ����ʵ������Ѫ��
        slider.maxValue = mySts.maxHealth.GetValue();
        //����ĵ�ǰֵ����ʵ��ĵ�ǰѪ��
        slider.value = mySts.currentHealth;
    }

    //��ʵ��ת��󣬰�Ѫ��UI����תһ�Σ�����UI����ת
    private void FlipTheUI()
    {
        //����תһ�Σ���ʵ�����ת������������ת
        myTransform.Rotate(0, 180, 0);
    }
}