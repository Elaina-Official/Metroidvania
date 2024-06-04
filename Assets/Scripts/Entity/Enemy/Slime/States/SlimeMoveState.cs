using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveState : SlimeGroundedState
{
    public SlimeMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Slime _slime) : base(_enemyBase, _stateMachine, _animBoolName, _slime)
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
        slime.SetVelocity(slime.moveSpeed * slime.facingDir, rb.velocity.y);

        //�������ǽ�ڻ������£�����ĵ������߷��ڹ����ǰ��һ�㣩����ת��
        if(slime.isWall || !slime.isGround)
        {
            slime.Flip();

            //�л���վ��״̬����վ��ʱ��idleStayTime��ȥ�������ʼ�ƶ������л��Ļ��ᷴ��Flip������֣�
            slime.stateMachine.ChangeState(slime.idleState);
        }

        //������Һ󣬻�����Ȼ��������״̬������BattleState
        if (slime.isPlayer || slime.shouldEnterBattle)
        {
            //��������ģʽ��ֻ�г������뷶Χ��ʱ��Ż��false
            slime.shouldEnterBattle = true;
            //����battle
            slime.stateMachine.ChangeState(slime.battleState);
        }
    }
}
