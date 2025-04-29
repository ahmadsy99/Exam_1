using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineStarter : MonoBehaviour
{
    private PlayableDirector _director;
    private void Awake()
    {
        _director = GetComponent<PlayableDirector>();
    }
    private IEnumerator Start()
    {
        yield return null;
        _director.Play();
    }

}
