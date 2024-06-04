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
        //����������Ч
        AudioManager.instance.PlaySFX(0, bringer.transform);

        Collider2D[] collidersInAttackZone = Physics2D.OverlapCircleAll(bringer.attackCheck.position, bringer.attackCheckRadius);

        foreach (var beHitEntity in collidersInAttackZone)
        {
            if (beHitEntity.GetComponent<Player>() != null)
            {
                //�������ٶԷ�����ֵ�������ܻ�Ч��
                beHitEntity.GetComponent<PlayerStats>().GetTotalNormalDmgFrom(bringer.sts, true, true);
            }
        }
    }

    //�������Ա�����ѣ�ε�״̬
    private void OpenCounterAttackWindow() => bringer.OpenCounterAttackWindow();

    //�رտ��Ա�����ѣ�ε�״̬
    private void CloseCounterAttackWindow() => bringer.CloseCounterAttackWindow();

    //��������
    private void BringerDead()
    {
        //����ʵ��
        Destroy(bringer.gameObject);

        //������ᷢ��������д����
    }
}
