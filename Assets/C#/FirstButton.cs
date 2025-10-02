using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //�V�[�����Ő؂�ւ���
    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // InputField �ɓ��͂��ꂽ������Ő؂�ւ���
    public void Load(InputField inputField)
    {
        Load(inputField.text);
    }
}
