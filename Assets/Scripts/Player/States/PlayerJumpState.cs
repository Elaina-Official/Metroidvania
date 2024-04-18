using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerUntouchedState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //������Ծ״̬ʱ����һ��˲������ϵ��ٶȣ�����ٶ�ֻ��Ҫ��һ�Σ��ʶ�����Ҫ����Update��һֱ����
        player.SetVelocity(rb.velocity.x, player.jumpForce);

        //����һ����Ծ״̬������Ծ������һ
        player.jumpNum--;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //����������Ծ����ʱ��������ȵ���׹���ܶ��������ٰ�һ�¿��Կ�������
        if (Input.GetKeyDown(KeyCode.Space) && player.jumpNum > 0)
        {
            player.stateMachine.ChangeState(player.jumpState);
        }

        //�����ϵ��ٶ�Ϊ������ת��Ϊ����״̬
        if (rb.velocity.y < 0)
        {
            player.stateMachine.ChangeState(player.airState);
        }

        //��Ծ������Ҳ���������ƶ������ǻ���һ��
        player.SetVelocity(xInput * player.airMoveSpeedRate * player.moveSpeed, rb.velocity.y);
    }
}
