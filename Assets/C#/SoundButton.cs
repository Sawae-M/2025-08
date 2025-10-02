using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonWithSound : MonoBehaviour
{
    public AudioSource audioSource;   // ���ʉ���炷 AudioSource
    public AudioClip clickSound;      // ���ʉ�
    public string nextSceneName;      // �J�ڐ�V�[����

    public void OnClick()
    {
        StartCoroutine(PlaySoundAndChangeScene());
    }

    private System.Collections.IEnumerator PlaySoundAndChangeScene()
    {
        // ���ʉ��Đ�
        audioSource.PlayOneShot(clickSound);

        // ���ʉ��̒��������҂�
        yield return new WaitForSeconds(clickSound.length);

        // �V�[���؂�ւ�
        SceneManager.LoadScene(nextSceneName);
    }
}
