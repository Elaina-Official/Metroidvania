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
        Collider2D[] collidersInAttackZone = Physics2D.OverlapCircleAll(slime.attackCheck.position, slime.attackCheckRadius);

        foreach (var beHitEntity in collidersInAttackZone)
        {
            if (beHitEntity.GetComponent<Player>() != null)
            {
                //�������ٶԷ�����ֵ�������ܻ�Ч��
                beHitEntity.GetComponent<PlayerStats>().GetTotalNormalDmgFrom(slime.sts, true, true);
            }
        }
    }

    //�������Ա�����ѣ�ε�״̬
    private void OpenCounterAttackWindow() => slime.OpenCounterAttackWindow();

    //�رտ��Ա�����ѣ�ε�״̬
    private void CloseCounterAttackWindow() => slime.CloseCounterAttackWindow();

    //��������
    private void SlimeDead()
    {
        //ʷ��ķ������ѣ�������С�Ĳ�����
        if (slime.enemyType == EnemyType.slime_Small)
        {
            //����ʵ��
            Destroy(slime.gameObject);
        }
        else
        {
            slime.CreatSplitSlime(slime.splitSlime, slime.splitSlimeCount);
            //����ʵ��
            Destroy(slime.gameObject);
        }
    }
}
