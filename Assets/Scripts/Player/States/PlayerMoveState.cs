using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
//�̳��Ը����״̬�������ڵ����״̬
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    //������ƶ�״̬
    {
    }

    public override void Enter()
    {
        base.Enter();

        //������·��Ч
        //AudioManager.instance.PlaySFX(1, null);
    }

    public override void Exit()
    {
        base.Exit();

        //������·��Ч
        //AudioManager.instance.StopSFX(1);
    }

    public override void Update()
    {
        base.Update();

        //��xInput * moveSpeed��ֵ��ˮƽ�ٶȣ��õ�ǰ�ٶ�rb.velocity.y��ֵ��ֱ�ƶ��ٶȼ�����ԭ����ֱ�ٶȣ���û������ʱ��ˮƽ�ٶȳ���ֵΪ0��xInput
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        //����������ŵ������ܣ����Ų��ŵأ������airState
        if(!player.isGround)
        {
            player.stateMachine.ChangeState(player.airState);
        }

        //�������ǽ����ת�Ƶ�վ������վ����Ӧ��һ���жϣ��������ǽ������ǽ����Ӧת����Move״̬�����ӵĻ���ᵼ��Move��Idle���ز����л�
        if(player.isWall)
        {
            player.stateMachine.ChangeState(player.idleState);
        }

        //����ڵ�������ˮƽ���룬�����վ��״̬
        if (player.isGround && xInput == 0)
        {
            //ͨ���Լ���PlayerState�̳����ĳ�Աplayer�����player���ڱ�Plaer.cs��ʼ����ʱ�����ӵ�Player.cs���ָ���վ��״̬
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
