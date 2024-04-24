using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerStats : EnemyStats
{
    Bringer bringer;

    protected override void Start()
    {
        base.Start();

        bringer = GetComponent<Bringer>();
    }

    public override void GetDamaged(int _damage)
    {
        base.GetDamaged(_damage);

        //�ܹ����Ĳ��ʱ仯��ʹ������˸�Ķ���Ч��
        bringer.fx.StartCoroutine("FlashHitFX");

        //���˵Ļ���Ч��
        bringer.StartCoroutine("HitKnockback");
    }

    public override void StatsDie()
    {
        base.StatsDie();

        bringer.EntityDie();
    }
}
