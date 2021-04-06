#ifndef CUSTOM_UNLIT_PASS_INCLUDED
#define CUSTOM_UNLIT_PASS_INCLUDED

#include "../ShaderLibrary/Common.hlsl"


CBUFFER_START(UnityPerMaterial)
float4 _BaseColor;
float4 _BaseMap_ST;
CBUFFER_END

TEXTURE2D(_BaseMap);
SAMPLER(sampler_BaseMap);


float4 UnlitPassVertex(float3 positionOS : POSITION) : SV_POSITION
{
	float3 positionWS = TransformObjectToWorld(positionOS);
	return TransformWorldToHClip(positionWS);
}

float4 UnlitPassFragment() : SV_TARGET
{
	return _BaseColor;
}

#endif