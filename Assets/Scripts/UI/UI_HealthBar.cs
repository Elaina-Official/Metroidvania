using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ע������������һ��UI��ص�
using UnityEngine.UI;


public class UI_HealthBar : MonoBehaviour
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

        #region Events
        //ʹ��ʵ����ô˺���ʱ�������һ��FlipTheUI�������˴���+=����������أ�ʵ�ֺ��������
        entity.onFlipped += FlipTheUI;
        //ʹ��ÿ��onHealthChanged�¼������������ã�ʱ�����ø���Ѫ��UI�ĺ���
        mySts.onHealthChanged += UpdateHealthUI;
        #endregion
        
        //ʵ�����ɵ�ʱ�򣬵���һ��Ѫ��UI�ĸ���
        UpdateHealthUI();

        //�����Start��������Ҫȷ���ȳ�ʼ��ʵ��Ѫ����Start��������ã�����UI����ʵ��Ѫ��������
        //�����������˳�򣬿���Project Settings��Scripts Execution Order���޸�
        //Debug.Log("HealthBar_UI Start() Func Called");
    }

    //���ڽ�ʡϵͳ���ܿ��ǣ����ѡ��ʹ��Update��������Ѫ��������ʹ���¼�����ÿһ�ζ�����ֵ���и���ʱ���ܵ��˺���������һ�θ���UI�ĺ���
    //���ң��̳���MonoState��Ľű�����Update�����������׻��ң����ֻ��ʵ��ű���Update����״̬��Updateʵ������������ʵ���Update���ã�Ҳ�����������
    /*    private void Update()
        {
            //UpdateHealthUI();
        }*/

    private void UpdateHealthUI()
    //��onHealthChanged�¼����ӵ��ã����ϸ���ʵ��ĵ�ǰѪ�����Ա����뻬������
    {
        //��������ֵ����ʵ������Ѫ��
        slider.maxValue = mySts.originalMaxHealth.GetValue();
        //����ĵ�ǰֵ����ʵ��ĵ�ǰѪ��
        slider.value = mySts.currentHealth;
    }

    //��ʵ��ת��󣬰�Ѫ��UI����תһ�Σ�����UI����ת
    private void FlipTheUI()
    {
        //����תһ�Σ���ʵ�����ת������������ת
        myTransform.Rotate(0, 180, 0);
    }

    private void OnOfEventsDisable()
    //�ڲ���Ҫ��ʱ�򣬽����Щ�¼�����ʱ�Ķ�����õĺ���
    {
        entity.onFlipped -= FlipTheUI;
        mySts.onHealthChanged -= UpdateHealthUI;
    }
}