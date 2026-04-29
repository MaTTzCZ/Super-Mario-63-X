using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Range(0, 5)] [SerializeField] private float startTimeDelay;
    [SerializeField] private AudioSource audioSourceStart;
    [SerializeField] private AudioSource audioSourceLoop;
    
    
    private double startTime;

    public static MusicManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Play(AudioClip audioClipStart, AudioClip audioClipLoop)
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

    public void Stop()
    {
        audioSourceStart.Stop();
        audioSourceLoop.Stop();
    }
}