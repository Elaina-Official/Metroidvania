using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    //���Լ�����Ϣ
    [SerializeField] ItemData itemData;
    //���ӵ�SpriteRenderer
    private SpriteRenderer sr;

    private void Start()
    {
        //���ӵ�SpriteRenderer
        sr = GetComponent<SpriteRenderer>();
        //��itemData����Spriteͼ��
        sr.sprite = itemData.itemIcon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    //�ж������Ƿ�����Ʒ��������ײ
    {
        //����������Ʒ����ײ����ײ���������Ʒ
        if(collision.GetComponent<Player>() != null)
        {
            //ͨ��instance������Ʒ����ֱ�ӵ���Inventory��instance����
            //��ͬ��PlayerManager��ͨ��instance.player�����ã���Ϊ������instance�������PlayerManager����Player���Ͷ���
            Inventory.instance.AddItem(itemData);
            //���ٴ�item
            Destroy(gameObject);
        }
    }
}
