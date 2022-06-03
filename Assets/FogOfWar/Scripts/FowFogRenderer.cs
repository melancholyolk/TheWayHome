using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FogOfWar
{
    public class FowFogRenderer : MonoBehaviour
    {
        public GameObject renderer;
        public FowManager fowManager;
        public GameObject rendererPrefab;
        public Material material;
        // Use this for initialization
        void Start()
        {
            renderer.transform.localPosition = Vector3.zero;
            renderer.transform.localScale = new Vector3(fowManager.FogSizeX/2, 1, fowManager.FogSizeY/2);
        }

        // Update is called once per frame
        void Update()
        {
            if (fowManager.map.FogTexture != null)
            {
                material.SetTexture("_MainTex", fowManager.map.FogTexture);
            }
        }
    }

}
