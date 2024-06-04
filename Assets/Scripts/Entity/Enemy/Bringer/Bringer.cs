using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Bringer : Enemy
{
    #region States
    public BringerIdleState idleState { get; private set; }
    public BringerMoveState moveState { get; private set; }
    public BringerBattleState battleState { get; private set; }
    public BringerAttackState attackState { get; private set; }
    public BringerStunnedState stunnedState { get; private set; }
    public BringerDeadState deadState { get; private set; }
    #endregion

    #region Components
    public BringerStats sts {  get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        #region States
        //ע�⣬�ഫ����һ��this
        idleState = new BringerIdleState(this, stateMachine, "Idle", this);
        moveState = new BringerMoveState(this, stateMachine, "Move", this);
        //��Ϊ����ս��״̬����Ҫ�ӽ���ң����߹�ȥ����������Move����
        battleState = new BringerBattleState(this, stateMachine, "Move", this);
        attackState = new BringerAttackState(this, stateMachine, "Attack", this);
        stunnedState = new BringerStunnedState(this, stateMachine, "Stunned", this);
        deadState = new BringerDeadState(this, stateMachine, "Dead", this);
        #endregion
    }

    protected override void Start()
    {
        base.Start();

        #region Components
        sts = GetComponent<BringerStats>();
        #endregion

        //��վ��״̬��ʼ�������״̬��
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        //ս��״̬��������λ��
        BattleMoveDirCheck();
        //ս��״̬��Ҫ����facingDir��battleMoveDir����һ��
        KeepDirectionSame();
    }

    #region StunnedOverride
    public override bool WhetherCanBeStunned()
    //���״̬ת���������������attackState�����Ϊ����CounterAttackWindowֻ����attackState�Ķ����ﱻ���ã�������������л�״̬��û����
    //ͬʱ����Bringer�ű��ڻ�����д�������
    {
        if (base.WhetherCanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            
            return true;
        }
        return false;
    }
    #endregion

    #region BattleMove
    //ս��״̬��������λ��
    public void BattleMoveDirCheck()
    {
        //��Ӧ��attackState�б���⣬��ֹ������ʱ��ͻȻת����Ϊ�ڴ�״̬����battleMoveDir��facingDir�Ƿ�ͳһ�������������Ϊǰ�ߣ�
        if(stateMachine.currentState == battleState || stateMachine.currentState == idleState)
        {
            if (PlayerManager.instance.player.transform.position.x > (rb.position.x))
            {
                battleMoveDir = 1;
            }
            if (PlayerManager.instance.player.transform.position.x < (rb.position.x))
            {
                battleMoveDir = -1;
            }
       
            //����ֱ���ж�playerPos.position.x == rb.position.x����Ϊ����λ�����ڸ��㾫�Ȼ�ÿ֡λ�õ�Ӱ�첻������ȫ��ͬ
        }
    }
    public void KeepDirectionSame()
    {
        //ս��״̬��Ҫ����facingDir��battleMoveDir����һ�£���ֹ���ֱ���ɺ�Ī��ת�������
        if ((stateMachine.currentState == battleState) || (stateMachine.currentState == attackState) || (stateMachine.currentState == stunnedState))
        //if��ʹ��ʹ���˳�battle״̬��facingDir�ع�ԭ�����жϣ�Ҳ����˵battleMoveDirֻ����battle����attack����stunned״̬��ʹ��
        {
            if(facingDir != battleMoveDir)
            {
                Flip();
            }
        }
    }
    #endregion

    #region DieOverride
    protected override void DieDetect()
    {
        base.DieDetect();

        if(sts.currentHealth <= 0)
        {
            stateMachine.ChangeState(deadState);
        }
    }
    #endregion
}
