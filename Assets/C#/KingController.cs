using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class KingController : MonoBehaviour
{
    [Header("ステータス")]
    public int maxHP = 100;
    public int currentHP;

    [Header("UI")]
    public Slider hpSlider;

    [Header("回復アイテム")]
    public GameObject healItemPrefab;
    public float spawnInterval = 10f;

    [Header("ゲームオーバー演出")]
    public GameObject gameOverEffectPrefab;
    public float delayBeforeEffect = 1f;
    public float delayBeforeScene = 2f;

    [Header("効果音")]
    public AudioClip hitSound;   // 敵とぶつかったときの音
    private AudioSource audioSource;

    private float timer;
    private Rigidbody2D rb;
    private bool isDead = false; // 多重実行防止

    private void Start()
    {
        if (TimeKeep.Instance != null) TimeKeep.Instance.playTime = 0f;

        currentHP = maxHP;
        UpdateHPUI();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 回復アイテムの生成
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnHealItem();
            timer = 0f;
        }
    }

    void SpawnHealItem()
    {
        Vector3 spawnPos = transform.position + new Vector3(
            Random.Range(-2f, 2f),
            Random.Range(-2f, 2f),
            0);
        Instantiate(healItemPrefab, spawnPos, Quaternion.identity);
    }

    // 敵に衝突したときの処理を追加
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

            if (collision.gameObject.CompareTag("Enemy"))
         {

            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                // Kingがダメージを受ける
                int damage = Mathf.Max(enemy.attackPower - 5, 1);
                TakeDamage(damage);

                // ノックバック方向を計算
                Vector2 dirToEnemy = (enemy.transform.position - transform.position).normalized;
                Vector2 dirToKing = -dirToEnemy;

                // Kingを吹き飛ばす
                rb.velocity = Vector2.zero;
                rb.AddForce(dirToKing * 5f, ForceMode2D.Impulse);

                // 敵を吹き飛ばす（EnemyController 側に GetKnockback がある前提）
                enemy.GetKnockback(dirToEnemy, enemy.knockbackForce);
            }
        }
    }

    // ダメージ処理
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        UpdateHPUI();

        if (currentHP == 0)
        {
            StartCoroutine(DieSequence());
        }
    }

    // HPバー更新
    void UpdateHPUI()
    {
        if (hpSlider != null)
        {
            hpSlider.value = (float)currentHP / maxHP;
        }
    }

    // 死亡
    private IEnumerator DieSequence()
    {
        isDead = true;
        GameObject obj = GameObject.Find("Sound");
        obj.SetActive(false);
        Time.timeScale = 0f;

        // 1秒間静止
        //if (rb != null) rb.velocity = Vector2.zero;
        yield return new WaitForSecondsRealtime(delayBeforeEffect);

        // Kingを非表示にする
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;
        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // 演出プレハブを生成
        if (gameOverEffectPrefab != null)
        {
            Instantiate(gameOverEffectPrefab, transform.position, Quaternion.identity);
        }

        // 演出を見せる時間だけ待機
        yield return new WaitForSecondsRealtime(delayBeforeScene);

        Time.timeScale = 1f;

        // ゲームオーバーシーン
        SceneManager.LoadScene("End");
    }
}
