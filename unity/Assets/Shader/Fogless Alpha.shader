// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TurboChilli/Unlit/Color/Fogless Alpha" {
Properties {
 _Color ("Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 _Alpha ("Alpha", Float) = 0.750000
}
SubShader { 
 LOD 200
 Tags { "LIGHTMODE"="Always" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="Always" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" }
  Blend SrcAlpha OneMinusSrcAlpha
  GpuProgramID 54125
CGPROGRAM
//#pragma target 4.0

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"


#define CODE_BLOCK_VERTEX
//uniform float4x4 UNITY_MATRIX_MVP;
uniform float4 _Color;
uniform float _Alpha;
struct appdata_t
{
    float4 vertex :POSITION;
    float4 texcoord :TEXCOORD0;
};

struct OUT_Data_Vert
{
    float2 xlv_TEXCOORD0 :TEXCOORD0;
    float4 vertex :SV_POSITION;
};

struct v2f
{
    float2 xlv_TEXCOORD0 :TEXCOORD0;
    float4 vertex :Position;
};

struct OUT_Data_Frag
{
    float4 color :SV_Target0;
};

OUT_Data_Vert vert(appdata_t in_v)
{
    OUT_Data_Vert out_v;
    float4 tmpvar_1;
    tmpvar_1.w = 1;
    tmpvar_1.xyz = in_v.vertex.xyz;
    out_v.vertex = UnityObjectToClipPos(tmpvar_1);
    out_v.xlv_TEXCOORD0 = in_v.texcoord.xy;
    return out_v;
}

#define CODE_BLOCK_FRAGMENT
OUT_Data_Frag frag(v2f in_f)
{
    OUT_Data_Frag out_f;
    float4 colr_1;
    colr_1.xyz = _Color.xyz;
    colr_1.w = (_Color.w * _Alpha);
    out_f.color = colr_1;
    return out_f;
}


ENDCG

}
}
Fallback "Unlit/Transparent Cutout"
}