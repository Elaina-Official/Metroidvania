using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall_Controller : MonoBehaviour
{
    #region Components
    private BoxCollider2D cd;
    private Rigidbody2D rb;
    private Animator anim;
    #endregion

    //ǰ������
    private int direction;

    private void Awake()
    {
        #region Components
        cd = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        #endregion
    }

    private void Start()
    {
        //�ڼ���cd����������
        Invoke("DestroySkillObject", PlayerSkillManager.instance.iceballSkill.cooldown);
    }

    private void Update()
    {
        //�����������ʻ
        rb.velocity = new Vector2(direction, 0) * PlayerSkillManager.instance.iceballSkill.moveSpeed;

        //��֤��ͼ�����ٶȷ���
        transform.right = rb.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���뱬ը����
        anim.SetBool("boom", true);

        if (collision.GetComponent<EnemyStats>() != null)
        {
            //�洢Ҫ�õ��Ĵ������
            EntityStats _sts = PlayerManager.instance.player.sts;
            int _skilldmg = PlayerManager.instance.player.sts.iceballDamage.GetValue();
            //��ɼ����˺����������˺�������䶳buff
            collision.GetComponent<EnemyStats>().GetTotalSkillDmgFrom(_sts, _skilldmg, false, true, false, true, false);
        }
    }

    public void SetupIceBall(int _dir)
    //���ڱ����ö���ʼ��״̬
    {
        direction = _dir;
    }

    public void DestroySkillObject()
    //��Animator����ը��ɺ��������
    {
        PlayerSkillManager.instance.ClearAssignedIceBall();
    }
}
