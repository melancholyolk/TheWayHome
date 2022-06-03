using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShowShadow : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        GetComponent<SpriteRenderer>().receiveShadows = true;
        GetComponent<SpriteRenderer>().shadowCastingMode = ShadowCastingMode.TwoSided;
    }
}
