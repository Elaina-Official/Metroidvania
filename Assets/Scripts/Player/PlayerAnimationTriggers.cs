using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    Player player => GetComponentInParent<Player>();
    //�����Ļ��ڱ����е��õ�player��Ϊ���ô˽ű��Ķ���ĸ���Component�е�Player��

    private void AnimationTrigger()
    {
        //�˴���player��Ϊ����Լ����Ǹ�Player
        //���������Ŷ������ʱ���˺��������ã������˵�ǰ�׶�attack�����Ľ���
        player.AnimationTrigger();
    }

    //���¼��ڹ����������е�����˺�����һִ֡��
    private void AttackDamageTrigger()
    {
        //����һ����ʱ���飬�����ʱ�����﹥�����Ȧ�ڵ�����ʵ��
        Collider2D[] collidersInAttackZone = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        
        //ѭ���������������ڵĵ���ʵ�壬�����˺�
        foreach(var beHitEntity in collidersInAttackZone)
        {
            //�Ե�����ʵ������˺�
            if(beHitEntity.GetComponent<Enemy>() != null)
            {
                beHitEntity.GetComponent<Enemy>().Damage();
            }
        }
    }

    //��Ԥ����Ĵ����Ĵ���
    private void ThrowSwordTrigger()
    {
        PlayerSkillManager.instance.swordSkill.CreateSword();
    }
}