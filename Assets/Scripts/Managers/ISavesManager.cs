using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISavesManager
//����һ���ӿڣ�����һ����I��ͷ������Ҫ��SavesManager���̳У���ToolTip�õ�����IPointInɶ�����ִ�I�Ķ��ǽӿ�
{
    void LoadData(GameData _data);

    //ע�����������ã�ʹ�ÿ��Ըı䴫�����
    void SaveData(ref GameData _data);
}
