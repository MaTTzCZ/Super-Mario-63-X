using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AudioClip levelMusicStart;
    [SerializeField] private AudioClip levelMusicLoop;

    private void Start()
    {
        MusicManager.Instance.Play(levelMusicStart, levelMusicLoop);
    }
}
