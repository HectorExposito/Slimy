using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private static int actualLevel = 0;
    private static int previousLevel = 0;

    public GameObject tutorial;
    public Canvas levelCompletedCanvas;

    public Sound buttonSound;
    public Sound gameOverSound;
    public Sound levelCompletedFx;

    public FireballSpawner[] fireballSpawner;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

        switch (SceneManager.GetActiveScene().name)
        {
            case Scenes.level1:
                actualLevel = 1;
                break;
            case Scenes.level2:
                actualLevel = 2;
                break;
            case Scenes.level3:
                actualLevel = 3;
                break;
            case Scenes.gameOver:
                AudioManager.Instance.PlaySound(gameOverSound);
                previousLevel = actualLevel;
                actualLevel = 0;
                break;
            default:
                previousLevel = actualLevel;
                actualLevel = 0;
                break;
        }
    }
    public void PlayAgain()
    {
        StartCoroutine(ButtonSoundCoroutine());
        switch (previousLevel)
        {
            case 1:
                SceneManager.LoadScene(Scenes.level1);
                break;
            case 2:
                SceneManager.LoadScene(Scenes.level2);
                break;
            case 3:
                SceneManager.LoadScene(Scenes.level3);
                break;
        }
        PlayerManager.Instance.ResetSpawn();
    }

    public void GameOver()
    {
        SceneManager.LoadScene(Scenes.gameOver);
    }
    public void LevelCompleted(short level)
    {
        switch (level)
        {
            case 0:
                PlayerPrefs.SetInt(PlayerPrefsStrings.level1, 1);
                break;
            case 1:
                PlayerPrefs.SetInt(PlayerPrefsStrings.level2, 1);
                break;
            case 2:
                for (int i = 0; i < fireballSpawner.Length; i++)
                {
                    fireballSpawner[i].levelCompleted = true;
                }
                PlayerPrefs.SetInt(PlayerPrefsStrings.level3, 1);
                break;
        }
        StartCoroutine(playLevelCompletedFxCoroutine());
        StartCoroutine(ShowCanvasCoroutine());

        
    }

    public void DisableTutorial()
    {
        if (actualLevel == 1)
        {
            tutorial.SetActive(false);
        }

    }

    public void ChangeLevel()
    {
        StartCoroutine(ButtonSoundCoroutine());
        switch (actualLevel)
        {
            case 1:
                SceneManager.LoadScene(Scenes.level2);
                break;
            case 2:
                SceneManager.LoadScene(Scenes.level3);
                break;
            case 3:
                break;

        }
    }

    public void LoadMenu()
    {
        StartCoroutine(ButtonSoundCoroutine());
        SceneManager.LoadScene(Scenes.menu);
    }

    private IEnumerator ShowCanvasCoroutine()
    {
        yield return new WaitForSeconds(3f);
        levelCompletedCanvas.gameObject.SetActive(true);
    }

    private IEnumerator ButtonSoundCoroutine()
    {
        AudioManager.Instance.PlaySound(buttonSound);
        yield return new WaitForSeconds(buttonSound.clip.length);
    }

    public IEnumerator playLevelCompletedFxCoroutine()
    {
        GameplayAudioPlayer.Instance.StopMusic();
        AudioManager.Instance.PlaySound(levelCompletedFx);
        yield return new WaitForSeconds(levelCompletedFx.clip.length);
    }
}
