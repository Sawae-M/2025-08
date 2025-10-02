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
                hero.currentHP = hero.maxHP; // �S��
                hero.UpdateUI();             // HP�o�[�X�V
            }

            Destroy(gameObject); // �A�C�e������
        }
    }
}
