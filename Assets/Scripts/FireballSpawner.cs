using System.Collections;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject fireballGBPrefab;
    public GameObject fireball;
    
    public float time;
    private int levelsCompleted;

    public bool levelCompleted;

    public Sound fireballSound;
    private bool isRunning;
    void Start()
    {
        CountLevelsCompleted();
        SelectFireball();
        if (!isRunning)
        {
            StartCoroutine(SpawnFireballCoroutine());
        }
    }

    private void SpawnFireball()
    {
        Instantiate(fireball, transform.position, Quaternion.identity);

        if (!levelCompleted)
        {
            AudioManager.Instance.PlaySound(fireballSound);
        }
        
    }

    private void CountLevelsCompleted()
    {
        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0:
                    if (PlayerPrefs.GetInt(PlayerPrefsStrings.level1) == 1)
                    {
                        levelsCompleted++;
                    }
                    break;
                case 1:
                    if (PlayerPrefs.GetInt(PlayerPrefsStrings.level2) == 1)
                    {
                        levelsCompleted++;
                    }
                    break;
                case 2:
                    if (PlayerPrefs.GetInt(PlayerPrefsStrings.level3) == 1)
                    {
                        levelsCompleted++;
                    }
                    break;
            }
        }
    }
    
    private void SelectFireball()
    {
        if (levelsCompleted==2)
        {
            fireball = fireballGBPrefab;
        }
        else
        {
            fireball = fireballPrefab;
        }
    }

    private IEnumerator SpawnFireballCoroutine()
    {
        isRunning = true;
        SpawnFireball();
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnFireballCoroutine());
        isRunning = false;

    }

}
