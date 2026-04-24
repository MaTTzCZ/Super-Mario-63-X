using System;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] private AudioSource SFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public AudioSource PlaySFXClip(AudioClip clip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(SFXObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, clip.length);
        return audioSource;
        
    }
    public AudioSource PlayLoopingSFXClip(AudioClip clip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(SFXObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
        return audioSource;
        
    }
    public void PlayRandomSFXClip(AudioClip[] clips, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(SFXObject, transform.position, Quaternion.identity);
        audioSource.clip = clips[UnityEngine.Random.Range(0, clips.Length)];
        audioSource.volume = volume;
        audioSource.Play();
        float length = audioSource.clip.length;
        Destroy(audioSource.gameObject, length);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
