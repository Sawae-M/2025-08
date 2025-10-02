using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HeroController hero = collision.GetComponent<HeroController>();
            if (hero != null)
            {
                hero.currentHP = hero.maxHP; // 全回復
                hero.UpdateUI();             // HPバー更新
            }

            Destroy(gameObject); // アイテム消滅
        }
    }
}
