using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonWithSound : MonoBehaviour
{
    public AudioSource audioSource;   // 効果音を鳴らす AudioSource
    public AudioClip clickSound;      // 効果音
    public string nextSceneName;      // 遷移先シーン名

    public void OnClick()
    {
        StartCoroutine(PlaySoundAndChangeScene());
    }

    private System.Collections.IEnumerator PlaySoundAndChangeScene()
    {
        // 効果音再生
        audioSource.PlayOneShot(clickSound);

        // 効果音の長さだけ待つ
        yield return new WaitForSeconds(clickSound.length);

        // シーン切り替え
        SceneManager.LoadScene(nextSceneName);
    }
}
