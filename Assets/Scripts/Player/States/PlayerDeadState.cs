using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //����������Ļ�Ľ���
        UI.instance.fadeScreen.GetComponent<UI_FadeScreen>().FadeOut();

        //������������
        UI.instance.PlayDeathText();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //���˾Ͳ��ܶ���
        player.SetVelocity(0, 0);
    }
}
