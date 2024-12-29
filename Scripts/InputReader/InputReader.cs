using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputReader : MonoBehaviour
{
    public const string S_Horizontal = "Horizontal";

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(S_Horizontal);
    }
}
