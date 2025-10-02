using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //ƒV[ƒ“–¼‚ÅØ‚è‘Ö‚¦‚é
    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // InputField ‚É“ü—Í‚³‚ê‚½•¶š—ñ‚ÅØ‚è‘Ö‚¦‚é
    public void Load(InputField inputField)
    {
        Load(inputField.text);
    }
}
