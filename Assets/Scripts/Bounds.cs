using System;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;

    public static Bounds Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
