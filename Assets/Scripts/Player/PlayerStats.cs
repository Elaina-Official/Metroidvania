using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
{
    //��ʵֱ����etity�ͺã��������entity�Ѿ���ȡ��Player�ű��ˣ��������ﻹ��ϰ����player
    Player player;

    #region Skill
    [Header("Skill Stats")]
    //Ͷ�������ܵĸ��ӵĶ����˺����ʶ��ɽ����˺���primaryAttackDamage�ļӳɺ󣨱����жϵȣ��������������ֵ
    //�����ĺô��ǲ���Ϊ�����˺�������㱩���ʵ���Ϣ�ˣ��������Ҫ�ģ�����ʱ����
    public Stat extraSwordDamage;
    #endregion

    protected override void Start()
    {
        base.Start();

        //��player = entity��Ч
        player = PlayerManager.instance.player;
    }

    public override void GetDamagedBy(int _damage)
    {
        //��̵�ʱ�򲻴����ܻ�
        if (player.stateMachine.currentState == player.dashState)
        {
            return;
        }
        else
        {
            base.GetDamagedBy(_damage);
        }
        
    }

    public override void StatsDie()
    {
        base.StatsDie();
    }
}
