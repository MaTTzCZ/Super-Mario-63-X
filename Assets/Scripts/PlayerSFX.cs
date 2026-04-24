using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [Header("Movement")] 
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private AudioClip doubleJumpSound;
    [SerializeField] private AudioClip[] tripleJumpSounds;
    [SerializeField] private AudioClip fallingSound;
    [Header("Collectibles")]
    [SerializeField] private AudioClip shineSpriteCollectedSound;

    public void PlayJumpSound(float volume)
    {
        SFXManager.instance.PlayRandomSFXClip(jumpSounds, transform, volume);
    }
    public void PlayDoubleJumpSound(float volume)
    {
        SFXManager.instance.PlaySFXClip(doubleJumpSound, transform, volume);
    }
    public void PlayTripleJumpSound(float volume)
    {
        SFXManager.instance.PlayRandomSFXClip(tripleJumpSounds, transform, volume);
    }
    public AudioSource PlayFallingSound(float volume)
    {
        return SFXManager.instance.PlaySFXClip(fallingSound, transform, volume);
    }

    public AudioSource PlayShineSpriteCollectedSound(float volume)
    {
        return SFXManager.instance.PlaySFXClip(shineSpriteCollectedSound, transform, volume);
    }
}