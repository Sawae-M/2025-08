using UnityEngine;

public class TimeKeep
    : MonoBehaviour
{
    public static TimeKeep Instance;  // �V���O���g��

    public float playTime = 0f;  // �o�ߎ���

    private void Awake()
    {
        // �V���O���g���i�V�[�����ς���Ă��j������Ȃ��j
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
        // �^�C���X�P�[����0�̎��͌v�����Ȃ�
        if (Time.timeScale > 0)
        {
            playTime += Time.deltaTime;
        }
    }
}
