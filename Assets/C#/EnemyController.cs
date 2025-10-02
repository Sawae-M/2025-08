using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("�G�X�e�[�^�X")]
    public float moveSpeed = 2f;
    public int maxHP = 50;
    public int attackPower = 5;
    public int defensePower = 2;
    public float knockbackForce = 3f;

    private int currentHP;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Transform target;
    private bool isKnockback = false; // �m�b�N�o�b�N�����ǂ���

    private void Start()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("King").transform;
    }

    private void Update()
    {
        if (rb.velocity.x > 0.1f)
        {
            sr.flipX = false; // �E
        }
        else if (rb.velocity.x < -0.1f)
        {
            sr.flipX = true;  // ��
        }
        // �m�b�N�o�b�N���͈ړ���~
        if (target != null && !isKnockback)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            rb.velocity = dir * moveSpeed;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GetKnockback(Vector2 dir, float force)
    {
        rb.velocity = Vector2.zero; // �������񓮂����~�߂�
        rb.AddForce(dir * force, ForceMode2D.Impulse);
        isKnockback = true;
        StartCoroutine(ResetKnockback(0.2f));
    }

    private System.Collections.IEnumerator ResetKnockback(float time)
    {
        yield return new WaitForSeconds(time);
        isKnockback = false;
    }
}
