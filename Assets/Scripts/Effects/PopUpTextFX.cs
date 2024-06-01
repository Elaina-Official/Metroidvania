using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpTextFX : MonoBehaviour
{
    public TextMeshPro myText;

    //����Ч�����ֵĳ��ٶ�
    [SerializeField] private float textAppearSpeed;
    //����Ч����ʧ���ٶ�
    [SerializeField] private float textDisappearSpeed;
    [SerializeField] private float colorDisappearSpeed;

    //����Ч�����ڵ�ʱ��
    [SerializeField] private float lifeTime;
    //��ʱ��
    private float textTimer;

    private void Start()
    {
        myText = GetComponent<TextMeshPro>();
        textTimer = lifeTime;
    }

    private void Update()
    {
        textTimer -= Time.time;
    
        if(textTimer < 0)
        {
            float _alpha = myText.color.a - colorDisappearSpeed * Time.deltaTime;
            myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, _alpha);
        
            if(myText.color.a < 50)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), textDisappearSpeed * Time.deltaTime);
            }

            if (myText.color.a <= 0)
                Destroy(gameObject);
            }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), textAppearSpeed * Time.deltaTime);
        }
    }
}
