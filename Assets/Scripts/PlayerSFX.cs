using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private SFXManager  SFXManager;
    [Header("Movement")] 
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private AudioClip doubleJumpSound;
    [SerializeField] private AudioClip[] tripleJumpSounds;
    [SerializeField] private AudioClip fallingSound;
    [Header("Collectibles")]
    [SerializeField] private AudioClip shineSpriteCollectedSound;

    public void PlayJumpSound()
    {
        SFXManager.PlayRandomSFXClip(jumpSounds, transform);
    }
    public void PlayDoubleJumpSound()
    {
        SFXManager.PlaySFXClip(doubleJumpSound, transform);
    }
    public void PlayTripleJumpSound()
    {
        SFXManager.PlayRandomSFXClip(tripleJumpSounds, transform);
    }
    public AudioSource PlayFallingSound()
    {
        return SFXManager.PlaySFXClip(fallingSound, transform);
    }

    public AudioSource PlayShineSpriteCollectedSound()
    {
        return SFXManager.PlaySFXClip(shineSpriteCollectedSound, transform);
    }
}