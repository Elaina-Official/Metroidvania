using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_InGame : MonoBehaviour
{
    private PlayerStats pStats;
    private UnityEngine.UI.Slider healthBarSlider;

    [Header("Skill UI")]
    //�ֶ���Hierarchy�ڸ�ֵ�ɣ�Start��ȡ��������Ƚ��ѣ���ΪImage���͵�̫����
    [SerializeField] private UnityEngine.UI.Image dashCooldownImage;

    private void Start()
    {
        pStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        healthBarSlider = GetComponentInChildren<UnityEngine.UI.Slider>();

        //�¼��ĵ���
        if (pStats != null)
            pStats.onHealthChanged += UpdateHealthUI;

        //�����Start��������Ҫȷ���ȳ�ʼ��ʵ��Ѫ����Start��������ã�����UI����ʵ��Ѫ��������
        //�����������˳�򣬿���Project Settings��Scripts Execution Order���޸�
        //Debug.Log("UI_InGame Start() Func Called");
        //ʵ�����ɵ�ʱ�򣬵���һ��Ѫ��UI�ĸ���
        UpdateHealthUI();
    }

    private void Update()
    //������Ϸ��UI��Ҫʵʱ���һЩ��Ҫ���ܣ����UI�л���������ȴ��ʾUI�ȣ����Զ���Щ����ʹ��Update����
    {
        //dash������ȴ���ĸ���
        UpdateSkillCooldownUIOf(dashCooldownImage, PlayerSkillManager.instance.dashSkill.cooldown);

        //��������shift������ܽ��г�̣�����ҽ����˳�̣�ʱ��Ԥʾ����ҳ�̼��ܽ�����ȴ���ʶ����¼���ͼ�������ȴ
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            GetIntoSkillCooldownFor(dashCooldownImage);
        }
    }

    private void UpdateHealthUI()
    //��onHealthChanged�¼����ӵ��ã����ϸ���ʵ��ĵ�ǰѪ�����Ա����뻬������
    {
        //��������ֵ����ʵ���������Ѫ��
        healthBarSlider.maxValue = pStats.GetFinalMaxHealth();
        //����ĵ�ǰֵ����ʵ��ĵ�ǰѪ��
        healthBarSlider.value = pStats.currentHealth;
    }

    #region SkillCooldown
    private void UpdateSkillCooldownUIOf(UnityEngine.UI.Image _image, float _cooldown)
    //��һ�����ܴ�����ȴʱ��������ȴ�����еݼ���ֱ����ȴ����
    {
        if(_image.fillAmount > 0)
        {
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
        }
    }
    private void GetIntoSkillCooldownFor(UnityEngine.UI.Image _image)
    //ʹ����Ϸ��UI�еļ���ͼ����н�����ȴ������
    {
        //�����ô˺���ʱ������ͼ�������ȴ״̬
        //�����Լ������ʹ�ü�ʹ����ȴδ����ǰ�ٴΰ��˴�����ȴ�ļ���Ҳ�������´�ͷ��ʼ��ʾ��ȴʱ�䣬����Update�����ڵ��ж��������Բ���д����ôȫ
        if(_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }
    #endregion
}
