using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SlimeIdleState : SlimeGroundedState
{
    public SlimeIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Slime _slime) : base(_enemyBase, _stateMachine, _animBoolName, _slime)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //վ��ʱ��
        stateTimer = slime.pauseTime;

        //�����״̬��ֹ
        slime.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //����վ����һ����Զ���ʼ�ƶ�
        if (slime.isGround && stateTimer < 0)
        {
            slime.stateMachine.ChangeState(slime.moveState);
        }

        //������Һ󣬻�����Ȼ��������״̬������BattleState
        if (slime.isPlayer || slime.shouldEnterBattle)
        {
            if (stateTimer < 0)
            {
                //�������ڹ���ı�������Ҫת��
                if (slime.battleMoveDir != slime.facingDir)
                {
                    slime.Flip();
                }
                //��������˾������һ���ľ��뷶Χ�ڣ����ҹ�����ȴ�������㷢������
                if (slime.enterAttackRegion && slime.canAttack)
                {
                    slime.stateMachine.ChangeState(slime.attackState);
                }
                else
                {
                    //��������ģʽ��ֻ�г������뷶Χ��ʱ��Ż��false
                    slime.shouldEnterBattle = true;
                    //����battle
                    slime.stateMachine.ChangeState(slime.battleState);
                }
            }
        }
    }
}
