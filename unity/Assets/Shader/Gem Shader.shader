Shader "TurboChilli/Lit/Textured/Refractive and Reflective Gem Shader" {
Properties {
 _ColorD ("Diffuse", Color) = (1.000000,1.000000,1.000000,1.000000)
 _ColorS ("Specular", Color) = (1.000000,1.000000,1.000000,1.000000)
 _ColorR ("Reflected Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 _Gloss ("Gloss", Range(0.000000,1.000000)) = 0.000000
 _ReflectTex ("Reflection Texture", CUBE) = "dummy.jpg" { }
 _RefractTex ("Refraction Texture", CUBE) = "dummy.jpg" { }
 _Refraction ("Refraction", Range(0.000000,1.000000)) = 0.500000
 _Opacity ("Opacity", Range(0.000000,1.000000)) = 0.100000
}
SubShader { 
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "SHADOWSUPPORT"="true" }
  Cull Off
  Blend SrcAlpha OneMinusSrcAlpha
  GpuProgramID 21585
CGPROGRAM

#pragma multi_compile_fog
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
#define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
#define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
#define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)


#define CODE_BLOCK_VERTEX
uniform float4 _LightColor0;
uniform float3 _ColorD;
uniform float3 _ColorS;
uniform float _Gloss;
uniform samplerCUBE _ReflectTex;
uniform samplerCUBE _RefractTex;
uniform float _Refraction;
uniform float _Opacity;
uniform float3 _ColorR;
struct appdata_t
{
    float4 vertex :POSITION;
    float3 normal :NORMAL;
};

struct OUT_Data_Vert
{
    UNITY_FOG_COORDS(69)
    float3 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float3 xlv_TEXCOORD2 :TEXCOORD2;
    float3 xlv_TEXCOORD3 :TEXCOORD3;
    float3 xlv_TEXCOORD4 :TEXCOORD4;
    float4 vertex :SV_POSITION;
};

struct v2f
{
    UNITY_FOG_COORDS(69)
    float3 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float3 xlv_TEXCOORD2 :TEXCOORD2;
    float3 xlv_TEXCOORD4 :TEXCOORD4;
};

struct OUT_Data_Frag
{
    float4 color :SV_Target0;
};

OUT_Data_Vert vert(appdata_t in_v)
{
    OUT_Data_Vert out_v;
    float3 shlight_1;
    float3 worldNormal_2;
    float3 tmpvar_3;
    float3 tmpvar_4;
    float3 tmpvar_5;
    float4 tmpvar_6;
    tmpvar_6.w = 1;
    tmpvar_6.xyz = in_v.vertex.xyz;
    float3 tmpvar_7;
    tmpvar_7 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
    float4 v_8;
    v_8.x = conv_mxt4x4_0(unity_WorldToObject).x;
    v_8.y = conv_mxt4x4_1(unity_WorldToObject).x;
    v_8.z = conv_mxt4x4_2(unity_WorldToObject).x;
    v_8.w = conv_mxt4x4_3(unity_WorldToObject).x;
    float4 v_9;
    v_9.x = conv_mxt4x4_0(unity_WorldToObject).y;
    v_9.y = conv_mxt4x4_1(unity_WorldToObject).y;
    v_9.z = conv_mxt4x4_2(unity_WorldToObject).y;
    v_9.w = conv_mxt4x4_3(unity_WorldToObject).y;
    float4 v_10;
    v_10.x = conv_mxt4x4_0(unity_WorldToObject).z;
    v_10.y = conv_mxt4x4_1(unity_WorldToObject).z;
    v_10.z = conv_mxt4x4_2(unity_WorldToObject).z;
    v_10.w = conv_mxt4x4_3(unity_WorldToObject).z;
    float3 tmpvar_11;
    tmpvar_11 = normalize((((v_8.xyz * in_v.normal.x) + (v_9.xyz * in_v.normal.y)) + (v_10.xyz * in_v.normal.z)));
    worldNormal_2 = tmpvar_11;
    tmpvar_4 = worldNormal_2;
    float3 tmpvar_12;
    float3 I_13;
    I_13 = (tmpvar_7 - _WorldSpaceCameraPos);
    tmpvar_12 = (I_13 - (2 * (dot(worldNormal_2, I_13) * worldNormal_2)));
    tmpvar_3 = tmpvar_12;
    float4 tmpvar_14;
    tmpvar_14.w = 1;
    tmpvar_14.xyz = float3(worldNormal_2);
    float4 normal_15;
    normal_15 = tmpvar_14;
    float3 res_16;
    float3 x_17;
    x_17.x = dot(unity_SHAr, normal_15);
    x_17.y = dot(unity_SHAg, normal_15);
    x_17.z = dot(unity_SHAb, normal_15);
    float3 x1_18;
    float4 tmpvar_19;
    tmpvar_19 = (normal_15.xyzz * normal_15.yzzx);
    x1_18.x = dot(unity_SHBr, tmpvar_19);
    x1_18.y = dot(unity_SHBg, tmpvar_19);
    x1_18.z = dot(unity_SHBb, tmpvar_19);
    res_16 = (x_17 + (x1_18 + (unity_SHC.xyz * ((normal_15.x * normal_15.x) - (normal_15.y * normal_15.y)))));
    float _tmp_dvx_54 = max(((1.055 * pow(max(res_16, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
    res_16 = float3(_tmp_dvx_54, _tmp_dvx_54, _tmp_dvx_54);
    shlight_1 = res_16;
    tmpvar_5 = shlight_1;
    out_v.vertex = UnityObjectToClipPos(tmpvar_6);
    out_v.xlv_TEXCOORD0 = tmpvar_3;
    out_v.xlv_TEXCOORD1 = tmpvar_4;
    out_v.xlv_TEXCOORD2 = tmpvar_7;
    out_v.xlv_TEXCOORD3 = abs(in_v.normal);
    out_v.xlv_TEXCOORD4 = tmpvar_5;
    UNITY_TRANSFER_FOG(out_v,out_v.vertex);
    return out_v;
}

#define CODE_BLOCK_FRAGMENT
OUT_Data_Frag frag(v2f in_f)
{
    OUT_Data_Frag out_f;
    float4 c_1;
    float3 tmpvar_2;
    float3 worldViewDir_3;
    float3 lightDir_4;
    float3 tmpvar_5;
    float3 tmpvar_6;
    float3 tmpvar_7;
    tmpvar_7 = _WorldSpaceLightPos0.xyz;
    lightDir_4 = tmpvar_7;
    float3 tmpvar_8;
    tmpvar_8 = normalize((_WorldSpaceCameraPos - in_f.xlv_TEXCOORD2));
    worldViewDir_3 = tmpvar_8;
    tmpvar_5 = in_f.xlv_TEXCOORD0;
    tmpvar_6 = worldViewDir_3;
    tmpvar_2 = in_f.xlv_TEXCOORD1;
    float3 tmpvar_9;
    float tmpvar_10;
    float foreFacing_11;
    float faceDirection_12;
    float reflection_13;
    float refraction_14;
    float tmpvar_15;
    tmpvar_15 = texCUBE(_RefractTex, tmpvar_5).x;
    refraction_14 = tmpvar_15;
    float tmpvar_16;
    tmpvar_16 = texCUBE(_ReflectTex, tmpvar_5).x;
    reflection_13 = tmpvar_16;
    float tmpvar_17;
    tmpvar_17 = dot(normalize(tmpvar_6), tmpvar_2);
    faceDirection_12 = tmpvar_17;
    float tmpvar_18;
    tmpvar_18 = max(0, (-faceDirection_12));
    float tmpvar_19;
    tmpvar_19 = max(0, faceDirection_12);
    foreFacing_11 = tmpvar_19;
    float tmpvar_20;
    tmpvar_20 = ((tmpvar_18 * max(0, (1 - _Refraction))) + foreFacing_11);
    tmpvar_9 = (_ColorD + (_ColorR * ((reflection_13 + refraction_14) * tmpvar_20)));
    float tmpvar_21;
    tmpvar_21 = max(_Opacity, tmpvar_20);
    tmpvar_10 = tmpvar_21;
    c_1.w = 0;
    c_1.xyz = (tmpvar_9 * in_f.xlv_TEXCOORD4);
    float4 tmpvar_22;
    float3 lightDirection_23;
    lightDirection_23 = lightDir_4;
    float3 viewDirection_24;
    viewDirection_24 = worldViewDir_3;
    float tmpvar_25;
    tmpvar_25 = max(0, dot(tmpvar_2, lightDirection_23));
    float tmpvar_26;
    tmpvar_26 = max(0, dot(tmpvar_2, normalize((lightDirection_23 + viewDirection_24))));
    float4 tmpvar_27;
    tmpvar_27.xyz = ((((tmpvar_25 * tmpvar_9) + (pow(tmpvar_26, exp2(((_Gloss * 10) + 1))) * _ColorS)) * _LightColor0.xyz) * 2);
    tmpvar_27.w = tmpvar_10;
    tmpvar_22 = tmpvar_27;
    c_1.xyz = (c_1 + tmpvar_22).xyz;
    c_1.w = 1;
    out_f.color = c_1;
    UNITY_APPLY_FOG(in_f.fogCoord, out_f.color);
    return out_f;
}


ENDCG

}
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardAdd" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" }
  ZWrite Off
  Cull Off
  Blend One One
  GpuProgramID 90353
CGPROGRAM

#pragma multi_compile_fog
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
#define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
#define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
#define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)


#define CODE_BLOCK_VERTEX
uniform float4 _LightColor0;
uniform sampler2D _LightTexture0;
uniform float4x4 unity_WorldToLight;
uniform float3 _ColorD;
uniform float3 _ColorS;
uniform float _Gloss;
uniform samplerCUBE _ReflectTex;
uniform samplerCUBE _RefractTex;
uniform float _Refraction;
uniform float _Opacity;
uniform float3 _ColorR;
struct appdata_t
{
    float4 vertex :POSITION;
    float3 normal :NORMAL;
};

struct OUT_Data_Vert
{
    UNITY_FOG_COORDS(69)
    float3 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float3 xlv_TEXCOORD2 :TEXCOORD2;
    float3 xlv_TEXCOORD3 :TEXCOORD3;
    float4 vertex :SV_POSITION;
};

struct v2f
{
    UNITY_FOG_COORDS(69)
    float3 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float3 xlv_TEXCOORD2 :TEXCOORD2;
};

struct OUT_Data_Frag
{
    float4 color :SV_Target0;
};

OUT_Data_Vert vert(appdata_t in_v)
{
    OUT_Data_Vert out_v;
    float3 worldNormal_1;
    float3 tmpvar_2;
    float3 tmpvar_3;
    float4 tmpvar_4;
    tmpvar_4.w = 1;
    tmpvar_4.xyz = in_v.vertex.xyz;
    float3 tmpvar_5;
    tmpvar_5 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
    float4 v_6;
    v_6.x = conv_mxt4x4_0(unity_WorldToObject).x;
    v_6.y = conv_mxt4x4_1(unity_WorldToObject).x;
    v_6.z = conv_mxt4x4_2(unity_WorldToObject).x;
    v_6.w = conv_mxt4x4_3(unity_WorldToObject).x;
    float4 v_7;
    v_7.x = conv_mxt4x4_0(unity_WorldToObject).y;
    v_7.y = conv_mxt4x4_1(unity_WorldToObject).y;
    v_7.z = conv_mxt4x4_2(unity_WorldToObject).y;
    v_7.w = conv_mxt4x4_3(unity_WorldToObject).y;
    float4 v_8;
    v_8.x = conv_mxt4x4_0(unity_WorldToObject).z;
    v_8.y = conv_mxt4x4_1(unity_WorldToObject).z;
    v_8.z = conv_mxt4x4_2(unity_WorldToObject).z;
    v_8.w = conv_mxt4x4_3(unity_WorldToObject).z;
    float3 tmpvar_9;
    tmpvar_9 = normalize((((v_6.xyz * in_v.normal.x) + (v_7.xyz * in_v.normal.y)) + (v_8.xyz * in_v.normal.z)));
    worldNormal_1 = tmpvar_9;
    tmpvar_3 = worldNormal_1;
    float3 tmpvar_10;
    float3 I_11;
    I_11 = (tmpvar_5 - _WorldSpaceCameraPos);
    tmpvar_10 = (I_11 - (2 * (dot(worldNormal_1, I_11) * worldNormal_1)));
    tmpvar_2 = tmpvar_10;
    out_v.vertex = UnityObjectToClipPos(tmpvar_4);
    out_v.xlv_TEXCOORD0 = tmpvar_2;
    out_v.xlv_TEXCOORD1 = tmpvar_3;
    out_v.xlv_TEXCOORD2 = tmpvar_5;
    out_v.xlv_TEXCOORD3 = abs(in_v.normal);
    UNITY_TRANSFER_FOG(out_v,out_v.vertex);
    return out_v;
}

#define CODE_BLOCK_FRAGMENT
OUT_Data_Frag frag(v2f in_f)
{
    OUT_Data_Frag out_f;
    float4 c_1;
    float3 tmpvar_2;
    float3 worldViewDir_3;
    float3 lightDir_4;
    float3 tmpvar_5;
    float3 tmpvar_6;
    float3 tmpvar_7;
    tmpvar_7 = normalize((_WorldSpaceLightPos0.xyz - in_f.xlv_TEXCOORD2));
    lightDir_4 = tmpvar_7;
    float3 tmpvar_8;
    tmpvar_8 = normalize((_WorldSpaceCameraPos - in_f.xlv_TEXCOORD2));
    worldViewDir_3 = tmpvar_8;
    tmpvar_5 = in_f.xlv_TEXCOORD0;
    tmpvar_6 = worldViewDir_3;
    tmpvar_2 = in_f.xlv_TEXCOORD1;
    float3 tmpvar_9;
    float tmpvar_10;
    float foreFacing_11;
    float faceDirection_12;
    float reflection_13;
    float refraction_14;
    float tmpvar_15;
    tmpvar_15 = texCUBE(_RefractTex, tmpvar_5).x;
    refraction_14 = tmpvar_15;
    float tmpvar_16;
    tmpvar_16 = texCUBE(_ReflectTex, tmpvar_5).x;
    reflection_13 = tmpvar_16;
    float tmpvar_17;
    tmpvar_17 = dot(normalize(tmpvar_6), tmpvar_2);
    faceDirection_12 = tmpvar_17;
    float tmpvar_18;
    tmpvar_18 = max(0, (-faceDirection_12));
    float tmpvar_19;
    tmpvar_19 = max(0, faceDirection_12);
    foreFacing_11 = tmpvar_19;
    float tmpvar_20;
    tmpvar_20 = ((tmpvar_18 * max(0, (1 - _Refraction))) + foreFacing_11);
    tmpvar_9 = (_ColorD + (_ColorR * ((reflection_13 + refraction_14) * tmpvar_20)));
    float tmpvar_21;
    tmpvar_21 = max(_Opacity, tmpvar_20);
    tmpvar_10 = tmpvar_21;
    float4 tmpvar_22;
    tmpvar_22.w = 1;
    tmpvar_22.xyz = in_f.xlv_TEXCOORD2;
    float3 tmpvar_23;
    tmpvar_23 = mul(unity_WorldToLight, tmpvar_22).xyz;
    float tmpvar_24;
    tmpvar_24 = dot(tmpvar_23, tmpvar_23);
    float tmpvar_25;
    tmpvar_25 = tex2D(_LightTexture0, float2(tmpvar_24, tmpvar_24)).w;
    float4 tmpvar_26;
    float3 lightDirection_27;
    lightDirection_27 = lightDir_4;
    float3 viewDirection_28;
    viewDirection_28 = worldViewDir_3;
    float attenuation_29;
    attenuation_29 = tmpvar_25;
    float tmpvar_30;
    tmpvar_30 = max(0, dot(tmpvar_2, lightDirection_27));
    float tmpvar_31;
    tmpvar_31 = max(0, dot(tmpvar_2, normalize((lightDirection_27 + viewDirection_28))));
    float4 tmpvar_32;
    tmpvar_32.xyz = ((((tmpvar_30 * tmpvar_9) + (pow(tmpvar_31, exp2(((_Gloss * 10) + 1))) * _ColorS)) * _LightColor0.xyz) * (attenuation_29 * 2));
    tmpvar_32.w = tmpvar_10;
    tmpvar_26 = tmpvar_32;
    c_1.xyz = tmpvar_26.xyz;
    c_1.w = 1;
    out_f.color = c_1;
    UNITY_APPLY_FOG(in_f.fogCoord, out_f.color);
    return out_f;
}


ENDCG

}
 Pass {
  Name "META"
  Tags { "LIGHTMODE"="Meta" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" }
  Cull Off
  Blend SrcAlpha OneMinusSrcAlpha
  GpuProgramID 135698
CGPROGRAM

#pragma multi_compile_fog
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
#define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
#define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
#define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)


#define CODE_BLOCK_VERTEX
uniform float4 unity_MetaVertexControl;
uniform float3 _ColorD;
uniform samplerCUBE _ReflectTex;
uniform samplerCUBE _RefractTex;
uniform float _Refraction;
uniform float3 _ColorR;
uniform float4 unity_MetaFragmentControl;
uniform float unity_OneOverOutputBoost;
uniform float unity_MaxOutputValue;
uniform float unity_UseLinearSpace;
struct appdata_t
{
    float4 vertex :POSITION;
    float3 normal :NORMAL;
    float4 texcoord1 :TEXCOORD1;
    float4 texcoord2 :TEXCOORD2;
};

struct OUT_Data_Vert
{
    UNITY_FOG_COORDS(69)
    float3 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float3 xlv_TEXCOORD2 :TEXCOORD2;
    float3 xlv_TEXCOORD3 :TEXCOORD3;
    float4 vertex :SV_POSITION;
};

struct v2f
{
    UNITY_FOG_COORDS(69)
    float3 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float3 xlv_TEXCOORD2 :TEXCOORD2;
};

struct OUT_Data_Frag
{
    float4 color :SV_Target0;
};

OUT_Data_Vert vert(appdata_t in_v)
{
    OUT_Data_Vert out_v;
    float3 worldNormal_1;
    float3 tmpvar_2;
    float3 tmpvar_3;
    float3 tmpvar_4;
    tmpvar_4 = abs(in_v.normal);
    float4 vertex_5;
    vertex_5 = in_v.vertex;
    if(unity_MetaVertexControl.x)
    {
        vertex_5.xy = ((in_v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
        float tmpvar_6;
        if((in_v.vertex.z>0))
        {
            tmpvar_6 = 0.0001;
        }
        else
        {
            tmpvar_6 = 0;
        }
        vertex_5.z = tmpvar_6;
    }
    if(unity_MetaVertexControl.y)
    {
        vertex_5.xy = ((in_v.texcoord2.xy * unity_DynamicLightmapST.xy) + unity_DynamicLightmapST.zw);
        float tmpvar_7;
        if((vertex_5.z>0))
        {
            tmpvar_7 = 0.0001;
        }
        else
        {
            tmpvar_7 = 0;
        }
        vertex_5.z = tmpvar_7;
    }
    float4 tmpvar_8;
    tmpvar_8.w = 1;
    tmpvar_8.xyz = vertex_5.xyz;
    float3 tmpvar_9;
    tmpvar_9 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
    float4 v_10;
    v_10.x = conv_mxt4x4_0(unity_WorldToObject).x;
    v_10.y = conv_mxt4x4_1(unity_WorldToObject).x;
    v_10.z = conv_mxt4x4_2(unity_WorldToObject).x;
    v_10.w = conv_mxt4x4_3(unity_WorldToObject).x;
    float4 v_11;
    v_11.x = conv_mxt4x4_0(unity_WorldToObject).y;
    v_11.y = conv_mxt4x4_1(unity_WorldToObject).y;
    v_11.z = conv_mxt4x4_2(unity_WorldToObject).y;
    v_11.w = conv_mxt4x4_3(unity_WorldToObject).y;
    float4 v_12;
    v_12.x = conv_mxt4x4_0(unity_WorldToObject).z;
    v_12.y = conv_mxt4x4_1(unity_WorldToObject).z;
    v_12.z = conv_mxt4x4_2(unity_WorldToObject).z;
    v_12.w = conv_mxt4x4_3(unity_WorldToObject).z;
    float3 tmpvar_13;
    tmpvar_13 = normalize((((v_10.xyz * in_v.normal.x) + (v_11.xyz * in_v.normal.y)) + (v_12.xyz * in_v.normal.z)));
    worldNormal_1 = tmpvar_13;
    tmpvar_3 = worldNormal_1;
    float3 tmpvar_14;
    float3 I_15;
    I_15 = (tmpvar_9 - _WorldSpaceCameraPos);
    tmpvar_14 = (I_15 - (2 * (dot(worldNormal_1, I_15) * worldNormal_1)));
    tmpvar_2 = tmpvar_14;
    out_v.vertex = UnityObjectToClipPos(tmpvar_8);
    out_v.xlv_TEXCOORD0 = tmpvar_2;
    out_v.xlv_TEXCOORD1 = tmpvar_3;
    out_v.xlv_TEXCOORD2 = tmpvar_9;
    out_v.xlv_TEXCOORD3 = tmpvar_4;
    UNITY_TRANSFER_FOG(out_v,out_v.vertex);
    return out_v;
}

#define CODE_BLOCK_FRAGMENT
OUT_Data_Frag frag(v2f in_f)
{
    OUT_Data_Frag out_f;
    float4 tmpvar_1;
    float3 tmpvar_2;
    float3 tmpvar_3;
    float3 worldViewDir_4;
    float3 tmpvar_5;
    float3 tmpvar_6;
    float3 tmpvar_7;
    tmpvar_7 = normalize((_WorldSpaceCameraPos - in_f.xlv_TEXCOORD2));
    worldViewDir_4 = tmpvar_7;
    tmpvar_5 = in_f.xlv_TEXCOORD0;
    tmpvar_6 = worldViewDir_4;
    tmpvar_3 = in_f.xlv_TEXCOORD1;
    float3 tmpvar_8;
    float foreFacing_9;
    float faceDirection_10;
    float reflection_11;
    float refraction_12;
    float tmpvar_13;
    tmpvar_13 = texCUBE(_RefractTex, tmpvar_5).x;
    refraction_12 = tmpvar_13;
    float tmpvar_14;
    tmpvar_14 = texCUBE(_ReflectTex, tmpvar_5).x;
    reflection_11 = tmpvar_14;
    float tmpvar_15;
    tmpvar_15 = dot(normalize(tmpvar_6), tmpvar_3);
    faceDirection_10 = tmpvar_15;
    float tmpvar_16;
    tmpvar_16 = max(0, (-faceDirection_10));
    float tmpvar_17;
    tmpvar_17 = max(0, faceDirection_10);
    foreFacing_9 = tmpvar_17;
    float tmpvar_18;
    tmpvar_18 = ((tmpvar_16 * max(0, (1 - _Refraction))) + foreFacing_9);
    tmpvar_8 = (_ColorD + (_ColorR * ((reflection_11 + refraction_12) * tmpvar_18)));
    tmpvar_2 = tmpvar_8;
    float4 res_19;
    res_19 = float4(0, 0, 0, 0);
    if(unity_MetaFragmentControl.x)
    {
        float4 tmpvar_20;
        tmpvar_20.w = 1;
        tmpvar_20.xyz = float3(tmpvar_2);
        res_19.w = tmpvar_20.w;
        float3 tmpvar_21;
        float _tmp_dvx_34 = clamp(unity_OneOverOutputBoost, 0, 1);
        tmpvar_21 = clamp(pow(tmpvar_2, float3(_tmp_dvx_34, _tmp_dvx_34, _tmp_dvx_34)), float3(0, 0, 0), float3(unity_MaxOutputValue, unity_MaxOutputValue, unity_MaxOutputValue));
        res_19.xyz = float3(tmpvar_21);
    }
    if(unity_MetaFragmentControl.y)
    {
        float3 emission_22;
        if(int(unity_UseLinearSpace))
        {
            emission_22 = float3(0, 0, 0);
        }
        else
        {
            emission_22 = float3(0, 0, 0);
        }
        float4 tmpvar_23;
        float4 rgbm_24;
        float4 tmpvar_25;
        tmpvar_25.w = 1;
        tmpvar_25.xyz = float3((emission_22 * 0.01030928));
        rgbm_24.xyz = tmpvar_25.xyz;
        rgbm_24.w = max(max(tmpvar_25.x, tmpvar_25.y), max(tmpvar_25.z, 0.02));
        rgbm_24.w = (ceil((rgbm_24.w * 255)) / 255);
        rgbm_24.w = max(rgbm_24.w, 0.02);
        rgbm_24.xyz = (tmpvar_25.xyz / rgbm_24.w);
        tmpvar_23 = rgbm_24;
        res_19 = tmpvar_23;
    }
    tmpvar_1 = res_19;
    out_f.color = tmpvar_1;
    UNITY_APPLY_FOG(in_f.fogCoord, out_f.color);
    return out_f;
}


ENDCG

}
}
}