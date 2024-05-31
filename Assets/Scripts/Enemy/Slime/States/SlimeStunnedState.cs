using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStunnedState : EnemyState
{
    private Slime slime;

    public SlimeStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Slime _slime) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.slime = _slime;
    }

    public override void Enter()
    {
        base.Enter();

        //�����ʱ��ѣ��ʱ��
        stateTimer = slime.stunnedDuration;

        //����һ���������������RedBlink����һֱ��Invoke���ã��ڶ������������ӳ٣�delay����ú��һ��ִ������������������Ǻ����ͷż��
        slime.fx.InvokeRepeating("RedBlink", 0, 0.1f);
    }

    public override void Exit()
    {
        base.Exit();

        //�뿪ʱȡ����ɫ���⣻�ڶ������������ӳٵ��ô˺�������˼
        slime.fx.Invoke("CancelRedBlink", 0);
    }

    public override void Update()
    {
        base.Update();

        //��������ʱ��ʵ���ϱ���ҹ������ˣ��ᴥ��knockback�����ʱ��λ��ʮ�ֹ��죬�������õĻ��ͱ�������������߹켣�ˣ�����������
        slime.SetVelocity(0, 0);

        //ѣ�ν��������idle
        if (stateTimer < 0)
            slime.stateMachine.ChangeState(slime.idleState);
    }
}
