using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerIdleState : BringerGroundedState
{
    public BringerIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Bringer _bringer) : base(_enemyBase, _stateMachine, _animBoolName, _bringer)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //վ��ʱ��
        stateTimer = bringer.pauseTime;

        //�����״̬��ֹ
        bringer.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //����վ����һ����Զ���ʼ�ƶ�
        if(bringer.isGround && stateTimer < 0)
        {
            bringer.stateMachine.ChangeState(bringer.moveState);
        }

        //������Һ󣬻�����Ȼ��������״̬������BattleState
        if (bringer.isPlayer || bringer.shouldEnterBattle)
        {
            if (stateTimer < 0)
            {
                //�������ڹ���ı�������Ҫת��
                if (bringer.battleMoveDir != bringer.facingDir)
                {
                    bringer.Flip();
                }
                //��������˾������һ���ľ��뷶Χ�ڣ����ҹ�����ȴ�������㷢������
                if (bringer.enterAttackRegion && bringer.canAttack)
                {
                    bringer.stateMachine.ChangeState(bringer.attackState);
                }
                else
                {
                    //��������ģʽ��ֻ�г������뷶Χ��ʱ��Ż��false
                    bringer.shouldEnterBattle = true;
                    //����battle
                    bringer.stateMachine.ChangeState(bringer.battleState);
                }
            }
        }
    }
}
