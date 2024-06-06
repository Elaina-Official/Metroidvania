using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Controller : MonoBehaviour
//�˽ű��ڽ����ϣ�Ϊ�˷�ֹ��ճ����һ��������������ϣ�Ӧ��Ϊ����������һ��ͼ��Projectile������ProjectSettings��Physics2D�ڽ������ѡGround��Enemy
{
    #region Components
    private BoxCollider2D cd;
    private Rigidbody2D rb;
    private Animator anim;
    #endregion

    //�Ƿ��ڷ���״̬�����ǣ���Ӧ�����ص���Ҵ�
    private bool isReturning = false;

    /*[Header("Sword Bounce Info")]
    //��ǰ���Ե��Ĵ���
    public int bounceNumber;
    //��Ⲣ���淶Χ�ڿɵ��ĵ���Ŀ��λ��
    public List<Transform> enemyTargets;
    //������һ�������ﵯ���������Ǹ��б�ı��
    private int targetIndex = 0;*/

    private void Awake()
    //��������˵�����ֵ��Ҫ����Awake�У���Start�л��пգ������ǣ�
    {
        #region Components
        cd = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        #endregion

        /*//��ʼ����������
        bounceNumber = PlayerSkillManager.instance.swordSkill.bounceMaxAmount;*/
    }

    private void Update()
    {
        //�ڱ��ʲô�������ϵ�ʱ��׼����
        if (transform.parent == null)
        {
            //��֤���⳯�Ž����ٶȷ��򣬸���Ȼ
            transform.right = rb.velocity;
        }

        #region Return
        //��ʱ�ѽ����ٴ��ͻ����������ڵ���һ����������ٽ�����
        if (isReturning)
        {
            //Vector2.MoveTowards(�������, �����յ�, �ƶ��ٶ�)
            transform.position = Vector2.MoveTowards(transform.position, PlayerManager.instance.player.transform.position, PlayerSkillManager.instance.swordSkill.returnSpeed * Time.deltaTime);

            //��������ҽ��ľ���С��1�������ٽ�Prefab
            if (Vector2.Distance(transform.position, PlayerManager.instance.player.transform.position) < 1)
            {
                PlayerSkillManager.instance.ClearAssignedSword();
            }
        }
        #endregion

        /*#region Bounce
        //�ɵ����Ļ����������ɵ������������㡢�е��˿��Ա����������ڷ���״̬
        if (bounceNumber > 0 && enemyTargets.Count > 0 && !isReturning)
        {
            //��ֹ����Խ��
            if (targetIndex >= enemyTargets.Count)
            {
                //˵�������ˣ��Ǿ��Զ�����
                ReturnTheSword();
                Debug.Log("Automatically Return");
                return;
            }

            //������һ�����˶�ʧ���������ˣ�����ô����������һ�����ˣ���ΪҪ�õ�transform����������ǿյĻᱨ��
            if (enemyTargets[targetIndex] != null)
            {
                Debug.Log("Move Towards " + targetIndex);
                //�ӵ�ǰλ����һ���ٶ�����һ��Ŀ���ƶ�
                transform.position = Vector2.MoveTowards(transform.position, enemyTargets[targetIndex].position, PlayerSkillManager.instance.swordSkill.bounceSpeed * Time.deltaTime);
            
                //������Ŀ��λ�ø���������һ��Ŀ��ǰ��
                if (Vector2.Distance(transform.position, enemyTargets[targetIndex].position) < 0.5f)
                {
                    Debug.Log("Reach Target " + targetIndex);
                    //��λ��һ��Ŀ��
                    targetIndex++;
                    //�ɵ���������һ
                    bounceNumber--;
                }
            }
        }
        #endregion*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    //��ʵ�����κ�������ײʱ�������������
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            #region Damage
            //��Enemy�������������˺�
            //�洢Ҫ�õ��Ĵ������
            EntityStats _sts = PlayerManager.instance.player.sts;
            int _swordDamage = PlayerManager.instance.player.sts.swordDamage.GetValue();
            //��ɼ����˺����������˺�
            collision.transform.GetComponent<EnemyStats>().GetTotalSpecialDmgFrom(_sts, _swordDamage, true, true, false, false, true);
            #endregion

            /*#region Bounce
            //�������ֻ�е�һ����ײ��ʱ��Żᴥ������ֻ��ӵ�һ�����˵�λ�ÿ�ʼ���һ�η�Χ�ڵ���
            if (enemyTargets.Count == 0)
            {
                //��ȡ���뾶�ڵĵ���
                Collider2D[] _colliders = Physics2D.OverlapCircleAll(transform.position, PlayerSkillManager.instance.swordSkill.bounceRadius);

                //��һ��ȡ��Щ���˵�λ��
                foreach (var _target in _colliders)
                {
                    if (_target.GetComponent<Enemy>() != null)
                        enemyTargets.Add(_target.transform);
                }

                Debug.Log("Record " + enemyTargets.Count + " Enemies");
            }
            #endregion*/
        }

        //����ȥ
        StuckInto(collision);
    }

    #region Return
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
    #endregion

    #region Stuck
    private void StuckInto(Collider2D _collision)
    {
        //�رս�����ײ
        cd.enabled = false;
        //Լ����ʵ�壬ʹ�䶳��xyz����ı仯
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //Kinematicʱֻ��ͨ�����ĸ�����ٶ��������ı�λ�ã������ܵ���������Ч����Ӱ�죬���彫�ɶ�����ű�ͨ������transform.position������ȫ����
        rb.isKinematic = true;
        //����ѽ���ɱ���ײ��������Ӷ���
        transform.parent = _collision.transform;
    }
    #endregion

    #region Setup
    public void SetupSword(Vector2 _dir, float _gravity)
    //���ڱ����ö���ʼ������״̬
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravity;
    }
    #endregion
}
