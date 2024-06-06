using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //����״̬��Ч����ʱ��
        stateTimer = player.counterAttackDuration;
        //�������������������������ص�parameters����������Player�ű����½�������״̬�Ķ��������
        //ֵ��һ�ᣬ����ֻ��Ҫһ��PlayerCounterAttackState�ű����ɣ����ڿ��Ƶ���׼��״̬�ͳɹ�״̬��������
        player.anim.SetBool("SuccessCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //����������ʱ����Ҷ���
        player.SetVelocity(0, 0);

        //����һ����ʱ���飬�����ʱ�����﹥�����Ȧ�ڵ�����ʵ��
        Collider2D[] collidersInAttackZone = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        //ѭ���������������ڵĵ���ʵ�壬���е����ж�
        foreach (var beHitEntity in collidersInAttackZone)
        {
            //�Ե�����ʵ����ɵ���ѣ��
            if (beHitEntity.GetComponent<Enemy>() != null)
            {
                //������ǰ���ǵ��˴�������״̬
                if(beHitEntity.GetComponent<Enemy>().WhetherCanBeStunned())
                {
                    //����һ����������ֹ���������Ǹ�if��(stateTimer < 0)����
                    stateTimer = 100;

                    //���ص����ɹ���Ѷ��
                    player.anim.SetBool("SuccessCounterAttack", true);

                    //�����ɹ�����Ч
                    AudioManager.instance.PlaySFX(15, null);
                }
            }
        }

        //������ʱ����������ߵ����Ķ�������ˣ�AnimationTrigger�ĺ��������ã�����ص�idle
        if(stateTimer < 0 || stateActionFinished)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
