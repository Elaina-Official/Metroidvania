using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowSwordState : PlayerState
{
    public PlayerThrowSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
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

        //��׼ʱ��ֹ
        player.SetVelocity(0, 0);

        //�����������뿪��״̬����״̬ĩβ�����˺��ж���
        if(stateActionFinished)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
