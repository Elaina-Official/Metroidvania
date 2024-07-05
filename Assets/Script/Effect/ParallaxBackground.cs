using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    //�����������
    private GameObject cam;

    //����ƶ���Ч��
    [SerializeField] private float parallaxEffect;

    private float xPosition;

    void Start()
    {
        //��ֵ�������
        cam = GameObject.Find("Main Camera");

        //��ʼ��Ϊ�ýű���ӦĿ���x����
        xPosition = transform.position.x;
    }

    void Update()
    {
        float distanceToMove = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);
    }
}
