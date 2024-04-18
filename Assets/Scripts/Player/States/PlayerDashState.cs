using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //���ʱ�䣬ֻ����һ�Σ��ʷ���Enter
        stateTimer = player.dashDuration;

        //�����̣�����canDashΪ�٣���GroundedState������������ֻ�е��Ӵ��˵����ǽ�ں��ֵ���ָܻ�Ϊ��
        player.CanDashSetting(false);
    }

    public override void Exit()
    {
        base.Exit();

        //���������ٶȣ����Լ�������˶���ˮƽ�ٶȣ��������ԸУ�����0.2f
        //player.SetVelocity(0.2f * rb.velocity.x, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        //��stateTimer����������ֹͣ���
        if (stateTimer > 0 && xInput != 0)
        {
            //���ʱ��Ҫ����ֱ�ٶ�Ϊ��
            player.SetVelocity(xInput * player.dashSpeed, 0);
        }
        //��ˮƽ�ٶ�Ϊ���ʱ���泯������
        if (stateTimer > 0 && xInput == 0)
        {
            player.SetVelocity(player.facingDir * player.dashSpeed, 0);
        }

        //���ʱ�����ʱ�������ڵ����ϣ���ת����IdleState���������ڿ��г�̣����л���AirState��׹״̬
        if (player.isGround)
        {
            if (stateTimer <= 0)
            {
                player.stateMachine.ChangeState(player.idleState);
            }
        }
        else if(!player.isGround)
        {
            if (stateTimer <= 0)
            {
                player.stateMachine.ChangeState(player.airState);
            }
        }

        //��ǽ����ص��ж�
        if(!player.isGround && player.isWall)
        {
            //���Գ���뿪ǽ��
            if(player.facingDir != xInput)
            {
                player.Flip();
            }
            //��̵�ǽ����������WallState�����ǽ���AirState�ٽ���WallState
            else if(player.facingDir == xInput)
            {
                player.stateMachine.ChangeState(player.wallSlideState);
            }
        }

    }
}
