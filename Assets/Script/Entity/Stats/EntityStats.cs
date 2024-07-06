using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.IO.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

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
    fireDamage,
    iceDamage,
    lightningDamage,
    evasion,
    armor,
    resistance,
    fireballDamage,
    iceballDamage
}
#endregion

public class EntityStats : MonoBehaviour
//����ฺ�����ʵ���ͳ������
{
    #region Components
    private Entity entity;
    private EntityBuffs buf;
    private EntityFX fx;
    #endregion

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
    //�����ʣ��ٷֱȣ�
    public Stat criticChance;
    //�����˺����ʣ��ٷֱȣ�����100��
    public Stat criticPower;
    
    //ʵ��Ļ����������˺�
    public Stat primaryPhysicalDamage;
    //�����˺�
    public Stat fireAttackDamage;
    //�����˺�
    public Stat iceAttackDamage;
    //�����˺�
    public Stat lightningAttackDamage;
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

    #region Default
    //�洢ԭ��ֵ
    private int defaultIntEvasion;
    private int defaultIntArmor;
    private int defaultIntResistance;
    #endregion

    #region Events
    //��¼ʵ������ֵ�仯����¼����Ա�ʹ��ֻ����Ѫ���䶯ʱ����Ѫ��������һֱ�ڸ���
    public System.Action onHealthChanged;
    #endregion

    protected virtual void Start()
    {
        #region Components
        //����ʵ��ű������Զ�������ӵ�������ű�
        entity = GetComponent<Entity>();
        //Debug.Log(entity.name);
        //���ӵ�Buffs�ű�
        buf = GetComponent<EntityBuffs>();
        //���ӵ�Ч���ű�
        fx = GetComponent<EntityFX>();
        #endregion

        //�����Start��������Ҫȷ���ȸ���Ѫ��UI��Start�����ȵ��ã�����UI����ʵ��Ѫ�������ϣ������������˳�򣬿���Project Settings��Scripts Execution Order���޸�
        //��ʼʱ����ʵ����ӳɹ�����������ֵ
        currentHealth = GetFinalMaxHealth();
        //Debug.Log("EntityStats Start() Func Called");
    }

    #region GetDamaged
    //���忼��ʵ���ܵ��Ĺ�����������ʵ���ܵ��������ħ�������˺�
    #region TotalDamage
    public virtual void GetTotalNormalDmgFrom(EntityStats _attackingEntity, bool _doPhysic, bool _doMagic)
    //�ڶ�����������λҪ�����Ƿ�ֻ��������һ���˺���������һ�𴥷��Ĳ���ֵ
    {
        #region Evade&Crit
        //��¼�Է��˺�������������
        int _attackingCritPower = _attackingEntity.GetFinalCriticPower();
        int _physicDmg = _attackingEntity.GetNonCritPhysicalDamage();
        int _magicDmg = _attackingEntity.GetNonCritMagicalDamage();

        //������������ܣ���ֱ�ӷ��أ�������
        if (CanEvade())
        {
            return;
        }

        //����Է������˱��������ȡ�Է������˺��������˵�����
        if (CanCrit(_attackingEntity))
        {
            //ʹ�ñ���������Ҫ����100��Ϊ��������ʽ�������ջ���Ҫ����һ����������
            float _criticPowerPercentage = GetFinalCriticPower() * 0.01f;
            //�Ӹ���ת��Ϊ����
            _physicDmg = Mathf.RoundToInt(_criticPowerPercentage * _physicDmg);
            _magicDmg = Mathf.RoundToInt(_criticPowerPercentage * _magicDmg);
        }
        #endregion

        #region AttackedFX
        //�ܹ�������Ч
        AudioManager.instance.PlaySFX(12, null);
        //�ܹ���������Ч�������Լ����ܹ����ߣ�����
        fx.CreateHitFX00(this.transform);
        #endregion

        //���ǶԷ������˺�Ϊ0����Ӧ�����˺�
        if (_attackingEntity.GetNonCritPhysicalDamage() > 0 && _doPhysic)
        {
            //������ֵ�˺���ʩ��
            this.GetPhysicalDamagedBy(_physicDmg);
        }
        if (_attackingEntity.GetNonCritMagicalDamage() > 0 && _doMagic)
        {
            //ħ����ֵ�˺���ʩ��
            this.GetMagicalDamagedBy(_magicDmg);

            //ħ��Ԫ�����Buff��ʩ��
            buf.CheckBuffsFrom(_attackingEntity); 
        }
    }
    public virtual void GetTotalSkillDmgFrom(EntityStats _attackingEntity, int _skillDmg, bool _doPhysic, bool _doMagic, bool _ignite, bool _chill, bool _shock)
    //�����缼���˺��Ĵ��������빥���ߺ���ɵ��˺���С���˺������ͣ���ħ���Ļ���Ҫ����Debuffʩ���ж���
    {
        #region Evade&Crit
        //��¼�Է��˺�������������
        int _attackingCritPower = _attackingEntity.GetFinalCriticPower();
        int _physicDmg = _attackingEntity.GetNonCritPhysicalDamage();
        int _magicDmg = _attackingEntity.GetNonCritMagicalDamage();

        //������������ܣ���ֱ�ӷ��أ�������
        if (CanEvade())
        {
            return;
        }

        //����Է������˱��������ȡ�Է������˺��������˵�����
        if (CanCrit(_attackingEntity))
        {
            //ʹ�ñ���������Ҫ����100��Ϊ��������ʽ�������ջ���Ҫ����һ����������
            float _criticPowerPercentage = GetFinalCriticPower() * 0.01f;
            //�Ӹ���ת��Ϊ����
            _physicDmg = Mathf.RoundToInt(_criticPowerPercentage * _physicDmg);
            _magicDmg = Mathf.RoundToInt(_criticPowerPercentage * _magicDmg);
        }
        #endregion

        #region AttackedFX
        //�ܹ�������Ч
        AudioManager.instance.PlaySFX(12, null);
        //�ܹ���������Ч�������Լ����ܹ����ߣ�����
        fx.CreateHitFX00(this.transform);
        #endregion

        //����һ�����˺������Բ��ü������˺��Ƿ������
        if (_doPhysic)
        {
            //������ֵ�˺���ʩ��
            this.GetPhysicalDamagedBy(_physicDmg + _skillDmg); 
        }
        if (_doMagic)
        {
            //ħ����ֵ�˺���ʩ��
            this.GetMagicalDamagedBy(_magicDmg + _skillDmg);

            //debuffʩ��
            buf.ApplyBuffs(_ignite, _chill, _shock);
        }
    }
    #endregion

    //��������ʵ���ܵ��������˺�ֵ
    #region PhysicalDamaged
    public virtual void GetPhysicalDamagedBy(int _damage)
    //�й������˺���ֵ�ĵ��ã������������Ч�����ڼ̳к���д����Ҫʱ����
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

    //��������ʵ���ܵ���ħ���˺�ֵ
    #region MagicalDamaged
    public virtual void GetMagicalDamagedBy(int _damage)
    //��������ֵ���˺���ʩ�ӣ���debuff���ж�����Ҫ����з�Stats�������������Ϊ��Щ��ħ�������������debuff
    {
        #region AttackedFX
        //�ܹ����Ĳ��ʱ仯��ʹ������˸�Ķ���Ч��
        entity.fx.StartCoroutine("FlashHitFX");

        //�����˺���ֵ�ı�Ч������Ҳ���
        if (entity.GetComponent<Player>() == null)
            entity.fx.CreatPopUpText(_damage.ToString(), Color.cyan);

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
    #endregion

    #region DecreaseDefence
    public void DecreaseDefenceBy(float _percentage, float _duration)
    //�������Ͷ��ٰٷֱȣ����Ͷ��
    {
        //�����Ľ��ͣ�Ҫ�ȱ���ԭ�е���ֵ
        defaultIntEvasion = evasionChance.GetValue();
        defaultIntArmor = physicalArmor.GetValue();
        defaultIntResistance = magicalResistance.GetValue();
        //���ͷ���ֵ
        evasionChance.SetValue(Mathf.RoundToInt(evasionChance.GetValue() * (1 - _percentage)));
        physicalArmor.SetValue(Mathf.RoundToInt(physicalArmor.GetValue() * (1 - _percentage)));
        magicalResistance.SetValue(Mathf.RoundToInt(magicalResistance.GetValue() * (1 - _percentage)));

        //��ú�ָ�
        Invoke("ReturnToDefaultDefence", _duration);
    }
    public void ReturnToDefaultDefence()
    {
        //�ָ���ֵ
        evasionChance.SetValue(defaultIntEvasion);
        physicalArmor.SetValue(defaultIntArmor);
        magicalResistance.SetValue(defaultIntResistance);
    }
    #endregion

    #region FinalValues
    public virtual int GetNonCritPhysicalDamage()
    //�õ������б����ж���ԭʼ�����˺�
    {
        return primaryPhysicalDamage.GetValue() + 10 * strength.GetValue() + 5 * agility.GetValue();
    }
    public virtual int GetNonCritMagicalDamage()
    //�õ������б����ж���ԭʼħ���˺�
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
    public virtual int GetFinalArmor()
    {
        //�������ջ���ֵ
        return physicalArmor.GetValue() + 2 * vitality.GetValue();
    }
    public virtual int GetFinalResistance()
    {
        //��ȡ���շ���������
        return magicalResistance.GetValue() + 2 * intelligence.GetValue();
    }
    #endregion

    #region ChanceAnalyze
    private bool CanEvade()
    //�Ƿ����ܵļ�����ڱ����������ϵĽű�ȥ����
    {
        //ͨ��������ķ�ʽ���ж��Ƿ��������
        if (UnityEngine.Random.Range(0, 100) <= GetFinalEvasionChance())
        {
            #region EvadeFX
            //���ֵ���Ч������Һ͹��ﶼ��
            entity.fx.CreatPopUpText("Miss", Color.yellow);
            //���ܵ���Ч
            AudioManager.instance.PlaySFX(12, null);
            //���ܵ�����Ч�������Լ����ܹ����ߣ�����
            fx.CreateHitFX00(this.transform);
            #endregion

            return true;
        }
        return false;
    }
    private bool CanCrit(EntityStats _attackingEntity)
    //�Ƿ񱩻��ļ��ҲӦ���ڱ����������ϵĽű����У���Ȼ�޷��ж��Ƿ񱻱������Ӷ��޷�ʩ����Ӧ��Ч��
    {
        //ע��Randomʹ�õ���Unity�ڵ�Random�������˴�ͨ��������ķ�ʽ���ж��Ƿ���Ա���
        if(UnityEngine.Random.Range(0,100) <= _attackingEntity.GetFinalCriticChance())
        {
            #region CritFX
            //�ܱ�������Ч
            AudioManager.instance.PlaySFX(13, null);
            //������������Ч�������Լ����ܹ����ߣ�����
            fx.CreateHitFX01(this.transform);
            #endregion

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
        if (_statType == StatType.fireDamage) { return fireAttackDamage.GetValue(); }
        if (_statType == StatType.iceDamage) { return iceAttackDamage.GetValue(); }
        if (_statType == StatType.lightningDamage) { return lightningAttackDamage.GetValue(); }
        if (_statType == StatType.evasion) { return GetFinalEvasionChance(); }
        if (_statType == StatType.armor) { return GetFinalArmor(); }
        if (_statType == StatType.resistance) { return GetFinalResistance(); }

        //ע�����ﷵ�ؿգ���Ȼ�ᱨ����Ϊ���еĿ����Զ���Ҫ�з���ֵ��
        return 0;
    }
    #endregion
}