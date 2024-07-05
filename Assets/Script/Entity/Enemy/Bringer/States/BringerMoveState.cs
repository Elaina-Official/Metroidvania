using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerMoveState : BringerGroundedState
{
    public BringerMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Bringer _bringer) : base(_enemyBase, _stateMachine, _animBoolName, _bringer)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //�����ƶ��ٶ�
        bringer.SetVelocity(bringer.moveSpeed * bringer.facingDir, rb.velocity.y);

        //�������ǽ�ڻ������£�����ĵ������߷��ڹ����ǰ��һ�㣩����ת��
        if(bringer.isWall || !bringer.isGround)
        {
            bringer.Flip();

            //�л���վ��״̬����վ��ʱ��idleStayTime��ȥ�������ʼ�ƶ������л��Ļ��ᷴ��Flip������֣�
            bringer.stateMachine.ChangeState(bringer.idleState);
        }

        //������Һ󣬻�����Ȼ��������״̬������BattleState
        if (bringer.isPlayer || bringer.shouldEnterBattle)
        {
            //��������ģʽ��ֻ�г������뷶Χ��ʱ��Ż��false
            bringer.shouldEnterBattle = true;
            //����battle
            bringer.stateMachine.ChangeState(bringer.battleState);
        }
    }
}
