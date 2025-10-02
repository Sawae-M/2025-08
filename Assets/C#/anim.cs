using UnityEngine;

public class anim : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb; // 3Dなら Rigidbody に変更

    public bool isDown = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ダウンしていないときだけ速度を反映
        if (!isDown)
        {
            float speed = rb.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
        else
        {
            animator.SetFloat("Speed", 0); // ダウン中は走らない
        }

        // ダウン状態を反映
        animator.SetBool("IsDown", isDown);
    }

    // 外部から呼び出して倒れる/復活を切り替えられるようにする
    public void SetDown(bool value)
    {
        isDown = value;
    }
}
