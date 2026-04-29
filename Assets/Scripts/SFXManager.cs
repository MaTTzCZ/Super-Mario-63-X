using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    [SerializeField] private AudioSource SFXObject;

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

    public AudioSource PlaySFXClip(AudioClip clip, Transform transform)
    {
        var audioSource = Instantiate(SFXObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = 1;
        audioSource.Play();
        Destroy(audioSource.gameObject, clip.length);
        return audioSource;
    }

    public AudioSource PlayLoopingSFXClip(AudioClip clip, Transform transform)
    {
        var audioSource = Instantiate(SFXObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = 1;
        audioSource.loop = true;
        audioSource.Play();
        return audioSource;
    }

    public void PlayRandomSFXClip(AudioClip[] clips, Transform transform)
    {
        var audioSource = Instantiate(SFXObject, transform.position, Quaternion.identity);
        audioSource.clip = clips[UnityEngine.Random.Range(0, clips.Length)];
        audioSource.volume = 1;
        audioSource.Play();
        var length = audioSource.clip.length;
        Destroy(audioSource.gameObject, length);
    }
}