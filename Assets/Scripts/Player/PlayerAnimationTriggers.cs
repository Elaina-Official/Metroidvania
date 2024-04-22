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

    #region Damage
    private void AttackDamageTrigger()
    //���¼��ڹ����������е�����˺�����һִ֡��
    {
        //����һ����ʱ���飬�����ʱ�����﹥�����Ȧ�ڵ�����ʵ��
        Collider2D[] collidersInAttackZone = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        
        //ѭ���������������ڵĵ���ʵ�壬�����˺�
        foreach(var beHitEntity in collidersInAttackZone)
        {

                /*
                 * �����������һ
                 * �����չ���˺����б����е�Enemy�����������Bringer
                 */

            //��Bringer��ʵ������˺�
            if (beHitEntity.GetComponent<Bringer>() != null)
            {
                //�ܵ��˺���Ч������ֵ�仯
                beHitEntity.GetComponent<BringerStats>().GetDamaged(player.sts.primaryAttackDamage.GetValue());
            }
        }
    }
    #endregion

    #region Sword
    //��Ԥ����Ĵ����Ĵ���
    private void ThrowSwordTrigger()
    {
        PlayerSkillManager.instance.swordSkill.CreateSword();
    }
    #endregion
}