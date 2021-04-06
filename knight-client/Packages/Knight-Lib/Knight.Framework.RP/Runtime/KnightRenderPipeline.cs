using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Framework.RP
{
    public class KnightRenderPipeline : RenderPipeline
    {
        private bool                        mIsUseDynamicBatching;
        private bool                        mIsUseGPUInstancing;

        private KnightScriptableRenderer    mRenderer = new KnightScriptableRenderer();

        public KnightRenderPipeline(bool bIsUseDynamicBatching, bool bIsUseGPUInstancing, bool bIsUseSRPBatcher)
        {
            this.mIsUseDynamicBatching = bIsUseDynamicBatching;
            this.mIsUseGPUInstancing = bIsUseGPUInstancing;
            
            GraphicsSettings.useScriptableRenderPipelineBatching = bIsUseSRPBatcher;
        }

        protected override void Render(ScriptableRenderContext rContext, Camera[] rCameras)
        {
            for (int i = 0; i < rCameras.Length; i++)
            {
                this.mRenderer.Render(rContext, rCameras[i], this.mIsUseDynamicBatching, this.mIsUseGPUInstancing);
            }
        }
    }
}
