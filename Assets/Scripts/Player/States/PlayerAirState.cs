using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerUntouchedState
//�ڿ������������״̬
{
    //����Ҫ�ڰ���һ��A/D��λ��㲻���ٱ���ǽ�����ٶ��ˣ������ǰ���һ�κ����ɿ����ܼ�������
    private bool keepWallJumpVelocity;

    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //Ĭ������Ҫ���ֵ�
        keepWallJumpVelocity = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        #region DoubleJump
        //�������GroundedStateֱ�ӽ���AirStateʱ��������Ծ����Ϊ1
        if(player.stateMachine.formerState == player.idleState || player.stateMachine.formerState == player.moveState)
        {
            //��Ծ����Ϊ2�Ļ�����ʵ�Ƿϻ�������Ϊ���Է���һ��
            if (player.jumpNum == 2)
            {
                player.jumpNum = 1;
            }
        }
        //��������׹��ʱ����ʣ�����Ծ����Ϊ1�������й�һ����Ծ�����ɽ��ж�����
        if (Input.GetKeyDown(KeyCode.Space) && player.jumpNum == 1)
        {
            player.stateMachine.ChangeState(player.jumpState);
        }
        #endregion

        //���ڵ����ϣ���ת��Ϊվ��״̬
        if (player.isGround)
        {
            player.stateMachine.ChangeState(player.idleState);
        }

        //׹���ʱ���������ǽ������뻬ǽ״̬
        if (player.isWall && !player.isGround)
        {
            //���ܲ���������ߵ�ǽ�ڰ���A���ܽ��뻬ǽ״̬�ɣ�ע��������GetKey������GetKeyDown
            if( (Input.GetKey(KeyCode.A) && player.facingDir == -1) || (Input.GetKey(KeyCode.D) && player.facingDir == 1) )
            {
                player.stateMachine.ChangeState(player.wallSlideState);
                //Debug.Log("Air to WallSlide");
            }
        }

        //����Ǵ�ǽ��״̬ת�ƹ�����״̬�ģ�����ɱ���ԭ���ٶȣ�����Ҫ����ԭ��ˮƽ�ٶȣ�������A/D��������ֱ���
        if (player.stateMachine.formerState == player.wallJumpState && keepWallJumpVelocity)
        {
            //�˼�����ֶ�����A/D������ǰ�����ֶ�����ģʽ�����̳�ԭ��ˮƽ�ٶ�
            if(xInput != 0)
            {
                //����ˮƽ�ٶ����룬����ǰ�����ٶȵı���
                keepWallJumpVelocity = false;
            }
            else
            {
                //����ԭ��ˮƽ�ٶ�
                player.SetVelocity(rb.velocity.x, rb.velocity.y);
            }
        }
        else
        {
            //��׹������ͨ��A/D�������ƶ�
            player.SetVelocity(xInput * player.airMoveSpeedRate * player.moveSpeed, rb.velocity.y);
        }
    }
}
