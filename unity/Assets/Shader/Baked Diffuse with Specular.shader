Shader "Porygon Axolotl/Rolling Sky/Enemy" {
Properties {
 _MainTex ("Baked Diffuse", 2D) = "white" { }
 _Specular ("Specular Level", Float) = 2.000000
 _Gloss ("Gloss", Float) = 0.500000
 _Color ("Specular Color", Color) = (1.000000,1.000000,1.000000,1.000000)
}
SubShader { 
 Pass {
  Tags { "LIGHTMODE"="ForwardBase" }
  GpuProgramID 24346
CGPROGRAM

#pragma multi_compile_fog //Add fog compilation support
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"


#define CODE_BLOCK_VERTEX
uniform float4 _LightColor0;
uniform sampler2D _MainTex;
uniform float _Specular;
uniform float _Gloss;
uniform float3 _Color;
struct appdata_t
{
    float4 vertex :POSITION;
    float3 normal :NORMAL;
    float4 texcoord :TEXCOORD0;
};

struct OUT_Data_Vert
{
    UNITY_FOG_COORDS(69) //Add texcoord for fog in OUT_Data_Vert vert(appdata_t)
    float2 xlv_TEXCOORD0 :TEXCOORD0;
    float4 xlv_TEXCOORD1 :TEXCOORD1;
    float3 xlv_TEXCOORD2 :TEXCOORD2;
    float4 vertex :SV_POSITION;
};

struct v2f
{
    UNITY_FOG_COORDS(69) //Add texcoord for fog in OUT_Data_Frag frag(v2f)
    float2 xlv_TEXCOORD0 :TEXCOORD0;
    float4 xlv_TEXCOORD1 :TEXCOORD1;
    float3 xlv_TEXCOORD2 :TEXCOORD2;
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
    out_v.xlv_TEXCOORD0 = in_v.texcoord.xy;
    out_v.vertex = UnityObjectToClipPos(in_v.vertex);
    out_v.xlv_TEXCOORD1 = mul(unity_ObjectToWorld, in_v.vertex);
    out_v.xlv_TEXCOORD2 = normalize(mul(tmpvar_1, unity_WorldToObject).xyz);
    UNITY_TRANSFER_FOG(out_v,out_v.vertex); //Add fog to vertex
    return out_v;
}

#define CODE_BLOCK_FRAGMENT
OUT_Data_Frag frag(v2f in_f)
{
    OUT_Data_Frag out_f;
    float4 tmpvar_1;
    float4 col_2;
    float3 specularReflection_3;
    float3 tmpvar_4;
    tmpvar_4 = normalize(in_f.xlv_TEXCOORD2);
    float3 tmpvar_5;
    tmpvar_5 = normalize((_WorldSpaceCameraPos - in_f.xlv_TEXCOORD1.xyz));
    float3 tmpvar_6;
    tmpvar_6 = normalize(_WorldSpaceLightPos0.xyz);
    float tmpvar_7;
    tmpvar_7 = dot(tmpvar_4, tmpvar_6);
    if((tmpvar_7<0))
    {
        specularReflection_3 = float3(0, 0, 0);
    }
    else
    {
        float3 I_8;
        I_8 = (-tmpvar_6);
        specularReflection_3 = (_LightColor0.xyz * pow(max(0, dot((I_8 - (2 * (dot(tmpvar_4, I_8) * tmpvar_4))), tmpvar_5)), _Gloss));
    }
    float4 tmpvar_9;
    tmpvar_9 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
    float4 tmpvar_10;
    tmpvar_10.w = 1;
    tmpvar_10.xyz = (tmpvar_9.xyz + ((specularReflection_3 * _Specular) * _Color));
    col_2 = tmpvar_10;
    tmpvar_1 = col_2;
    out_f.color = tmpvar_1;
    UNITY_APPLY_FOG(in_f.fogCoord, out_f.color); //Apply fog
    return out_f;
}


ENDCG

}
}
Fallback "Specular"
}