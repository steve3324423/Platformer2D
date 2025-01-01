using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputReader : MonoBehaviour
{
    public const string Horizontal = "Horizontal";

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);
    }
}
