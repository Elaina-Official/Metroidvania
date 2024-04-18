using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    //�����anim����ȥ���պͿ������ʵ�����Sprite����������
    public Rigidbody2D rb { get; private set; }
    //����ʵ��ĸ���Component
    public EntityFX fx { get; private set; }
    //���տ���ʵ�嶯��Ч���Ľű����
    #endregion

    #region CollisionInfo
    [Header("Basic Collision Info")]
    //������ײ��Χ��ʵ��ǰ����һ��Բ��
    public Transform attackCheck;
    public float attackCheckRadius = 1f;

    //������ָ��
    public bool isGround {  get; private set; }
    //�����⣻���Transform��������ڽ���Unity��ʵ��������Sprite�����ڵ����ٿ���ײ����ߵ�λ�ã�����������ֹ��λ��bug
    [SerializeField] protected Transform groundCheck_1;
    [SerializeField] protected Transform groundCheck_2;
    [SerializeField] protected float groundCheckDistance;
    //�������ͼ�㣨���������Ҫ�ֶ������ÿ��Sprite������Щ�����ں���Ϊ֮������Ӧ��Ӧ
    [SerializeField] protected LayerMask whatIsGround;

    //ǽ�ڼ�⣬ǽ�ڵ�ͼ������whatIsGround���ɣ������ǽ��ûɶ����
    public bool isWall {  get; private set; }
    [SerializeField] protected Transform wallCheck_1;
    [SerializeField] protected Transform wallCheck_2;
    [SerializeField] protected float wallCheckDistance;
    #endregion

    #region MovementInfo
    [Header("Basic Movement Info")]
    //��ʼ�ƶ��ٶȱ���
    public float moveSpeed = 10;

    //1Ϊ�ң�-1Ϊ��Ĭ��������Ϊ����
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    #endregion

    #region KnockbackInfo
    [Header("Knockback Info")]
    //ʵ�屻������Ļ���Ч��������������x��y����Ч������
    [SerializeField] protected Vector2 knockbackDirVector;
    //��¼���˵ĳ���
    protected int knockbackDir;
    //����Ч������ʱ��
    [SerializeField] protected float knockbackDuration = 0.07f;
    //Ĭ������false�������ܴ˲���Ӱ���SetVelocity�����޷����У���һ�ֲ�������Ĭ��ֵ�ķ���������ú�����
    protected bool isKnocked = false;
    #endregion

    protected virtual void Awake()
    //����ճ���ʱ�����ҽ�����һ�Σ��൱����Ĺ��캯������Start�������
    {
    }

    protected virtual void Start()
    {
        #region Components
        //����rb��ʵ��ĸ���Component��
        rb = GetComponent<Rigidbody2D>();
        //�������ʵ�����Sprite����������
        anim = GetComponentInChildren<Animator>();
        //���տ���ʵ�嶯��Ч���Ľű����
        fx = GetComponent<EntityFX>();
        #endregion
    }

    protected virtual void Update()
    {
        //���ϸ���ʵ���������ײ���
        CollisionDetect();
        //ȷ��ʵ��ĳ�����ȷ
        FlipController();
        //�����˷�����
        KnockbackDirDetect();
    }

    #region Knockback
    protected virtual void KnockbackDirDetect()
    {
    }
    public IEnumerator HitKnockback()
    //��Damage()��������StartCoroutine("HitKnockback");�ķ�ʽ������
    //���ǽ��ղ��������ñ���StartCoroutine("BusyFor", _seconds);������
    {
        //���boolֵ����SetVelocity�����Ƿ񼤻��ֹ�ٶ����������
        isKnocked = true;

        //�����������������г���������Ϸŵķ������������Է��򱻻���
        rb.velocity = new Vector2(knockbackDir * knockbackDirVector.x, knockbackDirVector.y);
        //���Ч���������
        yield return new WaitForSeconds(knockbackDuration);

        isKnocked = false;
    }
    #endregion

    #region Damage
    public virtual void Damage()
    //ʵ���ܵ��˺��ĺ�������
    {
        //�ܹ����Ĳ��ʱ仯��ʹ������˸�Ķ���Ч��
        fx.StartCoroutine("FlashHitFX");

        //���˵Ļ���Ч��
        StartCoroutine("HitKnockback");

        //Debug.Log(gameObject.name + " Was Damaged");
    }
    #endregion

    #region Collision
    public virtual void CollisionDetect()
    {
        //ʵ�ʽ�����ײ���Ĵ���
        bool isGround_1 = Physics2D.Raycast(groundCheck_1.position, Vector2.down, groundCheckDistance, whatIsGround);
        bool isGround_2 = Physics2D.Raycast(groundCheck_2.position, Vector2.down, groundCheckDistance, whatIsGround);
        //���������ֻҪ��һ����⵽�ذ壬����Ϊ�����ڵذ���
        isGround = isGround_1 || isGround_2;

        bool isWall_1 = Physics2D.Raycast(wallCheck_1.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
        bool isWall_2 = Physics2D.Raycast(wallCheck_2.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
        isWall = isWall_1 || isWall_2;
    }
    public virtual void OnDrawGizmos()
    //�˺������ڿ��ӻ��ػ�����������ߣ��˺��������ֶ�����
    {
        //�������ߣ����Կ����������Ǵ�groundCheck�����Sprite���Ļ����ģ����Ǵ�ʵ�����Ļ������������Ը������Ľ�����ײ���
        Gizmos.DrawLine(groundCheck_1.position, new Vector3(groundCheck_1.position.x, groundCheck_1.position.y - groundCheckDistance));
        Gizmos.DrawLine(groundCheck_2.position, new Vector3(groundCheck_2.position.x, groundCheck_2.position.y - groundCheckDistance));

        //ǽ�ڼ���ߣ�����wallCheckDistance������facingDir��ȷ��ǽ�ڼ���ߵ�ת��
        Gizmos.DrawLine(wallCheck_1.position, new Vector3(wallCheck_1.position.x + wallCheckDistance * facingDir, wallCheck_1.position.y));
        Gizmos.DrawLine(wallCheck_2.position, new Vector3(wallCheck_2.position.x + wallCheckDistance * facingDir, wallCheck_2.position.y));

        //������Χ��Բ
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    #region Velocity
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    //����Ҫ����ʵ���ٶȵ�ʱ����ô˺���
    {
        //�����ڱ�����״̬ʱ�����ٶ�����������д����������isKnocked��Ĭ��ֵ
        if (isKnocked)
            return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }
    #endregion

    #region Flip
    public virtual void Flip()
    {
        //��תʵ��
        transform.Rotate(0, 180, 0);
        //�ѷ��������жϲ�������
        facingDir *= -1;
        facingRight = !facingRight;
    }
    public virtual void FlipController()
    {
        //�ǻ���״̬�ſ��������ƶ��ٶ�ת�򣬲�Ȼ���˻��Զ�ת�򣬲���Ȼ
        if (isKnocked)
            return;
        else
        {
            //��ʼ�������ҳ���Ϊ��ʱ����ת
            if (rb.velocity.x > 0 && !facingRight)
            {
                Flip();
            }
            //��ʼ�������ҳ���Ϊ��ʱ����ת
            if (rb.velocity.x < 0 && facingRight)
            {
                Flip();
            }
        }
    }
    #endregion
}
