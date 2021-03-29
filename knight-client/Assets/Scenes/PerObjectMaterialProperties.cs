using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour
{
    private static int                      mBaseColorId = Shader.PropertyToID("_BaseColor");
    private static MaterialPropertyBlock    mBlock;

    [SerializeField]
    private Color                           mBaseColor = Color.white;

    private void Awake()
    {
        this.OnValidate();
    }

    private void OnValidate()
    {
        if (mBlock == null)
            mBlock = new MaterialPropertyBlock();
        mBlock.SetColor(mBaseColorId, mBaseColor);
        this.GetComponent<Renderer>().SetPropertyBlock(mBlock);
    }
}
