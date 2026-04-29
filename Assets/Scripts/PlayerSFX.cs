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

    public void PlayJumpSound()
    {
        SFXManager.Instance.PlayRandomSFXClip(jumpSounds, transform);
    }
    public void PlayDoubleJumpSound()
    {
        SFXManager.Instance.PlaySFXClip(doubleJumpSound, transform);
    }
    public void PlayTripleJumpSound()
    {
        SFXManager.Instance.PlayRandomSFXClip(tripleJumpSounds, transform);
    }
    public AudioSource PlayFallingSound()
    {
        return SFXManager.Instance.PlaySFXClip(fallingSound, transform);
    }

    public AudioSource PlayShineSpriteCollectedSound()
    {
        return SFXManager.Instance.PlaySFXClip(shineSpriteCollectedSound, transform);
    }
}