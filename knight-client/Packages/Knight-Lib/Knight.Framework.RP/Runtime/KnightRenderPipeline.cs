using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Framework.RP
{
    public class KnightRenderPipeline : RenderPipeline
    {
        private KnightScriptableRenderer mRenderer = new KnightScriptableRenderer();

        public KnightRenderPipeline()
        {
            GraphicsSettings.useScriptableRenderPipelineBatching = true;
        }

        protected override void Render(ScriptableRenderContext rContext, Camera[] rCameras)
        {
            for (int i = 0; i < rCameras.Length; i++)
            {
                this.mRenderer.Render(rContext, rCameras[i]);
            }
        }
    }
}
