using UnityEngine;

public class TimeKeep
    : MonoBehaviour
{
    public static TimeKeep Instance;  // シングルトン

    public float playTime = 0f;  // 経過時間

    private void Awake()
    {
        // シングルトン（シーンが変わっても破棄されない）
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // タイムスケールが0の時は計測しない
        if (Time.timeScale > 0)
        {
            playTime += Time.deltaTime;
        }
    }
}
