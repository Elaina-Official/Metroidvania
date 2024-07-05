using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    #region States
    public SlimeIdleState idleState { get; private set; }
    public SlimeMoveState moveState { get; private set; }
    public SlimeBattleState battleState { get; private set; }
    public SlimeAttackState attackState { get; private set; }
    public SlimeStunnedState stunnedState { get; private set; }
    public SlimeDeadState deadState { get; private set; }
    #endregion

    #region Components
    public SlimeStats sts { get; private set; }
    #endregion

    #region SlimeSplit
    [Header("Slime Split")]
    //ʷ��ķ���Ѻ����������һ��ʷ��ķ
    public int splitSlimeCount;
    //���ѳ�������һ��ʷ��ķ����С��ʷ��ķ������
    public GameObject splitSlime;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        #region States
        //ע�⣬�ഫ����һ��this
        idleState = new SlimeIdleState(this, stateMachine, "Idle", this);
        moveState = new SlimeMoveState(this, stateMachine, "Move", this);
        //��Ϊ����ս��״̬����Ҫ�ӽ���ң����߹�ȥ����������Move����
        battleState = new SlimeBattleState(this, stateMachine, "Move", this);
        attackState = new SlimeAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SlimeStunnedState(this, stateMachine, "Stunned", this);
        deadState = new SlimeDeadState(this, stateMachine, "Dead", this);
        #endregion
    }

    protected override void Start()
    {
        base.Start();

        #region Components
        sts = GetComponent<SlimeStats>();
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
    //ͬʱ����Slime�ű��ڻ�����д�������
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
        if (stateMachine.currentState == battleState || stateMachine.currentState == idleState)
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
            if (facingDir != battleMoveDir)
            {
                Flip();
            }
        }
    }
    #endregion

    #region SlimeSplit
    /*public void CreatSplitSlime(GameObject _splitPrefab, int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            //���λ��������һ��ʷ��ķ
            float _xPos = UnityEngine.Random.Range(-2f, 2f);

            //����������һ��ʷ��ķ
            GameObject _newSlime = Instantiate(_splitPrefab, transform.position + new Vector3(_xPos, 0), Quaternion.identity);
        }
    }*/

    public void CreatSplitSlime(GameObject _splitPrefab, int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            //����������һ��ʷ��ķ
            GameObject _newSlime = Instantiate(_splitPrefab, transform.position, Quaternion.identity);
            //������ѳ�����ʷ��ķһ�����ⷽ��ĳ��ٶ�
            _newSlime.GetComponent<Slime>().SetupSplitVector();
        }
    }
    public void SetupSplitVector()
    //�����ѳ�����ʷ��ķһ�����ٶȣ������ж���
    {
        //����һ����Χ��������ٶ������������ǿ򶨷�Χ
        float dx = Random.Range(-5, 5);
        float dy = Random.Range(6, 9);

        //��ʵ�崦��isKnocked״̬��ʱ��ʩ�ӵ��ٶ��������ᱻԭ����SetVelocity���ٶ������ǣ��ʶ��ﵽ�������ٶ�Ч��
        isKnocked = true;

        //������ʩ��
        GetComponent<Rigidbody2D>().velocity = new Vector2(dx, dy);
        
        //������isKnocked��ֵΪfalse
        Invoke("CancelSliptVector", 1f);
    }
    private void CancelSliptVector() => isKnocked = false;
    #endregion

    #region DieOverride
    protected override void DieDetect()
    {
        base.DieDetect();

        if (sts.currentHealth <= 0)
        {
            stateMachine.ChangeState(deadState);
        }
    }
    #endregion
}
