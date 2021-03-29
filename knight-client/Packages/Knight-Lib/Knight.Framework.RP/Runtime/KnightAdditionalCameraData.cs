using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Framework.RP
{
    public class KnightAdditionalCameraData : MonoBehaviour
    {
        public CameraRenderType CameraRenderType;
        public bool             IsUICamera;

        public Camera           PrevCamera;
        public Camera           NextCamera;
    }
}
