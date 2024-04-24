using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
{
    //���ٴ�����
    Player player;

    //Ͷ�������ܵ��˺�
    public Stat swordDamage;

    protected override void Start()
    {
        base.Start();

        player = PlayerManager.instance.player;
    }

    public override void GetDamaged(int _damage)
    {
        //��̵�ʱ�򲻴����ܻ�
        if (player.stateMachine.currentState != player.dashState)
        {
            //������ʱ�����öԷ��Ĺ�����ֵ�����Լ��ĵ�ǰ����ֵ�ϼ���
            currentHealth -= _damage;

            //�ܹ����Ĳ��ʱ仯��ʹ������˸�Ķ���Ч��
            player.fx.StartCoroutine("FlashHitFX");

            //������ʱ������һ��Ѫ��UI�ĸ���
            if (onHealthChanged != null)
            {
                onHealthChanged();
            }
        }
    }

    public override void StatsDie()
    {
        base.StatsDie();

        //������״̬��ת����deadState�ĵ���
        player.EntityDie();
    }
}
