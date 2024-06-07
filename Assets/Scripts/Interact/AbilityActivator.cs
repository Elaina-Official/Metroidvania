using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityActivator : MonoBehaviour
{
    //Ҫ�������������
    [SerializeField] private AbilityType abilityType;
    //����Ч��
    private ParticleSystem particle;

    private void Start()
    {
        if (GetComponentInChildren<ParticleSystem>() != null)
        {
            particle = GetComponentInChildren<ParticleSystem>();
            particle.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�����֮�Ӵ��󴥷���������
        if (collision.GetComponent<Player>() != null)
        {
            //�����Ӧ����
            PlayerManager.instance.ActivateAbility(abilityType);

            #region ActivationFX
            //������������Ч
            AudioManager.instance.PlaySFX(16, null);

            //���������Ķ�Ӧ��Ч
            #endregion

            //�ر�����
            this.gameObject.SetActive(false);
        }
    }
}
