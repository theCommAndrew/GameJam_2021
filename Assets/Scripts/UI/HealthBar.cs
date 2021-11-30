using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public List<Image> hearts;
    public Image heartPrefab;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private int maxHearts = 0;
    private Image end = null;
    public Animator animator;


    private void Awake()
    {
        Player.updateHealth += (playerHealth, maxHealth) => updateHealthBar(playerHealth, maxHealth);
    }


    private void updateHealthBar(int playerHealth, int maxHealth)
    {
        if (this == null)
            return;

        if (maxHealth > maxHearts)
        {
            for (int i = 0; i < maxHealth - maxHearts; i++)
            {

                Vector3 endPos = end == null ? transform.position : new Vector3(end.transform.position.x, end.transform.position.y, end.transform.position.z);

                Image newHeart = Instantiate(heartPrefab, transform);
                newHeart.transform.position = new Vector3(endPos.x + 60, endPos.y, endPos.z);
                hearts.Add(newHeart);
                end = newHeart;
            }
            maxHearts = maxHealth;
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < playerHealth)
            {

                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
