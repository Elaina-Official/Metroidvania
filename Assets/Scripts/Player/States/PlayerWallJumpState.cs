using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //��ǽ������ˮƽ���ٳ���ʱ��
        stateTimer = player.wallJumpReverseSpeedDuration;

        //��תһ�£���ֹ�������죻��Ҳ��֪��ΪʲôҪ���������ǲ������ͻ��bug
        player.Flip();
        //Debug.Log("WallJump�е�Flip������");
    }

    public override void Exit()
    {
        base.Exit();

        //���ڴ�״̬���Ϊ�棬��һ״̬airState�����̸�ֵΪ��
        player.FromWallJumpToAirStateSetting(true);
    }

    public override void Update()
    {
        base.Update();

        //���跴��ǽ�ڵ�ˮƽ�ٶȺ���ֱ��Ծ�ٶȣ�������Enter���Ѿ�Flip��ת���ˣ�����moveSpeed���ϵĲ��Ǹ���facingDir
        player.SetVelocity(player.moveSpeed * player.facingDir * 0.5f, player.jumpForce * 0.8f);

        //ʱ�䵽�˺����׹��ģʽ
        if(stateTimer < 0)
        {
            player.stateMachine.ChangeState(player.airState);
        }
    }
}
