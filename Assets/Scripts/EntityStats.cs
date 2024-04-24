using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
//����ฺ�����ʵ���ͳ������
{
    #region Health
    //��ɫ�������ֵ
    public Stat OriginalMaxHealth;
    //��ɫ��ǰ����ֵ
    public int currentHealth;
    #endregion

    #region AttackDamage
    //��ɫ�Ļ��������˺�
    public Stat primaryAttackDamage;
    #endregion

    #region Events
    //��¼ʵ������ֵ�仯����¼����Ա�ʹ��ֻ����Ѫ���䶯ʱ����Ѫ��������һֱ�ڸ���
    public System.Action onHealthChanged;
    #endregion

    protected virtual void Start()
    {
        //��ʼʱ����ʵ����ӳɹ�����������ֵ
        currentHealth = GetFinalMaxHealth();

        //�����Start��������Ҫȷ���ȸ���Ѫ��UI��Start�����ȵ��ã�����UI����ʵ��Ѫ��������
        //�����������˳�򣬿���Project Settings��Scripts Execution Order���޸�
        //Debug.Log("EntityStats Start() Func Called");
    }

    public virtual void GetDamaged(int _damage)
    //�й��˺���ֵ�ĵ��ã������������Ч�����ڼ̳к���д����Ҫʱ����
    {
        //������ʱ�����öԷ��Ĺ�����ֵ�����Լ��ĵ�ǰ����ֵ�ϼ���
        currentHealth -= _damage;
        //������ʱ������һ��Ѫ��UI�ĸ���
        if(onHealthChanged != null)
        {
            onHealthChanged();
        }
    }

    public virtual int GetFinalMaxHealth()
    //�˺�������ʵ����������Ѫ���������ڳ�ʼ���Ѫ�����ϱ�ļӳ�
    {
        return OriginalMaxHealth.GetValue();
    }

    public virtual void StatsDie()
    //ʵ��������������Ҫ��������д
    {
        //throw new NotImplementedException();
    }
}
