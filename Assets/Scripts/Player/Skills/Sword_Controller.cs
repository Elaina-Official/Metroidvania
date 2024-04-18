using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Controller : MonoBehaviour
{
    private BoxCollider2D cd;
    private Rigidbody2D rb;
    private Animator anim;

    void Awake()
    //��������˵rb�Ķ�����Ҫ����Awake�У���Start�л��пգ�
    {
        cd = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    public void SetupSword(Vector2 _dir, float _gravity)
    //���ڱ����ö���ʼ������״̬
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravity;
    }
}
