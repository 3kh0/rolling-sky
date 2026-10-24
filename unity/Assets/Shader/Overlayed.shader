// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TurboChilli/Unlit/Textured/OLD - Overlayed Grid" {
Properties {
 _MainTex ("First Texture", 2D) = "white" { }
 _Color ("Overlay Color", Color) = (1.000000,1.000000,1.000000,0.750000)
 _Ammount ("Ammount", Range(0.000000,1.000000)) = 0.500000
 _Size ("Pulse Size", Float) = 10.000000
 _SizeX ("Grid Size X", Float) = 10.000000
 _SizeY ("Grid Size Y", Float) = 10.000000
}
SubShader { 
 LOD 200
 Tags { "LIGHTMODE"="Always" "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" "RenderType"="Opaque" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="Always" "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" "RenderType"="Opaque" }
  GpuProgramID 19435
CGPROGRAM
//#pragma target 4.0

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"


#define CODE_BLOCK_VERTEX
//uniform float4x4 UNITY_MATRIX_MVP;
uniform sampler2D _MainTex;
uniform float4 _Color;
uniform float _Ammount;
uniform float _Size;
uniform float _SizeX;
uniform float _SizeY;
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
    float4 tmpvar_1;
    tmpvar_1 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
    float tmpvar_2;
    tmpvar_2 = (floor((abs(((in_f.xlv_TEXCOORD0.x - 0.5) * 2)) * _SizeX)) / _SizeX);
    float tmpvar_3;
    tmpvar_3 = (floor((abs(((in_f.xlv_TEXCOORD0.y - 0.5) * 2)) * _SizeY)) / _SizeY);
    float4 tmpvar_4;
    float _tmp_dvx_56 = max(0, (floor(((((tmpvar_2 * tmpvar_2) + (tmpvar_3 * tmpvar_3)) + ((_Ammount - 0.6) * 2.7)) * _Size)) / _Size));
    tmpvar_4 = lerp(tmpvar_1, _Color, float4(_tmp_dvx_56, _tmp_dvx_56, _tmp_dvx_56, _tmp_dvx_56));
    out_f.color = tmpvar_4;
    return out_f;
}


ENDCG

}
}
Fallback "Unlit/Texture"
}