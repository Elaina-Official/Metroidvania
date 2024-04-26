using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
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

        #region SetDefault
        //��ʼ�ɽ��˺�����Ϊ10
        swordExtraDamage.SetDefaultValue(10);
        #endregion

        //��player=entity��Ч
        player = PlayerManager.instance.player;
    }

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

    #region Die
    public override void StatsDie()
    {
        base.StatsDie();
    }
    #endregion
}
