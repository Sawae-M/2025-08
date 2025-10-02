using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("�o��������G�̃v���n�u�ꗗ")]
    public GameObject[] enemyPrefabs; // �����̓G�v���n�u��o�^�ł���

    [Header("�o���ݒ�")]
    public float spawnInterval = 5f;
    public Transform[] spawnPoints; // �G�̏o���ʒu���

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

        // �o���ʒu�������_���őI��
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // �G�v���n�u�������_���őI��
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Z���W��0�ɌŒ�
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.z = 0f;

        // �G�𐶐�
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
