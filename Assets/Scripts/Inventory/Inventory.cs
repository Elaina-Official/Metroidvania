using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //ȷ�����еط����ܵ��ôˣ�Ψһ�ģ���Ʒ���ű���ֱ��ʹ��instance����
    //��ͬ��PlayerManager��ͨ��instance.player�����ã���Ϊ������instance�������PlayerManager����Player���Ͷ���
    public static Inventory instance;

    //��¼����Ʒ����Ʒ����һ������Stat���Զ����������ͣ�������ֱ��ʹ�ò��ù����ItemData����Ϣ���б�
    [SerializeField] public List<InventoryStoragedItem> inventoryItemsList;
    //ʹ���ֵ����洢ItemData��InventoryItemһһ��Ӧ�Ĺ�ϵ
    public Dictionary<ItemData,InventoryStoragedItem> inventoryItemsDictionary;

    private void Awake()
    {
        //������Ʒ���Լ�ȷ��ֻ��һ���˽ű���instance
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        //��ʼ����Ʒ����Ʒ�б��Լ���Ʒ���ֵ�
        inventoryItemsList = new List<InventoryStoragedItem>();
        inventoryItemsDictionary = new Dictionary<ItemData,InventoryStoragedItem>();
    }

    #region ChangeInventory
    public void AddItem(ItemData _newItemData)
    {
        //��ԭ������Ʒ���ֵ����������Ʒ�ˣ���ô���ڴ˻���������һ���ѵ��������ɣ�ע���Ƿ��жѵ����ޣ�
        if(inventoryItemsDictionary.TryGetValue(_newItemData, out InventoryStoragedItem _value))
        {
            //һ�μ���һ��
            _value.AddStackSizeBy(1);
        }
        //��ԭ��û�������Ʒ�������б�����������Ʒ
        else
        {
            //C#�д����Զ������¶���ķ�ʽ
            InventoryStoragedItem _newInventoryStoragedItem = new InventoryStoragedItem(_newItemData);
            //��Ʒ�ѵ�������
            _newInventoryStoragedItem.AddStackSizeBy(1);

            //��Ʒ���´���һ����Ʒ������װ���������Ʒ
            inventoryItemsList.Add(_newInventoryStoragedItem);

            //����µ�ӳ���ϵ���Ա��´α���⵽��ֱ�Ӷѵ������Ӽ���
            inventoryItemsDictionary.Add(_newItemData, _newInventoryStoragedItem);
        }
    }
    public void RemoveItemTotally(ItemData _itemData)
    {
        //�����������Ҫ��ɾ������Ʒ����ɾ�������Ʒ��ȫ�����������
        if(inventoryItemsDictionary.TryGetValue(_itemData, out InventoryStoragedItem _InvItem))
        {
            //���˸��Ӵ���ƷΪ0����1�������������Ʒ����
            if(_InvItem.stackSize <= 1)
            {
                //�Ƴ���Ʒ�������б��е����ItemData
                inventoryItemsList.Remove(_InvItem);
                //�Ƴ��ֵ��е������Ϊkey��ItemData
                inventoryItemsDictionary.Remove(_itemData);
            }
            else
            {
                //��֮����������ѵ�������
                _InvItem.DecreaseStackSizeBy(1);
            }
        }
    }
    #endregion

}
