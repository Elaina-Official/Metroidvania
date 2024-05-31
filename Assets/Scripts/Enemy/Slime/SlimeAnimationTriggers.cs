using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimationTriggers : MonoBehaviour
{
    Slime slime => GetComponentInParent<Slime>();

    private void AnimationTrigger()
    {
        slime.AnimationTrigger();
    }

    private void AttackDamageTrigger()
    {
        //����������Ч
        AudioManager.instance.PlaySFX(0, slime.transform);

        Collider2D[] collidersInAttackZone = Physics2D.OverlapCircleAll(slime.attackCheck.position, slime.attackCheckRadius);

        foreach (var beHitEntity in collidersInAttackZone)
        {
            if (beHitEntity.GetComponent<Player>() != null)
            {
                //�������ٶԷ�����ֵ�������ܻ�Ч��
                beHitEntity.GetComponent<PlayerStats>().GetTotalDamageFrom(slime.sts);
            }
        }
    }

    //�������Ա�����ѣ�ε�״̬
    private void OpenCounterAttackWindow() => slime.OpenCounterAttackWindow();

    //�رտ��Ա�����ѣ�ε�״̬
    private void CloseCounterAttackWindow() => slime.CloseCounterAttackWindow();

    //������������Ҫ��������
    private void SlimeDead() => Destroy(slime.gameObject);
}
