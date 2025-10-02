using UnityEngine;

public class anim : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb; // 3D�Ȃ� Rigidbody �ɕύX

    public bool isDown = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �_�E�����Ă��Ȃ��Ƃ��������x�𔽉f
        if (!isDown)
        {
            float speed = rb.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
        else
        {
            animator.SetFloat("Speed", 0); // �_�E�����͑���Ȃ�
        }

        // �_�E����Ԃ𔽉f
        animator.SetBool("IsDown", isDown);
    }

    // �O������Ăяo���ē|���/������؂�ւ�����悤�ɂ���
    public void SetDown(bool value)
    {
        isDown = value;
    }
}
