using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerAttackState : EnemyState
{
    private Bringer bringer;

    public BringerAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Bringer _bringer) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.bringer = _bringer;
    }

    public override void Enter()
    {
        base.Enter();

        //�����״̬��ֹ
        bringer.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();

        //ÿ�ι������ˢ�¹�����ȴ
        bringer.AttackCooldownRefresher();
    }

    public override void Update()
    {
        base.Update();

        //����ʱ���Ҷ���Ҳ���ɱ�����
        bringer.SetVelocity(0, 0);

        //����һ�κ󷵻�battleState
        if (stateActionFinished)
        {
            bringer.stateMachine.ChangeState(bringer.battleState);
        }

        //�ڹ���״̬�����������������canBeStunned��true״̬���л�״̬��stunnedState
        //�ⲿ��ת��״̬�Ĵ�����Bringer�ű�����д��WhetherCanBeStunned����ִ�У��������߼����ԣ�������������˵��һ�£���ֹ�Ҳ���
        //if (bringer.WhetherCanBeStunned){...}
    }
}
