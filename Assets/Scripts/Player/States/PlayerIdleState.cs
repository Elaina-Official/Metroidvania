using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
//�̳��Ը����״̬�������ڵ����״̬
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    //�����վ��״̬���˹��캯����ҪĿ���ǵ��û���Ĺ��캯�����������ĳ�ʼ��������
    //����base�ؼ��ֵ����˻���PlayerState�Ĺ��캯������������������ּ���ڴ���PlayerIdle����ʱ������ִ�л���PlayerState�Ĺ��캯����ȷ�������ʼ�������
    {
    }

    //�����Ƕ�PlayerState��һϵ����Ҫ��������д
    public override void Enter()
    {
        base.Enter();

        //����վ��״̬�ͱ�
        player.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    //����������ϵ��ڱ�Player�е�Update�������£�����ͬ��һֱ�ڸ���
    {
        base.Update();

        //�������ǽ�ڵ�������������жϣ�����ǽ���޷�ת�Ƶ�Move�������򱣳־�ֹ����������Խ���Move
        if (player.isWall)
        {
            //��������ǽ�����޷��ߵ�������xInputΪ���ʱ��Ҳ����������վ�Ų���������
            if(player.facingDir == xInput || xInput == 0)
            {
                //ֱ��ֹͣ�������ж��Ƿ�ִ�������if���µ�����
                return;
            }
            //����ǽ����
            else if(player.facingDir * xInput < 0)
            {
                //��֪Ϊ�Σ�����Ҫ�����������һ���ֶ���ת
                player.Flip();
                player.stateMachine.ChangeState(player.moveState);
            }
        }

        //���ڵ�����xˮƽ�����������ʱ��Ž����ƶ�״̬
        if(xInput != 0 && player.isGround)
        {
            //ͨ���Լ���PlayerState�̳����ĳ�Աplayer�����player���ڱ�Plaer.cs��ʼ����ʱ�����ӵ�Player.cs��ת��״̬���ƶ�״̬
            player.stateMachine.ChangeState(player.moveState);
        }
    }
}
