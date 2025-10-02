using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Android;

public class HeroController : MonoBehaviour
{
    [Header("�E�҃X�e�[�^�X")]
    public float moveSpeed = 5f;
    public int attackPower = 10;
    public int defensePower = 5;
    public int maxHP = 100;

    private SpriteRenderer sr;
    private Vector2 moveInput;

    [Header("UI")]
    public Slider hpSlider;

    [Header("���ʉ�")]
    public AudioClip hitSound;   // �G�ƂԂ������Ƃ��̉�
    private AudioSource audioSource;

    [Header("UI�I�u�W�F�N�g")]
    public GameObject scPanel;   // �_�E�����ɕ\������UI


    public int currentHP;
    private Rigidbody2D rb;
    private int spacePressCount = 0;
    private int revivePressCount = 10;
    private Animator animator;

    // �Ԃ��������Ɉ�u�~�߂�
    private bool isStagger = false;
    private bool isDown = false;
    private float staggerTime = 0.1f; // ��~����

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        UpdateUI();
    }

    private void Update()
    {
        if (isDown)
        {
            animator.SetFloat("Speed", 0);

            rb.velocity = Vector2.zero;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spacePressCount++;
                if (spacePressCount >= revivePressCount)
                {
                    Revive();
                }
            }
            return;
        }
        if (!isStagger)
        {
            Move();
        }

        // �X�v���C�g�𔽓]
        if (rb.velocity.x > 0.1f)
        {
            sr.flipX = false; // �E
        }
        else if (rb.velocity.x < -0.1f)
        {
            sr.flipX = true;  // ��
        }
        animator.SetFloat("Speed", rb.velocity.magnitude);

    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(h, v).normalized;
        rb.velocity = dir * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                if (hitSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(hitSound);
                }
                // ���݂��Ƀ_���[�W
                int damageToEnemy = Mathf.Max(attackPower - enemy.defensePower, 1);
                enemy.TakeDamage(damageToEnemy);

                int damageToHero = Mathf.Max(enemy.attackPower - defensePower, 1);
                TakeDamage(damageToHero);

                Vector2 knockDir = (enemy.transform.position - transform.position).normalized;
                enemy.GetKnockback(knockDir, enemy.knockbackForce);

                // �E�҂���u�~�߂�
                StartCoroutine(Stagger());
            }
        }
    }

    private IEnumerator Stagger()
    {
        isStagger = true;
        rb.velocity = Vector2.zero; // ���S��~
        yield return new WaitForSeconds(staggerTime);
        isStagger = false;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Down();
        }
            UpdateUI();
    }

    public void UpdateUI()
    {
        if (hpSlider != null)
        {
            hpSlider.value = (float)currentHP / maxHP;
        }
    }
    private void Down()
    {
        isDown = true;

        if (scPanel != null)
        {
            scPanel.SetActive(true);
        }

        rb.velocity = Vector2.zero;
        spacePressCount = 0;
    }
    private void Revive()
    {
        isDown = false;

        if (scPanel != null)
        {
            scPanel.SetActive(false);
        }

        currentHP = maxHP;
        UpdateUI();
        animator.SetBool("IsDown", false);
    }
}
