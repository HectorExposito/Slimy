using UnityEngine;
using System.Collections;

public class GameplayAudioPlayer : MonoBehaviour
{
    public Sound levelSong;
    
    public static GameplayAudioPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        AudioManager.Instance.PlaySound(levelSong);
    }
    
    public void StopMusic()
    {
        
        AudioManager.Instance.StopMusic();
    }

    public IEnumerator StopMusicCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        
    }
}
