using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public Text resultText;   // UIのText参照

    void Start()
    {
        // ✅ TimeKeep.Instance から時間を取得する
        float time = TimeKeep.Instance.playTime;

        string rank = GetRank(time);

        resultText.text = $"TIME: {time:F1}秒\nRANK: {rank}";
    }

    string GetRank(float time)
    {
        if (time >= 50f) return "SS";
        if (time >= 40f) return "S";   
        if (time >= 30f) return "A";
        if (time >= 20f) return "B";
        return "C";
    }
}
