using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTraffic : MonoBehaviour
{
    public float MinModifier;
    public float MaxModifier;

    public static float NetworkTrafficMod;

    // Update is called once per frame
    void Update()
    {
        NetworkTrafficMod = Random.Range(MinModifier, MaxModifier);
    }
}
