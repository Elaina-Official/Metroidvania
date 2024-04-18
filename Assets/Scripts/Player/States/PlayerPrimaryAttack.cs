using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    #region ComboSettings
    //�����ж������õļ�����
    private int comboCounter = 1;
    //���һ�ι�����ʱ��
    private float lastTimeAttack;
    //��ò��������˳��������ۻ����ӵ�һ�����п�ʼ
    private float comboRefreshDuration = 1;
    #endregion

    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        #region ComboCounter
        //������������������������1������һ�ι����ع��һ�й�����
        //�������֮����̫�ã�����comboRefreshDuration����������´ӵ�һ�п�ʼ����
        if (comboCounter > 3 || (lastTimeAttack + comboRefreshDuration < Time.time))
        {
            comboCounter = 1;
        }
        //Debug.Log(comboCounter);

        //��δ������������棬��֤��������1�����Ҳ�ܽ��յ�
        //��AttackStack�ڵ�comboCounter��Animator�ڵĶ�ӦParameter��������
        player.anim.SetInteger("ComboCounter", comboCounter);
        #endregion

        #region AttackDetails
        //�ܶ����빥��ʱ���������ܹ�ά��ԭ���ٶ�һ��ʱ�䣬���ֹ��Ը�
        stateTimer = player.runIntoAttackInertiaDuration;
        
        /*�����ã��������������
        float attackDir = player.facingDir;
        //��������м���ı乥�����򣬻���һ���ӳ٣����������ֶ�����ת��������
        if(xInput != 0)
        {
            attackDir = xInput;
        }
        */

        //��ͬ�ι���������ͬ�ε�λ�ƣ���������ʸ�������ٶȣ��ý�ɫ����ʱ��һ�����ϵĶ�����������
        player.SetVelocity(player.attackMovement[comboCounter - 1].x * player.facingDir, player.attackMovement[comboCounter - 1].y);
        #endregion
    }

    public override void Exit()
    {
        base.Exit();

        //�������״̬�����Player.cs�ű��е�primaryAttack���ĳ�ԱcomboCounter����������
        comboCounter++;

        //��¼���һ�ι�����ʱ���
        lastTimeAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();

        //����״̬ʱ��ά��ԭ�����ٶ�һ��ʱ�䣬ʱ���������ֹ
        if (stateTimer < 0)
        {
            player.SetVelocity(0, 0);
        }

        //�����һ�ι�������������idle״̬����Ϊ����ֻ���ڵ��ϱ����������Բ��õ������ǿ��й������������idle���޷�����air
        if (stateActionFinished)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
