using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
//���������UI��صİ��ǵ�Ҫ����

public class UI_ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
//���ǵ�������Ʒ������UI����InventoryStoragedItemһһ��Ӧ
{
    //Image��UI��ص�ͼ�񣬶�����Sprite
    [SerializeField] private Image itemImageInSlot;
    //��Ʒ��UI��ʾ����Ʒ������ı�
    [SerializeField] private TextMeshProUGUI itemText;

    //���ӵ���Ʒ���洢����Ʒ����Ϣ
    public InventoryStoragedItem item;

    private void Start()
    {
        itemImageInSlot = GetComponent<Image>();
        itemText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateItemSlotUI(InventoryStoragedItem _newItem)
    //��Inventory�б����ø���
    {
        //�����������Ʒ������Ʒ
        item = _newItem;
        //ע������ǿ��ж�
        if (item != null)
        {
            //���������Ʒ���ϵ���Ʒͼ��
            itemImageInSlot.sprite = item.itemData.itemIcon;
            //������Ʒ���Ӱ�͸��״̬ת��Ϊ��ɫ����Ȼ�Ļ���Ʒͼ��Ҳ��͸��
            itemImageInSlot.color = Color.white;

            //��Ʒ������һ��ʱ�����ʾ����������ֻ��һ��ʱ����ʾ�����������ÿ�
            if (item.stackSize > 1)
            {
                //��ʾ�����Ʒ�Ķѵ�������ע������͵��ַ�����ת��
                itemText.text = item.stackSize.ToString();
            }
            if(item.stackSize == 1)
            {
                //���ı�
                itemText.text = "";
            }
        }
    }

    #region ItemToolTip
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item.itemData != null)
            UI.instance.itemToolTip.ShowItemToolTip(item.itemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item.itemData != null)
            UI.instance.itemToolTip.HideItemToolTip();
    }
    #endregion
}
