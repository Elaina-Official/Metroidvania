using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUntouchedState : PlayerState
//�����ڿ��е�״̬��Jump��Air
{
    public PlayerUntouchedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
    }
}
