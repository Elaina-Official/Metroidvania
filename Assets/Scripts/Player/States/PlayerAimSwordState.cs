using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        //�����״̬ʱ�򿪸�����׼�켣���ߣ�������������ʱ������Ϊfalse���������ڴ�״̬����ʱ����Ϊ��������ʱ�������throwSwordState���м�ĳʱ���
        player.skill.swordSkill.ActivateDots(true);

        //��׼ʱ��ֹ
        player.SetVelocity(0, 0);

        //��������׼�ķ������ɫ�泯�����෴����ת��
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > player.transform.position.x)
        {
            if (player.facingDir == -1)
                player.Flip();
        }
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < player.transform.position.x)
        {
            if (player.facingDir == 1)
                player.Flip();
        }

        //�ɿ�����м����뿪��״̬������Ͷ��״̬
        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            player.stateMachine.ChangeState(player.throwSwordState);
        }
    }
}
