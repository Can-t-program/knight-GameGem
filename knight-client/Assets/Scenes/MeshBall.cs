using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBall : MonoBehaviour
{
    private static int  mBaseColorId    = Shader.PropertyToID("_BaseColor");
    private static int  mCutOffId       = Shader.PropertyToID("_Cutoff");

    [SerializeField]
    private Mesh        mMesh           = default;
    [SerializeField]
    private Material    mMaterial       = default;

    private Matrix4x4[] mMatrices       = new Matrix4x4[1023];
    private Vector4[]   mBaseColors     = new Vector4[1023];
    private float[]     mCutOffs        = new float[1023];

    private MaterialPropertyBlock       mBlock;

    void Awake()
    {
        for (int i = 0; i < this.mMatrices.Length; i++)
        {
            this.mMatrices[i] = Matrix4x4.TRS(
                Random.insideUnitSphere * 10f, 
                Quaternion.Euler(Random.value * 360f, Random.value * 360f, Random.value * 360f), 
                Vector3.one * Random.Range(0.5f, 1.5f)
            );
            this.mBaseColors[i] =
                new Vector4(Random.value, Random.value, Random.value, Random.Range(0.5f, 1.0f));
            this.mCutOffs[i] = Random.Range(0.3f, 0.7f);
        }
    }

    void Update()
    {
        if (this.mBlock == null)
        {
            this.mBlock = new MaterialPropertyBlock();
            this.mBlock.SetVectorArray(mBaseColorId, this.mBaseColors);
            this.mBlock.SetFloatArray(mCutOffId, this.mCutOffs);
        }
        Graphics.DrawMeshInstanced(this.mMesh, 0, this.mMaterial, this.mMatrices, 1023, this.mBlock);
    }
}
