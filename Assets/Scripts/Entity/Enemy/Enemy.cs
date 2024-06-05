using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    #region States
    //���ӵ�ÿ���������״̬��
    public EnemyStateMachine stateMachine { get; private set; }
    #endregion

    #region EnemyType
    [Header("EnemyType")]
    //��¼���������
    public EnemyType enemyType;
    #endregion

    #region EnemyMove
    [Header("Enemy Move Info")]
    //����վ����һ��ʱ����Զ���ʼ�ƶ�
    public float pauseTime = 0.5f;

    //���﷢����Һ��Լ��ٱ����ƶ������ֵ����������moveSpeed�ģ�
    public float battleSpeedMultiplier = 2f;
    //�������λ�þ���battle״̬��Ӧ��ǰ����ߣ�-1�������ұߣ�1���н�
    public int battleMoveDir;

    //����������غ�λ�õ��ж�����뾶
    //public float overlapRegionRadius = 0.1f;
    #endregion

    #region Default
    private float defaultBattleSpeedMultiplier;
    #endregion

    #region Battle
    [Header("Battle Info")]
    public bool isPlayer;
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected float playerCheckDistance;
    [SerializeField] protected LayerMask whatIsPlayer;
    //���빥�����루�½�һ�������⣩֮�󣬿��Խ��빥��״̬�ˣ�����Player��ͼ��
    [SerializeField] protected Transform canAttackCheck;
    [SerializeField] protected float canAttackCheckDistance;
    public bool enterAttackRegion;
    //������ȴʱ�䳤��
    public float attackCooldown = 1.1f;
    //������ȴ����ʱ�����Թ���
    public bool canAttack;
    //������ȴʱ���ʱ��
    protected float attackCooldownTimer;

    //����һ��ʱ����battleState��û�н��й����Ļ�����ս
    public float quitBattleTime = 20f;
    //����������ʳ���playerCheckDistance�ľ���ͬ����ս
    [SerializeField] protected float quitBattleDistanceRatio = 1.5f;
    //�����ֹ�һ����һ��߱���ҹ���������battle��ֻ�е�����뿪�޶���Χ����һ��ʱ����δ��������ҲŻ������������������۾���ʲô״̬ת������Ӧ���ص�battle
    public bool shouldEnterBattle = false;
    #endregion

    #region Stunned
    [Header("Stunned Info")]
    //���ﱻ���Ǹ񵲵��������ѣ�Σ��˴�Ϊѣ��ʱ���������Ļ��˵�����ͨ��������StartCoroutine("HitKnockback");����
    public float stunnedDuration = 1f;
    //���Ա��������źţ���ר���������ƣ�ΪAnimationTrigger�ű��ṩ�ӿ�
    private bool canBeStunned = false;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        #region States
        stateMachine = new EnemyStateMachine();
        #endregion
    }

    protected override void Start()
    {
        base.Start();

        #region Default
        defaultBattleSpeedMultiplier = battleSpeedMultiplier;
        #endregion
    }

protected override void Update()
    {
        base.Update();

        //�˴���ͨ��MonoBehavior��Update���������ϵ���EnemyState���е�Update����������ˢ�¹���״̬
        stateMachine.currentState.Update();

        //��Ҽ��
        PlayerCollisionDetect();

        //���ƹ�����ȴ��
        AttackController();

        //�Ƿ������״̬���
        AggressiveDetect();
    }

    #region KnockbackOverride
    protected override void KnockbackDirDetect()
    //����Enemy���ԣ�Ӧ���Ǳ���ҹ��������˷�����Ȼ����ҵĳ���
    {
        base.KnockbackDirDetect();

        //����������๥��������˷���Ϊ�ұߣ���֮Ϊ��
        knockbackDir = PlayerManager.instance.player.facingDir;
    }
    #endregion

    #region Stunned
    public void OpenCounterAttackWindow()
    {
        //�����ܱ�����ѣ�ε�״̬
        canBeStunned = true;
    }
    public void CloseCounterAttackWindow()
    {
        //�ر����״̬
        canBeStunned = false;
    }
    public virtual bool WhetherCanBeStunned()
    {
        //����������ɹ��ˣ���canBeStunned����true������дһ��������ԭ����Ϊ�˵���һ��CloseCounterAttackWindow();
        //ͬʱ�����������������˽ű��б�override��ͬʱ����stateMachine.ChangeState()����
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
    #endregion

    #region SlowEntityOverride
    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
        //���Եļ��٣�д��ǰ��
        battleSpeedMultiplier *= (1 - _slowPercentage);

        base.SlowEntityBy(_slowPercentage, _slowDuration);
    }
    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        //�ָ��ٶ�
        battleSpeedMultiplier = defaultBattleSpeedMultiplier;
    }
    #endregion


    #region Attack
    public virtual void AttackController()
    {
        //��ʱ����ʼ��ʱ
        attackCooldownTimer -= Time.deltaTime;

        //�����ʱ��Ϊ�������������ҽ��빥����Χ�󹥻�
        if(attackCooldownTimer < 0)
        {
            canAttack = true;
        }
        else if(attackCooldownTimer > 0)
        {
            canAttack = false;
        }
    }

    //������ȴˢ��
    public virtual void AttackCooldownRefresher() => attackCooldownTimer = attackCooldown;
    
    //���ֵ��������ս����
    public virtual float GetQuitBattleDisance() => playerCheckDistance * quitBattleDistanceRatio;
    #endregion  

    #region AggressiveDetect
    protected virtual void AggressiveDetect()
    {
        if(isKnocked)
        {
            //�����ﱻ�����󣬽�����״̬
            shouldEnterBattle = true;
        }
    }
    #endregion

    #region PlayerDetect
    public virtual void PlayerCollisionDetect()
    {
        isPlayer = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer);
        enterAttackRegion = Physics2D.Raycast(canAttackCheck.position, Vector2.right * facingDir, canAttackCheckDistance, whatIsPlayer);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        //Ϊ��Ҽ��������һ���ر����ɫ
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + playerCheckDistance * facingDir, playerCheck.position.y));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(canAttackCheck.position, new Vector3(canAttackCheck.position.x + canAttackCheckDistance * facingDir, canAttackCheck.position.y));
    }
    #endregion

    #region AnimationTrigger
    public void AnimationTrigger() => stateMachine.currentState.TriggerWhenAnimationFinished();
    //���˺���������ʱ������������������ʱ�򣩣����ص��õ�ǰ״̬��TriggerWhenAnimationFinished()�����Ľ���������ȼ����������
    //public void AnimationTrigger(){stateMachine.currentState.TriggerWhenAnimationFinished();}
    #endregion
}
