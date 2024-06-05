using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBattleState : EnemyState
{
    private Slime slime;

    public SlimeBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Slime _slime) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.slime = _slime;
    }

    public override void Enter()
    {
        base.Enter();

        //����״̬�����ٶ�Ϊ�㣬��ֹ�����˺���ظı䳯��
        slime.SetVelocity(0, 0);

        //������δ��������ս������idle״̬
        stateTimer = slime.quitBattleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        #region ApproachPlayer
        //�������ڹ���ı�������Ҫת��
        if (slime.battleMoveDir != slime.facingDir)
        {
            slime.Flip();
        }
        //��һ����Ҫ�ߵ���ȫ������غϵ�λ�ã�Ԥ��һ��ռ䣬�����attackCheckRadius��0.65������
        if ((rb.position.x < (PlayerManager.instance.player.transform.position.x + slime.attackCheckRadius * 0.65f)) && (rb.position.x > (PlayerManager.instance.player.transform.position.x - slime.attackCheckRadius * 0.65f)))
        {
            //�ڷ�Χ�ڵ���ͣ����ת����վ��״̬����Ȼ��ֱ���ߵ��غ�
            slime.SetVelocity(0, 0);
            //�����������Χ�ڵ�ʱ�򣬱���վ��
            slime.stateMachine.ChangeState(slime.idleState);
        }
        else
        {
            {
                //��ֹ����������ߵ�̫��
                if (!slime.enterAttackRegion)
                {
                    //���賯����ҵ��ƶ��ٶ�
                    slime.SetVelocity(slime.moveSpeed * slime.battleSpeedMultiplier * slime.battleMoveDir, rb.velocity.y);
                }
                else
                {
                    //��ֹ��battleState��ԭ��̤��
                    slime.stateMachine.ChangeState(slime.idleState);
                }
            }
        }
        #endregion

        #region Attack
        //��������˾������һ���ľ��뷶Χ�ڣ����ҹ�����ȴ�������㷢������
        if (slime.enterAttackRegion && slime.canAttack)
        {
            slime.stateMachine.ChangeState(slime.attackState);
        }
        //��������ڿɹ����뾶�ڣ����������ײ�˺�
        //if(PlayerManager.instance.transform.position)
        #endregion

        #region CaseThatQuitBattle
        //����������£�����ս����idle
        if (!slime.isGround)
        {
            slime.stateMachine.ChangeState(slime.idleState);
        }
        //��ŭʱ�䵽�˺󣬻�����Ҿ��볬����Χ������ս
        if (stateTimer < 0 || Vector2.Distance(slime.transform.position, PlayerManager.instance.player.transform.position) > slime.GetQuitBattleDisance())
        {
            //�ر�����״̬
            slime.shouldEnterBattle = false;
            //�ص�վ��״̬
            slime.stateMachine.ChangeState(slime.idleState);
        }
        #endregion
    }
}
