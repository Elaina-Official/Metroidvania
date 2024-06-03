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

    #region Attribute
    [Header("Attribute Stats")]
    //�������ԣ�����10�㹥������1%�����ʣ�2%�����˺�����Щ�ӳ���modifiers�ļӳɲ���ͬһ��ϵ�ģ�
    public Stat strength;
    //��������,����5�㹥������2%�����ʣ�1%������evasion
    public Stat agility;
    //���������ԣ�ÿ������20��maxHealth��2��������
    public Stat vitality;
    //�������ԣ�����10�㷨����������2�㷨���ֿ���
    public Stat intelligence;
    #endregion

    #region Attack
    [Header("Attack Stats")]
    //�����˺����ʣ��ٷֱȣ�����100��
    public Stat criticPower;
    //�����ʣ��ٷֱȣ�
    public Stat criticChance;
    
    //ʵ��Ļ����������˺�
    public Stat primaryPhysicalDamage;
    //�����˺�
    public Stat fireAttackDamage;
    //�����˺�
    public Stat iceAttackDamage;
    //�����˺�
    public Stat lightningAttackDamage;
    #endregion

    #region Ailments
    //����ȼ��״̬
    public bool isIgnited;
    //���ڱ���״̬
    public bool isChilled;
    //����ѣ��״̬
    public bool isShocked;
    #endregion

    #region Defence
    [Header("Defence Stats")]
    //�����ʣ��ٷֱȣ�
    public Stat evasionChance;
    //����ֵ���ṩ������ˣ��ٷֱȣ�
    public Stat physicalArmor;
    //�����ֿ������ṩ�������ˣ��ٷֱȣ�
    public Stat magicalResistance;
    #endregion

    #region Events
    //��¼ʵ������ֵ�仯����¼����Ա�ʹ��ֻ����Ѫ���䶯ʱ����Ѫ��������һֱ�ڸ���
    public System.Action onHealthChanged;
    #endregion

    protected virtual void Start()
    {
        //�����Start��������Ҫȷ���ȸ���Ѫ��UI��Start�����ȵ��ã�����UI����ʵ��Ѫ��������
        //�����������˳�򣬿���Project Settings��Scripts Execution Order���޸�
        //Debug.Log("EntityStats Start() Func Called");
        //��ʼʱ����ʵ����ӳɹ�����������ֵ
        currentHealth = GetFinalMaxHealth();

        //����ʵ��ű������Զ�������ӵ�������ű�
        entity = GetComponent<Entity>();
        //Debug.Log(entity.name);

        //��ʼʱ������и���buff
        GetAilments(false, false, false);
    }

    #region TotalDamage
    public virtual void GetTotalNormalDmgFrom(EntityStats _attackingEntity, bool _doPhysic, bool _doMagic)
    //�ṩһ�ֵ���ȫ���˺��ĺ��������д����˺������ô˺����������鵥������
    //�ڶ�����������λҪ�����Ƿ�ֻ��������һ���˺���������һ�𴥷��Ĳ���ֵ
    {
        //���ǶԷ������˺�Ϊ0����Ӧ��������ֵ�Ļ�ȡ�����л�Ҫ���б����ж����ж����ǳɹ��˻��᷵�ر���Ч�������ǲ���Ҫ�ģ���Ϊ���������0�˺���
        if (_attackingEntity.GetNonCritMagicalDamage() > 0 && _doMagic)
        {
            //��ֵ�˺���ʩ��
            this.GetMagicalDamagedBy(_attackingEntity.GetFinalMagicalDamage());

            //����Ч����ʩ��
            CheckAilmentsFrom(_attackingEntity); 
        }
        if(_attackingEntity.GetNonCritPhysicalDamage() > 0 && _doPhysic)
        {
            this.GetPhysicalDamagedBy(_attackingEntity.GetFinalPhysicalDamage());
        }
    }
    public virtual void GetTotalSkillDmgFrom(EntityStats _attackingEntity, int _skillDmg, bool _isMagical)
    //���ܵ��˺�������ħ�������˺��ģ���������˺��ߺ���ɵ��˺���С���˺������ͣ���ħ���Ļ���Ҫ����debuffʩ���ж���
    //�����˺���Է�ԭ���˺�������ֵ���Ǳ��������е��ӣ��Լ����˺�ʱ���ᷢ������
    {
        if(_isMagical)
        {
            this.GetMagicalDamagedBy(_attackingEntity.GetNonCritMagicalDamage() + _skillDmg);

            //debuffʩ��
            CheckAilmentsFrom(_attackingEntity);
        }
        if(!_isMagical)
        {
            this.GetPhysicalDamagedBy(_attackingEntity.GetFinalPhysicalDamage() + _skillDmg); 
        }
    }
    #endregion

    #region Ailments
    public virtual void CheckAilmentsFrom(EntityStats _entity)
    {
        //�洢�����Լ���ʵ���ħ���˺�����
        int _fireDmg = _entity.fireAttackDamage.GetValue();
        int _iceDmg = _entity.iceAttackDamage.GetValue();
        int _lightDmg = _entity.lightningAttackDamage.GetValue();
        //ѡ���������ݣ���ʩ�����Ӧ��debuff����Ϊֻ��ʩ��һ��debuff����ѡȡ�˺�����
        bool _canApplyIgnite = _fireDmg > _iceDmg && _fireDmg > _lightDmg;
        bool _canApplyChill = _iceDmg > _fireDmg && _iceDmg > _lightDmg;
        bool _canApplyShock = _lightDmg > _fireDmg && _lightDmg > _iceDmg;
        //ʩ�Ӹ���Ч��
        GetAilments(_canApplyIgnite, _canApplyChill, _canApplyShock);
    }
    public virtual void GetAilments(bool _ignited, bool _chilled, bool _shocked)
    //Ӧ��ħ���˺�������Ϊ�����Ե�debuff
    {
        //�����ǰ��debuff�ˣ���ʱ���Ϊ�����ٶ�ʩ����debuff
        if (isIgnited || isChilled || _shocked)
            return;

        //����debuff״̬
        isIgnited = _ignited;
        isChilled = _chilled;
        isShocked = _shocked;
    }
    #endregion

    #region MagicalDamaged
    public virtual void GetMagicalDamagedBy(int _damage)
    //��������ֵ���˺���ʩ�ӣ���debuff���ж�����Ҫ����з�Stats�������������Ϊ��Щ��ħ�������������debuff
    {
        //������������ܣ���ֱ�ӷ��أ�������
        if (CanEvade())
        {
            return;
        }
        else
        {
            #region AttackedFX
            //�ܹ����Ĳ��ʱ仯��ʹ�������ħ���˺�����˸�Ķ���Ч��
            entity.fx.StartCoroutine("MagicalHitFX");

            //�����˺���ֵ�ı�Ч������Ҳ���
            if (entity.GetComponent<Player>() == null)
                entity.fx.CreatPopUpText(_damage.ToString(), Color.white);

            //ħ���˺�����Ҫ���ˣ���ʵ�Ƿ�ֹ�и����˺�ʱ�Ļ��˾������
            //entity.StartCoroutine("HitKnockback");
            #endregion

            //�ܵ����˺�������ֿ������������������ֵ��
            currentHealth -= CheckResistance(this, _damage);

            //������ʱ������һ��Ѫ��UI�ĸ���
            if (onHealthChanged != null)
            {
                onHealthChanged();
            }
        }
    }
    public virtual int CheckResistance(EntityStats _targetStats, int _damage)
    {
        //��¼�Լ��ķ����ֿ�����0~70���䣬���˺����ⲻ�ܳ���70%������������ҲҪ�յ��Է��˺���30%
        int _resistance = _targetStats.GetFinalResistance();
        _resistance = Mathf.Clamp(_resistance, 0, 70);
        //��������ʣ��ٷֱȣ�
        float _resistancePercentage = _resistance * 0.01f;
        //�������ճ����˺�����������
        float _checkedFinalDamage = _damage * (1 - _resistancePercentage);
        //ת��Ϊ�����˺�
        return Mathf.RoundToInt(_checkedFinalDamage);
    }
    #endregion

    #region PhysicalDamaged
    public virtual void GetPhysicalDamagedBy(int _damage)
    //�й������˺���ֵ�ĵ��ã������������Ч�����ڼ̳к���д����Ҫʱ����
    {
        //������������ܣ���ֱ�ӷ��أ�������
        if(CanEvade())
        {
            return;
        }
        else
        {
            #region AttackedFX
            //�ܹ����Ĳ��ʱ仯��ʹ������˸�Ķ���Ч��
            entity.fx.StartCoroutine("FlashHitFX");

            //�����˺���ֵ�ı�Ч������Ҳ���
            if (entity.GetComponent<Player>() == null)
                entity.fx.CreatPopUpText(_damage.ToString(), Color.white);

            //���˵Ļ���Ч��
            entity.StartCoroutine("HitKnockback");
            #endregion

            //������ʱ�����öԷ��Ĺ�����ֵ�����Լ��ĵ�ǰ����ֵ�ϼ���
            currentHealth -= CheckArmor(this, _damage);

            //������ʱ������һ��Ѫ��UI�ĸ���
            if (onHealthChanged != null)
            {
                onHealthChanged();
            }
        }
    }
    public virtual int CheckArmor(EntityStats _targetStats, int _damage)
    //��������˺�ǰ���һ�»���ֵ���˺�������
    {
        //��¼�Լ��������ף�0~70���䣬���˺����ⲻ�ܳ���70%������������ҲҪ�յ��Է��˺���30%
        int _armor = _targetStats.GetFinalArmor();
        //��ǯ�ƶ���_armor��С�ڵڶ���������ǯ���������Сֵmin�����򷵻�min��������ǯ���������ֵmax�򷵻�max;�����������򷵻�����
        _armor = Mathf.Clamp(_armor, 0, 70);
        //��������ʣ��ٷֱȣ�
        float _armorPercentage = _armor * 0.01f;
        //�������ճ����˺�����������
        float _checkedFinalDamage = _damage * (1 - _armorPercentage);
        //ת��Ϊ�����˺�
        return Mathf.RoundToInt(_checkedFinalDamage);
    }
    #endregion

    #region FinalValues
    public virtual int GetFinalPhysicalDamage()
    {
        //��¼�����Ǳ����˺�
        int _nonCritDamage = GetNonCritPhysicalDamage();

        //���Ǵ����˱������򷵻ص����˱������ʺ���˺�
        if (CanCrit())
        {
            //ʹ�ñ���������Ҫ����100��Ϊ��������ʽ�������ջ���Ҫ����һ����������
            float _criticPower = GetFinalCriticPower() * 0.01f;

            //�Ӹ���ת��Ϊ����
            return Mathf.RoundToInt(_criticPower * _nonCritDamage);
        }
        else
        {
            //���طǱ��������յĹ����˺�ֵ
            return _nonCritDamage;
        }
    }
    public virtual int GetNonCritPhysicalDamage()
    //�õ������б����ж���ԭʼ�˺������ڸ�UI��ֵ����ȻUI�Ǳ�ˢ������ʱ�����ܴ�����������������˺�ƫ��
    {
        return primaryPhysicalDamage.GetValue() + 10 * strength.GetValue() + 5 * agility.GetValue();
    }
    public virtual int GetFinalMagicalDamage()
    {
        //��¼�Ǳ�����ħ���˺�
        int _nonCritDamage = GetNonCritMagicalDamage();

        if (CanCrit())
        {
            float _criticPower = GetFinalCriticPower() * 0.01f;
            return Mathf.RoundToInt(_criticPower * _nonCritDamage);
        }
        else
        {
            return _nonCritDamage;
        }
    }
    public virtual int GetNonCritMagicalDamage()
    {
        return 10 * intelligence.GetValue() + fireAttackDamage.GetValue() + iceAttackDamage.GetValue() + lightningAttackDamage.GetValue();
    }
    public virtual int GetFinalMaxHealth()
    {
        //�˺�������ʵ����������Ѫ���������ڳ�ʼ���Ѫ�����ϱ�ļӳ�
        return originalMaxHealth.GetValue() + 20 * vitality.GetValue();
    }
    public virtual int GetFinalCriticPower()
    {
        //�����˺�����
        int _finalCriticPower = criticPower.GetValue() + 2 * strength.GetValue();
        //��֤����Ҫ����100%������Ϊint.MaxValue�����ͱ߽�
        _finalCriticPower = Mathf.Clamp(_finalCriticPower, 100, int.MaxValue);
        //�������ձ����˺����ʵ�%��ǰ����
        return _finalCriticPower;
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
    public virtual int GetFinalResistance()
    {
        //��ȡ���շ���������
        return magicalResistance.GetValue() + 2 * intelligence.GetValue();
    }
    public virtual int GetFinalArmor()
    {
        //�������ջ���ֵ
        return physicalArmor.GetValue() + 2 * vitality.GetValue();
    }
    #endregion

    #region StatsDie
    public virtual void StatsDie()
    //ʵ��������������Ҫ��������д
    {
        //������״̬��ת����deadState�ĵ���
        entity.EntityDie();
    }
    #endregion

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
            //���ֵ���Ч������Һ͹��ﶼ��
            entity.fx.CreatPopUpText("Miss", Color.yellow);

            return true;
        }
        return false;
    }
    #endregion

    #region StatTypeMapping
    public int GetValueOfStatType(StatType _statType)
    //���ض�Ӧ����ֵ
    {
        if (_statType == StatType.health) { return GetFinalMaxHealth(); }
        if (_statType == StatType.strength) { return strength.GetValue(); }
        if (_statType == StatType.agility) { return agility.GetValue(); }
        if (_statType == StatType.vitality) { return vitality.GetValue(); }
        if (_statType == StatType.intelligence) { return intelligence.GetValue(); }
        if (_statType == StatType.physicalDamage) { return GetNonCritPhysicalDamage(); }
        if (_statType == StatType.critChance) { return GetFinalCriticChance(); }
        if (_statType == StatType.critPower) { return GetFinalCriticPower(); }
        if (_statType == StatType.magicalDamage) { return GetNonCritMagicalDamage(); }
        if (_statType == StatType.evasion) { return GetFinalEvasionChance(); }
        if (_statType == StatType.armor) { return GetFinalArmor(); }
        if (_statType == StatType.resistance) { return GetFinalResistance(); }

        //ע�����ﷵ�ؿգ���Ȼ�ᱨ���������еĴ���·��������ֵ
        return 0;
    }
    #endregion
}

#region StatTypeEnum
public enum StatType
//��Hierarchy�������ϵĽű��ڵı�����ֵ��ʱ�������� ����������StatType�������ֱ��ѡȡ�˽ṹ�ڵĸ���Ա��Ϊֵ�������
{
    health,
    strength,
    agility,
    vitality,
    intelligence,
    physicalDamage,
    critChance,
    critPower,
    magicalDamage,
    evasion,
    armor,
    resistance
}
#endregion