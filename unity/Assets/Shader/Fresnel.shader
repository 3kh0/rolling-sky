// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TurboChilli/Unlit/Color/Fresnel" {
Properties {
 _ColorE ("Core Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 _Color ("Mid Color", Color) = (1.000000,0.750000,0.000000,1.000000)
 _ColorEAlt ("Rim  Color", Color) = (1.000000,0.000000,0.000000,1.000000)
 _Ammount ("Ammount", Range(0.000000,1.000000)) = 0.500000
}
SubShader { 
 LOD 200
 Tags { "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" }
 Pass {
  Name "FORWARDBASE"
  Tags { "LIGHTMODE"="ForwardBase" "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" "SHADOWSUPPORT"="true" }
  GpuProgramID 3988
CGPROGRAM
//#pragma target 4.0

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"


#define CODE_BLOCK_VERTEX
//uniform float4x4 UNITY_MATRIX_MVP;
//uniform float4x4 unity_ObjectToWorld;
//uniform float4x4 unity_WorldToObject;
//uniform float3 _WorldSpaceCameraPos;
uniform float3 _Color;
uniform float3 _ColorE;
uniform float3 _ColorEAlt;
uniform float _Ammount;
struct appdata_t
{
    float4 vertex :POSITION;
    float3 normal :NORMAL;
};

struct OUT_Data_Vert
{
    float4 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float4 vertex :SV_POSITION;
};

struct v2f
{
    float4 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
};

struct OUT_Data_Frag
{
    float4 color :SV_Target0;
};

OUT_Data_Vert vert(appdata_t in_v)
{
    OUT_Data_Vert out_v;
    float4 tmpvar_1;
    tmpvar_1.w = 0;
    tmpvar_1.xyz = float3(in_v.normal);
    out_v.vertex = UnityObjectToClipPos(in_v.vertex);
    out_v.xlv_TEXCOORD0 = mul(unity_ObjectToWorld, in_v.vertex);
    out_v.xlv_TEXCOORD1 = mul(tmpvar_1, unity_WorldToObject).xyz;
    return out_v;
}

#define CODE_BLOCK_FRAGMENT
OUT_Data_Frag frag(v2f in_f)
{
    OUT_Data_Frag out_f;
    float4 tmpvar_1;
    float tmpvar_2;
    tmpvar_2 = dot(normalize(in_f.xlv_TEXCOORD1), normalize((_WorldSpaceCameraPos - in_f.xlv_TEXCOORD0.xyz)));
    float tmpvar_3;
    tmpvar_3 = max(0, ((tmpvar_2 - _Ammount) / (1 - _Ammount)));
    float tmpvar_4;
    tmpvar_4 = min(max((tmpvar_2 / _Ammount), 0), 1);
    float4 tmpvar_5;
    tmpvar_5.w = 1;
    tmpvar_5.xyz = float3((((_ColorEAlt * (1 - (tmpvar_3 + tmpvar_4))) + (_Color * tmpvar_4)) + (_ColorE * tmpvar_3)));
    tmpvar_1 = tmpvar_5;
    out_f.color = tmpvar_1;
    return out_f;
}


ENDCG

}
}
Fallback "Diffuse"
}