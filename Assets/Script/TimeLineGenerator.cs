using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject;

    [Header("Activation Track")]
    [SerializeField] private float _activeStart = 0;
    [SerializeField] private float _activeDuration = 5f;
    [Header("Animation Track")]
    [SerializeField] private Vector3 _startRotation;
    [SerializeField] private Vector3 _endRotation;
    [SerializeField] private float _rotationStartTime = 0;
    [SerializeField] private float _rotationEndTime = 5f;

    [Header("Audio Track")]
    [SerializeField] private AudioClip _backGroundAudio;
    [SerializeField] private float _audioActiveStart = 0;
    [SerializeField] private float _audioActiveDuration = 5f;

    private void Start()
    {
        //  Make sure the object has an Animator
        Animator animator = _targetObject.GetComponent<Animator>();
        if (animator == null)
            animator = _targetObject.AddComponent<Animator>();

        //  Create a PlayableDirector on this controller
        var director = gameObject.AddComponent<PlayableDirector>();
        director.playOnAwake = false;

        //  Create a TimelineAsset
        var timeline = ScriptableObject.CreateInstance<TimelineAsset>();
        director.playableAsset = timeline;

        // Set the TimeLine Duration to the Max Duration + 0.1s
        var maxDuration = Mathf.Max(_activeDuration + _activeStart, _rotationEndTime + _rotationStartTime, _audioActiveDuration + _audioActiveStart) + 0.1f;
        timeline.durationMode = TimelineAsset.DurationMode.FixedLength;
        timeline.fixedDuration = maxDuration;

        // Create an Activation track inside TimeLine
        var activTrack=timeline.CreateTrack<ActivationTrack>(null,"ActivTrack");
        
        // Create The Acrivation Clip
        var activClip=activTrack.CreateDefaultClip();
        activClip.start = _activeStart;
        activClip.duration = _activeDuration;


        var audioTrack = timeline.CreateTrack<AudioTrack>(null, "AudioTrack");
        var audioClip = audioTrack.CreateClip(_backGroundAudio);
        audioClip.start = _audioActiveStart;
        audioClip.duration = _audioActiveDuration;
        


        //  Create an AnimationTrack inside Timeline
        var animTrack = timeline.CreateTrack<AnimationTrack>(null, "AnimTrack");

        //  Create an AnimationClip at runtime
        AnimationClip clip = new AnimationClip();

        // Rotation curves (Euler angles)
        AnimationCurve rotationX = AnimationCurve.Linear(_rotationStartTime, _startRotation.x, _rotationEndTime, _endRotation.x);
        AnimationCurve rotationY = AnimationCurve.Linear(_rotationStartTime, _startRotation.y, _rotationEndTime, _endRotation.y);
        AnimationCurve rotationZ = AnimationCurve.Linear(_rotationStartTime, _startRotation.z, _rotationEndTime, _endRotation.z);

        clip.SetCurve("", typeof(Transform), "localEulerAnglesRaw.x", rotationX);
        clip.SetCurve("", typeof(Transform), "localEulerAnglesRaw.y", rotationY);
        clip.SetCurve("", typeof(Transform), "localEulerAnglesRaw.z", rotationZ);

        //  Create a Timeline Clip and assign the AnimationClip to it
        TimelineClip timelineClip = animTrack.CreateClip(clip);
        timelineClip.duration = _rotationEndTime;

        //  Bind the track to the targetObject
        director.SetGenericBinding(activTrack, _targetObject);
        director.SetGenericBinding(animTrack, _targetObject);
        director.SetGenericBinding(audioTrack, _targetObject);
        
        StartCoroutine(TrackStarter(director));
    }
    private IEnumerator TrackStarter(PlayableDirector director)
    {
        yield return null;

        //  Play the timeline
        director.Play();
    }
}
