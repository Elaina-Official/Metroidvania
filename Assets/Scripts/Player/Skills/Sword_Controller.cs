using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Controller : MonoBehaviour
//Ϊ�˷�ֹ��ճ����һ��������������ϣ�Ӧ��Ϊ����������һ��ͼ��Sword������ProjectSettings��Physics2D�ڽ������ѡGround��Enemy
{
    #region Components
    private BoxCollider2D cd;
    private Rigidbody2D rb;
    private Animator anim;
    #endregion

    //���������ָ�����Ƿ������ٶȸı�
    private bool canRotate = true;
    //�Ƿ��ڷ���״̬�����ǣ���Ӧ�����ص���Ҵ�
    private bool isReturning = false;
    //���ص��ٶ�
    private float returnSpeed = 30f;

    void Awake()
    //��������˵rb�Ķ�����Ҫ����Awake�У���Start�л��пգ�
    {
        cd = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if(canRotate)
        {
            //��֤���⳯�Ž����ٶȷ��򣬸���Ȼ
            transform.right = rb.velocity;
        }

        //��ʱ�ѽ����ٴ��ͻ����������ڵ���һ����������ٽ�����
        if(isReturning)
        {
            //Vector2.MoveTowards(�������, �����յ�, �ƶ��ٶ�)
            transform.position = Vector2.MoveTowards(transform.position, PlayerManager.instance.player.transform.position, returnSpeed * Time.deltaTime);
        
            //��������ҽ��ľ���С��1�������ٽ�Prefab
            if(Vector2.Distance(transform.position, PlayerManager.instance.player.transform.position) < 1)
            {
                PlayerManager.instance.player.ClearAssignedSword();
            }
        }
    }

    public void ReturnTheSword()
    //�����Ƿ�ѽ����ظ����
    {
        //�̶�ס����λ�ã���Ȼ�Ļ����������ذ���ߵ��˲��ܱ��ٻ�����
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //ʹ�ý�Prefab����޸������״̬
        transform.parent = null;
        //���ÿ��Է���
        isReturning = true;
        
        //rb.isKinematic = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    //��ʵ�����κ�������ײʱ�������������
    {
        //�ر���ת���
        canRotate = false;
        //�رս�����ײ
        cd.enabled = false;
        //Լ����ʵ�壬ʹ�䶳��xyz����ı仯
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //Kinematicʱֻ��ͨ�����ĸ�����ٶ��������ı�λ�ã������ܵ���������Ч����Ӱ�죬���彫�ɶ�����ű�ͨ������transform.position������ȫ����
        rb.isKinematic = true;

        //����ѽ���ɱ���ײ��������Ӷ���
        transform.parent = collision.transform;

        //��Enemy�������������˺�
        if(transform.parent.GetComponentInParent<Enemy>() != null)
        {
            //�Թ�����ɽ��ļ����˺�����ʽΪ�ɽ��Ķ����˺���������������˺������Ա���ʱֻ��������ı��屩���˺����ɽ�������˺�ֵ�����뱩���˺��ļ���
            int _swordExtraDamage = PlayerManager.instance.player.sts.swordExtraDamage.GetValue();
            int _totalSwordDamage = _swordExtraDamage + PlayerManager.instance.player.sts.GetFinalPhysicalDamage();
            transform.parent.GetComponentInParent<EnemyStats>().GetPhysicalDamagedBy(_totalSwordDamage);
        }
    }

    public void SetupSword(Vector2 _dir, float _gravity)
    //���ڱ����ö���ʼ������״̬
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravity;
    }
}
