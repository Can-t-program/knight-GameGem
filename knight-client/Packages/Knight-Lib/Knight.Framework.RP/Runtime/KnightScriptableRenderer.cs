using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering;
using UnityEngine;

namespace Framework.RP
{
    public partial class KnightScriptableRenderer
    {
        private const string            mBufferName = "Render Camera";
        private static ShaderTagId      mUnlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");

        private ScriptableRenderContext mContext;
        private Camera                  mCamera;
        private CommandBuffer           mCmdBuffer = new CommandBuffer() { name = mBufferName };
        private CullingResults          mCullingResults;

        public void Render(ScriptableRenderContext rContext, Camera rCamera)
        {
            this.mContext = rContext;
            this.mCamera = rCamera;

            this.PrepareBuff();
            this.PrepareForSceneWindow();
            
            if (!this.Cull())
            {
                return;
            }
            
            this.SetUp();
            this.DrawVisibleGeometry();
            this.DrawUnsupportedShaders();
            this.DrawGizmos();
            this.Submit();
        }

        private bool Cull()
        {
            if (this.mCamera.TryGetCullingParameters(out var rCullingParams))
            {
                this.mCullingResults = this.mContext.Cull(ref rCullingParams);
                return true;
            }
            return false;
        }

        private void SetUp()
        {
            this.mContext.SetupCameraProperties(this.mCamera);
            CameraClearFlags rFlags = this.mCamera.clearFlags;
            this.mCmdBuffer.ClearRenderTarget(
                rFlags <= CameraClearFlags.Depth,
                rFlags == CameraClearFlags.Color,
                rFlags == CameraClearFlags.Color ? this.mCamera.backgroundColor.linear : Color.clear);
            this.mCmdBuffer.BeginSample(mSampleName);
            this.ExecuteBuffer();
        }

        private void DrawVisibleGeometry()
        {
            var rSortingSettings = new SortingSettings(this.mCamera) { criteria = SortingCriteria.CommonOpaque };
            var rDrawingSettings = new DrawingSettings(mUnlitShaderTagId, rSortingSettings);
            var rFilteringSettings = new FilteringSettings(RenderQueueRange.opaque);

            this.mContext.DrawRenderers(this.mCullingResults, ref rDrawingSettings, ref rFilteringSettings);

            this.mContext.DrawSkybox(this.mCamera);

            rSortingSettings.criteria = SortingCriteria.CommonTransparent;
            rDrawingSettings.sortingSettings = rSortingSettings;
            rFilteringSettings.renderQueueRange = RenderQueueRange.transparent;

            this.mContext.DrawRenderers(this.mCullingResults, ref rDrawingSettings, ref rFilteringSettings);
        }

        partial void DrawUnsupportedShaders();
        partial void DrawGizmos();
        partial void PrepareForSceneWindow();
        partial void PrepareBuff();

        private void Submit()
        {
            this.mCmdBuffer.EndSample(mSampleName);
            this.ExecuteBuffer();
            this.mContext.Submit();
        }

        private void ExecuteBuffer()
        {
            this.mContext.ExecuteCommandBuffer(this.mCmdBuffer);
            this.mCmdBuffer.Clear();
        }
    }
}
