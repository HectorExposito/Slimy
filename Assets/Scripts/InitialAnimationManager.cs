using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class InitialAnimationManager : MonoBehaviour
{
    public GameObject animation;
    void Start()
    {
        StartCoroutine(LoadMenuCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(Scenes.menu);
        }
    }

    private IEnumerator LoadMenuCoroutine()
    {
        AnimationClip[] clip=animation.GetComponent<Animator>().runtimeAnimatorController.animationClips;
        yield return new WaitForSeconds(clip[0].length);
        SceneManager.LoadScene(Scenes.menu);
    }
}
