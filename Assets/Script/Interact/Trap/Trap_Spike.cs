using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Spike : Trap
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        //����Ӧ��������ʵ�嶼�ܴ����ģ�������Һ͵���
        if (collision.GetComponent<Entity>() != null)
        {
            #region AttackedFX
            //�ܹ�������Ч
            AudioManager.instance.PlaySFX(12, null);
            //�ܹ���������Ч�������Լ����ܹ����ߣ�����
            collision.GetComponent<Entity>().fx.CreateHitFX00(collision.GetComponent<Entity>().transform);
            #endregion

            //�ش������ֵ�˺�
            collision.GetComponent<Entity>().GetComponent<EntityStats>().GetPhysicalDamagedBy(trapDamage);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
