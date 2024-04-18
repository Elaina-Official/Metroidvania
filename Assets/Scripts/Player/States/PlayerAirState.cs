using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerUntouchedState
//�ڿ������������״̬
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        //�˱������ڴ�ǽ��״̬���뱾״̬ʱ��Ϊ�棬���air��������Ҫ��Ϊ��
        player.FromWallJumpToAirStateSetting(false);
    }

    public override void Update()
    {
        base.Update();

        //��������׹��ʱ����ʣ�����Ծ����Ϊ1���ſɽ��ж�����
        if(Input.GetKeyDown(KeyCode.Space) && player.jumpNum == 1)
        {
            player.stateMachine.ChangeState(player.jumpState);
        }

        //���ڵ����ϣ���ת��Ϊվ��״̬
        if(player.isGround)
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

        //����Ǵ�ǽ��״̬ת�ƹ�����״̬�ģ�����Ҫ����ԭ��ˮƽ�ٶȣ�������A/D��������ֱ���
        if (player.isFromWallJumpToAirState)
        {
            //�˼�����ֶ�����A/D������ǰ�����ֶ�����ģʽ�����̳�ԭ��ˮƽ�ٶ�
            if(xInput != 0)
            {
                //������Ҫ��air������ű�٣���ʱ��ǰ���
                player.FromWallJumpToAirStateSetting(false);
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
