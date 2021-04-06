using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace Framework.RP
{
    [CreateAssetMenu(menuName = "Rendering/Knight Render Pipeline/Pipeline Asset")]
    public class KnightRenderPipelineAsset : RenderPipelineAsset
    {
        [SerializeField]
        private bool mUseDynamicBatching = true;
        [SerializeField]
        private bool mUseGPUInstancing   = true;
        [SerializeField]
        private bool mUseSRPBatcher      = true;

        protected override RenderPipeline CreatePipeline()
        {
            return new KnightRenderPipeline(this.mUseDynamicBatching, this.mUseGPUInstancing, this.mUseSRPBatcher);
        }
    }
}
