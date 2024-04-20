using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
//����һ������Idle��Move����״̬�Ĵ�״̬��super state�������״̬��һ��PlayerState�������ڵ���ʱ���״̬�����ʼ̳���PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //ÿ����������ʱ����Ծ�����ָ�Ϊ2
        player.jumpNum = 2;

        //ÿ�������������ǽ�ں�����Լ�������ȴ��������г��
        player.CanDashSetting(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //�ڵ����״̬ʱ������Idle��Move���������ո����ڵ�����ʱ���������Ծ״̬
        if(Input.GetKeyDown(KeyCode.Space) && player.isGround)
            stateMachine.ChangeState(player.jumpState);

        //�ڵ����ϰ���J���������������빥��״̬
        if((Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Mouse0)) && player.isGround)
            player.stateMachine.ChangeState(player.primaryAttack);

        //�ڵ����ϰ���K��������Ҽ������������״̬
        if((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Mouse1)) && player.isGround)
            player.stateMachine.ChangeState(player.counterAttackState);

        #region SwordSkill
        //�ڵ����ϰ�������м�������׼״̬
        //ǰ���ǿ���Ͷ�����������Ͷ������Ϊ1�������еĽ�û����ȥ���ſ���Ͷ��
        if (Input.GetKeyDown(KeyCode.Mouse2) && player.isGround && !player.assignedSword)
            player.stateMachine.ChangeState(player.aimSwordState);

        //������Ͷ����ȥ���˺��ٰ�һ���м�����ʹ�����ص��������
        if (PlayerManager.instance.player.assignedSword && Input.GetKeyDown(KeyCode.Mouse2))
        {
            //������Ҷ���ȥ�Ľ�����ķ��غ���
            player.assignedSword.GetComponent<Sword_Controller>().ReturnTheSword();
        }
        #endregion
    }
}
