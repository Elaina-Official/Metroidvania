using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerAnimationTriggers : MonoBehaviour
{
    Bringer bringer => GetComponentInParent<Bringer>();

    private void AnimationTrigger()
    {
        bringer.AnimationTrigger();
    }

    private void AttackDamageTrigger()
    {
        Collider2D[] collidersInAttackZone = Physics2D.OverlapCircleAll(bringer.attackCheck.position, bringer.attackCheckRadius);

        foreach (var beHitEntity in collidersInAttackZone)
        {
            if (beHitEntity.GetComponent<Player>() != null)
            {
                //�������ٶԷ�����ֵ�������ܻ�Ч��
                int _damage = bringer.sts.GetFinalAttackDamage(bringer.sts.primaryAttackDamage);
                beHitEntity.GetComponent<PlayerStats>().GetDamagedBy(_damage);
            }
        }
    }

    //�������Ա�����ѣ�ε�״̬
    private void OpenCounterAttackWindow() => bringer.OpenCounterAttackWindow();

    //�رտ��Ա�����ѣ�ε�״̬
    private void CloseCounterAttackWindow() => bringer.CloseCounterAttackWindow();
}
