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

        #region BasicInput
        //�ڵ����״̬ʱ������Idle��Move���������ո����ڵ�����ʱ���������Ծ״̬
        if (Input.GetKeyDown(KeyCode.Space) && player.isGround)
        {
            //����ҪUI��ʾ��ʱ�򣬲��ܽ��д��˶�
            if (UI_MainScene.instance.ActivatedStateOfMainUIs() == true)
                return;

            stateMachine.ChangeState(player.jumpState);
        }
        //�ڵ����ϰ���J���������������빥��״̬
        if((Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Mouse0)) && player.isGround)
        {
            //����ҪUI��ʾ��ʱ�򣬲��ܽ��д��˶�
            if (UI_MainScene.instance.ActivatedStateOfMainUIs() == true)
                return;

            player.stateMachine.ChangeState(player.primaryAttackState);
        }
        //�ڵ����ϰ���K��������Ҽ������������״̬
        if((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Mouse1)) && player.isGround)
        {
            //����ҪUI��ʾ��ʱ�򣬲��ܽ��д��˶�
            if (UI_MainScene.instance.ActivatedStateOfMainUIs() == true)
                return;

            player.stateMachine.ChangeState(player.counterAttackState);
        }
        #endregion

        #region SkillInput
        //�ڵ����ϰ�������м�������׼״̬��ǰ���ǿ���Ͷ�����������Ͷ������Ϊ1�������еĽ�û����ȥ���ſ���Ͷ��
        if (Input.GetKeyDown(KeyCode.Mouse2) && player.isGround && !PlayerSkillManager.instance.assignedSword)
        {
            //��������
            if (PlayerManager.instance.ability_CanThrowSword == false)
                return;

            //����ҪUI��ʾ��ʱ�򣬲��ܽ��д��˶�
            if (UI_MainScene.instance.ActivatedStateOfMainUIs() == true)
                return;

            player.stateMachine.ChangeState(player.aimSwordState);
        }
        //������Ͷ����ȥ���˺��ٰ�һ���м�����ʹ�����ص��������
        if (PlayerSkillManager.instance.assignedSword && Input.GetKeyDown(KeyCode.Mouse2))
        {
            //����ҪUI��ʾ��ʱ�򣬲��ܽ��д��˶�
            if (UI_MainScene.instance.ActivatedStateOfMainUIs() == true)
                return;

            //������Ҷ���ȥ�Ľ�����ķ��غ���
            PlayerSkillManager.instance.assignedSword.GetComponent<Sword_Controller>().ReturnTheSword();
        }
        //������
        if (Input.GetKeyDown(KeyCode.Alpha1) && player.isGround)
        {
            //��������
            if (PlayerManager.instance.ability_CanFireBall == false)
                return;

            //��ȴʱ�����ã�Ӧ���ȼ�ⲻ���ã���Ȼ���ں���Ļ����ռ����������ã���ôһ���ᱻ����������
            if (!PlayerSkillManager.instance.fireballSkill.CanUseSkill())
            {
                //�������ֵ���Ч������ʾ���ܴ�����ȴ
                PlayerManager.instance.player.fx.CreatPopUpText("Cooldown", Color.white);
            }
            if (PlayerSkillManager.instance.fireballSkill.CanUseSkill() && !PlayerSkillManager.instance.assignedFireBall)
            {
                //����ҪUI��ʾ��ʱ�򣬲��ܽ��д��˶�
                if (UI_MainScene.instance.ActivatedStateOfMainUIs() == true)
                    return;

                //�����λ�ã����������Է���������
                PlayerSkillManager.instance.fireballSkill.CreateFireBall(player.transform.position, player.facingDir);
            }
        }
        //������
        if (Input.GetKeyDown(KeyCode.Alpha2) && player.isGround)
        {
            //��������
            if (PlayerManager.instance.ability_CanIceBall == false)
                return;

            //��ȴʱ�����ã�Ӧ���ȼ�ⲻ���ã���Ȼ���ں���Ļ����ռ����������ã���ôһ���ᱻ����������
            if (!PlayerSkillManager.instance.iceballSkill.CanUseSkill())
            {
                //�������ֵ���Ч������ʾ���ܴ�����ȴ
                PlayerManager.instance.player.fx.CreatPopUpText("Cooldown", Color.white);
            }
            if (PlayerSkillManager.instance.iceballSkill.CanUseSkill() && !PlayerSkillManager.instance.assignedIceBall)
            {
                //����ҪUI��ʾ��ʱ�򣬲��ܽ��д��˶�
                if (UI_MainScene.instance.ActivatedStateOfMainUIs() == true)
                    return;

                //�����λ�ã����������Է���������
                PlayerSkillManager.instance.iceballSkill.CreateIceBall(player.transform.position, player.facingDir);
            }
        }
        #endregion
    }
}
