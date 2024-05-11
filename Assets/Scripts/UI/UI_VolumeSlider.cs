using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
//ע������������һ��UI��ص�
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    //���������Ļ���
    private Slider slider;
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

    public void LinkSliderValueToAudioMixer(float _value)
    //���ڽ�AudioMixer��Volumeֵ��Slider��ֵ������һ��
    {
        //�����õ��Ĺ�ʽͦ����
        audioMixer.SetFloat(parameter, Mathf.Log10(_value) * multiplier);
    }
}