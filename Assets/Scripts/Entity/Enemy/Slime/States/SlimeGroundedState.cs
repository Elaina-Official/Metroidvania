using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGroundedState : EnemyState
{
    //����Ϊ����Slime������һ�����֣�protectedΪ�˸�idle��move״̬�̳У�һ��private����
    protected Slime slime;

    public SlimeGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Slime _slime) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        //ע������ഫ����һ���ض��������Slime�Ĳ���������Ϊ����Enemy������һ�����֣������ڴ���SlimeIdle��ʱҪ�ഫ����������������
        this.slime = _slime;
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
    }
}
