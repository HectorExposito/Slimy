using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas levelCanvas;

    public GameObject lockLevel2;
    public GameObject lockLevel3;

    public Image[] unlockedBuckets;
    public Image[] colorBuckets;

    public Sound menuSong;
    public Sound buttonSound;
    private void Start()
    {
        AudioManager.Instance.PlaySound(menuSong);

        if (!PlayerPrefs.HasKey(PlayerPrefsStrings.firstTime))
        {
            SetPlayerPrefsInitialValues();
            PlayerPrefs.SetInt(PlayerPrefsStrings.firstTime,1);
        }
        else
        {
            if (PlayerPrefs.GetInt(PlayerPrefsStrings.firstTime) == 1)
            {
                
                PlayerPrefs.SetInt(PlayerPrefsStrings.firstTime, 2);
            }
            
        }
        BucketsManager();
    }
    public void LoadCreditsScene()
    {
        StartCoroutine(ButtonSoundCoroutine());
        SceneManager.LoadScene(Scenes.credits);
    }

    public void LoadLevelCanvas()
    {
        StartCoroutine(ButtonSoundCoroutine());
        menuCanvas.gameObject.SetActive(false);
        levelCanvas.gameObject.SetActive(true);
        LockManager();
    }

    public void ExitGame()
    {
        StartCoroutine(ButtonSoundCoroutine());
        Application.Quit();
    }

    public void LoadMenuCanvas()
    {
        StartCoroutine(ButtonSoundCoroutine());
        levelCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(ButtonSoundCoroutine());
        switch (level)
        {
            case 1:
                SceneManager.LoadScene(Scenes.level1);
                break;
            case 2:
                if (PlayerPrefs.HasKey(PlayerPrefsStrings.level1) && PlayerPrefs.GetInt(PlayerPrefsStrings.level1)==1)
                {
                    SceneManager.LoadScene(Scenes.level2);
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt(PlayerPrefsStrings.level2) == 1)
                {
                    SceneManager.LoadScene(Scenes.level3);
                }
                break;
        }
    }

    private void LockManager()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsStrings.level1) == 1)
        {
            lockLevel2.SetActive(false);
        }
        if (PlayerPrefs.GetInt(PlayerPrefsStrings.level2) == 1)
        {
            lockLevel3.SetActive(false);
        }
    }

    private void SetPlayerPrefsInitialValues()
    {
        PlayerPrefs.SetInt(PlayerPrefsStrings.level1, 0);
        PlayerPrefs.SetInt(PlayerPrefsStrings.level2, 0);
        PlayerPrefs.SetInt(PlayerPrefsStrings.level3, 0);
    }

    public void ResetPrefs()
    {
        SetPlayerPrefsInitialValues();
    }

    public void BucketsManager()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsStrings.level1) == 1)
        {
            unlockedBuckets[0].gameObject.SetActive(false);
            colorBuckets[0].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt(PlayerPrefsStrings.level2) == 1)
        {
            unlockedBuckets[1].gameObject.SetActive(false);
            colorBuckets[1].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt(PlayerPrefsStrings.level3) == 1)
        {
            unlockedBuckets[2].gameObject.SetActive(false);
            colorBuckets[2].gameObject.SetActive(true);
        }
    }

    private IEnumerator ButtonSoundCoroutine()
    {
        AudioManager.Instance.PlaySound(buttonSound);
        yield return new WaitForSeconds(buttonSound.clip.length);
    }
}
