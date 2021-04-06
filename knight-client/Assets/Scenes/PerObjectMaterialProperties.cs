using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour
{
    private static int                      mBaseColorId = Shader.PropertyToID("_BaseColor");
    private static int                      mCutoffId    = Shader.PropertyToID("_CutOff");

    private static MaterialPropertyBlock    mBlock;

    [SerializeField]
    private Color                           mBaseColor   = Color.white;
    [SerializeField, Range(0f, 1f)]
    private float                           mCutOff      = 0.5f;

    private void Awake()
    {
        this.OnValidate();
    }

    private void OnValidate()
    {
        if (mBlock == null)
            mBlock = new MaterialPropertyBlock();

        mBlock.SetColor(mBaseColorId, this.mBaseColor);
        mBlock.SetFloat(mCutoffId, this.mCutOff);
        
        this.GetComponent<Renderer>().SetPropertyBlock(mBlock);
    }
}
