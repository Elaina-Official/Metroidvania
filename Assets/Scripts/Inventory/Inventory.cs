using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour, ISavesManager
//����ű���ֵ��Player�µ�Inventory���������ǹ���CharacterUI�ڵ�Inventory��Slots����ʹ������ͬ�ķ���������CharacterUI�ڵ����ݵĸ���
{
    //ȷ�����еط����ܵ��ôˣ�Ψһ�ģ���Ʒ���ű���ֱ��ʹ��instance����
    //��ͬ��PlayerManager��ͨ��instance.player�����ã���Ϊ������instance�������PlayerManager����Player���Ͷ���
    public static Inventory instance;

    [Header("Character UI")]
    //������Ʒ����λ��
    [SerializeField] private Transform inventorySlotParent;
    //������Ʒ��UI���б�
    private UI_ItemSlot[] itemSlotsUIList;
    
    //��¼Stat��������
    [SerializeField] private Transform statSlotParent;
    //��¼��slot���б�
    private UI_StatSlot[] statSlotsUIList;

    [Header("Inventory Items")]
    //��¼����Ʒ����Ʒ����һ������Stat���Զ����������ͣ�������ֱ��ʹ�ò��ù����ItemData����Ϣ���б�
    public List<StoragedItem> inventoryItemsList;
    //ʹ���ֵ����洢ItemData��InventoryItemһһ��Ӧ�Ĺ�ϵ
    public Dictionary<ItemData,StoragedItem> inventoryItemsDictionary;

    [Header("ItemData Base")]
    //�Ӵ浵���ص���Ʒ������Ʒ�б�
    public List<StoragedItem> loadedItems;

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
        inventoryItemsList = new List<StoragedItem>();
        inventoryItemsDictionary = new Dictionary<ItemData,StoragedItem>();
    
        //ע��������Components����s����Ϊ��ֵ���б���¼�������
        itemSlotsUIList = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        statSlotsUIList = statSlotParent.GetComponentsInChildren<UI_StatSlot>();

        //���صĴ浵�ں��е���Ʒ
        AddStartingItems();
    }

    private void AddStartingItems()
    {
        if(loadedItems.Count > 0)
        {
            foreach(StoragedItem _item in  loadedItems)
            {
                for(int i = 0; i < _item.stackSize; i++)
                {
                    AddItem(_item.itemData);
                }
            }
        }
    }

    #region UpdateSlotUIs
    private void UpdateItemSlotUI()
    //�˺���������Ʒ���ĸ��ӣ�����Ʒ�仯��Add��Remove������ʱ������һ��
    {
        //����Ʒ���б��ڵ���Ʒ���б���
        for (int i = 0; i < inventoryItemsList.Count; i++)
        {
            //����Ʒ���б��ڵ���Ʒһһ������Ʒ��UI�б�
            itemSlotsUIList[i].UpdateItemSlotUI(inventoryItemsList[i]);
        }
    }
    private void UpdateStatSlotUI()
    //�˺�������CharacterUI�ڵ�������ֵ���Ӹ��£�����ֵ�仯��ʱ�򱻵��ø���
    {
        for (int i = 0; i < statSlotsUIList.Length; i++)
        {
            statSlotsUIList[i].UpdateStatValueSlotUI();
        }
    }
    #endregion

    #region ChangeInventoryItems
    public bool CanAddNewItem()
    //�����Ʒ���Ƿ��������ռ�
    {
        if (inventoryItemsList.Count >= itemSlotsUIList.Length)
        {
            Debug.Log("Inventory No More Space");
            return false;
        }
        else
        {
            //Debug.Log("Inventory Has Space");
            return true;
        }
    }
    public void AddItem(ItemData _newItemData)
    {
        //��ԭ������Ʒ���ֵ����������Ʒ�ˣ���ô���ڴ˻���������һ���ѵ��������ɣ�ע���Ƿ��жѵ����ޣ�
        if(inventoryItemsDictionary.TryGetValue(_newItemData, out StoragedItem _value))
        {
            //һ�μ���һ��
            _value.AddStackSizeBy(1);
        }
        //��ԭ��û�������Ʒ�������б�����������Ʒ��ǰ���Ǳ���û��
        else
        {
            if(CanAddNewItem())
            {
                //C#�д����Զ������¶���ķ�ʽ
                StoragedItem _newStoragedItem = new StoragedItem(_newItemData);

                //��Ʒ���´���һ����Ʒ������װ���������Ʒ���ѵ����ڹ��캯���б�Ĭ�ϳ�ʼ��Ϊ1
                inventoryItemsList.Add(_newStoragedItem);

                //����µ�ӳ���ϵ���Ա��´α���⵽��ֱ�ӽ��ѵ�����������
                inventoryItemsDictionary.Add(_newItemData, _newStoragedItem);
            }
        }

        //ˢ����Ʒ��UI
        UpdateItemSlotUI();
    }
    public void RemoveItemTotally(ItemData _itemData)
    {
        //�����������Ҫ��ɾ������Ʒ����ɾ�������Ʒ��ȫ�����������
        if(inventoryItemsDictionary.TryGetValue(_itemData, out StoragedItem _InvItem))
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

        //ˢ����Ʒ��UI
        UpdateItemSlotUI();
    }
    #endregion

    #region ISavesManager
    public void LoadData(GameData _data)
    {
        foreach(KeyValuePair<string, int> _pair in _data.inventory)
        {
            foreach(var _item in GetItemDataBase())
            {
                if(_item != null && _item.itemID == _pair.Key)
                {
                    StoragedItem _itemToLoad = new StoragedItem(_item);
                    _itemToLoad.stackSize = _pair.Value;
                
                    loadedItems.Add(_itemToLoad);
                }
            }
        }
    }
    public void SaveData(ref GameData _data)
    {
        //���ԭ�������ݣ���Ϊ��Ʒ���ֵ��е�ӳ����ܻᷢ���仯
        _data.inventory.Clear();

        //д���µ���Ʒ������
        foreach(KeyValuePair<ItemData, StoragedItem> _pair in inventoryItemsDictionary)
        {
            _data.inventory.Add(_pair.Key.itemID, _pair.Value.stackSize);
        }
    }
    private List<ItemData> GetItemDataBase()
    {
        //��¼��ƷID���ӳ������ݿ�
        List<ItemData> itemDataBase = new List<ItemData>();
        //·������ItemData��ŵ��ļ����Ǹ��ط�
        string[] _assetNames = AssetDatabase.FindAssets("", new[] { "Assets/ItemData" });
    
        foreach(string _SOName in _assetNames)
        {
            var _SOPath = AssetDatabase.GUIDToAssetPath(_SOName);
            var _itemData = AssetDatabase.LoadAssetAtPath<ItemData>(_SOPath);
            itemDataBase.Add(_itemData);
        }

        return itemDataBase;
    }
    #endregion
}
