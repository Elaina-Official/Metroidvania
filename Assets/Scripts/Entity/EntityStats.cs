using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;

public class EntityStats : MonoBehaviour
//����ฺ�����ʵ���ͳ������
{
    #region Components
    private Entity entity;
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
    [Header("Ailments")]
    //����ȼ��״̬��Ч��ʱ���ڳ�����Ѫ
    public bool isIgnited;
    //���ڱ���״̬��Ч��ʱ�����ٶȼ���
    public bool isChilled;
    //����ѣ��״̬
    public bool isShocked;
    
    //״̬����ʱ�������ʱ��
    [SerializeField] private float ignitedDuration = 5f;
    [SerializeField] private float chilledDuration = 5f;
    [SerializeField] private float shockedDuration = 5f;
    private float ignitedTimer;
    private float chilledTimer;
    private float shockedTimer;

    //��������״̬ʱ��ÿ���೤ʱ���ܵ�һ�������˺�
    public float ignitedDamageCooldown = 1f;
    private float ignitedDamageTimer;

    //���ڱ���״̬ʱ�������ٶȼ���
    public float slowPercentage = 0.5f;
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

        #region Components
        //����ʵ��ű������Զ�������ӵ�������ű�
        entity = GetComponent<Entity>();
        //Debug.Log(entity.name);

        //���ӵ�Ч���ű�
        fx = GetComponent<EntityFX>();
        #endregion

        //��ʼʱ������и���buff
        GetAilments(false, false, false);
    }

    protected virtual void Update()
    {
        #region Ailments
        if (isIgnited)
        {   
            //���ü�ʱ����ˢ��
            ignitedTimer -= Time.deltaTime;
            ignitedDamageTimer -= Time.deltaTime;

            //�˳�ȼ��״̬
            if(ignitedTimer < 0)
            {
                isIgnited = false;
            }

            //�����˺���ʩ��
            if(ignitedDamageTimer < 0)
            {
                //Debug.Log(this.name + " Take Fire Damage");
                //�ٷֱ���Ѫ
                int _ignitedDamage = Mathf.RoundToInt(originalMaxHealth.GetValue() * 0.02f);
                //����ʹ�õĺ���������ֵ����Ӱ�죬�������ڻ��ᴥ������Ч����ͬʱ�������Ailments�ļ�⣬������Ч����
                this.GetMagicalDamagedBy(_ignitedDamage);

                //������ȴʱ�����ﵽÿ��һ��ʱ��������յ�Ч��
                ignitedDamageTimer = ignitedDamageCooldown;
            }
        }
        if(isChilled)
        //�䶳״̬�ļ���buff�ڸտ�ʼ�����״̬��ʱ�����һ�μ��ɣ���GetAilments������
        {
            chilledTimer -= Time.deltaTime;

            //�˳�����״̬
            if(chilledTimer < 0)
            {
                isChilled = false;
            }
        }
        if(isShocked)
        {
            shockedTimer -= Time.deltaTime;

            //�˳�ѣ��״̬
            if(shockedTimer < 0)
            {
                isShocked = false;
            }
        }
        #endregion
    }

    #region TotalDamage
    public virtual void GetTotalNormalDmgFrom(EntityStats _attackingEntity, bool _doPhysic, bool _doMagic)
    //�ṩһ�ֵ���ȫ���˺��ĺ��������д����˺������ô˺����������鵥������
    //�ڶ�����������λҪ�����Ƿ�ֻ��������һ���˺���������һ�𴥷��Ĳ���ֵ
    {
        //������������ܣ���ֱ�ӷ��أ�������
        if (CanEvade())
        {
            
            //���ܵ���Ч
            AudioManager.instance.PlaySFX(12, null);
            //���ܵ�����Ч�������Լ����ܹ����ߣ�����
            fx.CreatHitFX00(this.transform);

            return;
        }
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

        //�ܹ�������Ч
        AudioManager.instance.PlaySFX(12, null);
        //�ܹ���������Ч�������Լ����ܹ����ߣ�����
        fx.CreatHitFX00(this.transform);
    }
    public virtual void GetTotalSkillDmgFrom(EntityStats _attackingEntity, int _skillDmg, bool _isMagical)
    //���ܵ��˺�������ħ�������˺��ģ���������˺��ߺ���ɵ��˺���С���˺������ͣ���ħ���Ļ���Ҫ����debuffʩ���ж���
    //�����˺���Է�ԭ���˺�������ֵ���Ǳ��������е��ӣ��Լ����˺�ʱ���ᷢ������
    {
        //���ܲ�������
        /*//������������ܣ���ֱ�ӷ��أ�������
        if (CanEvade())
        {
            return;
        }*/
        if (_isMagical)
        {
            this.GetMagicalDamagedBy(_attackingEntity.GetNonCritMagicalDamage() + _skillDmg);

            //debuffʩ��
            CheckAilmentsFrom(_attackingEntity);
        }
        if(!_isMagical)
        {
            this.GetPhysicalDamagedBy(_attackingEntity.GetFinalPhysicalDamage() + _skillDmg); 
        }

        //�ܹ�������Ч
        AudioManager.instance.PlaySFX(12, null);
        //�ܹ���������Ч�������Լ����ܹ����ߣ�����
        fx.CreatHitFX00(this.transform);
    }
    #endregion

    #region Ailments
    public virtual void CheckAilmentsFrom(EntityStats _entity)
    {
        //�洢�����Լ���ʵ���ħ��Ԫ���˺�����
        int _fireDmg = _entity.fireAttackDamage.GetValue();
        int _iceDmg = _entity.iceAttackDamage.GetValue();
        int _lightDmg = _entity.lightningAttackDamage.GetValue();

        //ѡ���������ݣ���ʩ�����Ӧ��Ԫ���˺�debuff����Ϊֻ��ʩ��һ��debuff����ѡȡ�˺�����
        bool _canApplyIgnite = _fireDmg > _iceDmg && _fireDmg > _lightDmg;
        bool _canApplyChill = _iceDmg > _fireDmg && _iceDmg > _lightDmg;
        bool _canApplyShock = _lightDmg > _fireDmg && _lightDmg > _iceDmg;

        //�����������ֵ�������ѭ�������ѡȡһ���˺��������Ԫ���˺�
        if (!_canApplyIgnite && !_canApplyChill && !_canApplyShock)
        {
            //û�˺�ֱ���˳������ü����
            if (_fireDmg <= 0 && _iceDmg <= 0 && _lightDmg <= 0)
                return;

            //���������
            int _random = UnityEngine.Random.Range(0, 100);
            if(_random <= 33 && _fireDmg > 0)
            {
                _canApplyIgnite = true;
            }
            else if(_random <= 66 && _iceDmg > 0)
            {
                _canApplyChill = true;
            }
            else if(_lightDmg > 0)
            {
                _canApplyShock = true;
            }
        }

        //ʩ��debuff
        GetAilments(_canApplyIgnite, _canApplyChill, _canApplyShock);
    }
    public virtual void GetAilments(bool _ignited, bool _chilled, bool _shocked)
    //Ӧ��ħ���˺�������Ϊ�����Ե�debuff
    {
        //�����ǰ��debuff�ˣ���ʱ���Ϊ�����ٶ�ʩ����debuff
        if (isIgnited || isChilled || isShocked)
            return;

        //����debuff״̬����Ч��
        if(_ignited)
        {
            isIgnited = _ignited;
            //ˢ�¼�ʱ��
            ignitedTimer = ignitedDuration;

            //����״̬Ч��
            fx.InvokeIgnitedFXFor(ignitedDuration);
        }
        if (_chilled)
        {
            isChilled = _chilled;
            chilledTimer = chilledDuration;

            //����״̬Ч��
            fx.InvokeChilledFXFor(chilledDuration);

            //Ӧ���䶳�ļ���
            entity.SlowEntityBy(slowPercentage, chilledDuration);
        }
        if(_shocked)
        {
            isShocked = _shocked;
            shockedTimer = shockedDuration;

            //����״̬Ч��
            fx.InvokeShockedFXFor(shockedDuration);
        }
    }
    #endregion

    #region MagicalDamaged
    public virtual void GetMagicalDamagedBy(int _damage)
    //��������ֵ���˺���ʩ�ӣ���debuff���ж�����Ҫ����з�Stats�������������Ϊ��Щ��ħ�������������debuff
    {
        #region AttackedFX
        //�ܹ����Ĳ��ʱ仯��ʹ�������ħ���˺�����˸�Ķ���Ч��
        entity.fx.StartCoroutine("MagicalHitFX");

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
        if (_statType == StatType.fireDamage) { return fireAttackDamage.GetValue(); }
        if (_statType == StatType.iceDamage) { return iceAttackDamage.GetValue(); }
        if (_statType == StatType.lightningDamage) { return lightningAttackDamage.GetValue(); }
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
    fireDamage,
    iceDamage,
    lightningDamage,
    evasion,
    armor,
    resistance
}
#endregion