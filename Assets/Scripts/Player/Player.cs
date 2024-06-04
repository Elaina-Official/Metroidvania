using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Player : Entity
{
    #region States
    public PlayerStateMachine stateMachine {  get; private set; }
    //��������ܵ���������״̬������
    public PlayerIdleState idleState { get; private set; }
    //���������վ��״̬
    public PlayerMoveState moveState { get; private set; }
    //����������ƶ�״̬
    public PlayerJumpState jumpState { get; private set; }
    //�����������Ծ״̬
    public PlayerAirState airState { get; private set; }
    //���������׹��״̬
    public PlayerDashState dashState { get; private set; }
    //��������ĳ��״̬
    public playerWallSlideState wallSlideState { get; private set; }
    //��������Ļ�ǽ״̬
    public PlayerWallJumpState wallJumpState { get; private set; }
    //���������ǽ��״̬
    public PlayerPrimaryAttack primaryAttack { get; private set; }
    //��������Ĺ���״̬
    public PlayerCounterAttackState counterAttackState { get; private set; }
    //��������ĵ���״̬�����״̬������������ص�parameters
    public PlayerAimSwordState aimSwordState { get; private set; }
    //�����������׼״̬
    public PlayerThrowSwordState throwSwordState { get; private set;}
    //���������Ͷ��״̬
    public PlayerDeadState deadState { get; private set; }
    //��������״̬
    #endregion

    #region Components
    //��¼���������ͳ�ƽű�
    public PlayerStats sts {  get; private set; }
    #endregion

    #region Movement
    [Header("Player Movement Info")]
    //�����ڿ��е��ƶ��ٶ���moveSpeed��С��һ��
    public float airMoveSpeedRate = 0.9f;
    #endregion

    #region Jump
    [Header("Jump Info")]
    //��ʼ��Ծ��
    public float jumpForce = 15;
    //����ʣ��Ŀ���Ծ���������������Զ�����
    public int jumpNum = 2;
    #endregion

    #region Skills
    //ʹ�ô������࣬����PlayerSkillManager.instance.xxx��ֱ��player.skill.instance.xxx����
    public PlayerSkillManager skill {  get; private set; }
    //��¼�Ƿ��Ѿ�Ͷ����ȥ�˽�����ֹ����Ͷ������GroundedState�У���Ͷ����������ڴ�������Ƿ��Ѿ���������Prefab
    //�����player.assignedSword���Ե�boolֵʹ��
    public GameObject assignedSword {  get; private set; }
    #endregion

    #region Dash
    [Header("Dash Info")]
    //���ʱ��Ĭ��0.2�룬���ƶ��ٶȳ���dashSpeed���ʵĳ���ʱ��
    public float dashDuration = 0.2f;
    //����ٶ�Ҫ��moveSpeed�󣬲�Ȼ���г���ˣ�Ĭ��Ϊ26
    public float dashSpeed = 26;
    //�ڿ��г�̹�һ�κ󣬼�ʹ��ȴʱ�䵽��Ҳ���ܵڶ��γ��
    public bool canDash {  get; private set; } = true;

    //����PlayerSkillManager�������Skill�����DashSkill������������ؿɵ������ݣ����������б���
    //��ȴʱ�䳤��
    //public float dashCooldown = 0.6f;
    //��ȴʱ���ʱ��
    //private float dashCooldownTimer;
    #endregion

    #region AttackDetails
    [Header("Attack Details")]
    //�ܶ����빥��״̬ʱά��ԭ���ٶȣ����ԸУ���ʱ��
    public float runIntoAttackInertiaDuration = 0.1f;
    //���治ͬ�ι����Ĳ�ͬλ�Ƶ���������
    public Vector2[] attackMovement;

    //���������ܴ�������Чʱ��
    public float counterAttackDuration = 0.3f;
    #endregion

    #region WallSlide
    [Header("WallSlide Info")]
    //��ǽ���ٶȱ���
    public float slideSpeed = 0.9f;
    //��ǽ�ļ��������ٶȱ��ʣ�������ͨ��ǽ�ٶȱ��ʣ�
    public float biggerSlideSpeed = 0.99f;
    //ǽ������ˮƽ�ٶ�ʩ��ʱ��
    public float wallJumpReverseSpeedDuration = 0.1f;
    #endregion

    #region CDPlayer
    [Header("CDPlayer")]
    //�������������õ�������Ԥ����
    public GameObject cdPlayerPrefab;
    //���ڴ洢���ɺ���������������ڷ�ֹ���ɶ��������Ԥ����
    public GameObject assignedCDPlayer {  get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        #region States
        //�½�һ��״̬��
        stateMachine = new PlayerStateMachine();
        //����Player����ʹ�õ�״̬���������������Unity��Animator����Ӧ�ж�Parameter����"Idle"��������PlayerState��"idleState"�Ĺ���
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        //ȷ��PlayerState��������Ӱ���player������ű����Player�����������״̬�Ŀ�����Animator��Move������
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        //��ʼ����Ծ״̬
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        //ע������󶨵�Ҳ��Jump��������Ϊûʲô����
        airState = new PlayerAirState(this, stateMachine, "Jump");
        //��ʼ�����״̬
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        //��ʼ����ǽ״̬
        wallSlideState = new playerWallSlideState(this, stateMachine, "WallSlide");
        //��ʼ��ǽ��״̬
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "WallJump");
        //��ʼ����һ�ι���
        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
        //��ʼ����������
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        //��ʼ����׼����
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        //��ʼ��Ͷ��״̬
        throwSwordState = new PlayerThrowSwordState(this, stateMachine, "ThrowSword");
        //��ʼ������״̬
        deadState = new PlayerDeadState(this, stateMachine, "Dead");
        #endregion
    }

    protected override void Start()
    {
        base.Start();

        #region Components
        //����ͳ�ƽű�
        sts = GetComponent<PlayerStats>();
        #endregion

        //��վ��״̬��ʼ����ҵ�״̬��
        stateMachine.Initialize(idleState);
        //�򻯴���
        skill = PlayerSkillManager.instance;
    }

    protected override void Update()
    {
        #region GamePause
        //��Ϸ������ͣ״̬��ʱ�򣬲�ִ��������κ����
        if (Time.timeScale == 0)
            return;
        #endregion

        base.Update();

        //�˴���ͨ��MonoBehavior��Update���������ϵ���PlayerState���е�Update����������ˢ������״̬
        stateMachine.currentState.Update();
        //��������ĳ��״̬
        DashController();
        //���������������
        CDPlayerController();
    }

    #region Dash
    private void DashController()
    {
        //��ʱ����ʼ��ʱ
        //dashCooldownTimer -= Time.deltaTime;

        //���ܴӹ�������׼��Ͷ��״̬���
        if (stateMachine.currentState != primaryAttack && stateMachine.currentState != aimSwordState && stateMachine.currentState != throwSwordState)
        {
            //��̿��Դ����������״̬���뿪ʼ���ʶ����ڴ˴�Update�︳��������ȼ���ֻҪ������shift������ȴʱ��������������״̬��
            //ע������ʹ����PlayerSkillManager
            if (Input.GetKeyDown(KeyCode.LeftShift) && skill.dashSkill.WhetherCanUseSkill() && canDash)
            {
                stateMachine.ChangeState(dashState);
            }
        }
    }
    #endregion

    #region FlipControllerOverride
    public override void FlipController()
    {
        if (isKnocked)
            return;
        else
        {
            //����ģ����״ֱ̬�Ӹ������е����ҵ�����tm�����������⣡
            if(stateMachine.currentState != wallSlideState)
            {
                //��������һ��xInput���ƣ���ֹĪ�����ٶ�����������ת������
                if ((stateMachine.currentState.xInput > 0) && (rb.velocity.x > 0) && !facingRight)
                {
                    Flip();
                }
                if ((stateMachine.currentState.xInput < 0) && (rb.velocity.x < 0) && facingRight)
                {
                    Flip();
                }
            }
        }
    }
    #endregion

    #region Sword
    public void AssignNewSword(GameObject _newSword)
    {
        //��¼һ���½���һ����Prefab����CreateSword()�����б�����һ��
        assignedSword = _newSword;
    }
    public void ClearAssignedSword()
    {
        //���ٶ���Ľ�Prefab
        Destroy(assignedSword);
    }
    #endregion

    #region CDPlayer
    private void CDPlayerController()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //�������ٻ�����֮ǰ���ɹ�����������������ڵ�������������ifһ��Ҫ����һ��if��ǰ�棬��Ȼ�����ɵ�prefab�����̱������
            if (assignedCDPlayer != null)
            {
                //��Ч
                AudioManager.instance.PlaySFX(7, null);

                //��ֹ���ɶ��������
                Destroy(assignedCDPlayer);
            }
            //�������ٻ�����֮ǰû���ɹ����������򴴽�һ���µ�
            if(assignedCDPlayer == null)
            {
                //��Ч
                AudioManager.instance.PlaySFX(7, null);

                //��ʼ��������һ��������
                GameObject _newCDPlayer = Instantiate(cdPlayerPrefab, transform.position, transform.rotation);

                //��¼һ�£�������һ���µ�����������ֹ�����ٻ�
                assignedCDPlayer = _newCDPlayer;
            }
        }
    }
    #endregion

    #region DieOverride
    protected override void DieDetect()
    {
        //������Ѫ��С�ڵ����㣬����PlayerStats������������
        if (sts.currentHealth <= 0)
        {
            //��������״̬
            stateMachine.ChangeState(deadState);
        }
    }
    #endregion

    #region Accessibility
    public void CanDashSetting(bool _bool)
    {
        //�Ҳ��������������Unity�п��Բ��ݣ�������Ҫ��������������ű��б����ݸ�ֵ�����õ���һ����������
        canDash = _bool;
    }
    #endregion

    #region AttackAnimationRelatedScripts
    public void AnimationTrigger() => stateMachine.currentState.TriggerWhenAnimationFinished();
    //���˺���������ʱ������������������ʱ�򣩣����ص��õ�ǰ״̬��TriggerWhenAnimationFinished()�����Ľ���������ȼ����������
    //public void AnimationTrigger(){stateMachine.currentState.TriggerWhenAnimationFinished();}
    #endregion
}
