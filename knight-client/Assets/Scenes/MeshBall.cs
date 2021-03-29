using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBall : MonoBehaviour
{
    private static int  mBaseColorId    = Shader.PropertyToID("_BaseColor");

    [SerializeField]
    private Mesh        mMesh           = default;
    [SerializeField]
    private Material    mMaterial       = default;

    private Matrix4x4[] mMatrices       = new Matrix4x4[1023];
    private Vector4[]   mBaseColors     = new Vector4[1023];

    private MaterialPropertyBlock       mBlock;

    void Awake()
    {
        for (int i = 0; i < this.mMatrices.Length; i++)
        {
            this.mMatrices[i] = Matrix4x4.TRS(
                Random.insideUnitSphere * 10f, Quaternion.identity, Vector3.one
            );
            this.mBaseColors[i] =
                new Vector4(Random.value, Random.value, Random.value, 1f);
        }
    }

    void Update()
    {
        if (this.mBlock == null)
        {
            this.mBlock = new MaterialPropertyBlock();
            this.mBlock.SetVectorArray(mBaseColorId, this.mBaseColors);
        }
        Graphics.DrawMeshInstanced(this.mMesh, 0, this.mMaterial, this.mMatrices, 1023, this.mBlock);
    }
}
