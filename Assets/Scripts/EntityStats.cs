using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
//����ฺ�����ʵ���ͳ������
{
    #region Health
    //��ɫ�������ֵ
    public Stat maxHealth;
    //��ɫ��ǰ����ֵ
    public int currentHealth;
    #endregion

    #region AttackDamage
    //��ɫ�Ļ��������˺�
    public Stat primaryAttackDamage;
    #endregion

    protected virtual void Start()
    {
        //��ʼʱ����ʵ�����������ֵ
        currentHealth = maxHealth.GetValue();
    }

    public virtual void GetDamaged(int _damage)
    {
        //������ʱ�����öԷ��Ĺ�����ֵ�����Լ��ĵ�ǰ����ֵ�ϼ���
        currentHealth -= _damage;
    }
}
