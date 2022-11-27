using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public  GameObject[] hearts;
    private static int lifes;
    public static HeartManager Instance;

    private void Awake()
    {
        Instance = this;
        lifes = hearts.Length;
    }
    public void DecreaseLife()
    {
        lifes--;
        if (lifes<=0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void RecoverLife()
    {
        lifes = hearts.Length;
    }

    public int GetLifes()
    {
        return lifes;
    }
    private void Update()
    {
        switch (lifes)
        {
            case 3:
                hearts[0].gameObject.SetActive(true);
                hearts[1].gameObject.SetActive(true);
                hearts[2].gameObject.SetActive(true);
                break;
            case 2:
                hearts[0].gameObject.SetActive(false);
                hearts[1].gameObject.SetActive(true);
                hearts[2].gameObject.SetActive(true);
                break;
            case 1:
                hearts[0].gameObject.SetActive(false);
                hearts[1].gameObject.SetActive(false);
                hearts[2].gameObject.SetActive(true);
                break;
            case 0:
                hearts[0].gameObject.SetActive(false);
                hearts[1].gameObject.SetActive(false);
                hearts[2].gameObject.SetActive(false);
                break;
        }
    }
}
