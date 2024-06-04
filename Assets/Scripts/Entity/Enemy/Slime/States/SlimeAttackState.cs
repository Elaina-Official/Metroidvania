using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : EnemyState
{
    private Slime slime;

    public SlimeAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Slime _slime) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.slime = _slime;
    }

    public override void Enter()
    {
        base.Enter();

        //�����״̬��ֹ
        slime.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();

        //ÿ�ι������ˢ�¹�����ȴ
        slime.AttackCooldownRefresher();
    }

    public override void Update()
    {
        base.Update();

        //����ʱ���Ҷ���Ҳ���ɱ�����
        slime.SetVelocity(0, 0);


        //����һ�κ󷵻�battleState
        if (stateActionFinished)
        {
            slime.stateMachine.ChangeState(slime.battleState);
        }

        //�ڹ���״̬�����������������canBeStunned��true״̬���л�״̬��stunnedState
        //�ⲿ��ת��״̬�Ĵ�����Slime�ű�����д��WhetherCanBeStunned����ִ�У��������߼����ԣ�������������˵��һ�£���ֹ�Ҳ���
        //if (slime.WhetherCanBeStunned){...}
    }
}
