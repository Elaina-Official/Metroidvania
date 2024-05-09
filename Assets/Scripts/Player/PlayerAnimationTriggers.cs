using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    //�����Ļ��ڱ����е��õ�player��Ϊ���ô˽ű��Ķ���ĸ���Component�е�Player��
    Player player => GetComponentInParent<Player>();

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
        //����������Ч
        Audio_Manager.instance.PlaySFX(0, null);

        //����һ����ʱ���飬�����ʱ�����﹥�����Ȧ�ڵ�����ʵ��
        Collider2D[] collidersInAttackZone = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        
        //ѭ���������������ڵĵ���ʵ�壬�����˺�
        foreach(var beHitEntity in collidersInAttackZone)
        {

                /*
                 * �����������һ
                 * �����չ���˺����б����е�Enemy�����������Bringer
                 * ����취��ֱ�����ӵ�<Entity>�����Զ��������ӵ���̳е������еĽű�
                 */

            //��Enemy������ʵ������˺�
            if (beHitEntity.GetComponent<Enemy>() != null)
            {
                //�����ܵ����˺���ֵ����Ч��
                beHitEntity.GetComponent<EnemyStats>().GetTotalDamageFrom(PlayerManager.instance.player.sts);
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