using System;
using UnityEngine;

public class OneUpMushroom : MonoBehaviour
{
    [SerializeField] private AudioClip oneUpMushroomCollectedSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SFXManager.Instance.PlaySFXClip(oneUpMushroomCollectedSound, transform);
            Destroy(gameObject);
        }
    }
}
