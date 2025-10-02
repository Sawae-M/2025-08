using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("出現させる敵のプレハブ一覧")]
    public GameObject[] enemyPrefabs; // 複数の敵プレハブを登録できる

    [Header("出現設定")]
    public float spawnInterval = 5f;
    public Transform[] spawnPoints; // 敵の出現位置候補

    public float surviveTime = 30f;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0) return;

        // 出現位置をランダムで選ぶ
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 敵プレハブをランダムで選ぶ
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Z座標を0に固定
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.z = 0f;

        // 敵を生成
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
