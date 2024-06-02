using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats, ISavesManager
{
    //��ʵֱ����etity�ͺã��������entity�Ѿ���ȡ��Player�ű��ˣ��������ﻹ��ϰ����player
    Player player;

    #region Skill
    [Header("Skill Stats")]
    //Ͷ�������ܵĸ��ӵĶ����˺����ʶ��ɽ����˺��ǵ����ԭ�����˺����������жϵȣ��������������ֵ
    //�����ĺô��ǲ���Ϊ�����˺�������㱩���ʵ���Ϣ�ˣ��������Ҫ�ģ�����ʱ����
    public Stat swordExtraDamage;
    #endregion

    protected override void Start()
    {
        base.Start();

        //��player=entity��Ч
        player = PlayerManager.instance.player;
    }

    #region ChangePlayerStat
    public void EditPlayerStat(StatType _statType, int _modify)
    //�������ν���Ϸ�ж�������мӵ㴦��ԭ����ֻ����������ֵAttributes���мӵ�
    {
        //������������ԭ����ֵ�Ļ����ϼ���_modify��ֵ����������
        if (_statType == StatType.strength) { this.strength.SetValue(this.strength.GetValue() + _modify); }
        if (_statType == StatType.agility) { this.agility.SetValue(this.agility.GetValue() + _modify); }
        if (_statType == StatType.vitality) { this.vitality.SetValue(this.vitality.GetValue() + _modify); }
        if (_statType == StatType.intelligence) { this.intelligence.SetValue(this.intelligence.GetValue() + _modify); }
    }
    #endregion

    #region DamagedOverride
    public override void GetMagicalDamagedBy(int _damage)
    {
        //��̵�ʱ�򲻴����ܻ�
        if (player.stateMachine.currentState == player.dashState)
            return;
        base.GetMagicalDamagedBy(_damage);
    }
    public override void GetPhysicalDamagedBy(int _damage)
    {
        //��̵�ʱ�򲻴����ܻ�
        if (player.stateMachine.currentState == player.dashState)
            return;
        base.GetPhysicalDamagedBy(_damage);
    }
    #endregion

    #region DieOverride
    public override void StatsDie()
    {
        base.StatsDie();
    }
    #endregion

    #region ISaveManager
    public void LoadData(GameData _data)
    {
        //��ɫ����ֵ
        this.strength.SetValue(_data.strength);
        this.agility.SetValue(_data.agility);
        this.vitality.SetValue(_data.vitality);
        this.intelligence.SetValue(_data.intelligence);

        //ԭʼ�������ֵ��δ���ӳɣ�
        this.originalMaxHealth.SetValue(_data.originalMaxHealth);

        //����������ԣ�δ���ӳɣ�
        this.criticPower.SetValue(_data.criticPower);
        this.criticChance.SetValue(_data.criticChance);

        //���������δ���ӳɣ�
        this.primaryPhysicalDamage.SetValue(_data.primaryPhysicalDamage);
        this.swordExtraDamage.SetValue(_data.swordExtraDamage);
        this.fireAttackDamage.SetValue(_data.fireAttackDamage);
        this.iceAttackDamage.SetValue(_data.iceAttackDamage);
        this.lightningAttackDamage.SetValue(_data.lightningAttackDamage);

        //�����������δ���ӳɣ�
        this.evasionChance.SetValue(_data.evasionChance);
        this.physicalArmor.SetValue(_data.physicalArmor);
        this.magicalResistance.SetValue(_data.magicalResistance);
    }

    public void SaveData(ref GameData _data)
    //����Ϸ�ڽ���������ֵ�ļӵ㣬����б��棬���ֻ�����������ֵ���мӵ㣬��ֻ��Ҫ������Щ
    {
        //��ɫ����ֵ
        _data.strength = this.strength.GetValue();
        _data.agility = this.agility.GetValue();
        _data.vitality = this.vitality.GetValue();
        _data.intelligence = this.intelligence.GetValue();

        //ԭʼ�������ֵ��δ���ӳɣ�
        _data.originalMaxHealth = this.originalMaxHealth.GetValue();

        //����������ԣ�δ���ӳɣ�
        _data.criticPower = this.criticPower.GetValue();
        _data.criticChance = this.criticChance.GetValue();

        //����ͷ�����������δ���ӳɣ�
        _data.primaryPhysicalDamage = this.primaryPhysicalDamage.GetValue();
        _data.fireAttackDamage = this.fireAttackDamage.GetValue();
        _data.iceAttackDamage = this.iceAttackDamage.GetValue();
        _data.lightningAttackDamage = this.lightningAttackDamage.GetValue();

        //���ܹ�������δ���ӳɣ�
        _data.swordExtraDamage = this.swordExtraDamage.GetValue();

        //�����������δ���ӳɣ�
        _data.evasionChance = this.evasionChance.GetValue();
        _data.physicalArmor = this.physicalArmor.GetValue();
        _data.magicalResistance = this.magicalResistance.GetValue();
    }
    #endregion
}
