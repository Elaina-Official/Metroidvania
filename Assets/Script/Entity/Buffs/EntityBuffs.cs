using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityBuffs : MonoBehaviour
//���ڿ���ʵ���Buff״̬
{
    #region Components
    private Entity entity;
    private EntityStats sts;
    private EntityFX fx;
    #endregion

    #region Buffs
    [Header("Buffs")]
    //ȼ��״̬��Ч��ʱ���ڳ�����Ѫ
    public Buff_Ignited ignited;
    //����״̬��Ч��ʱ�����ٶȼ���
    public Buff_Chilled chilled;
    //ѣ��״̬��Ч��ʱ���ڷ�������
    public Buff_Shocked shocked;
    #endregion

    #region Timers
    //����״̬��ʱ��
    private float ignitedTimer;
    private float ignitedDamageTimer;
    //����״̬��ʱ��
    private float chilledTimer;
    //ѣ��״̬��ʱ��
    private float shockedTimer;
    #endregion

    private void Start()
    {
        #region Components
        entity = GetComponent<Entity>();
        sts = GetComponent<EntityStats>();
        fx = GetComponent<EntityFX>();
        #endregion

        //��ʼʱ�������Buffs
        ClearAllBuffs();
    }

    private void Update()
    {
        //������Buff
        BuffsDetector();
    }

    #region DetectBuffs
    private void BuffsDetector()
    {
        if (ignited.GetStatus())
        {
            //���ü�ʱ����ˢ��
            ignitedTimer -= Time.deltaTime;
            ignitedDamageTimer -= Time.deltaTime;

            //�����˺���ʩ��
            if (ignitedDamageTimer < 0)
            {
                //�ٷֱ���Ѫ
                int _ignitedDamage = Mathf.RoundToInt(sts.GetFinalMaxHealth() * ignited.burnHealthPercentage);
                //����ʹ�õĺ���������ֵ����Ӱ�죬�������ڻ��ᴥ������Ч����ͬʱ�������BuffӦ�õļ�⣬������Ч����
                this.sts.GetMagicalDamagedBy(_ignitedDamage);

                //������ȴʱ�����ﵽÿ��һ��ʱ��������յ�Ч��
                ignitedDamageTimer = ignited.damageCooldown;
            }

            //�˳�ȼ��״̬
            if (ignitedTimer < 0)
            {
                ignited.SetStatus(false);
            }
        }

        if (chilled.GetStatus())
        //�䶳״̬�м���Ч�������ڸս����״̬ʱ����һ��
        {
            chilledTimer -= Time.deltaTime;

            //�˳�����״̬
            if (chilledTimer < 0)
            {
                chilled.SetStatus(false);
            }
        }

        if (shocked.GetStatus())
        //ѣ��״̬�з�������Ч�������ڸս����״̬ʱ����һ��
        {
            shockedTimer -= Time.deltaTime;

            //�˳�ѣ��״̬
            if (shockedTimer < 0)
            {
                shocked.SetStatus(false);
            }
        }
    }
    #endregion

    #region ApplyBuffs
    public virtual void CheckBuffsFrom(EntityStats _entity)
    {
        #region Evaluation
        //�洢�����Լ���ʵ���ħ��Ԫ���˺�����
        int _fireDmg = _entity.fireAttackDamage.GetValue();
        int _iceDmg = _entity.iceAttackDamage.GetValue();
        int _lightDmg = _entity.lightningAttackDamage.GetValue();

        //ֻҪ��������͵�ħ���˺�����ʩ�����buff
        bool _canApplyIgnite = (_fireDmg > 0);
        bool _canApplyChill = (_iceDmg > 0);
        bool _canApplyShock = (_lightDmg > 0);
        #endregion

        //ʩ��Buffs
        ApplyBuffs(_canApplyIgnite, _canApplyChill, _canApplyShock);
    }
    public virtual void ApplyBuffs(bool _ignited, bool _chilled, bool _shocked)
    {
        #region Evaluation
        //���ԭ���Ƿ��Ѿ�������Ӧ���͵�Buff��������Ӧ���ٶ����һ����Ч�����ᵼ�±������Ч�����ۼӣ������߲�����
        bool _canCallIgnited = _ignited;
        if (ignited.GetStatus() == true)
        {
            _canCallIgnited = false;
        }

        bool _canCallChilled = _chilled;
        if (chilled.GetStatus() == true)
        {
            _canCallChilled = false;
        }

        bool _canCallShocked = _shocked;
        if (shocked.GetStatus() == true)
        {
            _canCallShocked = false;
        }
        #endregion

        //����Buffs����Ч��
        if (_canCallIgnited)
        {
            //�����Buff�������˼���
            //Debug.Log(entity.name + " Get Ignited");

            //����״̬
            ignited.SetStatus(true);
            
            //ˢ�¼�ʱ��
            ignitedTimer = ignited.duration;
            ignitedDamageTimer = ignited.damageCooldown;

            //����״̬Ч��
            fx.InvokeIgnitedFXFor(ignited.duration);
        }

        if (_canCallChilled)
        {
            //����״̬
            chilled.SetStatus(true);

            //ˢ�¼�ʱ��
            chilledTimer = chilled.duration;

            //Ӧ���䶳�ļ��٣������䶳״̬��ָ�ԭ���ٶ�
            entity.SlowEntityBy(chilled.slowPercentage, chilled.duration);

            //����״̬Ч��
            fx.InvokeChilledFXFor(chilled.duration);
        }

        if (_canCallShocked)
        {
            //����״̬
            shocked.SetStatus(true);

            //ˢ�¼�ʱ��
            shockedTimer = shocked.duration;

            //�����Ľ��ͣ�����ѣ��״̬��ָ�ԭ����ֵ
            sts.DecreaseDefenceBy(shocked.defenceDecreasePercentage, shocked.duration);

            //����״̬Ч��
            fx.InvokeShockedFXFor(shocked.duration);
        }
    }
    #endregion

    #region ClearBuffs
    public void ClearAllBuffs()
    {
        //�������Buffs
        ignited.SetStatus(false);
        chilled.SetStatus(false);
        shocked.SetStatus(false);
    }
    #endregion
}
