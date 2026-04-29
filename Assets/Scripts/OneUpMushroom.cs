using System;
using UnityEngine;

public class OneUpMushroom : MonoBehaviour
{
    [SerializeField] SFXManager SFXManager;
    [SerializeField] private AudioClip oneUpMushroomCollectedSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SFXManager.PlaySFXClip(oneUpMushroomCollectedSound, transform);
            Destroy(gameObject);
        }
    }
}
