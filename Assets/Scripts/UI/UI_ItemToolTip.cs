using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;

    public void ShowItemToolTip(ItemData _item)
    //������Ʒ��Ϣ
    {
        itemNameText.text = _item.itemName;
        itemTypeText.text = _item.itemType.ToString();
        itemDescriptionText.text = _item.itemDescription;

        //��ʾ���ToolTip
        gameObject.SetActive(true);

        //UI��Ч
        AudioManager.instance.PlaySFX(5, null);
    }

    public void HideItemToolTip()
    {
        gameObject.SetActive(false);
    }
}
