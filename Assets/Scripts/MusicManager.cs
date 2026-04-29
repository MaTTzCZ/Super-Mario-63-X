using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Range(0, 5)] [SerializeField] private float startTimeDelay;
    [SerializeField] private AudioSource audioSourceStart;
    [SerializeField] private AudioSource audioSourceLoop;
    [SerializeField] private AudioClip audioClipStart;
    [SerializeField] private AudioClip audioClipLoop;
    
    
    private double startTime;
    

    public void Start()
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