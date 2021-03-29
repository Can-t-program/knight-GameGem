using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Profiling;

namespace Framework.RP
{
    public partial class KnightScriptableRenderer
    {
#if UNITY_EDITOR
        private static ShaderTagId[]    mLegacyShaderTagIds = new ShaderTagId[] 
        {
            new ShaderTagId("Awalys"),
            new ShaderTagId("ForwardBase"),
            new ShaderTagId("PrepassBase"),
            new ShaderTagId("Vertex"),
            new ShaderTagId("VertexLMRGBM"),
            new ShaderTagId("VertexLM")
        };
        private static Material         mErrorMaterial;
        private string                  mSampleName { get; set; }

        partial void DrawUnsupportedShaders()
        {
            if (mErrorMaterial == null)
                mErrorMaterial = new Material(Shader.Find("Hidden/InternalErrorShader"));

            var rDrawingSettings = new DrawingSettings(mLegacyShaderTagIds[0], new SortingSettings(this.mCamera))
            {
                overrideMaterial = mErrorMaterial
            };
            for (int i = 1; i < mLegacyShaderTagIds.Length; i++)
            {
                rDrawingSettings.SetShaderPassName(i, mLegacyShaderTagIds[i]);
            }
            var rFilteringSettings = FilteringSettings.defaultValue;
            this.mContext.DrawRenderers(this.mCullingResults, ref rDrawingSettings, ref rFilteringSettings);
        }

        partial void DrawGizmos()
        {
            if (UnityEditor.Handles.ShouldRenderGizmos())
            {
                this.mContext.DrawGizmos(this.mCamera, GizmoSubset.PreImageEffects);
                this.mContext.DrawGizmos(this.mCamera, GizmoSubset.PostImageEffects);
            }
        }

        partial void PrepareForSceneWindow()
        {
            if (this.mCamera.cameraType == CameraType.SceneView)
            {
                ScriptableRenderContext.EmitWorldGeometryForSceneView(this.mCamera);
            }
        }

        partial void PrepareBuff()
        {
            Profiler.BeginSample("Editor Only");
            this.mCmdBuffer.name = this.mCamera.name;
            mSampleName = this.mCamera.name;
            Profiler.EndSample();
        }
#else
        private const string        mSampleName = mBufferName;
#endif
    }
}
