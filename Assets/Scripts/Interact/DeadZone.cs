using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
//�������������ʱ����Ҫ������������Ȼ��һֱ����
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EntityStats>() != null)
        {
            //������ֵ����ʵ�壬������ʹ��StatsDie�������ᴥ�������쳣bug��Ӧ����ʵ��������Ҫ��������Die������ԭ�򣬵���StatsDie������
            collision.GetComponent<EntityStats>().GetPhysicalDamagedBy(999999);
        }
        else
        {
            //����������ʵ�壬��ֱ�����ⶫ����ʧ�����糪Ƭ������Ͷ��������
            Destroy(collision.gameObject);
        }
    }
}
