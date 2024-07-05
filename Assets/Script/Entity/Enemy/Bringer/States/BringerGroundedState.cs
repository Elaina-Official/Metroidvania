using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerGroundedState : EnemyState
{
    //����Ϊ����Enemy������һ�����֣�protectedΪ�˸�idle��move״̬�̳У�һ��private����
    protected Bringer bringer;

    public BringerGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Bringer _bringer) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        //ע������ഫ����һ���ض��������Bringer�Ĳ���������Ϊ����Enemy������һ�����֣������ڴ���BringerIdle��ʱҪ�ഫ����������������
        this.bringer = _bringer;
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
