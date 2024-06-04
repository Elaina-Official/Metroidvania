using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BringerBattleState : EnemyState
{
    private Bringer bringer;

    public BringerBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Bringer _bringer) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.bringer = _bringer;
    }

    public override void Enter()
    {
        base.Enter();

        //����״̬�����ٶ�Ϊ�㣬��ֹ�����˺���ظı䳯��
        bringer.SetVelocity(0, 0);

        //������δ��������ս������idle״̬
        stateTimer = bringer.quitBattleTime;
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
        if (bringer.battleMoveDir != bringer.facingDir)
        {
            bringer.Flip();
        }
        //��һ����Ҫ�ߵ���ȫ������غϵ�λ�ã�Ԥ��һ��ռ䣬�����attackCheckRadius��0.65������
        if ((rb.position.x < (PlayerManager.instance.player.transform.position.x + bringer.attackCheckRadius * 0.65f)) && (rb.position.x > (PlayerManager.instance.player.transform.position.x - bringer.attackCheckRadius * 0.65f)))
        {
            //�ڷ�Χ�ڵ���ͣ����ת����վ��״̬����Ȼ��ֱ���ߵ��غ�
            bringer.SetVelocity(0, 0);
            //�����������Χ�ڵ�ʱ�򣬱���վ��
            bringer.stateMachine.ChangeState(bringer.idleState);
        }
        else
        {
            {
                //��ֹ����������ߵ�̫��
                if(!bringer.enterAttackRegion)
                {
                    //���賯����ҵ��ƶ��ٶ�
                    bringer.SetVelocity(bringer.moveSpeed * bringer.fasterSpeedInBattle * bringer.battleMoveDir, rb.velocity.y);
                }
                else
                {
                    //��ֹ��battleState��ԭ��̤��
                    bringer.stateMachine.ChangeState(bringer.idleState);
                }
            }
        }
        #endregion

        #region Attack
        //��������˾������һ���ľ��뷶Χ�ڣ����ҹ�����ȴ�������㷢������
        if (bringer.enterAttackRegion && bringer.canAttack)
        {
            bringer.stateMachine.ChangeState(bringer.attackState);
        }
        //��������ڿɹ����뾶�ڣ����������ײ�˺�
        //if(PlayerManager.instance.transform.position)
        #endregion

        #region CaseThatQuitBattle
        //����������£�����ս����idle
        if (!bringer.isGround)
        {
            bringer.stateMachine.ChangeState(bringer.idleState);
        }
        //��ŭʱ�䵽�˺󣬻�����Ҿ��볬����Χ������ս
        if (stateTimer < 0 || Vector2.Distance(bringer.transform.position, PlayerManager.instance.player.transform.position) > bringer.GetQuitBattleDisance())
        {
            //�ر�����״̬
            bringer.shouldEnterBattle = false;
            //�ص�վ��״̬
            bringer.stateMachine.ChangeState(bringer.idleState);
        }
        #endregion
    }
}
