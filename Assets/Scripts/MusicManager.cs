using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float volume;
    [Range(0, 5)] [SerializeField] private float startTimeDelay;
    [SerializeField] private AudioSource audioSourceStart;
    [SerializeField] private AudioSource audioSourceLoop;
    [SerializeField] private AudioClip audioClipStart;
    [SerializeField] private AudioClip audioClipLoop;
    private double startTime;
    public static MusicManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        audioSourceStart.clip = audioClipStart;
        startTime = AudioSettings.dspTime + startTimeDelay;
        var startClipDuration = (double)audioClipStart.samples / audioClipStart.frequency;
        audioSourceStart.PlayScheduled(startTime);
        if (audioClipLoop != null)
        {
            audioSourceStart.SetScheduledEndTime(startTime + startClipDuration);
            audioSourceLoop.clip = audioClipLoop;
            audioSourceLoop.loop = true;
            audioSourceLoop.PlayScheduled(startTime + startClipDuration);
        }
        else
        {
            audioSourceStart.loop = true;
        }
    }

    private void FixedUpdate()
    {
        audioSourceStart.volume = volume;
        audioSourceLoop.volume = volume;
    }

    public void StopMusic()
    {
        audioSourceStart.Stop();
        audioSourceLoop.Stop();
    }
}