using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    //���Լ�����Ϣ
    [SerializeField] ItemData itemData;

    private void OnValidate()
    //�˺�����Unity��Hierarchy�ڽ��и��ֶ���Ĳ���ʱ���ͻ���е��ã������õȵ���ʼ������Ϸʱ�Ž��и��£�����Start�����ڸ��£�
    {
        //���ӵ�SpriteRenderer����������ͼ��ΪitemData�ڴ洢��icon�������Ϳ��������Ǹ����ItemObject��ֵ��ItemDataʱ��������ͼ�񣬶����õȵ�Start����ܿ���
        GetComponent<SpriteRenderer>().sprite = itemData.itemIcon;
        //ͬ����ItemData�е���Ʒ���ָ����GameObject
        gameObject.name = "ItemObject  " + itemData.itemName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    //�ж������Ƿ�����Ʒ��������ײ���ǵñ�֤��ƷObject��Collider���
    {
        //����������Ʒ����ײ����ײ���ұ�������λ���������Ʒ
        if(collision.GetComponent<Player>() != null && Inventory.instance.CanAddNewItem())
        {
            //ͨ��instance������Ʒ����ֱ�ӵ���Inventory��instance����
            //��ͬ��PlayerManager��ͨ��instance.player�����ã���Ϊ������instance�������PlayerManager����Player���Ͷ���
            Inventory.instance.AddItem(itemData);
            //���ٴ�item
            Destroy(gameObject);
        }
    }
}
