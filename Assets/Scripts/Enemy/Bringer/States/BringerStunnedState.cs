using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerStunnedState : EnemyState
{
    private Bringer bringer;

    public BringerStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Bringer _bringer) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.bringer = _bringer;
    }

    public override void Enter()
    {
        base.Enter();

        //�����ʱ��ѣ��ʱ��
        stateTimer = bringer.stunnedDuration;

        //����һ���������������RedBlink����һֱ��Invoke���ã��ڶ������������ӳ٣�delay����ú��һ��ִ������������������Ǻ����ͷż��
        bringer.fx.InvokeRepeating("RedBlink", 0, 0.1f);
    }

    public override void Exit()
    {
        base.Exit();

        //�뿪ʱȡ����ɫ���⣻�ڶ������������ӳٵ��ô˺�������˼
        bringer.fx.Invoke("CancelRedBlink", 0);
    }

    public override void Update()
    {
        base.Update();

        //��������ʱ��ʵ���ϱ���ҹ������ˣ��ᴥ��knockback�����ʱ��λ��ʮ�ֹ��죬�������õĻ��ͱ�������������߹켣�ˣ�����������
        bringer.SetVelocity(0, 0);

        //ѣ�ν��������idle
        if(stateTimer < 0)
            bringer.stateMachine.ChangeState(bringer.idleState);
    }
}
