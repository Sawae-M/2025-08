using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("敵ステータス")]
    public float moveSpeed = 2f;
    public int maxHP = 50;
    public int attackPower = 5;
    public int defensePower = 2;
    public float knockbackForce = 3f;

    private int currentHP;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Transform target;
    private bool isKnockback = false; // ノックバック中かどうか

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
            sr.flipX = false; // 右
        }
        else if (rb.velocity.x < -0.1f)
        {
            sr.flipX = true;  // 左
        }
        // ノックバック中は移動停止
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
        rb.velocity = Vector2.zero; // いったん動きを止める
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
