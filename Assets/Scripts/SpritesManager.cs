using UnityEngine;

public class SpritesManager : MonoBehaviour
{
    public static SpritesManager Instance;
    public GameObject[] originalSprites;
    public Sprite[] spritesBlackWhite;
    public Sprite[] spritesGreen;
    public Sprite[] spritesGreenBlue;
    public Sprite[] activeCheckpointSprites;
    public GameObject[] lava;
    public GameObject[] lavaGB;
    public GameObject fireball;
    public GameObject fireballGB;
    int levelsCompletedCounter=0;
    private int numberOfLevels = 3;

    void Start()
    {
        Instance = this;
        levelsCompletedCounter = 0;

        CountLevelsCompleted();
        ChangeSprites();
        ChangePrefabs();
    }

    private void ChangeSprites()
    {
        switch (levelsCompletedCounter)
        {
            case 0:
                for (int i = 0; i < originalSprites.Length; i++)
                {
                    originalSprites[i].GetComponent<SpriteRenderer>().sprite = spritesBlackWhite[i];
                }
                break;
            case 1:
                for (int i = 0; i < originalSprites.Length; i++)
                {
                    originalSprites[i].GetComponent<SpriteRenderer>().sprite = spritesGreen[i];
                }
                break;
            case 2:
                for (int i = 0; i < originalSprites.Length; i++)
                {
                    originalSprites[i].GetComponent<SpriteRenderer>().sprite = spritesGreenBlue[i];
                }
                break;
            case 3:
                
                break;
        }
    }

    public Sprite GetActiveCheckpointsSprites()
    {
        switch (levelsCompletedCounter)
        {
            case 0:
                return activeCheckpointSprites[0];
            case 1:
                return activeCheckpointSprites[1];
            case 2:
                return activeCheckpointSprites[2];
            default:
                return activeCheckpointSprites[3];
        }
        
    }
    
    public void ChangePrefabs()
    {
        if (levelsCompletedCounter==2)
        {
            for (int i = 0; i < lava.Length; i++)
            {
                lava[i].SetActive(false);
                lavaGB[i].SetActive(true);
            }
        }
    }

    private void CountLevelsCompleted()
    {
        for (int i = 0; i < numberOfLevels; i++)
        {
            switch (i)
            {
                case 0:
                    if (PlayerPrefs.GetInt(PlayerPrefsStrings.level1) == 1)
                    {
                        levelsCompletedCounter++;
                    }
                    break;
                case 1:
                    if (PlayerPrefs.GetInt(PlayerPrefsStrings.level2) == 1)
                    {
                        levelsCompletedCounter++;
                    }
                    break;
                case 2:
                    if (PlayerPrefs.GetInt(PlayerPrefsStrings.level3) == 1)
                    {
                        levelsCompletedCounter++;
                    }
                    break;
            }
        }
    }

    public int GetLevelsCompleted()
    {
        return levelsCompletedCounter;
    }
    public GameObject FireballSelector()
    {
        if(levelsCompletedCounter==2)
        {
            return fireballGB;
        }
        else
        {
            return fireball;
        }
    }
}
