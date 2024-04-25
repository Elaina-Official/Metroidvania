using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EntityStats : MonoBehaviour
//����ฺ�����ʵ���ͳ������
{
    Entity entity;

    #region Health
    [Header("Health Stats")]
    //��ɫ�������ֵ
    public Stat originalMaxHealth;
    //��ɫ��ǰ����ֵ
    public int currentHealth;
    #endregion

    #region Attack
    [Header("Attack Stats")]
    //ʵ��Ļ��������˺�
    public Stat primaryAttackDamage;
    //�����˺����ʣ������������˺��ϵİٷֱȣ�Ĭ��150%��ͨ����ֵ����ʵ�֣�
    public Stat criticPower;
    //������
    public Stat criticChance;
    #endregion

    #region Attribute
    [Header("Attribute Stats")]
    //�������ԣ�����10�㹥������1%�����ʣ�2%�����˺�����Щ�ӳ���modifiers�ļӳɲ���ͬһ��ϵ�ģ�
    public Stat strength;
    //��������,����5�㹥������2%�����ʣ�1%������evasion
    public Stat agility;
    //���������ԣ�ÿ������20��maxHealth
    public Stat vitality;
    //�������ԣ�����10�㷨��������������з��������Ļ���
    //public Stat intelligence;
    #endregion

    #region Defence
    [Header("Defence Stats")]
    //����ֵ���ṩ����
    public Stat armor;
    //������
    public Stat evasionChance;
    #endregion

    #region Events
    //��¼ʵ������ֵ�仯����¼����Ա�ʹ��ֻ����Ѫ���䶯ʱ����Ѫ��������һֱ�ڸ���
    public System.Action onHealthChanged;
    #endregion

    protected virtual void Start()
    {
        #region SetDefault
        //����Ĭ���������ֵ
        originalMaxHealth.SetDefaultValue(100);
        //����Ĭ�ϱ����˺�����Ϊ150%
        criticPower.SetDefaultValue(150);
        //��ʼ������Ϊ5%
        criticChance.SetDefaultValue(5);
        //��ʼ������Ϊ5%
        evasionChance.SetDefaultValue(5);
        #endregion

        //�����Start��������Ҫȷ���ȸ���Ѫ��UI��Start�����ȵ��ã�����UI����ʵ��Ѫ��������
        //�����������˳�򣬿���Project Settings��Scripts Execution Order���޸�
        //��ʼʱ����ʵ����ӳɹ�����������ֵ
        currentHealth = GetFinalMaxHealth();

        //����ʵ��ű������Զ�������ӵ�������ű�
        entity = GetComponent<Entity>();
        //Debug.Log(entity.name);
    }

    #region ChanceAnalyze
    private bool CanCrit()
    //�ж��Ƿ���Ա���
    {
        //ע��Randomʹ�õ���Unity�ڵ�Random����
        //ͨ��������ķ�ʽ���ж��Ƿ���Ա���
        if(UnityEngine.Random.Range(0,100) <= GetFinalCriticChance())
        {
            return true;
        }
        return false;
    }
    private bool CanEvade()
    {
        //ע��Randomʹ�õ���Unity�ڵ�Random����
        //ͨ��������ķ�ʽ���ж��Ƿ��������
        if (UnityEngine.Random.Range(0, 100) <= GetFinalEvasionChance())
        {
            return true;
        }
        return false;
    }
    #endregion

    #region CalculateFinalValues
    public virtual int GetFinalAttackDamage(Stat _primaryDamage)
    {
        //���Ǵ����˱������򷵻ص����˱������ʺ���˺�
        if(CanCrit())
        {
            Debug.Log(entity.name + " Crit");

            //ע�������벿�ֱ��õݹ飬���ܶ�δ����������ʵ����
            int _nonCritDamage = _primaryDamage.GetValue() + 10 * strength.GetValue() + 5 * agility.GetValue();
            //ʹ�ñ���������Ҫ����100��Ϊ��������ʽ�������ջ���Ҫ����һ����������
            float _criticPower = GetFinalCriticPower() * 0.01f;

            //�Ӹ���ת��Ϊ����
            return Mathf.RoundToInt(_criticPower * _nonCritDamage);
        }
        else
        {
            //���طǱ��������յĹ����˺�ֵ
            return _primaryDamage.GetValue() + 10 * strength.GetValue() + 5 * agility.GetValue();
        }
    }
    public virtual int GetFinalMaxHealth()
    {
        //�˺�������ʵ����������Ѫ���������ڳ�ʼ���Ѫ�����ϱ�ļӳ�
        return originalMaxHealth.GetValue() + 20 * vitality.GetValue();
    }
    public virtual float GetFinalCriticPower()
    {
        //�������ձ����˺����ʵ�%��ǰ����
        return criticPower.GetValue() + 2 * strength.GetValue();
    }
    public virtual int GetFinalCriticChance()
    {
        //criticChanceȡֵ����Ϊ0~100����λΪ%
        return Mathf.Clamp(criticChance.GetValue(), 0, 100) + 1 * strength.GetValue() + 2 * agility.GetValue();
    }
    public virtual int GetFinalEvasionChance()
    {
        //criticChanceȡֵ����Ϊ0~100����λΪ%
        return Mathf.Clamp(evasionChance.GetValue(), 0, 100) + 1 * agility.GetValue();
    }
    #endregion

    #region GetDamaged
    public virtual void GetDamagedBy(int _damage)
    //�й��˺���ֵ�ĵ��ã������������Ч�����ڼ̳к���д����Ҫʱ����
    {
        //������������ܣ���ֱ�ӷ��أ�������
        if(CanEvade())
        {
            Debug.Log(entity.name + " Evade");
            return;
        }
        else
        {
            //������ʱ�����öԷ��Ĺ�����ֵ�����Լ��ĵ�ǰ����ֵ�ϼ���
            currentHealth -= CheckArmor(this, _damage);

            //�ܹ����Ĳ��ʱ仯��ʹ������˸�Ķ���Ч��
            entity.fx.StartCoroutine("FlashHitFX");

            //���˵Ļ���Ч��
            entity.StartCoroutine("HitKnockback");

            //������ʱ������һ��Ѫ��UI�ĸ���
            if (onHealthChanged != null)
            {
                onHealthChanged();
            }
        }
    }
    public virtual int CheckArmor(EntityStats _targetEntity, int _damage)
    //��������˺�ǰ���һ�»���ֵ���˺�������
    {
        //�˴�����ֵ���˺������Ƿǰٷֱ���ʽ�ģ������ɸ�Ϊ�ٷֱȼ���
        int _totalDamage = _damage - _targetEntity.armor.GetValue();
        //��ǯ�ƶ���_totalDamage��С��Clamp�ڵĵڶ���������ǯ���������Сֵmin�����򷵻�min
        //������ǯ���������ֵmax���˴�Ϊint,MaxValue�����������ɵ�����������򷵻�max�������������򷵻�����
        //�˴����طǸ���������Ϊ�˺�����Ϊ��������������ƣ�
        return Mathf.Clamp(_totalDamage, 0, int.MaxValue);
    }
    #endregion

    #region Die
    public virtual void StatsDie()
    //ʵ��������������Ҫ��������д
    {
        //������״̬��ת����deadState�ĵ���
        entity.EntityDie();

        //throw new NotImplementedException();
    }
    #endregion
}
