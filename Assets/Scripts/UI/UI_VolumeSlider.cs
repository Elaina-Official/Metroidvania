using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
//ע������������һ��UI��ص�
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    //���������Ļ���
    public Slider slider;
    //���ӵ�AudioMixer�������˲��������ǵ�����������AudioMixer��Volumeֵ�������
    //���ֵ��Hierarchy���ֶ���д��ע������Ҫ���������ò���ʱ������һ�£���"Volume_SFX"
    public string parameter;

    //�ֶ����ӵ�Asset�еĻ���������Unity�п���������С�ȵ�Window
    [SerializeField] private AudioMixer audioMixer;
    //����Slider�Ļ����������仯�������ԣ�����25
    [SerializeField] private float multiplier;

    private void Start()
    {
        slider = GetComponent<Slider>();
        //��ֹ�����ϵ���׶˵�ʱ��AudioMixer��Volume������0������Ƶ��������������
        slider.minValue = 0.001f;
    }

    public void LoadSlider(float _value)
    //���ش浵ʱ����֮ǰ���ڹ�������
    {
        //�����ǲο������slider.minValue = 0.001f;����ֵ
        //����Ҫ�е��ںţ���Ȼ������������ף���õ�0.0010000123f����ֱ�Ӳ���¼����ֵ
        if (_value >= 0.001f)
        {
            slider.value = _value;
        }
    }

    public void LinkSliderValueToAudioMixer(float _value)
    //���ڽ�AudioMixer��Volumeֵ��Slider��ֵ������һ��
    {
        //�����õ��Ĺ�ʽͦ����
        audioMixer.SetFloat(parameter, Mathf.Log10(_value) * multiplier);
    }
}