// Signed Sunny 3.2.5 GLES -> Cg expression translation, not original authoring source.
// Complete native program table SHA256: 0fdd6ee50367c05910538d885bbedd5a0e856d50716a3d9a45d6f377cc57dbdc
Shader "Classic325/Legacy Shaders/VertexLit" {
Properties {
_Color ("Main Color", Color) = (1.0,1.0,1.0,1.0)
_SpecColor ("Spec Color", Color) = (1.0,1.0,1.0,1.0)
_Emission ("Emissive Color", Color) = (0.0,0.0,0.0,0.0)
_Shininess ("Shininess", Range(0.009999999776482582, 1.0)) = 0.699999988079071
_MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader {
LOD 100
Tags { "RenderType"="Opaque" }
Pass {
Tags { "LightMode"="Vertex" "RenderType"="Opaque" }
Blend One Zero, One Zero
BlendOp Add, Add
ColorMask RGBA
ZWrite On
ZTest LEqual
Cull Back
Offset 0.0, 0.0
AlphaToMask Off
Stencil { Ref 0 ReadMask 255 WriteMask 255 Comp Always Pass Keep Fail Keep ZFail Keep }
Fog { Mode Off }
CGPROGRAM
#pragma target 3.0
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile __ POINT SPOT
#pragma multi_compile __ FOG_LINEAR
#if (!defined(FOG_LINEAR) && !defined(POINT) && !defined(SPOT))
#include "UnityCG.cginc"
half4 _Color;
half4 _Emission;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
half4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
fixed4 xlv_COLOR0 : TEXCOORD0;
fixed3 xlv_COLOR1 : TEXCOORD1;
float2 xlv_TEXCOORD0 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4x4 m_1;
m_1 = (mul(transpose(unity_MatrixInvV), transpose(unity_WorldToObject)));
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_2).x = (m_1[0]).x;
(tmpvar_2).y = (m_1[1]).x;
(tmpvar_2).z = (m_1[2]).x;
(tmpvar_2).w = (m_1[3]).x;
(tmpvar_3).x = (m_1[0]).y;
(tmpvar_3).y = (m_1[1]).y;
(tmpvar_3).z = (m_1[2]).y;
(tmpvar_3).w = (m_1[3]).y;
(tmpvar_4).x = (m_1[0]).z;
(tmpvar_4).y = (m_1[1]).z;
(tmpvar_4).z = (m_1[2]).z;
(tmpvar_4).w = (m_1[3]).z;
float3 tmpvar_5;
tmpvar_5 = (v._glesVertex).xyz;
half shininess_7;
half3 specColor_8;
half3 lcolor_9;
half3 viewDir_10;
half3 eyeNormal_11;
half4 color_12;
color_12 = float4(0.0, 0.0, 0.0, 1.1);
float4 tmpvar_13;
(tmpvar_13).w = 1.0;
(tmpvar_13).xyz = tmpvar_5;
float3x3 tmpvar_14;
tmpvar_14[0] = (tmpvar_2).xyz;
tmpvar_14[1] = (tmpvar_3).xyz;
tmpvar_14[2] = (tmpvar_4).xyz;
float3 tmpvar_15;
tmpvar_15 = normalize((mul(v._glesNormal, tmpvar_14)));
eyeNormal_11 = tmpvar_15;
float3 tmpvar_16;
tmpvar_16 = normalize(((mul(tmpvar_13, (mul(transpose(unity_ObjectToWorld), transpose(unity_MatrixV)))))).xyz);
viewDir_10 = (-(tmpvar_16));
lcolor_9 = (((_Emission).xyz + (((_Color).xyz * (glstate_lightmodel_ambient).xyz))));
specColor_8 = float3(0.0, 0.0, 0.0);
shininess_7 = ((_Shininess * 128.0));
for (int il_6 = 0; (il_6 < 8); il_6++) {
float3 tmpvar_17;
tmpvar_17 = (unity_LightPosition[il_6]).xyz;
half3 dirToLight_18;
dirToLight_18 = tmpvar_17;
half3 specColor_19;
specColor_19 = specColor_8;
half tmpvar_20;
tmpvar_20 = max(dot(eyeNormal_11, dirToLight_18), 0.0);
half3 tmpvar_21;
tmpvar_21 = ((((tmpvar_20 * (_Color).xyz)) * (unity_LightColor[il_6]).xyz));
if (((tmpvar_20 > 0.0))) {
specColor_19 = ((specColor_8 + ((((0.5 * clamp(pow(max(dot(eyeNormal_11, normalize(((dirToLight_18 + viewDir_10)))), 0.0), shininess_7), 0.0, 1.0))) * (unity_LightColor[il_6]).xyz))));
}

specColor_8 = specColor_19;
lcolor_9 = ((lcolor_9 + min(((tmpvar_21 * 0.5)), float3(1.0, 1.0, 1.0))));
}

(color_12).xyz = lcolor_9;
(color_12).w = (_Color).w;
specColor_8 = ((specColor_8 * (_SpecColor).xyz));
fixed4 tmpvar_22;
half4 tmpvar_23;
tmpvar_23 = clamp(color_12, 0.0, 1.0);
tmpvar_22 = tmpvar_23;
fixed3 tmpvar_24;
half3 tmpvar_25;
tmpvar_25 = clamp(specColor_8, 0.0, 1.0);
tmpvar_24 = tmpvar_25;
float4 tmpvar_26;
(tmpvar_26).w = 1.0;
(tmpvar_26).xyz = tmpvar_5;
o.xlv_COLOR0 = tmpvar_22;
o.xlv_COLOR1 = tmpvar_24;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.position = (mul((mul(tmpvar_26, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 col_1;
(col_1).xyz = (((tex2D(_MainTex, i.xlv_TEXCOORD0) * i.xlv_COLOR0))).xyz;
(col_1).xyz = (((col_1 * 2.0))).xyz;
(col_1).w = 1.0;
(col_1).xyz = (((col_1).xyz + i.xlv_COLOR1));
o.target0 = col_1;

return o;
}

#elif (!defined(FOG_LINEAR) && defined(POINT) && !defined(SPOT))
#include "UnityCG.cginc"
half4 _Color;
half4 _Emission;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
half4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
fixed4 xlv_COLOR0 : TEXCOORD0;
fixed3 xlv_COLOR1 : TEXCOORD1;
float2 xlv_TEXCOORD0 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4x4 m_1;
m_1 = (mul(transpose(unity_MatrixInvV), transpose(unity_WorldToObject)));
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_2).x = (m_1[0]).x;
(tmpvar_2).y = (m_1[1]).x;
(tmpvar_2).z = (m_1[2]).x;
(tmpvar_2).w = (m_1[3]).x;
(tmpvar_3).x = (m_1[0]).y;
(tmpvar_3).y = (m_1[1]).y;
(tmpvar_3).z = (m_1[2]).y;
(tmpvar_3).w = (m_1[3]).y;
(tmpvar_4).x = (m_1[0]).z;
(tmpvar_4).y = (m_1[1]).z;
(tmpvar_4).z = (m_1[2]).z;
(tmpvar_4).w = (m_1[3]).z;
float3 tmpvar_5;
tmpvar_5 = (v._glesVertex).xyz;
half shininess_7;
half3 specColor_8;
half3 lcolor_9;
half3 viewDir_10;
half3 eyeNormal_11;
float3 eyePos_12;
half4 color_13;
color_13 = float4(0.0, 0.0, 0.0, 1.1);
float4 tmpvar_14;
(tmpvar_14).w = 1.0;
(tmpvar_14).xyz = tmpvar_5;
float3 tmpvar_15;
tmpvar_15 = ((mul(tmpvar_14, (mul(transpose(unity_ObjectToWorld), transpose(unity_MatrixV)))))).xyz;
eyePos_12 = tmpvar_15;
float3x3 tmpvar_16;
tmpvar_16[0] = (tmpvar_2).xyz;
tmpvar_16[1] = (tmpvar_3).xyz;
tmpvar_16[2] = (tmpvar_4).xyz;
float3 tmpvar_17;
tmpvar_17 = normalize((mul(v._glesNormal, tmpvar_16)));
eyeNormal_11 = tmpvar_17;
float3 tmpvar_18;
tmpvar_18 = normalize(tmpvar_15);
viewDir_10 = (-(tmpvar_18));
lcolor_9 = (((_Emission).xyz + (((_Color).xyz * (glstate_lightmodel_ambient).xyz))));
specColor_8 = float3(0.0, 0.0, 0.0);
shininess_7 = ((_Shininess * 128.0));
for (int il_6 = 0; (il_6 < 8); il_6++) {
half att_19;
float3 dirToLight_20;
dirToLight_20 = (((unity_LightPosition[il_6]).xyz - ((eyePos_12 * (unity_LightPosition[il_6]).w))));
float tmpvar_21;
tmpvar_21 = dot(dirToLight_20, dirToLight_20);
att_19 = ((1.0 / (((1.0 + (((unity_LightAtten[il_6]).z * tmpvar_21)))))));
if ((((((unity_LightPosition[il_6]).w != 0.0)) && ((tmpvar_21 > (unity_LightAtten[il_6]).w))))) {
att_19 = 0.0;
}

dirToLight_20 = ((dirToLight_20 * rsqrt(max(tmpvar_21, 1e-06))));
att_19 = ((att_19 * 0.5));
half3 dirToLight_22;
dirToLight_22 = dirToLight_20;
half3 specColor_23;
specColor_23 = specColor_8;
half tmpvar_24;
tmpvar_24 = max(dot(eyeNormal_11, dirToLight_22), 0.0);
half3 tmpvar_25;
tmpvar_25 = ((((tmpvar_24 * (_Color).xyz)) * (unity_LightColor[il_6]).xyz));
if (((tmpvar_24 > 0.0))) {
specColor_23 = ((specColor_8 + ((((att_19 * clamp(pow(max(dot(eyeNormal_11, normalize(((dirToLight_22 + viewDir_10)))), 0.0), shininess_7), 0.0, 1.0))) * (unity_LightColor[il_6]).xyz))));
}

specColor_8 = specColor_23;
lcolor_9 = ((lcolor_9 + min(((tmpvar_25 * att_19)), float3(1.0, 1.0, 1.0))));
}

(color_13).xyz = lcolor_9;
(color_13).w = (_Color).w;
specColor_8 = ((specColor_8 * (_SpecColor).xyz));
fixed4 tmpvar_26;
half4 tmpvar_27;
tmpvar_27 = clamp(color_13, 0.0, 1.0);
tmpvar_26 = tmpvar_27;
fixed3 tmpvar_28;
half3 tmpvar_29;
tmpvar_29 = clamp(specColor_8, 0.0, 1.0);
tmpvar_28 = tmpvar_29;
float4 tmpvar_30;
(tmpvar_30).w = 1.0;
(tmpvar_30).xyz = tmpvar_5;
o.xlv_COLOR0 = tmpvar_26;
o.xlv_COLOR1 = tmpvar_28;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.position = (mul((mul(tmpvar_30, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 col_1;
(col_1).xyz = (((tex2D(_MainTex, i.xlv_TEXCOORD0) * i.xlv_COLOR0))).xyz;
(col_1).xyz = (((col_1 * 2.0))).xyz;
(col_1).w = 1.0;
(col_1).xyz = (((col_1).xyz + i.xlv_COLOR1));
o.target0 = col_1;

return o;
}

#elif (!defined(FOG_LINEAR) && !defined(POINT) && defined(SPOT))
#include "UnityCG.cginc"
half4 _Color;
half4 _Emission;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
half4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
fixed4 xlv_COLOR0 : TEXCOORD0;
fixed3 xlv_COLOR1 : TEXCOORD1;
float2 xlv_TEXCOORD0 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4x4 m_1;
m_1 = (mul(transpose(unity_MatrixInvV), transpose(unity_WorldToObject)));
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_2).x = (m_1[0]).x;
(tmpvar_2).y = (m_1[1]).x;
(tmpvar_2).z = (m_1[2]).x;
(tmpvar_2).w = (m_1[3]).x;
(tmpvar_3).x = (m_1[0]).y;
(tmpvar_3).y = (m_1[1]).y;
(tmpvar_3).z = (m_1[2]).y;
(tmpvar_3).w = (m_1[3]).y;
(tmpvar_4).x = (m_1[0]).z;
(tmpvar_4).y = (m_1[1]).z;
(tmpvar_4).z = (m_1[2]).z;
(tmpvar_4).w = (m_1[3]).z;
float3 tmpvar_5;
tmpvar_5 = (v._glesVertex).xyz;
half shininess_7;
half3 specColor_8;
half3 lcolor_9;
half3 viewDir_10;
half3 eyeNormal_11;
float3 eyePos_12;
half4 color_13;
color_13 = float4(0.0, 0.0, 0.0, 1.1);
float4 tmpvar_14;
(tmpvar_14).w = 1.0;
(tmpvar_14).xyz = tmpvar_5;
float3 tmpvar_15;
tmpvar_15 = ((mul(tmpvar_14, (mul(transpose(unity_ObjectToWorld), transpose(unity_MatrixV)))))).xyz;
eyePos_12 = tmpvar_15;
float3x3 tmpvar_16;
tmpvar_16[0] = (tmpvar_2).xyz;
tmpvar_16[1] = (tmpvar_3).xyz;
tmpvar_16[2] = (tmpvar_4).xyz;
float3 tmpvar_17;
tmpvar_17 = normalize((mul(v._glesNormal, tmpvar_16)));
eyeNormal_11 = tmpvar_17;
float3 tmpvar_18;
tmpvar_18 = normalize(tmpvar_15);
viewDir_10 = (-(tmpvar_18));
lcolor_9 = (((_Emission).xyz + (((_Color).xyz * (glstate_lightmodel_ambient).xyz))));
specColor_8 = float3(0.0, 0.0, 0.0);
shininess_7 = ((_Shininess * 128.0));
for (int il_6 = 0; (il_6 < 8); il_6++) {
half rho_19;
half att_20;
float3 dirToLight_21;
dirToLight_21 = (((unity_LightPosition[il_6]).xyz - ((eyePos_12 * (unity_LightPosition[il_6]).w))));
float tmpvar_22;
tmpvar_22 = dot(dirToLight_21, dirToLight_21);
att_20 = ((1.0 / (((1.0 + (((unity_LightAtten[il_6]).z * tmpvar_22)))))));
if ((((((unity_LightPosition[il_6]).w != 0.0)) && ((tmpvar_22 > (unity_LightAtten[il_6]).w))))) {
att_20 = 0.0;
}

dirToLight_21 = ((dirToLight_21 * rsqrt(max(tmpvar_22, 1e-06))));
float tmpvar_23;
tmpvar_23 = max(dot(dirToLight_21, (unity_SpotDirection[il_6]).xyz), 0.0);
rho_19 = tmpvar_23;
att_20 = ((att_20 * clamp(((((rho_19 - (unity_LightAtten[il_6]).x)) * (unity_LightAtten[il_6]).y)), 0.0, 1.0)));
att_20 = ((att_20 * 0.5));
half3 dirToLight_24;
dirToLight_24 = dirToLight_21;
half3 specColor_25;
specColor_25 = specColor_8;
half tmpvar_26;
tmpvar_26 = max(dot(eyeNormal_11, dirToLight_24), 0.0);
half3 tmpvar_27;
tmpvar_27 = ((((tmpvar_26 * (_Color).xyz)) * (unity_LightColor[il_6]).xyz));
if (((tmpvar_26 > 0.0))) {
specColor_25 = ((specColor_8 + ((((att_20 * clamp(pow(max(dot(eyeNormal_11, normalize(((dirToLight_24 + viewDir_10)))), 0.0), shininess_7), 0.0, 1.0))) * (unity_LightColor[il_6]).xyz))));
}

specColor_8 = specColor_25;
lcolor_9 = ((lcolor_9 + min(((tmpvar_27 * att_20)), float3(1.0, 1.0, 1.0))));
}

(color_13).xyz = lcolor_9;
(color_13).w = (_Color).w;
specColor_8 = ((specColor_8 * (_SpecColor).xyz));
fixed4 tmpvar_28;
half4 tmpvar_29;
tmpvar_29 = clamp(color_13, 0.0, 1.0);
tmpvar_28 = tmpvar_29;
fixed3 tmpvar_30;
half3 tmpvar_31;
tmpvar_31 = clamp(specColor_8, 0.0, 1.0);
tmpvar_30 = tmpvar_31;
float4 tmpvar_32;
(tmpvar_32).w = 1.0;
(tmpvar_32).xyz = tmpvar_5;
o.xlv_COLOR0 = tmpvar_28;
o.xlv_COLOR1 = tmpvar_30;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.position = (mul((mul(tmpvar_32, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 col_1;
(col_1).xyz = (((tex2D(_MainTex, i.xlv_TEXCOORD0) * i.xlv_COLOR0))).xyz;
(col_1).xyz = (((col_1 * 2.0))).xyz;
(col_1).w = 1.0;
(col_1).xyz = (((col_1).xyz + i.xlv_COLOR1));
o.target0 = col_1;

return o;
}

#elif (defined(FOG_LINEAR) && !defined(POINT) && !defined(SPOT))
#include "UnityCG.cginc"
half4 _Color;
half4 _Emission;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
half4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
fixed4 xlv_COLOR0 : TEXCOORD0;
fixed3 xlv_COLOR1 : TEXCOORD1;
float2 xlv_TEXCOORD0 : TEXCOORD2;
fixed xlv_TEXCOORD1 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4x4 m_1;
m_1 = (mul(transpose(unity_MatrixInvV), transpose(unity_WorldToObject)));
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_2).x = (m_1[0]).x;
(tmpvar_2).y = (m_1[1]).x;
(tmpvar_2).z = (m_1[2]).x;
(tmpvar_2).w = (m_1[3]).x;
(tmpvar_3).x = (m_1[0]).y;
(tmpvar_3).y = (m_1[1]).y;
(tmpvar_3).z = (m_1[2]).y;
(tmpvar_3).w = (m_1[3]).y;
(tmpvar_4).x = (m_1[0]).z;
(tmpvar_4).y = (m_1[1]).z;
(tmpvar_4).z = (m_1[2]).z;
(tmpvar_4).w = (m_1[3]).z;
float3 tmpvar_5;
tmpvar_5 = (v._glesVertex).xyz;
half shininess_7;
half3 specColor_8;
half3 lcolor_9;
half3 viewDir_10;
half3 eyeNormal_11;
half4 color_12;
color_12 = float4(0.0, 0.0, 0.0, 1.1);
float4 tmpvar_13;
(tmpvar_13).w = 1.0;
(tmpvar_13).xyz = tmpvar_5;
float3 tmpvar_14;
tmpvar_14 = ((mul(tmpvar_13, (mul(transpose(unity_ObjectToWorld), transpose(unity_MatrixV)))))).xyz;
float3x3 tmpvar_15;
tmpvar_15[0] = (tmpvar_2).xyz;
tmpvar_15[1] = (tmpvar_3).xyz;
tmpvar_15[2] = (tmpvar_4).xyz;
float3 tmpvar_16;
tmpvar_16 = normalize((mul(v._glesNormal, tmpvar_15)));
eyeNormal_11 = tmpvar_16;
float3 tmpvar_17;
tmpvar_17 = normalize(tmpvar_14);
viewDir_10 = (-(tmpvar_17));
lcolor_9 = (((_Emission).xyz + (((_Color).xyz * (glstate_lightmodel_ambient).xyz))));
specColor_8 = float3(0.0, 0.0, 0.0);
shininess_7 = ((_Shininess * 128.0));
for (int il_6 = 0; (il_6 < 8); il_6++) {
float3 tmpvar_18;
tmpvar_18 = (unity_LightPosition[il_6]).xyz;
half3 dirToLight_19;
dirToLight_19 = tmpvar_18;
half3 specColor_20;
specColor_20 = specColor_8;
half tmpvar_21;
tmpvar_21 = max(dot(eyeNormal_11, dirToLight_19), 0.0);
half3 tmpvar_22;
tmpvar_22 = ((((tmpvar_21 * (_Color).xyz)) * (unity_LightColor[il_6]).xyz));
if (((tmpvar_21 > 0.0))) {
specColor_20 = ((specColor_8 + ((((0.5 * clamp(pow(max(dot(eyeNormal_11, normalize(((dirToLight_19 + viewDir_10)))), 0.0), shininess_7), 0.0, 1.0))) * (unity_LightColor[il_6]).xyz))));
}

specColor_8 = specColor_20;
lcolor_9 = ((lcolor_9 + min(((tmpvar_22 * 0.5)), float3(1.0, 1.0, 1.0))));
}

(color_12).xyz = lcolor_9;
(color_12).w = (_Color).w;
specColor_8 = ((specColor_8 * (_SpecColor).xyz));
fixed4 tmpvar_23;
half4 tmpvar_24;
tmpvar_24 = clamp(color_12, 0.0, 1.0);
tmpvar_23 = tmpvar_24;
fixed3 tmpvar_25;
half3 tmpvar_26;
tmpvar_26 = clamp(specColor_8, 0.0, 1.0);
tmpvar_25 = tmpvar_26;
fixed tmpvar_27;
float tmpvar_28;
tmpvar_28 = clamp(((((sqrt(dot(tmpvar_14, tmpvar_14)) * (unity_FogParams).z)) + (unity_FogParams).w)), 0.0, 1.0);
tmpvar_27 = tmpvar_28;
float4 tmpvar_29;
(tmpvar_29).w = 1.0;
(tmpvar_29).xyz = tmpvar_5;
o.xlv_COLOR0 = tmpvar_23;
o.xlv_COLOR1 = tmpvar_25;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_27;
o.position = (mul((mul(tmpvar_29, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 col_1;
(col_1).xyz = (((tex2D(_MainTex, i.xlv_TEXCOORD0) * i.xlv_COLOR0))).xyz;
(col_1).xyz = (((col_1 * 2.0))).xyz;
(col_1).w = 1.0;
(col_1).xyz = (((col_1).xyz + i.xlv_COLOR1));
(col_1).xyz = lerp((unity_FogColor).xyz, (col_1).xyz, ((float3)(i.xlv_TEXCOORD1)));
o.target0 = col_1;

return o;
}

#elif (defined(FOG_LINEAR) && defined(POINT) && !defined(SPOT))
#include "UnityCG.cginc"
half4 _Color;
half4 _Emission;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
half4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
fixed4 xlv_COLOR0 : TEXCOORD0;
fixed3 xlv_COLOR1 : TEXCOORD1;
float2 xlv_TEXCOORD0 : TEXCOORD2;
fixed xlv_TEXCOORD1 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4x4 m_1;
m_1 = (mul(transpose(unity_MatrixInvV), transpose(unity_WorldToObject)));
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_2).x = (m_1[0]).x;
(tmpvar_2).y = (m_1[1]).x;
(tmpvar_2).z = (m_1[2]).x;
(tmpvar_2).w = (m_1[3]).x;
(tmpvar_3).x = (m_1[0]).y;
(tmpvar_3).y = (m_1[1]).y;
(tmpvar_3).z = (m_1[2]).y;
(tmpvar_3).w = (m_1[3]).y;
(tmpvar_4).x = (m_1[0]).z;
(tmpvar_4).y = (m_1[1]).z;
(tmpvar_4).z = (m_1[2]).z;
(tmpvar_4).w = (m_1[3]).z;
float3 tmpvar_5;
tmpvar_5 = (v._glesVertex).xyz;
half shininess_7;
half3 specColor_8;
half3 lcolor_9;
half3 viewDir_10;
half3 eyeNormal_11;
float3 eyePos_12;
half4 color_13;
color_13 = float4(0.0, 0.0, 0.0, 1.1);
float4 tmpvar_14;
(tmpvar_14).w = 1.0;
(tmpvar_14).xyz = tmpvar_5;
float3 tmpvar_15;
tmpvar_15 = ((mul(tmpvar_14, (mul(transpose(unity_ObjectToWorld), transpose(unity_MatrixV)))))).xyz;
eyePos_12 = tmpvar_15;
float3x3 tmpvar_16;
tmpvar_16[0] = (tmpvar_2).xyz;
tmpvar_16[1] = (tmpvar_3).xyz;
tmpvar_16[2] = (tmpvar_4).xyz;
float3 tmpvar_17;
tmpvar_17 = normalize((mul(v._glesNormal, tmpvar_16)));
eyeNormal_11 = tmpvar_17;
float3 tmpvar_18;
tmpvar_18 = normalize(tmpvar_15);
viewDir_10 = (-(tmpvar_18));
lcolor_9 = (((_Emission).xyz + (((_Color).xyz * (glstate_lightmodel_ambient).xyz))));
specColor_8 = float3(0.0, 0.0, 0.0);
shininess_7 = ((_Shininess * 128.0));
for (int il_6 = 0; (il_6 < 8); il_6++) {
half att_19;
float3 dirToLight_20;
dirToLight_20 = (((unity_LightPosition[il_6]).xyz - ((eyePos_12 * (unity_LightPosition[il_6]).w))));
float tmpvar_21;
tmpvar_21 = dot(dirToLight_20, dirToLight_20);
att_19 = ((1.0 / (((1.0 + (((unity_LightAtten[il_6]).z * tmpvar_21)))))));
if ((((((unity_LightPosition[il_6]).w != 0.0)) && ((tmpvar_21 > (unity_LightAtten[il_6]).w))))) {
att_19 = 0.0;
}

dirToLight_20 = ((dirToLight_20 * rsqrt(max(tmpvar_21, 1e-06))));
att_19 = ((att_19 * 0.5));
half3 dirToLight_22;
dirToLight_22 = dirToLight_20;
half3 specColor_23;
specColor_23 = specColor_8;
half tmpvar_24;
tmpvar_24 = max(dot(eyeNormal_11, dirToLight_22), 0.0);
half3 tmpvar_25;
tmpvar_25 = ((((tmpvar_24 * (_Color).xyz)) * (unity_LightColor[il_6]).xyz));
if (((tmpvar_24 > 0.0))) {
specColor_23 = ((specColor_8 + ((((att_19 * clamp(pow(max(dot(eyeNormal_11, normalize(((dirToLight_22 + viewDir_10)))), 0.0), shininess_7), 0.0, 1.0))) * (unity_LightColor[il_6]).xyz))));
}

specColor_8 = specColor_23;
lcolor_9 = ((lcolor_9 + min(((tmpvar_25 * att_19)), float3(1.0, 1.0, 1.0))));
}

(color_13).xyz = lcolor_9;
(color_13).w = (_Color).w;
specColor_8 = ((specColor_8 * (_SpecColor).xyz));
fixed4 tmpvar_26;
half4 tmpvar_27;
tmpvar_27 = clamp(color_13, 0.0, 1.0);
tmpvar_26 = tmpvar_27;
fixed3 tmpvar_28;
half3 tmpvar_29;
tmpvar_29 = clamp(specColor_8, 0.0, 1.0);
tmpvar_28 = tmpvar_29;
fixed tmpvar_30;
float tmpvar_31;
tmpvar_31 = clamp(((((sqrt(dot(tmpvar_15, tmpvar_15)) * (unity_FogParams).z)) + (unity_FogParams).w)), 0.0, 1.0);
tmpvar_30 = tmpvar_31;
float4 tmpvar_32;
(tmpvar_32).w = 1.0;
(tmpvar_32).xyz = tmpvar_5;
o.xlv_COLOR0 = tmpvar_26;
o.xlv_COLOR1 = tmpvar_28;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_30;
o.position = (mul((mul(tmpvar_32, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 col_1;
(col_1).xyz = (((tex2D(_MainTex, i.xlv_TEXCOORD0) * i.xlv_COLOR0))).xyz;
(col_1).xyz = (((col_1 * 2.0))).xyz;
(col_1).w = 1.0;
(col_1).xyz = (((col_1).xyz + i.xlv_COLOR1));
(col_1).xyz = lerp((unity_FogColor).xyz, (col_1).xyz, ((float3)(i.xlv_TEXCOORD1)));
o.target0 = col_1;

return o;
}

#elif (defined(FOG_LINEAR) && !defined(POINT) && defined(SPOT))
#include "UnityCG.cginc"
half4 _Color;
half4 _Emission;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
half4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
fixed4 xlv_COLOR0 : TEXCOORD0;
fixed3 xlv_COLOR1 : TEXCOORD1;
float2 xlv_TEXCOORD0 : TEXCOORD2;
fixed xlv_TEXCOORD1 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4x4 m_1;
m_1 = (mul(transpose(unity_MatrixInvV), transpose(unity_WorldToObject)));
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_2).x = (m_1[0]).x;
(tmpvar_2).y = (m_1[1]).x;
(tmpvar_2).z = (m_1[2]).x;
(tmpvar_2).w = (m_1[3]).x;
(tmpvar_3).x = (m_1[0]).y;
(tmpvar_3).y = (m_1[1]).y;
(tmpvar_3).z = (m_1[2]).y;
(tmpvar_3).w = (m_1[3]).y;
(tmpvar_4).x = (m_1[0]).z;
(tmpvar_4).y = (m_1[1]).z;
(tmpvar_4).z = (m_1[2]).z;
(tmpvar_4).w = (m_1[3]).z;
float3 tmpvar_5;
tmpvar_5 = (v._glesVertex).xyz;
half shininess_7;
half3 specColor_8;
half3 lcolor_9;
half3 viewDir_10;
half3 eyeNormal_11;
float3 eyePos_12;
half4 color_13;
color_13 = float4(0.0, 0.0, 0.0, 1.1);
float4 tmpvar_14;
(tmpvar_14).w = 1.0;
(tmpvar_14).xyz = tmpvar_5;
float3 tmpvar_15;
tmpvar_15 = ((mul(tmpvar_14, (mul(transpose(unity_ObjectToWorld), transpose(unity_MatrixV)))))).xyz;
eyePos_12 = tmpvar_15;
float3x3 tmpvar_16;
tmpvar_16[0] = (tmpvar_2).xyz;
tmpvar_16[1] = (tmpvar_3).xyz;
tmpvar_16[2] = (tmpvar_4).xyz;
float3 tmpvar_17;
tmpvar_17 = normalize((mul(v._glesNormal, tmpvar_16)));
eyeNormal_11 = tmpvar_17;
float3 tmpvar_18;
tmpvar_18 = normalize(tmpvar_15);
viewDir_10 = (-(tmpvar_18));
lcolor_9 = (((_Emission).xyz + (((_Color).xyz * (glstate_lightmodel_ambient).xyz))));
specColor_8 = float3(0.0, 0.0, 0.0);
shininess_7 = ((_Shininess * 128.0));
for (int il_6 = 0; (il_6 < 8); il_6++) {
half rho_19;
half att_20;
float3 dirToLight_21;
dirToLight_21 = (((unity_LightPosition[il_6]).xyz - ((eyePos_12 * (unity_LightPosition[il_6]).w))));
float tmpvar_22;
tmpvar_22 = dot(dirToLight_21, dirToLight_21);
att_20 = ((1.0 / (((1.0 + (((unity_LightAtten[il_6]).z * tmpvar_22)))))));
if ((((((unity_LightPosition[il_6]).w != 0.0)) && ((tmpvar_22 > (unity_LightAtten[il_6]).w))))) {
att_20 = 0.0;
}

dirToLight_21 = ((dirToLight_21 * rsqrt(max(tmpvar_22, 1e-06))));
float tmpvar_23;
tmpvar_23 = max(dot(dirToLight_21, (unity_SpotDirection[il_6]).xyz), 0.0);
rho_19 = tmpvar_23;
att_20 = ((att_20 * clamp(((((rho_19 - (unity_LightAtten[il_6]).x)) * (unity_LightAtten[il_6]).y)), 0.0, 1.0)));
att_20 = ((att_20 * 0.5));
half3 dirToLight_24;
dirToLight_24 = dirToLight_21;
half3 specColor_25;
specColor_25 = specColor_8;
half tmpvar_26;
tmpvar_26 = max(dot(eyeNormal_11, dirToLight_24), 0.0);
half3 tmpvar_27;
tmpvar_27 = ((((tmpvar_26 * (_Color).xyz)) * (unity_LightColor[il_6]).xyz));
if (((tmpvar_26 > 0.0))) {
specColor_25 = ((specColor_8 + ((((att_20 * clamp(pow(max(dot(eyeNormal_11, normalize(((dirToLight_24 + viewDir_10)))), 0.0), shininess_7), 0.0, 1.0))) * (unity_LightColor[il_6]).xyz))));
}

specColor_8 = specColor_25;
lcolor_9 = ((lcolor_9 + min(((tmpvar_27 * att_20)), float3(1.0, 1.0, 1.0))));
}

(color_13).xyz = lcolor_9;
(color_13).w = (_Color).w;
specColor_8 = ((specColor_8 * (_SpecColor).xyz));
fixed4 tmpvar_28;
half4 tmpvar_29;
tmpvar_29 = clamp(color_13, 0.0, 1.0);
tmpvar_28 = tmpvar_29;
fixed3 tmpvar_30;
half3 tmpvar_31;
tmpvar_31 = clamp(specColor_8, 0.0, 1.0);
tmpvar_30 = tmpvar_31;
fixed tmpvar_32;
float tmpvar_33;
tmpvar_33 = clamp(((((sqrt(dot(tmpvar_15, tmpvar_15)) * (unity_FogParams).z)) + (unity_FogParams).w)), 0.0, 1.0);
tmpvar_32 = tmpvar_33;
float4 tmpvar_34;
(tmpvar_34).w = 1.0;
(tmpvar_34).xyz = tmpvar_5;
o.xlv_COLOR0 = tmpvar_28;
o.xlv_COLOR1 = tmpvar_30;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_32;
o.position = (mul((mul(tmpvar_34, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 col_1;
(col_1).xyz = (((tex2D(_MainTex, i.xlv_TEXCOORD0) * i.xlv_COLOR0))).xyz;
(col_1).xyz = (((col_1 * 2.0))).xyz;
(col_1).w = 1.0;
(col_1).xyz = (((col_1).xyz + i.xlv_COLOR1));
(col_1).xyz = lerp((unity_FogColor).xyz, (col_1).xyz, ((float3)(i.xlv_TEXCOORD1)));
o.target0 = col_1;

return o;
}

#else
#error Sunny shader keyword combination has no signed donor program
#endif
ENDCG
}
Pass {
Tags { "LightMode"="VertexLM" "RenderType"="Opaque" }
Blend One Zero, One Zero
BlendOp Add, Add
ColorMask RGBA
ZWrite On
ZTest LEqual
Cull Back
Offset 0.0, 0.0
AlphaToMask Off
Stencil { Ref 0 ReadMask 255 WriteMask 255 Comp Always Pass Keep Fail Keep ZFail Keep }
CGPROGRAM
#pragma target 3.0
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile __ FOG_LINEAR
#if (!defined(FOG_LINEAR))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
float4 unity_Lightmap_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float2 xlv_TEXCOORD1 : TEXCOORD1;
float2 xlv_TEXCOORD2 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
(tmpvar_1).w = 1.0;
(tmpvar_1).xyz = (v._glesVertex).xyz;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD1 = (((((v._glesMultiTexCoord1).xy * (unity_Lightmap_ST).xy)) + (unity_Lightmap_ST).zw));
o.xlv_TEXCOORD2 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.position = (mul((mul(tmpvar_1, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tex_1;
fixed4 col_2;
half4 tmpvar_3;
tmpvar_3 = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.xlv_TEXCOORD0);
tex_1 = tmpvar_3;
half4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (((unity_Lightmap_HDR).x * (tex_1).xyz));
col_2 = ((tmpvar_4 * _Color));
fixed4 tmpvar_5;
tmpvar_5 = tex2D(_MainTex, i.xlv_TEXCOORD2);
tex_1 = tmpvar_5;
(col_2).xyz = (((tmpvar_5).xyz * (col_2).xyz));
(col_2).w = 1.0;
o.target0 = col_2;

return o;
}

#elif (defined(FOG_LINEAR))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
float4 unity_Lightmap_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float2 xlv_TEXCOORD1 : TEXCOORD1;
float2 xlv_TEXCOORD2 : TEXCOORD2;
fixed xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float3 tmpvar_1;
tmpvar_1 = (v._glesVertex).xyz;
float3 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = tmpvar_1;
tmpvar_2 = ((mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).xyz;
fixed tmpvar_4;
float tmpvar_5;
tmpvar_5 = clamp(((((sqrt(dot(tmpvar_2, tmpvar_2)) * (unity_FogParams).z)) + (unity_FogParams).w)), 0.0, 1.0);
tmpvar_4 = tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = tmpvar_1;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD1 = (((((v._glesMultiTexCoord1).xy * (unity_Lightmap_ST).xy)) + (unity_Lightmap_ST).zw));
o.xlv_TEXCOORD2 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD3 = tmpvar_4;
o.position = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tex_1;
fixed4 col_2;
half4 tmpvar_3;
tmpvar_3 = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.xlv_TEXCOORD0);
tex_1 = tmpvar_3;
half4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (((unity_Lightmap_HDR).x * (tex_1).xyz));
col_2 = ((tmpvar_4 * _Color));
fixed4 tmpvar_5;
tmpvar_5 = tex2D(_MainTex, i.xlv_TEXCOORD2);
tex_1 = tmpvar_5;
(col_2).xyz = (((tmpvar_5).xyz * (col_2).xyz));
(col_2).w = 1.0;
(col_2).xyz = lerp((unity_FogColor).xyz, (col_2).xyz, ((float3)(i.xlv_TEXCOORD3)));
o.target0 = col_2;

return o;
}

#else
#error Sunny shader keyword combination has no signed donor program
#endif
ENDCG
}
Pass {
Name "SHADOWCASTER"
Tags { "LightMode"="ShadowCaster" "RenderType"="Opaque" "ShadowSupport"="true" }
Blend One Zero, One Zero
BlendOp Add, Add
ColorMask RGBA
ZWrite On
ZTest LEqual
Cull Back
Offset 0.0, 0.0
AlphaToMask Off
Stencil { Ref 0 ReadMask 255 WriteMask 255 Comp Always Pass Keep Fail Keep ZFail Keep }
CGPROGRAM
#pragma target 3.0
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_shadowcaster
#if (!defined(SHADOWS_CUBE) && defined(SHADOWS_DEPTH))
#include "UnityCG.cginc"
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
};
struct SunnyVaryings { float4 position : SV_POSITION;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 wPos_2;
float4 tmpvar_3;
tmpvar_3 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
wPos_2 = tmpvar_3;
if ((((unity_LightShadowBias).z != 0.0))) {
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_5;
tmpvar_5 = normalize((mul(tmpvar_4, v._glesNormal)));
float tmpvar_6;
tmpvar_6 = dot(tmpvar_5, normalize((((_WorldSpaceLightPos0).xyz - (((tmpvar_3).xyz * (_WorldSpaceLightPos0).w))))));
(wPos_2).xyz = (((tmpvar_3).xyz - ((tmpvar_5 * (((unity_LightShadowBias).z * sqrt(((1.0 - ((tmpvar_6 * tmpvar_6)))))))))));
}

tmpvar_1 = (mul(wPos_2, transpose(unity_MatrixVP)));
float4 clipPos_7;
(clipPos_7).xyw = (tmpvar_1).xyw;
clipPos_7 = UnityApplyLinearShadowBias(tmpvar_1);
o.position = clipPos_7;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

o.target0 = float4(0.0, 0.0, 0.0, 0.0);

return o;
}

#elif (defined(SHADOWS_CUBE) && !defined(SHADOWS_DEPTH))
#include "UnityCG.cginc"
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 sunnyShadowNormal : NORMAL;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float3 xlv_TEXCOORD0 : TEXCOORD0;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
(tmpvar_1).w = 1.0;
(tmpvar_1).xyz = (v._glesVertex).xyz;
o.xlv_TEXCOORD0 = ((((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz - (_LightPositionRange).xyz));
o.position = (mul((mul(tmpvar_1, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

#if defined(SHADOWS_CUBE_IN_DEPTH_TEX)
o.position = UnityApplyLinearShadowBias(UnityClipSpaceShadowCasterPos(v._glesVertex, v.sunnyShadowNormal));
#endif
return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;
#if defined(UNITY_USE_RGBA_FOR_POINT_SHADOWS)

float4 tmpvar_1;
tmpvar_1 = frac(((float4(1.0, 255.0, 65025.0, 1.658138e+07) * min(((((sqrt(dot(i.xlv_TEXCOORD0, i.xlv_TEXCOORD0)) + (unity_LightShadowBias).x)) * (_LightPositionRange).w)), 0.999))));
float4 tmpvar_2;
tmpvar_2 = ((tmpvar_1 - (((tmpvar_1).yzww * 0.003921569))));
o.target0 = tmpvar_2;

#else

o.target0 = UnityEncodeCubeShadowDepth((((sqrt(dot(i.xlv_TEXCOORD0, i.xlv_TEXCOORD0)) + (unity_LightShadowBias).x)) * (_LightPositionRange).w));

#endif
#if defined(SHADOWS_CUBE_IN_DEPTH_TEX)
o.target0 = 0;
#endif
return o;
}

#else
#error Sunny shader keyword combination has no signed donor program
#endif
ENDCG
}
}
Fallback Off
}
