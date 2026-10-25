// Signed Sunny 3.2.5 GLES -> Cg expression translation, not original authoring source.
// Complete native program table SHA256: b96ee564866e2080dc795f332e1e7b6742341ef3ece7aee0fb242f61394ffb2d
Shader "Classic325/Legacy Shaders/Specular" {
Properties {
_Color ("Main Color", Color) = (1.0,1.0,1.0,1.0)
_SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1.0)
_Shininess ("Shininess", Range(0.009999999776482582, 1.0)) = 0.078125
_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
}
SubShader {
LOD 300
Tags { "RenderType"="Opaque" }
Pass {
Name "FORWARD"
Tags { "LightMode"="ForwardBase" "RenderType"="Opaque" "ShadowSupport"="true" }
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
#pragma multi_compile_fwdbase
#pragma multi_compile __ FOG_LINEAR
#if (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
(tmpvar_1).w = 1.0;
(tmpvar_1).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_2;
tmpvar_2[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_2[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_2[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = (mul((mul(tmpvar_1, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_2, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
float3 worldViewDir_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
worldViewDir_5 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_4 = i.xlv_TEXCOORD1;
fixed4 tmpvar_8;
tmpvar_8 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_6;
half3 viewDir_9;
viewDir_9 = worldViewDir_5;
fixed4 c_10;
fixed4 c_11;
float nh_12;
fixed diff_13;
half tmpvar_14;
tmpvar_14 = max(0.0, dot(tmpvar_4, tmpvar_2));
diff_13 = tmpvar_14;
half tmpvar_15;
tmpvar_15 = max(0.0, dot(tmpvar_4, normalize(((tmpvar_2 + viewDir_9)))));
nh_12 = tmpvar_15;
half y_16;
y_16 = ((_Shininess * 128.0));
float tmpvar_17;
tmpvar_17 = ((pow(nh_12, y_16) * (tmpvar_8).w));
(c_11).xyz = (((((((tmpvar_8).xyz * (_Color).xyz)) * ((tmpvar_1 * diff_13)))) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_17))));
(c_11).w = (((tmpvar_8).w * (_Color).w));
(c_10).w = (c_11).w;
(c_10).xyz = (c_11).xyz;
(c_3).xyz = (c_10).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
(tmpvar_1).w = 1.0;
(tmpvar_1).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_2;
tmpvar_2[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_2[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_2[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_3;
tmpvar_3 = normalize((mul(tmpvar_2, v._glesNormal)));
half3 normal_4;
normal_4 = tmpvar_3;
half4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = normal_4;
half3 res_6;
half3 x_7;
(x_7).x = dot(unity_SHAr, tmpvar_5);
(x_7).y = dot(unity_SHAg, tmpvar_5);
(x_7).z = dot(unity_SHAb, tmpvar_5);
half3 x1_8;
half4 tmpvar_9;
tmpvar_9 = (((normal_4).xyzz * (normal_4).yzzx));
(x1_8).x = dot(unity_SHBr, tmpvar_9);
(x1_8).y = dot(unity_SHBg, tmpvar_9);
(x1_8).z = dot(unity_SHBb, tmpvar_9);
res_6 = ((x_7 + ((x1_8 + (((unity_SHC).xyz * (((((normal_4).x * (normal_4).x)) - (((normal_4).y * (normal_4).y))))))))));
half3 tmpvar_10;
tmpvar_10 = max(((((1.055 * pow(max(res_6, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_6 = tmpvar_10;
o.position = (mul((mul(tmpvar_1, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_3;
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = max(float3(0.0, 0.0, 0.0), tmpvar_10);

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 viewDir_10;
viewDir_10 = worldViewDir_6;
fixed4 c_11;
fixed4 c_12;
float nh_13;
fixed diff_14;
half tmpvar_15;
tmpvar_15 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_14 = tmpvar_15;
half tmpvar_16;
tmpvar_16 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_10)))));
nh_13 = tmpvar_16;
half y_17;
y_17 = ((_Shininess * 128.0));
float tmpvar_18;
tmpvar_18 = ((pow(nh_13, y_17) * (tmpvar_9).w));
(c_12).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_14)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_18))));
(c_12).w = (((tmpvar_9).w * (_Color).w));
(c_11).w = (c_12).w;
(c_11).xyz = (((c_12).xyz + ((tmpvar_4 * i.xlv_TEXCOORD3))));
(c_3).xyz = (c_11).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
(tmpvar_1).w = 1.0;
(tmpvar_1).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_2;
tmpvar_2[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_2[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_2[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_3;
tmpvar_3 = normalize((mul(tmpvar_2, v._glesNormal)));
half3 normal_4;
normal_4 = tmpvar_3;
half4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = normal_4;
half3 res_6;
half3 x_7;
(x_7).x = dot(unity_SHAr, tmpvar_5);
(x_7).y = dot(unity_SHAg, tmpvar_5);
(x_7).z = dot(unity_SHAb, tmpvar_5);
half3 x1_8;
half4 tmpvar_9;
tmpvar_9 = (((normal_4).xyzz * (normal_4).yzzx));
(x1_8).x = dot(unity_SHBr, tmpvar_9);
(x1_8).y = dot(unity_SHBg, tmpvar_9);
(x1_8).z = dot(unity_SHBb, tmpvar_9);
res_6 = ((x_7 + ((x1_8 + (((unity_SHC).xyz * (((((normal_4).x * (normal_4).x)) - (((normal_4).y * (normal_4).y))))))))));
half3 tmpvar_10;
tmpvar_10 = max(((((1.055 * pow(max(res_6, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_6 = tmpvar_10;
o.position = (mul((mul(tmpvar_1, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_3;
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = max(float3(0.0, 0.0, 0.0), tmpvar_10);
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 tmpvar_14;
tmpvar_14 = ((tmpvar_2 * tmpvar_1));
tmpvar_2 = tmpvar_14;
half3 viewDir_15;
viewDir_15 = worldViewDir_8;
fixed4 c_16;
fixed4 c_17;
float nh_18;
fixed diff_19;
half tmpvar_20;
tmpvar_20 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_19 = tmpvar_20;
half tmpvar_21;
tmpvar_21 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_15)))));
nh_18 = tmpvar_21;
half y_22;
y_22 = ((_Shininess * 128.0));
float tmpvar_23;
tmpvar_23 = ((pow(nh_18, y_22) * (tmpvar_11).w));
(c_17).xyz = ((((((tmpvar_6 * tmpvar_14)) * diff_19)) + ((((tmpvar_14 * (_SpecColor).xyz)) * tmpvar_23))));
(c_17).w = (((tmpvar_11).w * (_Color).w));
(c_16).w = (c_17).w;
(c_16).xyz = (((c_17).xyz + ((tmpvar_6 * i.xlv_TEXCOORD3))));
(c_4).xyz = (c_16).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD6 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
tmpvar_10 = pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_11).w)) * (tmpvar_11).xyz)), (unity_DynamicLightmap_HDR).yyy);
half3 viewDir_12;
viewDir_12 = worldViewDir_6;
fixed4 c_13;
fixed4 c_14;
float nh_15;
fixed diff_16;
half tmpvar_17;
tmpvar_17 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_16 = tmpvar_17;
half tmpvar_18;
tmpvar_18 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_12)))));
nh_15 = tmpvar_18;
half y_19;
y_19 = ((_Shininess * 128.0));
float tmpvar_20;
tmpvar_20 = ((pow(nh_15, y_19) * (tmpvar_9).w));
(c_14).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_16)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_20))));
(c_14).w = (((tmpvar_9).w * (_Color).w));
(c_13).w = (c_14).w;
(c_13).xyz = (((c_14).xyz + ((tmpvar_4 * tmpvar_10))));
(c_3).xyz = (c_13).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 tmpvar_10;
half4 tmpvar_11;
tmpvar_11 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_12;
color_12 = tmpvar_11;
tmpvar_10 = (((unity_Lightmap_HDR).x * (color_12).xyz));
half3 viewDir_13;
viewDir_13 = worldViewDir_6;
fixed4 c_14;
fixed4 c_15;
float nh_16;
fixed diff_17;
half tmpvar_18;
tmpvar_18 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_17 = tmpvar_18;
half tmpvar_19;
tmpvar_19 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_13)))));
nh_16 = tmpvar_19;
half y_20;
y_20 = ((_Shininess * 128.0));
float tmpvar_21;
tmpvar_21 = ((pow(nh_16, y_20) * (tmpvar_9).w));
(c_15).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_17)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_21))));
(c_15).w = (((tmpvar_9).w * (_Color).w));
(c_14).w = (c_15).w;
(c_14).xyz = (((c_15).xyz + ((tmpvar_4 * tmpvar_10))));
(c_3).xyz = (c_14).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 tmpvar_10;
half4 tmpvar_11;
tmpvar_11 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_12;
color_12 = tmpvar_11;
tmpvar_10 = (((unity_Lightmap_HDR).x * (color_12).xyz));
fixed4 tmpvar_13;
tmpvar_13 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_10 = ((tmpvar_10 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_13).w)) * (tmpvar_13).xyz)), (unity_DynamicLightmap_HDR).yyy)));
half3 viewDir_14;
viewDir_14 = worldViewDir_6;
fixed4 c_15;
fixed4 c_16;
float nh_17;
fixed diff_18;
half tmpvar_19;
tmpvar_19 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_18 = tmpvar_19;
half tmpvar_20;
tmpvar_20 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_14)))));
nh_17 = tmpvar_20;
half y_21;
y_21 = ((_Shininess * 128.0));
float tmpvar_22;
tmpvar_22 = ((pow(nh_17, y_21) * (tmpvar_9).w));
(c_16).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_18)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_22))));
(c_16).w = (((tmpvar_9).w * (_Color).w));
(c_15).w = (c_16).w;
(c_15).xyz = (((c_16).xyz + ((tmpvar_4 * tmpvar_10))));
(c_3).xyz = (c_15).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 normalWorld_10;
normalWorld_10 = tmpvar_5;
half3 tmpvar_11;
half3 ambient_12;
half4 tmpvar_13;
(tmpvar_13).w = 1.0;
(tmpvar_13).xyz = normalWorld_10;
half3 x_14;
(x_14).x = dot(unity_SHAr, tmpvar_13);
(x_14).y = dot(unity_SHAg, tmpvar_13);
(x_14).z = dot(unity_SHAb, tmpvar_13);
half3 x1_15;
half4 tmpvar_16;
tmpvar_16 = (((normalWorld_10).xyzz * (normalWorld_10).yzzx));
(x1_15).x = dot(unity_SHBr, tmpvar_16);
(x1_15).y = dot(unity_SHBg, tmpvar_16);
(x1_15).z = dot(unity_SHBb, tmpvar_16);
ambient_12 = max(float3(0.0, 0.0, 0.0), ((x_14 + ((x1_15 + (((unity_SHC).xyz * (((((normalWorld_10).x * (normalWorld_10).x)) - (((normalWorld_10).y * (normalWorld_10).y)))))))))));
half3 tmpvar_17;
tmpvar_17 = max(((((1.055 * pow(max(ambient_12, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_12 = tmpvar_17;
half4 tmpvar_18;
tmpvar_18 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_19;
color_19 = tmpvar_18;
tmpvar_11 = ((tmpvar_17 + (((unity_Lightmap_HDR).x * (color_19).xyz))));
half3 viewDir_20;
viewDir_20 = worldViewDir_6;
fixed4 c_21;
fixed4 c_22;
float nh_23;
fixed diff_24;
half tmpvar_25;
tmpvar_25 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_24 = tmpvar_25;
half tmpvar_26;
tmpvar_26 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_20)))));
nh_23 = tmpvar_26;
half y_27;
y_27 = ((_Shininess * 128.0));
float tmpvar_28;
tmpvar_28 = ((pow(nh_23, y_27) * (tmpvar_9).w));
(c_22).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_24)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_28))));
(c_22).w = (((tmpvar_9).w * (_Color).w));
(c_21).w = (c_22).w;
(c_21).xyz = (((c_22).xyz + ((tmpvar_4 * tmpvar_11))));
(c_3).xyz = (c_21).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 c_1;
fixed4 tmpvar_2;
tmpvar_2 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half3 tmpvar_3;
half4 tmpvar_4;
tmpvar_4 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_5;
color_5 = tmpvar_4;
tmpvar_3 = (((unity_Lightmap_HDR).x * (color_5).xyz));
fixed4 c_6;
fixed4 c_7;
(c_7).xyz = float3(0.0, 0.0, 0.0);
(c_7).w = (((tmpvar_2).w * (_Color).w));
(c_6).w = (c_7).w;
(c_6).xyz = (((((tmpvar_2).xyz * (_Color).xyz)) * tmpvar_3));
(c_1).xyz = (c_6).xyz;
(c_1).w = 1.0;
o.target0 = c_1;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 c_1;
fixed4 tmpvar_2;
tmpvar_2 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half3 tmpvar_3;
half4 tmpvar_4;
tmpvar_4 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_5;
color_5 = tmpvar_4;
tmpvar_3 = (((unity_Lightmap_HDR).x * (color_5).xyz));
fixed4 tmpvar_6;
tmpvar_6 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_3 = ((tmpvar_3 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_6).w)) * (tmpvar_6).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_7;
fixed4 c_8;
(c_8).xyz = float3(0.0, 0.0, 0.0);
(c_8).w = (((tmpvar_2).w * (_Color).w));
(c_7).w = (c_8).w;
(c_7).xyz = (((((tmpvar_2).xyz * (_Color).xyz)) * tmpvar_3));
(c_1).xyz = (c_7).xyz;
(c_1).w = 1.0;
o.target0 = c_1;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 c_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half3 normalWorld_4;
normalWorld_4 = tmpvar_2;
half3 tmpvar_5;
half3 ambient_6;
half4 tmpvar_7;
(tmpvar_7).w = 1.0;
(tmpvar_7).xyz = normalWorld_4;
half3 x_8;
(x_8).x = dot(unity_SHAr, tmpvar_7);
(x_8).y = dot(unity_SHAg, tmpvar_7);
(x_8).z = dot(unity_SHAb, tmpvar_7);
half3 x1_9;
half4 tmpvar_10;
tmpvar_10 = (((normalWorld_4).xyzz * (normalWorld_4).yzzx));
(x1_9).x = dot(unity_SHBr, tmpvar_10);
(x1_9).y = dot(unity_SHBg, tmpvar_10);
(x1_9).z = dot(unity_SHBb, tmpvar_10);
ambient_6 = max(float3(0.0, 0.0, 0.0), ((x_8 + ((x1_9 + (((unity_SHC).xyz * (((((normalWorld_4).x * (normalWorld_4).x)) - (((normalWorld_4).y * (normalWorld_4).y)))))))))));
half3 tmpvar_11;
tmpvar_11 = max(((((1.055 * pow(max(ambient_6, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_6 = tmpvar_11;
half4 tmpvar_12;
tmpvar_12 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_13;
color_13 = tmpvar_12;
tmpvar_5 = ((tmpvar_11 + (((unity_Lightmap_HDR).x * (color_13).xyz))));
fixed4 c_14;
fixed4 c_15;
(c_15).xyz = float3(0.0, 0.0, 0.0);
(c_15).w = (((tmpvar_3).w * (_Color).w));
(c_14).w = (c_15).w;
(c_14).xyz = (((((tmpvar_3).xyz * (_Color).xyz)) * tmpvar_5));
(c_1).xyz = (c_14).xyz;
(c_1).w = 1.0;
o.target0 = c_1;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD6 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 normalWorld_10;
normalWorld_10 = tmpvar_5;
half4 realtimeDirTex_11;
half3 tmpvar_12;
fixed4 tmpvar_13;
tmpvar_13 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
fixed4 tmpvar_14;
tmpvar_14 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
realtimeDirTex_11 = tmpvar_14;
fixed4 dirTex_15;
dirTex_15 = realtimeDirTex_11;
fixed3 y_16;
y_16 = (((dirTex_15).xyz - 0.5));
fixed tmpvar_17;
tmpvar_17 = max(0.0001, (dirTex_15).w);
tmpvar_12 = ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_13).w)) * (tmpvar_13).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_10, y_16) + 0.5)))) / tmpvar_17));
half3 viewDir_18;
viewDir_18 = worldViewDir_6;
fixed4 c_19;
fixed4 c_20;
float nh_21;
fixed diff_22;
half tmpvar_23;
tmpvar_23 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_22 = tmpvar_23;
half tmpvar_24;
tmpvar_24 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_18)))));
nh_21 = tmpvar_24;
half y_25;
y_25 = ((_Shininess * 128.0));
float tmpvar_26;
tmpvar_26 = ((pow(nh_21, y_25) * (tmpvar_9).w));
(c_20).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_22)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_26))));
(c_20).w = (((tmpvar_9).w * (_Color).w));
(c_19).w = (c_20).w;
(c_19).xyz = (((c_20).xyz + ((tmpvar_4 * tmpvar_12))));
(c_3).xyz = (c_19).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD6 : TEXCOORD4;
float3 xlv_TEXCOORD7 : TEXCOORD5;
float3 xlv_TEXCOORD8 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
tmpvar_6 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_7;
tmpvar_7[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_7[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_7[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_8;
tmpvar_8 = normalize((mul(tmpvar_7, v._glesNormal)));
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_9[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_9[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul((v._glesTANGENT).xyz, tmpvar_9)));
worldTangent_3 = tmpvar_10;
float tmpvar_11;
tmpvar_11 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_11;
float3 tmpvar_12;
tmpvar_12 = (((((((tmpvar_8).yzx * (worldTangent_3).zxy)) - (((tmpvar_8).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_12;
float4 tmpvar_13;
(tmpvar_13).x = (worldTangent_3).x;
(tmpvar_13).y = (worldBinormal_1).x;
(tmpvar_13).z = (tmpvar_8).x;
(tmpvar_13).w = (tmpvar_6).x;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).y;
(tmpvar_14).y = (worldBinormal_1).y;
(tmpvar_14).z = (tmpvar_8).y;
(tmpvar_14).w = (tmpvar_6).y;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).z;
(tmpvar_15).y = (worldBinormal_1).z;
(tmpvar_15).z = (tmpvar_8).z;
(tmpvar_15).w = (tmpvar_6).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_8;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD6 = (tmpvar_13).xyz;
o.xlv_TEXCOORD7 = (tmpvar_14).xyz;
o.xlv_TEXCOORD8 = (tmpvar_15).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 normalWorld_10;
normalWorld_10 = tmpvar_5;
fixed4 bakedDirTex_11;
half3 tmpvar_12;
half4 tmpvar_13;
tmpvar_13 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_14;
color_14 = tmpvar_13;
half4 tmpvar_15;
tmpvar_15 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_11 = tmpvar_15;
fixed3 y_16;
y_16 = (((bakedDirTex_11).xyz - 0.5));
fixed tmpvar_17;
tmpvar_17 = max(0.0001, (bakedDirTex_11).w);
tmpvar_12 = (((((((unity_Lightmap_HDR).x * (color_14).xyz)) * ((dot(normalWorld_10, y_16) + 0.5)))) / tmpvar_17));
half3 viewDir_18;
viewDir_18 = worldViewDir_6;
fixed4 c_19;
fixed4 c_20;
float nh_21;
fixed diff_22;
half tmpvar_23;
tmpvar_23 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_22 = tmpvar_23;
half tmpvar_24;
tmpvar_24 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_18)))));
nh_21 = tmpvar_24;
half y_25;
y_25 = ((_Shininess * 128.0));
float tmpvar_26;
tmpvar_26 = ((pow(nh_21, y_25) * (tmpvar_9).w));
(c_20).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_22)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_26))));
(c_20).w = (((tmpvar_9).w * (_Color).w));
(c_19).w = (c_20).w;
(c_19).xyz = (((c_20).xyz + ((tmpvar_4 * tmpvar_12))));
(c_3).xyz = (c_19).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD6 : TEXCOORD4;
float3 xlv_TEXCOORD7 : TEXCOORD5;
float3 xlv_TEXCOORD8 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
tmpvar_6 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_7;
tmpvar_7[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_7[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_7[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_8;
tmpvar_8 = normalize((mul(tmpvar_7, v._glesNormal)));
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_9[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_9[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul((v._glesTANGENT).xyz, tmpvar_9)));
worldTangent_3 = tmpvar_10;
float tmpvar_11;
tmpvar_11 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_11;
float3 tmpvar_12;
tmpvar_12 = (((((((tmpvar_8).yzx * (worldTangent_3).zxy)) - (((tmpvar_8).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_12;
float4 tmpvar_13;
(tmpvar_13).x = (worldTangent_3).x;
(tmpvar_13).y = (worldBinormal_1).x;
(tmpvar_13).z = (tmpvar_8).x;
(tmpvar_13).w = (tmpvar_6).x;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).y;
(tmpvar_14).y = (worldBinormal_1).y;
(tmpvar_14).z = (tmpvar_8).y;
(tmpvar_14).w = (tmpvar_6).y;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).z;
(tmpvar_15).y = (worldBinormal_1).z;
(tmpvar_15).z = (tmpvar_8).z;
(tmpvar_15).w = (tmpvar_6).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_8;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD6 = (tmpvar_13).xyz;
o.xlv_TEXCOORD7 = (tmpvar_14).xyz;
o.xlv_TEXCOORD8 = (tmpvar_15).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 normalWorld_10;
normalWorld_10 = tmpvar_5;
fixed4 bakedDirTex_11;
half3 tmpvar_12;
half3 ambient_13;
half4 tmpvar_14;
(tmpvar_14).w = 1.0;
(tmpvar_14).xyz = normalWorld_10;
half3 x_15;
(x_15).x = dot(unity_SHAr, tmpvar_14);
(x_15).y = dot(unity_SHAg, tmpvar_14);
(x_15).z = dot(unity_SHAb, tmpvar_14);
half3 x1_16;
half4 tmpvar_17;
tmpvar_17 = (((normalWorld_10).xyzz * (normalWorld_10).yzzx));
(x1_16).x = dot(unity_SHBr, tmpvar_17);
(x1_16).y = dot(unity_SHBg, tmpvar_17);
(x1_16).z = dot(unity_SHBb, tmpvar_17);
ambient_13 = max(float3(0.0, 0.0, 0.0), ((x_15 + ((x1_16 + (((unity_SHC).xyz * (((((normalWorld_10).x * (normalWorld_10).x)) - (((normalWorld_10).y * (normalWorld_10).y)))))))))));
half3 tmpvar_18;
tmpvar_18 = max(((((1.055 * pow(max(ambient_13, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_13 = tmpvar_18;
half4 tmpvar_19;
tmpvar_19 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_20;
color_20 = tmpvar_19;
half4 tmpvar_21;
tmpvar_21 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_11 = tmpvar_21;
fixed3 y_22;
y_22 = (((bakedDirTex_11).xyz - 0.5));
fixed tmpvar_23;
tmpvar_23 = max(0.0001, (bakedDirTex_11).w);
tmpvar_12 = ((tmpvar_18 + (((((((unity_Lightmap_HDR).x * (color_20).xyz)) * ((dot(normalWorld_10, y_22) + 0.5)))) / tmpvar_23))));
half3 viewDir_24;
viewDir_24 = worldViewDir_6;
fixed4 c_25;
fixed4 c_26;
float nh_27;
fixed diff_28;
half tmpvar_29;
tmpvar_29 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_28 = tmpvar_29;
half tmpvar_30;
tmpvar_30 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_24)))));
nh_27 = tmpvar_30;
half y_31;
y_31 = ((_Shininess * 128.0));
float tmpvar_32;
tmpvar_32 = ((pow(nh_27, y_31) * (tmpvar_9).w));
(c_26).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_28)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_32))));
(c_26).w = (((tmpvar_9).w * (_Color).w));
(c_25).w = (c_26).w;
(c_25).xyz = (((c_26).xyz + ((tmpvar_4 * tmpvar_12))));
(c_3).xyz = (c_25).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD6 : TEXCOORD4;
float3 xlv_TEXCOORD7 : TEXCOORD5;
float3 xlv_TEXCOORD8 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
tmpvar_6 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_7;
tmpvar_7[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_7[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_7[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_8;
tmpvar_8 = normalize((mul(tmpvar_7, v._glesNormal)));
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_9[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_9[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul((v._glesTANGENT).xyz, tmpvar_9)));
worldTangent_3 = tmpvar_10;
float tmpvar_11;
tmpvar_11 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_11;
float3 tmpvar_12;
tmpvar_12 = (((((((tmpvar_8).yzx * (worldTangent_3).zxy)) - (((tmpvar_8).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_12;
float4 tmpvar_13;
(tmpvar_13).x = (worldTangent_3).x;
(tmpvar_13).y = (worldBinormal_1).x;
(tmpvar_13).z = (tmpvar_8).x;
(tmpvar_13).w = (tmpvar_6).x;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).y;
(tmpvar_14).y = (worldBinormal_1).y;
(tmpvar_14).z = (tmpvar_8).y;
(tmpvar_14).w = (tmpvar_6).y;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).z;
(tmpvar_15).y = (worldBinormal_1).z;
(tmpvar_15).z = (tmpvar_8).z;
(tmpvar_15).w = (tmpvar_6).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_8;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD6 = (tmpvar_13).xyz;
o.xlv_TEXCOORD7 = (tmpvar_14).xyz;
o.xlv_TEXCOORD8 = (tmpvar_15).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 normalWorld_10;
normalWorld_10 = tmpvar_5;
half4 realtimeDirTex_11;
fixed4 bakedDirTex_12;
half3 tmpvar_13;
half4 tmpvar_14;
tmpvar_14 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_15;
color_15 = tmpvar_14;
half4 tmpvar_16;
tmpvar_16 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_12 = tmpvar_16;
fixed3 y_17;
y_17 = (((bakedDirTex_12).xyz - 0.5));
fixed tmpvar_18;
tmpvar_18 = max(0.0001, (bakedDirTex_12).w);
tmpvar_13 = (((((((unity_Lightmap_HDR).x * (color_15).xyz)) * ((dot(normalWorld_10, y_17) + 0.5)))) / tmpvar_18));
fixed4 tmpvar_19;
tmpvar_19 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_20;
tmpvar_20 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_11 = tmpvar_20;
fixed4 dirTex_21;
dirTex_21 = realtimeDirTex_11;
fixed3 y_22;
y_22 = (((dirTex_21).xyz - 0.5));
fixed tmpvar_23;
tmpvar_23 = max(0.0001, (dirTex_21).w);
tmpvar_13 = ((tmpvar_13 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_19).w)) * (tmpvar_19).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_10, y_22) + 0.5)))) / tmpvar_23))));
half3 viewDir_24;
viewDir_24 = worldViewDir_6;
fixed4 c_25;
fixed4 c_26;
float nh_27;
fixed diff_28;
half tmpvar_29;
tmpvar_29 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_28 = tmpvar_29;
half tmpvar_30;
tmpvar_30 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_24)))));
nh_27 = tmpvar_30;
half y_31;
y_31 = ((_Shininess * 128.0));
float tmpvar_32;
tmpvar_32 = ((pow(nh_27, y_31) * (tmpvar_9).w));
(c_26).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_28)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_32))));
(c_26).w = (((tmpvar_9).w * (_Color).w));
(c_25).w = (c_26).w;
(c_25).xyz = (((c_26).xyz + ((tmpvar_4 * tmpvar_13))));
(c_3).xyz = (c_25).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD6 : TEXCOORD4;
float3 xlv_TEXCOORD7 : TEXCOORD5;
float3 xlv_TEXCOORD8 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
tmpvar_6 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_7;
tmpvar_7[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_7[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_7[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_8;
tmpvar_8 = normalize((mul(tmpvar_7, v._glesNormal)));
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_9[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_9[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul((v._glesTANGENT).xyz, tmpvar_9)));
worldTangent_3 = tmpvar_10;
float tmpvar_11;
tmpvar_11 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_11;
float3 tmpvar_12;
tmpvar_12 = (((((((tmpvar_8).yzx * (worldTangent_3).zxy)) - (((tmpvar_8).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_12;
float4 tmpvar_13;
(tmpvar_13).x = (worldTangent_3).x;
(tmpvar_13).y = (worldBinormal_1).x;
(tmpvar_13).z = (tmpvar_8).x;
(tmpvar_13).w = (tmpvar_6).x;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).y;
(tmpvar_14).y = (worldBinormal_1).y;
(tmpvar_14).z = (tmpvar_8).y;
(tmpvar_14).w = (tmpvar_6).y;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).z;
(tmpvar_15).y = (worldBinormal_1).z;
(tmpvar_15).z = (tmpvar_8).z;
(tmpvar_15).w = (tmpvar_6).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_8;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD6 = (tmpvar_13).xyz;
o.xlv_TEXCOORD7 = (tmpvar_14).xyz;
o.xlv_TEXCOORD8 = (tmpvar_15).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 c_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half3 normalWorld_4;
normalWorld_4 = tmpvar_2;
fixed4 bakedDirTex_5;
half3 tmpvar_6;
half4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_8;
color_8 = tmpvar_7;
half4 tmpvar_9;
tmpvar_9 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_5 = tmpvar_9;
fixed3 y_10;
y_10 = (((bakedDirTex_5).xyz - 0.5));
fixed tmpvar_11;
tmpvar_11 = max(0.0001, (bakedDirTex_5).w);
tmpvar_6 = (((((((unity_Lightmap_HDR).x * (color_8).xyz)) * ((dot(normalWorld_4, y_10) + 0.5)))) / tmpvar_11));
fixed4 c_12;
fixed4 c_13;
(c_13).xyz = float3(0.0, 0.0, 0.0);
(c_13).w = (((tmpvar_3).w * (_Color).w));
(c_12).w = (c_13).w;
(c_12).xyz = (((((tmpvar_3).xyz * (_Color).xyz)) * tmpvar_6));
(c_1).xyz = (c_12).xyz;
(c_1).w = 1.0;
o.target0 = c_1;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD6 : TEXCOORD4;
float3 xlv_TEXCOORD7 : TEXCOORD5;
float3 xlv_TEXCOORD8 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
tmpvar_6 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_7;
tmpvar_7[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_7[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_7[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_8;
tmpvar_8 = normalize((mul(tmpvar_7, v._glesNormal)));
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_9[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_9[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul((v._glesTANGENT).xyz, tmpvar_9)));
worldTangent_3 = tmpvar_10;
float tmpvar_11;
tmpvar_11 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_11;
float3 tmpvar_12;
tmpvar_12 = (((((((tmpvar_8).yzx * (worldTangent_3).zxy)) - (((tmpvar_8).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_12;
float4 tmpvar_13;
(tmpvar_13).x = (worldTangent_3).x;
(tmpvar_13).y = (worldBinormal_1).x;
(tmpvar_13).z = (tmpvar_8).x;
(tmpvar_13).w = (tmpvar_6).x;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).y;
(tmpvar_14).y = (worldBinormal_1).y;
(tmpvar_14).z = (tmpvar_8).y;
(tmpvar_14).w = (tmpvar_6).y;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).z;
(tmpvar_15).y = (worldBinormal_1).z;
(tmpvar_15).z = (tmpvar_8).z;
(tmpvar_15).w = (tmpvar_6).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_8;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD6 = (tmpvar_13).xyz;
o.xlv_TEXCOORD7 = (tmpvar_14).xyz;
o.xlv_TEXCOORD8 = (tmpvar_15).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 c_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half3 normalWorld_4;
normalWorld_4 = tmpvar_2;
fixed4 bakedDirTex_5;
half3 tmpvar_6;
half3 ambient_7;
half4 tmpvar_8;
(tmpvar_8).w = 1.0;
(tmpvar_8).xyz = normalWorld_4;
half3 x_9;
(x_9).x = dot(unity_SHAr, tmpvar_8);
(x_9).y = dot(unity_SHAg, tmpvar_8);
(x_9).z = dot(unity_SHAb, tmpvar_8);
half3 x1_10;
half4 tmpvar_11;
tmpvar_11 = (((normalWorld_4).xyzz * (normalWorld_4).yzzx));
(x1_10).x = dot(unity_SHBr, tmpvar_11);
(x1_10).y = dot(unity_SHBg, tmpvar_11);
(x1_10).z = dot(unity_SHBb, tmpvar_11);
ambient_7 = max(float3(0.0, 0.0, 0.0), ((x_9 + ((x1_10 + (((unity_SHC).xyz * (((((normalWorld_4).x * (normalWorld_4).x)) - (((normalWorld_4).y * (normalWorld_4).y)))))))))));
half3 tmpvar_12;
tmpvar_12 = max(((((1.055 * pow(max(ambient_7, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_7 = tmpvar_12;
half4 tmpvar_13;
tmpvar_13 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_14;
color_14 = tmpvar_13;
half4 tmpvar_15;
tmpvar_15 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_5 = tmpvar_15;
fixed3 y_16;
y_16 = (((bakedDirTex_5).xyz - 0.5));
fixed tmpvar_17;
tmpvar_17 = max(0.0001, (bakedDirTex_5).w);
tmpvar_6 = ((tmpvar_12 + (((((((unity_Lightmap_HDR).x * (color_14).xyz)) * ((dot(normalWorld_4, y_16) + 0.5)))) / tmpvar_17))));
fixed4 c_18;
fixed4 c_19;
(c_19).xyz = float3(0.0, 0.0, 0.0);
(c_19).w = (((tmpvar_3).w * (_Color).w));
(c_18).w = (c_19).w;
(c_18).xyz = (((((tmpvar_3).xyz * (_Color).xyz)) * tmpvar_6));
(c_1).xyz = (c_18).xyz;
(c_1).w = 1.0;
o.target0 = c_1;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD6 : TEXCOORD4;
float3 xlv_TEXCOORD7 : TEXCOORD5;
float3 xlv_TEXCOORD8 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
tmpvar_6 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_7;
tmpvar_7[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_7[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_7[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_8;
tmpvar_8 = normalize((mul(tmpvar_7, v._glesNormal)));
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_9[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_9[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul((v._glesTANGENT).xyz, tmpvar_9)));
worldTangent_3 = tmpvar_10;
float tmpvar_11;
tmpvar_11 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_11;
float3 tmpvar_12;
tmpvar_12 = (((((((tmpvar_8).yzx * (worldTangent_3).zxy)) - (((tmpvar_8).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_12;
float4 tmpvar_13;
(tmpvar_13).x = (worldTangent_3).x;
(tmpvar_13).y = (worldBinormal_1).x;
(tmpvar_13).z = (tmpvar_8).x;
(tmpvar_13).w = (tmpvar_6).x;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).y;
(tmpvar_14).y = (worldBinormal_1).y;
(tmpvar_14).z = (tmpvar_8).y;
(tmpvar_14).w = (tmpvar_6).y;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).z;
(tmpvar_15).y = (worldBinormal_1).z;
(tmpvar_15).z = (tmpvar_8).z;
(tmpvar_15).w = (tmpvar_6).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_8;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD6 = (tmpvar_13).xyz;
o.xlv_TEXCOORD7 = (tmpvar_14).xyz;
o.xlv_TEXCOORD8 = (tmpvar_15).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 c_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half3 normalWorld_4;
normalWorld_4 = tmpvar_2;
half4 realtimeDirTex_5;
fixed4 bakedDirTex_6;
half3 tmpvar_7;
half4 tmpvar_8;
tmpvar_8 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_9;
color_9 = tmpvar_8;
half4 tmpvar_10;
tmpvar_10 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_6 = tmpvar_10;
fixed3 y_11;
y_11 = (((bakedDirTex_6).xyz - 0.5));
fixed tmpvar_12;
tmpvar_12 = max(0.0001, (bakedDirTex_6).w);
tmpvar_7 = (((((((unity_Lightmap_HDR).x * (color_9).xyz)) * ((dot(normalWorld_4, y_11) + 0.5)))) / tmpvar_12));
fixed4 tmpvar_13;
tmpvar_13 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_14;
tmpvar_14 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_5 = tmpvar_14;
fixed4 dirTex_15;
dirTex_15 = realtimeDirTex_5;
fixed3 y_16;
y_16 = (((dirTex_15).xyz - 0.5));
fixed tmpvar_17;
tmpvar_17 = max(0.0001, (dirTex_15).w);
tmpvar_7 = ((tmpvar_7 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_13).w)) * (tmpvar_13).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_4, y_16) + 0.5)))) / tmpvar_17))));
fixed4 c_18;
fixed4 c_19;
(c_19).xyz = float3(0.0, 0.0, 0.0);
(c_19).w = (((tmpvar_3).w * (_Color).w));
(c_18).w = (c_19).w;
(c_18).xyz = (((((tmpvar_3).xyz * (_Color).xyz)) * tmpvar_7));
(c_1).xyz = (c_18).xyz;
(c_1).w = 1.0;
o.target0 = c_1;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
float2 tmpvar_4;
tmpvar_4 = (((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy));
(tmpvar_1).xy = ((tmpvar_4 + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = ((tmpvar_4 + (unity_LightmapST).zw));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 tmpvar_14;
half3 tmpvar_15;
tmpvar_14 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_16;
tmpvar_16 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_17;
color_17 = tmpvar_16;
tmpvar_15 = (((unity_Lightmap_HDR).x * (color_17).xyz));
tmpvar_2 = tmpvar_14;
half3 viewDir_18;
viewDir_18 = worldViewDir_8;
fixed4 c_19;
fixed4 c_20;
float nh_21;
fixed diff_22;
half tmpvar_23;
tmpvar_23 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_22 = tmpvar_23;
half tmpvar_24;
tmpvar_24 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_18)))));
nh_21 = tmpvar_24;
half y_25;
y_25 = ((_Shininess * 128.0));
float tmpvar_26;
tmpvar_26 = ((pow(nh_21, y_25) * (tmpvar_11).w));
(c_20).xyz = ((((((tmpvar_6 * tmpvar_14)) * diff_22)) + ((((tmpvar_14 * (_SpecColor).xyz)) * tmpvar_26))));
(c_20).w = (((tmpvar_11).w * (_Color).w));
(c_19).w = (c_20).w;
(c_19).xyz = (((c_20).xyz + ((tmpvar_6 * tmpvar_15))));
(c_4).xyz = (c_19).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
float2 tmpvar_4;
tmpvar_4 = (((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy));
(tmpvar_1).xy = ((tmpvar_4 + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = ((tmpvar_4 + (unity_LightmapST).zw));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 tmpvar_14;
half3 tmpvar_15;
tmpvar_14 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_16;
tmpvar_16 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_17;
color_17 = tmpvar_16;
tmpvar_15 = (((unity_Lightmap_HDR).x * (color_17).xyz));
fixed4 tmpvar_18;
tmpvar_18 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_15 = ((tmpvar_15 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_18).w)) * (tmpvar_18).xyz)), (unity_DynamicLightmap_HDR).yyy)));
tmpvar_2 = tmpvar_14;
half3 viewDir_19;
viewDir_19 = worldViewDir_8;
fixed4 c_20;
fixed4 c_21;
float nh_22;
fixed diff_23;
half tmpvar_24;
tmpvar_24 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_23 = tmpvar_24;
half tmpvar_25;
tmpvar_25 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_19)))));
nh_22 = tmpvar_25;
half y_26;
y_26 = ((_Shininess * 128.0));
float tmpvar_27;
tmpvar_27 = ((pow(nh_22, y_26) * (tmpvar_11).w));
(c_21).xyz = ((((((tmpvar_6 * tmpvar_14)) * diff_23)) + ((((tmpvar_14 * (_SpecColor).xyz)) * tmpvar_27))));
(c_21).w = (((tmpvar_11).w * (_Color).w));
(c_20).w = (c_21).w;
(c_20).xyz = (((c_21).xyz + ((tmpvar_6 * tmpvar_15))));
(c_4).xyz = (c_20).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
float2 tmpvar_4;
tmpvar_4 = (((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy));
(tmpvar_1).xy = ((tmpvar_4 + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = ((tmpvar_4 + (unity_LightmapST).zw));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 normalWorld_14;
normalWorld_14 = tmpvar_7;
half3 tmpvar_15;
half3 tmpvar_16;
tmpvar_15 = ((tmpvar_2 * tmpvar_1));
half3 ambient_17;
half4 tmpvar_18;
(tmpvar_18).w = 1.0;
(tmpvar_18).xyz = normalWorld_14;
half3 x_19;
(x_19).x = dot(unity_SHAr, tmpvar_18);
(x_19).y = dot(unity_SHAg, tmpvar_18);
(x_19).z = dot(unity_SHAb, tmpvar_18);
half3 x1_20;
half4 tmpvar_21;
tmpvar_21 = (((normalWorld_14).xyzz * (normalWorld_14).yzzx));
(x1_20).x = dot(unity_SHBr, tmpvar_21);
(x1_20).y = dot(unity_SHBg, tmpvar_21);
(x1_20).z = dot(unity_SHBb, tmpvar_21);
ambient_17 = max(float3(0.0, 0.0, 0.0), ((x_19 + ((x1_20 + (((unity_SHC).xyz * (((((normalWorld_14).x * (normalWorld_14).x)) - (((normalWorld_14).y * (normalWorld_14).y)))))))))));
half3 tmpvar_22;
tmpvar_22 = max(((((1.055 * pow(max(ambient_17, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_17 = tmpvar_22;
half4 tmpvar_23;
tmpvar_23 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_24;
color_24 = tmpvar_23;
tmpvar_16 = ((tmpvar_22 + (((unity_Lightmap_HDR).x * (color_24).xyz))));
tmpvar_2 = tmpvar_15;
half3 viewDir_25;
viewDir_25 = worldViewDir_8;
fixed4 c_26;
fixed4 c_27;
float nh_28;
fixed diff_29;
half tmpvar_30;
tmpvar_30 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_29 = tmpvar_30;
half tmpvar_31;
tmpvar_31 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_25)))));
nh_28 = tmpvar_31;
half y_32;
y_32 = ((_Shininess * 128.0));
float tmpvar_33;
tmpvar_33 = ((pow(nh_28, y_32) * (tmpvar_11).w));
(c_27).xyz = ((((((tmpvar_6 * tmpvar_15)) * diff_29)) + ((((tmpvar_15 * (_SpecColor).xyz)) * tmpvar_33))));
(c_27).w = (((tmpvar_11).w * (_Color).w));
(c_26).w = (c_27).w;
(c_26).xyz = (((c_27).xyz + ((tmpvar_6 * tmpvar_16))));
(c_4).xyz = (c_26).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
tmpvar_6 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_7;
tmpvar_7[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_7[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_7[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_8;
tmpvar_8 = normalize((mul(tmpvar_7, v._glesNormal)));
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_9[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_9[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul((v._glesTANGENT).xyz, tmpvar_9)));
worldTangent_3 = tmpvar_10;
float tmpvar_11;
tmpvar_11 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_11;
float3 tmpvar_12;
tmpvar_12 = (((((((tmpvar_8).yzx * (worldTangent_3).zxy)) - (((tmpvar_8).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_12;
float4 tmpvar_13;
(tmpvar_13).x = (worldTangent_3).x;
(tmpvar_13).y = (worldBinormal_1).x;
(tmpvar_13).z = (tmpvar_8).x;
(tmpvar_13).w = (tmpvar_6).x;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).y;
(tmpvar_14).y = (worldBinormal_1).y;
(tmpvar_14).z = (tmpvar_8).y;
(tmpvar_14).w = (tmpvar_6).y;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).z;
(tmpvar_15).y = (worldBinormal_1).z;
(tmpvar_15).z = (tmpvar_8).z;
(tmpvar_15).w = (tmpvar_6).z;
float2 tmpvar_16;
tmpvar_16 = (((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy));
(tmpvar_4).xy = ((tmpvar_16 + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_8;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = ((tmpvar_16 + (unity_LightmapST).zw));
o.xlv_TEXCOORD6 = (tmpvar_13).xyz;
o.xlv_TEXCOORD7 = (tmpvar_14).xyz;
o.xlv_TEXCOORD8 = (tmpvar_15).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 normalWorld_14;
normalWorld_14 = tmpvar_7;
fixed4 bakedDirTex_15;
half3 tmpvar_16;
half3 tmpvar_17;
tmpvar_16 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_18;
tmpvar_18 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_19;
color_19 = tmpvar_18;
half4 tmpvar_20;
tmpvar_20 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_15 = tmpvar_20;
fixed3 y_21;
y_21 = (((bakedDirTex_15).xyz - 0.5));
fixed tmpvar_22;
tmpvar_22 = max(0.0001, (bakedDirTex_15).w);
tmpvar_17 = (((((((unity_Lightmap_HDR).x * (color_19).xyz)) * ((dot(normalWorld_14, y_21) + 0.5)))) / tmpvar_22));
tmpvar_2 = tmpvar_16;
half3 viewDir_23;
viewDir_23 = worldViewDir_8;
fixed4 c_24;
fixed4 c_25;
float nh_26;
fixed diff_27;
half tmpvar_28;
tmpvar_28 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_27 = tmpvar_28;
half tmpvar_29;
tmpvar_29 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_23)))));
nh_26 = tmpvar_29;
half y_30;
y_30 = ((_Shininess * 128.0));
float tmpvar_31;
tmpvar_31 = ((pow(nh_26, y_30) * (tmpvar_11).w));
(c_25).xyz = ((((((tmpvar_6 * tmpvar_16)) * diff_27)) + ((((tmpvar_16 * (_SpecColor).xyz)) * tmpvar_31))));
(c_25).w = (((tmpvar_11).w * (_Color).w));
(c_24).w = (c_25).w;
(c_24).xyz = (((c_25).xyz + ((tmpvar_6 * tmpvar_17))));
(c_4).xyz = (c_24).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
tmpvar_6 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_7;
tmpvar_7[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_7[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_7[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_8;
tmpvar_8 = normalize((mul(tmpvar_7, v._glesNormal)));
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_9[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_9[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul((v._glesTANGENT).xyz, tmpvar_9)));
worldTangent_3 = tmpvar_10;
float tmpvar_11;
tmpvar_11 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_11;
float3 tmpvar_12;
tmpvar_12 = (((((((tmpvar_8).yzx * (worldTangent_3).zxy)) - (((tmpvar_8).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_12;
float4 tmpvar_13;
(tmpvar_13).x = (worldTangent_3).x;
(tmpvar_13).y = (worldBinormal_1).x;
(tmpvar_13).z = (tmpvar_8).x;
(tmpvar_13).w = (tmpvar_6).x;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).y;
(tmpvar_14).y = (worldBinormal_1).y;
(tmpvar_14).z = (tmpvar_8).y;
(tmpvar_14).w = (tmpvar_6).y;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).z;
(tmpvar_15).y = (worldBinormal_1).z;
(tmpvar_15).z = (tmpvar_8).z;
(tmpvar_15).w = (tmpvar_6).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
float2 tmpvar_16;
tmpvar_16 = (((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy));
(tmpvar_4).xy = ((tmpvar_16 + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_8;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = ((tmpvar_16 + (unity_LightmapST).zw));
o.xlv_TEXCOORD6 = (tmpvar_13).xyz;
o.xlv_TEXCOORD7 = (tmpvar_14).xyz;
o.xlv_TEXCOORD8 = (tmpvar_15).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 normalWorld_14;
normalWorld_14 = tmpvar_7;
half4 realtimeDirTex_15;
fixed4 bakedDirTex_16;
half3 tmpvar_17;
half3 tmpvar_18;
tmpvar_17 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_19;
tmpvar_19 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_20;
color_20 = tmpvar_19;
half4 tmpvar_21;
tmpvar_21 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_16 = tmpvar_21;
fixed3 y_22;
y_22 = (((bakedDirTex_16).xyz - 0.5));
fixed tmpvar_23;
tmpvar_23 = max(0.0001, (bakedDirTex_16).w);
tmpvar_18 = (((((((unity_Lightmap_HDR).x * (color_20).xyz)) * ((dot(normalWorld_14, y_22) + 0.5)))) / tmpvar_23));
fixed4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_25;
tmpvar_25 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_15 = tmpvar_25;
fixed4 dirTex_26;
dirTex_26 = realtimeDirTex_15;
fixed3 y_27;
y_27 = (((dirTex_26).xyz - 0.5));
fixed tmpvar_28;
tmpvar_28 = max(0.0001, (dirTex_26).w);
tmpvar_18 = ((tmpvar_18 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_24).w)) * (tmpvar_24).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_14, y_27) + 0.5)))) / tmpvar_28))));
tmpvar_2 = tmpvar_17;
half3 viewDir_29;
viewDir_29 = worldViewDir_8;
fixed4 c_30;
fixed4 c_31;
float nh_32;
fixed diff_33;
half tmpvar_34;
tmpvar_34 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_33 = tmpvar_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_29)))));
nh_32 = tmpvar_35;
half y_36;
y_36 = ((_Shininess * 128.0));
float tmpvar_37;
tmpvar_37 = ((pow(nh_32, y_36) * (tmpvar_11).w));
(c_31).xyz = ((((((tmpvar_6 * tmpvar_17)) * diff_33)) + ((((tmpvar_17 * (_SpecColor).xyz)) * tmpvar_37))));
(c_31).w = (((tmpvar_11).w * (_Color).w));
(c_30).w = (c_31).w;
(c_30).xyz = (((c_31).xyz + ((tmpvar_6 * tmpvar_18))));
(c_4).xyz = (c_30).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
tmpvar_6 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_7;
tmpvar_7[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_7[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_7[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_8;
tmpvar_8 = normalize((mul(tmpvar_7, v._glesNormal)));
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_9[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_9[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul((v._glesTANGENT).xyz, tmpvar_9)));
worldTangent_3 = tmpvar_10;
float tmpvar_11;
tmpvar_11 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_11;
float3 tmpvar_12;
tmpvar_12 = (((((((tmpvar_8).yzx * (worldTangent_3).zxy)) - (((tmpvar_8).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_12;
float4 tmpvar_13;
(tmpvar_13).x = (worldTangent_3).x;
(tmpvar_13).y = (worldBinormal_1).x;
(tmpvar_13).z = (tmpvar_8).x;
(tmpvar_13).w = (tmpvar_6).x;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).y;
(tmpvar_14).y = (worldBinormal_1).y;
(tmpvar_14).z = (tmpvar_8).y;
(tmpvar_14).w = (tmpvar_6).y;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).z;
(tmpvar_15).y = (worldBinormal_1).z;
(tmpvar_15).z = (tmpvar_8).z;
(tmpvar_15).w = (tmpvar_6).z;
float2 tmpvar_16;
tmpvar_16 = (((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy));
(tmpvar_4).xy = ((tmpvar_16 + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_8;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = ((tmpvar_16 + (unity_LightmapST).zw));
o.xlv_TEXCOORD6 = (tmpvar_13).xyz;
o.xlv_TEXCOORD7 = (tmpvar_14).xyz;
o.xlv_TEXCOORD8 = (tmpvar_15).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 normalWorld_14;
normalWorld_14 = tmpvar_7;
fixed4 bakedDirTex_15;
half3 tmpvar_16;
half3 tmpvar_17;
tmpvar_16 = ((tmpvar_2 * tmpvar_1));
half3 ambient_18;
half4 tmpvar_19;
(tmpvar_19).w = 1.0;
(tmpvar_19).xyz = normalWorld_14;
half3 x_20;
(x_20).x = dot(unity_SHAr, tmpvar_19);
(x_20).y = dot(unity_SHAg, tmpvar_19);
(x_20).z = dot(unity_SHAb, tmpvar_19);
half3 x1_21;
half4 tmpvar_22;
tmpvar_22 = (((normalWorld_14).xyzz * (normalWorld_14).yzzx));
(x1_21).x = dot(unity_SHBr, tmpvar_22);
(x1_21).y = dot(unity_SHBg, tmpvar_22);
(x1_21).z = dot(unity_SHBb, tmpvar_22);
ambient_18 = max(float3(0.0, 0.0, 0.0), ((x_20 + ((x1_21 + (((unity_SHC).xyz * (((((normalWorld_14).x * (normalWorld_14).x)) - (((normalWorld_14).y * (normalWorld_14).y)))))))))));
half3 tmpvar_23;
tmpvar_23 = max(((((1.055 * pow(max(ambient_18, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_18 = tmpvar_23;
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_25;
color_25 = tmpvar_24;
half4 tmpvar_26;
tmpvar_26 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_15 = tmpvar_26;
fixed3 y_27;
y_27 = (((bakedDirTex_15).xyz - 0.5));
fixed tmpvar_28;
tmpvar_28 = max(0.0001, (bakedDirTex_15).w);
tmpvar_17 = ((tmpvar_23 + (((((((unity_Lightmap_HDR).x * (color_25).xyz)) * ((dot(normalWorld_14, y_27) + 0.5)))) / tmpvar_28))));
tmpvar_2 = tmpvar_16;
half3 viewDir_29;
viewDir_29 = worldViewDir_8;
fixed4 c_30;
fixed4 c_31;
float nh_32;
fixed diff_33;
half tmpvar_34;
tmpvar_34 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_33 = tmpvar_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_29)))));
nh_32 = tmpvar_35;
half y_36;
y_36 = ((_Shininess * 128.0));
float tmpvar_37;
tmpvar_37 = ((pow(nh_32, y_36) * (tmpvar_11).w));
(c_31).xyz = ((((((tmpvar_6 * tmpvar_16)) * diff_33)) + ((((tmpvar_16 * (_SpecColor).xyz)) * tmpvar_37))));
(c_31).w = (((tmpvar_11).w * (_Color).w));
(c_30).w = (c_31).w;
(c_30).xyz = (((c_31).xyz + ((tmpvar_6 * tmpvar_17))));
(c_4).xyz = (c_30).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD4 : TEXCOORD3;
float4 xlv_TEXCOORD6 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = tmpvar_1;
o.xlv_TEXCOORD6 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half realtimeShadowAttenuation_12;
float4 v_13;
(v_13).x = (transpose(unity_MatrixV)[0]).z;
(v_13).y = (transpose(unity_MatrixV)[1]).z;
(v_13).z = (transpose(unity_MatrixV)[2]).z;
(v_13).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_14;
tmpvar_14 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_15;
float tmpvar_16;
tmpvar_16 = clamp(((((lerp(dot(tmpvar_10, (v_13).xyz), sqrt(dot(tmpvar_14, tmpvar_14)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_15 = tmpvar_16;
float4 tmpvar_17;
(tmpvar_17).w = 1.0;
(tmpvar_17).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_18;
float4 shadowCoord_19;
shadowCoord_19 = (mul(tmpvar_17, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_20;
half tmpvar_21;
tmpvar_21 = (_LightShadowData).x;
lightShadowDataX_20 = tmpvar_21;
float tmpvar_22;
tmpvar_22 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_19).xy)).x > (shadowCoord_19).z)))), lightShadowDataX_20);
tmpvar_18 = tmpvar_22;
realtimeShadowAttenuation_12 = tmpvar_18;
half tmpvar_23;
tmpvar_23 = clamp(((realtimeShadowAttenuation_12 + tmpvar_15)), 0.0, 1.0);
atten_5 = tmpvar_23;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 tmpvar_24;
tmpvar_24 = ((tmpvar_2 * tmpvar_1));
tmpvar_2 = tmpvar_24;
half3 viewDir_25;
viewDir_25 = tmpvar_9;
fixed4 c_26;
fixed4 c_27;
float nh_28;
fixed diff_29;
half tmpvar_30;
tmpvar_30 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_29 = tmpvar_30;
half tmpvar_31;
tmpvar_31 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_25)))));
nh_28 = tmpvar_31;
half y_32;
y_32 = ((_Shininess * 128.0));
float tmpvar_33;
tmpvar_33 = ((pow(nh_28, y_32) * (tmpvar_11).w));
(c_27).xyz = (((((((tmpvar_11).xyz * (_Color).xyz)) * ((tmpvar_24 * diff_29)))) + ((((tmpvar_24 * (_SpecColor).xyz)) * tmpvar_33))));
(c_27).w = (((tmpvar_11).w * (_Color).w));
(c_26).w = (c_27).w;
(c_26).xyz = (c_27).xyz;
(c_4).xyz = (c_26).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_5;
tmpvar_5 = normalize((mul(tmpvar_4, v._glesNormal)));
half3 normal_6;
normal_6 = tmpvar_5;
half3 x1_7;
half4 tmpvar_8;
tmpvar_8 = (((normal_6).xyzz * (normal_6).yzzx));
(x1_7).x = dot(unity_SHBr, tmpvar_8);
(x1_7).y = dot(unity_SHBg, tmpvar_8);
(x1_7).z = dot(unity_SHBb, tmpvar_8);
o.position = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_5;
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = ((x1_7 + (((unity_SHC).xyz * (((((normal_6).x * (normal_6).x)) - (((normal_6).y * (normal_6).y))))))));
o.xlv_TEXCOORD4 = tmpvar_1;
o.xlv_TEXCOORD6 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
float4 v_14;
(v_14).x = (transpose(unity_MatrixV)[0]).z;
(v_14).y = (transpose(unity_MatrixV)[1]).z;
(v_14).z = (transpose(unity_MatrixV)[2]).z;
(v_14).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_15;
tmpvar_15 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_16;
float tmpvar_17;
tmpvar_17 = clamp(((((lerp(dot(tmpvar_10, (v_14).xyz), sqrt(dot(tmpvar_15, tmpvar_15)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_16 = tmpvar_17;
float4 tmpvar_18;
(tmpvar_18).w = 1.0;
(tmpvar_18).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_19;
float4 shadowCoord_20;
shadowCoord_20 = (mul(tmpvar_18, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_21;
half tmpvar_22;
tmpvar_22 = (_LightShadowData).x;
lightShadowDataX_21 = tmpvar_22;
float tmpvar_23;
tmpvar_23 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_20).xy)).x > (shadowCoord_20).z)))), lightShadowDataX_21);
tmpvar_19 = tmpvar_23;
realtimeShadowAttenuation_13 = tmpvar_19;
half tmpvar_24;
tmpvar_24 = clamp(((realtimeShadowAttenuation_13 + tmpvar_16)), 0.0, 1.0);
atten_5 = tmpvar_24;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_25;
normalWorld_25 = tmpvar_6;
half3 tmpvar_26;
tmpvar_26 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_27;
(tmpvar_27).w = 1.0;
(tmpvar_27).xyz = normalWorld_25;
half3 x_28;
(x_28).x = dot(unity_SHAr, tmpvar_27);
(x_28).y = dot(unity_SHAg, tmpvar_27);
(x_28).z = dot(unity_SHAb, tmpvar_27);
half3 tmpvar_29;
tmpvar_29 = max(((((1.055 * pow(max(float3(0.0, 0.0, 0.0), ((i.xlv_TEXCOORD3 + x_28))), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
tmpvar_2 = tmpvar_26;
half3 viewDir_30;
viewDir_30 = tmpvar_9;
fixed4 c_31;
fixed4 c_32;
float nh_33;
fixed diff_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_34 = tmpvar_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_30)))));
nh_33 = tmpvar_36;
half y_37;
y_37 = ((_Shininess * 128.0));
float tmpvar_38;
tmpvar_38 = ((pow(nh_33, y_37) * (tmpvar_12).w));
(c_32).xyz = ((((((tmpvar_11 * tmpvar_26)) * diff_34)) + ((((tmpvar_26 * (_SpecColor).xyz)) * tmpvar_38))));
(c_32).w = (((tmpvar_12).w * (_Color).w));
(c_31).w = (c_32).w;
(c_31).xyz = (((c_32).xyz + ((tmpvar_11 * tmpvar_29))));
(c_4).xyz = (c_31).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD4 : TEXCOORD3;
float4 xlv_TEXCOORD6 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_2).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = tmpvar_1;
o.xlv_TEXCOORD6 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
float4 v_14;
(v_14).x = (transpose(unity_MatrixV)[0]).z;
(v_14).y = (transpose(unity_MatrixV)[1]).z;
(v_14).z = (transpose(unity_MatrixV)[2]).z;
(v_14).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_15;
tmpvar_15 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_16;
float tmpvar_17;
tmpvar_17 = clamp(((((lerp(dot(tmpvar_10, (v_14).xyz), sqrt(dot(tmpvar_15, tmpvar_15)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_16 = tmpvar_17;
float4 tmpvar_18;
(tmpvar_18).w = 1.0;
(tmpvar_18).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_19;
float4 shadowCoord_20;
shadowCoord_20 = (mul(tmpvar_18, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_21;
half tmpvar_22;
tmpvar_22 = (_LightShadowData).x;
lightShadowDataX_21 = tmpvar_22;
float tmpvar_23;
tmpvar_23 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_20).xy)).x > (shadowCoord_20).z)))), lightShadowDataX_21);
tmpvar_19 = tmpvar_23;
realtimeShadowAttenuation_13 = tmpvar_19;
half tmpvar_24;
tmpvar_24 = clamp(((realtimeShadowAttenuation_13 + tmpvar_16)), 0.0, 1.0);
atten_5 = tmpvar_24;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 tmpvar_25;
half3 tmpvar_26;
tmpvar_25 = ((tmpvar_2 * tmpvar_1));
fixed4 tmpvar_27;
tmpvar_27 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
tmpvar_26 = pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_27).w)) * (tmpvar_27).xyz)), (unity_DynamicLightmap_HDR).yyy);
tmpvar_2 = tmpvar_25;
half3 viewDir_28;
viewDir_28 = tmpvar_9;
fixed4 c_29;
fixed4 c_30;
float nh_31;
fixed diff_32;
half tmpvar_33;
tmpvar_33 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_32 = tmpvar_33;
half tmpvar_34;
tmpvar_34 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_28)))));
nh_31 = tmpvar_34;
half y_35;
y_35 = ((_Shininess * 128.0));
float tmpvar_36;
tmpvar_36 = ((pow(nh_31, y_35) * (tmpvar_12).w));
(c_30).xyz = ((((((tmpvar_11 * tmpvar_25)) * diff_32)) + ((((tmpvar_25 * (_SpecColor).xyz)) * tmpvar_36))));
(c_30).w = (((tmpvar_12).w * (_Color).w));
(c_29).w = (c_30).w;
(c_29).xyz = (((c_30).xyz + ((tmpvar_11 * tmpvar_26))));
(c_4).xyz = (c_29).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_4;
tmpvar_4 = normalize((mul(tmpvar_3, v._glesNormal)));
half3 normal_5;
normal_5 = tmpvar_4;
half3 x1_6;
half4 tmpvar_7;
tmpvar_7 = (((normal_5).xyzz * (normal_5).yzzx));
(x1_6).x = dot(unity_SHBr, tmpvar_7);
(x1_6).y = dot(unity_SHBg, tmpvar_7);
(x1_6).z = dot(unity_SHBb, tmpvar_7);
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_4;
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = ((x1_6 + (((unity_SHC).xyz * (((((normal_5).x * (normal_5).x)) - (((normal_5).y * (normal_5).y))))))));
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = lerp(realtimeShadowAttenuation_13, shadowMaskAttenuation_14, tmpvar_17);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_27;
normalWorld_27 = tmpvar_6;
half3 tmpvar_28;
tmpvar_28 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_29;
(tmpvar_29).w = 1.0;
(tmpvar_29).xyz = normalWorld_27;
half3 x_30;
(x_30).x = dot(unity_SHAr, tmpvar_29);
(x_30).y = dot(unity_SHAg, tmpvar_29);
(x_30).z = dot(unity_SHAb, tmpvar_29);
half3 tmpvar_31;
tmpvar_31 = max(((((1.055 * pow(max(float3(0.0, 0.0, 0.0), ((i.xlv_TEXCOORD3 + x_30))), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
tmpvar_2 = tmpvar_28;
half3 viewDir_32;
viewDir_32 = tmpvar_9;
fixed4 c_33;
fixed4 c_34;
float nh_35;
fixed diff_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_36 = tmpvar_37;
half tmpvar_38;
tmpvar_38 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_32)))));
nh_35 = tmpvar_38;
half y_39;
y_39 = ((_Shininess * 128.0));
float tmpvar_40;
tmpvar_40 = ((pow(nh_35, y_39) * (tmpvar_12).w));
(c_34).xyz = ((((((tmpvar_11 * tmpvar_28)) * diff_36)) + ((((tmpvar_28 * (_SpecColor).xyz)) * tmpvar_40))));
(c_34).w = (((tmpvar_12).w * (_Color).w));
(c_33).w = (c_34).w;
(c_33).xyz = (((c_34).xyz + ((tmpvar_11 * tmpvar_31))));
(c_4).xyz = (c_33).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float2 xlv_TEXCOORD4 : TEXCOORD3;
float4 xlv_TEXCOORD6 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = lerp(realtimeShadowAttenuation_13, shadowMaskAttenuation_14, tmpvar_17);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 tmpvar_27;
half3 tmpvar_28;
tmpvar_27 = ((tmpvar_2 * tmpvar_1));
fixed4 tmpvar_29;
tmpvar_29 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
tmpvar_28 = pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_29).w)) * (tmpvar_29).xyz)), (unity_DynamicLightmap_HDR).yyy);
tmpvar_2 = tmpvar_27;
half3 viewDir_30;
viewDir_30 = tmpvar_9;
fixed4 c_31;
fixed4 c_32;
float nh_33;
fixed diff_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_34 = tmpvar_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_30)))));
nh_33 = tmpvar_36;
half y_37;
y_37 = ((_Shininess * 128.0));
float tmpvar_38;
tmpvar_38 = ((pow(nh_33, y_37) * (tmpvar_12).w));
(c_32).xyz = ((((((tmpvar_11 * tmpvar_27)) * diff_34)) + ((((tmpvar_27 * (_SpecColor).xyz)) * tmpvar_38))));
(c_32).w = (((tmpvar_12).w * (_Color).w));
(c_31).w = (c_32).w;
(c_31).xyz = (((c_32).xyz + ((tmpvar_11 * tmpvar_28))));
(c_4).xyz = (c_31).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_4;
tmpvar_4 = normalize((mul(tmpvar_3, v._glesNormal)));
half3 normal_5;
normal_5 = tmpvar_4;
half3 x1_6;
half4 tmpvar_7;
tmpvar_7 = (((normal_5).xyzz * (normal_5).yzzx));
(x1_6).x = dot(unity_SHBr, tmpvar_7);
(x1_6).y = dot(unity_SHBg, tmpvar_7);
(x1_6).z = dot(unity_SHBb, tmpvar_7);
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_4;
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = ((x1_6 + (((unity_SHC).xyz * (((((normal_5).x * (normal_5).x)) - (((normal_5).y * (normal_5).y))))))));
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = min(clamp(((realtimeShadowAttenuation_13 + tmpvar_17)), 0.0, 1.0), shadowMaskAttenuation_14);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_27;
normalWorld_27 = tmpvar_6;
half3 tmpvar_28;
tmpvar_28 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_29;
(tmpvar_29).w = 1.0;
(tmpvar_29).xyz = normalWorld_27;
half3 x_30;
(x_30).x = dot(unity_SHAr, tmpvar_29);
(x_30).y = dot(unity_SHAg, tmpvar_29);
(x_30).z = dot(unity_SHAb, tmpvar_29);
half3 tmpvar_31;
tmpvar_31 = max(((((1.055 * pow(max(float3(0.0, 0.0, 0.0), ((i.xlv_TEXCOORD3 + x_30))), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
tmpvar_2 = tmpvar_28;
half3 viewDir_32;
viewDir_32 = tmpvar_9;
fixed4 c_33;
fixed4 c_34;
float nh_35;
fixed diff_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_36 = tmpvar_37;
half tmpvar_38;
tmpvar_38 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_32)))));
nh_35 = tmpvar_38;
half y_39;
y_39 = ((_Shininess * 128.0));
float tmpvar_40;
tmpvar_40 = ((pow(nh_35, y_39) * (tmpvar_12).w));
(c_34).xyz = ((((((tmpvar_11 * tmpvar_28)) * diff_36)) + ((((tmpvar_28 * (_SpecColor).xyz)) * tmpvar_40))));
(c_34).w = (((tmpvar_12).w * (_Color).w));
(c_33).w = (c_34).w;
(c_33).xyz = (((c_34).xyz + ((tmpvar_11 * tmpvar_31))));
(c_4).xyz = (c_33).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float2 xlv_TEXCOORD4 : TEXCOORD3;
float4 xlv_TEXCOORD6 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = min(clamp(((realtimeShadowAttenuation_13 + tmpvar_17)), 0.0, 1.0), shadowMaskAttenuation_14);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 tmpvar_27;
half3 tmpvar_28;
tmpvar_27 = ((tmpvar_2 * tmpvar_1));
fixed4 tmpvar_29;
tmpvar_29 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
tmpvar_28 = pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_29).w)) * (tmpvar_29).xyz)), (unity_DynamicLightmap_HDR).yyy);
tmpvar_2 = tmpvar_27;
half3 viewDir_30;
viewDir_30 = tmpvar_9;
fixed4 c_31;
fixed4 c_32;
float nh_33;
fixed diff_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_34 = tmpvar_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_30)))));
nh_33 = tmpvar_36;
half y_37;
y_37 = ((_Shininess * 128.0));
float tmpvar_38;
tmpvar_38 = ((pow(nh_33, y_37) * (tmpvar_12).w));
(c_32).xyz = ((((((tmpvar_11 * tmpvar_27)) * diff_34)) + ((((tmpvar_27 * (_SpecColor).xyz)) * tmpvar_38))));
(c_32).w = (((tmpvar_12).w * (_Color).w));
(c_31).w = (c_32).w;
(c_31).xyz = (((c_32).xyz + ((tmpvar_11 * tmpvar_28))));
(c_4).xyz = (c_31).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
float4 tmpvar_4;
tmpvar_4 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_4).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_4, transpose(unity_WorldToShadow[0])));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 tmpvar_16;
half3 tmpvar_17;
float4 v_18;
(v_18).x = (transpose(unity_MatrixV)[0]).z;
(v_18).y = (transpose(unity_MatrixV)[1]).z;
(v_18).z = (transpose(unity_MatrixV)[2]).z;
(v_18).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_19;
tmpvar_19 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_20;
float tmpvar_21;
tmpvar_21 = clamp(((((lerp(dot(tmpvar_9, (v_18).xyz), sqrt(dot(tmpvar_19, tmpvar_19)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_20 = tmpvar_21;
tmpvar_16 = ((tmpvar_2 * clamp(((tmpvar_1 + tmpvar_20)), 0.0, 1.0)));
half4 tmpvar_22;
tmpvar_22 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_23;
color_23 = tmpvar_22;
tmpvar_17 = (((unity_Lightmap_HDR).x * (color_23).xyz));
tmpvar_2 = tmpvar_16;
half3 viewDir_24;
viewDir_24 = tmpvar_8;
fixed4 c_25;
fixed4 c_26;
float nh_27;
fixed diff_28;
half tmpvar_29;
tmpvar_29 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_28 = tmpvar_29;
half tmpvar_30;
tmpvar_30 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_24)))));
nh_27 = tmpvar_30;
half y_31;
y_31 = ((_Shininess * 128.0));
float tmpvar_32;
tmpvar_32 = ((pow(nh_27, y_31) * (tmpvar_11).w));
(c_26).xyz = ((((((tmpvar_10 * tmpvar_16)) * diff_28)) + ((((tmpvar_16 * (_SpecColor).xyz)) * tmpvar_32))));
(c_26).w = (((tmpvar_11).w * (_Color).w));
(c_25).w = (c_26).w;
(c_25).xyz = (((c_26).xyz + ((tmpvar_10 * tmpvar_17))));
(c_4).xyz = (c_25).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
float4 tmpvar_4;
tmpvar_4 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_4).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_4, transpose(unity_WorldToShadow[0])));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 tmpvar_16;
half3 tmpvar_17;
float4 v_18;
(v_18).x = (transpose(unity_MatrixV)[0]).z;
(v_18).y = (transpose(unity_MatrixV)[1]).z;
(v_18).z = (transpose(unity_MatrixV)[2]).z;
(v_18).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_19;
tmpvar_19 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_20;
float tmpvar_21;
tmpvar_21 = clamp(((((lerp(dot(tmpvar_9, (v_18).xyz), sqrt(dot(tmpvar_19, tmpvar_19)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_20 = tmpvar_21;
tmpvar_16 = ((tmpvar_2 * clamp(((tmpvar_1 + tmpvar_20)), 0.0, 1.0)));
half4 tmpvar_22;
tmpvar_22 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_23;
color_23 = tmpvar_22;
tmpvar_17 = (((unity_Lightmap_HDR).x * (color_23).xyz));
fixed4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_17 = ((tmpvar_17 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_24).w)) * (tmpvar_24).xyz)), (unity_DynamicLightmap_HDR).yyy)));
tmpvar_2 = tmpvar_16;
half3 viewDir_25;
viewDir_25 = tmpvar_8;
fixed4 c_26;
fixed4 c_27;
float nh_28;
fixed diff_29;
half tmpvar_30;
tmpvar_30 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_29 = tmpvar_30;
half tmpvar_31;
tmpvar_31 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_25)))));
nh_28 = tmpvar_31;
half y_32;
y_32 = ((_Shininess * 128.0));
float tmpvar_33;
tmpvar_33 = ((pow(nh_28, y_32) * (tmpvar_11).w));
(c_27).xyz = ((((((tmpvar_10 * tmpvar_16)) * diff_29)) + ((((tmpvar_16 * (_SpecColor).xyz)) * tmpvar_33))));
(c_27).w = (((tmpvar_11).w * (_Color).w));
(c_26).w = (c_27).w;
(c_26).xyz = (((c_27).xyz + ((tmpvar_10 * tmpvar_17))));
(c_4).xyz = (c_26).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
float4 tmpvar_4;
tmpvar_4 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_4).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_4, transpose(unity_WorldToShadow[0])));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
half3 tmpvar_17;
half3 tmpvar_18;
float4 v_19;
(v_19).x = (transpose(unity_MatrixV)[0]).z;
(v_19).y = (transpose(unity_MatrixV)[1]).z;
(v_19).z = (transpose(unity_MatrixV)[2]).z;
(v_19).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_20;
tmpvar_20 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_21;
float tmpvar_22;
tmpvar_22 = clamp(((((lerp(dot(tmpvar_9, (v_19).xyz), sqrt(dot(tmpvar_20, tmpvar_20)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_21 = tmpvar_22;
tmpvar_17 = ((tmpvar_2 * clamp(((tmpvar_1 + tmpvar_21)), 0.0, 1.0)));
half3 ambient_23;
half4 tmpvar_24;
(tmpvar_24).w = 1.0;
(tmpvar_24).xyz = normalWorld_16;
half3 x_25;
(x_25).x = dot(unity_SHAr, tmpvar_24);
(x_25).y = dot(unity_SHAg, tmpvar_24);
(x_25).z = dot(unity_SHAb, tmpvar_24);
half3 x1_26;
half4 tmpvar_27;
tmpvar_27 = (((normalWorld_16).xyzz * (normalWorld_16).yzzx));
(x1_26).x = dot(unity_SHBr, tmpvar_27);
(x1_26).y = dot(unity_SHBg, tmpvar_27);
(x1_26).z = dot(unity_SHBb, tmpvar_27);
ambient_23 = max(float3(0.0, 0.0, 0.0), ((x_25 + ((x1_26 + (((unity_SHC).xyz * (((((normalWorld_16).x * (normalWorld_16).x)) - (((normalWorld_16).y * (normalWorld_16).y)))))))))));
half3 tmpvar_28;
tmpvar_28 = max(((((1.055 * pow(max(ambient_23, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_23 = tmpvar_28;
half4 tmpvar_29;
tmpvar_29 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_30;
color_30 = tmpvar_29;
tmpvar_18 = ((tmpvar_28 + (((unity_Lightmap_HDR).x * (color_30).xyz))));
tmpvar_2 = tmpvar_17;
half3 viewDir_31;
viewDir_31 = tmpvar_8;
fixed4 c_32;
fixed4 c_33;
float nh_34;
fixed diff_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_35 = tmpvar_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_31)))));
nh_34 = tmpvar_37;
half y_38;
y_38 = ((_Shininess * 128.0));
float tmpvar_39;
tmpvar_39 = ((pow(nh_34, y_38) * (tmpvar_11).w));
(c_33).xyz = ((((((tmpvar_10 * tmpvar_17)) * diff_35)) + ((((tmpvar_17 * (_SpecColor).xyz)) * tmpvar_39))));
(c_33).w = (((tmpvar_11).w * (_Color).w));
(c_32).w = (c_33).w;
(c_32).xyz = (((c_33).xyz + ((tmpvar_10 * tmpvar_18))));
(c_4).xyz = (c_32).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
float4 tmpvar_4;
tmpvar_4 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_4).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_4, transpose(unity_WorldToShadow[0])));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
fixed4 c_2;
fixed3 tmpvar_3;
tmpvar_3 = i.xlv_TEXCOORD1;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed tmpvar_5;
float lightShadowDataX_6;
half tmpvar_7;
tmpvar_7 = (_LightShadowData).x;
lightShadowDataX_6 = tmpvar_7;
float tmpvar_8;
tmpvar_8 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_6);
tmpvar_5 = tmpvar_8;
tmpvar_1 = tmpvar_5;
half3 normalWorld_9;
normalWorld_9 = tmpvar_3;
half3 tmpvar_10;
float4 v_11;
(v_11).x = (transpose(unity_MatrixV)[0]).z;
(v_11).y = (transpose(unity_MatrixV)[1]).z;
(v_11).z = (transpose(unity_MatrixV)[2]).z;
(v_11).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_12;
tmpvar_12 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_13;
float tmpvar_14;
tmpvar_14 = clamp(((((lerp(dot(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)), (v_11).xyz), sqrt(dot(tmpvar_12, tmpvar_12)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_13 = tmpvar_14;
half4 tmpvar_15;
tmpvar_15 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_16;
color_16 = tmpvar_15;
tmpvar_10 = (((unity_Lightmap_HDR).x * (color_16).xyz));
half3 shadowColor_17;
fixed3 tmpvar_18;
tmpvar_18 = (unity_ShadowColor).xyz;
shadowColor_17 = tmpvar_18;
tmpvar_10 = min(tmpvar_10, lerp(max(((tmpvar_10 - ((((max(0.0, dot(normalWorld_9, (_WorldSpaceLightPos0).xyz)) * ((1.0 - clamp(((tmpvar_1 + tmpvar_13)), 0.0, 1.0))))) * (_LightColor0).xyz)))), shadowColor_17), tmpvar_10, (_LightShadowData).xxx));
fixed4 c_19;
fixed4 c_20;
(c_20).xyz = float3(0.0, 0.0, 0.0);
(c_20).w = (((tmpvar_4).w * (_Color).w));
(c_19).w = (c_20).w;
(c_19).xyz = (((((tmpvar_4).xyz * (_Color).xyz)) * tmpvar_10));
(c_2).xyz = (c_19).xyz;
(c_2).w = 1.0;
o.target0 = c_2;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
float4 tmpvar_4;
tmpvar_4 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_4).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_4, transpose(unity_WorldToShadow[0])));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
fixed4 c_2;
fixed3 tmpvar_3;
tmpvar_3 = i.xlv_TEXCOORD1;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed tmpvar_5;
float lightShadowDataX_6;
half tmpvar_7;
tmpvar_7 = (_LightShadowData).x;
lightShadowDataX_6 = tmpvar_7;
float tmpvar_8;
tmpvar_8 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_6);
tmpvar_5 = tmpvar_8;
tmpvar_1 = tmpvar_5;
half3 normalWorld_9;
normalWorld_9 = tmpvar_3;
half3 tmpvar_10;
float4 v_11;
(v_11).x = (transpose(unity_MatrixV)[0]).z;
(v_11).y = (transpose(unity_MatrixV)[1]).z;
(v_11).z = (transpose(unity_MatrixV)[2]).z;
(v_11).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_12;
tmpvar_12 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_13;
float tmpvar_14;
tmpvar_14 = clamp(((((lerp(dot(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)), (v_11).xyz), sqrt(dot(tmpvar_12, tmpvar_12)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_13 = tmpvar_14;
half4 tmpvar_15;
tmpvar_15 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_16;
color_16 = tmpvar_15;
tmpvar_10 = (((unity_Lightmap_HDR).x * (color_16).xyz));
half3 shadowColor_17;
fixed3 tmpvar_18;
tmpvar_18 = (unity_ShadowColor).xyz;
shadowColor_17 = tmpvar_18;
tmpvar_10 = min(tmpvar_10, lerp(max(((tmpvar_10 - ((((max(0.0, dot(normalWorld_9, (_WorldSpaceLightPos0).xyz)) * ((1.0 - clamp(((tmpvar_1 + tmpvar_13)), 0.0, 1.0))))) * (_LightColor0).xyz)))), shadowColor_17), tmpvar_10, (_LightShadowData).xxx));
fixed4 tmpvar_19;
tmpvar_19 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_10 = ((tmpvar_10 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_19).w)) * (tmpvar_19).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_20;
fixed4 c_21;
(c_21).xyz = float3(0.0, 0.0, 0.0);
(c_21).w = (((tmpvar_4).w * (_Color).w));
(c_20).w = (c_21).w;
(c_20).xyz = (((((tmpvar_4).xyz * (_Color).xyz)) * tmpvar_10));
(c_2).xyz = (c_20).xyz;
(c_2).w = 1.0;
o.target0 = c_2;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
float4 tmpvar_4;
tmpvar_4 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_4).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_4, transpose(unity_WorldToShadow[0])));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
fixed4 c_2;
fixed3 tmpvar_3;
tmpvar_3 = i.xlv_TEXCOORD1;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed tmpvar_5;
float lightShadowDataX_6;
half tmpvar_7;
tmpvar_7 = (_LightShadowData).x;
lightShadowDataX_6 = tmpvar_7;
float tmpvar_8;
tmpvar_8 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_6);
tmpvar_5 = tmpvar_8;
tmpvar_1 = tmpvar_5;
half3 normalWorld_9;
normalWorld_9 = tmpvar_3;
half3 tmpvar_10;
float4 v_11;
(v_11).x = (transpose(unity_MatrixV)[0]).z;
(v_11).y = (transpose(unity_MatrixV)[1]).z;
(v_11).z = (transpose(unity_MatrixV)[2]).z;
(v_11).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_12;
tmpvar_12 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_13;
float tmpvar_14;
tmpvar_14 = clamp(((((lerp(dot(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)), (v_11).xyz), sqrt(dot(tmpvar_12, tmpvar_12)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_13 = tmpvar_14;
half3 ambient_15;
half4 tmpvar_16;
(tmpvar_16).w = 1.0;
(tmpvar_16).xyz = normalWorld_9;
half3 x_17;
(x_17).x = dot(unity_SHAr, tmpvar_16);
(x_17).y = dot(unity_SHAg, tmpvar_16);
(x_17).z = dot(unity_SHAb, tmpvar_16);
half3 x1_18;
half4 tmpvar_19;
tmpvar_19 = (((normalWorld_9).xyzz * (normalWorld_9).yzzx));
(x1_18).x = dot(unity_SHBr, tmpvar_19);
(x1_18).y = dot(unity_SHBg, tmpvar_19);
(x1_18).z = dot(unity_SHBb, tmpvar_19);
ambient_15 = max(float3(0.0, 0.0, 0.0), ((x_17 + ((x1_18 + (((unity_SHC).xyz * (((((normalWorld_9).x * (normalWorld_9).x)) - (((normalWorld_9).y * (normalWorld_9).y)))))))))));
half3 tmpvar_20;
tmpvar_20 = max(((((1.055 * pow(max(ambient_15, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_15 = tmpvar_20;
half4 tmpvar_21;
tmpvar_21 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_22;
color_22 = tmpvar_21;
tmpvar_10 = ((tmpvar_20 + (((unity_Lightmap_HDR).x * (color_22).xyz))));
half3 shadowColor_23;
fixed3 tmpvar_24;
tmpvar_24 = (unity_ShadowColor).xyz;
shadowColor_23 = tmpvar_24;
tmpvar_10 = min(tmpvar_10, lerp(max(((tmpvar_10 - ((((max(0.0, dot(normalWorld_9, (_WorldSpaceLightPos0).xyz)) * ((1.0 - clamp(((tmpvar_1 + tmpvar_13)), 0.0, 1.0))))) * (_LightColor0).xyz)))), shadowColor_23), tmpvar_10, (_LightShadowData).xxx));
fixed4 c_25;
fixed4 c_26;
(c_26).xyz = float3(0.0, 0.0, 0.0);
(c_26).w = (((tmpvar_4).w * (_Color).w));
(c_25).w = (c_26).w;
(c_25).xyz = (((((tmpvar_4).xyz * (_Color).xyz)) * tmpvar_10));
(c_2).xyz = (c_25).xyz;
(c_2).w = 1.0;
o.target0 = c_2;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
float4 tmpvar_4;
tmpvar_4 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_4).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_4, transpose(unity_WorldToShadow[0])));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half bakedAtten_16;
half3 tmpvar_17;
half3 tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_16 = tmpvar_19;
float4 v_20;
(v_20).x = (transpose(unity_MatrixV)[0]).z;
(v_20).y = (transpose(unity_MatrixV)[1]).z;
(v_20).z = (transpose(unity_MatrixV)[2]).z;
(v_20).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_21;
tmpvar_21 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_22;
float tmpvar_23;
tmpvar_23 = clamp(((((lerp(dot(tmpvar_9, (v_20).xyz), sqrt(dot(tmpvar_21, tmpvar_21)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_22 = tmpvar_23;
tmpvar_17 = ((tmpvar_2 * lerp(tmpvar_1, bakedAtten_16, tmpvar_22)));
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_25;
color_25 = tmpvar_24;
tmpvar_18 = (((unity_Lightmap_HDR).x * (color_25).xyz));
tmpvar_2 = tmpvar_17;
half3 viewDir_26;
viewDir_26 = tmpvar_8;
fixed4 c_27;
fixed4 c_28;
float nh_29;
fixed diff_30;
half tmpvar_31;
tmpvar_31 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_30 = tmpvar_31;
half tmpvar_32;
tmpvar_32 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_26)))));
nh_29 = tmpvar_32;
half y_33;
y_33 = ((_Shininess * 128.0));
float tmpvar_34;
tmpvar_34 = ((pow(nh_29, y_33) * (tmpvar_11).w));
(c_28).xyz = ((((((tmpvar_10 * tmpvar_17)) * diff_30)) + ((((tmpvar_17 * (_SpecColor).xyz)) * tmpvar_34))));
(c_28).w = (((tmpvar_11).w * (_Color).w));
(c_27).w = (c_28).w;
(c_27).xyz = (((c_28).xyz + ((tmpvar_10 * tmpvar_18))));
(c_4).xyz = (c_27).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
float4 tmpvar_4;
tmpvar_4 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_4).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_4, transpose(unity_WorldToShadow[0])));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half bakedAtten_16;
half3 tmpvar_17;
half3 tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_16 = tmpvar_19;
float4 v_20;
(v_20).x = (transpose(unity_MatrixV)[0]).z;
(v_20).y = (transpose(unity_MatrixV)[1]).z;
(v_20).z = (transpose(unity_MatrixV)[2]).z;
(v_20).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_21;
tmpvar_21 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_22;
float tmpvar_23;
tmpvar_23 = clamp(((((lerp(dot(tmpvar_9, (v_20).xyz), sqrt(dot(tmpvar_21, tmpvar_21)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_22 = tmpvar_23;
tmpvar_17 = ((tmpvar_2 * lerp(tmpvar_1, bakedAtten_16, tmpvar_22)));
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_25;
color_25 = tmpvar_24;
tmpvar_18 = (((unity_Lightmap_HDR).x * (color_25).xyz));
fixed4 tmpvar_26;
tmpvar_26 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_18 = ((tmpvar_18 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_26).w)) * (tmpvar_26).xyz)), (unity_DynamicLightmap_HDR).yyy)));
tmpvar_2 = tmpvar_17;
half3 viewDir_27;
viewDir_27 = tmpvar_8;
fixed4 c_28;
fixed4 c_29;
float nh_30;
fixed diff_31;
half tmpvar_32;
tmpvar_32 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_31 = tmpvar_32;
half tmpvar_33;
tmpvar_33 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_27)))));
nh_30 = tmpvar_33;
half y_34;
y_34 = ((_Shininess * 128.0));
float tmpvar_35;
tmpvar_35 = ((pow(nh_30, y_34) * (tmpvar_11).w));
(c_29).xyz = ((((((tmpvar_10 * tmpvar_17)) * diff_31)) + ((((tmpvar_17 * (_SpecColor).xyz)) * tmpvar_35))));
(c_29).w = (((tmpvar_11).w * (_Color).w));
(c_28).w = (c_29).w;
(c_28).xyz = (((c_29).xyz + ((tmpvar_10 * tmpvar_18))));
(c_4).xyz = (c_28).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
float4 tmpvar_4;
tmpvar_4 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_4).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_4, transpose(unity_WorldToShadow[0])));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
half bakedAtten_17;
half3 tmpvar_18;
half3 tmpvar_19;
fixed tmpvar_20;
tmpvar_20 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_17 = tmpvar_20;
float4 v_21;
(v_21).x = (transpose(unity_MatrixV)[0]).z;
(v_21).y = (transpose(unity_MatrixV)[1]).z;
(v_21).z = (transpose(unity_MatrixV)[2]).z;
(v_21).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_22;
tmpvar_22 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_23;
float tmpvar_24;
tmpvar_24 = clamp(((((lerp(dot(tmpvar_9, (v_21).xyz), sqrt(dot(tmpvar_22, tmpvar_22)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_23 = tmpvar_24;
tmpvar_18 = ((tmpvar_2 * lerp(tmpvar_1, bakedAtten_17, tmpvar_23)));
half3 ambient_25;
half4 tmpvar_26;
(tmpvar_26).w = 1.0;
(tmpvar_26).xyz = normalWorld_16;
half3 x_27;
(x_27).x = dot(unity_SHAr, tmpvar_26);
(x_27).y = dot(unity_SHAg, tmpvar_26);
(x_27).z = dot(unity_SHAb, tmpvar_26);
half3 x1_28;
half4 tmpvar_29;
tmpvar_29 = (((normalWorld_16).xyzz * (normalWorld_16).yzzx));
(x1_28).x = dot(unity_SHBr, tmpvar_29);
(x1_28).y = dot(unity_SHBg, tmpvar_29);
(x1_28).z = dot(unity_SHBb, tmpvar_29);
ambient_25 = max(float3(0.0, 0.0, 0.0), ((x_27 + ((x1_28 + (((unity_SHC).xyz * (((((normalWorld_16).x * (normalWorld_16).x)) - (((normalWorld_16).y * (normalWorld_16).y)))))))))));
half3 tmpvar_30;
tmpvar_30 = max(((((1.055 * pow(max(ambient_25, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_25 = tmpvar_30;
half4 tmpvar_31;
tmpvar_31 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_32;
color_32 = tmpvar_31;
tmpvar_19 = ((tmpvar_30 + (((unity_Lightmap_HDR).x * (color_32).xyz))));
tmpvar_2 = tmpvar_18;
half3 viewDir_33;
viewDir_33 = tmpvar_8;
fixed4 c_34;
fixed4 c_35;
float nh_36;
fixed diff_37;
half tmpvar_38;
tmpvar_38 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_37 = tmpvar_38;
half tmpvar_39;
tmpvar_39 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_33)))));
nh_36 = tmpvar_39;
half y_40;
y_40 = ((_Shininess * 128.0));
float tmpvar_41;
tmpvar_41 = ((pow(nh_36, y_40) * (tmpvar_11).w));
(c_35).xyz = ((((((tmpvar_10 * tmpvar_18)) * diff_37)) + ((((tmpvar_18 * (_SpecColor).xyz)) * tmpvar_41))));
(c_35).w = (((tmpvar_11).w * (_Color).w));
(c_34).w = (c_35).w;
(c_34).xyz = (((c_35).xyz + ((tmpvar_10 * tmpvar_19))));
(c_4).xyz = (c_34).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
float4 tmpvar_4;
tmpvar_4 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_4).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_4, transpose(unity_WorldToShadow[0])));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half bakedAtten_16;
half3 tmpvar_17;
half3 tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_16 = tmpvar_19;
float4 v_20;
(v_20).x = (transpose(unity_MatrixV)[0]).z;
(v_20).y = (transpose(unity_MatrixV)[1]).z;
(v_20).z = (transpose(unity_MatrixV)[2]).z;
(v_20).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_21;
tmpvar_21 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_22;
float tmpvar_23;
tmpvar_23 = clamp(((((lerp(dot(tmpvar_9, (v_20).xyz), sqrt(dot(tmpvar_21, tmpvar_21)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_22 = tmpvar_23;
tmpvar_17 = ((tmpvar_2 * min(clamp(((tmpvar_1 + tmpvar_22)), 0.0, 1.0), bakedAtten_16)));
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_25;
color_25 = tmpvar_24;
tmpvar_18 = (((unity_Lightmap_HDR).x * (color_25).xyz));
tmpvar_2 = tmpvar_17;
half3 viewDir_26;
viewDir_26 = tmpvar_8;
fixed4 c_27;
fixed4 c_28;
float nh_29;
fixed diff_30;
half tmpvar_31;
tmpvar_31 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_30 = tmpvar_31;
half tmpvar_32;
tmpvar_32 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_26)))));
nh_29 = tmpvar_32;
half y_33;
y_33 = ((_Shininess * 128.0));
float tmpvar_34;
tmpvar_34 = ((pow(nh_29, y_33) * (tmpvar_11).w));
(c_28).xyz = ((((((tmpvar_10 * tmpvar_17)) * diff_30)) + ((((tmpvar_17 * (_SpecColor).xyz)) * tmpvar_34))));
(c_28).w = (((tmpvar_11).w * (_Color).w));
(c_27).w = (c_28).w;
(c_27).xyz = (((c_28).xyz + ((tmpvar_10 * tmpvar_18))));
(c_4).xyz = (c_27).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
float4 tmpvar_4;
tmpvar_4 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_4).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_4, transpose(unity_WorldToShadow[0])));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half bakedAtten_16;
half3 tmpvar_17;
half3 tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_16 = tmpvar_19;
float4 v_20;
(v_20).x = (transpose(unity_MatrixV)[0]).z;
(v_20).y = (transpose(unity_MatrixV)[1]).z;
(v_20).z = (transpose(unity_MatrixV)[2]).z;
(v_20).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_21;
tmpvar_21 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_22;
float tmpvar_23;
tmpvar_23 = clamp(((((lerp(dot(tmpvar_9, (v_20).xyz), sqrt(dot(tmpvar_21, tmpvar_21)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_22 = tmpvar_23;
tmpvar_17 = ((tmpvar_2 * min(clamp(((tmpvar_1 + tmpvar_22)), 0.0, 1.0), bakedAtten_16)));
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_25;
color_25 = tmpvar_24;
tmpvar_18 = (((unity_Lightmap_HDR).x * (color_25).xyz));
fixed4 tmpvar_26;
tmpvar_26 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_18 = ((tmpvar_18 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_26).w)) * (tmpvar_26).xyz)), (unity_DynamicLightmap_HDR).yyy)));
tmpvar_2 = tmpvar_17;
half3 viewDir_27;
viewDir_27 = tmpvar_8;
fixed4 c_28;
fixed4 c_29;
float nh_30;
fixed diff_31;
half tmpvar_32;
tmpvar_32 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_31 = tmpvar_32;
half tmpvar_33;
tmpvar_33 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_27)))));
nh_30 = tmpvar_33;
half y_34;
y_34 = ((_Shininess * 128.0));
float tmpvar_35;
tmpvar_35 = ((pow(nh_30, y_34) * (tmpvar_11).w));
(c_29).xyz = ((((((tmpvar_10 * tmpvar_17)) * diff_31)) + ((((tmpvar_17 * (_SpecColor).xyz)) * tmpvar_35))));
(c_29).w = (((tmpvar_11).w * (_Color).w));
(c_28).w = (c_29).w;
(c_28).xyz = (((c_29).xyz + ((tmpvar_10 * tmpvar_18))));
(c_4).xyz = (c_28).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
float4 tmpvar_4;
tmpvar_4 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_4).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_4, transpose(unity_WorldToShadow[0])));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
half bakedAtten_17;
half3 tmpvar_18;
half3 tmpvar_19;
fixed tmpvar_20;
tmpvar_20 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_17 = tmpvar_20;
float4 v_21;
(v_21).x = (transpose(unity_MatrixV)[0]).z;
(v_21).y = (transpose(unity_MatrixV)[1]).z;
(v_21).z = (transpose(unity_MatrixV)[2]).z;
(v_21).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_22;
tmpvar_22 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_23;
float tmpvar_24;
tmpvar_24 = clamp(((((lerp(dot(tmpvar_9, (v_21).xyz), sqrt(dot(tmpvar_22, tmpvar_22)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_23 = tmpvar_24;
tmpvar_18 = ((tmpvar_2 * min(clamp(((tmpvar_1 + tmpvar_23)), 0.0, 1.0), bakedAtten_17)));
half3 ambient_25;
half4 tmpvar_26;
(tmpvar_26).w = 1.0;
(tmpvar_26).xyz = normalWorld_16;
half3 x_27;
(x_27).x = dot(unity_SHAr, tmpvar_26);
(x_27).y = dot(unity_SHAg, tmpvar_26);
(x_27).z = dot(unity_SHAb, tmpvar_26);
half3 x1_28;
half4 tmpvar_29;
tmpvar_29 = (((normalWorld_16).xyzz * (normalWorld_16).yzzx));
(x1_28).x = dot(unity_SHBr, tmpvar_29);
(x1_28).y = dot(unity_SHBg, tmpvar_29);
(x1_28).z = dot(unity_SHBb, tmpvar_29);
ambient_25 = max(float3(0.0, 0.0, 0.0), ((x_27 + ((x1_28 + (((unity_SHC).xyz * (((((normalWorld_16).x * (normalWorld_16).x)) - (((normalWorld_16).y * (normalWorld_16).y)))))))))));
half3 tmpvar_30;
tmpvar_30 = max(((((1.055 * pow(max(ambient_25, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_25 = tmpvar_30;
half4 tmpvar_31;
tmpvar_31 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_32;
color_32 = tmpvar_31;
tmpvar_19 = ((tmpvar_30 + (((unity_Lightmap_HDR).x * (color_32).xyz))));
tmpvar_2 = tmpvar_18;
half3 viewDir_33;
viewDir_33 = tmpvar_8;
fixed4 c_34;
fixed4 c_35;
float nh_36;
fixed diff_37;
half tmpvar_38;
tmpvar_38 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_37 = tmpvar_38;
half tmpvar_39;
tmpvar_39 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_33)))));
nh_36 = tmpvar_39;
half y_40;
y_40 = ((_Shininess * 128.0));
float tmpvar_41;
tmpvar_41 = ((pow(nh_36, y_40) * (tmpvar_11).w));
(c_35).xyz = ((((((tmpvar_10 * tmpvar_18)) * diff_37)) + ((((tmpvar_18 * (_SpecColor).xyz)) * tmpvar_41))));
(c_35).w = (((tmpvar_11).w * (_Color).w));
(c_34).w = (c_35).w;
(c_34).xyz = (((c_35).xyz + ((tmpvar_10 * tmpvar_19))));
(c_4).xyz = (c_34).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD4 : TEXCOORD3;
float4 xlv_TEXCOORD6 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_2).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = tmpvar_1;
o.xlv_TEXCOORD6 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
float4 v_14;
(v_14).x = (transpose(unity_MatrixV)[0]).z;
(v_14).y = (transpose(unity_MatrixV)[1]).z;
(v_14).z = (transpose(unity_MatrixV)[2]).z;
(v_14).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_15;
tmpvar_15 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_16;
float tmpvar_17;
tmpvar_17 = clamp(((((lerp(dot(tmpvar_10, (v_14).xyz), sqrt(dot(tmpvar_15, tmpvar_15)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_16 = tmpvar_17;
float4 tmpvar_18;
(tmpvar_18).w = 1.0;
(tmpvar_18).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_19;
float4 shadowCoord_20;
shadowCoord_20 = (mul(tmpvar_18, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_21;
half tmpvar_22;
tmpvar_22 = (_LightShadowData).x;
lightShadowDataX_21 = tmpvar_22;
float tmpvar_23;
tmpvar_23 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_20).xy)).x > (shadowCoord_20).z)))), lightShadowDataX_21);
tmpvar_19 = tmpvar_23;
realtimeShadowAttenuation_13 = tmpvar_19;
half tmpvar_24;
tmpvar_24 = clamp(((realtimeShadowAttenuation_13 + tmpvar_16)), 0.0, 1.0);
atten_5 = tmpvar_24;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_25;
normalWorld_25 = tmpvar_6;
half4 realtimeDirTex_26;
half3 tmpvar_27;
half3 tmpvar_28;
tmpvar_27 = ((tmpvar_2 * tmpvar_1));
fixed4 tmpvar_29;
tmpvar_29 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
fixed4 tmpvar_30;
tmpvar_30 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
realtimeDirTex_26 = tmpvar_30;
fixed4 dirTex_31;
dirTex_31 = realtimeDirTex_26;
fixed3 y_32;
y_32 = (((dirTex_31).xyz - 0.5));
fixed tmpvar_33;
tmpvar_33 = max(0.0001, (dirTex_31).w);
tmpvar_28 = ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_29).w)) * (tmpvar_29).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_25, y_32) + 0.5)))) / tmpvar_33));
tmpvar_2 = tmpvar_27;
half3 viewDir_34;
viewDir_34 = tmpvar_9;
fixed4 c_35;
fixed4 c_36;
float nh_37;
fixed diff_38;
half tmpvar_39;
tmpvar_39 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_38 = tmpvar_39;
half tmpvar_40;
tmpvar_40 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_34)))));
nh_37 = tmpvar_40;
half y_41;
y_41 = ((_Shininess * 128.0));
float tmpvar_42;
tmpvar_42 = ((pow(nh_37, y_41) * (tmpvar_12).w));
(c_36).xyz = ((((((tmpvar_11 * tmpvar_27)) * diff_38)) + ((((tmpvar_27 * (_SpecColor).xyz)) * tmpvar_42))));
(c_36).w = (((tmpvar_12).w * (_Color).w));
(c_35).w = (c_36).w;
(c_35).xyz = (((c_36).xyz + ((tmpvar_11 * tmpvar_28))));
(c_4).xyz = (c_35).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
float4 tmpvar_7;
tmpvar_7 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_6 = (tmpvar_7).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_6).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_6).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_6).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_7, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
fixed4 bakedDirTex_17;
half3 tmpvar_18;
half3 tmpvar_19;
float4 v_20;
(v_20).x = (transpose(unity_MatrixV)[0]).z;
(v_20).y = (transpose(unity_MatrixV)[1]).z;
(v_20).z = (transpose(unity_MatrixV)[2]).z;
(v_20).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_21;
tmpvar_21 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_22;
float tmpvar_23;
tmpvar_23 = clamp(((((lerp(dot(tmpvar_9, (v_20).xyz), sqrt(dot(tmpvar_21, tmpvar_21)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_22 = tmpvar_23;
tmpvar_18 = ((tmpvar_2 * clamp(((tmpvar_1 + tmpvar_22)), 0.0, 1.0)));
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_25;
color_25 = tmpvar_24;
half4 tmpvar_26;
tmpvar_26 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_17 = tmpvar_26;
fixed3 y_27;
y_27 = (((bakedDirTex_17).xyz - 0.5));
fixed tmpvar_28;
tmpvar_28 = max(0.0001, (bakedDirTex_17).w);
tmpvar_19 = (((((((unity_Lightmap_HDR).x * (color_25).xyz)) * ((dot(normalWorld_16, y_27) + 0.5)))) / tmpvar_28));
tmpvar_2 = tmpvar_18;
half3 viewDir_29;
viewDir_29 = tmpvar_8;
fixed4 c_30;
fixed4 c_31;
float nh_32;
fixed diff_33;
half tmpvar_34;
tmpvar_34 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_33 = tmpvar_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_29)))));
nh_32 = tmpvar_35;
half y_36;
y_36 = ((_Shininess * 128.0));
float tmpvar_37;
tmpvar_37 = ((pow(nh_32, y_36) * (tmpvar_11).w));
(c_31).xyz = ((((((tmpvar_10 * tmpvar_18)) * diff_33)) + ((((tmpvar_18 * (_SpecColor).xyz)) * tmpvar_37))));
(c_31).w = (((tmpvar_11).w * (_Color).w));
(c_30).w = (c_31).w;
(c_30).xyz = (((c_31).xyz + ((tmpvar_10 * tmpvar_19))));
(c_4).xyz = (c_30).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
float4 tmpvar_7;
tmpvar_7 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_6 = (tmpvar_7).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_6).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_6).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_6).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_7, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
fixed4 bakedDirTex_17;
half3 tmpvar_18;
half3 tmpvar_19;
float4 v_20;
(v_20).x = (transpose(unity_MatrixV)[0]).z;
(v_20).y = (transpose(unity_MatrixV)[1]).z;
(v_20).z = (transpose(unity_MatrixV)[2]).z;
(v_20).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_21;
tmpvar_21 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_22;
float tmpvar_23;
tmpvar_23 = clamp(((((lerp(dot(tmpvar_9, (v_20).xyz), sqrt(dot(tmpvar_21, tmpvar_21)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_22 = tmpvar_23;
tmpvar_18 = ((tmpvar_2 * clamp(((tmpvar_1 + tmpvar_22)), 0.0, 1.0)));
half3 ambient_24;
half4 tmpvar_25;
(tmpvar_25).w = 1.0;
(tmpvar_25).xyz = normalWorld_16;
half3 x_26;
(x_26).x = dot(unity_SHAr, tmpvar_25);
(x_26).y = dot(unity_SHAg, tmpvar_25);
(x_26).z = dot(unity_SHAb, tmpvar_25);
half3 x1_27;
half4 tmpvar_28;
tmpvar_28 = (((normalWorld_16).xyzz * (normalWorld_16).yzzx));
(x1_27).x = dot(unity_SHBr, tmpvar_28);
(x1_27).y = dot(unity_SHBg, tmpvar_28);
(x1_27).z = dot(unity_SHBb, tmpvar_28);
ambient_24 = max(float3(0.0, 0.0, 0.0), ((x_26 + ((x1_27 + (((unity_SHC).xyz * (((((normalWorld_16).x * (normalWorld_16).x)) - (((normalWorld_16).y * (normalWorld_16).y)))))))))));
half3 tmpvar_29;
tmpvar_29 = max(((((1.055 * pow(max(ambient_24, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_24 = tmpvar_29;
half4 tmpvar_30;
tmpvar_30 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_31;
color_31 = tmpvar_30;
half4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_17 = tmpvar_32;
fixed3 y_33;
y_33 = (((bakedDirTex_17).xyz - 0.5));
fixed tmpvar_34;
tmpvar_34 = max(0.0001, (bakedDirTex_17).w);
tmpvar_19 = ((tmpvar_29 + (((((((unity_Lightmap_HDR).x * (color_31).xyz)) * ((dot(normalWorld_16, y_33) + 0.5)))) / tmpvar_34))));
tmpvar_2 = tmpvar_18;
half3 viewDir_35;
viewDir_35 = tmpvar_8;
fixed4 c_36;
fixed4 c_37;
float nh_38;
fixed diff_39;
half tmpvar_40;
tmpvar_40 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_39 = tmpvar_40;
half tmpvar_41;
tmpvar_41 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_35)))));
nh_38 = tmpvar_41;
half y_42;
y_42 = ((_Shininess * 128.0));
float tmpvar_43;
tmpvar_43 = ((pow(nh_38, y_42) * (tmpvar_11).w));
(c_37).xyz = ((((((tmpvar_10 * tmpvar_18)) * diff_39)) + ((((tmpvar_18 * (_SpecColor).xyz)) * tmpvar_43))));
(c_37).w = (((tmpvar_11).w * (_Color).w));
(c_36).w = (c_37).w;
(c_36).xyz = (((c_37).xyz + ((tmpvar_10 * tmpvar_19))));
(c_4).xyz = (c_36).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
float4 tmpvar_7;
tmpvar_7 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_6 = (tmpvar_7).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_6).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_6).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_6).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_7, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
half4 realtimeDirTex_17;
fixed4 bakedDirTex_18;
half3 tmpvar_19;
half3 tmpvar_20;
float4 v_21;
(v_21).x = (transpose(unity_MatrixV)[0]).z;
(v_21).y = (transpose(unity_MatrixV)[1]).z;
(v_21).z = (transpose(unity_MatrixV)[2]).z;
(v_21).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_22;
tmpvar_22 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_23;
float tmpvar_24;
tmpvar_24 = clamp(((((lerp(dot(tmpvar_9, (v_21).xyz), sqrt(dot(tmpvar_22, tmpvar_22)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_23 = tmpvar_24;
tmpvar_19 = ((tmpvar_2 * clamp(((tmpvar_1 + tmpvar_23)), 0.0, 1.0)));
half4 tmpvar_25;
tmpvar_25 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_26;
color_26 = tmpvar_25;
half4 tmpvar_27;
tmpvar_27 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_18 = tmpvar_27;
fixed3 y_28;
y_28 = (((bakedDirTex_18).xyz - 0.5));
fixed tmpvar_29;
tmpvar_29 = max(0.0001, (bakedDirTex_18).w);
tmpvar_20 = (((((((unity_Lightmap_HDR).x * (color_26).xyz)) * ((dot(normalWorld_16, y_28) + 0.5)))) / tmpvar_29));
fixed4 tmpvar_30;
tmpvar_30 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_31;
tmpvar_31 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_17 = tmpvar_31;
fixed4 dirTex_32;
dirTex_32 = realtimeDirTex_17;
fixed3 y_33;
y_33 = (((dirTex_32).xyz - 0.5));
fixed tmpvar_34;
tmpvar_34 = max(0.0001, (dirTex_32).w);
tmpvar_20 = ((tmpvar_20 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_30).w)) * (tmpvar_30).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_16, y_33) + 0.5)))) / tmpvar_34))));
tmpvar_2 = tmpvar_19;
half3 viewDir_35;
viewDir_35 = tmpvar_8;
fixed4 c_36;
fixed4 c_37;
float nh_38;
fixed diff_39;
half tmpvar_40;
tmpvar_40 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_39 = tmpvar_40;
half tmpvar_41;
tmpvar_41 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_35)))));
nh_38 = tmpvar_41;
half y_42;
y_42 = ((_Shininess * 128.0));
float tmpvar_43;
tmpvar_43 = ((pow(nh_38, y_42) * (tmpvar_11).w));
(c_37).xyz = ((((((tmpvar_10 * tmpvar_19)) * diff_39)) + ((((tmpvar_19 * (_SpecColor).xyz)) * tmpvar_43))));
(c_37).w = (((tmpvar_11).w * (_Color).w));
(c_36).w = (c_37).w;
(c_36).xyz = (((c_37).xyz + ((tmpvar_10 * tmpvar_20))));
(c_4).xyz = (c_36).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
float4 tmpvar_7;
tmpvar_7 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_6 = (tmpvar_7).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_6).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_6).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_6).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_7, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
fixed4 c_2;
fixed3 tmpvar_3;
tmpvar_3 = i.xlv_TEXCOORD1;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed tmpvar_5;
float lightShadowDataX_6;
half tmpvar_7;
tmpvar_7 = (_LightShadowData).x;
lightShadowDataX_6 = tmpvar_7;
float tmpvar_8;
tmpvar_8 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_6);
tmpvar_5 = tmpvar_8;
tmpvar_1 = tmpvar_5;
half3 normalWorld_9;
normalWorld_9 = tmpvar_3;
fixed4 bakedDirTex_10;
half3 tmpvar_11;
float4 v_12;
(v_12).x = (transpose(unity_MatrixV)[0]).z;
(v_12).y = (transpose(unity_MatrixV)[1]).z;
(v_12).z = (transpose(unity_MatrixV)[2]).z;
(v_12).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_13;
tmpvar_13 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_14;
float tmpvar_15;
tmpvar_15 = clamp(((((lerp(dot(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)), (v_12).xyz), sqrt(dot(tmpvar_13, tmpvar_13)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_14 = tmpvar_15;
half4 tmpvar_16;
tmpvar_16 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_17;
color_17 = tmpvar_16;
half4 tmpvar_18;
tmpvar_18 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_10 = tmpvar_18;
fixed3 y_19;
y_19 = (((bakedDirTex_10).xyz - 0.5));
fixed tmpvar_20;
tmpvar_20 = max(0.0001, (bakedDirTex_10).w);
tmpvar_11 = (((((((unity_Lightmap_HDR).x * (color_17).xyz)) * ((dot(normalWorld_9, y_19) + 0.5)))) / tmpvar_20));
half3 shadowColor_21;
fixed3 tmpvar_22;
tmpvar_22 = (unity_ShadowColor).xyz;
shadowColor_21 = tmpvar_22;
tmpvar_11 = min(tmpvar_11, lerp(max(((tmpvar_11 - ((((max(0.0, dot(normalWorld_9, (_WorldSpaceLightPos0).xyz)) * ((1.0 - clamp(((tmpvar_1 + tmpvar_14)), 0.0, 1.0))))) * (_LightColor0).xyz)))), shadowColor_21), tmpvar_11, (_LightShadowData).xxx));
fixed4 c_23;
fixed4 c_24;
(c_24).xyz = float3(0.0, 0.0, 0.0);
(c_24).w = (((tmpvar_4).w * (_Color).w));
(c_23).w = (c_24).w;
(c_23).xyz = (((((tmpvar_4).xyz * (_Color).xyz)) * tmpvar_11));
(c_2).xyz = (c_23).xyz;
(c_2).w = 1.0;
o.target0 = c_2;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
float4 tmpvar_7;
tmpvar_7 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_6 = (tmpvar_7).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_6).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_6).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_6).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_7, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
fixed4 c_2;
fixed3 tmpvar_3;
tmpvar_3 = i.xlv_TEXCOORD1;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed tmpvar_5;
float lightShadowDataX_6;
half tmpvar_7;
tmpvar_7 = (_LightShadowData).x;
lightShadowDataX_6 = tmpvar_7;
float tmpvar_8;
tmpvar_8 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_6);
tmpvar_5 = tmpvar_8;
tmpvar_1 = tmpvar_5;
half3 normalWorld_9;
normalWorld_9 = tmpvar_3;
fixed4 bakedDirTex_10;
half3 tmpvar_11;
float4 v_12;
(v_12).x = (transpose(unity_MatrixV)[0]).z;
(v_12).y = (transpose(unity_MatrixV)[1]).z;
(v_12).z = (transpose(unity_MatrixV)[2]).z;
(v_12).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_13;
tmpvar_13 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_14;
float tmpvar_15;
tmpvar_15 = clamp(((((lerp(dot(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)), (v_12).xyz), sqrt(dot(tmpvar_13, tmpvar_13)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_14 = tmpvar_15;
half3 ambient_16;
half4 tmpvar_17;
(tmpvar_17).w = 1.0;
(tmpvar_17).xyz = normalWorld_9;
half3 x_18;
(x_18).x = dot(unity_SHAr, tmpvar_17);
(x_18).y = dot(unity_SHAg, tmpvar_17);
(x_18).z = dot(unity_SHAb, tmpvar_17);
half3 x1_19;
half4 tmpvar_20;
tmpvar_20 = (((normalWorld_9).xyzz * (normalWorld_9).yzzx));
(x1_19).x = dot(unity_SHBr, tmpvar_20);
(x1_19).y = dot(unity_SHBg, tmpvar_20);
(x1_19).z = dot(unity_SHBb, tmpvar_20);
ambient_16 = max(float3(0.0, 0.0, 0.0), ((x_18 + ((x1_19 + (((unity_SHC).xyz * (((((normalWorld_9).x * (normalWorld_9).x)) - (((normalWorld_9).y * (normalWorld_9).y)))))))))));
half3 tmpvar_21;
tmpvar_21 = max(((((1.055 * pow(max(ambient_16, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_16 = tmpvar_21;
half4 tmpvar_22;
tmpvar_22 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_23;
color_23 = tmpvar_22;
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_10 = tmpvar_24;
fixed3 y_25;
y_25 = (((bakedDirTex_10).xyz - 0.5));
fixed tmpvar_26;
tmpvar_26 = max(0.0001, (bakedDirTex_10).w);
tmpvar_11 = ((tmpvar_21 + (((((((unity_Lightmap_HDR).x * (color_23).xyz)) * ((dot(normalWorld_9, y_25) + 0.5)))) / tmpvar_26))));
half3 shadowColor_27;
fixed3 tmpvar_28;
tmpvar_28 = (unity_ShadowColor).xyz;
shadowColor_27 = tmpvar_28;
tmpvar_11 = min(tmpvar_11, lerp(max(((tmpvar_11 - ((((max(0.0, dot(normalWorld_9, (_WorldSpaceLightPos0).xyz)) * ((1.0 - clamp(((tmpvar_1 + tmpvar_14)), 0.0, 1.0))))) * (_LightColor0).xyz)))), shadowColor_27), tmpvar_11, (_LightShadowData).xxx));
fixed4 c_29;
fixed4 c_30;
(c_30).xyz = float3(0.0, 0.0, 0.0);
(c_30).w = (((tmpvar_4).w * (_Color).w));
(c_29).w = (c_30).w;
(c_29).xyz = (((((tmpvar_4).xyz * (_Color).xyz)) * tmpvar_11));
(c_2).xyz = (c_29).xyz;
(c_2).w = 1.0;
o.target0 = c_2;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
float4 tmpvar_7;
tmpvar_7 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_6 = (tmpvar_7).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_6).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_6).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_6).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_7, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
fixed4 c_2;
fixed3 tmpvar_3;
tmpvar_3 = i.xlv_TEXCOORD1;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed tmpvar_5;
float lightShadowDataX_6;
half tmpvar_7;
tmpvar_7 = (_LightShadowData).x;
lightShadowDataX_6 = tmpvar_7;
float tmpvar_8;
tmpvar_8 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_6);
tmpvar_5 = tmpvar_8;
tmpvar_1 = tmpvar_5;
half3 normalWorld_9;
normalWorld_9 = tmpvar_3;
half4 realtimeDirTex_10;
fixed4 bakedDirTex_11;
half3 tmpvar_12;
float4 v_13;
(v_13).x = (transpose(unity_MatrixV)[0]).z;
(v_13).y = (transpose(unity_MatrixV)[1]).z;
(v_13).z = (transpose(unity_MatrixV)[2]).z;
(v_13).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_14;
tmpvar_14 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_15;
float tmpvar_16;
tmpvar_16 = clamp(((((lerp(dot(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)), (v_13).xyz), sqrt(dot(tmpvar_14, tmpvar_14)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_15 = tmpvar_16;
half4 tmpvar_17;
tmpvar_17 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_18;
color_18 = tmpvar_17;
half4 tmpvar_19;
tmpvar_19 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_11 = tmpvar_19;
fixed3 y_20;
y_20 = (((bakedDirTex_11).xyz - 0.5));
fixed tmpvar_21;
tmpvar_21 = max(0.0001, (bakedDirTex_11).w);
tmpvar_12 = (((((((unity_Lightmap_HDR).x * (color_18).xyz)) * ((dot(normalWorld_9, y_20) + 0.5)))) / tmpvar_21));
half3 shadowColor_22;
fixed3 tmpvar_23;
tmpvar_23 = (unity_ShadowColor).xyz;
shadowColor_22 = tmpvar_23;
tmpvar_12 = min(tmpvar_12, lerp(max(((tmpvar_12 - ((((max(0.0, dot(normalWorld_9, (_WorldSpaceLightPos0).xyz)) * ((1.0 - clamp(((tmpvar_1 + tmpvar_15)), 0.0, 1.0))))) * (_LightColor0).xyz)))), shadowColor_22), tmpvar_12, (_LightShadowData).xxx));
fixed4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_25;
tmpvar_25 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_10 = tmpvar_25;
fixed4 dirTex_26;
dirTex_26 = realtimeDirTex_10;
fixed3 y_27;
y_27 = (((dirTex_26).xyz - 0.5));
fixed tmpvar_28;
tmpvar_28 = max(0.0001, (dirTex_26).w);
tmpvar_12 = ((tmpvar_12 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_24).w)) * (tmpvar_24).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_9, y_27) + 0.5)))) / tmpvar_28))));
fixed4 c_29;
fixed4 c_30;
(c_30).xyz = float3(0.0, 0.0, 0.0);
(c_30).w = (((tmpvar_4).w * (_Color).w));
(c_29).w = (c_30).w;
(c_29).xyz = (((((tmpvar_4).xyz * (_Color).xyz)) * tmpvar_12));
(c_2).xyz = (c_29).xyz;
(c_2).w = 1.0;
o.target0 = c_2;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
float4 tmpvar_7;
tmpvar_7 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_6 = (tmpvar_7).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_6).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_6).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_6).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_7, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
fixed4 bakedDirTex_17;
half bakedAtten_18;
half3 tmpvar_19;
half3 tmpvar_20;
fixed tmpvar_21;
tmpvar_21 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_18 = tmpvar_21;
float4 v_22;
(v_22).x = (transpose(unity_MatrixV)[0]).z;
(v_22).y = (transpose(unity_MatrixV)[1]).z;
(v_22).z = (transpose(unity_MatrixV)[2]).z;
(v_22).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_23;
tmpvar_23 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_24;
float tmpvar_25;
tmpvar_25 = clamp(((((lerp(dot(tmpvar_9, (v_22).xyz), sqrt(dot(tmpvar_23, tmpvar_23)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_24 = tmpvar_25;
tmpvar_19 = ((tmpvar_2 * lerp(tmpvar_1, bakedAtten_18, tmpvar_24)));
half4 tmpvar_26;
tmpvar_26 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_27;
color_27 = tmpvar_26;
half4 tmpvar_28;
tmpvar_28 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_17 = tmpvar_28;
fixed3 y_29;
y_29 = (((bakedDirTex_17).xyz - 0.5));
fixed tmpvar_30;
tmpvar_30 = max(0.0001, (bakedDirTex_17).w);
tmpvar_20 = (((((((unity_Lightmap_HDR).x * (color_27).xyz)) * ((dot(normalWorld_16, y_29) + 0.5)))) / tmpvar_30));
tmpvar_2 = tmpvar_19;
half3 viewDir_31;
viewDir_31 = tmpvar_8;
fixed4 c_32;
fixed4 c_33;
float nh_34;
fixed diff_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_35 = tmpvar_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_31)))));
nh_34 = tmpvar_37;
half y_38;
y_38 = ((_Shininess * 128.0));
float tmpvar_39;
tmpvar_39 = ((pow(nh_34, y_38) * (tmpvar_11).w));
(c_33).xyz = ((((((tmpvar_10 * tmpvar_19)) * diff_35)) + ((((tmpvar_19 * (_SpecColor).xyz)) * tmpvar_39))));
(c_33).w = (((tmpvar_11).w * (_Color).w));
(c_32).w = (c_33).w;
(c_32).xyz = (((c_33).xyz + ((tmpvar_10 * tmpvar_20))));
(c_4).xyz = (c_32).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
float4 tmpvar_7;
tmpvar_7 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_6 = (tmpvar_7).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_6).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_6).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_6).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_7, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
fixed4 bakedDirTex_17;
half bakedAtten_18;
half3 tmpvar_19;
half3 tmpvar_20;
fixed tmpvar_21;
tmpvar_21 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_18 = tmpvar_21;
float4 v_22;
(v_22).x = (transpose(unity_MatrixV)[0]).z;
(v_22).y = (transpose(unity_MatrixV)[1]).z;
(v_22).z = (transpose(unity_MatrixV)[2]).z;
(v_22).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_23;
tmpvar_23 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_24;
float tmpvar_25;
tmpvar_25 = clamp(((((lerp(dot(tmpvar_9, (v_22).xyz), sqrt(dot(tmpvar_23, tmpvar_23)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_24 = tmpvar_25;
tmpvar_19 = ((tmpvar_2 * lerp(tmpvar_1, bakedAtten_18, tmpvar_24)));
half3 ambient_26;
half4 tmpvar_27;
(tmpvar_27).w = 1.0;
(tmpvar_27).xyz = normalWorld_16;
half3 x_28;
(x_28).x = dot(unity_SHAr, tmpvar_27);
(x_28).y = dot(unity_SHAg, tmpvar_27);
(x_28).z = dot(unity_SHAb, tmpvar_27);
half3 x1_29;
half4 tmpvar_30;
tmpvar_30 = (((normalWorld_16).xyzz * (normalWorld_16).yzzx));
(x1_29).x = dot(unity_SHBr, tmpvar_30);
(x1_29).y = dot(unity_SHBg, tmpvar_30);
(x1_29).z = dot(unity_SHBb, tmpvar_30);
ambient_26 = max(float3(0.0, 0.0, 0.0), ((x_28 + ((x1_29 + (((unity_SHC).xyz * (((((normalWorld_16).x * (normalWorld_16).x)) - (((normalWorld_16).y * (normalWorld_16).y)))))))))));
half3 tmpvar_31;
tmpvar_31 = max(((((1.055 * pow(max(ambient_26, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_26 = tmpvar_31;
half4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_33;
color_33 = tmpvar_32;
half4 tmpvar_34;
tmpvar_34 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_17 = tmpvar_34;
fixed3 y_35;
y_35 = (((bakedDirTex_17).xyz - 0.5));
fixed tmpvar_36;
tmpvar_36 = max(0.0001, (bakedDirTex_17).w);
tmpvar_20 = ((tmpvar_31 + (((((((unity_Lightmap_HDR).x * (color_33).xyz)) * ((dot(normalWorld_16, y_35) + 0.5)))) / tmpvar_36))));
tmpvar_2 = tmpvar_19;
half3 viewDir_37;
viewDir_37 = tmpvar_8;
fixed4 c_38;
fixed4 c_39;
float nh_40;
fixed diff_41;
half tmpvar_42;
tmpvar_42 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_41 = tmpvar_42;
half tmpvar_43;
tmpvar_43 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_37)))));
nh_40 = tmpvar_43;
half y_44;
y_44 = ((_Shininess * 128.0));
float tmpvar_45;
tmpvar_45 = ((pow(nh_40, y_44) * (tmpvar_11).w));
(c_39).xyz = ((((((tmpvar_10 * tmpvar_19)) * diff_41)) + ((((tmpvar_19 * (_SpecColor).xyz)) * tmpvar_45))));
(c_39).w = (((tmpvar_11).w * (_Color).w));
(c_38).w = (c_39).w;
(c_38).xyz = (((c_39).xyz + ((tmpvar_10 * tmpvar_20))));
(c_4).xyz = (c_38).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
float4 tmpvar_7;
tmpvar_7 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_6 = (tmpvar_7).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_6).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_6).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_6).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_7, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
half4 realtimeDirTex_17;
fixed4 bakedDirTex_18;
half bakedAtten_19;
half3 tmpvar_20;
half3 tmpvar_21;
fixed tmpvar_22;
tmpvar_22 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_19 = tmpvar_22;
float4 v_23;
(v_23).x = (transpose(unity_MatrixV)[0]).z;
(v_23).y = (transpose(unity_MatrixV)[1]).z;
(v_23).z = (transpose(unity_MatrixV)[2]).z;
(v_23).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_24;
tmpvar_24 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_25;
float tmpvar_26;
tmpvar_26 = clamp(((((lerp(dot(tmpvar_9, (v_23).xyz), sqrt(dot(tmpvar_24, tmpvar_24)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_25 = tmpvar_26;
tmpvar_20 = ((tmpvar_2 * lerp(tmpvar_1, bakedAtten_19, tmpvar_25)));
half4 tmpvar_27;
tmpvar_27 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_28;
color_28 = tmpvar_27;
half4 tmpvar_29;
tmpvar_29 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_18 = tmpvar_29;
fixed3 y_30;
y_30 = (((bakedDirTex_18).xyz - 0.5));
fixed tmpvar_31;
tmpvar_31 = max(0.0001, (bakedDirTex_18).w);
tmpvar_21 = (((((((unity_Lightmap_HDR).x * (color_28).xyz)) * ((dot(normalWorld_16, y_30) + 0.5)))) / tmpvar_31));
fixed4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_33;
tmpvar_33 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_17 = tmpvar_33;
fixed4 dirTex_34;
dirTex_34 = realtimeDirTex_17;
fixed3 y_35;
y_35 = (((dirTex_34).xyz - 0.5));
fixed tmpvar_36;
tmpvar_36 = max(0.0001, (dirTex_34).w);
tmpvar_21 = ((tmpvar_21 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_32).w)) * (tmpvar_32).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_16, y_35) + 0.5)))) / tmpvar_36))));
tmpvar_2 = tmpvar_20;
half3 viewDir_37;
viewDir_37 = tmpvar_8;
fixed4 c_38;
fixed4 c_39;
float nh_40;
fixed diff_41;
half tmpvar_42;
tmpvar_42 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_41 = tmpvar_42;
half tmpvar_43;
tmpvar_43 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_37)))));
nh_40 = tmpvar_43;
half y_44;
y_44 = ((_Shininess * 128.0));
float tmpvar_45;
tmpvar_45 = ((pow(nh_40, y_44) * (tmpvar_11).w));
(c_39).xyz = ((((((tmpvar_10 * tmpvar_20)) * diff_41)) + ((((tmpvar_20 * (_SpecColor).xyz)) * tmpvar_45))));
(c_39).w = (((tmpvar_11).w * (_Color).w));
(c_38).w = (c_39).w;
(c_38).xyz = (((c_39).xyz + ((tmpvar_10 * tmpvar_21))));
(c_4).xyz = (c_38).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
float4 tmpvar_7;
tmpvar_7 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_6 = (tmpvar_7).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_6).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_6).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_6).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_7, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
fixed4 bakedDirTex_17;
half bakedAtten_18;
half3 tmpvar_19;
half3 tmpvar_20;
fixed tmpvar_21;
tmpvar_21 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_18 = tmpvar_21;
float4 v_22;
(v_22).x = (transpose(unity_MatrixV)[0]).z;
(v_22).y = (transpose(unity_MatrixV)[1]).z;
(v_22).z = (transpose(unity_MatrixV)[2]).z;
(v_22).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_23;
tmpvar_23 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_24;
float tmpvar_25;
tmpvar_25 = clamp(((((lerp(dot(tmpvar_9, (v_22).xyz), sqrt(dot(tmpvar_23, tmpvar_23)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_24 = tmpvar_25;
tmpvar_19 = ((tmpvar_2 * min(clamp(((tmpvar_1 + tmpvar_24)), 0.0, 1.0), bakedAtten_18)));
half4 tmpvar_26;
tmpvar_26 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_27;
color_27 = tmpvar_26;
half4 tmpvar_28;
tmpvar_28 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_17 = tmpvar_28;
fixed3 y_29;
y_29 = (((bakedDirTex_17).xyz - 0.5));
fixed tmpvar_30;
tmpvar_30 = max(0.0001, (bakedDirTex_17).w);
tmpvar_20 = (((((((unity_Lightmap_HDR).x * (color_27).xyz)) * ((dot(normalWorld_16, y_29) + 0.5)))) / tmpvar_30));
tmpvar_2 = tmpvar_19;
half3 viewDir_31;
viewDir_31 = tmpvar_8;
fixed4 c_32;
fixed4 c_33;
float nh_34;
fixed diff_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_35 = tmpvar_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_31)))));
nh_34 = tmpvar_37;
half y_38;
y_38 = ((_Shininess * 128.0));
float tmpvar_39;
tmpvar_39 = ((pow(nh_34, y_38) * (tmpvar_11).w));
(c_33).xyz = ((((((tmpvar_10 * tmpvar_19)) * diff_35)) + ((((tmpvar_19 * (_SpecColor).xyz)) * tmpvar_39))));
(c_33).w = (((tmpvar_11).w * (_Color).w));
(c_32).w = (c_33).w;
(c_32).xyz = (((c_33).xyz + ((tmpvar_10 * tmpvar_20))));
(c_4).xyz = (c_32).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
float4 tmpvar_7;
tmpvar_7 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_6 = (tmpvar_7).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_6).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_6).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_6).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_7, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
fixed4 bakedDirTex_17;
half bakedAtten_18;
half3 tmpvar_19;
half3 tmpvar_20;
fixed tmpvar_21;
tmpvar_21 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_18 = tmpvar_21;
float4 v_22;
(v_22).x = (transpose(unity_MatrixV)[0]).z;
(v_22).y = (transpose(unity_MatrixV)[1]).z;
(v_22).z = (transpose(unity_MatrixV)[2]).z;
(v_22).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_23;
tmpvar_23 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_24;
float tmpvar_25;
tmpvar_25 = clamp(((((lerp(dot(tmpvar_9, (v_22).xyz), sqrt(dot(tmpvar_23, tmpvar_23)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_24 = tmpvar_25;
tmpvar_19 = ((tmpvar_2 * min(clamp(((tmpvar_1 + tmpvar_24)), 0.0, 1.0), bakedAtten_18)));
half3 ambient_26;
half4 tmpvar_27;
(tmpvar_27).w = 1.0;
(tmpvar_27).xyz = normalWorld_16;
half3 x_28;
(x_28).x = dot(unity_SHAr, tmpvar_27);
(x_28).y = dot(unity_SHAg, tmpvar_27);
(x_28).z = dot(unity_SHAb, tmpvar_27);
half3 x1_29;
half4 tmpvar_30;
tmpvar_30 = (((normalWorld_16).xyzz * (normalWorld_16).yzzx));
(x1_29).x = dot(unity_SHBr, tmpvar_30);
(x1_29).y = dot(unity_SHBg, tmpvar_30);
(x1_29).z = dot(unity_SHBb, tmpvar_30);
ambient_26 = max(float3(0.0, 0.0, 0.0), ((x_28 + ((x1_29 + (((unity_SHC).xyz * (((((normalWorld_16).x * (normalWorld_16).x)) - (((normalWorld_16).y * (normalWorld_16).y)))))))))));
half3 tmpvar_31;
tmpvar_31 = max(((((1.055 * pow(max(ambient_26, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_26 = tmpvar_31;
half4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_33;
color_33 = tmpvar_32;
half4 tmpvar_34;
tmpvar_34 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_17 = tmpvar_34;
fixed3 y_35;
y_35 = (((bakedDirTex_17).xyz - 0.5));
fixed tmpvar_36;
tmpvar_36 = max(0.0001, (bakedDirTex_17).w);
tmpvar_20 = ((tmpvar_31 + (((((((unity_Lightmap_HDR).x * (color_33).xyz)) * ((dot(normalWorld_16, y_35) + 0.5)))) / tmpvar_36))));
tmpvar_2 = tmpvar_19;
half3 viewDir_37;
viewDir_37 = tmpvar_8;
fixed4 c_38;
fixed4 c_39;
float nh_40;
fixed diff_41;
half tmpvar_42;
tmpvar_42 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_41 = tmpvar_42;
half tmpvar_43;
tmpvar_43 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_37)))));
nh_40 = tmpvar_43;
half y_44;
y_44 = ((_Shininess * 128.0));
float tmpvar_45;
tmpvar_45 = ((pow(nh_40, y_44) * (tmpvar_11).w));
(c_39).xyz = ((((((tmpvar_10 * tmpvar_19)) * diff_41)) + ((((tmpvar_19 * (_SpecColor).xyz)) * tmpvar_45))));
(c_39).w = (((tmpvar_11).w * (_Color).w));
(c_38).w = (c_39).w;
(c_38).xyz = (((c_39).xyz + ((tmpvar_10 * tmpvar_20))));
(c_4).xyz = (c_38).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
float3 tmpvar_6;
float4 tmpvar_7;
tmpvar_7 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_6 = (tmpvar_7).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_6).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_6).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_6).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_7, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
half4 realtimeDirTex_17;
fixed4 bakedDirTex_18;
half bakedAtten_19;
half3 tmpvar_20;
half3 tmpvar_21;
fixed tmpvar_22;
tmpvar_22 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_19 = tmpvar_22;
float4 v_23;
(v_23).x = (transpose(unity_MatrixV)[0]).z;
(v_23).y = (transpose(unity_MatrixV)[1]).z;
(v_23).z = (transpose(unity_MatrixV)[2]).z;
(v_23).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_24;
tmpvar_24 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_25;
float tmpvar_26;
tmpvar_26 = clamp(((((lerp(dot(tmpvar_9, (v_23).xyz), sqrt(dot(tmpvar_24, tmpvar_24)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_25 = tmpvar_26;
tmpvar_20 = ((tmpvar_2 * min(clamp(((tmpvar_1 + tmpvar_25)), 0.0, 1.0), bakedAtten_19)));
half4 tmpvar_27;
tmpvar_27 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_28;
color_28 = tmpvar_27;
half4 tmpvar_29;
tmpvar_29 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_18 = tmpvar_29;
fixed3 y_30;
y_30 = (((bakedDirTex_18).xyz - 0.5));
fixed tmpvar_31;
tmpvar_31 = max(0.0001, (bakedDirTex_18).w);
tmpvar_21 = (((((((unity_Lightmap_HDR).x * (color_28).xyz)) * ((dot(normalWorld_16, y_30) + 0.5)))) / tmpvar_31));
fixed4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_33;
tmpvar_33 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_17 = tmpvar_33;
fixed4 dirTex_34;
dirTex_34 = realtimeDirTex_17;
fixed3 y_35;
y_35 = (((dirTex_34).xyz - 0.5));
fixed tmpvar_36;
tmpvar_36 = max(0.0001, (dirTex_34).w);
tmpvar_21 = ((tmpvar_21 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_32).w)) * (tmpvar_32).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_16, y_35) + 0.5)))) / tmpvar_36))));
tmpvar_2 = tmpvar_20;
half3 viewDir_37;
viewDir_37 = tmpvar_8;
fixed4 c_38;
fixed4 c_39;
float nh_40;
fixed diff_41;
half tmpvar_42;
tmpvar_42 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_41 = tmpvar_42;
half tmpvar_43;
tmpvar_43 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_37)))));
nh_40 = tmpvar_43;
half y_44;
y_44 = ((_Shininess * 128.0));
float tmpvar_45;
tmpvar_45 = ((pow(nh_40, y_44) * (tmpvar_11).w));
(c_39).xyz = ((((((tmpvar_10 * tmpvar_20)) * diff_41)) + ((((tmpvar_20 * (_SpecColor).xyz)) * tmpvar_45))));
(c_39).w = (((tmpvar_11).w * (_Color).w));
(c_38).w = (c_39).w;
(c_38).xyz = (((c_39).xyz + ((tmpvar_10 * tmpvar_21))));
(c_4).xyz = (c_38).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

half3 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3 tmpvar_3;
tmpvar_3 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_5;
tmpvar_5 = normalize((mul(tmpvar_4, v._glesNormal)));
float3 lightColor0_6;
lightColor0_6 = (unity_LightColor[0]).xyz;
float3 lightColor1_7;
lightColor1_7 = (unity_LightColor[1]).xyz;
float3 lightColor2_8;
lightColor2_8 = (unity_LightColor[2]).xyz;
float3 lightColor3_9;
lightColor3_9 = (unity_LightColor[3]).xyz;
float4 lightAttenSq_10;
lightAttenSq_10 = unity_4LightAtten0;
float3 col_11;
float4 ndotl_12;
float4 lengthSq_13;
float4 tmpvar_14;
tmpvar_14 = ((unity_4LightPosX0 - (tmpvar_3).x));
float4 tmpvar_15;
tmpvar_15 = ((unity_4LightPosY0 - (tmpvar_3).y));
float4 tmpvar_16;
tmpvar_16 = ((unity_4LightPosZ0 - (tmpvar_3).z));
lengthSq_13 = ((tmpvar_14 * tmpvar_14));
lengthSq_13 = ((lengthSq_13 + ((tmpvar_15 * tmpvar_15))));
lengthSq_13 = ((lengthSq_13 + ((tmpvar_16 * tmpvar_16))));
float4 tmpvar_17;
tmpvar_17 = max(lengthSq_13, float4(1e-06, 1e-06, 1e-06, 1e-06));
lengthSq_13 = tmpvar_17;
ndotl_12 = ((tmpvar_14 * (tmpvar_5).x));
ndotl_12 = ((ndotl_12 + ((tmpvar_15 * (tmpvar_5).y))));
ndotl_12 = ((ndotl_12 + ((tmpvar_16 * (tmpvar_5).z))));
float4 tmpvar_18;
tmpvar_18 = max(float4(0.0, 0.0, 0.0, 0.0), ((ndotl_12 * rsqrt(tmpvar_17))));
ndotl_12 = tmpvar_18;
float4 tmpvar_19;
tmpvar_19 = ((tmpvar_18 * ((1.0 / (((1.0 + ((tmpvar_17 * lightAttenSq_10)))))))));
col_11 = ((lightColor0_6 * (tmpvar_19).x));
col_11 = ((col_11 + ((lightColor1_7 * (tmpvar_19).y))));
col_11 = ((col_11 + ((lightColor2_8 * (tmpvar_19).z))));
col_11 = ((col_11 + ((lightColor3_9 * (tmpvar_19).w))));
tmpvar_1 = col_11;
half3 normal_20;
normal_20 = tmpvar_5;
half3 ambient_21;
half4 tmpvar_22;
(tmpvar_22).w = 1.0;
(tmpvar_22).xyz = normal_20;
half3 res_23;
half3 x_24;
(x_24).x = dot(unity_SHAr, tmpvar_22);
(x_24).y = dot(unity_SHAg, tmpvar_22);
(x_24).z = dot(unity_SHAb, tmpvar_22);
half3 x1_25;
half4 tmpvar_26;
tmpvar_26 = (((normal_20).xyzz * (normal_20).yzzx));
(x1_25).x = dot(unity_SHBr, tmpvar_26);
(x1_25).y = dot(unity_SHBg, tmpvar_26);
(x1_25).z = dot(unity_SHBb, tmpvar_26);
res_23 = ((x_24 + ((x1_25 + (((unity_SHC).xyz * (((((normal_20).x * (normal_20).x)) - (((normal_20).y * (normal_20).y))))))))));
half3 tmpvar_27;
tmpvar_27 = max(((((1.055 * pow(max(res_23, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_23 = tmpvar_27;
ambient_21 = ((tmpvar_1 + max(float3(0.0, 0.0, 0.0), tmpvar_27)));
tmpvar_1 = ambient_21;
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_5;
o.xlv_TEXCOORD2 = tmpvar_3;
o.xlv_TEXCOORD3 = ambient_21;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 viewDir_10;
viewDir_10 = worldViewDir_6;
fixed4 c_11;
fixed4 c_12;
float nh_13;
fixed diff_14;
half tmpvar_15;
tmpvar_15 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_14 = tmpvar_15;
half tmpvar_16;
tmpvar_16 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_10)))));
nh_13 = tmpvar_16;
half y_17;
y_17 = ((_Shininess * 128.0));
float tmpvar_18;
tmpvar_18 = ((pow(nh_13, y_17) * (tmpvar_9).w));
(c_12).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_14)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_18))));
(c_12).w = (((tmpvar_9).w * (_Color).w));
(c_11).w = (c_12).w;
(c_11).xyz = (((c_12).xyz + ((tmpvar_4 * i.xlv_TEXCOORD3))));
(c_3).xyz = (c_11).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

half3 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
float3 tmpvar_5;
tmpvar_5 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_6;
tmpvar_6[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_6[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_6[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_7;
tmpvar_7 = normalize((mul(tmpvar_6, v._glesNormal)));
float3 lightColor0_8;
lightColor0_8 = (unity_LightColor[0]).xyz;
float3 lightColor1_9;
lightColor1_9 = (unity_LightColor[1]).xyz;
float3 lightColor2_10;
lightColor2_10 = (unity_LightColor[2]).xyz;
float3 lightColor3_11;
lightColor3_11 = (unity_LightColor[3]).xyz;
float4 lightAttenSq_12;
lightAttenSq_12 = unity_4LightAtten0;
float3 col_13;
float4 ndotl_14;
float4 lengthSq_15;
float4 tmpvar_16;
tmpvar_16 = ((unity_4LightPosX0 - (tmpvar_5).x));
float4 tmpvar_17;
tmpvar_17 = ((unity_4LightPosY0 - (tmpvar_5).y));
float4 tmpvar_18;
tmpvar_18 = ((unity_4LightPosZ0 - (tmpvar_5).z));
lengthSq_15 = ((tmpvar_16 * tmpvar_16));
lengthSq_15 = ((lengthSq_15 + ((tmpvar_17 * tmpvar_17))));
lengthSq_15 = ((lengthSq_15 + ((tmpvar_18 * tmpvar_18))));
float4 tmpvar_19;
tmpvar_19 = max(lengthSq_15, float4(1e-06, 1e-06, 1e-06, 1e-06));
lengthSq_15 = tmpvar_19;
ndotl_14 = ((tmpvar_16 * (tmpvar_7).x));
ndotl_14 = ((ndotl_14 + ((tmpvar_17 * (tmpvar_7).y))));
ndotl_14 = ((ndotl_14 + ((tmpvar_18 * (tmpvar_7).z))));
float4 tmpvar_20;
tmpvar_20 = max(float4(0.0, 0.0, 0.0, 0.0), ((ndotl_14 * rsqrt(tmpvar_19))));
ndotl_14 = tmpvar_20;
float4 tmpvar_21;
tmpvar_21 = ((tmpvar_20 * ((1.0 / (((1.0 + ((tmpvar_19 * lightAttenSq_12)))))))));
col_13 = ((lightColor0_8 * (tmpvar_21).x));
col_13 = ((col_13 + ((lightColor1_9 * (tmpvar_21).y))));
col_13 = ((col_13 + ((lightColor2_10 * (tmpvar_21).z))));
col_13 = ((col_13 + ((lightColor3_11 * (tmpvar_21).w))));
tmpvar_1 = col_13;
half3 normal_22;
normal_22 = tmpvar_7;
half3 ambient_23;
half3 x1_24;
half4 tmpvar_25;
tmpvar_25 = (((normal_22).xyzz * (normal_22).yzzx));
(x1_24).x = dot(unity_SHBr, tmpvar_25);
(x1_24).y = dot(unity_SHBg, tmpvar_25);
(x1_24).z = dot(unity_SHBb, tmpvar_25);
ambient_23 = ((((tmpvar_1 * ((((tmpvar_1 * ((((tmpvar_1 * 0.305306)) + 0.6821711)))) + 0.01252288)))) + ((x1_24 + (((unity_SHC).xyz * (((((normal_22).x * (normal_22).x)) - (((normal_22).y * (normal_22).y))))))))));
tmpvar_1 = ambient_23;
o.position = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_7;
o.xlv_TEXCOORD2 = tmpvar_5;
o.xlv_TEXCOORD3 = ambient_23;
o.xlv_TEXCOORD4 = tmpvar_2;
o.xlv_TEXCOORD6 = tmpvar_3;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
float4 v_14;
(v_14).x = (transpose(unity_MatrixV)[0]).z;
(v_14).y = (transpose(unity_MatrixV)[1]).z;
(v_14).z = (transpose(unity_MatrixV)[2]).z;
(v_14).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_15;
tmpvar_15 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_16;
float tmpvar_17;
tmpvar_17 = clamp(((((lerp(dot(tmpvar_10, (v_14).xyz), sqrt(dot(tmpvar_15, tmpvar_15)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_16 = tmpvar_17;
float4 tmpvar_18;
(tmpvar_18).w = 1.0;
(tmpvar_18).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_19;
float4 shadowCoord_20;
shadowCoord_20 = (mul(tmpvar_18, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_21;
half tmpvar_22;
tmpvar_22 = (_LightShadowData).x;
lightShadowDataX_21 = tmpvar_22;
float tmpvar_23;
tmpvar_23 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_20).xy)).x > (shadowCoord_20).z)))), lightShadowDataX_21);
tmpvar_19 = tmpvar_23;
realtimeShadowAttenuation_13 = tmpvar_19;
half tmpvar_24;
tmpvar_24 = clamp(((realtimeShadowAttenuation_13 + tmpvar_16)), 0.0, 1.0);
atten_5 = tmpvar_24;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_25;
normalWorld_25 = tmpvar_6;
half3 tmpvar_26;
tmpvar_26 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_27;
(tmpvar_27).w = 1.0;
(tmpvar_27).xyz = normalWorld_25;
half3 x_28;
(x_28).x = dot(unity_SHAr, tmpvar_27);
(x_28).y = dot(unity_SHAg, tmpvar_27);
(x_28).z = dot(unity_SHAb, tmpvar_27);
half3 tmpvar_29;
tmpvar_29 = max(((((1.055 * pow(max(float3(0.0, 0.0, 0.0), ((i.xlv_TEXCOORD3 + x_28))), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
tmpvar_2 = tmpvar_26;
half3 viewDir_30;
viewDir_30 = tmpvar_9;
fixed4 c_31;
fixed4 c_32;
float nh_33;
fixed diff_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_34 = tmpvar_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_30)))));
nh_33 = tmpvar_36;
half y_37;
y_37 = ((_Shininess * 128.0));
float tmpvar_38;
tmpvar_38 = ((pow(nh_33, y_37) * (tmpvar_12).w));
(c_32).xyz = ((((((tmpvar_11 * tmpvar_26)) * diff_34)) + ((((tmpvar_26 * (_SpecColor).xyz)) * tmpvar_38))));
(c_32).w = (((tmpvar_12).w * (_Color).w));
(c_31).w = (c_32).w;
(c_31).xyz = (((c_32).xyz + ((tmpvar_11 * tmpvar_29))));
(c_4).xyz = (c_31).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float2 xlv_TEXCOORD4 : TEXCOORD3;
float4 xlv_TEXCOORD6 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half realtimeShadowAttenuation_12;
half shadowMaskAttenuation_13;
float4 v_14;
(v_14).x = (transpose(unity_MatrixV)[0]).z;
(v_14).y = (transpose(unity_MatrixV)[1]).z;
(v_14).z = (transpose(unity_MatrixV)[2]).z;
(v_14).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_15;
tmpvar_15 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_16;
float tmpvar_17;
tmpvar_17 = clamp(((((lerp(dot(tmpvar_10, (v_14).xyz), sqrt(dot(tmpvar_15, tmpvar_15)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_16 = tmpvar_17;
fixed tmpvar_18;
tmpvar_18 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_13 = tmpvar_18;
float4 tmpvar_19;
(tmpvar_19).w = 1.0;
(tmpvar_19).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_20;
float4 shadowCoord_21;
shadowCoord_21 = (mul(tmpvar_19, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_22;
half tmpvar_23;
tmpvar_23 = (_LightShadowData).x;
lightShadowDataX_22 = tmpvar_23;
float tmpvar_24;
tmpvar_24 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_21).xy)).x > (shadowCoord_21).z)))), lightShadowDataX_22);
tmpvar_20 = tmpvar_24;
realtimeShadowAttenuation_12 = tmpvar_20;
half tmpvar_25;
tmpvar_25 = lerp(realtimeShadowAttenuation_12, shadowMaskAttenuation_13, tmpvar_16);
atten_5 = tmpvar_25;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 tmpvar_26;
tmpvar_26 = ((tmpvar_2 * tmpvar_1));
tmpvar_2 = tmpvar_26;
half3 viewDir_27;
viewDir_27 = tmpvar_9;
fixed4 c_28;
fixed4 c_29;
float nh_30;
fixed diff_31;
half tmpvar_32;
tmpvar_32 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_31 = tmpvar_32;
half tmpvar_33;
tmpvar_33 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_27)))));
nh_30 = tmpvar_33;
half y_34;
y_34 = ((_Shininess * 128.0));
float tmpvar_35;
tmpvar_35 = ((pow(nh_30, y_34) * (tmpvar_11).w));
(c_29).xyz = (((((((tmpvar_11).xyz * (_Color).xyz)) * ((tmpvar_26 * diff_31)))) + ((((tmpvar_26 * (_SpecColor).xyz)) * tmpvar_35))));
(c_29).w = (((tmpvar_11).w * (_Color).w));
(c_28).w = (c_29).w;
(c_28).xyz = (c_29).xyz;
(c_4).xyz = (c_28).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

half3 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
float3 tmpvar_4;
tmpvar_4 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_5;
tmpvar_5[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_5[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_5[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_6;
tmpvar_6 = normalize((mul(tmpvar_5, v._glesNormal)));
float3 lightColor0_7;
lightColor0_7 = (unity_LightColor[0]).xyz;
float3 lightColor1_8;
lightColor1_8 = (unity_LightColor[1]).xyz;
float3 lightColor2_9;
lightColor2_9 = (unity_LightColor[2]).xyz;
float3 lightColor3_10;
lightColor3_10 = (unity_LightColor[3]).xyz;
float4 lightAttenSq_11;
lightAttenSq_11 = unity_4LightAtten0;
float3 col_12;
float4 ndotl_13;
float4 lengthSq_14;
float4 tmpvar_15;
tmpvar_15 = ((unity_4LightPosX0 - (tmpvar_4).x));
float4 tmpvar_16;
tmpvar_16 = ((unity_4LightPosY0 - (tmpvar_4).y));
float4 tmpvar_17;
tmpvar_17 = ((unity_4LightPosZ0 - (tmpvar_4).z));
lengthSq_14 = ((tmpvar_15 * tmpvar_15));
lengthSq_14 = ((lengthSq_14 + ((tmpvar_16 * tmpvar_16))));
lengthSq_14 = ((lengthSq_14 + ((tmpvar_17 * tmpvar_17))));
float4 tmpvar_18;
tmpvar_18 = max(lengthSq_14, float4(1e-06, 1e-06, 1e-06, 1e-06));
lengthSq_14 = tmpvar_18;
ndotl_13 = ((tmpvar_15 * (tmpvar_6).x));
ndotl_13 = ((ndotl_13 + ((tmpvar_16 * (tmpvar_6).y))));
ndotl_13 = ((ndotl_13 + ((tmpvar_17 * (tmpvar_6).z))));
float4 tmpvar_19;
tmpvar_19 = max(float4(0.0, 0.0, 0.0, 0.0), ((ndotl_13 * rsqrt(tmpvar_18))));
ndotl_13 = tmpvar_19;
float4 tmpvar_20;
tmpvar_20 = ((tmpvar_19 * ((1.0 / (((1.0 + ((tmpvar_18 * lightAttenSq_11)))))))));
col_12 = ((lightColor0_7 * (tmpvar_20).x));
col_12 = ((col_12 + ((lightColor1_8 * (tmpvar_20).y))));
col_12 = ((col_12 + ((lightColor2_9 * (tmpvar_20).z))));
col_12 = ((col_12 + ((lightColor3_10 * (tmpvar_20).w))));
tmpvar_1 = col_12;
half3 normal_21;
normal_21 = tmpvar_6;
half3 ambient_22;
half3 x1_23;
half4 tmpvar_24;
tmpvar_24 = (((normal_21).xyzz * (normal_21).yzzx));
(x1_23).x = dot(unity_SHBr, tmpvar_24);
(x1_23).y = dot(unity_SHBg, tmpvar_24);
(x1_23).z = dot(unity_SHBb, tmpvar_24);
ambient_22 = ((((tmpvar_1 * ((((tmpvar_1 * ((((tmpvar_1 * 0.305306)) + 0.6821711)))) + 0.01252288)))) + ((x1_23 + (((unity_SHC).xyz * (((((normal_21).x * (normal_21).x)) - (((normal_21).y * (normal_21).y))))))))));
tmpvar_1 = ambient_22;
o.position = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_6;
o.xlv_TEXCOORD2 = tmpvar_4;
o.xlv_TEXCOORD3 = ambient_22;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD6 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = lerp(realtimeShadowAttenuation_13, shadowMaskAttenuation_14, tmpvar_17);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_27;
normalWorld_27 = tmpvar_6;
half3 tmpvar_28;
tmpvar_28 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_29;
(tmpvar_29).w = 1.0;
(tmpvar_29).xyz = normalWorld_27;
half3 x_30;
(x_30).x = dot(unity_SHAr, tmpvar_29);
(x_30).y = dot(unity_SHAg, tmpvar_29);
(x_30).z = dot(unity_SHAb, tmpvar_29);
half3 tmpvar_31;
tmpvar_31 = max(((((1.055 * pow(max(float3(0.0, 0.0, 0.0), ((i.xlv_TEXCOORD3 + x_30))), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
tmpvar_2 = tmpvar_28;
half3 viewDir_32;
viewDir_32 = tmpvar_9;
fixed4 c_33;
fixed4 c_34;
float nh_35;
fixed diff_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_36 = tmpvar_37;
half tmpvar_38;
tmpvar_38 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_32)))));
nh_35 = tmpvar_38;
half y_39;
y_39 = ((_Shininess * 128.0));
float tmpvar_40;
tmpvar_40 = ((pow(nh_35, y_39) * (tmpvar_12).w));
(c_34).xyz = ((((((tmpvar_11 * tmpvar_28)) * diff_36)) + ((((tmpvar_28 * (_SpecColor).xyz)) * tmpvar_40))));
(c_34).w = (((tmpvar_12).w * (_Color).w));
(c_33).w = (c_34).w;
(c_33).xyz = (((c_34).xyz + ((tmpvar_11 * tmpvar_31))));
(c_4).xyz = (c_33).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float2 xlv_TEXCOORD4 : TEXCOORD3;
float4 xlv_TEXCOORD6 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = lerp(realtimeShadowAttenuation_13, shadowMaskAttenuation_14, tmpvar_17);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_27;
normalWorld_27 = tmpvar_6;
half4 realtimeDirTex_28;
half3 tmpvar_29;
half3 tmpvar_30;
tmpvar_29 = ((tmpvar_2 * tmpvar_1));
fixed4 tmpvar_31;
tmpvar_31 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
fixed4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
realtimeDirTex_28 = tmpvar_32;
fixed4 dirTex_33;
dirTex_33 = realtimeDirTex_28;
fixed3 y_34;
y_34 = (((dirTex_33).xyz - 0.5));
fixed tmpvar_35;
tmpvar_35 = max(0.0001, (dirTex_33).w);
tmpvar_30 = ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_31).w)) * (tmpvar_31).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_27, y_34) + 0.5)))) / tmpvar_35));
tmpvar_2 = tmpvar_29;
half3 viewDir_36;
viewDir_36 = tmpvar_9;
fixed4 c_37;
fixed4 c_38;
float nh_39;
fixed diff_40;
half tmpvar_41;
tmpvar_41 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_40 = tmpvar_41;
half tmpvar_42;
tmpvar_42 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_36)))));
nh_39 = tmpvar_42;
half y_43;
y_43 = ((_Shininess * 128.0));
float tmpvar_44;
tmpvar_44 = ((pow(nh_39, y_43) * (tmpvar_12).w));
(c_38).xyz = ((((((tmpvar_11 * tmpvar_29)) * diff_40)) + ((((tmpvar_29 * (_SpecColor).xyz)) * tmpvar_44))));
(c_38).w = (((tmpvar_12).w * (_Color).w));
(c_37).w = (c_38).w;
(c_37).xyz = (((c_38).xyz + ((tmpvar_11 * tmpvar_30))));
(c_4).xyz = (c_37).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float2 xlv_TEXCOORD4 : TEXCOORD3;
float4 xlv_TEXCOORD6 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half realtimeShadowAttenuation_12;
half shadowMaskAttenuation_13;
float4 v_14;
(v_14).x = (transpose(unity_MatrixV)[0]).z;
(v_14).y = (transpose(unity_MatrixV)[1]).z;
(v_14).z = (transpose(unity_MatrixV)[2]).z;
(v_14).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_15;
tmpvar_15 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_16;
float tmpvar_17;
tmpvar_17 = clamp(((((lerp(dot(tmpvar_10, (v_14).xyz), sqrt(dot(tmpvar_15, tmpvar_15)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_16 = tmpvar_17;
fixed tmpvar_18;
tmpvar_18 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_13 = tmpvar_18;
float4 tmpvar_19;
(tmpvar_19).w = 1.0;
(tmpvar_19).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_20;
float4 shadowCoord_21;
shadowCoord_21 = (mul(tmpvar_19, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_22;
half tmpvar_23;
tmpvar_23 = (_LightShadowData).x;
lightShadowDataX_22 = tmpvar_23;
float tmpvar_24;
tmpvar_24 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_21).xy)).x > (shadowCoord_21).z)))), lightShadowDataX_22);
tmpvar_20 = tmpvar_24;
realtimeShadowAttenuation_12 = tmpvar_20;
half tmpvar_25;
tmpvar_25 = min(clamp(((realtimeShadowAttenuation_12 + tmpvar_16)), 0.0, 1.0), shadowMaskAttenuation_13);
atten_5 = tmpvar_25;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 tmpvar_26;
tmpvar_26 = ((tmpvar_2 * tmpvar_1));
tmpvar_2 = tmpvar_26;
half3 viewDir_27;
viewDir_27 = tmpvar_9;
fixed4 c_28;
fixed4 c_29;
float nh_30;
fixed diff_31;
half tmpvar_32;
tmpvar_32 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_31 = tmpvar_32;
half tmpvar_33;
tmpvar_33 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_27)))));
nh_30 = tmpvar_33;
half y_34;
y_34 = ((_Shininess * 128.0));
float tmpvar_35;
tmpvar_35 = ((pow(nh_30, y_34) * (tmpvar_11).w));
(c_29).xyz = (((((((tmpvar_11).xyz * (_Color).xyz)) * ((tmpvar_26 * diff_31)))) + ((((tmpvar_26 * (_SpecColor).xyz)) * tmpvar_35))));
(c_29).w = (((tmpvar_11).w * (_Color).w));
(c_28).w = (c_29).w;
(c_28).xyz = (c_29).xyz;
(c_4).xyz = (c_28).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

half3 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
float3 tmpvar_4;
tmpvar_4 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_5;
tmpvar_5[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_5[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_5[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_6;
tmpvar_6 = normalize((mul(tmpvar_5, v._glesNormal)));
float3 lightColor0_7;
lightColor0_7 = (unity_LightColor[0]).xyz;
float3 lightColor1_8;
lightColor1_8 = (unity_LightColor[1]).xyz;
float3 lightColor2_9;
lightColor2_9 = (unity_LightColor[2]).xyz;
float3 lightColor3_10;
lightColor3_10 = (unity_LightColor[3]).xyz;
float4 lightAttenSq_11;
lightAttenSq_11 = unity_4LightAtten0;
float3 col_12;
float4 ndotl_13;
float4 lengthSq_14;
float4 tmpvar_15;
tmpvar_15 = ((unity_4LightPosX0 - (tmpvar_4).x));
float4 tmpvar_16;
tmpvar_16 = ((unity_4LightPosY0 - (tmpvar_4).y));
float4 tmpvar_17;
tmpvar_17 = ((unity_4LightPosZ0 - (tmpvar_4).z));
lengthSq_14 = ((tmpvar_15 * tmpvar_15));
lengthSq_14 = ((lengthSq_14 + ((tmpvar_16 * tmpvar_16))));
lengthSq_14 = ((lengthSq_14 + ((tmpvar_17 * tmpvar_17))));
float4 tmpvar_18;
tmpvar_18 = max(lengthSq_14, float4(1e-06, 1e-06, 1e-06, 1e-06));
lengthSq_14 = tmpvar_18;
ndotl_13 = ((tmpvar_15 * (tmpvar_6).x));
ndotl_13 = ((ndotl_13 + ((tmpvar_16 * (tmpvar_6).y))));
ndotl_13 = ((ndotl_13 + ((tmpvar_17 * (tmpvar_6).z))));
float4 tmpvar_19;
tmpvar_19 = max(float4(0.0, 0.0, 0.0, 0.0), ((ndotl_13 * rsqrt(tmpvar_18))));
ndotl_13 = tmpvar_19;
float4 tmpvar_20;
tmpvar_20 = ((tmpvar_19 * ((1.0 / (((1.0 + ((tmpvar_18 * lightAttenSq_11)))))))));
col_12 = ((lightColor0_7 * (tmpvar_20).x));
col_12 = ((col_12 + ((lightColor1_8 * (tmpvar_20).y))));
col_12 = ((col_12 + ((lightColor2_9 * (tmpvar_20).z))));
col_12 = ((col_12 + ((lightColor3_10 * (tmpvar_20).w))));
tmpvar_1 = col_12;
half3 normal_21;
normal_21 = tmpvar_6;
half3 ambient_22;
half3 x1_23;
half4 tmpvar_24;
tmpvar_24 = (((normal_21).xyzz * (normal_21).yzzx));
(x1_23).x = dot(unity_SHBr, tmpvar_24);
(x1_23).y = dot(unity_SHBg, tmpvar_24);
(x1_23).z = dot(unity_SHBb, tmpvar_24);
ambient_22 = ((((tmpvar_1 * ((((tmpvar_1 * ((((tmpvar_1 * 0.305306)) + 0.6821711)))) + 0.01252288)))) + ((x1_23 + (((unity_SHC).xyz * (((((normal_21).x * (normal_21).x)) - (((normal_21).y * (normal_21).y))))))))));
tmpvar_1 = ambient_22;
o.position = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_6;
o.xlv_TEXCOORD2 = tmpvar_4;
o.xlv_TEXCOORD3 = ambient_22;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD6 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = min(clamp(((realtimeShadowAttenuation_13 + tmpvar_17)), 0.0, 1.0), shadowMaskAttenuation_14);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_27;
normalWorld_27 = tmpvar_6;
half3 tmpvar_28;
tmpvar_28 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_29;
(tmpvar_29).w = 1.0;
(tmpvar_29).xyz = normalWorld_27;
half3 x_30;
(x_30).x = dot(unity_SHAr, tmpvar_29);
(x_30).y = dot(unity_SHAg, tmpvar_29);
(x_30).z = dot(unity_SHAb, tmpvar_29);
half3 tmpvar_31;
tmpvar_31 = max(((((1.055 * pow(max(float3(0.0, 0.0, 0.0), ((i.xlv_TEXCOORD3 + x_30))), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
tmpvar_2 = tmpvar_28;
half3 viewDir_32;
viewDir_32 = tmpvar_9;
fixed4 c_33;
fixed4 c_34;
float nh_35;
fixed diff_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_36 = tmpvar_37;
half tmpvar_38;
tmpvar_38 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_32)))));
nh_35 = tmpvar_38;
half y_39;
y_39 = ((_Shininess * 128.0));
float tmpvar_40;
tmpvar_40 = ((pow(nh_35, y_39) * (tmpvar_12).w));
(c_34).xyz = ((((((tmpvar_11 * tmpvar_28)) * diff_36)) + ((((tmpvar_28 * (_SpecColor).xyz)) * tmpvar_40))));
(c_34).w = (((tmpvar_12).w * (_Color).w));
(c_33).w = (c_34).w;
(c_33).xyz = (((c_34).xyz + ((tmpvar_11 * tmpvar_31))));
(c_4).xyz = (c_33).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float2 xlv_TEXCOORD4 : TEXCOORD3;
float4 xlv_TEXCOORD6 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = min(clamp(((realtimeShadowAttenuation_13 + tmpvar_17)), 0.0, 1.0), shadowMaskAttenuation_14);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_27;
normalWorld_27 = tmpvar_6;
half4 realtimeDirTex_28;
half3 tmpvar_29;
half3 tmpvar_30;
tmpvar_29 = ((tmpvar_2 * tmpvar_1));
fixed4 tmpvar_31;
tmpvar_31 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
fixed4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
realtimeDirTex_28 = tmpvar_32;
fixed4 dirTex_33;
dirTex_33 = realtimeDirTex_28;
fixed3 y_34;
y_34 = (((dirTex_33).xyz - 0.5));
fixed tmpvar_35;
tmpvar_35 = max(0.0001, (dirTex_33).w);
tmpvar_30 = ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_31).w)) * (tmpvar_31).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_27, y_34) + 0.5)))) / tmpvar_35));
tmpvar_2 = tmpvar_29;
half3 viewDir_36;
viewDir_36 = tmpvar_9;
fixed4 c_37;
fixed4 c_38;
float nh_39;
fixed diff_40;
half tmpvar_41;
tmpvar_41 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_40 = tmpvar_41;
half tmpvar_42;
tmpvar_42 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_36)))));
nh_39 = tmpvar_42;
half y_43;
y_43 = ((_Shininess * 128.0));
float tmpvar_44;
tmpvar_44 = ((pow(nh_39, y_43) * (tmpvar_12).w));
(c_38).xyz = ((((((tmpvar_11 * tmpvar_29)) * diff_40)) + ((((tmpvar_29 * (_SpecColor).xyz)) * tmpvar_44))));
(c_38).w = (((tmpvar_12).w * (_Color).w));
(c_37).w = (c_38).w;
(c_37).xyz = (((c_38).xyz + ((tmpvar_11 * tmpvar_30))));
(c_4).xyz = (c_37).xyz;
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float xlv_TEXCOORD5 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
tmpvar_1 = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = tmpvar_1;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_1).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
float3 worldViewDir_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
worldViewDir_5 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_4 = i.xlv_TEXCOORD1;
fixed4 tmpvar_8;
tmpvar_8 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_6;
half3 viewDir_9;
viewDir_9 = worldViewDir_5;
fixed4 c_10;
float nh_11;
fixed diff_12;
half tmpvar_13;
tmpvar_13 = max(0.0, dot(tmpvar_4, tmpvar_2));
diff_12 = tmpvar_13;
half tmpvar_14;
tmpvar_14 = max(0.0, dot(tmpvar_4, normalize(((tmpvar_2 + viewDir_9)))));
nh_11 = tmpvar_14;
half y_15;
y_15 = ((_Shininess * 128.0));
float tmpvar_16;
tmpvar_16 = ((pow(nh_11, y_15) * (tmpvar_8).w));
(c_10).xyz = (((((((tmpvar_8).xyz * (_Color).xyz)) * ((tmpvar_1 * diff_12)))) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_16))));
(c_10).w = (((tmpvar_8).w * (_Color).w));
float tmpvar_17;
tmpvar_17 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_3).xyz = lerp((unity_FogColor).xyz, (c_10).xyz, ((float3)(tmpvar_17)));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
tmpvar_1 = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_4;
tmpvar_4 = normalize((mul(tmpvar_3, v._glesNormal)));
half3 normal_5;
normal_5 = tmpvar_4;
half4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = normal_5;
half3 res_7;
half3 x_8;
(x_8).x = dot(unity_SHAr, tmpvar_6);
(x_8).y = dot(unity_SHAg, tmpvar_6);
(x_8).z = dot(unity_SHAb, tmpvar_6);
half3 x1_9;
half4 tmpvar_10;
tmpvar_10 = (((normal_5).xyzz * (normal_5).yzzx));
(x1_9).x = dot(unity_SHBr, tmpvar_10);
(x1_9).y = dot(unity_SHBg, tmpvar_10);
(x1_9).z = dot(unity_SHBb, tmpvar_10);
res_7 = ((x_8 + ((x1_9 + (((unity_SHC).xyz * (((((normal_5).x * (normal_5).x)) - (((normal_5).y * (normal_5).y))))))))));
half3 tmpvar_11;
tmpvar_11 = max(((((1.055 * pow(max(res_7, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_7 = tmpvar_11;
o.position = tmpvar_1;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_4;
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = max(float3(0.0, 0.0, 0.0), tmpvar_11);
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_1).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 viewDir_10;
viewDir_10 = worldViewDir_6;
fixed4 c_11;
fixed4 c_12;
float nh_13;
fixed diff_14;
half tmpvar_15;
tmpvar_15 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_14 = tmpvar_15;
half tmpvar_16;
tmpvar_16 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_10)))));
nh_13 = tmpvar_16;
half y_17;
y_17 = ((_Shininess * 128.0));
float tmpvar_18;
tmpvar_18 = ((pow(nh_13, y_17) * (tmpvar_9).w));
(c_12).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_14)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_18))));
(c_12).w = (((tmpvar_9).w * (_Color).w));
(c_11).w = (c_12).w;
(c_11).xyz = (((c_12).xyz + ((tmpvar_4 * i.xlv_TEXCOORD3))));
float tmpvar_19;
tmpvar_19 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_3).xyz = lerp((unity_FogColor).xyz, (c_11).xyz, ((float3)(tmpvar_19)));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
tmpvar_1 = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_4;
tmpvar_4 = normalize((mul(tmpvar_3, v._glesNormal)));
half3 normal_5;
normal_5 = tmpvar_4;
half4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = normal_5;
half3 res_7;
half3 x_8;
(x_8).x = dot(unity_SHAr, tmpvar_6);
(x_8).y = dot(unity_SHAg, tmpvar_6);
(x_8).z = dot(unity_SHAb, tmpvar_6);
half3 x1_9;
half4 tmpvar_10;
tmpvar_10 = (((normal_5).xyzz * (normal_5).yzzx));
(x1_9).x = dot(unity_SHBr, tmpvar_10);
(x1_9).y = dot(unity_SHBg, tmpvar_10);
(x1_9).z = dot(unity_SHBb, tmpvar_10);
res_7 = ((x_8 + ((x1_9 + (((unity_SHC).xyz * (((((normal_5).x * (normal_5).x)) - (((normal_5).y * (normal_5).y))))))))));
half3 tmpvar_11;
tmpvar_11 = max(((((1.055 * pow(max(res_7, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_7 = tmpvar_11;
o.position = tmpvar_1;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_4;
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = max(float3(0.0, 0.0, 0.0), tmpvar_11);
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_1).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 tmpvar_14;
tmpvar_14 = ((tmpvar_2 * tmpvar_1));
tmpvar_2 = tmpvar_14;
half3 viewDir_15;
viewDir_15 = worldViewDir_8;
fixed4 c_16;
fixed4 c_17;
float nh_18;
fixed diff_19;
half tmpvar_20;
tmpvar_20 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_19 = tmpvar_20;
half tmpvar_21;
tmpvar_21 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_15)))));
nh_18 = tmpvar_21;
half y_22;
y_22 = ((_Shininess * 128.0));
float tmpvar_23;
tmpvar_23 = ((pow(nh_18, y_22) * (tmpvar_11).w));
(c_17).xyz = ((((((tmpvar_6 * tmpvar_14)) * diff_19)) + ((((tmpvar_14 * (_SpecColor).xyz)) * tmpvar_23))));
(c_17).w = (((tmpvar_11).w * (_Color).w));
(c_16).w = (c_17).w;
(c_16).xyz = (((c_17).xyz + ((tmpvar_6 * i.xlv_TEXCOORD3))));
float tmpvar_24;
tmpvar_24 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_16).xyz, ((float3)(tmpvar_24)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float xlv_TEXCOORD5 : TEXCOORD3;
float4 xlv_TEXCOORD6 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
tmpvar_10 = pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_11).w)) * (tmpvar_11).xyz)), (unity_DynamicLightmap_HDR).yyy);
half3 viewDir_12;
viewDir_12 = worldViewDir_6;
fixed4 c_13;
fixed4 c_14;
float nh_15;
fixed diff_16;
half tmpvar_17;
tmpvar_17 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_16 = tmpvar_17;
half tmpvar_18;
tmpvar_18 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_12)))));
nh_15 = tmpvar_18;
half y_19;
y_19 = ((_Shininess * 128.0));
float tmpvar_20;
tmpvar_20 = ((pow(nh_15, y_19) * (tmpvar_9).w));
(c_14).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_16)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_20))));
(c_14).w = (((tmpvar_9).w * (_Color).w));
(c_13).w = (c_14).w;
(c_13).xyz = (((c_14).xyz + ((tmpvar_4 * tmpvar_10))));
float tmpvar_21;
tmpvar_21 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_3).xyz = lerp((unity_FogColor).xyz, (c_13).xyz, ((float3)(tmpvar_21)));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 tmpvar_10;
half4 tmpvar_11;
tmpvar_11 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_12;
color_12 = tmpvar_11;
tmpvar_10 = (((unity_Lightmap_HDR).x * (color_12).xyz));
half3 viewDir_13;
viewDir_13 = worldViewDir_6;
fixed4 c_14;
fixed4 c_15;
float nh_16;
fixed diff_17;
half tmpvar_18;
tmpvar_18 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_17 = tmpvar_18;
half tmpvar_19;
tmpvar_19 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_13)))));
nh_16 = tmpvar_19;
half y_20;
y_20 = ((_Shininess * 128.0));
float tmpvar_21;
tmpvar_21 = ((pow(nh_16, y_20) * (tmpvar_9).w));
(c_15).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_17)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_21))));
(c_15).w = (((tmpvar_9).w * (_Color).w));
(c_14).w = (c_15).w;
(c_14).xyz = (((c_15).xyz + ((tmpvar_4 * tmpvar_10))));
float tmpvar_22;
tmpvar_22 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_3).xyz = lerp((unity_FogColor).xyz, (c_14).xyz, ((float3)(tmpvar_22)));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 tmpvar_10;
half4 tmpvar_11;
tmpvar_11 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_12;
color_12 = tmpvar_11;
tmpvar_10 = (((unity_Lightmap_HDR).x * (color_12).xyz));
fixed4 tmpvar_13;
tmpvar_13 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_10 = ((tmpvar_10 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_13).w)) * (tmpvar_13).xyz)), (unity_DynamicLightmap_HDR).yyy)));
half3 viewDir_14;
viewDir_14 = worldViewDir_6;
fixed4 c_15;
fixed4 c_16;
float nh_17;
fixed diff_18;
half tmpvar_19;
tmpvar_19 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_18 = tmpvar_19;
half tmpvar_20;
tmpvar_20 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_14)))));
nh_17 = tmpvar_20;
half y_21;
y_21 = ((_Shininess * 128.0));
float tmpvar_22;
tmpvar_22 = ((pow(nh_17, y_21) * (tmpvar_9).w));
(c_16).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_18)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_22))));
(c_16).w = (((tmpvar_9).w * (_Color).w));
(c_15).w = (c_16).w;
(c_15).xyz = (((c_16).xyz + ((tmpvar_4 * tmpvar_10))));
float tmpvar_23;
tmpvar_23 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_3).xyz = lerp((unity_FogColor).xyz, (c_15).xyz, ((float3)(tmpvar_23)));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 normalWorld_10;
normalWorld_10 = tmpvar_5;
half3 tmpvar_11;
half3 ambient_12;
half4 tmpvar_13;
(tmpvar_13).w = 1.0;
(tmpvar_13).xyz = normalWorld_10;
half3 x_14;
(x_14).x = dot(unity_SHAr, tmpvar_13);
(x_14).y = dot(unity_SHAg, tmpvar_13);
(x_14).z = dot(unity_SHAb, tmpvar_13);
half3 x1_15;
half4 tmpvar_16;
tmpvar_16 = (((normalWorld_10).xyzz * (normalWorld_10).yzzx));
(x1_15).x = dot(unity_SHBr, tmpvar_16);
(x1_15).y = dot(unity_SHBg, tmpvar_16);
(x1_15).z = dot(unity_SHBb, tmpvar_16);
ambient_12 = max(float3(0.0, 0.0, 0.0), ((x_14 + ((x1_15 + (((unity_SHC).xyz * (((((normalWorld_10).x * (normalWorld_10).x)) - (((normalWorld_10).y * (normalWorld_10).y)))))))))));
half3 tmpvar_17;
tmpvar_17 = max(((((1.055 * pow(max(ambient_12, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_12 = tmpvar_17;
half4 tmpvar_18;
tmpvar_18 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_19;
color_19 = tmpvar_18;
tmpvar_11 = ((tmpvar_17 + (((unity_Lightmap_HDR).x * (color_19).xyz))));
half3 viewDir_20;
viewDir_20 = worldViewDir_6;
fixed4 c_21;
fixed4 c_22;
float nh_23;
fixed diff_24;
half tmpvar_25;
tmpvar_25 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_24 = tmpvar_25;
half tmpvar_26;
tmpvar_26 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_20)))));
nh_23 = tmpvar_26;
half y_27;
y_27 = ((_Shininess * 128.0));
float tmpvar_28;
tmpvar_28 = ((pow(nh_23, y_27) * (tmpvar_9).w));
(c_22).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_24)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_28))));
(c_22).w = (((tmpvar_9).w * (_Color).w));
(c_21).w = (c_22).w;
(c_21).xyz = (((c_22).xyz + ((tmpvar_4 * tmpvar_11))));
float tmpvar_29;
tmpvar_29 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_3).xyz = lerp((unity_FogColor).xyz, (c_21).xyz, ((float3)(tmpvar_29)));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 c_1;
fixed4 tmpvar_2;
tmpvar_2 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half3 tmpvar_3;
half4 tmpvar_4;
tmpvar_4 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_5;
color_5 = tmpvar_4;
tmpvar_3 = (((unity_Lightmap_HDR).x * (color_5).xyz));
fixed4 c_6;
fixed4 c_7;
(c_7).xyz = float3(0.0, 0.0, 0.0);
(c_7).w = (((tmpvar_2).w * (_Color).w));
(c_6).w = (c_7).w;
(c_6).xyz = (((((tmpvar_2).xyz * (_Color).xyz)) * tmpvar_3));
float tmpvar_8;
tmpvar_8 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_1).xyz = lerp((unity_FogColor).xyz, (c_6).xyz, ((float3)(tmpvar_8)));
(c_1).w = 1.0;
o.target0 = c_1;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 c_1;
fixed4 tmpvar_2;
tmpvar_2 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half3 tmpvar_3;
half4 tmpvar_4;
tmpvar_4 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_5;
color_5 = tmpvar_4;
tmpvar_3 = (((unity_Lightmap_HDR).x * (color_5).xyz));
fixed4 tmpvar_6;
tmpvar_6 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_3 = ((tmpvar_3 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_6).w)) * (tmpvar_6).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_7;
fixed4 c_8;
(c_8).xyz = float3(0.0, 0.0, 0.0);
(c_8).w = (((tmpvar_2).w * (_Color).w));
(c_7).w = (c_8).w;
(c_7).xyz = (((((tmpvar_2).xyz * (_Color).xyz)) * tmpvar_3));
float tmpvar_9;
tmpvar_9 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_1).xyz = lerp((unity_FogColor).xyz, (c_7).xyz, ((float3)(tmpvar_9)));
(c_1).w = 1.0;
o.target0 = c_1;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 c_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half3 normalWorld_4;
normalWorld_4 = tmpvar_2;
half3 tmpvar_5;
half3 ambient_6;
half4 tmpvar_7;
(tmpvar_7).w = 1.0;
(tmpvar_7).xyz = normalWorld_4;
half3 x_8;
(x_8).x = dot(unity_SHAr, tmpvar_7);
(x_8).y = dot(unity_SHAg, tmpvar_7);
(x_8).z = dot(unity_SHAb, tmpvar_7);
half3 x1_9;
half4 tmpvar_10;
tmpvar_10 = (((normalWorld_4).xyzz * (normalWorld_4).yzzx));
(x1_9).x = dot(unity_SHBr, tmpvar_10);
(x1_9).y = dot(unity_SHBg, tmpvar_10);
(x1_9).z = dot(unity_SHBb, tmpvar_10);
ambient_6 = max(float3(0.0, 0.0, 0.0), ((x_8 + ((x1_9 + (((unity_SHC).xyz * (((((normalWorld_4).x * (normalWorld_4).x)) - (((normalWorld_4).y * (normalWorld_4).y)))))))))));
half3 tmpvar_11;
tmpvar_11 = max(((((1.055 * pow(max(ambient_6, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_6 = tmpvar_11;
half4 tmpvar_12;
tmpvar_12 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_13;
color_13 = tmpvar_12;
tmpvar_5 = ((tmpvar_11 + (((unity_Lightmap_HDR).x * (color_13).xyz))));
fixed4 c_14;
fixed4 c_15;
(c_15).xyz = float3(0.0, 0.0, 0.0);
(c_15).w = (((tmpvar_3).w * (_Color).w));
(c_14).w = (c_15).w;
(c_14).xyz = (((((tmpvar_3).xyz * (_Color).xyz)) * tmpvar_5));
float tmpvar_16;
tmpvar_16 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_1).xyz = lerp((unity_FogColor).xyz, (c_14).xyz, ((float3)(tmpvar_16)));
(c_1).w = 1.0;
o.target0 = c_1;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float xlv_TEXCOORD5 : TEXCOORD3;
float4 xlv_TEXCOORD6 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 normalWorld_10;
normalWorld_10 = tmpvar_5;
half4 realtimeDirTex_11;
half3 tmpvar_12;
fixed4 tmpvar_13;
tmpvar_13 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
fixed4 tmpvar_14;
tmpvar_14 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
realtimeDirTex_11 = tmpvar_14;
fixed4 dirTex_15;
dirTex_15 = realtimeDirTex_11;
fixed3 y_16;
y_16 = (((dirTex_15).xyz - 0.5));
fixed tmpvar_17;
tmpvar_17 = max(0.0001, (dirTex_15).w);
tmpvar_12 = ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_13).w)) * (tmpvar_13).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_10, y_16) + 0.5)))) / tmpvar_17));
half3 viewDir_18;
viewDir_18 = worldViewDir_6;
fixed4 c_19;
fixed4 c_20;
float nh_21;
fixed diff_22;
half tmpvar_23;
tmpvar_23 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_22 = tmpvar_23;
half tmpvar_24;
tmpvar_24 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_18)))));
nh_21 = tmpvar_24;
half y_25;
y_25 = ((_Shininess * 128.0));
float tmpvar_26;
tmpvar_26 = ((pow(nh_21, y_25) * (tmpvar_9).w));
(c_20).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_22)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_26))));
(c_20).w = (((tmpvar_9).w * (_Color).w));
(c_19).w = (c_20).w;
(c_19).xyz = (((c_20).xyz + ((tmpvar_4 * tmpvar_12))));
float tmpvar_27;
tmpvar_27 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_3).xyz = lerp((unity_FogColor).xyz, (c_19).xyz, ((float3)(tmpvar_27)));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 normalWorld_10;
normalWorld_10 = tmpvar_5;
fixed4 bakedDirTex_11;
half3 tmpvar_12;
half4 tmpvar_13;
tmpvar_13 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_14;
color_14 = tmpvar_13;
half4 tmpvar_15;
tmpvar_15 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_11 = tmpvar_15;
fixed3 y_16;
y_16 = (((bakedDirTex_11).xyz - 0.5));
fixed tmpvar_17;
tmpvar_17 = max(0.0001, (bakedDirTex_11).w);
tmpvar_12 = (((((((unity_Lightmap_HDR).x * (color_14).xyz)) * ((dot(normalWorld_10, y_16) + 0.5)))) / tmpvar_17));
half3 viewDir_18;
viewDir_18 = worldViewDir_6;
fixed4 c_19;
fixed4 c_20;
float nh_21;
fixed diff_22;
half tmpvar_23;
tmpvar_23 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_22 = tmpvar_23;
half tmpvar_24;
tmpvar_24 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_18)))));
nh_21 = tmpvar_24;
half y_25;
y_25 = ((_Shininess * 128.0));
float tmpvar_26;
tmpvar_26 = ((pow(nh_21, y_25) * (tmpvar_9).w));
(c_20).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_22)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_26))));
(c_20).w = (((tmpvar_9).w * (_Color).w));
(c_19).w = (c_20).w;
(c_19).xyz = (((c_20).xyz + ((tmpvar_4 * tmpvar_12))));
float tmpvar_27;
tmpvar_27 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_3).xyz = lerp((unity_FogColor).xyz, (c_19).xyz, ((float3)(tmpvar_27)));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 normalWorld_10;
normalWorld_10 = tmpvar_5;
fixed4 bakedDirTex_11;
half3 tmpvar_12;
half3 ambient_13;
half4 tmpvar_14;
(tmpvar_14).w = 1.0;
(tmpvar_14).xyz = normalWorld_10;
half3 x_15;
(x_15).x = dot(unity_SHAr, tmpvar_14);
(x_15).y = dot(unity_SHAg, tmpvar_14);
(x_15).z = dot(unity_SHAb, tmpvar_14);
half3 x1_16;
half4 tmpvar_17;
tmpvar_17 = (((normalWorld_10).xyzz * (normalWorld_10).yzzx));
(x1_16).x = dot(unity_SHBr, tmpvar_17);
(x1_16).y = dot(unity_SHBg, tmpvar_17);
(x1_16).z = dot(unity_SHBb, tmpvar_17);
ambient_13 = max(float3(0.0, 0.0, 0.0), ((x_15 + ((x1_16 + (((unity_SHC).xyz * (((((normalWorld_10).x * (normalWorld_10).x)) - (((normalWorld_10).y * (normalWorld_10).y)))))))))));
half3 tmpvar_18;
tmpvar_18 = max(((((1.055 * pow(max(ambient_13, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_13 = tmpvar_18;
half4 tmpvar_19;
tmpvar_19 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_20;
color_20 = tmpvar_19;
half4 tmpvar_21;
tmpvar_21 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_11 = tmpvar_21;
fixed3 y_22;
y_22 = (((bakedDirTex_11).xyz - 0.5));
fixed tmpvar_23;
tmpvar_23 = max(0.0001, (bakedDirTex_11).w);
tmpvar_12 = ((tmpvar_18 + (((((((unity_Lightmap_HDR).x * (color_20).xyz)) * ((dot(normalWorld_10, y_22) + 0.5)))) / tmpvar_23))));
half3 viewDir_24;
viewDir_24 = worldViewDir_6;
fixed4 c_25;
fixed4 c_26;
float nh_27;
fixed diff_28;
half tmpvar_29;
tmpvar_29 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_28 = tmpvar_29;
half tmpvar_30;
tmpvar_30 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_24)))));
nh_27 = tmpvar_30;
half y_31;
y_31 = ((_Shininess * 128.0));
float tmpvar_32;
tmpvar_32 = ((pow(nh_27, y_31) * (tmpvar_9).w));
(c_26).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_28)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_32))));
(c_26).w = (((tmpvar_9).w * (_Color).w));
(c_25).w = (c_26).w;
(c_25).xyz = (((c_26).xyz + ((tmpvar_4 * tmpvar_12))));
float tmpvar_33;
tmpvar_33 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_3).xyz = lerp((unity_FogColor).xyz, (c_25).xyz, ((float3)(tmpvar_33)));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 normalWorld_10;
normalWorld_10 = tmpvar_5;
half4 realtimeDirTex_11;
fixed4 bakedDirTex_12;
half3 tmpvar_13;
half4 tmpvar_14;
tmpvar_14 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_15;
color_15 = tmpvar_14;
half4 tmpvar_16;
tmpvar_16 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_12 = tmpvar_16;
fixed3 y_17;
y_17 = (((bakedDirTex_12).xyz - 0.5));
fixed tmpvar_18;
tmpvar_18 = max(0.0001, (bakedDirTex_12).w);
tmpvar_13 = (((((((unity_Lightmap_HDR).x * (color_15).xyz)) * ((dot(normalWorld_10, y_17) + 0.5)))) / tmpvar_18));
fixed4 tmpvar_19;
tmpvar_19 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_20;
tmpvar_20 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_11 = tmpvar_20;
fixed4 dirTex_21;
dirTex_21 = realtimeDirTex_11;
fixed3 y_22;
y_22 = (((dirTex_21).xyz - 0.5));
fixed tmpvar_23;
tmpvar_23 = max(0.0001, (dirTex_21).w);
tmpvar_13 = ((tmpvar_13 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_19).w)) * (tmpvar_19).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_10, y_22) + 0.5)))) / tmpvar_23))));
half3 viewDir_24;
viewDir_24 = worldViewDir_6;
fixed4 c_25;
fixed4 c_26;
float nh_27;
fixed diff_28;
half tmpvar_29;
tmpvar_29 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_28 = tmpvar_29;
half tmpvar_30;
tmpvar_30 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_24)))));
nh_27 = tmpvar_30;
half y_31;
y_31 = ((_Shininess * 128.0));
float tmpvar_32;
tmpvar_32 = ((pow(nh_27, y_31) * (tmpvar_9).w));
(c_26).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_28)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_32))));
(c_26).w = (((tmpvar_9).w * (_Color).w));
(c_25).w = (c_26).w;
(c_25).xyz = (((c_26).xyz + ((tmpvar_4 * tmpvar_13))));
float tmpvar_33;
tmpvar_33 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_3).xyz = lerp((unity_FogColor).xyz, (c_25).xyz, ((float3)(tmpvar_33)));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 c_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half3 normalWorld_4;
normalWorld_4 = tmpvar_2;
fixed4 bakedDirTex_5;
half3 tmpvar_6;
half4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_8;
color_8 = tmpvar_7;
half4 tmpvar_9;
tmpvar_9 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_5 = tmpvar_9;
fixed3 y_10;
y_10 = (((bakedDirTex_5).xyz - 0.5));
fixed tmpvar_11;
tmpvar_11 = max(0.0001, (bakedDirTex_5).w);
tmpvar_6 = (((((((unity_Lightmap_HDR).x * (color_8).xyz)) * ((dot(normalWorld_4, y_10) + 0.5)))) / tmpvar_11));
fixed4 c_12;
fixed4 c_13;
(c_13).xyz = float3(0.0, 0.0, 0.0);
(c_13).w = (((tmpvar_3).w * (_Color).w));
(c_12).w = (c_13).w;
(c_12).xyz = (((((tmpvar_3).xyz * (_Color).xyz)) * tmpvar_6));
float tmpvar_14;
tmpvar_14 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_1).xyz = lerp((unity_FogColor).xyz, (c_12).xyz, ((float3)(tmpvar_14)));
(c_1).w = 1.0;
o.target0 = c_1;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 c_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half3 normalWorld_4;
normalWorld_4 = tmpvar_2;
fixed4 bakedDirTex_5;
half3 tmpvar_6;
half3 ambient_7;
half4 tmpvar_8;
(tmpvar_8).w = 1.0;
(tmpvar_8).xyz = normalWorld_4;
half3 x_9;
(x_9).x = dot(unity_SHAr, tmpvar_8);
(x_9).y = dot(unity_SHAg, tmpvar_8);
(x_9).z = dot(unity_SHAb, tmpvar_8);
half3 x1_10;
half4 tmpvar_11;
tmpvar_11 = (((normalWorld_4).xyzz * (normalWorld_4).yzzx));
(x1_10).x = dot(unity_SHBr, tmpvar_11);
(x1_10).y = dot(unity_SHBg, tmpvar_11);
(x1_10).z = dot(unity_SHBb, tmpvar_11);
ambient_7 = max(float3(0.0, 0.0, 0.0), ((x_9 + ((x1_10 + (((unity_SHC).xyz * (((((normalWorld_4).x * (normalWorld_4).x)) - (((normalWorld_4).y * (normalWorld_4).y)))))))))));
half3 tmpvar_12;
tmpvar_12 = max(((((1.055 * pow(max(ambient_7, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_7 = tmpvar_12;
half4 tmpvar_13;
tmpvar_13 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_14;
color_14 = tmpvar_13;
half4 tmpvar_15;
tmpvar_15 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_5 = tmpvar_15;
fixed3 y_16;
y_16 = (((bakedDirTex_5).xyz - 0.5));
fixed tmpvar_17;
tmpvar_17 = max(0.0001, (bakedDirTex_5).w);
tmpvar_6 = ((tmpvar_12 + (((((((unity_Lightmap_HDR).x * (color_14).xyz)) * ((dot(normalWorld_4, y_16) + 0.5)))) / tmpvar_17))));
fixed4 c_18;
fixed4 c_19;
(c_19).xyz = float3(0.0, 0.0, 0.0);
(c_19).w = (((tmpvar_3).w * (_Color).w));
(c_18).w = (c_19).w;
(c_18).xyz = (((((tmpvar_3).xyz * (_Color).xyz)) * tmpvar_6));
float tmpvar_20;
tmpvar_20 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_1).xyz = lerp((unity_FogColor).xyz, (c_18).xyz, ((float3)(tmpvar_20)));
(c_1).w = 1.0;
o.target0 = c_1;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 c_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half3 normalWorld_4;
normalWorld_4 = tmpvar_2;
half4 realtimeDirTex_5;
fixed4 bakedDirTex_6;
half3 tmpvar_7;
half4 tmpvar_8;
tmpvar_8 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_9;
color_9 = tmpvar_8;
half4 tmpvar_10;
tmpvar_10 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_6 = tmpvar_10;
fixed3 y_11;
y_11 = (((bakedDirTex_6).xyz - 0.5));
fixed tmpvar_12;
tmpvar_12 = max(0.0001, (bakedDirTex_6).w);
tmpvar_7 = (((((((unity_Lightmap_HDR).x * (color_9).xyz)) * ((dot(normalWorld_4, y_11) + 0.5)))) / tmpvar_12));
fixed4 tmpvar_13;
tmpvar_13 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_14;
tmpvar_14 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_5 = tmpvar_14;
fixed4 dirTex_15;
dirTex_15 = realtimeDirTex_5;
fixed3 y_16;
y_16 = (((dirTex_15).xyz - 0.5));
fixed tmpvar_17;
tmpvar_17 = max(0.0001, (dirTex_15).w);
tmpvar_7 = ((tmpvar_7 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_13).w)) * (tmpvar_13).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_4, y_16) + 0.5)))) / tmpvar_17))));
fixed4 c_18;
fixed4 c_19;
(c_19).xyz = float3(0.0, 0.0, 0.0);
(c_19).w = (((tmpvar_3).w * (_Color).w));
(c_18).w = (c_19).w;
(c_18).xyz = (((((tmpvar_3).xyz * (_Color).xyz)) * tmpvar_7));
float tmpvar_20;
tmpvar_20 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_1).xyz = lerp((unity_FogColor).xyz, (c_18).xyz, ((float3)(tmpvar_20)));
(c_1).w = 1.0;
o.target0 = c_1;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
float2 tmpvar_5;
tmpvar_5 = (((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy));
(tmpvar_1).xy = ((tmpvar_5 + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = ((tmpvar_5 + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 tmpvar_14;
half3 tmpvar_15;
tmpvar_14 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_16;
tmpvar_16 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_17;
color_17 = tmpvar_16;
tmpvar_15 = (((unity_Lightmap_HDR).x * (color_17).xyz));
tmpvar_2 = tmpvar_14;
half3 viewDir_18;
viewDir_18 = worldViewDir_8;
fixed4 c_19;
fixed4 c_20;
float nh_21;
fixed diff_22;
half tmpvar_23;
tmpvar_23 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_22 = tmpvar_23;
half tmpvar_24;
tmpvar_24 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_18)))));
nh_21 = tmpvar_24;
half y_25;
y_25 = ((_Shininess * 128.0));
float tmpvar_26;
tmpvar_26 = ((pow(nh_21, y_25) * (tmpvar_11).w));
(c_20).xyz = ((((((tmpvar_6 * tmpvar_14)) * diff_22)) + ((((tmpvar_14 * (_SpecColor).xyz)) * tmpvar_26))));
(c_20).w = (((tmpvar_11).w * (_Color).w));
(c_19).w = (c_20).w;
(c_19).xyz = (((c_20).xyz + ((tmpvar_6 * tmpvar_15))));
float tmpvar_27;
tmpvar_27 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_19).xyz, ((float3)(tmpvar_27)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
float2 tmpvar_5;
tmpvar_5 = (((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy));
(tmpvar_1).xy = ((tmpvar_5 + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = ((tmpvar_5 + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 tmpvar_14;
half3 tmpvar_15;
tmpvar_14 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_16;
tmpvar_16 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_17;
color_17 = tmpvar_16;
tmpvar_15 = (((unity_Lightmap_HDR).x * (color_17).xyz));
fixed4 tmpvar_18;
tmpvar_18 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_15 = ((tmpvar_15 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_18).w)) * (tmpvar_18).xyz)), (unity_DynamicLightmap_HDR).yyy)));
tmpvar_2 = tmpvar_14;
half3 viewDir_19;
viewDir_19 = worldViewDir_8;
fixed4 c_20;
fixed4 c_21;
float nh_22;
fixed diff_23;
half tmpvar_24;
tmpvar_24 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_23 = tmpvar_24;
half tmpvar_25;
tmpvar_25 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_19)))));
nh_22 = tmpvar_25;
half y_26;
y_26 = ((_Shininess * 128.0));
float tmpvar_27;
tmpvar_27 = ((pow(nh_22, y_26) * (tmpvar_11).w));
(c_21).xyz = ((((((tmpvar_6 * tmpvar_14)) * diff_23)) + ((((tmpvar_14 * (_SpecColor).xyz)) * tmpvar_27))));
(c_21).w = (((tmpvar_11).w * (_Color).w));
(c_20).w = (c_21).w;
(c_20).xyz = (((c_21).xyz + ((tmpvar_6 * tmpvar_15))));
float tmpvar_28;
tmpvar_28 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_20).xyz, ((float3)(tmpvar_28)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
float2 tmpvar_5;
tmpvar_5 = (((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy));
(tmpvar_1).xy = ((tmpvar_5 + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = ((tmpvar_5 + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 normalWorld_14;
normalWorld_14 = tmpvar_7;
half3 tmpvar_15;
half3 tmpvar_16;
tmpvar_15 = ((tmpvar_2 * tmpvar_1));
half3 ambient_17;
half4 tmpvar_18;
(tmpvar_18).w = 1.0;
(tmpvar_18).xyz = normalWorld_14;
half3 x_19;
(x_19).x = dot(unity_SHAr, tmpvar_18);
(x_19).y = dot(unity_SHAg, tmpvar_18);
(x_19).z = dot(unity_SHAb, tmpvar_18);
half3 x1_20;
half4 tmpvar_21;
tmpvar_21 = (((normalWorld_14).xyzz * (normalWorld_14).yzzx));
(x1_20).x = dot(unity_SHBr, tmpvar_21);
(x1_20).y = dot(unity_SHBg, tmpvar_21);
(x1_20).z = dot(unity_SHBb, tmpvar_21);
ambient_17 = max(float3(0.0, 0.0, 0.0), ((x_19 + ((x1_20 + (((unity_SHC).xyz * (((((normalWorld_14).x * (normalWorld_14).x)) - (((normalWorld_14).y * (normalWorld_14).y)))))))))));
half3 tmpvar_22;
tmpvar_22 = max(((((1.055 * pow(max(ambient_17, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_17 = tmpvar_22;
half4 tmpvar_23;
tmpvar_23 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_24;
color_24 = tmpvar_23;
tmpvar_16 = ((tmpvar_22 + (((unity_Lightmap_HDR).x * (color_24).xyz))));
tmpvar_2 = tmpvar_15;
half3 viewDir_25;
viewDir_25 = worldViewDir_8;
fixed4 c_26;
fixed4 c_27;
float nh_28;
fixed diff_29;
half tmpvar_30;
tmpvar_30 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_29 = tmpvar_30;
half tmpvar_31;
tmpvar_31 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_25)))));
nh_28 = tmpvar_31;
half y_32;
y_32 = ((_Shininess * 128.0));
float tmpvar_33;
tmpvar_33 = ((pow(nh_28, y_32) * (tmpvar_11).w));
(c_27).xyz = ((((((tmpvar_6 * tmpvar_15)) * diff_29)) + ((((tmpvar_15 * (_SpecColor).xyz)) * tmpvar_33))));
(c_27).w = (((tmpvar_11).w * (_Color).w));
(c_26).w = (c_27).w;
(c_26).xyz = (((c_27).xyz + ((tmpvar_6 * tmpvar_16))));
float tmpvar_34;
tmpvar_34 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_26).xyz, ((float3)(tmpvar_34)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
float2 tmpvar_17;
tmpvar_17 = (((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy));
(tmpvar_4).xy = ((tmpvar_17 + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = ((tmpvar_17 + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 normalWorld_14;
normalWorld_14 = tmpvar_7;
fixed4 bakedDirTex_15;
half3 tmpvar_16;
half3 tmpvar_17;
tmpvar_16 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_18;
tmpvar_18 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_19;
color_19 = tmpvar_18;
half4 tmpvar_20;
tmpvar_20 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_15 = tmpvar_20;
fixed3 y_21;
y_21 = (((bakedDirTex_15).xyz - 0.5));
fixed tmpvar_22;
tmpvar_22 = max(0.0001, (bakedDirTex_15).w);
tmpvar_17 = (((((((unity_Lightmap_HDR).x * (color_19).xyz)) * ((dot(normalWorld_14, y_21) + 0.5)))) / tmpvar_22));
tmpvar_2 = tmpvar_16;
half3 viewDir_23;
viewDir_23 = worldViewDir_8;
fixed4 c_24;
fixed4 c_25;
float nh_26;
fixed diff_27;
half tmpvar_28;
tmpvar_28 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_27 = tmpvar_28;
half tmpvar_29;
tmpvar_29 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_23)))));
nh_26 = tmpvar_29;
half y_30;
y_30 = ((_Shininess * 128.0));
float tmpvar_31;
tmpvar_31 = ((pow(nh_26, y_30) * (tmpvar_11).w));
(c_25).xyz = ((((((tmpvar_6 * tmpvar_16)) * diff_27)) + ((((tmpvar_16 * (_SpecColor).xyz)) * tmpvar_31))));
(c_25).w = (((tmpvar_11).w * (_Color).w));
(c_24).w = (c_25).w;
(c_24).xyz = (((c_25).xyz + ((tmpvar_6 * tmpvar_17))));
float tmpvar_32;
tmpvar_32 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_24).xyz, ((float3)(tmpvar_32)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
float2 tmpvar_17;
tmpvar_17 = (((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy));
(tmpvar_4).xy = ((tmpvar_17 + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = ((tmpvar_17 + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 normalWorld_14;
normalWorld_14 = tmpvar_7;
half4 realtimeDirTex_15;
fixed4 bakedDirTex_16;
half3 tmpvar_17;
half3 tmpvar_18;
tmpvar_17 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_19;
tmpvar_19 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_20;
color_20 = tmpvar_19;
half4 tmpvar_21;
tmpvar_21 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_16 = tmpvar_21;
fixed3 y_22;
y_22 = (((bakedDirTex_16).xyz - 0.5));
fixed tmpvar_23;
tmpvar_23 = max(0.0001, (bakedDirTex_16).w);
tmpvar_18 = (((((((unity_Lightmap_HDR).x * (color_20).xyz)) * ((dot(normalWorld_14, y_22) + 0.5)))) / tmpvar_23));
fixed4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_25;
tmpvar_25 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_15 = tmpvar_25;
fixed4 dirTex_26;
dirTex_26 = realtimeDirTex_15;
fixed3 y_27;
y_27 = (((dirTex_26).xyz - 0.5));
fixed tmpvar_28;
tmpvar_28 = max(0.0001, (dirTex_26).w);
tmpvar_18 = ((tmpvar_18 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_24).w)) * (tmpvar_24).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_14, y_27) + 0.5)))) / tmpvar_28))));
tmpvar_2 = tmpvar_17;
half3 viewDir_29;
viewDir_29 = worldViewDir_8;
fixed4 c_30;
fixed4 c_31;
float nh_32;
fixed diff_33;
half tmpvar_34;
tmpvar_34 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_33 = tmpvar_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_29)))));
nh_32 = tmpvar_35;
half y_36;
y_36 = ((_Shininess * 128.0));
float tmpvar_37;
tmpvar_37 = ((pow(nh_32, y_36) * (tmpvar_11).w));
(c_31).xyz = ((((((tmpvar_6 * tmpvar_17)) * diff_33)) + ((((tmpvar_17 * (_SpecColor).xyz)) * tmpvar_37))));
(c_31).w = (((tmpvar_11).w * (_Color).w));
(c_30).w = (c_31).w;
(c_30).xyz = (((c_31).xyz + ((tmpvar_6 * tmpvar_18))));
float tmpvar_38;
tmpvar_38 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_30).xyz, ((float3)(tmpvar_38)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
float2 tmpvar_17;
tmpvar_17 = (((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy));
(tmpvar_4).xy = ((tmpvar_17 + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_9;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = ((tmpvar_17 + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 tmpvar_7;
float3 worldViewDir_8;
fixed3 lightDir_9;
half3 tmpvar_10;
tmpvar_10 = (_WorldSpaceLightPos0).xyz;
lightDir_9 = tmpvar_10;
worldViewDir_8 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_7 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_6 = (((tmpvar_11).xyz * (_Color).xyz));
half shadowMaskAttenuation_12;
fixed tmpvar_13;
tmpvar_13 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_12 = tmpvar_13;
atten_5 = shadowMaskAttenuation_12;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_9;
tmpvar_1 = atten_5;
half3 normalWorld_14;
normalWorld_14 = tmpvar_7;
fixed4 bakedDirTex_15;
half3 tmpvar_16;
half3 tmpvar_17;
tmpvar_16 = ((tmpvar_2 * tmpvar_1));
half3 ambient_18;
half4 tmpvar_19;
(tmpvar_19).w = 1.0;
(tmpvar_19).xyz = normalWorld_14;
half3 x_20;
(x_20).x = dot(unity_SHAr, tmpvar_19);
(x_20).y = dot(unity_SHAg, tmpvar_19);
(x_20).z = dot(unity_SHAb, tmpvar_19);
half3 x1_21;
half4 tmpvar_22;
tmpvar_22 = (((normalWorld_14).xyzz * (normalWorld_14).yzzx));
(x1_21).x = dot(unity_SHBr, tmpvar_22);
(x1_21).y = dot(unity_SHBg, tmpvar_22);
(x1_21).z = dot(unity_SHBb, tmpvar_22);
ambient_18 = max(float3(0.0, 0.0, 0.0), ((x_20 + ((x1_21 + (((unity_SHC).xyz * (((((normalWorld_14).x * (normalWorld_14).x)) - (((normalWorld_14).y * (normalWorld_14).y)))))))))));
half3 tmpvar_23;
tmpvar_23 = max(((((1.055 * pow(max(ambient_18, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_18 = tmpvar_23;
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_25;
color_25 = tmpvar_24;
half4 tmpvar_26;
tmpvar_26 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_15 = tmpvar_26;
fixed3 y_27;
y_27 = (((bakedDirTex_15).xyz - 0.5));
fixed tmpvar_28;
tmpvar_28 = max(0.0001, (bakedDirTex_15).w);
tmpvar_17 = ((tmpvar_23 + (((((((unity_Lightmap_HDR).x * (color_25).xyz)) * ((dot(normalWorld_14, y_27) + 0.5)))) / tmpvar_28))));
tmpvar_2 = tmpvar_16;
half3 viewDir_29;
viewDir_29 = worldViewDir_8;
fixed4 c_30;
fixed4 c_31;
float nh_32;
fixed diff_33;
half tmpvar_34;
tmpvar_34 = max(0.0, dot(tmpvar_7, tmpvar_3));
diff_33 = tmpvar_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_7, normalize(((tmpvar_3 + viewDir_29)))));
nh_32 = tmpvar_35;
half y_36;
y_36 = ((_Shininess * 128.0));
float tmpvar_37;
tmpvar_37 = ((pow(nh_32, y_36) * (tmpvar_11).w));
(c_31).xyz = ((((((tmpvar_6 * tmpvar_16)) * diff_33)) + ((((tmpvar_16 * (_SpecColor).xyz)) * tmpvar_37))));
(c_31).w = (((tmpvar_11).w * (_Color).w));
(c_30).w = (c_31).w;
(c_30).xyz = (((c_31).xyz + ((tmpvar_6 * tmpvar_17))));
float tmpvar_38;
tmpvar_38 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_30).xyz, ((float3)(tmpvar_38)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD4 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_5;
tmpvar_5[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_5[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_5[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_5, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = tmpvar_1;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_3).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half realtimeShadowAttenuation_12;
float4 v_13;
(v_13).x = (transpose(unity_MatrixV)[0]).z;
(v_13).y = (transpose(unity_MatrixV)[1]).z;
(v_13).z = (transpose(unity_MatrixV)[2]).z;
(v_13).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_14;
tmpvar_14 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_15;
float tmpvar_16;
tmpvar_16 = clamp(((((lerp(dot(tmpvar_10, (v_13).xyz), sqrt(dot(tmpvar_14, tmpvar_14)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_15 = tmpvar_16;
float4 tmpvar_17;
(tmpvar_17).w = 1.0;
(tmpvar_17).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_18;
float4 shadowCoord_19;
shadowCoord_19 = (mul(tmpvar_17, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_20;
half tmpvar_21;
tmpvar_21 = (_LightShadowData).x;
lightShadowDataX_20 = tmpvar_21;
float tmpvar_22;
tmpvar_22 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_19).xy)).x > (shadowCoord_19).z)))), lightShadowDataX_20);
tmpvar_18 = tmpvar_22;
realtimeShadowAttenuation_12 = tmpvar_18;
half tmpvar_23;
tmpvar_23 = clamp(((realtimeShadowAttenuation_12 + tmpvar_15)), 0.0, 1.0);
atten_5 = tmpvar_23;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 tmpvar_24;
tmpvar_24 = ((tmpvar_2 * tmpvar_1));
tmpvar_2 = tmpvar_24;
half3 viewDir_25;
viewDir_25 = tmpvar_9;
fixed4 c_26;
float nh_27;
fixed diff_28;
half tmpvar_29;
tmpvar_29 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_28 = tmpvar_29;
half tmpvar_30;
tmpvar_30 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_25)))));
nh_27 = tmpvar_30;
half y_31;
y_31 = ((_Shininess * 128.0));
float tmpvar_32;
tmpvar_32 = ((pow(nh_27, y_31) * (tmpvar_11).w));
(c_26).xyz = (((((((tmpvar_11).xyz * (_Color).xyz)) * ((tmpvar_24 * diff_28)))) + ((((tmpvar_24 * (_SpecColor).xyz)) * tmpvar_32))));
(c_26).w = (((tmpvar_11).w * (_Color).w));
float tmpvar_33;
tmpvar_33 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_26).xyz, ((float3)(tmpvar_33)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float4 xlv_TEXCOORD6 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_5;
tmpvar_5[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_5[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_5[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_6;
tmpvar_6 = normalize((mul(tmpvar_5, v._glesNormal)));
half3 normal_7;
normal_7 = tmpvar_6;
half3 x1_8;
half4 tmpvar_9;
tmpvar_9 = (((normal_7).xyzz * (normal_7).yzzx));
(x1_8).x = dot(unity_SHBr, tmpvar_9);
(x1_8).y = dot(unity_SHBg, tmpvar_9);
(x1_8).z = dot(unity_SHBb, tmpvar_9);
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_6;
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = ((x1_8 + (((unity_SHC).xyz * (((((normal_7).x * (normal_7).x)) - (((normal_7).y * (normal_7).y))))))));
o.xlv_TEXCOORD4 = tmpvar_1;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_3).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
float4 v_14;
(v_14).x = (transpose(unity_MatrixV)[0]).z;
(v_14).y = (transpose(unity_MatrixV)[1]).z;
(v_14).z = (transpose(unity_MatrixV)[2]).z;
(v_14).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_15;
tmpvar_15 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_16;
float tmpvar_17;
tmpvar_17 = clamp(((((lerp(dot(tmpvar_10, (v_14).xyz), sqrt(dot(tmpvar_15, tmpvar_15)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_16 = tmpvar_17;
float4 tmpvar_18;
(tmpvar_18).w = 1.0;
(tmpvar_18).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_19;
float4 shadowCoord_20;
shadowCoord_20 = (mul(tmpvar_18, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_21;
half tmpvar_22;
tmpvar_22 = (_LightShadowData).x;
lightShadowDataX_21 = tmpvar_22;
float tmpvar_23;
tmpvar_23 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_20).xy)).x > (shadowCoord_20).z)))), lightShadowDataX_21);
tmpvar_19 = tmpvar_23;
realtimeShadowAttenuation_13 = tmpvar_19;
half tmpvar_24;
tmpvar_24 = clamp(((realtimeShadowAttenuation_13 + tmpvar_16)), 0.0, 1.0);
atten_5 = tmpvar_24;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_25;
normalWorld_25 = tmpvar_6;
half3 tmpvar_26;
tmpvar_26 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_27;
(tmpvar_27).w = 1.0;
(tmpvar_27).xyz = normalWorld_25;
half3 x_28;
(x_28).x = dot(unity_SHAr, tmpvar_27);
(x_28).y = dot(unity_SHAg, tmpvar_27);
(x_28).z = dot(unity_SHAb, tmpvar_27);
half3 tmpvar_29;
tmpvar_29 = max(((((1.055 * pow(max(float3(0.0, 0.0, 0.0), ((i.xlv_TEXCOORD3 + x_28))), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
tmpvar_2 = tmpvar_26;
half3 viewDir_30;
viewDir_30 = tmpvar_9;
fixed4 c_31;
fixed4 c_32;
float nh_33;
fixed diff_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_34 = tmpvar_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_30)))));
nh_33 = tmpvar_36;
half y_37;
y_37 = ((_Shininess * 128.0));
float tmpvar_38;
tmpvar_38 = ((pow(nh_33, y_37) * (tmpvar_12).w));
(c_32).xyz = ((((((tmpvar_11 * tmpvar_26)) * diff_34)) + ((((tmpvar_26 * (_SpecColor).xyz)) * tmpvar_38))));
(c_32).w = (((tmpvar_12).w * (_Color).w));
(c_31).w = (c_32).w;
(c_31).xyz = (((c_32).xyz + ((tmpvar_11 * tmpvar_29))));
float tmpvar_39;
tmpvar_39 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_31).xyz, ((float3)(tmpvar_39)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD4 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_5;
tmpvar_5[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_5[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_5[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_2).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_5, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = tmpvar_1;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_3).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
float4 v_14;
(v_14).x = (transpose(unity_MatrixV)[0]).z;
(v_14).y = (transpose(unity_MatrixV)[1]).z;
(v_14).z = (transpose(unity_MatrixV)[2]).z;
(v_14).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_15;
tmpvar_15 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_16;
float tmpvar_17;
tmpvar_17 = clamp(((((lerp(dot(tmpvar_10, (v_14).xyz), sqrt(dot(tmpvar_15, tmpvar_15)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_16 = tmpvar_17;
float4 tmpvar_18;
(tmpvar_18).w = 1.0;
(tmpvar_18).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_19;
float4 shadowCoord_20;
shadowCoord_20 = (mul(tmpvar_18, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_21;
half tmpvar_22;
tmpvar_22 = (_LightShadowData).x;
lightShadowDataX_21 = tmpvar_22;
float tmpvar_23;
tmpvar_23 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_20).xy)).x > (shadowCoord_20).z)))), lightShadowDataX_21);
tmpvar_19 = tmpvar_23;
realtimeShadowAttenuation_13 = tmpvar_19;
half tmpvar_24;
tmpvar_24 = clamp(((realtimeShadowAttenuation_13 + tmpvar_16)), 0.0, 1.0);
atten_5 = tmpvar_24;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 tmpvar_25;
half3 tmpvar_26;
tmpvar_25 = ((tmpvar_2 * tmpvar_1));
fixed4 tmpvar_27;
tmpvar_27 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
tmpvar_26 = pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_27).w)) * (tmpvar_27).xyz)), (unity_DynamicLightmap_HDR).yyy);
tmpvar_2 = tmpvar_25;
half3 viewDir_28;
viewDir_28 = tmpvar_9;
fixed4 c_29;
fixed4 c_30;
float nh_31;
fixed diff_32;
half tmpvar_33;
tmpvar_33 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_32 = tmpvar_33;
half tmpvar_34;
tmpvar_34 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_28)))));
nh_31 = tmpvar_34;
half y_35;
y_35 = ((_Shininess * 128.0));
float tmpvar_36;
tmpvar_36 = ((pow(nh_31, y_35) * (tmpvar_12).w));
(c_30).xyz = ((((((tmpvar_11 * tmpvar_25)) * diff_32)) + ((((tmpvar_25 * (_SpecColor).xyz)) * tmpvar_36))));
(c_30).w = (((tmpvar_12).w * (_Color).w));
(c_29).w = (c_30).w;
(c_29).xyz = (((c_30).xyz + ((tmpvar_11 * tmpvar_26))));
float tmpvar_37;
tmpvar_37 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_29).xyz, ((float3)(tmpvar_37)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float4 xlv_TEXCOORD6 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_5;
tmpvar_5 = normalize((mul(tmpvar_4, v._glesNormal)));
half3 normal_6;
normal_6 = tmpvar_5;
half3 x1_7;
half4 tmpvar_8;
tmpvar_8 = (((normal_6).xyzz * (normal_6).yzzx));
(x1_7).x = dot(unity_SHBr, tmpvar_8);
(x1_7).y = dot(unity_SHBg, tmpvar_8);
(x1_7).z = dot(unity_SHBb, tmpvar_8);
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_5;
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = ((x1_7 + (((unity_SHC).xyz * (((((normal_6).x * (normal_6).x)) - (((normal_6).y * (normal_6).y))))))));
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = lerp(realtimeShadowAttenuation_13, shadowMaskAttenuation_14, tmpvar_17);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_27;
normalWorld_27 = tmpvar_6;
half3 tmpvar_28;
tmpvar_28 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_29;
(tmpvar_29).w = 1.0;
(tmpvar_29).xyz = normalWorld_27;
half3 x_30;
(x_30).x = dot(unity_SHAr, tmpvar_29);
(x_30).y = dot(unity_SHAg, tmpvar_29);
(x_30).z = dot(unity_SHAb, tmpvar_29);
half3 tmpvar_31;
tmpvar_31 = max(((((1.055 * pow(max(float3(0.0, 0.0, 0.0), ((i.xlv_TEXCOORD3 + x_30))), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
tmpvar_2 = tmpvar_28;
half3 viewDir_32;
viewDir_32 = tmpvar_9;
fixed4 c_33;
fixed4 c_34;
float nh_35;
fixed diff_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_36 = tmpvar_37;
half tmpvar_38;
tmpvar_38 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_32)))));
nh_35 = tmpvar_38;
half y_39;
y_39 = ((_Shininess * 128.0));
float tmpvar_40;
tmpvar_40 = ((pow(nh_35, y_39) * (tmpvar_12).w));
(c_34).xyz = ((((((tmpvar_11 * tmpvar_28)) * diff_36)) + ((((tmpvar_28 * (_SpecColor).xyz)) * tmpvar_40))));
(c_34).w = (((tmpvar_12).w * (_Color).w));
(c_33).w = (c_34).w;
(c_33).xyz = (((c_34).xyz + ((tmpvar_11 * tmpvar_31))));
float tmpvar_41;
tmpvar_41 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_33).xyz, ((float3)(tmpvar_41)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float2 xlv_TEXCOORD4 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = lerp(realtimeShadowAttenuation_13, shadowMaskAttenuation_14, tmpvar_17);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 tmpvar_27;
half3 tmpvar_28;
tmpvar_27 = ((tmpvar_2 * tmpvar_1));
fixed4 tmpvar_29;
tmpvar_29 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
tmpvar_28 = pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_29).w)) * (tmpvar_29).xyz)), (unity_DynamicLightmap_HDR).yyy);
tmpvar_2 = tmpvar_27;
half3 viewDir_30;
viewDir_30 = tmpvar_9;
fixed4 c_31;
fixed4 c_32;
float nh_33;
fixed diff_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_34 = tmpvar_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_30)))));
nh_33 = tmpvar_36;
half y_37;
y_37 = ((_Shininess * 128.0));
float tmpvar_38;
tmpvar_38 = ((pow(nh_33, y_37) * (tmpvar_12).w));
(c_32).xyz = ((((((tmpvar_11 * tmpvar_27)) * diff_34)) + ((((tmpvar_27 * (_SpecColor).xyz)) * tmpvar_38))));
(c_32).w = (((tmpvar_12).w * (_Color).w));
(c_31).w = (c_32).w;
(c_31).xyz = (((c_32).xyz + ((tmpvar_11 * tmpvar_28))));
float tmpvar_39;
tmpvar_39 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_31).xyz, ((float3)(tmpvar_39)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float4 xlv_TEXCOORD6 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_5;
tmpvar_5 = normalize((mul(tmpvar_4, v._glesNormal)));
half3 normal_6;
normal_6 = tmpvar_5;
half3 x1_7;
half4 tmpvar_8;
tmpvar_8 = (((normal_6).xyzz * (normal_6).yzzx));
(x1_7).x = dot(unity_SHBr, tmpvar_8);
(x1_7).y = dot(unity_SHBg, tmpvar_8);
(x1_7).z = dot(unity_SHBb, tmpvar_8);
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_5;
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = ((x1_7 + (((unity_SHC).xyz * (((((normal_6).x * (normal_6).x)) - (((normal_6).y * (normal_6).y))))))));
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = min(clamp(((realtimeShadowAttenuation_13 + tmpvar_17)), 0.0, 1.0), shadowMaskAttenuation_14);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_27;
normalWorld_27 = tmpvar_6;
half3 tmpvar_28;
tmpvar_28 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_29;
(tmpvar_29).w = 1.0;
(tmpvar_29).xyz = normalWorld_27;
half3 x_30;
(x_30).x = dot(unity_SHAr, tmpvar_29);
(x_30).y = dot(unity_SHAg, tmpvar_29);
(x_30).z = dot(unity_SHAb, tmpvar_29);
half3 tmpvar_31;
tmpvar_31 = max(((((1.055 * pow(max(float3(0.0, 0.0, 0.0), ((i.xlv_TEXCOORD3 + x_30))), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
tmpvar_2 = tmpvar_28;
half3 viewDir_32;
viewDir_32 = tmpvar_9;
fixed4 c_33;
fixed4 c_34;
float nh_35;
fixed diff_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_36 = tmpvar_37;
half tmpvar_38;
tmpvar_38 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_32)))));
nh_35 = tmpvar_38;
half y_39;
y_39 = ((_Shininess * 128.0));
float tmpvar_40;
tmpvar_40 = ((pow(nh_35, y_39) * (tmpvar_12).w));
(c_34).xyz = ((((((tmpvar_11 * tmpvar_28)) * diff_36)) + ((((tmpvar_28 * (_SpecColor).xyz)) * tmpvar_40))));
(c_34).w = (((tmpvar_12).w * (_Color).w));
(c_33).w = (c_34).w;
(c_33).xyz = (((c_34).xyz + ((tmpvar_11 * tmpvar_31))));
float tmpvar_41;
tmpvar_41 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_33).xyz, ((float3)(tmpvar_41)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float2 xlv_TEXCOORD4 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = min(clamp(((realtimeShadowAttenuation_13 + tmpvar_17)), 0.0, 1.0), shadowMaskAttenuation_14);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 tmpvar_27;
half3 tmpvar_28;
tmpvar_27 = ((tmpvar_2 * tmpvar_1));
fixed4 tmpvar_29;
tmpvar_29 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
tmpvar_28 = pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_29).w)) * (tmpvar_29).xyz)), (unity_DynamicLightmap_HDR).yyy);
tmpvar_2 = tmpvar_27;
half3 viewDir_30;
viewDir_30 = tmpvar_9;
fixed4 c_31;
fixed4 c_32;
float nh_33;
fixed diff_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_34 = tmpvar_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_30)))));
nh_33 = tmpvar_36;
half y_37;
y_37 = ((_Shininess * 128.0));
float tmpvar_38;
tmpvar_38 = ((pow(nh_33, y_37) * (tmpvar_12).w));
(c_32).xyz = ((((((tmpvar_11 * tmpvar_27)) * diff_34)) + ((((tmpvar_27 * (_SpecColor).xyz)) * tmpvar_38))));
(c_32).w = (((tmpvar_12).w * (_Color).w));
(c_31).w = (c_32).w;
(c_31).xyz = (((c_32).xyz + ((tmpvar_11 * tmpvar_28))));
float tmpvar_39;
tmpvar_39 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_31).xyz, ((float3)(tmpvar_39)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
float4 tmpvar_5;
tmpvar_5 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_5).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_5, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 tmpvar_16;
half3 tmpvar_17;
float4 v_18;
(v_18).x = (transpose(unity_MatrixV)[0]).z;
(v_18).y = (transpose(unity_MatrixV)[1]).z;
(v_18).z = (transpose(unity_MatrixV)[2]).z;
(v_18).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_19;
tmpvar_19 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_20;
float tmpvar_21;
tmpvar_21 = clamp(((((lerp(dot(tmpvar_9, (v_18).xyz), sqrt(dot(tmpvar_19, tmpvar_19)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_20 = tmpvar_21;
tmpvar_16 = ((tmpvar_2 * clamp(((tmpvar_1 + tmpvar_20)), 0.0, 1.0)));
half4 tmpvar_22;
tmpvar_22 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_23;
color_23 = tmpvar_22;
tmpvar_17 = (((unity_Lightmap_HDR).x * (color_23).xyz));
tmpvar_2 = tmpvar_16;
half3 viewDir_24;
viewDir_24 = tmpvar_8;
fixed4 c_25;
fixed4 c_26;
float nh_27;
fixed diff_28;
half tmpvar_29;
tmpvar_29 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_28 = tmpvar_29;
half tmpvar_30;
tmpvar_30 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_24)))));
nh_27 = tmpvar_30;
half y_31;
y_31 = ((_Shininess * 128.0));
float tmpvar_32;
tmpvar_32 = ((pow(nh_27, y_31) * (tmpvar_11).w));
(c_26).xyz = ((((((tmpvar_10 * tmpvar_16)) * diff_28)) + ((((tmpvar_16 * (_SpecColor).xyz)) * tmpvar_32))));
(c_26).w = (((tmpvar_11).w * (_Color).w));
(c_25).w = (c_26).w;
(c_25).xyz = (((c_26).xyz + ((tmpvar_10 * tmpvar_17))));
float tmpvar_33;
tmpvar_33 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_25).xyz, ((float3)(tmpvar_33)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
float4 tmpvar_5;
tmpvar_5 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_5).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_5, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 tmpvar_16;
half3 tmpvar_17;
float4 v_18;
(v_18).x = (transpose(unity_MatrixV)[0]).z;
(v_18).y = (transpose(unity_MatrixV)[1]).z;
(v_18).z = (transpose(unity_MatrixV)[2]).z;
(v_18).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_19;
tmpvar_19 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_20;
float tmpvar_21;
tmpvar_21 = clamp(((((lerp(dot(tmpvar_9, (v_18).xyz), sqrt(dot(tmpvar_19, tmpvar_19)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_20 = tmpvar_21;
tmpvar_16 = ((tmpvar_2 * clamp(((tmpvar_1 + tmpvar_20)), 0.0, 1.0)));
half4 tmpvar_22;
tmpvar_22 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_23;
color_23 = tmpvar_22;
tmpvar_17 = (((unity_Lightmap_HDR).x * (color_23).xyz));
fixed4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_17 = ((tmpvar_17 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_24).w)) * (tmpvar_24).xyz)), (unity_DynamicLightmap_HDR).yyy)));
tmpvar_2 = tmpvar_16;
half3 viewDir_25;
viewDir_25 = tmpvar_8;
fixed4 c_26;
fixed4 c_27;
float nh_28;
fixed diff_29;
half tmpvar_30;
tmpvar_30 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_29 = tmpvar_30;
half tmpvar_31;
tmpvar_31 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_25)))));
nh_28 = tmpvar_31;
half y_32;
y_32 = ((_Shininess * 128.0));
float tmpvar_33;
tmpvar_33 = ((pow(nh_28, y_32) * (tmpvar_11).w));
(c_27).xyz = ((((((tmpvar_10 * tmpvar_16)) * diff_29)) + ((((tmpvar_16 * (_SpecColor).xyz)) * tmpvar_33))));
(c_27).w = (((tmpvar_11).w * (_Color).w));
(c_26).w = (c_27).w;
(c_26).xyz = (((c_27).xyz + ((tmpvar_10 * tmpvar_17))));
float tmpvar_34;
tmpvar_34 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_26).xyz, ((float3)(tmpvar_34)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
float4 tmpvar_5;
tmpvar_5 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_5).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_5, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
half3 tmpvar_17;
half3 tmpvar_18;
float4 v_19;
(v_19).x = (transpose(unity_MatrixV)[0]).z;
(v_19).y = (transpose(unity_MatrixV)[1]).z;
(v_19).z = (transpose(unity_MatrixV)[2]).z;
(v_19).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_20;
tmpvar_20 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_21;
float tmpvar_22;
tmpvar_22 = clamp(((((lerp(dot(tmpvar_9, (v_19).xyz), sqrt(dot(tmpvar_20, tmpvar_20)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_21 = tmpvar_22;
tmpvar_17 = ((tmpvar_2 * clamp(((tmpvar_1 + tmpvar_21)), 0.0, 1.0)));
half3 ambient_23;
half4 tmpvar_24;
(tmpvar_24).w = 1.0;
(tmpvar_24).xyz = normalWorld_16;
half3 x_25;
(x_25).x = dot(unity_SHAr, tmpvar_24);
(x_25).y = dot(unity_SHAg, tmpvar_24);
(x_25).z = dot(unity_SHAb, tmpvar_24);
half3 x1_26;
half4 tmpvar_27;
tmpvar_27 = (((normalWorld_16).xyzz * (normalWorld_16).yzzx));
(x1_26).x = dot(unity_SHBr, tmpvar_27);
(x1_26).y = dot(unity_SHBg, tmpvar_27);
(x1_26).z = dot(unity_SHBb, tmpvar_27);
ambient_23 = max(float3(0.0, 0.0, 0.0), ((x_25 + ((x1_26 + (((unity_SHC).xyz * (((((normalWorld_16).x * (normalWorld_16).x)) - (((normalWorld_16).y * (normalWorld_16).y)))))))))));
half3 tmpvar_28;
tmpvar_28 = max(((((1.055 * pow(max(ambient_23, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_23 = tmpvar_28;
half4 tmpvar_29;
tmpvar_29 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_30;
color_30 = tmpvar_29;
tmpvar_18 = ((tmpvar_28 + (((unity_Lightmap_HDR).x * (color_30).xyz))));
tmpvar_2 = tmpvar_17;
half3 viewDir_31;
viewDir_31 = tmpvar_8;
fixed4 c_32;
fixed4 c_33;
float nh_34;
fixed diff_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_35 = tmpvar_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_31)))));
nh_34 = tmpvar_37;
half y_38;
y_38 = ((_Shininess * 128.0));
float tmpvar_39;
tmpvar_39 = ((pow(nh_34, y_38) * (tmpvar_11).w));
(c_33).xyz = ((((((tmpvar_10 * tmpvar_17)) * diff_35)) + ((((tmpvar_17 * (_SpecColor).xyz)) * tmpvar_39))));
(c_33).w = (((tmpvar_11).w * (_Color).w));
(c_32).w = (c_33).w;
(c_32).xyz = (((c_33).xyz + ((tmpvar_10 * tmpvar_18))));
float tmpvar_40;
tmpvar_40 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_32).xyz, ((float3)(tmpvar_40)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
float4 tmpvar_5;
tmpvar_5 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_5).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_5, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
fixed4 c_2;
fixed3 tmpvar_3;
tmpvar_3 = i.xlv_TEXCOORD1;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed tmpvar_5;
float lightShadowDataX_6;
half tmpvar_7;
tmpvar_7 = (_LightShadowData).x;
lightShadowDataX_6 = tmpvar_7;
float tmpvar_8;
tmpvar_8 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_6);
tmpvar_5 = tmpvar_8;
tmpvar_1 = tmpvar_5;
half3 normalWorld_9;
normalWorld_9 = tmpvar_3;
half3 tmpvar_10;
float4 v_11;
(v_11).x = (transpose(unity_MatrixV)[0]).z;
(v_11).y = (transpose(unity_MatrixV)[1]).z;
(v_11).z = (transpose(unity_MatrixV)[2]).z;
(v_11).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_12;
tmpvar_12 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_13;
float tmpvar_14;
tmpvar_14 = clamp(((((lerp(dot(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)), (v_11).xyz), sqrt(dot(tmpvar_12, tmpvar_12)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_13 = tmpvar_14;
half4 tmpvar_15;
tmpvar_15 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_16;
color_16 = tmpvar_15;
tmpvar_10 = (((unity_Lightmap_HDR).x * (color_16).xyz));
half3 shadowColor_17;
fixed3 tmpvar_18;
tmpvar_18 = (unity_ShadowColor).xyz;
shadowColor_17 = tmpvar_18;
tmpvar_10 = min(tmpvar_10, lerp(max(((tmpvar_10 - ((((max(0.0, dot(normalWorld_9, (_WorldSpaceLightPos0).xyz)) * ((1.0 - clamp(((tmpvar_1 + tmpvar_13)), 0.0, 1.0))))) * (_LightColor0).xyz)))), shadowColor_17), tmpvar_10, (_LightShadowData).xxx));
fixed4 c_19;
fixed4 c_20;
(c_20).xyz = float3(0.0, 0.0, 0.0);
(c_20).w = (((tmpvar_4).w * (_Color).w));
(c_19).w = (c_20).w;
(c_19).xyz = (((((tmpvar_4).xyz * (_Color).xyz)) * tmpvar_10));
float tmpvar_21;
tmpvar_21 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_19).xyz, ((float3)(tmpvar_21)));
(c_2).w = 1.0;
o.target0 = c_2;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
float4 tmpvar_5;
tmpvar_5 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_5).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_5, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
fixed4 c_2;
fixed3 tmpvar_3;
tmpvar_3 = i.xlv_TEXCOORD1;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed tmpvar_5;
float lightShadowDataX_6;
half tmpvar_7;
tmpvar_7 = (_LightShadowData).x;
lightShadowDataX_6 = tmpvar_7;
float tmpvar_8;
tmpvar_8 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_6);
tmpvar_5 = tmpvar_8;
tmpvar_1 = tmpvar_5;
half3 normalWorld_9;
normalWorld_9 = tmpvar_3;
half3 tmpvar_10;
float4 v_11;
(v_11).x = (transpose(unity_MatrixV)[0]).z;
(v_11).y = (transpose(unity_MatrixV)[1]).z;
(v_11).z = (transpose(unity_MatrixV)[2]).z;
(v_11).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_12;
tmpvar_12 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_13;
float tmpvar_14;
tmpvar_14 = clamp(((((lerp(dot(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)), (v_11).xyz), sqrt(dot(tmpvar_12, tmpvar_12)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_13 = tmpvar_14;
half4 tmpvar_15;
tmpvar_15 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_16;
color_16 = tmpvar_15;
tmpvar_10 = (((unity_Lightmap_HDR).x * (color_16).xyz));
half3 shadowColor_17;
fixed3 tmpvar_18;
tmpvar_18 = (unity_ShadowColor).xyz;
shadowColor_17 = tmpvar_18;
tmpvar_10 = min(tmpvar_10, lerp(max(((tmpvar_10 - ((((max(0.0, dot(normalWorld_9, (_WorldSpaceLightPos0).xyz)) * ((1.0 - clamp(((tmpvar_1 + tmpvar_13)), 0.0, 1.0))))) * (_LightColor0).xyz)))), shadowColor_17), tmpvar_10, (_LightShadowData).xxx));
fixed4 tmpvar_19;
tmpvar_19 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_10 = ((tmpvar_10 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_19).w)) * (tmpvar_19).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_20;
fixed4 c_21;
(c_21).xyz = float3(0.0, 0.0, 0.0);
(c_21).w = (((tmpvar_4).w * (_Color).w));
(c_20).w = (c_21).w;
(c_20).xyz = (((((tmpvar_4).xyz * (_Color).xyz)) * tmpvar_10));
float tmpvar_22;
tmpvar_22 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_20).xyz, ((float3)(tmpvar_22)));
(c_2).w = 1.0;
o.target0 = c_2;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
float4 tmpvar_5;
tmpvar_5 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_5).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_5, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
fixed4 c_2;
fixed3 tmpvar_3;
tmpvar_3 = i.xlv_TEXCOORD1;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed tmpvar_5;
float lightShadowDataX_6;
half tmpvar_7;
tmpvar_7 = (_LightShadowData).x;
lightShadowDataX_6 = tmpvar_7;
float tmpvar_8;
tmpvar_8 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_6);
tmpvar_5 = tmpvar_8;
tmpvar_1 = tmpvar_5;
half3 normalWorld_9;
normalWorld_9 = tmpvar_3;
half3 tmpvar_10;
float4 v_11;
(v_11).x = (transpose(unity_MatrixV)[0]).z;
(v_11).y = (transpose(unity_MatrixV)[1]).z;
(v_11).z = (transpose(unity_MatrixV)[2]).z;
(v_11).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_12;
tmpvar_12 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_13;
float tmpvar_14;
tmpvar_14 = clamp(((((lerp(dot(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)), (v_11).xyz), sqrt(dot(tmpvar_12, tmpvar_12)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_13 = tmpvar_14;
half3 ambient_15;
half4 tmpvar_16;
(tmpvar_16).w = 1.0;
(tmpvar_16).xyz = normalWorld_9;
half3 x_17;
(x_17).x = dot(unity_SHAr, tmpvar_16);
(x_17).y = dot(unity_SHAg, tmpvar_16);
(x_17).z = dot(unity_SHAb, tmpvar_16);
half3 x1_18;
half4 tmpvar_19;
tmpvar_19 = (((normalWorld_9).xyzz * (normalWorld_9).yzzx));
(x1_18).x = dot(unity_SHBr, tmpvar_19);
(x1_18).y = dot(unity_SHBg, tmpvar_19);
(x1_18).z = dot(unity_SHBb, tmpvar_19);
ambient_15 = max(float3(0.0, 0.0, 0.0), ((x_17 + ((x1_18 + (((unity_SHC).xyz * (((((normalWorld_9).x * (normalWorld_9).x)) - (((normalWorld_9).y * (normalWorld_9).y)))))))))));
half3 tmpvar_20;
tmpvar_20 = max(((((1.055 * pow(max(ambient_15, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_15 = tmpvar_20;
half4 tmpvar_21;
tmpvar_21 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_22;
color_22 = tmpvar_21;
tmpvar_10 = ((tmpvar_20 + (((unity_Lightmap_HDR).x * (color_22).xyz))));
half3 shadowColor_23;
fixed3 tmpvar_24;
tmpvar_24 = (unity_ShadowColor).xyz;
shadowColor_23 = tmpvar_24;
tmpvar_10 = min(tmpvar_10, lerp(max(((tmpvar_10 - ((((max(0.0, dot(normalWorld_9, (_WorldSpaceLightPos0).xyz)) * ((1.0 - clamp(((tmpvar_1 + tmpvar_13)), 0.0, 1.0))))) * (_LightColor0).xyz)))), shadowColor_23), tmpvar_10, (_LightShadowData).xxx));
fixed4 c_25;
fixed4 c_26;
(c_26).xyz = float3(0.0, 0.0, 0.0);
(c_26).w = (((tmpvar_4).w * (_Color).w));
(c_25).w = (c_26).w;
(c_25).xyz = (((((tmpvar_4).xyz * (_Color).xyz)) * tmpvar_10));
float tmpvar_27;
tmpvar_27 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_25).xyz, ((float3)(tmpvar_27)));
(c_2).w = 1.0;
o.target0 = c_2;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
float4 tmpvar_5;
tmpvar_5 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_5).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_5, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half bakedAtten_16;
half3 tmpvar_17;
half3 tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_16 = tmpvar_19;
float4 v_20;
(v_20).x = (transpose(unity_MatrixV)[0]).z;
(v_20).y = (transpose(unity_MatrixV)[1]).z;
(v_20).z = (transpose(unity_MatrixV)[2]).z;
(v_20).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_21;
tmpvar_21 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_22;
float tmpvar_23;
tmpvar_23 = clamp(((((lerp(dot(tmpvar_9, (v_20).xyz), sqrt(dot(tmpvar_21, tmpvar_21)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_22 = tmpvar_23;
tmpvar_17 = ((tmpvar_2 * lerp(tmpvar_1, bakedAtten_16, tmpvar_22)));
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_25;
color_25 = tmpvar_24;
tmpvar_18 = (((unity_Lightmap_HDR).x * (color_25).xyz));
tmpvar_2 = tmpvar_17;
half3 viewDir_26;
viewDir_26 = tmpvar_8;
fixed4 c_27;
fixed4 c_28;
float nh_29;
fixed diff_30;
half tmpvar_31;
tmpvar_31 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_30 = tmpvar_31;
half tmpvar_32;
tmpvar_32 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_26)))));
nh_29 = tmpvar_32;
half y_33;
y_33 = ((_Shininess * 128.0));
float tmpvar_34;
tmpvar_34 = ((pow(nh_29, y_33) * (tmpvar_11).w));
(c_28).xyz = ((((((tmpvar_10 * tmpvar_17)) * diff_30)) + ((((tmpvar_17 * (_SpecColor).xyz)) * tmpvar_34))));
(c_28).w = (((tmpvar_11).w * (_Color).w));
(c_27).w = (c_28).w;
(c_27).xyz = (((c_28).xyz + ((tmpvar_10 * tmpvar_18))));
float tmpvar_35;
tmpvar_35 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_27).xyz, ((float3)(tmpvar_35)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
float4 tmpvar_5;
tmpvar_5 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_5).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_5, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half bakedAtten_16;
half3 tmpvar_17;
half3 tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_16 = tmpvar_19;
float4 v_20;
(v_20).x = (transpose(unity_MatrixV)[0]).z;
(v_20).y = (transpose(unity_MatrixV)[1]).z;
(v_20).z = (transpose(unity_MatrixV)[2]).z;
(v_20).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_21;
tmpvar_21 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_22;
float tmpvar_23;
tmpvar_23 = clamp(((((lerp(dot(tmpvar_9, (v_20).xyz), sqrt(dot(tmpvar_21, tmpvar_21)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_22 = tmpvar_23;
tmpvar_17 = ((tmpvar_2 * lerp(tmpvar_1, bakedAtten_16, tmpvar_22)));
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_25;
color_25 = tmpvar_24;
tmpvar_18 = (((unity_Lightmap_HDR).x * (color_25).xyz));
fixed4 tmpvar_26;
tmpvar_26 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_18 = ((tmpvar_18 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_26).w)) * (tmpvar_26).xyz)), (unity_DynamicLightmap_HDR).yyy)));
tmpvar_2 = tmpvar_17;
half3 viewDir_27;
viewDir_27 = tmpvar_8;
fixed4 c_28;
fixed4 c_29;
float nh_30;
fixed diff_31;
half tmpvar_32;
tmpvar_32 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_31 = tmpvar_32;
half tmpvar_33;
tmpvar_33 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_27)))));
nh_30 = tmpvar_33;
half y_34;
y_34 = ((_Shininess * 128.0));
float tmpvar_35;
tmpvar_35 = ((pow(nh_30, y_34) * (tmpvar_11).w));
(c_29).xyz = ((((((tmpvar_10 * tmpvar_17)) * diff_31)) + ((((tmpvar_17 * (_SpecColor).xyz)) * tmpvar_35))));
(c_29).w = (((tmpvar_11).w * (_Color).w));
(c_28).w = (c_29).w;
(c_28).xyz = (((c_29).xyz + ((tmpvar_10 * tmpvar_18))));
float tmpvar_36;
tmpvar_36 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_28).xyz, ((float3)(tmpvar_36)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
float4 tmpvar_5;
tmpvar_5 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_5).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_5, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
half bakedAtten_17;
half3 tmpvar_18;
half3 tmpvar_19;
fixed tmpvar_20;
tmpvar_20 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_17 = tmpvar_20;
float4 v_21;
(v_21).x = (transpose(unity_MatrixV)[0]).z;
(v_21).y = (transpose(unity_MatrixV)[1]).z;
(v_21).z = (transpose(unity_MatrixV)[2]).z;
(v_21).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_22;
tmpvar_22 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_23;
float tmpvar_24;
tmpvar_24 = clamp(((((lerp(dot(tmpvar_9, (v_21).xyz), sqrt(dot(tmpvar_22, tmpvar_22)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_23 = tmpvar_24;
tmpvar_18 = ((tmpvar_2 * lerp(tmpvar_1, bakedAtten_17, tmpvar_23)));
half3 ambient_25;
half4 tmpvar_26;
(tmpvar_26).w = 1.0;
(tmpvar_26).xyz = normalWorld_16;
half3 x_27;
(x_27).x = dot(unity_SHAr, tmpvar_26);
(x_27).y = dot(unity_SHAg, tmpvar_26);
(x_27).z = dot(unity_SHAb, tmpvar_26);
half3 x1_28;
half4 tmpvar_29;
tmpvar_29 = (((normalWorld_16).xyzz * (normalWorld_16).yzzx));
(x1_28).x = dot(unity_SHBr, tmpvar_29);
(x1_28).y = dot(unity_SHBg, tmpvar_29);
(x1_28).z = dot(unity_SHBb, tmpvar_29);
ambient_25 = max(float3(0.0, 0.0, 0.0), ((x_27 + ((x1_28 + (((unity_SHC).xyz * (((((normalWorld_16).x * (normalWorld_16).x)) - (((normalWorld_16).y * (normalWorld_16).y)))))))))));
half3 tmpvar_30;
tmpvar_30 = max(((((1.055 * pow(max(ambient_25, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_25 = tmpvar_30;
half4 tmpvar_31;
tmpvar_31 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_32;
color_32 = tmpvar_31;
tmpvar_19 = ((tmpvar_30 + (((unity_Lightmap_HDR).x * (color_32).xyz))));
tmpvar_2 = tmpvar_18;
half3 viewDir_33;
viewDir_33 = tmpvar_8;
fixed4 c_34;
fixed4 c_35;
float nh_36;
fixed diff_37;
half tmpvar_38;
tmpvar_38 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_37 = tmpvar_38;
half tmpvar_39;
tmpvar_39 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_33)))));
nh_36 = tmpvar_39;
half y_40;
y_40 = ((_Shininess * 128.0));
float tmpvar_41;
tmpvar_41 = ((pow(nh_36, y_40) * (tmpvar_11).w));
(c_35).xyz = ((((((tmpvar_10 * tmpvar_18)) * diff_37)) + ((((tmpvar_18 * (_SpecColor).xyz)) * tmpvar_41))));
(c_35).w = (((tmpvar_11).w * (_Color).w));
(c_34).w = (c_35).w;
(c_34).xyz = (((c_35).xyz + ((tmpvar_10 * tmpvar_19))));
float tmpvar_42;
tmpvar_42 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_34).xyz, ((float3)(tmpvar_42)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
float4 tmpvar_5;
tmpvar_5 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_5).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_5, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half bakedAtten_16;
half3 tmpvar_17;
half3 tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_16 = tmpvar_19;
float4 v_20;
(v_20).x = (transpose(unity_MatrixV)[0]).z;
(v_20).y = (transpose(unity_MatrixV)[1]).z;
(v_20).z = (transpose(unity_MatrixV)[2]).z;
(v_20).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_21;
tmpvar_21 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_22;
float tmpvar_23;
tmpvar_23 = clamp(((((lerp(dot(tmpvar_9, (v_20).xyz), sqrt(dot(tmpvar_21, tmpvar_21)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_22 = tmpvar_23;
tmpvar_17 = ((tmpvar_2 * min(clamp(((tmpvar_1 + tmpvar_22)), 0.0, 1.0), bakedAtten_16)));
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_25;
color_25 = tmpvar_24;
tmpvar_18 = (((unity_Lightmap_HDR).x * (color_25).xyz));
tmpvar_2 = tmpvar_17;
half3 viewDir_26;
viewDir_26 = tmpvar_8;
fixed4 c_27;
fixed4 c_28;
float nh_29;
fixed diff_30;
half tmpvar_31;
tmpvar_31 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_30 = tmpvar_31;
half tmpvar_32;
tmpvar_32 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_26)))));
nh_29 = tmpvar_32;
half y_33;
y_33 = ((_Shininess * 128.0));
float tmpvar_34;
tmpvar_34 = ((pow(nh_29, y_33) * (tmpvar_11).w));
(c_28).xyz = ((((((tmpvar_10 * tmpvar_17)) * diff_30)) + ((((tmpvar_17 * (_SpecColor).xyz)) * tmpvar_34))));
(c_28).w = (((tmpvar_11).w * (_Color).w));
(c_27).w = (c_28).w;
(c_27).xyz = (((c_28).xyz + ((tmpvar_10 * tmpvar_18))));
float tmpvar_35;
tmpvar_35 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_27).xyz, ((float3)(tmpvar_35)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
float4 tmpvar_5;
tmpvar_5 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_5).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_5, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half bakedAtten_16;
half3 tmpvar_17;
half3 tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_16 = tmpvar_19;
float4 v_20;
(v_20).x = (transpose(unity_MatrixV)[0]).z;
(v_20).y = (transpose(unity_MatrixV)[1]).z;
(v_20).z = (transpose(unity_MatrixV)[2]).z;
(v_20).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_21;
tmpvar_21 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_22;
float tmpvar_23;
tmpvar_23 = clamp(((((lerp(dot(tmpvar_9, (v_20).xyz), sqrt(dot(tmpvar_21, tmpvar_21)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_22 = tmpvar_23;
tmpvar_17 = ((tmpvar_2 * min(clamp(((tmpvar_1 + tmpvar_22)), 0.0, 1.0), bakedAtten_16)));
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_25;
color_25 = tmpvar_24;
tmpvar_18 = (((unity_Lightmap_HDR).x * (color_25).xyz));
fixed4 tmpvar_26;
tmpvar_26 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_18 = ((tmpvar_18 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_26).w)) * (tmpvar_26).xyz)), (unity_DynamicLightmap_HDR).yyy)));
tmpvar_2 = tmpvar_17;
half3 viewDir_27;
viewDir_27 = tmpvar_8;
fixed4 c_28;
fixed4 c_29;
float nh_30;
fixed diff_31;
half tmpvar_32;
tmpvar_32 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_31 = tmpvar_32;
half tmpvar_33;
tmpvar_33 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_27)))));
nh_30 = tmpvar_33;
half y_34;
y_34 = ((_Shininess * 128.0));
float tmpvar_35;
tmpvar_35 = ((pow(nh_30, y_34) * (tmpvar_11).w));
(c_29).xyz = ((((((tmpvar_10 * tmpvar_17)) * diff_31)) + ((((tmpvar_17 * (_SpecColor).xyz)) * tmpvar_35))));
(c_29).w = (((tmpvar_11).w * (_Color).w));
(c_28).w = (c_29).w;
(c_28).xyz = (((c_29).xyz + ((tmpvar_10 * tmpvar_18))));
float tmpvar_36;
tmpvar_36 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_28).xyz, ((float3)(tmpvar_36)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
float4 tmpvar_5;
tmpvar_5 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = (tmpvar_5).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = (mul(tmpvar_5, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
half bakedAtten_17;
half3 tmpvar_18;
half3 tmpvar_19;
fixed tmpvar_20;
tmpvar_20 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_17 = tmpvar_20;
float4 v_21;
(v_21).x = (transpose(unity_MatrixV)[0]).z;
(v_21).y = (transpose(unity_MatrixV)[1]).z;
(v_21).z = (transpose(unity_MatrixV)[2]).z;
(v_21).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_22;
tmpvar_22 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_23;
float tmpvar_24;
tmpvar_24 = clamp(((((lerp(dot(tmpvar_9, (v_21).xyz), sqrt(dot(tmpvar_22, tmpvar_22)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_23 = tmpvar_24;
tmpvar_18 = ((tmpvar_2 * min(clamp(((tmpvar_1 + tmpvar_23)), 0.0, 1.0), bakedAtten_17)));
half3 ambient_25;
half4 tmpvar_26;
(tmpvar_26).w = 1.0;
(tmpvar_26).xyz = normalWorld_16;
half3 x_27;
(x_27).x = dot(unity_SHAr, tmpvar_26);
(x_27).y = dot(unity_SHAg, tmpvar_26);
(x_27).z = dot(unity_SHAb, tmpvar_26);
half3 x1_28;
half4 tmpvar_29;
tmpvar_29 = (((normalWorld_16).xyzz * (normalWorld_16).yzzx));
(x1_28).x = dot(unity_SHBr, tmpvar_29);
(x1_28).y = dot(unity_SHBg, tmpvar_29);
(x1_28).z = dot(unity_SHBb, tmpvar_29);
ambient_25 = max(float3(0.0, 0.0, 0.0), ((x_27 + ((x1_28 + (((unity_SHC).xyz * (((((normalWorld_16).x * (normalWorld_16).x)) - (((normalWorld_16).y * (normalWorld_16).y)))))))))));
half3 tmpvar_30;
tmpvar_30 = max(((((1.055 * pow(max(ambient_25, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_25 = tmpvar_30;
half4 tmpvar_31;
tmpvar_31 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_32;
color_32 = tmpvar_31;
tmpvar_19 = ((tmpvar_30 + (((unity_Lightmap_HDR).x * (color_32).xyz))));
tmpvar_2 = tmpvar_18;
half3 viewDir_33;
viewDir_33 = tmpvar_8;
fixed4 c_34;
fixed4 c_35;
float nh_36;
fixed diff_37;
half tmpvar_38;
tmpvar_38 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_37 = tmpvar_38;
half tmpvar_39;
tmpvar_39 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_33)))));
nh_36 = tmpvar_39;
half y_40;
y_40 = ((_Shininess * 128.0));
float tmpvar_41;
tmpvar_41 = ((pow(nh_36, y_40) * (tmpvar_11).w));
(c_35).xyz = ((((((tmpvar_10 * tmpvar_18)) * diff_37)) + ((((tmpvar_18 * (_SpecColor).xyz)) * tmpvar_41))));
(c_35).w = (((tmpvar_11).w * (_Color).w));
(c_34).w = (c_35).w;
(c_34).xyz = (((c_35).xyz + ((tmpvar_10 * tmpvar_19))));
float tmpvar_42;
tmpvar_42 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_34).xyz, ((float3)(tmpvar_42)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD4 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_5;
tmpvar_5[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_5[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_5[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_2).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_5, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = tmpvar_1;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_3).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
float4 v_14;
(v_14).x = (transpose(unity_MatrixV)[0]).z;
(v_14).y = (transpose(unity_MatrixV)[1]).z;
(v_14).z = (transpose(unity_MatrixV)[2]).z;
(v_14).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_15;
tmpvar_15 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_16;
float tmpvar_17;
tmpvar_17 = clamp(((((lerp(dot(tmpvar_10, (v_14).xyz), sqrt(dot(tmpvar_15, tmpvar_15)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_16 = tmpvar_17;
float4 tmpvar_18;
(tmpvar_18).w = 1.0;
(tmpvar_18).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_19;
float4 shadowCoord_20;
shadowCoord_20 = (mul(tmpvar_18, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_21;
half tmpvar_22;
tmpvar_22 = (_LightShadowData).x;
lightShadowDataX_21 = tmpvar_22;
float tmpvar_23;
tmpvar_23 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_20).xy)).x > (shadowCoord_20).z)))), lightShadowDataX_21);
tmpvar_19 = tmpvar_23;
realtimeShadowAttenuation_13 = tmpvar_19;
half tmpvar_24;
tmpvar_24 = clamp(((realtimeShadowAttenuation_13 + tmpvar_16)), 0.0, 1.0);
atten_5 = tmpvar_24;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_25;
normalWorld_25 = tmpvar_6;
half4 realtimeDirTex_26;
half3 tmpvar_27;
half3 tmpvar_28;
tmpvar_27 = ((tmpvar_2 * tmpvar_1));
fixed4 tmpvar_29;
tmpvar_29 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
fixed4 tmpvar_30;
tmpvar_30 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
realtimeDirTex_26 = tmpvar_30;
fixed4 dirTex_31;
dirTex_31 = realtimeDirTex_26;
fixed3 y_32;
y_32 = (((dirTex_31).xyz - 0.5));
fixed tmpvar_33;
tmpvar_33 = max(0.0001, (dirTex_31).w);
tmpvar_28 = ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_29).w)) * (tmpvar_29).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_25, y_32) + 0.5)))) / tmpvar_33));
tmpvar_2 = tmpvar_27;
half3 viewDir_34;
viewDir_34 = tmpvar_9;
fixed4 c_35;
fixed4 c_36;
float nh_37;
fixed diff_38;
half tmpvar_39;
tmpvar_39 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_38 = tmpvar_39;
half tmpvar_40;
tmpvar_40 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_34)))));
nh_37 = tmpvar_40;
half y_41;
y_41 = ((_Shininess * 128.0));
float tmpvar_42;
tmpvar_42 = ((pow(nh_37, y_41) * (tmpvar_12).w));
(c_36).xyz = ((((((tmpvar_11 * tmpvar_27)) * diff_38)) + ((((tmpvar_27 * (_SpecColor).xyz)) * tmpvar_42))));
(c_36).w = (((tmpvar_12).w * (_Color).w));
(c_35).w = (c_36).w;
(c_35).xyz = (((c_36).xyz + ((tmpvar_11 * tmpvar_28))));
float tmpvar_43;
tmpvar_43 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_35).xyz, ((float3)(tmpvar_43)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
float4 tmpvar_8;
tmpvar_8 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_7 = (tmpvar_8).xyz;
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_9[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_9[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul(tmpvar_9, v._glesNormal)));
float3x3 tmpvar_11;
tmpvar_11[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_11[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_11[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_12;
tmpvar_12 = normalize((mul((v._glesTANGENT).xyz, tmpvar_11)));
worldTangent_3 = tmpvar_12;
float tmpvar_13;
tmpvar_13 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_13;
float3 tmpvar_14;
tmpvar_14 = (((((((tmpvar_10).yzx * (worldTangent_3).zxy)) - (((tmpvar_10).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_14;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).x;
(tmpvar_15).y = (worldBinormal_1).x;
(tmpvar_15).z = (tmpvar_10).x;
(tmpvar_15).w = (tmpvar_7).x;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).y;
(tmpvar_16).y = (worldBinormal_1).y;
(tmpvar_16).z = (tmpvar_10).y;
(tmpvar_16).w = (tmpvar_7).y;
float4 tmpvar_17;
(tmpvar_17).x = (worldTangent_3).z;
(tmpvar_17).y = (worldBinormal_1).z;
(tmpvar_17).z = (tmpvar_10).z;
(tmpvar_17).w = (tmpvar_7).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_10;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_8, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_15).xyz;
o.xlv_TEXCOORD7 = (tmpvar_16).xyz;
o.xlv_TEXCOORD8 = (tmpvar_17).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
fixed4 bakedDirTex_17;
half3 tmpvar_18;
half3 tmpvar_19;
float4 v_20;
(v_20).x = (transpose(unity_MatrixV)[0]).z;
(v_20).y = (transpose(unity_MatrixV)[1]).z;
(v_20).z = (transpose(unity_MatrixV)[2]).z;
(v_20).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_21;
tmpvar_21 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_22;
float tmpvar_23;
tmpvar_23 = clamp(((((lerp(dot(tmpvar_9, (v_20).xyz), sqrt(dot(tmpvar_21, tmpvar_21)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_22 = tmpvar_23;
tmpvar_18 = ((tmpvar_2 * clamp(((tmpvar_1 + tmpvar_22)), 0.0, 1.0)));
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_25;
color_25 = tmpvar_24;
half4 tmpvar_26;
tmpvar_26 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_17 = tmpvar_26;
fixed3 y_27;
y_27 = (((bakedDirTex_17).xyz - 0.5));
fixed tmpvar_28;
tmpvar_28 = max(0.0001, (bakedDirTex_17).w);
tmpvar_19 = (((((((unity_Lightmap_HDR).x * (color_25).xyz)) * ((dot(normalWorld_16, y_27) + 0.5)))) / tmpvar_28));
tmpvar_2 = tmpvar_18;
half3 viewDir_29;
viewDir_29 = tmpvar_8;
fixed4 c_30;
fixed4 c_31;
float nh_32;
fixed diff_33;
half tmpvar_34;
tmpvar_34 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_33 = tmpvar_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_29)))));
nh_32 = tmpvar_35;
half y_36;
y_36 = ((_Shininess * 128.0));
float tmpvar_37;
tmpvar_37 = ((pow(nh_32, y_36) * (tmpvar_11).w));
(c_31).xyz = ((((((tmpvar_10 * tmpvar_18)) * diff_33)) + ((((tmpvar_18 * (_SpecColor).xyz)) * tmpvar_37))));
(c_31).w = (((tmpvar_11).w * (_Color).w));
(c_30).w = (c_31).w;
(c_30).xyz = (((c_31).xyz + ((tmpvar_10 * tmpvar_19))));
float tmpvar_38;
tmpvar_38 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_30).xyz, ((float3)(tmpvar_38)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
float4 tmpvar_8;
tmpvar_8 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_7 = (tmpvar_8).xyz;
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_9[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_9[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul(tmpvar_9, v._glesNormal)));
float3x3 tmpvar_11;
tmpvar_11[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_11[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_11[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_12;
tmpvar_12 = normalize((mul((v._glesTANGENT).xyz, tmpvar_11)));
worldTangent_3 = tmpvar_12;
float tmpvar_13;
tmpvar_13 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_13;
float3 tmpvar_14;
tmpvar_14 = (((((((tmpvar_10).yzx * (worldTangent_3).zxy)) - (((tmpvar_10).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_14;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).x;
(tmpvar_15).y = (worldBinormal_1).x;
(tmpvar_15).z = (tmpvar_10).x;
(tmpvar_15).w = (tmpvar_7).x;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).y;
(tmpvar_16).y = (worldBinormal_1).y;
(tmpvar_16).z = (tmpvar_10).y;
(tmpvar_16).w = (tmpvar_7).y;
float4 tmpvar_17;
(tmpvar_17).x = (worldTangent_3).z;
(tmpvar_17).y = (worldBinormal_1).z;
(tmpvar_17).z = (tmpvar_10).z;
(tmpvar_17).w = (tmpvar_7).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_10;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_8, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_15).xyz;
o.xlv_TEXCOORD7 = (tmpvar_16).xyz;
o.xlv_TEXCOORD8 = (tmpvar_17).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
fixed4 bakedDirTex_17;
half3 tmpvar_18;
half3 tmpvar_19;
float4 v_20;
(v_20).x = (transpose(unity_MatrixV)[0]).z;
(v_20).y = (transpose(unity_MatrixV)[1]).z;
(v_20).z = (transpose(unity_MatrixV)[2]).z;
(v_20).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_21;
tmpvar_21 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_22;
float tmpvar_23;
tmpvar_23 = clamp(((((lerp(dot(tmpvar_9, (v_20).xyz), sqrt(dot(tmpvar_21, tmpvar_21)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_22 = tmpvar_23;
tmpvar_18 = ((tmpvar_2 * clamp(((tmpvar_1 + tmpvar_22)), 0.0, 1.0)));
half3 ambient_24;
half4 tmpvar_25;
(tmpvar_25).w = 1.0;
(tmpvar_25).xyz = normalWorld_16;
half3 x_26;
(x_26).x = dot(unity_SHAr, tmpvar_25);
(x_26).y = dot(unity_SHAg, tmpvar_25);
(x_26).z = dot(unity_SHAb, tmpvar_25);
half3 x1_27;
half4 tmpvar_28;
tmpvar_28 = (((normalWorld_16).xyzz * (normalWorld_16).yzzx));
(x1_27).x = dot(unity_SHBr, tmpvar_28);
(x1_27).y = dot(unity_SHBg, tmpvar_28);
(x1_27).z = dot(unity_SHBb, tmpvar_28);
ambient_24 = max(float3(0.0, 0.0, 0.0), ((x_26 + ((x1_27 + (((unity_SHC).xyz * (((((normalWorld_16).x * (normalWorld_16).x)) - (((normalWorld_16).y * (normalWorld_16).y)))))))))));
half3 tmpvar_29;
tmpvar_29 = max(((((1.055 * pow(max(ambient_24, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_24 = tmpvar_29;
half4 tmpvar_30;
tmpvar_30 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_31;
color_31 = tmpvar_30;
half4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_17 = tmpvar_32;
fixed3 y_33;
y_33 = (((bakedDirTex_17).xyz - 0.5));
fixed tmpvar_34;
tmpvar_34 = max(0.0001, (bakedDirTex_17).w);
tmpvar_19 = ((tmpvar_29 + (((((((unity_Lightmap_HDR).x * (color_31).xyz)) * ((dot(normalWorld_16, y_33) + 0.5)))) / tmpvar_34))));
tmpvar_2 = tmpvar_18;
half3 viewDir_35;
viewDir_35 = tmpvar_8;
fixed4 c_36;
fixed4 c_37;
float nh_38;
fixed diff_39;
half tmpvar_40;
tmpvar_40 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_39 = tmpvar_40;
half tmpvar_41;
tmpvar_41 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_35)))));
nh_38 = tmpvar_41;
half y_42;
y_42 = ((_Shininess * 128.0));
float tmpvar_43;
tmpvar_43 = ((pow(nh_38, y_42) * (tmpvar_11).w));
(c_37).xyz = ((((((tmpvar_10 * tmpvar_18)) * diff_39)) + ((((tmpvar_18 * (_SpecColor).xyz)) * tmpvar_43))));
(c_37).w = (((tmpvar_11).w * (_Color).w));
(c_36).w = (c_37).w;
(c_36).xyz = (((c_37).xyz + ((tmpvar_10 * tmpvar_19))));
float tmpvar_44;
tmpvar_44 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_36).xyz, ((float3)(tmpvar_44)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
float4 tmpvar_8;
tmpvar_8 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_7 = (tmpvar_8).xyz;
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_9[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_9[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul(tmpvar_9, v._glesNormal)));
float3x3 tmpvar_11;
tmpvar_11[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_11[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_11[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_12;
tmpvar_12 = normalize((mul((v._glesTANGENT).xyz, tmpvar_11)));
worldTangent_3 = tmpvar_12;
float tmpvar_13;
tmpvar_13 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_13;
float3 tmpvar_14;
tmpvar_14 = (((((((tmpvar_10).yzx * (worldTangent_3).zxy)) - (((tmpvar_10).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_14;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).x;
(tmpvar_15).y = (worldBinormal_1).x;
(tmpvar_15).z = (tmpvar_10).x;
(tmpvar_15).w = (tmpvar_7).x;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).y;
(tmpvar_16).y = (worldBinormal_1).y;
(tmpvar_16).z = (tmpvar_10).y;
(tmpvar_16).w = (tmpvar_7).y;
float4 tmpvar_17;
(tmpvar_17).x = (worldTangent_3).z;
(tmpvar_17).y = (worldBinormal_1).z;
(tmpvar_17).z = (tmpvar_10).z;
(tmpvar_17).w = (tmpvar_7).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_10;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_8, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_15).xyz;
o.xlv_TEXCOORD7 = (tmpvar_16).xyz;
o.xlv_TEXCOORD8 = (tmpvar_17).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
half4 realtimeDirTex_17;
fixed4 bakedDirTex_18;
half3 tmpvar_19;
half3 tmpvar_20;
float4 v_21;
(v_21).x = (transpose(unity_MatrixV)[0]).z;
(v_21).y = (transpose(unity_MatrixV)[1]).z;
(v_21).z = (transpose(unity_MatrixV)[2]).z;
(v_21).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_22;
tmpvar_22 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_23;
float tmpvar_24;
tmpvar_24 = clamp(((((lerp(dot(tmpvar_9, (v_21).xyz), sqrt(dot(tmpvar_22, tmpvar_22)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_23 = tmpvar_24;
tmpvar_19 = ((tmpvar_2 * clamp(((tmpvar_1 + tmpvar_23)), 0.0, 1.0)));
half4 tmpvar_25;
tmpvar_25 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_26;
color_26 = tmpvar_25;
half4 tmpvar_27;
tmpvar_27 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_18 = tmpvar_27;
fixed3 y_28;
y_28 = (((bakedDirTex_18).xyz - 0.5));
fixed tmpvar_29;
tmpvar_29 = max(0.0001, (bakedDirTex_18).w);
tmpvar_20 = (((((((unity_Lightmap_HDR).x * (color_26).xyz)) * ((dot(normalWorld_16, y_28) + 0.5)))) / tmpvar_29));
fixed4 tmpvar_30;
tmpvar_30 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_31;
tmpvar_31 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_17 = tmpvar_31;
fixed4 dirTex_32;
dirTex_32 = realtimeDirTex_17;
fixed3 y_33;
y_33 = (((dirTex_32).xyz - 0.5));
fixed tmpvar_34;
tmpvar_34 = max(0.0001, (dirTex_32).w);
tmpvar_20 = ((tmpvar_20 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_30).w)) * (tmpvar_30).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_16, y_33) + 0.5)))) / tmpvar_34))));
tmpvar_2 = tmpvar_19;
half3 viewDir_35;
viewDir_35 = tmpvar_8;
fixed4 c_36;
fixed4 c_37;
float nh_38;
fixed diff_39;
half tmpvar_40;
tmpvar_40 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_39 = tmpvar_40;
half tmpvar_41;
tmpvar_41 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_35)))));
nh_38 = tmpvar_41;
half y_42;
y_42 = ((_Shininess * 128.0));
float tmpvar_43;
tmpvar_43 = ((pow(nh_38, y_42) * (tmpvar_11).w));
(c_37).xyz = ((((((tmpvar_10 * tmpvar_19)) * diff_39)) + ((((tmpvar_19 * (_SpecColor).xyz)) * tmpvar_43))));
(c_37).w = (((tmpvar_11).w * (_Color).w));
(c_36).w = (c_37).w;
(c_36).xyz = (((c_37).xyz + ((tmpvar_10 * tmpvar_20))));
float tmpvar_44;
tmpvar_44 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_36).xyz, ((float3)(tmpvar_44)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
float4 tmpvar_8;
tmpvar_8 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_7 = (tmpvar_8).xyz;
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_9[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_9[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul(tmpvar_9, v._glesNormal)));
float3x3 tmpvar_11;
tmpvar_11[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_11[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_11[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_12;
tmpvar_12 = normalize((mul((v._glesTANGENT).xyz, tmpvar_11)));
worldTangent_3 = tmpvar_12;
float tmpvar_13;
tmpvar_13 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_13;
float3 tmpvar_14;
tmpvar_14 = (((((((tmpvar_10).yzx * (worldTangent_3).zxy)) - (((tmpvar_10).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_14;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).x;
(tmpvar_15).y = (worldBinormal_1).x;
(tmpvar_15).z = (tmpvar_10).x;
(tmpvar_15).w = (tmpvar_7).x;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).y;
(tmpvar_16).y = (worldBinormal_1).y;
(tmpvar_16).z = (tmpvar_10).y;
(tmpvar_16).w = (tmpvar_7).y;
float4 tmpvar_17;
(tmpvar_17).x = (worldTangent_3).z;
(tmpvar_17).y = (worldBinormal_1).z;
(tmpvar_17).z = (tmpvar_10).z;
(tmpvar_17).w = (tmpvar_7).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_10;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_8, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_15).xyz;
o.xlv_TEXCOORD7 = (tmpvar_16).xyz;
o.xlv_TEXCOORD8 = (tmpvar_17).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
fixed4 c_2;
fixed3 tmpvar_3;
tmpvar_3 = i.xlv_TEXCOORD1;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed tmpvar_5;
float lightShadowDataX_6;
half tmpvar_7;
tmpvar_7 = (_LightShadowData).x;
lightShadowDataX_6 = tmpvar_7;
float tmpvar_8;
tmpvar_8 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_6);
tmpvar_5 = tmpvar_8;
tmpvar_1 = tmpvar_5;
half3 normalWorld_9;
normalWorld_9 = tmpvar_3;
fixed4 bakedDirTex_10;
half3 tmpvar_11;
float4 v_12;
(v_12).x = (transpose(unity_MatrixV)[0]).z;
(v_12).y = (transpose(unity_MatrixV)[1]).z;
(v_12).z = (transpose(unity_MatrixV)[2]).z;
(v_12).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_13;
tmpvar_13 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_14;
float tmpvar_15;
tmpvar_15 = clamp(((((lerp(dot(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)), (v_12).xyz), sqrt(dot(tmpvar_13, tmpvar_13)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_14 = tmpvar_15;
half4 tmpvar_16;
tmpvar_16 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_17;
color_17 = tmpvar_16;
half4 tmpvar_18;
tmpvar_18 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_10 = tmpvar_18;
fixed3 y_19;
y_19 = (((bakedDirTex_10).xyz - 0.5));
fixed tmpvar_20;
tmpvar_20 = max(0.0001, (bakedDirTex_10).w);
tmpvar_11 = (((((((unity_Lightmap_HDR).x * (color_17).xyz)) * ((dot(normalWorld_9, y_19) + 0.5)))) / tmpvar_20));
half3 shadowColor_21;
fixed3 tmpvar_22;
tmpvar_22 = (unity_ShadowColor).xyz;
shadowColor_21 = tmpvar_22;
tmpvar_11 = min(tmpvar_11, lerp(max(((tmpvar_11 - ((((max(0.0, dot(normalWorld_9, (_WorldSpaceLightPos0).xyz)) * ((1.0 - clamp(((tmpvar_1 + tmpvar_14)), 0.0, 1.0))))) * (_LightColor0).xyz)))), shadowColor_21), tmpvar_11, (_LightShadowData).xxx));
fixed4 c_23;
fixed4 c_24;
(c_24).xyz = float3(0.0, 0.0, 0.0);
(c_24).w = (((tmpvar_4).w * (_Color).w));
(c_23).w = (c_24).w;
(c_23).xyz = (((((tmpvar_4).xyz * (_Color).xyz)) * tmpvar_11));
float tmpvar_25;
tmpvar_25 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_23).xyz, ((float3)(tmpvar_25)));
(c_2).w = 1.0;
o.target0 = c_2;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
float4 tmpvar_8;
tmpvar_8 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_7 = (tmpvar_8).xyz;
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_9[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_9[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul(tmpvar_9, v._glesNormal)));
float3x3 tmpvar_11;
tmpvar_11[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_11[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_11[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_12;
tmpvar_12 = normalize((mul((v._glesTANGENT).xyz, tmpvar_11)));
worldTangent_3 = tmpvar_12;
float tmpvar_13;
tmpvar_13 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_13;
float3 tmpvar_14;
tmpvar_14 = (((((((tmpvar_10).yzx * (worldTangent_3).zxy)) - (((tmpvar_10).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_14;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).x;
(tmpvar_15).y = (worldBinormal_1).x;
(tmpvar_15).z = (tmpvar_10).x;
(tmpvar_15).w = (tmpvar_7).x;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).y;
(tmpvar_16).y = (worldBinormal_1).y;
(tmpvar_16).z = (tmpvar_10).y;
(tmpvar_16).w = (tmpvar_7).y;
float4 tmpvar_17;
(tmpvar_17).x = (worldTangent_3).z;
(tmpvar_17).y = (worldBinormal_1).z;
(tmpvar_17).z = (tmpvar_10).z;
(tmpvar_17).w = (tmpvar_7).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_10;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_8, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_15).xyz;
o.xlv_TEXCOORD7 = (tmpvar_16).xyz;
o.xlv_TEXCOORD8 = (tmpvar_17).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
fixed4 c_2;
fixed3 tmpvar_3;
tmpvar_3 = i.xlv_TEXCOORD1;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed tmpvar_5;
float lightShadowDataX_6;
half tmpvar_7;
tmpvar_7 = (_LightShadowData).x;
lightShadowDataX_6 = tmpvar_7;
float tmpvar_8;
tmpvar_8 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_6);
tmpvar_5 = tmpvar_8;
tmpvar_1 = tmpvar_5;
half3 normalWorld_9;
normalWorld_9 = tmpvar_3;
fixed4 bakedDirTex_10;
half3 tmpvar_11;
float4 v_12;
(v_12).x = (transpose(unity_MatrixV)[0]).z;
(v_12).y = (transpose(unity_MatrixV)[1]).z;
(v_12).z = (transpose(unity_MatrixV)[2]).z;
(v_12).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_13;
tmpvar_13 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_14;
float tmpvar_15;
tmpvar_15 = clamp(((((lerp(dot(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)), (v_12).xyz), sqrt(dot(tmpvar_13, tmpvar_13)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_14 = tmpvar_15;
half3 ambient_16;
half4 tmpvar_17;
(tmpvar_17).w = 1.0;
(tmpvar_17).xyz = normalWorld_9;
half3 x_18;
(x_18).x = dot(unity_SHAr, tmpvar_17);
(x_18).y = dot(unity_SHAg, tmpvar_17);
(x_18).z = dot(unity_SHAb, tmpvar_17);
half3 x1_19;
half4 tmpvar_20;
tmpvar_20 = (((normalWorld_9).xyzz * (normalWorld_9).yzzx));
(x1_19).x = dot(unity_SHBr, tmpvar_20);
(x1_19).y = dot(unity_SHBg, tmpvar_20);
(x1_19).z = dot(unity_SHBb, tmpvar_20);
ambient_16 = max(float3(0.0, 0.0, 0.0), ((x_18 + ((x1_19 + (((unity_SHC).xyz * (((((normalWorld_9).x * (normalWorld_9).x)) - (((normalWorld_9).y * (normalWorld_9).y)))))))))));
half3 tmpvar_21;
tmpvar_21 = max(((((1.055 * pow(max(ambient_16, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_16 = tmpvar_21;
half4 tmpvar_22;
tmpvar_22 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_23;
color_23 = tmpvar_22;
half4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_10 = tmpvar_24;
fixed3 y_25;
y_25 = (((bakedDirTex_10).xyz - 0.5));
fixed tmpvar_26;
tmpvar_26 = max(0.0001, (bakedDirTex_10).w);
tmpvar_11 = ((tmpvar_21 + (((((((unity_Lightmap_HDR).x * (color_23).xyz)) * ((dot(normalWorld_9, y_25) + 0.5)))) / tmpvar_26))));
half3 shadowColor_27;
fixed3 tmpvar_28;
tmpvar_28 = (unity_ShadowColor).xyz;
shadowColor_27 = tmpvar_28;
tmpvar_11 = min(tmpvar_11, lerp(max(((tmpvar_11 - ((((max(0.0, dot(normalWorld_9, (_WorldSpaceLightPos0).xyz)) * ((1.0 - clamp(((tmpvar_1 + tmpvar_14)), 0.0, 1.0))))) * (_LightColor0).xyz)))), shadowColor_27), tmpvar_11, (_LightShadowData).xxx));
fixed4 c_29;
fixed4 c_30;
(c_30).xyz = float3(0.0, 0.0, 0.0);
(c_30).w = (((tmpvar_4).w * (_Color).w));
(c_29).w = (c_30).w;
(c_29).xyz = (((((tmpvar_4).xyz * (_Color).xyz)) * tmpvar_11));
float tmpvar_31;
tmpvar_31 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_29).xyz, ((float3)(tmpvar_31)));
(c_2).w = 1.0;
o.target0 = c_2;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
float4 tmpvar_8;
tmpvar_8 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_7 = (tmpvar_8).xyz;
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_9[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_9[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul(tmpvar_9, v._glesNormal)));
float3x3 tmpvar_11;
tmpvar_11[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_11[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_11[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_12;
tmpvar_12 = normalize((mul((v._glesTANGENT).xyz, tmpvar_11)));
worldTangent_3 = tmpvar_12;
float tmpvar_13;
tmpvar_13 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_13;
float3 tmpvar_14;
tmpvar_14 = (((((((tmpvar_10).yzx * (worldTangent_3).zxy)) - (((tmpvar_10).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_14;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).x;
(tmpvar_15).y = (worldBinormal_1).x;
(tmpvar_15).z = (tmpvar_10).x;
(tmpvar_15).w = (tmpvar_7).x;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).y;
(tmpvar_16).y = (worldBinormal_1).y;
(tmpvar_16).z = (tmpvar_10).y;
(tmpvar_16).w = (tmpvar_7).y;
float4 tmpvar_17;
(tmpvar_17).x = (worldTangent_3).z;
(tmpvar_17).y = (worldBinormal_1).z;
(tmpvar_17).z = (tmpvar_10).z;
(tmpvar_17).w = (tmpvar_7).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_10;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_8, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_15).xyz;
o.xlv_TEXCOORD7 = (tmpvar_16).xyz;
o.xlv_TEXCOORD8 = (tmpvar_17).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
fixed4 c_2;
fixed3 tmpvar_3;
tmpvar_3 = i.xlv_TEXCOORD1;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed tmpvar_5;
float lightShadowDataX_6;
half tmpvar_7;
tmpvar_7 = (_LightShadowData).x;
lightShadowDataX_6 = tmpvar_7;
float tmpvar_8;
tmpvar_8 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_6);
tmpvar_5 = tmpvar_8;
tmpvar_1 = tmpvar_5;
half3 normalWorld_9;
normalWorld_9 = tmpvar_3;
half4 realtimeDirTex_10;
fixed4 bakedDirTex_11;
half3 tmpvar_12;
float4 v_13;
(v_13).x = (transpose(unity_MatrixV)[0]).z;
(v_13).y = (transpose(unity_MatrixV)[1]).z;
(v_13).z = (transpose(unity_MatrixV)[2]).z;
(v_13).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_14;
tmpvar_14 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_15;
float tmpvar_16;
tmpvar_16 = clamp(((((lerp(dot(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)), (v_13).xyz), sqrt(dot(tmpvar_14, tmpvar_14)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_15 = tmpvar_16;
half4 tmpvar_17;
tmpvar_17 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_18;
color_18 = tmpvar_17;
half4 tmpvar_19;
tmpvar_19 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_11 = tmpvar_19;
fixed3 y_20;
y_20 = (((bakedDirTex_11).xyz - 0.5));
fixed tmpvar_21;
tmpvar_21 = max(0.0001, (bakedDirTex_11).w);
tmpvar_12 = (((((((unity_Lightmap_HDR).x * (color_18).xyz)) * ((dot(normalWorld_9, y_20) + 0.5)))) / tmpvar_21));
half3 shadowColor_22;
fixed3 tmpvar_23;
tmpvar_23 = (unity_ShadowColor).xyz;
shadowColor_22 = tmpvar_23;
tmpvar_12 = min(tmpvar_12, lerp(max(((tmpvar_12 - ((((max(0.0, dot(normalWorld_9, (_WorldSpaceLightPos0).xyz)) * ((1.0 - clamp(((tmpvar_1 + tmpvar_15)), 0.0, 1.0))))) * (_LightColor0).xyz)))), shadowColor_22), tmpvar_12, (_LightShadowData).xxx));
fixed4 tmpvar_24;
tmpvar_24 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_25;
tmpvar_25 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_10 = tmpvar_25;
fixed4 dirTex_26;
dirTex_26 = realtimeDirTex_10;
fixed3 y_27;
y_27 = (((dirTex_26).xyz - 0.5));
fixed tmpvar_28;
tmpvar_28 = max(0.0001, (dirTex_26).w);
tmpvar_12 = ((tmpvar_12 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_24).w)) * (tmpvar_24).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_9, y_27) + 0.5)))) / tmpvar_28))));
fixed4 c_29;
fixed4 c_30;
(c_30).xyz = float3(0.0, 0.0, 0.0);
(c_30).w = (((tmpvar_4).w * (_Color).w));
(c_29).w = (c_30).w;
(c_29).xyz = (((((tmpvar_4).xyz * (_Color).xyz)) * tmpvar_12));
float tmpvar_31;
tmpvar_31 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_29).xyz, ((float3)(tmpvar_31)));
(c_2).w = 1.0;
o.target0 = c_2;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
float4 tmpvar_8;
tmpvar_8 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_7 = (tmpvar_8).xyz;
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_9[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_9[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul(tmpvar_9, v._glesNormal)));
float3x3 tmpvar_11;
tmpvar_11[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_11[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_11[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_12;
tmpvar_12 = normalize((mul((v._glesTANGENT).xyz, tmpvar_11)));
worldTangent_3 = tmpvar_12;
float tmpvar_13;
tmpvar_13 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_13;
float3 tmpvar_14;
tmpvar_14 = (((((((tmpvar_10).yzx * (worldTangent_3).zxy)) - (((tmpvar_10).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_14;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).x;
(tmpvar_15).y = (worldBinormal_1).x;
(tmpvar_15).z = (tmpvar_10).x;
(tmpvar_15).w = (tmpvar_7).x;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).y;
(tmpvar_16).y = (worldBinormal_1).y;
(tmpvar_16).z = (tmpvar_10).y;
(tmpvar_16).w = (tmpvar_7).y;
float4 tmpvar_17;
(tmpvar_17).x = (worldTangent_3).z;
(tmpvar_17).y = (worldBinormal_1).z;
(tmpvar_17).z = (tmpvar_10).z;
(tmpvar_17).w = (tmpvar_7).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_10;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_8, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_15).xyz;
o.xlv_TEXCOORD7 = (tmpvar_16).xyz;
o.xlv_TEXCOORD8 = (tmpvar_17).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
fixed4 bakedDirTex_17;
half bakedAtten_18;
half3 tmpvar_19;
half3 tmpvar_20;
fixed tmpvar_21;
tmpvar_21 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_18 = tmpvar_21;
float4 v_22;
(v_22).x = (transpose(unity_MatrixV)[0]).z;
(v_22).y = (transpose(unity_MatrixV)[1]).z;
(v_22).z = (transpose(unity_MatrixV)[2]).z;
(v_22).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_23;
tmpvar_23 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_24;
float tmpvar_25;
tmpvar_25 = clamp(((((lerp(dot(tmpvar_9, (v_22).xyz), sqrt(dot(tmpvar_23, tmpvar_23)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_24 = tmpvar_25;
tmpvar_19 = ((tmpvar_2 * lerp(tmpvar_1, bakedAtten_18, tmpvar_24)));
half4 tmpvar_26;
tmpvar_26 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_27;
color_27 = tmpvar_26;
half4 tmpvar_28;
tmpvar_28 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_17 = tmpvar_28;
fixed3 y_29;
y_29 = (((bakedDirTex_17).xyz - 0.5));
fixed tmpvar_30;
tmpvar_30 = max(0.0001, (bakedDirTex_17).w);
tmpvar_20 = (((((((unity_Lightmap_HDR).x * (color_27).xyz)) * ((dot(normalWorld_16, y_29) + 0.5)))) / tmpvar_30));
tmpvar_2 = tmpvar_19;
half3 viewDir_31;
viewDir_31 = tmpvar_8;
fixed4 c_32;
fixed4 c_33;
float nh_34;
fixed diff_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_35 = tmpvar_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_31)))));
nh_34 = tmpvar_37;
half y_38;
y_38 = ((_Shininess * 128.0));
float tmpvar_39;
tmpvar_39 = ((pow(nh_34, y_38) * (tmpvar_11).w));
(c_33).xyz = ((((((tmpvar_10 * tmpvar_19)) * diff_35)) + ((((tmpvar_19 * (_SpecColor).xyz)) * tmpvar_39))));
(c_33).w = (((tmpvar_11).w * (_Color).w));
(c_32).w = (c_33).w;
(c_32).xyz = (((c_33).xyz + ((tmpvar_10 * tmpvar_20))));
float tmpvar_40;
tmpvar_40 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_32).xyz, ((float3)(tmpvar_40)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
float4 tmpvar_8;
tmpvar_8 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_7 = (tmpvar_8).xyz;
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_9[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_9[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul(tmpvar_9, v._glesNormal)));
float3x3 tmpvar_11;
tmpvar_11[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_11[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_11[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_12;
tmpvar_12 = normalize((mul((v._glesTANGENT).xyz, tmpvar_11)));
worldTangent_3 = tmpvar_12;
float tmpvar_13;
tmpvar_13 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_13;
float3 tmpvar_14;
tmpvar_14 = (((((((tmpvar_10).yzx * (worldTangent_3).zxy)) - (((tmpvar_10).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_14;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).x;
(tmpvar_15).y = (worldBinormal_1).x;
(tmpvar_15).z = (tmpvar_10).x;
(tmpvar_15).w = (tmpvar_7).x;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).y;
(tmpvar_16).y = (worldBinormal_1).y;
(tmpvar_16).z = (tmpvar_10).y;
(tmpvar_16).w = (tmpvar_7).y;
float4 tmpvar_17;
(tmpvar_17).x = (worldTangent_3).z;
(tmpvar_17).y = (worldBinormal_1).z;
(tmpvar_17).z = (tmpvar_10).z;
(tmpvar_17).w = (tmpvar_7).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_10;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_8, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_15).xyz;
o.xlv_TEXCOORD7 = (tmpvar_16).xyz;
o.xlv_TEXCOORD8 = (tmpvar_17).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
fixed4 bakedDirTex_17;
half bakedAtten_18;
half3 tmpvar_19;
half3 tmpvar_20;
fixed tmpvar_21;
tmpvar_21 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_18 = tmpvar_21;
float4 v_22;
(v_22).x = (transpose(unity_MatrixV)[0]).z;
(v_22).y = (transpose(unity_MatrixV)[1]).z;
(v_22).z = (transpose(unity_MatrixV)[2]).z;
(v_22).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_23;
tmpvar_23 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_24;
float tmpvar_25;
tmpvar_25 = clamp(((((lerp(dot(tmpvar_9, (v_22).xyz), sqrt(dot(tmpvar_23, tmpvar_23)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_24 = tmpvar_25;
tmpvar_19 = ((tmpvar_2 * lerp(tmpvar_1, bakedAtten_18, tmpvar_24)));
half3 ambient_26;
half4 tmpvar_27;
(tmpvar_27).w = 1.0;
(tmpvar_27).xyz = normalWorld_16;
half3 x_28;
(x_28).x = dot(unity_SHAr, tmpvar_27);
(x_28).y = dot(unity_SHAg, tmpvar_27);
(x_28).z = dot(unity_SHAb, tmpvar_27);
half3 x1_29;
half4 tmpvar_30;
tmpvar_30 = (((normalWorld_16).xyzz * (normalWorld_16).yzzx));
(x1_29).x = dot(unity_SHBr, tmpvar_30);
(x1_29).y = dot(unity_SHBg, tmpvar_30);
(x1_29).z = dot(unity_SHBb, tmpvar_30);
ambient_26 = max(float3(0.0, 0.0, 0.0), ((x_28 + ((x1_29 + (((unity_SHC).xyz * (((((normalWorld_16).x * (normalWorld_16).x)) - (((normalWorld_16).y * (normalWorld_16).y)))))))))));
half3 tmpvar_31;
tmpvar_31 = max(((((1.055 * pow(max(ambient_26, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_26 = tmpvar_31;
half4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_33;
color_33 = tmpvar_32;
half4 tmpvar_34;
tmpvar_34 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_17 = tmpvar_34;
fixed3 y_35;
y_35 = (((bakedDirTex_17).xyz - 0.5));
fixed tmpvar_36;
tmpvar_36 = max(0.0001, (bakedDirTex_17).w);
tmpvar_20 = ((tmpvar_31 + (((((((unity_Lightmap_HDR).x * (color_33).xyz)) * ((dot(normalWorld_16, y_35) + 0.5)))) / tmpvar_36))));
tmpvar_2 = tmpvar_19;
half3 viewDir_37;
viewDir_37 = tmpvar_8;
fixed4 c_38;
fixed4 c_39;
float nh_40;
fixed diff_41;
half tmpvar_42;
tmpvar_42 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_41 = tmpvar_42;
half tmpvar_43;
tmpvar_43 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_37)))));
nh_40 = tmpvar_43;
half y_44;
y_44 = ((_Shininess * 128.0));
float tmpvar_45;
tmpvar_45 = ((pow(nh_40, y_44) * (tmpvar_11).w));
(c_39).xyz = ((((((tmpvar_10 * tmpvar_19)) * diff_41)) + ((((tmpvar_19 * (_SpecColor).xyz)) * tmpvar_45))));
(c_39).w = (((tmpvar_11).w * (_Color).w));
(c_38).w = (c_39).w;
(c_38).xyz = (((c_39).xyz + ((tmpvar_10 * tmpvar_20))));
float tmpvar_46;
tmpvar_46 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_38).xyz, ((float3)(tmpvar_46)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
float4 tmpvar_8;
tmpvar_8 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_7 = (tmpvar_8).xyz;
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_9[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_9[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul(tmpvar_9, v._glesNormal)));
float3x3 tmpvar_11;
tmpvar_11[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_11[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_11[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_12;
tmpvar_12 = normalize((mul((v._glesTANGENT).xyz, tmpvar_11)));
worldTangent_3 = tmpvar_12;
float tmpvar_13;
tmpvar_13 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_13;
float3 tmpvar_14;
tmpvar_14 = (((((((tmpvar_10).yzx * (worldTangent_3).zxy)) - (((tmpvar_10).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_14;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).x;
(tmpvar_15).y = (worldBinormal_1).x;
(tmpvar_15).z = (tmpvar_10).x;
(tmpvar_15).w = (tmpvar_7).x;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).y;
(tmpvar_16).y = (worldBinormal_1).y;
(tmpvar_16).z = (tmpvar_10).y;
(tmpvar_16).w = (tmpvar_7).y;
float4 tmpvar_17;
(tmpvar_17).x = (worldTangent_3).z;
(tmpvar_17).y = (worldBinormal_1).z;
(tmpvar_17).z = (tmpvar_10).z;
(tmpvar_17).w = (tmpvar_7).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_10;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_8, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_15).xyz;
o.xlv_TEXCOORD7 = (tmpvar_16).xyz;
o.xlv_TEXCOORD8 = (tmpvar_17).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
half4 realtimeDirTex_17;
fixed4 bakedDirTex_18;
half bakedAtten_19;
half3 tmpvar_20;
half3 tmpvar_21;
fixed tmpvar_22;
tmpvar_22 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_19 = tmpvar_22;
float4 v_23;
(v_23).x = (transpose(unity_MatrixV)[0]).z;
(v_23).y = (transpose(unity_MatrixV)[1]).z;
(v_23).z = (transpose(unity_MatrixV)[2]).z;
(v_23).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_24;
tmpvar_24 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_25;
float tmpvar_26;
tmpvar_26 = clamp(((((lerp(dot(tmpvar_9, (v_23).xyz), sqrt(dot(tmpvar_24, tmpvar_24)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_25 = tmpvar_26;
tmpvar_20 = ((tmpvar_2 * lerp(tmpvar_1, bakedAtten_19, tmpvar_25)));
half4 tmpvar_27;
tmpvar_27 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_28;
color_28 = tmpvar_27;
half4 tmpvar_29;
tmpvar_29 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_18 = tmpvar_29;
fixed3 y_30;
y_30 = (((bakedDirTex_18).xyz - 0.5));
fixed tmpvar_31;
tmpvar_31 = max(0.0001, (bakedDirTex_18).w);
tmpvar_21 = (((((((unity_Lightmap_HDR).x * (color_28).xyz)) * ((dot(normalWorld_16, y_30) + 0.5)))) / tmpvar_31));
fixed4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_33;
tmpvar_33 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_17 = tmpvar_33;
fixed4 dirTex_34;
dirTex_34 = realtimeDirTex_17;
fixed3 y_35;
y_35 = (((dirTex_34).xyz - 0.5));
fixed tmpvar_36;
tmpvar_36 = max(0.0001, (dirTex_34).w);
tmpvar_21 = ((tmpvar_21 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_32).w)) * (tmpvar_32).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_16, y_35) + 0.5)))) / tmpvar_36))));
tmpvar_2 = tmpvar_20;
half3 viewDir_37;
viewDir_37 = tmpvar_8;
fixed4 c_38;
fixed4 c_39;
float nh_40;
fixed diff_41;
half tmpvar_42;
tmpvar_42 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_41 = tmpvar_42;
half tmpvar_43;
tmpvar_43 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_37)))));
nh_40 = tmpvar_43;
half y_44;
y_44 = ((_Shininess * 128.0));
float tmpvar_45;
tmpvar_45 = ((pow(nh_40, y_44) * (tmpvar_11).w));
(c_39).xyz = ((((((tmpvar_10 * tmpvar_20)) * diff_41)) + ((((tmpvar_20 * (_SpecColor).xyz)) * tmpvar_45))));
(c_39).w = (((tmpvar_11).w * (_Color).w));
(c_38).w = (c_39).w;
(c_38).xyz = (((c_39).xyz + ((tmpvar_10 * tmpvar_21))));
float tmpvar_46;
tmpvar_46 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_38).xyz, ((float3)(tmpvar_46)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
float4 tmpvar_8;
tmpvar_8 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_7 = (tmpvar_8).xyz;
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_9[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_9[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul(tmpvar_9, v._glesNormal)));
float3x3 tmpvar_11;
tmpvar_11[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_11[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_11[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_12;
tmpvar_12 = normalize((mul((v._glesTANGENT).xyz, tmpvar_11)));
worldTangent_3 = tmpvar_12;
float tmpvar_13;
tmpvar_13 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_13;
float3 tmpvar_14;
tmpvar_14 = (((((((tmpvar_10).yzx * (worldTangent_3).zxy)) - (((tmpvar_10).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_14;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).x;
(tmpvar_15).y = (worldBinormal_1).x;
(tmpvar_15).z = (tmpvar_10).x;
(tmpvar_15).w = (tmpvar_7).x;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).y;
(tmpvar_16).y = (worldBinormal_1).y;
(tmpvar_16).z = (tmpvar_10).y;
(tmpvar_16).w = (tmpvar_7).y;
float4 tmpvar_17;
(tmpvar_17).x = (worldTangent_3).z;
(tmpvar_17).y = (worldBinormal_1).z;
(tmpvar_17).z = (tmpvar_10).z;
(tmpvar_17).w = (tmpvar_7).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_10;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_8, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_15).xyz;
o.xlv_TEXCOORD7 = (tmpvar_16).xyz;
o.xlv_TEXCOORD8 = (tmpvar_17).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
fixed4 bakedDirTex_17;
half bakedAtten_18;
half3 tmpvar_19;
half3 tmpvar_20;
fixed tmpvar_21;
tmpvar_21 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_18 = tmpvar_21;
float4 v_22;
(v_22).x = (transpose(unity_MatrixV)[0]).z;
(v_22).y = (transpose(unity_MatrixV)[1]).z;
(v_22).z = (transpose(unity_MatrixV)[2]).z;
(v_22).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_23;
tmpvar_23 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_24;
float tmpvar_25;
tmpvar_25 = clamp(((((lerp(dot(tmpvar_9, (v_22).xyz), sqrt(dot(tmpvar_23, tmpvar_23)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_24 = tmpvar_25;
tmpvar_19 = ((tmpvar_2 * min(clamp(((tmpvar_1 + tmpvar_24)), 0.0, 1.0), bakedAtten_18)));
half4 tmpvar_26;
tmpvar_26 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_27;
color_27 = tmpvar_26;
half4 tmpvar_28;
tmpvar_28 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_17 = tmpvar_28;
fixed3 y_29;
y_29 = (((bakedDirTex_17).xyz - 0.5));
fixed tmpvar_30;
tmpvar_30 = max(0.0001, (bakedDirTex_17).w);
tmpvar_20 = (((((((unity_Lightmap_HDR).x * (color_27).xyz)) * ((dot(normalWorld_16, y_29) + 0.5)))) / tmpvar_30));
tmpvar_2 = tmpvar_19;
half3 viewDir_31;
viewDir_31 = tmpvar_8;
fixed4 c_32;
fixed4 c_33;
float nh_34;
fixed diff_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_35 = tmpvar_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_31)))));
nh_34 = tmpvar_37;
half y_38;
y_38 = ((_Shininess * 128.0));
float tmpvar_39;
tmpvar_39 = ((pow(nh_34, y_38) * (tmpvar_11).w));
(c_33).xyz = ((((((tmpvar_10 * tmpvar_19)) * diff_35)) + ((((tmpvar_19 * (_SpecColor).xyz)) * tmpvar_39))));
(c_33).w = (((tmpvar_11).w * (_Color).w));
(c_32).w = (c_33).w;
(c_32).xyz = (((c_33).xyz + ((tmpvar_10 * tmpvar_20))));
float tmpvar_40;
tmpvar_40 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_32).xyz, ((float3)(tmpvar_40)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
float4 tmpvar_8;
tmpvar_8 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_7 = (tmpvar_8).xyz;
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_9[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_9[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul(tmpvar_9, v._glesNormal)));
float3x3 tmpvar_11;
tmpvar_11[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_11[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_11[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_12;
tmpvar_12 = normalize((mul((v._glesTANGENT).xyz, tmpvar_11)));
worldTangent_3 = tmpvar_12;
float tmpvar_13;
tmpvar_13 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_13;
float3 tmpvar_14;
tmpvar_14 = (((((((tmpvar_10).yzx * (worldTangent_3).zxy)) - (((tmpvar_10).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_14;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).x;
(tmpvar_15).y = (worldBinormal_1).x;
(tmpvar_15).z = (tmpvar_10).x;
(tmpvar_15).w = (tmpvar_7).x;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).y;
(tmpvar_16).y = (worldBinormal_1).y;
(tmpvar_16).z = (tmpvar_10).y;
(tmpvar_16).w = (tmpvar_7).y;
float4 tmpvar_17;
(tmpvar_17).x = (worldTangent_3).z;
(tmpvar_17).y = (worldBinormal_1).z;
(tmpvar_17).z = (tmpvar_10).z;
(tmpvar_17).w = (tmpvar_7).z;
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_10;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_8, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_15).xyz;
o.xlv_TEXCOORD7 = (tmpvar_16).xyz;
o.xlv_TEXCOORD8 = (tmpvar_17).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
fixed4 bakedDirTex_17;
half bakedAtten_18;
half3 tmpvar_19;
half3 tmpvar_20;
fixed tmpvar_21;
tmpvar_21 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_18 = tmpvar_21;
float4 v_22;
(v_22).x = (transpose(unity_MatrixV)[0]).z;
(v_22).y = (transpose(unity_MatrixV)[1]).z;
(v_22).z = (transpose(unity_MatrixV)[2]).z;
(v_22).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_23;
tmpvar_23 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_24;
float tmpvar_25;
tmpvar_25 = clamp(((((lerp(dot(tmpvar_9, (v_22).xyz), sqrt(dot(tmpvar_23, tmpvar_23)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_24 = tmpvar_25;
tmpvar_19 = ((tmpvar_2 * min(clamp(((tmpvar_1 + tmpvar_24)), 0.0, 1.0), bakedAtten_18)));
half3 ambient_26;
half4 tmpvar_27;
(tmpvar_27).w = 1.0;
(tmpvar_27).xyz = normalWorld_16;
half3 x_28;
(x_28).x = dot(unity_SHAr, tmpvar_27);
(x_28).y = dot(unity_SHAg, tmpvar_27);
(x_28).z = dot(unity_SHAb, tmpvar_27);
half3 x1_29;
half4 tmpvar_30;
tmpvar_30 = (((normalWorld_16).xyzz * (normalWorld_16).yzzx));
(x1_29).x = dot(unity_SHBr, tmpvar_30);
(x1_29).y = dot(unity_SHBg, tmpvar_30);
(x1_29).z = dot(unity_SHBb, tmpvar_30);
ambient_26 = max(float3(0.0, 0.0, 0.0), ((x_28 + ((x1_29 + (((unity_SHC).xyz * (((((normalWorld_16).x * (normalWorld_16).x)) - (((normalWorld_16).y * (normalWorld_16).y)))))))))));
half3 tmpvar_31;
tmpvar_31 = max(((((1.055 * pow(max(ambient_26, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_26 = tmpvar_31;
half4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_33;
color_33 = tmpvar_32;
half4 tmpvar_34;
tmpvar_34 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_17 = tmpvar_34;
fixed3 y_35;
y_35 = (((bakedDirTex_17).xyz - 0.5));
fixed tmpvar_36;
tmpvar_36 = max(0.0001, (bakedDirTex_17).w);
tmpvar_20 = ((tmpvar_31 + (((((((unity_Lightmap_HDR).x * (color_33).xyz)) * ((dot(normalWorld_16, y_35) + 0.5)))) / tmpvar_36))));
tmpvar_2 = tmpvar_19;
half3 viewDir_37;
viewDir_37 = tmpvar_8;
fixed4 c_38;
fixed4 c_39;
float nh_40;
fixed diff_41;
half tmpvar_42;
tmpvar_42 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_41 = tmpvar_42;
half tmpvar_43;
tmpvar_43 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_37)))));
nh_40 = tmpvar_43;
half y_44;
y_44 = ((_Shininess * 128.0));
float tmpvar_45;
tmpvar_45 = ((pow(nh_40, y_44) * (tmpvar_11).w));
(c_39).xyz = ((((((tmpvar_10 * tmpvar_19)) * diff_41)) + ((((tmpvar_19 * (_SpecColor).xyz)) * tmpvar_45))));
(c_39).w = (((tmpvar_11).w * (_Color).w));
(c_38).w = (c_39).w;
(c_38).xyz = (((c_39).xyz + ((tmpvar_10 * tmpvar_20))));
float tmpvar_46;
tmpvar_46 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_38).xyz, ((float3)(tmpvar_46)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && !defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float3 xlv_TEXCOORD6 : TEXCOORD6;
float3 xlv_TEXCOORD7 : TEXCOORD7;
float3 xlv_TEXCOORD8 : TEXCOORD8;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
float4 tmpvar_8;
tmpvar_8 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
tmpvar_7 = (tmpvar_8).xyz;
float3x3 tmpvar_9;
tmpvar_9[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_9[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_9[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_10;
tmpvar_10 = normalize((mul(tmpvar_9, v._glesNormal)));
float3x3 tmpvar_11;
tmpvar_11[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_11[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_11[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_12;
tmpvar_12 = normalize((mul((v._glesTANGENT).xyz, tmpvar_11)));
worldTangent_3 = tmpvar_12;
float tmpvar_13;
tmpvar_13 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_13;
float3 tmpvar_14;
tmpvar_14 = (((((((tmpvar_10).yzx * (worldTangent_3).zxy)) - (((tmpvar_10).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_14;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).x;
(tmpvar_15).y = (worldBinormal_1).x;
(tmpvar_15).z = (tmpvar_10).x;
(tmpvar_15).w = (tmpvar_7).x;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).y;
(tmpvar_16).y = (worldBinormal_1).y;
(tmpvar_16).z = (tmpvar_10).y;
(tmpvar_16).w = (tmpvar_7).y;
float4 tmpvar_17;
(tmpvar_17).x = (worldTangent_3).z;
(tmpvar_17).y = (worldBinormal_1).z;
(tmpvar_17).z = (tmpvar_10).z;
(tmpvar_17).w = (tmpvar_7).z;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_10;
o.xlv_TEXCOORD2 = tmpvar_7;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD4 = (mul(tmpvar_8, transpose(unity_WorldToShadow[0])));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_15).xyz;
o.xlv_TEXCOORD7 = (tmpvar_16).xyz;
o.xlv_TEXCOORD8 = (tmpvar_17).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed3 tmpvar_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
float3 tmpvar_8;
float3 tmpvar_9;
tmpvar_9 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_8 = normalize(tmpvar_9);
tmpvar_5 = i.xlv_TEXCOORD1;
fixed3 tmpvar_10;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_10 = (((tmpvar_11).xyz * (_Color).xyz));
fixed tmpvar_12;
float lightShadowDataX_13;
half tmpvar_14;
tmpvar_14 = (_LightShadowData).x;
lightShadowDataX_13 = tmpvar_14;
float tmpvar_15;
tmpvar_15 = max(((float)((((tex2D(_ShadowMapTexture, (i.xlv_TEXCOORD4).xy)).x > (i.xlv_TEXCOORD4).z)))), lightShadowDataX_13);
tmpvar_12 = tmpvar_15;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_6;
tmpvar_1 = tmpvar_12;
half3 normalWorld_16;
normalWorld_16 = tmpvar_5;
half4 realtimeDirTex_17;
fixed4 bakedDirTex_18;
half bakedAtten_19;
half3 tmpvar_20;
half3 tmpvar_21;
fixed tmpvar_22;
tmpvar_22 = clamp(dot(SunnySampleShadowMask((i.xlv_TEXCOORD3).xy), unity_OcclusionMaskSelector), 0.0, 1.0);
bakedAtten_19 = tmpvar_22;
float4 v_23;
(v_23).x = (transpose(unity_MatrixV)[0]).z;
(v_23).y = (transpose(unity_MatrixV)[1]).z;
(v_23).z = (transpose(unity_MatrixV)[2]).z;
(v_23).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_24;
tmpvar_24 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_25;
float tmpvar_26;
tmpvar_26 = clamp(((((lerp(dot(tmpvar_9, (v_23).xyz), sqrt(dot(tmpvar_24, tmpvar_24)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_25 = tmpvar_26;
tmpvar_20 = ((tmpvar_2 * min(clamp(((tmpvar_1 + tmpvar_25)), 0.0, 1.0), bakedAtten_19)));
half4 tmpvar_27;
tmpvar_27 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_28;
color_28 = tmpvar_27;
half4 tmpvar_29;
tmpvar_29 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_18 = tmpvar_29;
fixed3 y_30;
y_30 = (((bakedDirTex_18).xyz - 0.5));
fixed tmpvar_31;
tmpvar_31 = max(0.0001, (bakedDirTex_18).w);
tmpvar_21 = (((((((unity_Lightmap_HDR).x * (color_28).xyz)) * ((dot(normalWorld_16, y_30) + 0.5)))) / tmpvar_31));
fixed4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_33;
tmpvar_33 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_17 = tmpvar_33;
fixed4 dirTex_34;
dirTex_34 = realtimeDirTex_17;
fixed3 y_35;
y_35 = (((dirTex_34).xyz - 0.5));
fixed tmpvar_36;
tmpvar_36 = max(0.0001, (dirTex_34).w);
tmpvar_21 = ((tmpvar_21 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_32).w)) * (tmpvar_32).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_16, y_35) + 0.5)))) / tmpvar_36))));
tmpvar_2 = tmpvar_20;
half3 viewDir_37;
viewDir_37 = tmpvar_8;
fixed4 c_38;
fixed4 c_39;
float nh_40;
fixed diff_41;
half tmpvar_42;
tmpvar_42 = max(0.0, dot(tmpvar_5, tmpvar_3));
diff_41 = tmpvar_42;
half tmpvar_43;
tmpvar_43 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_3 + viewDir_37)))));
nh_40 = tmpvar_43;
half y_44;
y_44 = ((_Shininess * 128.0));
float tmpvar_45;
tmpvar_45 = ((pow(nh_40, y_44) * (tmpvar_11).w));
(c_39).xyz = ((((((tmpvar_10 * tmpvar_20)) * diff_41)) + ((((tmpvar_20 * (_SpecColor).xyz)) * tmpvar_45))));
(c_39).w = (((tmpvar_11).w * (_Color).w));
(c_38).w = (c_39).w;
(c_38).xyz = (((c_39).xyz + ((tmpvar_10 * tmpvar_21))));
float tmpvar_46;
tmpvar_46 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_38).xyz, ((float3)(tmpvar_46)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && !defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

half3 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_4;
tmpvar_4 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_5;
tmpvar_5[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_5[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_5[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_6;
tmpvar_6 = normalize((mul(tmpvar_5, v._glesNormal)));
float3 lightColor0_7;
lightColor0_7 = (unity_LightColor[0]).xyz;
float3 lightColor1_8;
lightColor1_8 = (unity_LightColor[1]).xyz;
float3 lightColor2_9;
lightColor2_9 = (unity_LightColor[2]).xyz;
float3 lightColor3_10;
lightColor3_10 = (unity_LightColor[3]).xyz;
float4 lightAttenSq_11;
lightAttenSq_11 = unity_4LightAtten0;
float3 col_12;
float4 ndotl_13;
float4 lengthSq_14;
float4 tmpvar_15;
tmpvar_15 = ((unity_4LightPosX0 - (tmpvar_4).x));
float4 tmpvar_16;
tmpvar_16 = ((unity_4LightPosY0 - (tmpvar_4).y));
float4 tmpvar_17;
tmpvar_17 = ((unity_4LightPosZ0 - (tmpvar_4).z));
lengthSq_14 = ((tmpvar_15 * tmpvar_15));
lengthSq_14 = ((lengthSq_14 + ((tmpvar_16 * tmpvar_16))));
lengthSq_14 = ((lengthSq_14 + ((tmpvar_17 * tmpvar_17))));
float4 tmpvar_18;
tmpvar_18 = max(lengthSq_14, float4(1e-06, 1e-06, 1e-06, 1e-06));
lengthSq_14 = tmpvar_18;
ndotl_13 = ((tmpvar_15 * (tmpvar_6).x));
ndotl_13 = ((ndotl_13 + ((tmpvar_16 * (tmpvar_6).y))));
ndotl_13 = ((ndotl_13 + ((tmpvar_17 * (tmpvar_6).z))));
float4 tmpvar_19;
tmpvar_19 = max(float4(0.0, 0.0, 0.0, 0.0), ((ndotl_13 * rsqrt(tmpvar_18))));
ndotl_13 = tmpvar_19;
float4 tmpvar_20;
tmpvar_20 = ((tmpvar_19 * ((1.0 / (((1.0 + ((tmpvar_18 * lightAttenSq_11)))))))));
col_12 = ((lightColor0_7 * (tmpvar_20).x));
col_12 = ((col_12 + ((lightColor1_8 * (tmpvar_20).y))));
col_12 = ((col_12 + ((lightColor2_9 * (tmpvar_20).z))));
col_12 = ((col_12 + ((lightColor3_10 * (tmpvar_20).w))));
tmpvar_1 = col_12;
half3 normal_21;
normal_21 = tmpvar_6;
half3 ambient_22;
half4 tmpvar_23;
(tmpvar_23).w = 1.0;
(tmpvar_23).xyz = normal_21;
half3 res_24;
half3 x_25;
(x_25).x = dot(unity_SHAr, tmpvar_23);
(x_25).y = dot(unity_SHAg, tmpvar_23);
(x_25).z = dot(unity_SHAb, tmpvar_23);
half3 x1_26;
half4 tmpvar_27;
tmpvar_27 = (((normal_21).xyzz * (normal_21).yzzx));
(x1_26).x = dot(unity_SHBr, tmpvar_27);
(x1_26).y = dot(unity_SHBg, tmpvar_27);
(x1_26).z = dot(unity_SHBb, tmpvar_27);
res_24 = ((x_25 + ((x1_26 + (((unity_SHC).xyz * (((((normal_21).x * (normal_21).x)) - (((normal_21).y * (normal_21).y))))))))));
half3 tmpvar_28;
tmpvar_28 = max(((((1.055 * pow(max(res_24, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_24 = tmpvar_28;
ambient_22 = ((tmpvar_1 + max(float3(0.0, 0.0, 0.0), tmpvar_28)));
tmpvar_1 = ambient_22;
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_6;
o.xlv_TEXCOORD2 = tmpvar_4;
o.xlv_TEXCOORD3 = ambient_22;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_4 = (((tmpvar_9).xyz * (_Color).xyz));
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
half3 viewDir_10;
viewDir_10 = worldViewDir_6;
fixed4 c_11;
fixed4 c_12;
float nh_13;
fixed diff_14;
half tmpvar_15;
tmpvar_15 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_14 = tmpvar_15;
half tmpvar_16;
tmpvar_16 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_10)))));
nh_13 = tmpvar_16;
half y_17;
y_17 = ((_Shininess * 128.0));
float tmpvar_18;
tmpvar_18 = ((pow(nh_13, y_17) * (tmpvar_9).w));
(c_12).xyz = ((((((tmpvar_4 * tmpvar_1)) * diff_14)) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_18))));
(c_12).w = (((tmpvar_9).w * (_Color).w));
(c_11).w = (c_12).w;
(c_11).xyz = (((c_12).xyz + ((tmpvar_4 * i.xlv_TEXCOORD3))));
float tmpvar_19;
tmpvar_19 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_3).xyz = lerp((unity_FogColor).xyz, (c_11).xyz, ((float3)(tmpvar_19)));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && !defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float4 xlv_TEXCOORD6 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

half3 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (v._glesVertex).xyz;
tmpvar_4 = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_6;
tmpvar_6 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_7;
tmpvar_7[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_7[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_7[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_8;
tmpvar_8 = normalize((mul(tmpvar_7, v._glesNormal)));
float3 lightColor0_9;
lightColor0_9 = (unity_LightColor[0]).xyz;
float3 lightColor1_10;
lightColor1_10 = (unity_LightColor[1]).xyz;
float3 lightColor2_11;
lightColor2_11 = (unity_LightColor[2]).xyz;
float3 lightColor3_12;
lightColor3_12 = (unity_LightColor[3]).xyz;
float4 lightAttenSq_13;
lightAttenSq_13 = unity_4LightAtten0;
float3 col_14;
float4 ndotl_15;
float4 lengthSq_16;
float4 tmpvar_17;
tmpvar_17 = ((unity_4LightPosX0 - (tmpvar_6).x));
float4 tmpvar_18;
tmpvar_18 = ((unity_4LightPosY0 - (tmpvar_6).y));
float4 tmpvar_19;
tmpvar_19 = ((unity_4LightPosZ0 - (tmpvar_6).z));
lengthSq_16 = ((tmpvar_17 * tmpvar_17));
lengthSq_16 = ((lengthSq_16 + ((tmpvar_18 * tmpvar_18))));
lengthSq_16 = ((lengthSq_16 + ((tmpvar_19 * tmpvar_19))));
float4 tmpvar_20;
tmpvar_20 = max(lengthSq_16, float4(1e-06, 1e-06, 1e-06, 1e-06));
lengthSq_16 = tmpvar_20;
ndotl_15 = ((tmpvar_17 * (tmpvar_8).x));
ndotl_15 = ((ndotl_15 + ((tmpvar_18 * (tmpvar_8).y))));
ndotl_15 = ((ndotl_15 + ((tmpvar_19 * (tmpvar_8).z))));
float4 tmpvar_21;
tmpvar_21 = max(float4(0.0, 0.0, 0.0, 0.0), ((ndotl_15 * rsqrt(tmpvar_20))));
ndotl_15 = tmpvar_21;
float4 tmpvar_22;
tmpvar_22 = ((tmpvar_21 * ((1.0 / (((1.0 + ((tmpvar_20 * lightAttenSq_13)))))))));
col_14 = ((lightColor0_9 * (tmpvar_22).x));
col_14 = ((col_14 + ((lightColor1_10 * (tmpvar_22).y))));
col_14 = ((col_14 + ((lightColor2_11 * (tmpvar_22).z))));
col_14 = ((col_14 + ((lightColor3_12 * (tmpvar_22).w))));
tmpvar_1 = col_14;
half3 normal_23;
normal_23 = tmpvar_8;
half3 ambient_24;
half3 x1_25;
half4 tmpvar_26;
tmpvar_26 = (((normal_23).xyzz * (normal_23).yzzx));
(x1_25).x = dot(unity_SHBr, tmpvar_26);
(x1_25).y = dot(unity_SHBg, tmpvar_26);
(x1_25).z = dot(unity_SHBb, tmpvar_26);
ambient_24 = ((((tmpvar_1 * ((((tmpvar_1 * ((((tmpvar_1 * 0.305306)) + 0.6821711)))) + 0.01252288)))) + ((x1_25 + (((unity_SHC).xyz * (((((normal_23).x * (normal_23).x)) - (((normal_23).y * (normal_23).y))))))))));
tmpvar_1 = ambient_24;
o.position = tmpvar_4;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_8;
o.xlv_TEXCOORD2 = tmpvar_6;
o.xlv_TEXCOORD3 = ambient_24;
o.xlv_TEXCOORD4 = tmpvar_2;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_4).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_3;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
float4 v_14;
(v_14).x = (transpose(unity_MatrixV)[0]).z;
(v_14).y = (transpose(unity_MatrixV)[1]).z;
(v_14).z = (transpose(unity_MatrixV)[2]).z;
(v_14).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_15;
tmpvar_15 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_16;
float tmpvar_17;
tmpvar_17 = clamp(((((lerp(dot(tmpvar_10, (v_14).xyz), sqrt(dot(tmpvar_15, tmpvar_15)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_16 = tmpvar_17;
float4 tmpvar_18;
(tmpvar_18).w = 1.0;
(tmpvar_18).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_19;
float4 shadowCoord_20;
shadowCoord_20 = (mul(tmpvar_18, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_21;
half tmpvar_22;
tmpvar_22 = (_LightShadowData).x;
lightShadowDataX_21 = tmpvar_22;
float tmpvar_23;
tmpvar_23 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_20).xy)).x > (shadowCoord_20).z)))), lightShadowDataX_21);
tmpvar_19 = tmpvar_23;
realtimeShadowAttenuation_13 = tmpvar_19;
half tmpvar_24;
tmpvar_24 = clamp(((realtimeShadowAttenuation_13 + tmpvar_16)), 0.0, 1.0);
atten_5 = tmpvar_24;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_25;
normalWorld_25 = tmpvar_6;
half3 tmpvar_26;
tmpvar_26 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_27;
(tmpvar_27).w = 1.0;
(tmpvar_27).xyz = normalWorld_25;
half3 x_28;
(x_28).x = dot(unity_SHAr, tmpvar_27);
(x_28).y = dot(unity_SHAg, tmpvar_27);
(x_28).z = dot(unity_SHAb, tmpvar_27);
half3 tmpvar_29;
tmpvar_29 = max(((((1.055 * pow(max(float3(0.0, 0.0, 0.0), ((i.xlv_TEXCOORD3 + x_28))), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
tmpvar_2 = tmpvar_26;
half3 viewDir_30;
viewDir_30 = tmpvar_9;
fixed4 c_31;
fixed4 c_32;
float nh_33;
fixed diff_34;
half tmpvar_35;
tmpvar_35 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_34 = tmpvar_35;
half tmpvar_36;
tmpvar_36 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_30)))));
nh_33 = tmpvar_36;
half y_37;
y_37 = ((_Shininess * 128.0));
float tmpvar_38;
tmpvar_38 = ((pow(nh_33, y_37) * (tmpvar_12).w));
(c_32).xyz = ((((((tmpvar_11 * tmpvar_26)) * diff_34)) + ((((tmpvar_26 * (_SpecColor).xyz)) * tmpvar_38))));
(c_32).w = (((tmpvar_12).w * (_Color).w));
(c_31).w = (c_32).w;
(c_31).xyz = (((c_32).xyz + ((tmpvar_11 * tmpvar_29))));
float tmpvar_39;
tmpvar_39 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_31).xyz, ((float3)(tmpvar_39)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float2 xlv_TEXCOORD4 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half realtimeShadowAttenuation_12;
half shadowMaskAttenuation_13;
float4 v_14;
(v_14).x = (transpose(unity_MatrixV)[0]).z;
(v_14).y = (transpose(unity_MatrixV)[1]).z;
(v_14).z = (transpose(unity_MatrixV)[2]).z;
(v_14).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_15;
tmpvar_15 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_16;
float tmpvar_17;
tmpvar_17 = clamp(((((lerp(dot(tmpvar_10, (v_14).xyz), sqrt(dot(tmpvar_15, tmpvar_15)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_16 = tmpvar_17;
fixed tmpvar_18;
tmpvar_18 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_13 = tmpvar_18;
float4 tmpvar_19;
(tmpvar_19).w = 1.0;
(tmpvar_19).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_20;
float4 shadowCoord_21;
shadowCoord_21 = (mul(tmpvar_19, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_22;
half tmpvar_23;
tmpvar_23 = (_LightShadowData).x;
lightShadowDataX_22 = tmpvar_23;
float tmpvar_24;
tmpvar_24 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_21).xy)).x > (shadowCoord_21).z)))), lightShadowDataX_22);
tmpvar_20 = tmpvar_24;
realtimeShadowAttenuation_12 = tmpvar_20;
half tmpvar_25;
tmpvar_25 = lerp(realtimeShadowAttenuation_12, shadowMaskAttenuation_13, tmpvar_16);
atten_5 = tmpvar_25;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 tmpvar_26;
tmpvar_26 = ((tmpvar_2 * tmpvar_1));
tmpvar_2 = tmpvar_26;
half3 viewDir_27;
viewDir_27 = tmpvar_9;
fixed4 c_28;
float nh_29;
fixed diff_30;
half tmpvar_31;
tmpvar_31 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_30 = tmpvar_31;
half tmpvar_32;
tmpvar_32 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_27)))));
nh_29 = tmpvar_32;
half y_33;
y_33 = ((_Shininess * 128.0));
float tmpvar_34;
tmpvar_34 = ((pow(nh_29, y_33) * (tmpvar_11).w));
(c_28).xyz = (((((((tmpvar_11).xyz * (_Color).xyz)) * ((tmpvar_26 * diff_30)))) + ((((tmpvar_26 * (_SpecColor).xyz)) * tmpvar_34))));
(c_28).w = (((tmpvar_11).w * (_Color).w));
float tmpvar_35;
tmpvar_35 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_28).xyz, ((float3)(tmpvar_35)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float4 xlv_TEXCOORD6 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

half3 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_5;
tmpvar_5 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_6;
tmpvar_6[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_6[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_6[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_7;
tmpvar_7 = normalize((mul(tmpvar_6, v._glesNormal)));
float3 lightColor0_8;
lightColor0_8 = (unity_LightColor[0]).xyz;
float3 lightColor1_9;
lightColor1_9 = (unity_LightColor[1]).xyz;
float3 lightColor2_10;
lightColor2_10 = (unity_LightColor[2]).xyz;
float3 lightColor3_11;
lightColor3_11 = (unity_LightColor[3]).xyz;
float4 lightAttenSq_12;
lightAttenSq_12 = unity_4LightAtten0;
float3 col_13;
float4 ndotl_14;
float4 lengthSq_15;
float4 tmpvar_16;
tmpvar_16 = ((unity_4LightPosX0 - (tmpvar_5).x));
float4 tmpvar_17;
tmpvar_17 = ((unity_4LightPosY0 - (tmpvar_5).y));
float4 tmpvar_18;
tmpvar_18 = ((unity_4LightPosZ0 - (tmpvar_5).z));
lengthSq_15 = ((tmpvar_16 * tmpvar_16));
lengthSq_15 = ((lengthSq_15 + ((tmpvar_17 * tmpvar_17))));
lengthSq_15 = ((lengthSq_15 + ((tmpvar_18 * tmpvar_18))));
float4 tmpvar_19;
tmpvar_19 = max(lengthSq_15, float4(1e-06, 1e-06, 1e-06, 1e-06));
lengthSq_15 = tmpvar_19;
ndotl_14 = ((tmpvar_16 * (tmpvar_7).x));
ndotl_14 = ((ndotl_14 + ((tmpvar_17 * (tmpvar_7).y))));
ndotl_14 = ((ndotl_14 + ((tmpvar_18 * (tmpvar_7).z))));
float4 tmpvar_20;
tmpvar_20 = max(float4(0.0, 0.0, 0.0, 0.0), ((ndotl_14 * rsqrt(tmpvar_19))));
ndotl_14 = tmpvar_20;
float4 tmpvar_21;
tmpvar_21 = ((tmpvar_20 * ((1.0 / (((1.0 + ((tmpvar_19 * lightAttenSq_12)))))))));
col_13 = ((lightColor0_8 * (tmpvar_21).x));
col_13 = ((col_13 + ((lightColor1_9 * (tmpvar_21).y))));
col_13 = ((col_13 + ((lightColor2_10 * (tmpvar_21).z))));
col_13 = ((col_13 + ((lightColor3_11 * (tmpvar_21).w))));
tmpvar_1 = col_13;
half3 normal_22;
normal_22 = tmpvar_7;
half3 ambient_23;
half3 x1_24;
half4 tmpvar_25;
tmpvar_25 = (((normal_22).xyzz * (normal_22).yzzx));
(x1_24).x = dot(unity_SHBr, tmpvar_25);
(x1_24).y = dot(unity_SHBg, tmpvar_25);
(x1_24).z = dot(unity_SHBb, tmpvar_25);
ambient_23 = ((((tmpvar_1 * ((((tmpvar_1 * ((((tmpvar_1 * 0.305306)) + 0.6821711)))) + 0.01252288)))) + ((x1_24 + (((unity_SHC).xyz * (((((normal_22).x * (normal_22).x)) - (((normal_22).y * (normal_22).y))))))))));
tmpvar_1 = ambient_23;
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_7;
o.xlv_TEXCOORD2 = tmpvar_5;
o.xlv_TEXCOORD3 = ambient_23;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_3).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = lerp(realtimeShadowAttenuation_13, shadowMaskAttenuation_14, tmpvar_17);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_27;
normalWorld_27 = tmpvar_6;
half3 tmpvar_28;
tmpvar_28 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_29;
(tmpvar_29).w = 1.0;
(tmpvar_29).xyz = normalWorld_27;
half3 x_30;
(x_30).x = dot(unity_SHAr, tmpvar_29);
(x_30).y = dot(unity_SHAg, tmpvar_29);
(x_30).z = dot(unity_SHAb, tmpvar_29);
half3 tmpvar_31;
tmpvar_31 = max(((((1.055 * pow(max(float3(0.0, 0.0, 0.0), ((i.xlv_TEXCOORD3 + x_30))), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
tmpvar_2 = tmpvar_28;
half3 viewDir_32;
viewDir_32 = tmpvar_9;
fixed4 c_33;
fixed4 c_34;
float nh_35;
fixed diff_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_36 = tmpvar_37;
half tmpvar_38;
tmpvar_38 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_32)))));
nh_35 = tmpvar_38;
half y_39;
y_39 = ((_Shininess * 128.0));
float tmpvar_40;
tmpvar_40 = ((pow(nh_35, y_39) * (tmpvar_12).w));
(c_34).xyz = ((((((tmpvar_11 * tmpvar_28)) * diff_36)) + ((((tmpvar_28 * (_SpecColor).xyz)) * tmpvar_40))));
(c_34).w = (((tmpvar_12).w * (_Color).w));
(c_33).w = (c_34).w;
(c_33).xyz = (((c_34).xyz + ((tmpvar_11 * tmpvar_31))));
float tmpvar_41;
tmpvar_41 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_33).xyz, ((float3)(tmpvar_41)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float2 xlv_TEXCOORD4 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = lerp(realtimeShadowAttenuation_13, shadowMaskAttenuation_14, tmpvar_17);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_27;
normalWorld_27 = tmpvar_6;
half4 realtimeDirTex_28;
half3 tmpvar_29;
half3 tmpvar_30;
tmpvar_29 = ((tmpvar_2 * tmpvar_1));
fixed4 tmpvar_31;
tmpvar_31 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
fixed4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
realtimeDirTex_28 = tmpvar_32;
fixed4 dirTex_33;
dirTex_33 = realtimeDirTex_28;
fixed3 y_34;
y_34 = (((dirTex_33).xyz - 0.5));
fixed tmpvar_35;
tmpvar_35 = max(0.0001, (dirTex_33).w);
tmpvar_30 = ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_31).w)) * (tmpvar_31).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_27, y_34) + 0.5)))) / tmpvar_35));
tmpvar_2 = tmpvar_29;
half3 viewDir_36;
viewDir_36 = tmpvar_9;
fixed4 c_37;
fixed4 c_38;
float nh_39;
fixed diff_40;
half tmpvar_41;
tmpvar_41 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_40 = tmpvar_41;
half tmpvar_42;
tmpvar_42 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_36)))));
nh_39 = tmpvar_42;
half y_43;
y_43 = ((_Shininess * 128.0));
float tmpvar_44;
tmpvar_44 = ((pow(nh_39, y_43) * (tmpvar_12).w));
(c_38).xyz = ((((((tmpvar_11 * tmpvar_29)) * diff_40)) + ((((tmpvar_29 * (_SpecColor).xyz)) * tmpvar_44))));
(c_38).w = (((tmpvar_12).w * (_Color).w));
(c_37).w = (c_38).w;
(c_37).xyz = (((c_38).xyz + ((tmpvar_11 * tmpvar_30))));
float tmpvar_45;
tmpvar_45 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_37).xyz, ((float3)(tmpvar_45)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float2 xlv_TEXCOORD4 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed4 tmpvar_11;
tmpvar_11 = tex2D(_MainTex, i.xlv_TEXCOORD0);
half realtimeShadowAttenuation_12;
half shadowMaskAttenuation_13;
float4 v_14;
(v_14).x = (transpose(unity_MatrixV)[0]).z;
(v_14).y = (transpose(unity_MatrixV)[1]).z;
(v_14).z = (transpose(unity_MatrixV)[2]).z;
(v_14).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_15;
tmpvar_15 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_16;
float tmpvar_17;
tmpvar_17 = clamp(((((lerp(dot(tmpvar_10, (v_14).xyz), sqrt(dot(tmpvar_15, tmpvar_15)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_16 = tmpvar_17;
fixed tmpvar_18;
tmpvar_18 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_13 = tmpvar_18;
float4 tmpvar_19;
(tmpvar_19).w = 1.0;
(tmpvar_19).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_20;
float4 shadowCoord_21;
shadowCoord_21 = (mul(tmpvar_19, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_22;
half tmpvar_23;
tmpvar_23 = (_LightShadowData).x;
lightShadowDataX_22 = tmpvar_23;
float tmpvar_24;
tmpvar_24 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_21).xy)).x > (shadowCoord_21).z)))), lightShadowDataX_22);
tmpvar_20 = tmpvar_24;
realtimeShadowAttenuation_12 = tmpvar_20;
half tmpvar_25;
tmpvar_25 = min(clamp(((realtimeShadowAttenuation_12 + tmpvar_16)), 0.0, 1.0), shadowMaskAttenuation_13);
atten_5 = tmpvar_25;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 tmpvar_26;
tmpvar_26 = ((tmpvar_2 * tmpvar_1));
tmpvar_2 = tmpvar_26;
half3 viewDir_27;
viewDir_27 = tmpvar_9;
fixed4 c_28;
float nh_29;
fixed diff_30;
half tmpvar_31;
tmpvar_31 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_30 = tmpvar_31;
half tmpvar_32;
tmpvar_32 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_27)))));
nh_29 = tmpvar_32;
half y_33;
y_33 = ((_Shininess * 128.0));
float tmpvar_34;
tmpvar_34 = ((pow(nh_29, y_33) * (tmpvar_11).w));
(c_28).xyz = (((((((tmpvar_11).xyz * (_Color).xyz)) * ((tmpvar_26 * diff_30)))) + ((((tmpvar_26 * (_SpecColor).xyz)) * tmpvar_34))));
(c_28).w = (((tmpvar_11).w * (_Color).w));
float tmpvar_35;
tmpvar_35 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_28).xyz, ((float3)(tmpvar_35)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
half3 xlv_TEXCOORD3 : TEXCOORD3;
float2 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
float4 xlv_TEXCOORD6 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

half3 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_5;
tmpvar_5 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_6;
tmpvar_6[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_6[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_6[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_7;
tmpvar_7 = normalize((mul(tmpvar_6, v._glesNormal)));
float3 lightColor0_8;
lightColor0_8 = (unity_LightColor[0]).xyz;
float3 lightColor1_9;
lightColor1_9 = (unity_LightColor[1]).xyz;
float3 lightColor2_10;
lightColor2_10 = (unity_LightColor[2]).xyz;
float3 lightColor3_11;
lightColor3_11 = (unity_LightColor[3]).xyz;
float4 lightAttenSq_12;
lightAttenSq_12 = unity_4LightAtten0;
float3 col_13;
float4 ndotl_14;
float4 lengthSq_15;
float4 tmpvar_16;
tmpvar_16 = ((unity_4LightPosX0 - (tmpvar_5).x));
float4 tmpvar_17;
tmpvar_17 = ((unity_4LightPosY0 - (tmpvar_5).y));
float4 tmpvar_18;
tmpvar_18 = ((unity_4LightPosZ0 - (tmpvar_5).z));
lengthSq_15 = ((tmpvar_16 * tmpvar_16));
lengthSq_15 = ((lengthSq_15 + ((tmpvar_17 * tmpvar_17))));
lengthSq_15 = ((lengthSq_15 + ((tmpvar_18 * tmpvar_18))));
float4 tmpvar_19;
tmpvar_19 = max(lengthSq_15, float4(1e-06, 1e-06, 1e-06, 1e-06));
lengthSq_15 = tmpvar_19;
ndotl_14 = ((tmpvar_16 * (tmpvar_7).x));
ndotl_14 = ((ndotl_14 + ((tmpvar_17 * (tmpvar_7).y))));
ndotl_14 = ((ndotl_14 + ((tmpvar_18 * (tmpvar_7).z))));
float4 tmpvar_20;
tmpvar_20 = max(float4(0.0, 0.0, 0.0, 0.0), ((ndotl_14 * rsqrt(tmpvar_19))));
ndotl_14 = tmpvar_20;
float4 tmpvar_21;
tmpvar_21 = ((tmpvar_20 * ((1.0 / (((1.0 + ((tmpvar_19 * lightAttenSq_12)))))))));
col_13 = ((lightColor0_8 * (tmpvar_21).x));
col_13 = ((col_13 + ((lightColor1_9 * (tmpvar_21).y))));
col_13 = ((col_13 + ((lightColor2_10 * (tmpvar_21).z))));
col_13 = ((col_13 + ((lightColor3_11 * (tmpvar_21).w))));
tmpvar_1 = col_13;
half3 normal_22;
normal_22 = tmpvar_7;
half3 ambient_23;
half3 x1_24;
half4 tmpvar_25;
tmpvar_25 = (((normal_22).xyzz * (normal_22).yzzx));
(x1_24).x = dot(unity_SHBr, tmpvar_25);
(x1_24).y = dot(unity_SHBg, tmpvar_25);
(x1_24).z = dot(unity_SHBb, tmpvar_25);
ambient_23 = ((((tmpvar_1 * ((((tmpvar_1 * ((((tmpvar_1 * 0.305306)) + 0.6821711)))) + 0.01252288)))) + ((x1_24 + (((unity_SHC).xyz * (((((normal_22).x * (normal_22).x)) - (((normal_22).y * (normal_22).y))))))))));
tmpvar_1 = ambient_23;
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_7;
o.xlv_TEXCOORD2 = tmpvar_5;
o.xlv_TEXCOORD3 = ambient_23;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_3).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = min(clamp(((realtimeShadowAttenuation_13 + tmpvar_17)), 0.0, 1.0), shadowMaskAttenuation_14);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_27;
normalWorld_27 = tmpvar_6;
half3 tmpvar_28;
tmpvar_28 = ((tmpvar_2 * tmpvar_1));
half4 tmpvar_29;
(tmpvar_29).w = 1.0;
(tmpvar_29).xyz = normalWorld_27;
half3 x_30;
(x_30).x = dot(unity_SHAr, tmpvar_29);
(x_30).y = dot(unity_SHAg, tmpvar_29);
(x_30).z = dot(unity_SHAb, tmpvar_29);
half3 tmpvar_31;
tmpvar_31 = max(((((1.055 * pow(max(float3(0.0, 0.0, 0.0), ((i.xlv_TEXCOORD3 + x_30))), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
tmpvar_2 = tmpvar_28;
half3 viewDir_32;
viewDir_32 = tmpvar_9;
fixed4 c_33;
fixed4 c_34;
float nh_35;
fixed diff_36;
half tmpvar_37;
tmpvar_37 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_36 = tmpvar_37;
half tmpvar_38;
tmpvar_38 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_32)))));
nh_35 = tmpvar_38;
half y_39;
y_39 = ((_Shininess * 128.0));
float tmpvar_40;
tmpvar_40 = ((pow(nh_35, y_39) * (tmpvar_12).w));
(c_34).xyz = ((((((tmpvar_11 * tmpvar_28)) * diff_36)) + ((((tmpvar_28 * (_SpecColor).xyz)) * tmpvar_40))));
(c_34).w = (((tmpvar_12).w * (_Color).w));
(c_33).w = (c_34).w;
(c_33).xyz = (((c_34).xyz + ((tmpvar_11 * tmpvar_31))));
float tmpvar_41;
tmpvar_41 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_33).xyz, ((float3)(tmpvar_41)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#elif (defined(DIRECTIONAL) && defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTMAP_SHADOW_MIXING) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SCREEN) && defined(SHADOWS_SHADOWMASK) && defined(VERTEXLIGHT_ON))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _ShadowMapTexture;
half _Shininess;
fixed4 _SpecColor;
float4 SunnySampleShadowMask(float2 uv) {
#if defined(LIGHTMAP_ON)
return UNITY_SAMPLE_TEX2D_SAMPLER(unity_ShadowMask, unity_Lightmap, uv);
#else
return UNITY_SAMPLE_TEX2D(unity_ShadowMask, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float2 xlv_TEXCOORD4 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float4 xlv_TEXCOORD6 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_4;
tmpvar_4[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_4[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_4[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
o.position = tmpvar_2;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_4, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half tmpvar_1;
half3 tmpvar_2;
half3 tmpvar_3;
fixed4 c_4;
fixed atten_5;
fixed3 tmpvar_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
float3 tmpvar_9;
float3 tmpvar_10;
tmpvar_10 = ((_WorldSpaceCameraPos - i.xlv_TEXCOORD2));
tmpvar_9 = normalize(tmpvar_10);
tmpvar_6 = i.xlv_TEXCOORD1;
fixed3 tmpvar_11;
fixed4 tmpvar_12;
tmpvar_12 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_11 = (((tmpvar_12).xyz * (_Color).xyz));
half realtimeShadowAttenuation_13;
half shadowMaskAttenuation_14;
float4 v_15;
(v_15).x = (transpose(unity_MatrixV)[0]).z;
(v_15).y = (transpose(unity_MatrixV)[1]).z;
(v_15).z = (transpose(unity_MatrixV)[2]).z;
(v_15).w = (transpose(unity_MatrixV)[3]).z;
float3 tmpvar_16;
tmpvar_16 = ((i.xlv_TEXCOORD2 - (unity_ShadowFadeCenterAndType).xyz));
half tmpvar_17;
float tmpvar_18;
tmpvar_18 = clamp(((((lerp(dot(tmpvar_10, (v_15).xyz), sqrt(dot(tmpvar_16, tmpvar_16)), (unity_ShadowFadeCenterAndType).w) * (_LightShadowData).z)) + (_LightShadowData).w)), 0.0, 1.0);
tmpvar_17 = tmpvar_18;
fixed tmpvar_19;
tmpvar_19 = clamp(dot(SunnySampleShadowMask(i.xlv_TEXCOORD4), unity_OcclusionMaskSelector), 0.0, 1.0);
shadowMaskAttenuation_14 = tmpvar_19;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = i.xlv_TEXCOORD2;
fixed tmpvar_21;
float4 shadowCoord_22;
shadowCoord_22 = (mul(tmpvar_20, transpose(unity_WorldToShadow[0])));
float lightShadowDataX_23;
half tmpvar_24;
tmpvar_24 = (_LightShadowData).x;
lightShadowDataX_23 = tmpvar_24;
float tmpvar_25;
tmpvar_25 = max(((float)((((tex2D(_ShadowMapTexture, (shadowCoord_22).xy)).x > (shadowCoord_22).z)))), lightShadowDataX_23);
tmpvar_21 = tmpvar_25;
realtimeShadowAttenuation_13 = tmpvar_21;
half tmpvar_26;
tmpvar_26 = min(clamp(((realtimeShadowAttenuation_13 + tmpvar_17)), 0.0, 1.0), shadowMaskAttenuation_14);
atten_5 = tmpvar_26;
tmpvar_2 = (_LightColor0).xyz;
tmpvar_3 = lightDir_7;
tmpvar_1 = atten_5;
half3 normalWorld_27;
normalWorld_27 = tmpvar_6;
half4 realtimeDirTex_28;
half3 tmpvar_29;
half3 tmpvar_30;
tmpvar_29 = ((tmpvar_2 * tmpvar_1));
fixed4 tmpvar_31;
tmpvar_31 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
fixed4 tmpvar_32;
tmpvar_32 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD6).zw);
realtimeDirTex_28 = tmpvar_32;
fixed4 dirTex_33;
dirTex_33 = realtimeDirTex_28;
fixed3 y_34;
y_34 = (((dirTex_33).xyz - 0.5));
fixed tmpvar_35;
tmpvar_35 = max(0.0001, (dirTex_33).w);
tmpvar_30 = ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_31).w)) * (tmpvar_31).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_27, y_34) + 0.5)))) / tmpvar_35));
tmpvar_2 = tmpvar_29;
half3 viewDir_36;
viewDir_36 = tmpvar_9;
fixed4 c_37;
fixed4 c_38;
float nh_39;
fixed diff_40;
half tmpvar_41;
tmpvar_41 = max(0.0, dot(tmpvar_6, tmpvar_3));
diff_40 = tmpvar_41;
half tmpvar_42;
tmpvar_42 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_3 + viewDir_36)))));
nh_39 = tmpvar_42;
half y_43;
y_43 = ((_Shininess * 128.0));
float tmpvar_44;
tmpvar_44 = ((pow(nh_39, y_43) * (tmpvar_12).w));
(c_38).xyz = ((((((tmpvar_11 * tmpvar_29)) * diff_40)) + ((((tmpvar_29 * (_SpecColor).xyz)) * tmpvar_44))));
(c_38).w = (((tmpvar_12).w * (_Color).w));
(c_37).w = (c_38).w;
(c_37).xyz = (((c_38).xyz + ((tmpvar_11 * tmpvar_30))));
float tmpvar_45;
tmpvar_45 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_4).xyz = lerp((unity_FogColor).xyz, (c_37).xyz, ((float3)(tmpvar_45)));
(c_4).w = 1.0;
o.target0 = c_4;

return o;
}

#else
#error Sunny shader keyword combination has no signed donor program
#endif
ENDCG
}
Pass {
Name "FORWARD"
Tags { "LightMode"="ForwardAdd" "RenderType"="Opaque" }
Blend One One, One One
BlendOp Add, Add
ColorMask RGBA
ZWrite Off
ZTest LEqual
Cull Back
Offset 0.0, 0.0
AlphaToMask Off
Stencil { Ref 0 ReadMask 255 WriteMask 255 Comp Always Pass Keep Fail Keep ZFail Keep }
CGPROGRAM
#pragma target 3.0
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fwdadd
#pragma multi_compile __ FOG_LINEAR
#if (!defined(DIRECTIONAL) && !defined(DIRECTIONAL_COOKIE) && !defined(FOG_LINEAR) && defined(POINT) && !defined(POINT_COOKIE) && !defined(SPOT))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _LightTexture0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4x4 unity_WorldToLight;
float4 SunnyAttenuationSample(sampler2D image, float2 uv) {
float4 value = tex2D(image, uv); value.a = value.UNITY_ATTEN_CHANNEL; return value; }
float4 SunnyLightTextureSample(sampler2D image, float2 uv) {
#if defined(POINT)
return SunnyAttenuationSample(image, uv);
#else
return tex2D(image, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
(tmpvar_1).w = 1.0;
(tmpvar_1).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_2;
tmpvar_2[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_2[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_2[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = (mul((mul(tmpvar_1, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_2, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed atten_4;
float3 lightCoord_5;
fixed3 tmpvar_6;
float3 worldViewDir_7;
fixed3 lightDir_8;
float3 tmpvar_9;
tmpvar_9 = normalize((((_WorldSpaceLightPos0).xyz - i.xlv_TEXCOORD2)));
lightDir_8 = tmpvar_9;
worldViewDir_7 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_6 = i.xlv_TEXCOORD1;
fixed4 tmpvar_10;
tmpvar_10 = tex2D(_MainTex, i.xlv_TEXCOORD0);
float4 tmpvar_11;
(tmpvar_11).w = 1.0;
(tmpvar_11).xyz = i.xlv_TEXCOORD2;
lightCoord_5 = ((mul(tmpvar_11, transpose(unity_WorldToLight)))).xyz;
float tmpvar_12;
tmpvar_12 = (SunnyLightTextureSample(_LightTexture0, ((float2)(dot(lightCoord_5, lightCoord_5))))).w;
atten_4 = tmpvar_12;
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_8;
tmpvar_1 = ((tmpvar_1 * atten_4));
half3 viewDir_13;
viewDir_13 = worldViewDir_7;
fixed4 c_14;
fixed4 c_15;
float nh_16;
fixed diff_17;
half tmpvar_18;
tmpvar_18 = max(0.0, dot(tmpvar_6, tmpvar_2));
diff_17 = tmpvar_18;
half tmpvar_19;
tmpvar_19 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_2 + viewDir_13)))));
nh_16 = tmpvar_19;
half y_20;
y_20 = ((_Shininess * 128.0));
float tmpvar_21;
tmpvar_21 = ((pow(nh_16, y_20) * (tmpvar_10).w));
(c_15).xyz = (((((((tmpvar_10).xyz * (_Color).xyz)) * ((tmpvar_1 * diff_17)))) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_21))));
(c_15).w = (((tmpvar_10).w * (_Color).w));
(c_14).w = (c_15).w;
(c_14).xyz = (c_15).xyz;
(c_3).xyz = (c_14).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRECTIONAL_COOKIE) && !defined(FOG_LINEAR) && !defined(POINT) && !defined(POINT_COOKIE) && !defined(SPOT))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
(tmpvar_1).w = 1.0;
(tmpvar_1).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_2;
tmpvar_2[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_2[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_2[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = (mul((mul(tmpvar_1, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_2, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
float3 worldViewDir_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
worldViewDir_5 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_4 = i.xlv_TEXCOORD1;
fixed4 tmpvar_8;
tmpvar_8 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_6;
half3 viewDir_9;
viewDir_9 = worldViewDir_5;
fixed4 c_10;
fixed4 c_11;
float nh_12;
fixed diff_13;
half tmpvar_14;
tmpvar_14 = max(0.0, dot(tmpvar_4, tmpvar_2));
diff_13 = tmpvar_14;
half tmpvar_15;
tmpvar_15 = max(0.0, dot(tmpvar_4, normalize(((tmpvar_2 + viewDir_9)))));
nh_12 = tmpvar_15;
half y_16;
y_16 = ((_Shininess * 128.0));
float tmpvar_17;
tmpvar_17 = ((pow(nh_12, y_16) * (tmpvar_8).w));
(c_11).xyz = (((((((tmpvar_8).xyz * (_Color).xyz)) * ((tmpvar_1 * diff_13)))) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_17))));
(c_11).w = (((tmpvar_8).w * (_Color).w));
(c_10).w = (c_11).w;
(c_10).xyz = (c_11).xyz;
(c_3).xyz = (c_10).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (!defined(DIRECTIONAL) && !defined(DIRECTIONAL_COOKIE) && !defined(FOG_LINEAR) && !defined(POINT) && !defined(POINT_COOKIE) && defined(SPOT))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _LightTexture0;
sampler2D _LightTextureB0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4x4 unity_WorldToLight;
float4 SunnyAttenuationSample(sampler2D image, float2 uv) {
float4 value = tex2D(image, uv); value.a = value.UNITY_ATTEN_CHANNEL; return value; }
float4 SunnyLightTextureSample(sampler2D image, float2 uv) {
#if defined(POINT)
return SunnyAttenuationSample(image, uv);
#else
return tex2D(image, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
(tmpvar_1).w = 1.0;
(tmpvar_1).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_2;
tmpvar_2[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_2[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_2[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = (mul((mul(tmpvar_1, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_2, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed atten_4;
float4 lightCoord_5;
fixed3 tmpvar_6;
float3 worldViewDir_7;
fixed3 lightDir_8;
float3 tmpvar_9;
tmpvar_9 = normalize((((_WorldSpaceLightPos0).xyz - i.xlv_TEXCOORD2)));
lightDir_8 = tmpvar_9;
worldViewDir_7 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_6 = i.xlv_TEXCOORD1;
fixed4 tmpvar_10;
tmpvar_10 = tex2D(_MainTex, i.xlv_TEXCOORD0);
float4 tmpvar_11;
(tmpvar_11).w = 1.0;
(tmpvar_11).xyz = i.xlv_TEXCOORD2;
lightCoord_5 = (mul(tmpvar_11, transpose(unity_WorldToLight)));
fixed tmpvar_12;
float4 tmpvar_13;
tmpvar_13 = SunnyLightTextureSample(_LightTexture0, (((((lightCoord_5).xy / (lightCoord_5).w)) + 0.5)));
tmpvar_12 = (tmpvar_13).w;
fixed tmpvar_14;
float4 tmpvar_15;
tmpvar_15 = SunnyAttenuationSample(_LightTextureB0, ((float2)(dot((lightCoord_5).xyz, (lightCoord_5).xyz))));
tmpvar_14 = (tmpvar_15).w;
float tmpvar_16;
tmpvar_16 = ((((((float)((((lightCoord_5).z > 0.0)))) * tmpvar_12)) * tmpvar_14));
atten_4 = tmpvar_16;
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_8;
tmpvar_1 = ((tmpvar_1 * atten_4));
half3 viewDir_17;
viewDir_17 = worldViewDir_7;
fixed4 c_18;
fixed4 c_19;
float nh_20;
fixed diff_21;
half tmpvar_22;
tmpvar_22 = max(0.0, dot(tmpvar_6, tmpvar_2));
diff_21 = tmpvar_22;
half tmpvar_23;
tmpvar_23 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_2 + viewDir_17)))));
nh_20 = tmpvar_23;
half y_24;
y_24 = ((_Shininess * 128.0));
float tmpvar_25;
tmpvar_25 = ((pow(nh_20, y_24) * (tmpvar_10).w));
(c_19).xyz = (((((((tmpvar_10).xyz * (_Color).xyz)) * ((tmpvar_1 * diff_21)))) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_25))));
(c_19).w = (((tmpvar_10).w * (_Color).w));
(c_18).w = (c_19).w;
(c_18).xyz = (c_19).xyz;
(c_3).xyz = (c_18).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (!defined(DIRECTIONAL) && !defined(DIRECTIONAL_COOKIE) && !defined(FOG_LINEAR) && !defined(POINT) && defined(POINT_COOKIE) && !defined(SPOT))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
samplerCUBE _LightTexture0;
sampler2D _LightTextureB0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4x4 unity_WorldToLight;
float4 SunnyAttenuationSample(sampler2D image, float2 uv) {
float4 value = tex2D(image, uv); value.a = value.UNITY_ATTEN_CHANNEL; return value; }
float4 SunnyLightTextureSample(sampler2D image, float2 uv) {
#if defined(POINT)
return SunnyAttenuationSample(image, uv);
#else
return tex2D(image, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
(tmpvar_1).w = 1.0;
(tmpvar_1).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_2;
tmpvar_2[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_2[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_2[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = (mul((mul(tmpvar_1, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_2, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed atten_4;
float3 lightCoord_5;
fixed3 tmpvar_6;
float3 worldViewDir_7;
fixed3 lightDir_8;
float3 tmpvar_9;
tmpvar_9 = normalize((((_WorldSpaceLightPos0).xyz - i.xlv_TEXCOORD2)));
lightDir_8 = tmpvar_9;
worldViewDir_7 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_6 = i.xlv_TEXCOORD1;
fixed4 tmpvar_10;
tmpvar_10 = tex2D(_MainTex, i.xlv_TEXCOORD0);
float4 tmpvar_11;
(tmpvar_11).w = 1.0;
(tmpvar_11).xyz = i.xlv_TEXCOORD2;
lightCoord_5 = ((mul(tmpvar_11, transpose(unity_WorldToLight)))).xyz;
float tmpvar_12;
tmpvar_12 = (((SunnyAttenuationSample(_LightTextureB0, ((float2)(dot(lightCoord_5, lightCoord_5))))).w * (texCUBE(_LightTexture0, lightCoord_5)).w));
atten_4 = tmpvar_12;
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_8;
tmpvar_1 = ((tmpvar_1 * atten_4));
half3 viewDir_13;
viewDir_13 = worldViewDir_7;
fixed4 c_14;
fixed4 c_15;
float nh_16;
fixed diff_17;
half tmpvar_18;
tmpvar_18 = max(0.0, dot(tmpvar_6, tmpvar_2));
diff_17 = tmpvar_18;
half tmpvar_19;
tmpvar_19 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_2 + viewDir_13)))));
nh_16 = tmpvar_19;
half y_20;
y_20 = ((_Shininess * 128.0));
float tmpvar_21;
tmpvar_21 = ((pow(nh_16, y_20) * (tmpvar_10).w));
(c_15).xyz = (((((((tmpvar_10).xyz * (_Color).xyz)) * ((tmpvar_1 * diff_17)))) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_21))));
(c_15).w = (((tmpvar_10).w * (_Color).w));
(c_14).w = (c_15).w;
(c_14).xyz = (c_15).xyz;
(c_3).xyz = (c_14).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (!defined(DIRECTIONAL) && defined(DIRECTIONAL_COOKIE) && !defined(FOG_LINEAR) && !defined(POINT) && !defined(POINT_COOKIE) && !defined(SPOT))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _LightTexture0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4x4 unity_WorldToLight;
float4 SunnyAttenuationSample(sampler2D image, float2 uv) {
float4 value = tex2D(image, uv); value.a = value.UNITY_ATTEN_CHANNEL; return value; }
float4 SunnyLightTextureSample(sampler2D image, float2 uv) {
#if defined(POINT)
return SunnyAttenuationSample(image, uv);
#else
return tex2D(image, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
(tmpvar_1).w = 1.0;
(tmpvar_1).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_2;
tmpvar_2[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_2[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_2[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = (mul((mul(tmpvar_1, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_2, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed atten_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
float4 tmpvar_10;
(tmpvar_10).w = 1.0;
(tmpvar_10).xyz = i.xlv_TEXCOORD2;
float tmpvar_11;
tmpvar_11 = (SunnyLightTextureSample(_LightTexture0, ((mul(tmpvar_10, transpose(unity_WorldToLight)))).xy)).w;
atten_4 = tmpvar_11;
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
tmpvar_1 = ((tmpvar_1 * atten_4));
half3 viewDir_12;
viewDir_12 = worldViewDir_6;
fixed4 c_13;
fixed4 c_14;
float nh_15;
fixed diff_16;
half tmpvar_17;
tmpvar_17 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_16 = tmpvar_17;
half tmpvar_18;
tmpvar_18 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_12)))));
nh_15 = tmpvar_18;
half y_19;
y_19 = ((_Shininess * 128.0));
float tmpvar_20;
tmpvar_20 = ((pow(nh_15, y_19) * (tmpvar_9).w));
(c_14).xyz = (((((((tmpvar_9).xyz * (_Color).xyz)) * ((tmpvar_1 * diff_16)))) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_20))));
(c_14).w = (((tmpvar_9).w * (_Color).w));
(c_13).w = (c_14).w;
(c_13).xyz = (c_14).xyz;
(c_3).xyz = (c_13).xyz;
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (!defined(DIRECTIONAL) && !defined(DIRECTIONAL_COOKIE) && defined(FOG_LINEAR) && defined(POINT) && !defined(POINT_COOKIE) && !defined(SPOT))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _LightTexture0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4x4 unity_WorldToLight;
float4 SunnyAttenuationSample(sampler2D image, float2 uv) {
float4 value = tex2D(image, uv); value.a = value.UNITY_ATTEN_CHANNEL; return value; }
float4 SunnyLightTextureSample(sampler2D image, float2 uv) {
#if defined(POINT)
return SunnyAttenuationSample(image, uv);
#else
return tex2D(image, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float xlv_TEXCOORD4 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
tmpvar_1 = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = tmpvar_1;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_1).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed atten_4;
float3 lightCoord_5;
fixed3 tmpvar_6;
float3 worldViewDir_7;
fixed3 lightDir_8;
float3 tmpvar_9;
tmpvar_9 = normalize((((_WorldSpaceLightPos0).xyz - i.xlv_TEXCOORD2)));
lightDir_8 = tmpvar_9;
worldViewDir_7 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_6 = i.xlv_TEXCOORD1;
fixed4 tmpvar_10;
tmpvar_10 = tex2D(_MainTex, i.xlv_TEXCOORD0);
float4 tmpvar_11;
(tmpvar_11).w = 1.0;
(tmpvar_11).xyz = i.xlv_TEXCOORD2;
lightCoord_5 = ((mul(tmpvar_11, transpose(unity_WorldToLight)))).xyz;
float tmpvar_12;
tmpvar_12 = (SunnyLightTextureSample(_LightTexture0, ((float2)(dot(lightCoord_5, lightCoord_5))))).w;
atten_4 = tmpvar_12;
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_8;
tmpvar_1 = ((tmpvar_1 * atten_4));
half3 viewDir_13;
viewDir_13 = worldViewDir_7;
fixed4 c_14;
float nh_15;
fixed diff_16;
half tmpvar_17;
tmpvar_17 = max(0.0, dot(tmpvar_6, tmpvar_2));
diff_16 = tmpvar_17;
half tmpvar_18;
tmpvar_18 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_2 + viewDir_13)))));
nh_15 = tmpvar_18;
half y_19;
y_19 = ((_Shininess * 128.0));
float tmpvar_20;
tmpvar_20 = ((pow(nh_15, y_19) * (tmpvar_10).w));
(c_14).xyz = (((((((tmpvar_10).xyz * (_Color).xyz)) * ((tmpvar_1 * diff_16)))) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_20))));
(c_14).w = (((tmpvar_10).w * (_Color).w));
float tmpvar_21;
tmpvar_21 = clamp(i.xlv_TEXCOORD4, 0.0, 1.0);
(c_3).xyz = (((c_14).xyz * ((float3)(tmpvar_21))));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (defined(DIRECTIONAL) && !defined(DIRECTIONAL_COOKIE) && defined(FOG_LINEAR) && !defined(POINT) && !defined(POINT_COOKIE) && !defined(SPOT))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float xlv_TEXCOORD4 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
tmpvar_1 = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = tmpvar_1;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_1).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed3 tmpvar_4;
float3 worldViewDir_5;
fixed3 lightDir_6;
half3 tmpvar_7;
tmpvar_7 = (_WorldSpaceLightPos0).xyz;
lightDir_6 = tmpvar_7;
worldViewDir_5 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_4 = i.xlv_TEXCOORD1;
fixed4 tmpvar_8;
tmpvar_8 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_6;
half3 viewDir_9;
viewDir_9 = worldViewDir_5;
fixed4 c_10;
float nh_11;
fixed diff_12;
half tmpvar_13;
tmpvar_13 = max(0.0, dot(tmpvar_4, tmpvar_2));
diff_12 = tmpvar_13;
half tmpvar_14;
tmpvar_14 = max(0.0, dot(tmpvar_4, normalize(((tmpvar_2 + viewDir_9)))));
nh_11 = tmpvar_14;
half y_15;
y_15 = ((_Shininess * 128.0));
float tmpvar_16;
tmpvar_16 = ((pow(nh_11, y_15) * (tmpvar_8).w));
(c_10).xyz = (((((((tmpvar_8).xyz * (_Color).xyz)) * ((tmpvar_1 * diff_12)))) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_16))));
(c_10).w = (((tmpvar_8).w * (_Color).w));
float tmpvar_17;
tmpvar_17 = clamp(i.xlv_TEXCOORD4, 0.0, 1.0);
(c_3).xyz = (((c_10).xyz * ((float3)(tmpvar_17))));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (!defined(DIRECTIONAL) && !defined(DIRECTIONAL_COOKIE) && defined(FOG_LINEAR) && !defined(POINT) && !defined(POINT_COOKIE) && defined(SPOT))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _LightTexture0;
sampler2D _LightTextureB0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4x4 unity_WorldToLight;
float4 SunnyAttenuationSample(sampler2D image, float2 uv) {
float4 value = tex2D(image, uv); value.a = value.UNITY_ATTEN_CHANNEL; return value; }
float4 SunnyLightTextureSample(sampler2D image, float2 uv) {
#if defined(POINT)
return SunnyAttenuationSample(image, uv);
#else
return tex2D(image, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float xlv_TEXCOORD4 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
tmpvar_1 = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = tmpvar_1;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_1).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed atten_4;
float4 lightCoord_5;
fixed3 tmpvar_6;
float3 worldViewDir_7;
fixed3 lightDir_8;
float3 tmpvar_9;
tmpvar_9 = normalize((((_WorldSpaceLightPos0).xyz - i.xlv_TEXCOORD2)));
lightDir_8 = tmpvar_9;
worldViewDir_7 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_6 = i.xlv_TEXCOORD1;
fixed4 tmpvar_10;
tmpvar_10 = tex2D(_MainTex, i.xlv_TEXCOORD0);
float4 tmpvar_11;
(tmpvar_11).w = 1.0;
(tmpvar_11).xyz = i.xlv_TEXCOORD2;
lightCoord_5 = (mul(tmpvar_11, transpose(unity_WorldToLight)));
fixed tmpvar_12;
float4 tmpvar_13;
tmpvar_13 = SunnyLightTextureSample(_LightTexture0, (((((lightCoord_5).xy / (lightCoord_5).w)) + 0.5)));
tmpvar_12 = (tmpvar_13).w;
fixed tmpvar_14;
float4 tmpvar_15;
tmpvar_15 = SunnyAttenuationSample(_LightTextureB0, ((float2)(dot((lightCoord_5).xyz, (lightCoord_5).xyz))));
tmpvar_14 = (tmpvar_15).w;
float tmpvar_16;
tmpvar_16 = ((((((float)((((lightCoord_5).z > 0.0)))) * tmpvar_12)) * tmpvar_14));
atten_4 = tmpvar_16;
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_8;
tmpvar_1 = ((tmpvar_1 * atten_4));
half3 viewDir_17;
viewDir_17 = worldViewDir_7;
fixed4 c_18;
float nh_19;
fixed diff_20;
half tmpvar_21;
tmpvar_21 = max(0.0, dot(tmpvar_6, tmpvar_2));
diff_20 = tmpvar_21;
half tmpvar_22;
tmpvar_22 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_2 + viewDir_17)))));
nh_19 = tmpvar_22;
half y_23;
y_23 = ((_Shininess * 128.0));
float tmpvar_24;
tmpvar_24 = ((pow(nh_19, y_23) * (tmpvar_10).w));
(c_18).xyz = (((((((tmpvar_10).xyz * (_Color).xyz)) * ((tmpvar_1 * diff_20)))) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_24))));
(c_18).w = (((tmpvar_10).w * (_Color).w));
float tmpvar_25;
tmpvar_25 = clamp(i.xlv_TEXCOORD4, 0.0, 1.0);
(c_3).xyz = (((c_18).xyz * ((float3)(tmpvar_25))));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (!defined(DIRECTIONAL) && !defined(DIRECTIONAL_COOKIE) && defined(FOG_LINEAR) && !defined(POINT) && defined(POINT_COOKIE) && !defined(SPOT))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
samplerCUBE _LightTexture0;
sampler2D _LightTextureB0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4x4 unity_WorldToLight;
float4 SunnyAttenuationSample(sampler2D image, float2 uv) {
float4 value = tex2D(image, uv); value.a = value.UNITY_ATTEN_CHANNEL; return value; }
float4 SunnyLightTextureSample(sampler2D image, float2 uv) {
#if defined(POINT)
return SunnyAttenuationSample(image, uv);
#else
return tex2D(image, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float xlv_TEXCOORD4 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
tmpvar_1 = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = tmpvar_1;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_1).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed atten_4;
float3 lightCoord_5;
fixed3 tmpvar_6;
float3 worldViewDir_7;
fixed3 lightDir_8;
float3 tmpvar_9;
tmpvar_9 = normalize((((_WorldSpaceLightPos0).xyz - i.xlv_TEXCOORD2)));
lightDir_8 = tmpvar_9;
worldViewDir_7 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_6 = i.xlv_TEXCOORD1;
fixed4 tmpvar_10;
tmpvar_10 = tex2D(_MainTex, i.xlv_TEXCOORD0);
float4 tmpvar_11;
(tmpvar_11).w = 1.0;
(tmpvar_11).xyz = i.xlv_TEXCOORD2;
lightCoord_5 = ((mul(tmpvar_11, transpose(unity_WorldToLight)))).xyz;
float tmpvar_12;
tmpvar_12 = (((SunnyAttenuationSample(_LightTextureB0, ((float2)(dot(lightCoord_5, lightCoord_5))))).w * (texCUBE(_LightTexture0, lightCoord_5)).w));
atten_4 = tmpvar_12;
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_8;
tmpvar_1 = ((tmpvar_1 * atten_4));
half3 viewDir_13;
viewDir_13 = worldViewDir_7;
fixed4 c_14;
float nh_15;
fixed diff_16;
half tmpvar_17;
tmpvar_17 = max(0.0, dot(tmpvar_6, tmpvar_2));
diff_16 = tmpvar_17;
half tmpvar_18;
tmpvar_18 = max(0.0, dot(tmpvar_6, normalize(((tmpvar_2 + viewDir_13)))));
nh_15 = tmpvar_18;
half y_19;
y_19 = ((_Shininess * 128.0));
float tmpvar_20;
tmpvar_20 = ((pow(nh_15, y_19) * (tmpvar_10).w));
(c_14).xyz = (((((((tmpvar_10).xyz * (_Color).xyz)) * ((tmpvar_1 * diff_16)))) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_20))));
(c_14).w = (((tmpvar_10).w * (_Color).w));
float tmpvar_21;
tmpvar_21 = clamp(i.xlv_TEXCOORD4, 0.0, 1.0);
(c_3).xyz = (((c_14).xyz * ((float3)(tmpvar_21))));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#elif (!defined(DIRECTIONAL) && defined(DIRECTIONAL_COOKIE) && defined(FOG_LINEAR) && !defined(POINT) && !defined(POINT_COOKIE) && !defined(SPOT))
#include "UnityCG.cginc"
fixed4 _Color;
fixed4 _LightColor0;
sampler2D _LightTexture0;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
float4x4 unity_WorldToLight;
float4 SunnyAttenuationSample(sampler2D image, float2 uv) {
float4 value = tex2D(image, uv); value.a = value.UNITY_ATTEN_CHANNEL; return value; }
float4 SunnyLightTextureSample(sampler2D image, float2 uv) {
#if defined(POINT)
return SunnyAttenuationSample(image, uv);
#else
return tex2D(image, uv);
#endif
}
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float xlv_TEXCOORD4 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
tmpvar_1 = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = tmpvar_1;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD4 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_1).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half3 tmpvar_1;
half3 tmpvar_2;
fixed4 c_3;
fixed atten_4;
fixed3 tmpvar_5;
float3 worldViewDir_6;
fixed3 lightDir_7;
half3 tmpvar_8;
tmpvar_8 = (_WorldSpaceLightPos0).xyz;
lightDir_7 = tmpvar_8;
worldViewDir_6 = normalize(((_WorldSpaceCameraPos - i.xlv_TEXCOORD2)));
tmpvar_5 = i.xlv_TEXCOORD1;
fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
float4 tmpvar_10;
(tmpvar_10).w = 1.0;
(tmpvar_10).xyz = i.xlv_TEXCOORD2;
float tmpvar_11;
tmpvar_11 = (SunnyLightTextureSample(_LightTexture0, ((mul(tmpvar_10, transpose(unity_WorldToLight)))).xy)).w;
atten_4 = tmpvar_11;
tmpvar_1 = (_LightColor0).xyz;
tmpvar_2 = lightDir_7;
tmpvar_1 = ((tmpvar_1 * atten_4));
half3 viewDir_12;
viewDir_12 = worldViewDir_6;
fixed4 c_13;
float nh_14;
fixed diff_15;
half tmpvar_16;
tmpvar_16 = max(0.0, dot(tmpvar_5, tmpvar_2));
diff_15 = tmpvar_16;
half tmpvar_17;
tmpvar_17 = max(0.0, dot(tmpvar_5, normalize(((tmpvar_2 + viewDir_12)))));
nh_14 = tmpvar_17;
half y_18;
y_18 = ((_Shininess * 128.0));
float tmpvar_19;
tmpvar_19 = ((pow(nh_14, y_18) * (tmpvar_9).w));
(c_13).xyz = (((((((tmpvar_9).xyz * (_Color).xyz)) * ((tmpvar_1 * diff_15)))) + ((((tmpvar_1 * (_SpecColor).xyz)) * tmpvar_19))));
(c_13).w = (((tmpvar_9).w * (_Color).w));
float tmpvar_20;
tmpvar_20 = clamp(i.xlv_TEXCOORD4, 0.0, 1.0);
(c_3).xyz = (((c_13).xyz * ((float3)(tmpvar_20))));
(c_3).w = 1.0;
o.target0 = c_3;

return o;
}

#else
#error Sunny shader keyword combination has no signed donor program
#endif
ENDCG
}
Pass {
Name "PREPASS"
Tags { "LightMode"="PrePassBase" "RenderType"="Opaque" }
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
#if 1
#include "UnityCG.cginc"
half _Shininess;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float3 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
(tmpvar_1).w = 1.0;
(tmpvar_1).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_2;
tmpvar_2[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_2[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_2[2] = (transpose(unity_WorldToObject)[2]).xyz;
o.position = (mul((mul(tmpvar_1, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = normalize((mul(tmpvar_2, v._glesNormal)));
o.xlv_TEXCOORD1 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 res_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD0;
(res_1).xyz = ((((tmpvar_2 * 0.5)) + 0.5));
(res_1).w = _Shininess;
o.target0 = res_1;

return o;
}

#else
#error Sunny shader keyword combination has no signed donor program
#endif
ENDCG
}
Pass {
Name "PREPASS"
Tags { "LightMode"="PrePassFinal" "RenderType"="Opaque" }
Blend One Zero, One Zero
BlendOp Add, Add
ColorMask RGBA
ZWrite Off
ZTest LEqual
Cull Back
Offset 0.0, 0.0
AlphaToMask Off
Stencil { Ref 0 ReadMask 255 WriteMask 255 Comp Always Pass Keep Fail Keep ZFail Keep }
CGPROGRAM
#pragma target 3.0
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_prepassfinal
#pragma multi_compile __ FOG_LINEAR
#if (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(SHADOWS_SHADOWMASK) && !defined(UNITY_HDR_ON)) || (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float3 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_5;
float4 tmpvar_6;
tmpvar_6 = ((tmpvar_3 * 0.5));
float2 tmpvar_7;
(tmpvar_7).x = (tmpvar_6).x;
(tmpvar_7).y = (((tmpvar_6).y * (_ProjectionParams).x));
(o_5).xy = ((tmpvar_7 + (tmpvar_6).w));
(o_5).zw = (tmpvar_3).zw;
(tmpvar_1).zw = float2(0.0, 0.0);
(tmpvar_1).xy = float2(0.0, 0.0);
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float4 tmpvar_9;
(tmpvar_9).w = 1.0;
(tmpvar_9).xyz = normalize((mul(tmpvar_8, v._glesNormal)));
half4 normal_10;
normal_10 = tmpvar_9;
half3 res_11;
half3 x_12;
(x_12).x = dot(unity_SHAr, normal_10);
(x_12).y = dot(unity_SHAg, normal_10);
(x_12).z = dot(unity_SHAb, normal_10);
half3 x1_13;
half4 tmpvar_14;
tmpvar_14 = (((normal_10).xyzz * (normal_10).yzzx));
(x1_13).x = dot(unity_SHBr, tmpvar_14);
(x1_13).y = dot(unity_SHBg, tmpvar_14);
(x1_13).z = dot(unity_SHBb, tmpvar_14);
res_11 = ((x_12 + ((x1_13 + (((unity_SHC).xyz * (((((normal_10).x * (normal_10).x)) - (((normal_10).y * (normal_10).y))))))))));
half3 tmpvar_15;
tmpvar_15 = max(((((1.055 * pow(max(res_11, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_11 = tmpvar_15;
tmpvar_2 = tmpvar_15;
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD2 = o_5;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
half4 light_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_5;
tmpvar_5 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_3 = tmpvar_5;
light_3 = (-(log2(max(light_3, float4(0.001, 0.001, 0.001, 0.001)))));
(light_3).xyz = (((light_3).xyz + i.xlv_TEXCOORD4));
fixed4 c_6;
fixed spec_7;
half tmpvar_8;
tmpvar_8 = (((light_3).w * (tmpvar_4).w));
spec_7 = tmpvar_8;
(c_6).xyz = (((((((tmpvar_4).xyz * (_Color).xyz)) * (light_3).xyz)) + (((((light_3).xyz * (_SpecColor).xyz)) * spec_7))));
(c_6).w = (((tmpvar_4).w * (_Color).w));
(c_2).xyz = (c_6).xyz;
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float3 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_5;
float4 tmpvar_6;
tmpvar_6 = ((tmpvar_3 * 0.5));
float2 tmpvar_7;
(tmpvar_7).x = (tmpvar_6).x;
(tmpvar_7).y = (((tmpvar_6).y * (_ProjectionParams).x));
(o_5).xy = ((tmpvar_7 + (tmpvar_6).w));
(o_5).zw = (tmpvar_3).zw;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = float2(0.0, 0.0);
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float4 tmpvar_9;
(tmpvar_9).w = 1.0;
(tmpvar_9).xyz = normalize((mul(tmpvar_8, v._glesNormal)));
half4 normal_10;
normal_10 = tmpvar_9;
half3 res_11;
half3 x_12;
(x_12).x = dot(unity_SHAr, normal_10);
(x_12).y = dot(unity_SHAg, normal_10);
(x_12).z = dot(unity_SHAb, normal_10);
half3 x1_13;
half4 tmpvar_14;
tmpvar_14 = (((normal_10).xyzz * (normal_10).yzzx));
(x1_13).x = dot(unity_SHBr, tmpvar_14);
(x1_13).y = dot(unity_SHBg, tmpvar_14);
(x1_13).z = dot(unity_SHBb, tmpvar_14);
res_11 = ((x_12 + ((x1_13 + (((unity_SHC).xyz * (((((normal_10).x * (normal_10).x)) - (((normal_10).y * (normal_10).y))))))))));
half3 tmpvar_15;
tmpvar_15 = max(((((1.055 * pow(max(res_11, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_11 = tmpvar_15;
tmpvar_2 = tmpvar_15;
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD2 = o_5;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
half4 light_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_5;
tmpvar_5 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_3 = tmpvar_5;
light_3 = (-(log2(max(light_3, float4(0.001, 0.001, 0.001, 0.001)))));
(light_3).xyz = (((light_3).xyz + i.xlv_TEXCOORD4));
fixed4 tmpvar_6;
tmpvar_6 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
(light_3).xyz = (((light_3).xyz + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_6).w)) * (tmpvar_6).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_7;
fixed spec_8;
half tmpvar_9;
tmpvar_9 = (((light_3).w * (tmpvar_4).w));
spec_8 = tmpvar_9;
(c_7).xyz = (((((((tmpvar_4).xyz * (_Color).xyz)) * (light_3).xyz)) + (((((light_3).xyz * (_SpecColor).xyz)) * spec_8))));
(c_7).w = (((tmpvar_4).w * (_Color).w));
(c_2).xyz = (c_7).xyz;
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (tmpvar_1).xyz;
tmpvar_4 = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_6;
float4 tmpvar_7;
tmpvar_7 = ((tmpvar_4 * 0.5));
float2 tmpvar_8;
(tmpvar_8).x = (tmpvar_7).x;
(tmpvar_8).y = (((tmpvar_7).y * (_ProjectionParams).x));
(o_6).xy = ((tmpvar_8 + (tmpvar_7).w));
(o_6).zw = (tmpvar_4).zw;
(tmpvar_2).zw = float2(0.0, 0.0);
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_9;
tmpvar_9 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_9).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_10;
(tmpvar_10).w = 1.0;
(tmpvar_10).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_10, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = tmpvar_4;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = (tmpvar_9).xyz;
o.xlv_TEXCOORD2 = o_6;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed3 lm_3;
fixed4 lmtex_4;
half4 light_5;
fixed4 tmpvar_6;
tmpvar_6 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_7;
tmpvar_7 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_5 = tmpvar_7;
light_5 = (-(log2(max(light_5, float4(0.001, 0.001, 0.001, 0.001)))));
half4 tmpvar_8;
tmpvar_8 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_4 = tmpvar_8;
half3 tmpvar_9;
tmpvar_9 = (((unity_Lightmap_HDR).x * (lmtex_4).xyz));
lm_3 = tmpvar_9;
(light_5).xyz = (((light_5).xyz + lm_3));
fixed4 c_10;
fixed spec_11;
half tmpvar_12;
tmpvar_12 = (((light_5).w * (tmpvar_6).w));
spec_11 = tmpvar_12;
(c_10).xyz = (((((((tmpvar_6).xyz * (_Color).xyz)) * (light_5).xyz)) + (((((light_5).xyz * (_SpecColor).xyz)) * spec_11))));
(c_10).w = (((tmpvar_6).w * (_Color).w));
(c_2).xyz = (c_10).xyz;
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (tmpvar_1).xyz;
tmpvar_4 = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_6;
float4 tmpvar_7;
tmpvar_7 = ((tmpvar_4 * 0.5));
float2 tmpvar_8;
(tmpvar_8).x = (tmpvar_7).x;
(tmpvar_8).y = (((tmpvar_7).y * (_ProjectionParams).x));
(o_6).xy = ((tmpvar_8 + (tmpvar_7).w));
(o_6).zw = (tmpvar_4).zw;
(tmpvar_2).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_9;
tmpvar_9 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_9).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_10;
(tmpvar_10).w = 1.0;
(tmpvar_10).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_10, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = tmpvar_4;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = (tmpvar_9).xyz;
o.xlv_TEXCOORD2 = o_6;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed3 lm_3;
fixed4 lmtex_4;
half4 light_5;
fixed4 tmpvar_6;
tmpvar_6 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_7;
tmpvar_7 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_5 = tmpvar_7;
light_5 = (-(log2(max(light_5, float4(0.001, 0.001, 0.001, 0.001)))));
half4 tmpvar_8;
tmpvar_8 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_4 = tmpvar_8;
half3 tmpvar_9;
tmpvar_9 = (((unity_Lightmap_HDR).x * (lmtex_4).xyz));
lm_3 = tmpvar_9;
(light_5).xyz = (((light_5).xyz + lm_3));
fixed4 tmpvar_10;
tmpvar_10 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
(light_5).xyz = (((light_5).xyz + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_10).w)) * (tmpvar_10).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_11;
fixed spec_12;
half tmpvar_13;
tmpvar_13 = (((light_5).w * (tmpvar_6).w));
spec_12 = tmpvar_13;
(c_11).xyz = (((((((tmpvar_6).xyz * (_Color).xyz)) * (light_5).xyz)) + (((((light_5).xyz * (_SpecColor).xyz)) * spec_12))));
(c_11).w = (((tmpvar_6).w * (_Color).w));
(c_2).xyz = (c_11).xyz;
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD6 : TEXCOORD4;
float3 xlv_TEXCOORD7 : TEXCOORD5;
float3 xlv_TEXCOORD8 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
float4 o_17;
float4 tmpvar_18;
tmpvar_18 = ((tmpvar_5 * 0.5));
float2 tmpvar_19;
(tmpvar_19).x = (tmpvar_18).x;
(tmpvar_19).y = (((tmpvar_18).y * (_ProjectionParams).x));
(o_17).xy = ((tmpvar_19 + (tmpvar_18).w));
(o_17).zw = (tmpvar_5).zw;
(tmpvar_4).zw = float2(0.0, 0.0);
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_7;
o.xlv_TEXCOORD2 = o_17;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed4 lmtex_3;
half4 light_4;
fixed4 tmpvar_5;
tmpvar_5 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_6;
tmpvar_6 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_4 = tmpvar_6;
light_4 = (-(log2(max(light_4, float4(0.001, 0.001, 0.001, 0.001)))));
half4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_3 = tmpvar_7;
half4 tmpvar_8;
(tmpvar_8).w = 0.0;
(tmpvar_8).xyz = (((unity_Lightmap_HDR).x * (lmtex_3).xyz));
light_4 = ((light_4 + tmpvar_8));
fixed4 c_9;
fixed spec_10;
half tmpvar_11;
tmpvar_11 = (((light_4).w * (tmpvar_5).w));
spec_10 = tmpvar_11;
(c_9).xyz = (((((((tmpvar_5).xyz * (_Color).xyz)) * (light_4).xyz)) + (((((light_4).xyz * (_SpecColor).xyz)) * spec_10))));
(c_9).w = (((tmpvar_5).w * (_Color).w));
(c_2).xyz = (c_9).xyz;
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD6 : TEXCOORD4;
float3 xlv_TEXCOORD7 : TEXCOORD5;
float3 xlv_TEXCOORD8 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
float4 o_17;
float4 tmpvar_18;
tmpvar_18 = ((tmpvar_5 * 0.5));
float2 tmpvar_19;
(tmpvar_19).x = (tmpvar_18).x;
(tmpvar_19).y = (((tmpvar_18).y * (_ProjectionParams).x));
(o_17).xy = ((tmpvar_19 + (tmpvar_18).w));
(o_17).zw = (tmpvar_5).zw;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_7;
o.xlv_TEXCOORD2 = o_17;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed4 lmtex_3;
half4 light_4;
fixed4 tmpvar_5;
tmpvar_5 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_6;
tmpvar_6 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_4 = tmpvar_6;
light_4 = (-(log2(max(light_4, float4(0.001, 0.001, 0.001, 0.001)))));
half4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_3 = tmpvar_7;
half4 tmpvar_8;
(tmpvar_8).w = 0.0;
(tmpvar_8).xyz = (((unity_Lightmap_HDR).x * (lmtex_3).xyz));
light_4 = ((light_4 + tmpvar_8));
fixed4 tmpvar_9;
tmpvar_9 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
(light_4).xyz = (((light_4).xyz + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_9).w)) * (tmpvar_9).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_10;
fixed spec_11;
half tmpvar_12;
tmpvar_12 = (((light_4).w * (tmpvar_5).w));
spec_11 = tmpvar_12;
(c_10).xyz = (((((((tmpvar_5).xyz * (_Color).xyz)) * (light_4).xyz)) + (((((light_4).xyz * (_SpecColor).xyz)) * spec_11))));
(c_10).w = (((tmpvar_5).w * (_Color).w));
(c_2).xyz = (c_10).xyz;
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(SHADOWS_SHADOWMASK) && defined(UNITY_HDR_ON)) || (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float3 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_5;
float4 tmpvar_6;
tmpvar_6 = ((tmpvar_3 * 0.5));
float2 tmpvar_7;
(tmpvar_7).x = (tmpvar_6).x;
(tmpvar_7).y = (((tmpvar_6).y * (_ProjectionParams).x));
(o_5).xy = ((tmpvar_7 + (tmpvar_6).w));
(o_5).zw = (tmpvar_3).zw;
(tmpvar_1).zw = float2(0.0, 0.0);
(tmpvar_1).xy = float2(0.0, 0.0);
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float4 tmpvar_9;
(tmpvar_9).w = 1.0;
(tmpvar_9).xyz = normalize((mul(tmpvar_8, v._glesNormal)));
half4 normal_10;
normal_10 = tmpvar_9;
half3 res_11;
half3 x_12;
(x_12).x = dot(unity_SHAr, normal_10);
(x_12).y = dot(unity_SHAg, normal_10);
(x_12).z = dot(unity_SHAb, normal_10);
half3 x1_13;
half4 tmpvar_14;
tmpvar_14 = (((normal_10).xyzz * (normal_10).yzzx));
(x1_13).x = dot(unity_SHBr, tmpvar_14);
(x1_13).y = dot(unity_SHBg, tmpvar_14);
(x1_13).z = dot(unity_SHBb, tmpvar_14);
res_11 = ((x_12 + ((x1_13 + (((unity_SHC).xyz * (((((normal_10).x * (normal_10).x)) - (((normal_10).y * (normal_10).y))))))))));
half3 tmpvar_15;
tmpvar_15 = max(((((1.055 * pow(max(res_11, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_11 = tmpvar_15;
tmpvar_2 = tmpvar_15;
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD2 = o_5;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
half4 light_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_5;
tmpvar_5 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_3 = tmpvar_5;
half4 tmpvar_6;
tmpvar_6 = max(light_3, float4(0.001, 0.001, 0.001, 0.001));
(light_3).w = (tmpvar_6).w;
(light_3).xyz = (((tmpvar_6).xyz + i.xlv_TEXCOORD4));
fixed4 c_7;
fixed spec_8;
half tmpvar_9;
tmpvar_9 = (((tmpvar_6).w * (tmpvar_4).w));
spec_8 = tmpvar_9;
(c_7).xyz = (((((((tmpvar_4).xyz * (_Color).xyz)) * (light_3).xyz)) + (((((light_3).xyz * (_SpecColor).xyz)) * spec_8))));
(c_7).w = (((tmpvar_4).w * (_Color).w));
(c_2).xyz = (c_7).xyz;
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SHADOWMASK) && defined(UNITY_HDR_ON)) || (defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float3 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_5;
float4 tmpvar_6;
tmpvar_6 = ((tmpvar_3 * 0.5));
float2 tmpvar_7;
(tmpvar_7).x = (tmpvar_6).x;
(tmpvar_7).y = (((tmpvar_6).y * (_ProjectionParams).x));
(o_5).xy = ((tmpvar_7 + (tmpvar_6).w));
(o_5).zw = (tmpvar_3).zw;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = float2(0.0, 0.0);
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float4 tmpvar_9;
(tmpvar_9).w = 1.0;
(tmpvar_9).xyz = normalize((mul(tmpvar_8, v._glesNormal)));
half4 normal_10;
normal_10 = tmpvar_9;
half3 res_11;
half3 x_12;
(x_12).x = dot(unity_SHAr, normal_10);
(x_12).y = dot(unity_SHAg, normal_10);
(x_12).z = dot(unity_SHAb, normal_10);
half3 x1_13;
half4 tmpvar_14;
tmpvar_14 = (((normal_10).xyzz * (normal_10).yzzx));
(x1_13).x = dot(unity_SHBr, tmpvar_14);
(x1_13).y = dot(unity_SHBg, tmpvar_14);
(x1_13).z = dot(unity_SHBb, tmpvar_14);
res_11 = ((x_12 + ((x1_13 + (((unity_SHC).xyz * (((((normal_10).x * (normal_10).x)) - (((normal_10).y * (normal_10).y))))))))));
half3 tmpvar_15;
tmpvar_15 = max(((((1.055 * pow(max(res_11, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_11 = tmpvar_15;
tmpvar_2 = tmpvar_15;
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD2 = o_5;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = tmpvar_2;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
half4 light_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_5;
tmpvar_5 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_3 = tmpvar_5;
half4 tmpvar_6;
tmpvar_6 = max(light_3, float4(0.001, 0.001, 0.001, 0.001));
(light_3).w = (tmpvar_6).w;
(light_3).xyz = (((tmpvar_6).xyz + i.xlv_TEXCOORD4));
fixed4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
(light_3).xyz = (((light_3).xyz + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_7).w)) * (tmpvar_7).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_8;
fixed spec_9;
half tmpvar_10;
tmpvar_10 = (((tmpvar_6).w * (tmpvar_4).w));
spec_9 = tmpvar_10;
(c_8).xyz = (((((((tmpvar_4).xyz * (_Color).xyz)) * (light_3).xyz)) + (((((light_3).xyz * (_SpecColor).xyz)) * spec_9))));
(c_8).w = (((tmpvar_4).w * (_Color).w));
(c_2).xyz = (c_8).xyz;
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (tmpvar_1).xyz;
tmpvar_4 = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_6;
float4 tmpvar_7;
tmpvar_7 = ((tmpvar_4 * 0.5));
float2 tmpvar_8;
(tmpvar_8).x = (tmpvar_7).x;
(tmpvar_8).y = (((tmpvar_7).y * (_ProjectionParams).x));
(o_6).xy = ((tmpvar_8 + (tmpvar_7).w));
(o_6).zw = (tmpvar_4).zw;
(tmpvar_2).zw = float2(0.0, 0.0);
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_9;
tmpvar_9 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_9).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_10;
(tmpvar_10).w = 1.0;
(tmpvar_10).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_10, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = tmpvar_4;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = (tmpvar_9).xyz;
o.xlv_TEXCOORD2 = o_6;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed3 lm_3;
fixed4 lmtex_4;
half4 light_5;
fixed4 tmpvar_6;
tmpvar_6 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_7;
tmpvar_7 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_5 = tmpvar_7;
half4 tmpvar_8;
tmpvar_8 = max(light_5, float4(0.001, 0.001, 0.001, 0.001));
(light_5).w = (tmpvar_8).w;
half4 tmpvar_9;
tmpvar_9 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_4 = tmpvar_9;
half3 tmpvar_10;
tmpvar_10 = (((unity_Lightmap_HDR).x * (lmtex_4).xyz));
lm_3 = tmpvar_10;
(light_5).xyz = (((tmpvar_8).xyz + lm_3));
fixed4 c_11;
fixed spec_12;
half tmpvar_13;
tmpvar_13 = (((tmpvar_8).w * (tmpvar_6).w));
spec_12 = tmpvar_13;
(c_11).xyz = (((((((tmpvar_6).xyz * (_Color).xyz)) * (light_5).xyz)) + (((((light_5).xyz * (_SpecColor).xyz)) * spec_12))));
(c_11).w = (((tmpvar_6).w * (_Color).w));
(c_2).xyz = (c_11).xyz;
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (tmpvar_1).xyz;
tmpvar_4 = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_6;
float4 tmpvar_7;
tmpvar_7 = ((tmpvar_4 * 0.5));
float2 tmpvar_8;
(tmpvar_8).x = (tmpvar_7).x;
(tmpvar_8).y = (((tmpvar_7).y * (_ProjectionParams).x));
(o_6).xy = ((tmpvar_8 + (tmpvar_7).w));
(o_6).zw = (tmpvar_4).zw;
(tmpvar_2).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_9;
tmpvar_9 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_9).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_10;
(tmpvar_10).w = 1.0;
(tmpvar_10).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_10, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = tmpvar_4;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = (tmpvar_9).xyz;
o.xlv_TEXCOORD2 = o_6;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed3 lm_3;
fixed4 lmtex_4;
half4 light_5;
fixed4 tmpvar_6;
tmpvar_6 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_7;
tmpvar_7 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_5 = tmpvar_7;
half4 tmpvar_8;
tmpvar_8 = max(light_5, float4(0.001, 0.001, 0.001, 0.001));
(light_5).w = (tmpvar_8).w;
half4 tmpvar_9;
tmpvar_9 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_4 = tmpvar_9;
half3 tmpvar_10;
tmpvar_10 = (((unity_Lightmap_HDR).x * (lmtex_4).xyz));
lm_3 = tmpvar_10;
(light_5).xyz = (((tmpvar_8).xyz + lm_3));
fixed4 tmpvar_11;
tmpvar_11 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
(light_5).xyz = (((light_5).xyz + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_11).w)) * (tmpvar_11).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_12;
fixed spec_13;
half tmpvar_14;
tmpvar_14 = (((tmpvar_8).w * (tmpvar_6).w));
spec_13 = tmpvar_14;
(c_12).xyz = (((((((tmpvar_6).xyz * (_Color).xyz)) * (light_5).xyz)) + (((((light_5).xyz * (_SpecColor).xyz)) * spec_13))));
(c_12).w = (((tmpvar_6).w * (_Color).w));
(c_2).xyz = (c_12).xyz;
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD6 : TEXCOORD4;
float3 xlv_TEXCOORD7 : TEXCOORD5;
float3 xlv_TEXCOORD8 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
float4 o_17;
float4 tmpvar_18;
tmpvar_18 = ((tmpvar_5 * 0.5));
float2 tmpvar_19;
(tmpvar_19).x = (tmpvar_18).x;
(tmpvar_19).y = (((tmpvar_18).y * (_ProjectionParams).x));
(o_17).xy = ((tmpvar_19 + (tmpvar_18).w));
(o_17).zw = (tmpvar_5).zw;
(tmpvar_4).zw = float2(0.0, 0.0);
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_7;
o.xlv_TEXCOORD2 = o_17;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed4 lmtex_3;
half4 light_4;
fixed4 tmpvar_5;
tmpvar_5 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_6;
tmpvar_6 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_4 = tmpvar_6;
half4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_3 = tmpvar_7;
half4 tmpvar_8;
(tmpvar_8).w = 0.0;
(tmpvar_8).xyz = (((unity_Lightmap_HDR).x * (lmtex_3).xyz));
light_4 = ((max(light_4, float4(0.001, 0.001, 0.001, 0.001)) + tmpvar_8));
fixed4 c_9;
fixed spec_10;
half tmpvar_11;
tmpvar_11 = (((light_4).w * (tmpvar_5).w));
spec_10 = tmpvar_11;
(c_9).xyz = (((((((tmpvar_5).xyz * (_Color).xyz)) * (light_4).xyz)) + (((((light_4).xyz * (_SpecColor).xyz)) * spec_10))));
(c_9).w = (((tmpvar_5).w * (_Color).w));
(c_2).xyz = (c_9).xyz;
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD6 : TEXCOORD4;
float3 xlv_TEXCOORD7 : TEXCOORD5;
float3 xlv_TEXCOORD8 : TEXCOORD6;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
float4 o_17;
float4 tmpvar_18;
tmpvar_18 = ((tmpvar_5 * 0.5));
float2 tmpvar_19;
(tmpvar_19).x = (tmpvar_18).x;
(tmpvar_19).y = (((tmpvar_18).y * (_ProjectionParams).x));
(o_17).xy = ((tmpvar_19 + (tmpvar_18).w));
(o_17).zw = (tmpvar_5).zw;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_7;
o.xlv_TEXCOORD2 = o_17;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed4 lmtex_3;
half4 light_4;
fixed4 tmpvar_5;
tmpvar_5 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_6;
tmpvar_6 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_4 = tmpvar_6;
half4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_3 = tmpvar_7;
half4 tmpvar_8;
(tmpvar_8).w = 0.0;
(tmpvar_8).xyz = (((unity_Lightmap_HDR).x * (lmtex_3).xyz));
light_4 = ((max(light_4, float4(0.001, 0.001, 0.001, 0.001)) + tmpvar_8));
fixed4 tmpvar_9;
tmpvar_9 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
(light_4).xyz = (((light_4).xyz + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_9).w)) * (tmpvar_9).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_10;
fixed spec_11;
half tmpvar_12;
tmpvar_12 = (((light_4).w * (tmpvar_5).w));
spec_11 = tmpvar_12;
(c_10).xyz = (((((((tmpvar_5).xyz * (_Color).xyz)) * (light_4).xyz)) + (((((light_4).xyz * (_SpecColor).xyz)) * spec_11))));
(c_10).w = (((tmpvar_5).w * (_Color).w));
(c_2).xyz = (c_10).xyz;
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(SHADOWS_SHADOWMASK) && !defined(UNITY_HDR_ON)) || (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float3 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_5;
float4 tmpvar_6;
tmpvar_6 = ((tmpvar_3 * 0.5));
float2 tmpvar_7;
(tmpvar_7).x = (tmpvar_6).x;
(tmpvar_7).y = (((tmpvar_6).y * (_ProjectionParams).x));
(o_5).xy = ((tmpvar_7 + (tmpvar_6).w));
(o_5).zw = (tmpvar_3).zw;
(tmpvar_1).zw = float2(0.0, 0.0);
(tmpvar_1).xy = float2(0.0, 0.0);
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float4 tmpvar_9;
(tmpvar_9).w = 1.0;
(tmpvar_9).xyz = normalize((mul(tmpvar_8, v._glesNormal)));
half4 normal_10;
normal_10 = tmpvar_9;
half3 res_11;
half3 x_12;
(x_12).x = dot(unity_SHAr, normal_10);
(x_12).y = dot(unity_SHAg, normal_10);
(x_12).z = dot(unity_SHAb, normal_10);
half3 x1_13;
half4 tmpvar_14;
tmpvar_14 = (((normal_10).xyzz * (normal_10).yzzx));
(x1_13).x = dot(unity_SHBr, tmpvar_14);
(x1_13).y = dot(unity_SHBg, tmpvar_14);
(x1_13).z = dot(unity_SHBb, tmpvar_14);
res_11 = ((x_12 + ((x1_13 + (((unity_SHC).xyz * (((((normal_10).x * (normal_10).x)) - (((normal_10).y * (normal_10).y))))))))));
half3 tmpvar_15;
tmpvar_15 = max(((((1.055 * pow(max(res_11, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_11 = tmpvar_15;
tmpvar_2 = tmpvar_15;
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD2 = o_5;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = tmpvar_2;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_3).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
half4 light_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_5;
tmpvar_5 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_3 = tmpvar_5;
light_3 = (-(log2(max(light_3, float4(0.001, 0.001, 0.001, 0.001)))));
(light_3).xyz = (((light_3).xyz + i.xlv_TEXCOORD4));
fixed4 c_6;
fixed spec_7;
half tmpvar_8;
tmpvar_8 = (((light_3).w * (tmpvar_4).w));
spec_7 = tmpvar_8;
(c_6).xyz = (((((((tmpvar_4).xyz * (_Color).xyz)) * (light_3).xyz)) + (((((light_3).xyz * (_SpecColor).xyz)) * spec_7))));
(c_6).w = (((tmpvar_4).w * (_Color).w));
c_2 = c_6;
float tmpvar_9;
tmpvar_9 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_2).xyz, ((float3)(tmpvar_9)));
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float3 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_5;
float4 tmpvar_6;
tmpvar_6 = ((tmpvar_3 * 0.5));
float2 tmpvar_7;
(tmpvar_7).x = (tmpvar_6).x;
(tmpvar_7).y = (((tmpvar_6).y * (_ProjectionParams).x));
(o_5).xy = ((tmpvar_7 + (tmpvar_6).w));
(o_5).zw = (tmpvar_3).zw;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = float2(0.0, 0.0);
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float4 tmpvar_9;
(tmpvar_9).w = 1.0;
(tmpvar_9).xyz = normalize((mul(tmpvar_8, v._glesNormal)));
half4 normal_10;
normal_10 = tmpvar_9;
half3 res_11;
half3 x_12;
(x_12).x = dot(unity_SHAr, normal_10);
(x_12).y = dot(unity_SHAg, normal_10);
(x_12).z = dot(unity_SHAb, normal_10);
half3 x1_13;
half4 tmpvar_14;
tmpvar_14 = (((normal_10).xyzz * (normal_10).yzzx));
(x1_13).x = dot(unity_SHBr, tmpvar_14);
(x1_13).y = dot(unity_SHBg, tmpvar_14);
(x1_13).z = dot(unity_SHBb, tmpvar_14);
res_11 = ((x_12 + ((x1_13 + (((unity_SHC).xyz * (((((normal_10).x * (normal_10).x)) - (((normal_10).y * (normal_10).y))))))))));
half3 tmpvar_15;
tmpvar_15 = max(((((1.055 * pow(max(res_11, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_11 = tmpvar_15;
tmpvar_2 = tmpvar_15;
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD2 = o_5;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = tmpvar_2;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_3).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
half4 light_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_5;
tmpvar_5 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_3 = tmpvar_5;
light_3 = (-(log2(max(light_3, float4(0.001, 0.001, 0.001, 0.001)))));
(light_3).xyz = (((light_3).xyz + i.xlv_TEXCOORD4));
fixed4 tmpvar_6;
tmpvar_6 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
(light_3).xyz = (((light_3).xyz + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_6).w)) * (tmpvar_6).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_7;
fixed spec_8;
half tmpvar_9;
tmpvar_9 = (((light_3).w * (tmpvar_4).w));
spec_8 = tmpvar_9;
(c_7).xyz = (((((((tmpvar_4).xyz * (_Color).xyz)) * (light_3).xyz)) + (((((light_3).xyz * (_SpecColor).xyz)) * spec_8))));
(c_7).w = (((tmpvar_4).w * (_Color).w));
c_2 = c_7;
float tmpvar_10;
tmpvar_10 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_2).xyz, ((float3)(tmpvar_10)));
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (tmpvar_1).xyz;
tmpvar_4 = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_6;
float4 tmpvar_7;
tmpvar_7 = ((tmpvar_4 * 0.5));
float2 tmpvar_8;
(tmpvar_8).x = (tmpvar_7).x;
(tmpvar_8).y = (((tmpvar_7).y * (_ProjectionParams).x));
(o_6).xy = ((tmpvar_8 + (tmpvar_7).w));
(o_6).zw = (tmpvar_4).zw;
(tmpvar_2).zw = float2(0.0, 0.0);
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_9;
tmpvar_9 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_9).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_10;
(tmpvar_10).w = 1.0;
(tmpvar_10).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_10, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = tmpvar_4;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = (tmpvar_9).xyz;
o.xlv_TEXCOORD2 = o_6;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_4).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed3 lm_3;
fixed4 lmtex_4;
half4 light_5;
fixed4 tmpvar_6;
tmpvar_6 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_7;
tmpvar_7 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_5 = tmpvar_7;
light_5 = (-(log2(max(light_5, float4(0.001, 0.001, 0.001, 0.001)))));
half4 tmpvar_8;
tmpvar_8 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_4 = tmpvar_8;
half3 tmpvar_9;
tmpvar_9 = (((unity_Lightmap_HDR).x * (lmtex_4).xyz));
lm_3 = tmpvar_9;
(light_5).xyz = (((light_5).xyz + lm_3));
fixed4 c_10;
fixed spec_11;
half tmpvar_12;
tmpvar_12 = (((light_5).w * (tmpvar_6).w));
spec_11 = tmpvar_12;
(c_10).xyz = (((((((tmpvar_6).xyz * (_Color).xyz)) * (light_5).xyz)) + (((((light_5).xyz * (_SpecColor).xyz)) * spec_11))));
(c_10).w = (((tmpvar_6).w * (_Color).w));
c_2 = c_10;
float tmpvar_13;
tmpvar_13 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_2).xyz, ((float3)(tmpvar_13)));
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (tmpvar_1).xyz;
tmpvar_4 = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_6;
float4 tmpvar_7;
tmpvar_7 = ((tmpvar_4 * 0.5));
float2 tmpvar_8;
(tmpvar_8).x = (tmpvar_7).x;
(tmpvar_8).y = (((tmpvar_7).y * (_ProjectionParams).x));
(o_6).xy = ((tmpvar_8 + (tmpvar_7).w));
(o_6).zw = (tmpvar_4).zw;
(tmpvar_2).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_9;
tmpvar_9 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_9).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_10;
(tmpvar_10).w = 1.0;
(tmpvar_10).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_10, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = tmpvar_4;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = (tmpvar_9).xyz;
o.xlv_TEXCOORD2 = o_6;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_4).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed3 lm_3;
fixed4 lmtex_4;
half4 light_5;
fixed4 tmpvar_6;
tmpvar_6 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_7;
tmpvar_7 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_5 = tmpvar_7;
light_5 = (-(log2(max(light_5, float4(0.001, 0.001, 0.001, 0.001)))));
half4 tmpvar_8;
tmpvar_8 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_4 = tmpvar_8;
half3 tmpvar_9;
tmpvar_9 = (((unity_Lightmap_HDR).x * (lmtex_4).xyz));
lm_3 = tmpvar_9;
(light_5).xyz = (((light_5).xyz + lm_3));
fixed4 tmpvar_10;
tmpvar_10 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
(light_5).xyz = (((light_5).xyz + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_10).w)) * (tmpvar_10).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_11;
fixed spec_12;
half tmpvar_13;
tmpvar_13 = (((light_5).w * (tmpvar_6).w));
spec_12 = tmpvar_13;
(c_11).xyz = (((((((tmpvar_6).xyz * (_Color).xyz)) * (light_5).xyz)) + (((((light_5).xyz * (_SpecColor).xyz)) * spec_12))));
(c_11).w = (((tmpvar_6).w * (_Color).w));
c_2 = c_11;
float tmpvar_14;
tmpvar_14 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_2).xyz, ((float3)(tmpvar_14)));
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
float4 o_17;
float4 tmpvar_18;
tmpvar_18 = ((tmpvar_5 * 0.5));
float2 tmpvar_19;
(tmpvar_19).x = (tmpvar_18).x;
(tmpvar_19).y = (((tmpvar_18).y * (_ProjectionParams).x));
(o_17).xy = ((tmpvar_19 + (tmpvar_18).w));
(o_17).zw = (tmpvar_5).zw;
(tmpvar_4).zw = float2(0.0, 0.0);
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_7;
o.xlv_TEXCOORD2 = o_17;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed4 lmtex_3;
half4 light_4;
fixed4 tmpvar_5;
tmpvar_5 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_6;
tmpvar_6 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_4 = tmpvar_6;
light_4 = (-(log2(max(light_4, float4(0.001, 0.001, 0.001, 0.001)))));
half4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_3 = tmpvar_7;
half4 tmpvar_8;
(tmpvar_8).w = 0.0;
(tmpvar_8).xyz = (((unity_Lightmap_HDR).x * (lmtex_3).xyz));
light_4 = ((light_4 + tmpvar_8));
fixed4 c_9;
fixed spec_10;
half tmpvar_11;
tmpvar_11 = (((light_4).w * (tmpvar_5).w));
spec_10 = tmpvar_11;
(c_9).xyz = (((((((tmpvar_5).xyz * (_Color).xyz)) * (light_4).xyz)) + (((((light_4).xyz * (_SpecColor).xyz)) * spec_10))));
(c_9).w = (((tmpvar_5).w * (_Color).w));
c_2 = c_9;
float tmpvar_12;
tmpvar_12 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_2).xyz, ((float3)(tmpvar_12)));
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
float4 o_17;
float4 tmpvar_18;
tmpvar_18 = ((tmpvar_5 * 0.5));
float2 tmpvar_19;
(tmpvar_19).x = (tmpvar_18).x;
(tmpvar_19).y = (((tmpvar_18).y * (_ProjectionParams).x));
(o_17).xy = ((tmpvar_19 + (tmpvar_18).w));
(o_17).zw = (tmpvar_5).zw;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_7;
o.xlv_TEXCOORD2 = o_17;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed4 lmtex_3;
half4 light_4;
fixed4 tmpvar_5;
tmpvar_5 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_6;
tmpvar_6 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_4 = tmpvar_6;
light_4 = (-(log2(max(light_4, float4(0.001, 0.001, 0.001, 0.001)))));
half4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_3 = tmpvar_7;
half4 tmpvar_8;
(tmpvar_8).w = 0.0;
(tmpvar_8).xyz = (((unity_Lightmap_HDR).x * (lmtex_3).xyz));
light_4 = ((light_4 + tmpvar_8));
fixed4 tmpvar_9;
tmpvar_9 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
(light_4).xyz = (((light_4).xyz + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_9).w)) * (tmpvar_9).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_10;
fixed spec_11;
half tmpvar_12;
tmpvar_12 = (((light_4).w * (tmpvar_5).w));
spec_11 = tmpvar_12;
(c_10).xyz = (((((((tmpvar_5).xyz * (_Color).xyz)) * (light_4).xyz)) + (((((light_4).xyz * (_SpecColor).xyz)) * spec_11))));
(c_10).w = (((tmpvar_5).w * (_Color).w));
c_2 = c_10;
float tmpvar_13;
tmpvar_13 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_2).xyz, ((float3)(tmpvar_13)));
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(SHADOWS_SHADOWMASK) && defined(UNITY_HDR_ON)) || (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float3 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_5;
float4 tmpvar_6;
tmpvar_6 = ((tmpvar_3 * 0.5));
float2 tmpvar_7;
(tmpvar_7).x = (tmpvar_6).x;
(tmpvar_7).y = (((tmpvar_6).y * (_ProjectionParams).x));
(o_5).xy = ((tmpvar_7 + (tmpvar_6).w));
(o_5).zw = (tmpvar_3).zw;
(tmpvar_1).zw = float2(0.0, 0.0);
(tmpvar_1).xy = float2(0.0, 0.0);
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float4 tmpvar_9;
(tmpvar_9).w = 1.0;
(tmpvar_9).xyz = normalize((mul(tmpvar_8, v._glesNormal)));
half4 normal_10;
normal_10 = tmpvar_9;
half3 res_11;
half3 x_12;
(x_12).x = dot(unity_SHAr, normal_10);
(x_12).y = dot(unity_SHAg, normal_10);
(x_12).z = dot(unity_SHAb, normal_10);
half3 x1_13;
half4 tmpvar_14;
tmpvar_14 = (((normal_10).xyzz * (normal_10).yzzx));
(x1_13).x = dot(unity_SHBr, tmpvar_14);
(x1_13).y = dot(unity_SHBg, tmpvar_14);
(x1_13).z = dot(unity_SHBb, tmpvar_14);
res_11 = ((x_12 + ((x1_13 + (((unity_SHC).xyz * (((((normal_10).x * (normal_10).x)) - (((normal_10).y * (normal_10).y))))))))));
half3 tmpvar_15;
tmpvar_15 = max(((((1.055 * pow(max(res_11, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_11 = tmpvar_15;
tmpvar_2 = tmpvar_15;
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD2 = o_5;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = tmpvar_2;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_3).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
half4 light_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_5;
tmpvar_5 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_3 = tmpvar_5;
half4 tmpvar_6;
tmpvar_6 = max(light_3, float4(0.001, 0.001, 0.001, 0.001));
(light_3).w = (tmpvar_6).w;
(light_3).xyz = (((tmpvar_6).xyz + i.xlv_TEXCOORD4));
fixed4 c_7;
fixed spec_8;
half tmpvar_9;
tmpvar_9 = (((tmpvar_6).w * (tmpvar_4).w));
spec_8 = tmpvar_9;
(c_7).xyz = (((((((tmpvar_4).xyz * (_Color).xyz)) * (light_3).xyz)) + (((((light_3).xyz * (_SpecColor).xyz)) * spec_8))));
(c_7).w = (((tmpvar_4).w * (_Color).w));
c_2 = c_7;
float tmpvar_10;
tmpvar_10 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_2).xyz, ((float3)(tmpvar_10)));
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SHADOWMASK) && defined(UNITY_HDR_ON)) || (defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && !defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float3 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float3 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (v._glesVertex).xyz;
tmpvar_3 = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_5;
float4 tmpvar_6;
tmpvar_6 = ((tmpvar_3 * 0.5));
float2 tmpvar_7;
(tmpvar_7).x = (tmpvar_6).x;
(tmpvar_7).y = (((tmpvar_6).y * (_ProjectionParams).x));
(o_5).xy = ((tmpvar_7 + (tmpvar_6).w));
(o_5).zw = (tmpvar_3).zw;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = float2(0.0, 0.0);
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float4 tmpvar_9;
(tmpvar_9).w = 1.0;
(tmpvar_9).xyz = normalize((mul(tmpvar_8, v._glesNormal)));
half4 normal_10;
normal_10 = tmpvar_9;
half3 res_11;
half3 x_12;
(x_12).x = dot(unity_SHAr, normal_10);
(x_12).y = dot(unity_SHAg, normal_10);
(x_12).z = dot(unity_SHAb, normal_10);
half3 x1_13;
half4 tmpvar_14;
tmpvar_14 = (((normal_10).xyzz * (normal_10).yzzx));
(x1_13).x = dot(unity_SHBr, tmpvar_14);
(x1_13).y = dot(unity_SHBg, tmpvar_14);
(x1_13).z = dot(unity_SHBb, tmpvar_14);
res_11 = ((x_12 + ((x1_13 + (((unity_SHC).xyz * (((((normal_10).x * (normal_10).x)) - (((normal_10).y * (normal_10).y))))))))));
half3 tmpvar_15;
tmpvar_15 = max(((((1.055 * pow(max(res_11, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_11 = tmpvar_15;
tmpvar_2 = tmpvar_15;
o.position = tmpvar_3;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD2 = o_5;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = tmpvar_2;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_3).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
half4 light_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_5;
tmpvar_5 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_3 = tmpvar_5;
half4 tmpvar_6;
tmpvar_6 = max(light_3, float4(0.001, 0.001, 0.001, 0.001));
(light_3).w = (tmpvar_6).w;
(light_3).xyz = (((tmpvar_6).xyz + i.xlv_TEXCOORD4));
fixed4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
(light_3).xyz = (((light_3).xyz + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_7).w)) * (tmpvar_7).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_8;
fixed spec_9;
half tmpvar_10;
tmpvar_10 = (((tmpvar_6).w * (tmpvar_4).w));
spec_9 = tmpvar_10;
(c_8).xyz = (((((((tmpvar_4).xyz * (_Color).xyz)) * (light_3).xyz)) + (((((light_3).xyz * (_SpecColor).xyz)) * spec_9))));
(c_8).w = (((tmpvar_4).w * (_Color).w));
c_2 = c_8;
float tmpvar_11;
tmpvar_11 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_2).xyz, ((float3)(tmpvar_11)));
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (tmpvar_1).xyz;
tmpvar_4 = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_6;
float4 tmpvar_7;
tmpvar_7 = ((tmpvar_4 * 0.5));
float2 tmpvar_8;
(tmpvar_8).x = (tmpvar_7).x;
(tmpvar_8).y = (((tmpvar_7).y * (_ProjectionParams).x));
(o_6).xy = ((tmpvar_8 + (tmpvar_7).w));
(o_6).zw = (tmpvar_4).zw;
(tmpvar_2).zw = float2(0.0, 0.0);
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_9;
tmpvar_9 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_9).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_10;
(tmpvar_10).w = 1.0;
(tmpvar_10).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_10, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = tmpvar_4;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = (tmpvar_9).xyz;
o.xlv_TEXCOORD2 = o_6;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_4).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed3 lm_3;
fixed4 lmtex_4;
half4 light_5;
fixed4 tmpvar_6;
tmpvar_6 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_7;
tmpvar_7 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_5 = tmpvar_7;
half4 tmpvar_8;
tmpvar_8 = max(light_5, float4(0.001, 0.001, 0.001, 0.001));
(light_5).w = (tmpvar_8).w;
half4 tmpvar_9;
tmpvar_9 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_4 = tmpvar_9;
half3 tmpvar_10;
tmpvar_10 = (((unity_Lightmap_HDR).x * (lmtex_4).xyz));
lm_3 = tmpvar_10;
(light_5).xyz = (((tmpvar_8).xyz + lm_3));
fixed4 c_11;
fixed spec_12;
half tmpvar_13;
tmpvar_13 = (((tmpvar_8).w * (tmpvar_6).w));
spec_12 = tmpvar_13;
(c_11).xyz = (((((((tmpvar_6).xyz * (_Color).xyz)) * (light_5).xyz)) + (((((light_5).xyz * (_SpecColor).xyz)) * spec_12))));
(c_11).w = (((tmpvar_6).w * (_Color).w));
c_2 = c_11;
float tmpvar_14;
tmpvar_14 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_2).xyz, ((float3)(tmpvar_14)));
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
float xlv_TEXCOORD5 : TEXCOORD5;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
float4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = (tmpvar_1).xyz;
tmpvar_4 = (mul((mul(tmpvar_5, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float4 o_6;
float4 tmpvar_7;
tmpvar_7 = ((tmpvar_4 * 0.5));
float2 tmpvar_8;
(tmpvar_8).x = (tmpvar_7).x;
(tmpvar_8).y = (((tmpvar_7).y * (_ProjectionParams).x));
(o_6).xy = ((tmpvar_8 + (tmpvar_7).w));
(o_6).zw = (tmpvar_4).zw;
(tmpvar_2).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_9;
tmpvar_9 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_9).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_10;
(tmpvar_10).w = 1.0;
(tmpvar_10).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_10, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = tmpvar_4;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = (tmpvar_9).xyz;
o.xlv_TEXCOORD2 = o_6;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_4).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed3 lm_3;
fixed4 lmtex_4;
half4 light_5;
fixed4 tmpvar_6;
tmpvar_6 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_7;
tmpvar_7 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_5 = tmpvar_7;
half4 tmpvar_8;
tmpvar_8 = max(light_5, float4(0.001, 0.001, 0.001, 0.001));
(light_5).w = (tmpvar_8).w;
half4 tmpvar_9;
tmpvar_9 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_4 = tmpvar_9;
half3 tmpvar_10;
tmpvar_10 = (((unity_Lightmap_HDR).x * (lmtex_4).xyz));
lm_3 = tmpvar_10;
(light_5).xyz = (((tmpvar_8).xyz + lm_3));
fixed4 tmpvar_11;
tmpvar_11 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
(light_5).xyz = (((light_5).xyz + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_11).w)) * (tmpvar_11).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_12;
fixed spec_13;
half tmpvar_14;
tmpvar_14 = (((tmpvar_8).w * (tmpvar_6).w));
spec_13 = tmpvar_14;
(c_12).xyz = (((((((tmpvar_6).xyz * (_Color).xyz)) * (light_5).xyz)) + (((((light_5).xyz * (_SpecColor).xyz)) * spec_13))));
(c_12).w = (((tmpvar_6).w * (_Color).w));
c_2 = c_12;
float tmpvar_15;
tmpvar_15 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_2).xyz, ((float3)(tmpvar_15)));
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
float4 o_17;
float4 tmpvar_18;
tmpvar_18 = ((tmpvar_5 * 0.5));
float2 tmpvar_19;
(tmpvar_19).x = (tmpvar_18).x;
(tmpvar_19).y = (((tmpvar_18).y * (_ProjectionParams).x));
(o_17).xy = ((tmpvar_19 + (tmpvar_18).w));
(o_17).zw = (tmpvar_5).zw;
(tmpvar_4).zw = float2(0.0, 0.0);
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_7;
o.xlv_TEXCOORD2 = o_17;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed4 lmtex_3;
half4 light_4;
fixed4 tmpvar_5;
tmpvar_5 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_6;
tmpvar_6 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_4 = tmpvar_6;
half4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_3 = tmpvar_7;
half4 tmpvar_8;
(tmpvar_8).w = 0.0;
(tmpvar_8).xyz = (((unity_Lightmap_HDR).x * (lmtex_3).xyz));
light_4 = ((max(light_4, float4(0.001, 0.001, 0.001, 0.001)) + tmpvar_8));
fixed4 c_9;
fixed spec_10;
half tmpvar_11;
tmpvar_11 = (((light_4).w * (tmpvar_5).w));
spec_10 = tmpvar_11;
(c_9).xyz = (((((((tmpvar_5).xyz * (_Color).xyz)) * (light_4).xyz)) + (((((light_4).xyz * (_SpecColor).xyz)) * spec_10))));
(c_9).w = (((tmpvar_5).w * (_Color).w));
c_2 = c_9;
float tmpvar_12;
tmpvar_12 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_2).xyz, ((float3)(tmpvar_12)));
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(FOG_LINEAR) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _LightBuffer;
sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesTANGENT : TANGENT;
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float4 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float xlv_TEXCOORD5 : TEXCOORD4;
float3 xlv_TEXCOORD6 : TEXCOORD5;
float3 xlv_TEXCOORD7 : TEXCOORD6;
float3 xlv_TEXCOORD8 : TEXCOORD7;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed3 worldBinormal_1;
fixed tangentSign_2;
fixed3 worldTangent_3;
float4 tmpvar_4;
float4 tmpvar_5;
float4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = (v._glesVertex).xyz;
tmpvar_5 = (mul((mul(tmpvar_6, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
float3 tmpvar_7;
tmpvar_7 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
float3x3 tmpvar_8;
tmpvar_8[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_8[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_8[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_9;
tmpvar_9 = normalize((mul(tmpvar_8, v._glesNormal)));
float3x3 tmpvar_10;
tmpvar_10[0] = (transpose(unity_ObjectToWorld)[0]).xyz;
tmpvar_10[1] = (transpose(unity_ObjectToWorld)[1]).xyz;
tmpvar_10[2] = (transpose(unity_ObjectToWorld)[2]).xyz;
float3 tmpvar_11;
tmpvar_11 = normalize((mul((v._glesTANGENT).xyz, tmpvar_10)));
worldTangent_3 = tmpvar_11;
float tmpvar_12;
tmpvar_12 = (((v._glesTANGENT).w * (unity_WorldTransformParams).w));
tangentSign_2 = tmpvar_12;
float3 tmpvar_13;
tmpvar_13 = (((((((tmpvar_9).yzx * (worldTangent_3).zxy)) - (((tmpvar_9).zxy * (worldTangent_3).yzx)))) * tangentSign_2));
worldBinormal_1 = tmpvar_13;
float4 tmpvar_14;
(tmpvar_14).x = (worldTangent_3).x;
(tmpvar_14).y = (worldBinormal_1).x;
(tmpvar_14).z = (tmpvar_9).x;
(tmpvar_14).w = (tmpvar_7).x;
float4 tmpvar_15;
(tmpvar_15).x = (worldTangent_3).y;
(tmpvar_15).y = (worldBinormal_1).y;
(tmpvar_15).z = (tmpvar_9).y;
(tmpvar_15).w = (tmpvar_7).y;
float4 tmpvar_16;
(tmpvar_16).x = (worldTangent_3).z;
(tmpvar_16).y = (worldBinormal_1).z;
(tmpvar_16).z = (tmpvar_9).z;
(tmpvar_16).w = (tmpvar_7).z;
float4 o_17;
float4 tmpvar_18;
tmpvar_18 = ((tmpvar_5 * 0.5));
float2 tmpvar_19;
(tmpvar_19).x = (tmpvar_18).x;
(tmpvar_19).y = (((tmpvar_18).y * (_ProjectionParams).x));
(o_17).xy = ((tmpvar_19 + (tmpvar_18).w));
(o_17).zw = (tmpvar_5).zw;
(tmpvar_4).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_4).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = tmpvar_5;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_7;
o.xlv_TEXCOORD2 = o_17;
o.xlv_TEXCOORD3 = tmpvar_4;
o.xlv_TEXCOORD5 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_5).z) * (unity_FogParams).z)) + (unity_FogParams).w));
o.xlv_TEXCOORD6 = (tmpvar_14).xyz;
o.xlv_TEXCOORD7 = (tmpvar_15).xyz;
o.xlv_TEXCOORD8 = (tmpvar_16).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half4 c_2;
fixed4 lmtex_3;
half4 light_4;
fixed4 tmpvar_5;
tmpvar_5 = tex2D(_MainTex, i.xlv_TEXCOORD0);
fixed4 tmpvar_6;
tmpvar_6 = tex2Dproj(_LightBuffer, i.xlv_TEXCOORD2);
light_4 = tmpvar_6;
half4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
lmtex_3 = tmpvar_7;
half4 tmpvar_8;
(tmpvar_8).w = 0.0;
(tmpvar_8).xyz = (((unity_Lightmap_HDR).x * (lmtex_3).xyz));
light_4 = ((max(light_4, float4(0.001, 0.001, 0.001, 0.001)) + tmpvar_8));
fixed4 tmpvar_9;
tmpvar_9 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
(light_4).xyz = (((light_4).xyz + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_9).w)) * (tmpvar_9).xyz)), (unity_DynamicLightmap_HDR).yyy)));
fixed4 c_10;
fixed spec_11;
half tmpvar_12;
tmpvar_12 = (((light_4).w * (tmpvar_5).w));
spec_11 = tmpvar_12;
(c_10).xyz = (((((((tmpvar_5).xyz * (_Color).xyz)) * (light_4).xyz)) + (((((light_4).xyz * (_SpecColor).xyz)) * spec_11))));
(c_10).w = (((tmpvar_5).w * (_Color).w));
c_2 = c_10;
float tmpvar_13;
tmpvar_13 = clamp(i.xlv_TEXCOORD5, 0.0, 1.0);
(c_2).xyz = lerp((unity_FogColor).xyz, (c_2).xyz, ((float3)(tmpvar_13)));
(c_2).w = 1.0;
tmpvar_1 = c_2;
o.target0 = tmpvar_1;

return o;
}

#else
#error Sunny shader keyword combination has no signed donor program
#endif
ENDCG
}
Pass {
Name "DEFERRED"
Tags { "LightMode"="Deferred" "RenderType"="Opaque" }
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
#pragma multi_compile_prepassfinal
#if (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SHADOWMASK) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = float2(0.0, 0.0);
(tmpvar_1).xy = float2(0.0, 0.0);
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half4 outEmission_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed3 tmpvar_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_3 = (((tmpvar_4).xyz * (_Color).xyz));
half4 emission_5;
half3 tmpvar_6;
half3 tmpvar_7;
float3 tmpvar_8;
tmpvar_6 = tmpvar_3;
tmpvar_7 = (((((_SpecColor).xyz * (tmpvar_4).w)) * 0.3183099));
tmpvar_8 = tmpvar_2;
half4 outGBuffer2_9;
half4 tmpvar_10;
(tmpvar_10).xyz = tmpvar_6;
(tmpvar_10).w = 1.0;
half4 tmpvar_11;
(tmpvar_11).xyz = tmpvar_7;
(tmpvar_11).w = _Shininess;
float4 tmpvar_12;
(tmpvar_12).w = 1.0;
(tmpvar_12).xyz = ((((tmpvar_8 * 0.5)) + 0.5));
outGBuffer2_9 = tmpvar_12;
fixed4 tmpvar_13;
(tmpvar_13).w = 1.0;
(tmpvar_13).xyz = float3(0.0, 0.0, 0.0);
emission_5 = tmpvar_13;
(emission_5).xyz = (emission_5).xyz;
(outEmission_1).w = (emission_5).w;
(outEmission_1).xyz = exp2((-((emission_5).xyz)));
o.target0 = tmpvar_10;
o.target1 = tmpvar_11;
o.target2 = outGBuffer2_9;
o.target3 = outEmission_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
half3 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_4;
tmpvar_4 = normalize((mul(tmpvar_3, v._glesNormal)));
(tmpvar_1).zw = float2(0.0, 0.0);
(tmpvar_1).xy = float2(0.0, 0.0);
half3 normal_5;
normal_5 = tmpvar_4;
half4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = normal_5;
half3 res_7;
half3 x_8;
(x_8).x = dot(unity_SHAr, tmpvar_6);
(x_8).y = dot(unity_SHAg, tmpvar_6);
(x_8).z = dot(unity_SHAb, tmpvar_6);
half3 x1_9;
half4 tmpvar_10;
tmpvar_10 = (((normal_5).xyzz * (normal_5).yzzx));
(x1_9).x = dot(unity_SHBr, tmpvar_10);
(x1_9).y = dot(unity_SHBg, tmpvar_10);
(x1_9).z = dot(unity_SHBb, tmpvar_10);
res_7 = ((x_8 + ((x1_9 + (((unity_SHC).xyz * (((((normal_5).x * (normal_5).x)) - (((normal_5).y * (normal_5).y))))))))));
half3 tmpvar_11;
tmpvar_11 = max(((((1.055 * pow(max(res_7, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_7 = tmpvar_11;
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_4;
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = max(float3(0.0, 0.0, 0.0), tmpvar_11);

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half4 outEmission_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed3 tmpvar_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_3 = (((tmpvar_4).xyz * (_Color).xyz));
half4 emission_5;
half3 tmpvar_6;
half3 tmpvar_7;
float3 tmpvar_8;
tmpvar_6 = tmpvar_3;
tmpvar_7 = (((((_SpecColor).xyz * (tmpvar_4).w)) * 0.3183099));
tmpvar_8 = tmpvar_2;
half4 outGBuffer2_9;
half4 tmpvar_10;
(tmpvar_10).xyz = tmpvar_6;
(tmpvar_10).w = 1.0;
half4 tmpvar_11;
(tmpvar_11).xyz = tmpvar_7;
(tmpvar_11).w = _Shininess;
float4 tmpvar_12;
(tmpvar_12).w = 1.0;
(tmpvar_12).xyz = ((((tmpvar_8 * 0.5)) + 0.5));
outGBuffer2_9 = tmpvar_12;
fixed4 tmpvar_13;
(tmpvar_13).w = 1.0;
(tmpvar_13).xyz = float3(0.0, 0.0, 0.0);
emission_5 = tmpvar_13;
(emission_5).xyz = (((emission_5).xyz + ((tmpvar_3 * i.xlv_TEXCOORD4))));
(outEmission_1).w = (emission_5).w;
(outEmission_1).xyz = exp2((-((emission_5).xyz)));
o.target0 = tmpvar_10;
o.target1 = tmpvar_11;
o.target2 = outGBuffer2_9;
o.target3 = outEmission_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = float2(0.0, 0.0);
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half4 outEmission_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed3 tmpvar_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_3 = (((tmpvar_4).xyz * (_Color).xyz));
fixed4 tmpvar_5;
tmpvar_5 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
half4 emission_6;
half3 tmpvar_7;
half3 tmpvar_8;
float3 tmpvar_9;
tmpvar_7 = tmpvar_3;
tmpvar_8 = (((((_SpecColor).xyz * (tmpvar_4).w)) * 0.3183099));
tmpvar_9 = tmpvar_2;
half4 outGBuffer2_10;
half4 tmpvar_11;
(tmpvar_11).xyz = tmpvar_7;
(tmpvar_11).w = 1.0;
half4 tmpvar_12;
(tmpvar_12).xyz = tmpvar_8;
(tmpvar_12).w = _Shininess;
float4 tmpvar_13;
(tmpvar_13).w = 1.0;
(tmpvar_13).xyz = ((((tmpvar_9 * 0.5)) + 0.5));
outGBuffer2_10 = tmpvar_13;
fixed4 tmpvar_14;
(tmpvar_14).w = 1.0;
(tmpvar_14).xyz = float3(0.0, 0.0, 0.0);
emission_6 = tmpvar_14;
(emission_6).xyz = (((emission_6).xyz + ((tmpvar_3 * pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_5).w)) * (tmpvar_5).xyz)), (unity_DynamicLightmap_HDR).yyy)))));
(outEmission_1).w = (emission_6).w;
(outEmission_1).xyz = exp2((-((emission_6).xyz)));
o.target0 = tmpvar_11;
o.target1 = tmpvar_12;
o.target2 = outGBuffer2_10;
o.target3 = outEmission_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (tmpvar_1).xyz;
float3x3 tmpvar_5;
tmpvar_5[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_5[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_5[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_2).zw = float2(0.0, 0.0);
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_6;
tmpvar_6 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_6).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_7;
(tmpvar_7).w = 1.0;
(tmpvar_7).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_7, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_5, v._glesNormal)));
o.xlv_TEXCOORD2 = (tmpvar_6).xyz;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half4 outEmission_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed3 tmpvar_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_3 = (((tmpvar_4).xyz * (_Color).xyz));
half4 tmpvar_5;
tmpvar_5 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_6;
color_6 = tmpvar_5;
half4 emission_7;
half3 tmpvar_8;
half3 tmpvar_9;
float3 tmpvar_10;
tmpvar_8 = tmpvar_3;
tmpvar_9 = (((((_SpecColor).xyz * (tmpvar_4).w)) * 0.3183099));
tmpvar_10 = tmpvar_2;
half4 outGBuffer2_11;
half4 tmpvar_12;
(tmpvar_12).xyz = tmpvar_8;
(tmpvar_12).w = 1.0;
half4 tmpvar_13;
(tmpvar_13).xyz = tmpvar_9;
(tmpvar_13).w = _Shininess;
float4 tmpvar_14;
(tmpvar_14).w = 1.0;
(tmpvar_14).xyz = ((((tmpvar_10 * 0.5)) + 0.5));
outGBuffer2_11 = tmpvar_14;
fixed4 tmpvar_15;
(tmpvar_15).w = 1.0;
(tmpvar_15).xyz = float3(0.0, 0.0, 0.0);
emission_7 = tmpvar_15;
(emission_7).xyz = (((emission_7).xyz + ((tmpvar_3 * (((unity_Lightmap_HDR).x * (color_6).xyz))))));
(outEmission_1).w = (emission_7).w;
(outEmission_1).xyz = exp2((-((emission_7).xyz)));
o.target0 = tmpvar_12;
o.target1 = tmpvar_13;
o.target2 = outGBuffer2_11;
o.target3 = outEmission_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (tmpvar_1).xyz;
float3x3 tmpvar_5;
tmpvar_5[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_5[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_5[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_2).zw = float2(0.0, 0.0);
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_6;
tmpvar_6 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_6).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_7;
(tmpvar_7).w = 1.0;
(tmpvar_7).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_7, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_5, v._glesNormal)));
o.xlv_TEXCOORD2 = (tmpvar_6).xyz;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half4 outEmission_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed3 tmpvar_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_3 = (((tmpvar_4).xyz * (_Color).xyz));
half3 normalWorld_5;
normalWorld_5 = tmpvar_2;
half3 ambient_6;
half4 tmpvar_7;
(tmpvar_7).w = 1.0;
(tmpvar_7).xyz = normalWorld_5;
half3 x_8;
(x_8).x = dot(unity_SHAr, tmpvar_7);
(x_8).y = dot(unity_SHAg, tmpvar_7);
(x_8).z = dot(unity_SHAb, tmpvar_7);
half3 x1_9;
half4 tmpvar_10;
tmpvar_10 = (((normalWorld_5).xyzz * (normalWorld_5).yzzx));
(x1_9).x = dot(unity_SHBr, tmpvar_10);
(x1_9).y = dot(unity_SHBg, tmpvar_10);
(x1_9).z = dot(unity_SHBb, tmpvar_10);
ambient_6 = max(float3(0.0, 0.0, 0.0), ((x_8 + ((x1_9 + (((unity_SHC).xyz * (((((normalWorld_5).x * (normalWorld_5).x)) - (((normalWorld_5).y * (normalWorld_5).y)))))))))));
half3 tmpvar_11;
tmpvar_11 = max(((((1.055 * pow(max(ambient_6, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_6 = tmpvar_11;
half4 tmpvar_12;
tmpvar_12 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_13;
color_13 = tmpvar_12;
half4 emission_14;
half3 tmpvar_15;
half3 tmpvar_16;
float3 tmpvar_17;
tmpvar_15 = tmpvar_3;
tmpvar_16 = (((((_SpecColor).xyz * (tmpvar_4).w)) * 0.3183099));
tmpvar_17 = tmpvar_2;
half4 outGBuffer2_18;
half4 tmpvar_19;
(tmpvar_19).xyz = tmpvar_15;
(tmpvar_19).w = 1.0;
half4 tmpvar_20;
(tmpvar_20).xyz = tmpvar_16;
(tmpvar_20).w = _Shininess;
float4 tmpvar_21;
(tmpvar_21).w = 1.0;
(tmpvar_21).xyz = ((((tmpvar_17 * 0.5)) + 0.5));
outGBuffer2_18 = tmpvar_21;
fixed4 tmpvar_22;
(tmpvar_22).w = 1.0;
(tmpvar_22).xyz = float3(0.0, 0.0, 0.0);
emission_14 = tmpvar_22;
(emission_14).xyz = (((emission_14).xyz + ((tmpvar_3 * ((tmpvar_11 + (((unity_Lightmap_HDR).x * (color_13).xyz))))))));
(outEmission_1).w = (emission_14).w;
(outEmission_1).xyz = exp2((-((emission_14).xyz)));
o.target0 = tmpvar_19;
o.target1 = tmpvar_20;
o.target2 = outGBuffer2_18;
o.target3 = outEmission_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (tmpvar_1).xyz;
float3x3 tmpvar_5;
tmpvar_5[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_5[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_5[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_2).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_6;
tmpvar_6 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_6).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_7;
(tmpvar_7).w = 1.0;
(tmpvar_7).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_7, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_5, v._glesNormal)));
o.xlv_TEXCOORD2 = (tmpvar_6).xyz;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half4 outEmission_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed3 tmpvar_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_3 = (((tmpvar_4).xyz * (_Color).xyz));
half3 tmpvar_5;
half4 tmpvar_6;
tmpvar_6 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_7;
color_7 = tmpvar_6;
tmpvar_5 = (((unity_Lightmap_HDR).x * (color_7).xyz));
fixed4 tmpvar_8;
tmpvar_8 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_5 = ((tmpvar_5 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_8).w)) * (tmpvar_8).xyz)), (unity_DynamicLightmap_HDR).yyy)));
half4 emission_9;
half3 tmpvar_10;
half3 tmpvar_11;
float3 tmpvar_12;
tmpvar_10 = tmpvar_3;
tmpvar_11 = (((((_SpecColor).xyz * (tmpvar_4).w)) * 0.3183099));
tmpvar_12 = tmpvar_2;
half4 outGBuffer2_13;
half4 tmpvar_14;
(tmpvar_14).xyz = tmpvar_10;
(tmpvar_14).w = 1.0;
half4 tmpvar_15;
(tmpvar_15).xyz = tmpvar_11;
(tmpvar_15).w = _Shininess;
float4 tmpvar_16;
(tmpvar_16).w = 1.0;
(tmpvar_16).xyz = ((((tmpvar_12 * 0.5)) + 0.5));
outGBuffer2_13 = tmpvar_16;
fixed4 tmpvar_17;
(tmpvar_17).w = 1.0;
(tmpvar_17).xyz = float3(0.0, 0.0, 0.0);
emission_9 = tmpvar_17;
(emission_9).xyz = (((emission_9).xyz + ((tmpvar_3 * tmpvar_5))));
(outEmission_1).w = (emission_9).w;
(outEmission_1).xyz = exp2((-((emission_9).xyz)));
o.target0 = tmpvar_14;
o.target1 = tmpvar_15;
o.target2 = outGBuffer2_13;
o.target3 = outEmission_1;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = float2(0.0, 0.0);
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half4 outEmission_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed3 tmpvar_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_3 = (((tmpvar_4).xyz * (_Color).xyz));
half3 normalWorld_5;
normalWorld_5 = tmpvar_2;
half4 realtimeDirTex_6;
fixed4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_8;
tmpvar_8 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_6 = tmpvar_8;
fixed4 dirTex_9;
dirTex_9 = realtimeDirTex_6;
fixed3 y_10;
y_10 = (((dirTex_9).xyz - 0.5));
fixed tmpvar_11;
tmpvar_11 = max(0.0001, (dirTex_9).w);
half4 emission_12;
half3 tmpvar_13;
half3 tmpvar_14;
float3 tmpvar_15;
tmpvar_13 = tmpvar_3;
tmpvar_14 = (((((_SpecColor).xyz * (tmpvar_4).w)) * 0.3183099));
tmpvar_15 = tmpvar_2;
half4 outGBuffer2_16;
half4 tmpvar_17;
(tmpvar_17).xyz = tmpvar_13;
(tmpvar_17).w = 1.0;
half4 tmpvar_18;
(tmpvar_18).xyz = tmpvar_14;
(tmpvar_18).w = _Shininess;
float4 tmpvar_19;
(tmpvar_19).w = 1.0;
(tmpvar_19).xyz = ((((tmpvar_15 * 0.5)) + 0.5));
outGBuffer2_16 = tmpvar_19;
fixed4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = float3(0.0, 0.0, 0.0);
emission_12 = tmpvar_20;
(emission_12).xyz = (((emission_12).xyz + ((tmpvar_3 * ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_7).w)) * (tmpvar_7).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_5, y_10) + 0.5)))) / tmpvar_11))))));
(outEmission_1).w = (emission_12).w;
(outEmission_1).xyz = exp2((-((emission_12).xyz)));
o.target0 = tmpvar_17;
o.target1 = tmpvar_18;
o.target2 = outGBuffer2_16;
o.target3 = outEmission_1;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = float2(0.0, 0.0);
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half4 outEmission_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed3 tmpvar_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_3 = (((tmpvar_4).xyz * (_Color).xyz));
half3 normalWorld_5;
normalWorld_5 = tmpvar_2;
fixed4 bakedDirTex_6;
half4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_8;
color_8 = tmpvar_7;
half4 tmpvar_9;
tmpvar_9 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_6 = tmpvar_9;
fixed3 y_10;
y_10 = (((bakedDirTex_6).xyz - 0.5));
fixed tmpvar_11;
tmpvar_11 = max(0.0001, (bakedDirTex_6).w);
half4 emission_12;
half3 tmpvar_13;
half3 tmpvar_14;
float3 tmpvar_15;
tmpvar_13 = tmpvar_3;
tmpvar_14 = (((((_SpecColor).xyz * (tmpvar_4).w)) * 0.3183099));
tmpvar_15 = tmpvar_2;
half4 outGBuffer2_16;
half4 tmpvar_17;
(tmpvar_17).xyz = tmpvar_13;
(tmpvar_17).w = 1.0;
half4 tmpvar_18;
(tmpvar_18).xyz = tmpvar_14;
(tmpvar_18).w = _Shininess;
float4 tmpvar_19;
(tmpvar_19).w = 1.0;
(tmpvar_19).xyz = ((((tmpvar_15 * 0.5)) + 0.5));
outGBuffer2_16 = tmpvar_19;
fixed4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = float3(0.0, 0.0, 0.0);
emission_12 = tmpvar_20;
(emission_12).xyz = (((emission_12).xyz + ((tmpvar_3 * (((((((unity_Lightmap_HDR).x * (color_8).xyz)) * ((dot(normalWorld_5, y_10) + 0.5)))) / tmpvar_11))))));
(outEmission_1).w = (emission_12).w;
(outEmission_1).xyz = exp2((-((emission_12).xyz)));
o.target0 = tmpvar_17;
o.target1 = tmpvar_18;
o.target2 = outGBuffer2_16;
o.target3 = outEmission_1;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = float2(0.0, 0.0);
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half4 outEmission_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed3 tmpvar_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_3 = (((tmpvar_4).xyz * (_Color).xyz));
half3 normalWorld_5;
normalWorld_5 = tmpvar_2;
fixed4 bakedDirTex_6;
half3 ambient_7;
half4 tmpvar_8;
(tmpvar_8).w = 1.0;
(tmpvar_8).xyz = normalWorld_5;
half3 x_9;
(x_9).x = dot(unity_SHAr, tmpvar_8);
(x_9).y = dot(unity_SHAg, tmpvar_8);
(x_9).z = dot(unity_SHAb, tmpvar_8);
half3 x1_10;
half4 tmpvar_11;
tmpvar_11 = (((normalWorld_5).xyzz * (normalWorld_5).yzzx));
(x1_10).x = dot(unity_SHBr, tmpvar_11);
(x1_10).y = dot(unity_SHBg, tmpvar_11);
(x1_10).z = dot(unity_SHBb, tmpvar_11);
ambient_7 = max(float3(0.0, 0.0, 0.0), ((x_9 + ((x1_10 + (((unity_SHC).xyz * (((((normalWorld_5).x * (normalWorld_5).x)) - (((normalWorld_5).y * (normalWorld_5).y)))))))))));
half3 tmpvar_12;
tmpvar_12 = max(((((1.055 * pow(max(ambient_7, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_7 = tmpvar_12;
half4 tmpvar_13;
tmpvar_13 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_14;
color_14 = tmpvar_13;
half4 tmpvar_15;
tmpvar_15 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_6 = tmpvar_15;
fixed3 y_16;
y_16 = (((bakedDirTex_6).xyz - 0.5));
fixed tmpvar_17;
tmpvar_17 = max(0.0001, (bakedDirTex_6).w);
half4 emission_18;
half3 tmpvar_19;
half3 tmpvar_20;
float3 tmpvar_21;
tmpvar_19 = tmpvar_3;
tmpvar_20 = (((((_SpecColor).xyz * (tmpvar_4).w)) * 0.3183099));
tmpvar_21 = tmpvar_2;
half4 outGBuffer2_22;
half4 tmpvar_23;
(tmpvar_23).xyz = tmpvar_19;
(tmpvar_23).w = 1.0;
half4 tmpvar_24;
(tmpvar_24).xyz = tmpvar_20;
(tmpvar_24).w = _Shininess;
float4 tmpvar_25;
(tmpvar_25).w = 1.0;
(tmpvar_25).xyz = ((((tmpvar_21 * 0.5)) + 0.5));
outGBuffer2_22 = tmpvar_25;
fixed4 tmpvar_26;
(tmpvar_26).w = 1.0;
(tmpvar_26).xyz = float3(0.0, 0.0, 0.0);
emission_18 = tmpvar_26;
(emission_18).xyz = (((emission_18).xyz + ((tmpvar_3 * ((tmpvar_12 + (((((((unity_Lightmap_HDR).x * (color_14).xyz)) * ((dot(normalWorld_5, y_16) + 0.5)))) / tmpvar_17))))))));
(outEmission_1).w = (emission_18).w;
(outEmission_1).xyz = exp2((-((emission_18).xyz)));
o.target0 = tmpvar_23;
o.target1 = tmpvar_24;
o.target2 = outGBuffer2_22;
o.target3 = outEmission_1;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

half4 outEmission_1;
fixed3 tmpvar_2;
tmpvar_2 = i.xlv_TEXCOORD1;
fixed3 tmpvar_3;
fixed4 tmpvar_4;
tmpvar_4 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_3 = (((tmpvar_4).xyz * (_Color).xyz));
half3 normalWorld_5;
normalWorld_5 = tmpvar_2;
half4 realtimeDirTex_6;
fixed4 bakedDirTex_7;
half3 tmpvar_8;
half4 tmpvar_9;
tmpvar_9 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_10;
color_10 = tmpvar_9;
half4 tmpvar_11;
tmpvar_11 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_7 = tmpvar_11;
fixed3 y_12;
y_12 = (((bakedDirTex_7).xyz - 0.5));
fixed tmpvar_13;
tmpvar_13 = max(0.0001, (bakedDirTex_7).w);
tmpvar_8 = (((((((unity_Lightmap_HDR).x * (color_10).xyz)) * ((dot(normalWorld_5, y_12) + 0.5)))) / tmpvar_13));
fixed4 tmpvar_14;
tmpvar_14 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_15;
tmpvar_15 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_6 = tmpvar_15;
fixed4 dirTex_16;
dirTex_16 = realtimeDirTex_6;
fixed3 y_17;
y_17 = (((dirTex_16).xyz - 0.5));
fixed tmpvar_18;
tmpvar_18 = max(0.0001, (dirTex_16).w);
tmpvar_8 = ((tmpvar_8 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_14).w)) * (tmpvar_14).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_5, y_17) + 0.5)))) / tmpvar_18))));
half4 emission_19;
half3 tmpvar_20;
half3 tmpvar_21;
float3 tmpvar_22;
tmpvar_20 = tmpvar_3;
tmpvar_21 = (((((_SpecColor).xyz * (tmpvar_4).w)) * 0.3183099));
tmpvar_22 = tmpvar_2;
half4 outGBuffer2_23;
half4 tmpvar_24;
(tmpvar_24).xyz = tmpvar_20;
(tmpvar_24).w = 1.0;
half4 tmpvar_25;
(tmpvar_25).xyz = tmpvar_21;
(tmpvar_25).w = _Shininess;
float4 tmpvar_26;
(tmpvar_26).w = 1.0;
(tmpvar_26).xyz = ((((tmpvar_22 * 0.5)) + 0.5));
outGBuffer2_23 = tmpvar_26;
fixed4 tmpvar_27;
(tmpvar_27).w = 1.0;
(tmpvar_27).xyz = float3(0.0, 0.0, 0.0);
emission_19 = tmpvar_27;
(emission_19).xyz = (((emission_19).xyz + ((tmpvar_3 * tmpvar_8))));
(outEmission_1).w = (emission_19).w;
(outEmission_1).xyz = exp2((-((emission_19).xyz)));
o.target0 = tmpvar_24;
o.target1 = tmpvar_25;
o.target2 = outGBuffer2_23;
o.target3 = outEmission_1;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
half3 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
float3 tmpvar_4;
tmpvar_4 = normalize((mul(tmpvar_3, v._glesNormal)));
(tmpvar_1).zw = float2(0.0, 0.0);
(tmpvar_1).xy = float2(0.0, 0.0);
half3 normal_5;
normal_5 = tmpvar_4;
half4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = normal_5;
half3 res_7;
half3 x_8;
(x_8).x = dot(unity_SHAr, tmpvar_6);
(x_8).y = dot(unity_SHAg, tmpvar_6);
(x_8).z = dot(unity_SHAb, tmpvar_6);
half3 x1_9;
half4 tmpvar_10;
tmpvar_10 = (((normal_5).xyzz * (normal_5).yzzx));
(x1_9).x = dot(unity_SHBr, tmpvar_10);
(x1_9).y = dot(unity_SHBg, tmpvar_10);
(x1_9).z = dot(unity_SHBb, tmpvar_10);
res_7 = ((x_8 + ((x1_9 + (((unity_SHC).xyz * (((((normal_5).x * (normal_5).x)) - (((normal_5).y * (normal_5).y))))))))));
half3 tmpvar_11;
tmpvar_11 = max(((((1.055 * pow(max(res_7, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
res_7 = tmpvar_11;
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_4;
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;
o.xlv_TEXCOORD4 = max(float3(0.0, 0.0, 0.0), tmpvar_11);

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed3 tmpvar_1;
tmpvar_1 = i.xlv_TEXCOORD1;
fixed3 tmpvar_2;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_2 = (((tmpvar_3).xyz * (_Color).xyz));
half4 emission_4;
half3 tmpvar_5;
half3 tmpvar_6;
float3 tmpvar_7;
tmpvar_5 = tmpvar_2;
tmpvar_6 = (((((_SpecColor).xyz * (tmpvar_3).w)) * 0.3183099));
tmpvar_7 = tmpvar_1;
half4 outGBuffer2_8;
half4 tmpvar_9;
(tmpvar_9).xyz = tmpvar_5;
(tmpvar_9).w = 1.0;
half4 tmpvar_10;
(tmpvar_10).xyz = tmpvar_6;
(tmpvar_10).w = _Shininess;
float4 tmpvar_11;
(tmpvar_11).w = 1.0;
(tmpvar_11).xyz = ((((tmpvar_7 * 0.5)) + 0.5));
outGBuffer2_8 = tmpvar_11;
fixed4 tmpvar_12;
(tmpvar_12).w = 1.0;
(tmpvar_12).xyz = float3(0.0, 0.0, 0.0);
emission_4 = tmpvar_12;
(emission_4).xyz = (((emission_4).xyz + ((tmpvar_2 * i.xlv_TEXCOORD4))));
o.target0 = tmpvar_9;
o.target1 = tmpvar_10;
o.target2 = outGBuffer2_8;
o.target3 = emission_4;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && !defined(SHADOWS_SHADOWMASK) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = float2(0.0, 0.0);
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed3 tmpvar_1;
tmpvar_1 = i.xlv_TEXCOORD1;
fixed3 tmpvar_2;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_2 = (((tmpvar_3).xyz * (_Color).xyz));
fixed4 tmpvar_4;
tmpvar_4 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
half4 emission_5;
half3 tmpvar_6;
half3 tmpvar_7;
float3 tmpvar_8;
tmpvar_6 = tmpvar_2;
tmpvar_7 = (((((_SpecColor).xyz * (tmpvar_3).w)) * 0.3183099));
tmpvar_8 = tmpvar_1;
half4 outGBuffer2_9;
half4 tmpvar_10;
(tmpvar_10).xyz = tmpvar_6;
(tmpvar_10).w = 1.0;
half4 tmpvar_11;
(tmpvar_11).xyz = tmpvar_7;
(tmpvar_11).w = _Shininess;
float4 tmpvar_12;
(tmpvar_12).w = 1.0;
(tmpvar_12).xyz = ((((tmpvar_8 * 0.5)) + 0.5));
outGBuffer2_9 = tmpvar_12;
fixed4 tmpvar_13;
(tmpvar_13).w = 1.0;
(tmpvar_13).xyz = float3(0.0, 0.0, 0.0);
emission_5 = tmpvar_13;
(emission_5).xyz = (((emission_5).xyz + ((tmpvar_2 * pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_4).w)) * (tmpvar_4).xyz)), (unity_DynamicLightmap_HDR).yyy)))));
o.target0 = tmpvar_10;
o.target1 = tmpvar_11;
o.target2 = outGBuffer2_9;
o.target3 = emission_5;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && !defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && defined(SHADOWS_SHADOWMASK) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = float2(0.0, 0.0);
(tmpvar_1).xy = float2(0.0, 0.0);
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed3 tmpvar_1;
tmpvar_1 = i.xlv_TEXCOORD1;
fixed3 tmpvar_2;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_2 = (((tmpvar_3).xyz * (_Color).xyz));
half4 emission_4;
half3 tmpvar_5;
half3 tmpvar_6;
float3 tmpvar_7;
tmpvar_5 = tmpvar_2;
tmpvar_6 = (((((_SpecColor).xyz * (tmpvar_3).w)) * 0.3183099));
tmpvar_7 = tmpvar_1;
half4 outGBuffer2_8;
half4 tmpvar_9;
(tmpvar_9).xyz = tmpvar_5;
(tmpvar_9).w = 1.0;
half4 tmpvar_10;
(tmpvar_10).xyz = tmpvar_6;
(tmpvar_10).w = _Shininess;
float4 tmpvar_11;
(tmpvar_11).w = 1.0;
(tmpvar_11).xyz = ((((tmpvar_7 * 0.5)) + 0.5));
outGBuffer2_8 = tmpvar_11;
fixed4 tmpvar_12;
(tmpvar_12).w = 1.0;
(tmpvar_12).xyz = float3(0.0, 0.0, 0.0);
emission_4 = tmpvar_12;
(emission_4).xyz = (emission_4).xyz;
o.target0 = tmpvar_9;
o.target1 = tmpvar_10;
o.target2 = outGBuffer2_8;
o.target3 = emission_4;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (tmpvar_1).xyz;
float3x3 tmpvar_5;
tmpvar_5[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_5[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_5[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_2).zw = float2(0.0, 0.0);
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_6;
tmpvar_6 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_6).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_7;
(tmpvar_7).w = 1.0;
(tmpvar_7).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_7, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_5, v._glesNormal)));
o.xlv_TEXCOORD2 = (tmpvar_6).xyz;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed3 tmpvar_1;
tmpvar_1 = i.xlv_TEXCOORD1;
fixed3 tmpvar_2;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_2 = (((tmpvar_3).xyz * (_Color).xyz));
half4 tmpvar_4;
tmpvar_4 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_5;
color_5 = tmpvar_4;
half4 emission_6;
half3 tmpvar_7;
half3 tmpvar_8;
float3 tmpvar_9;
tmpvar_7 = tmpvar_2;
tmpvar_8 = (((((_SpecColor).xyz * (tmpvar_3).w)) * 0.3183099));
tmpvar_9 = tmpvar_1;
half4 outGBuffer2_10;
half4 tmpvar_11;
(tmpvar_11).xyz = tmpvar_7;
(tmpvar_11).w = 1.0;
half4 tmpvar_12;
(tmpvar_12).xyz = tmpvar_8;
(tmpvar_12).w = _Shininess;
float4 tmpvar_13;
(tmpvar_13).w = 1.0;
(tmpvar_13).xyz = ((((tmpvar_9 * 0.5)) + 0.5));
outGBuffer2_10 = tmpvar_13;
fixed4 tmpvar_14;
(tmpvar_14).w = 1.0;
(tmpvar_14).xyz = float3(0.0, 0.0, 0.0);
emission_6 = tmpvar_14;
(emission_6).xyz = (((emission_6).xyz + ((tmpvar_2 * (((unity_Lightmap_HDR).x * (color_5).xyz))))));
o.target0 = tmpvar_11;
o.target1 = tmpvar_12;
o.target2 = outGBuffer2_10;
o.target3 = emission_6;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (tmpvar_1).xyz;
float3x3 tmpvar_5;
tmpvar_5[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_5[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_5[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_2).zw = float2(0.0, 0.0);
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_6;
tmpvar_6 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_6).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_7;
(tmpvar_7).w = 1.0;
(tmpvar_7).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_7, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_5, v._glesNormal)));
o.xlv_TEXCOORD2 = (tmpvar_6).xyz;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed3 tmpvar_1;
tmpvar_1 = i.xlv_TEXCOORD1;
fixed3 tmpvar_2;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_2 = (((tmpvar_3).xyz * (_Color).xyz));
half3 normalWorld_4;
normalWorld_4 = tmpvar_1;
half3 ambient_5;
half4 tmpvar_6;
(tmpvar_6).w = 1.0;
(tmpvar_6).xyz = normalWorld_4;
half3 x_7;
(x_7).x = dot(unity_SHAr, tmpvar_6);
(x_7).y = dot(unity_SHAg, tmpvar_6);
(x_7).z = dot(unity_SHAb, tmpvar_6);
half3 x1_8;
half4 tmpvar_9;
tmpvar_9 = (((normalWorld_4).xyzz * (normalWorld_4).yzzx));
(x1_8).x = dot(unity_SHBr, tmpvar_9);
(x1_8).y = dot(unity_SHBg, tmpvar_9);
(x1_8).z = dot(unity_SHBb, tmpvar_9);
ambient_5 = max(float3(0.0, 0.0, 0.0), ((x_7 + ((x1_8 + (((unity_SHC).xyz * (((((normalWorld_4).x * (normalWorld_4).x)) - (((normalWorld_4).y * (normalWorld_4).y)))))))))));
half3 tmpvar_10;
tmpvar_10 = max(((((1.055 * pow(max(ambient_5, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_5 = tmpvar_10;
half4 tmpvar_11;
tmpvar_11 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_12;
color_12 = tmpvar_11;
half4 emission_13;
half3 tmpvar_14;
half3 tmpvar_15;
float3 tmpvar_16;
tmpvar_14 = tmpvar_2;
tmpvar_15 = (((((_SpecColor).xyz * (tmpvar_3).w)) * 0.3183099));
tmpvar_16 = tmpvar_1;
half4 outGBuffer2_17;
half4 tmpvar_18;
(tmpvar_18).xyz = tmpvar_14;
(tmpvar_18).w = 1.0;
half4 tmpvar_19;
(tmpvar_19).xyz = tmpvar_15;
(tmpvar_19).w = _Shininess;
float4 tmpvar_20;
(tmpvar_20).w = 1.0;
(tmpvar_20).xyz = ((((tmpvar_16 * 0.5)) + 0.5));
outGBuffer2_17 = tmpvar_20;
fixed4 tmpvar_21;
(tmpvar_21).w = 1.0;
(tmpvar_21).xyz = float3(0.0, 0.0, 0.0);
emission_13 = tmpvar_21;
(emission_13).xyz = (((emission_13).xyz + ((tmpvar_2 * ((tmpvar_10 + (((unity_Lightmap_HDR).x * (color_12).xyz))))))));
o.target0 = tmpvar_18;
o.target1 = tmpvar_19;
o.target2 = outGBuffer2_17;
o.target3 = emission_13;

return o;
}

#elif (!defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
float4 xlv_TEXCOORD4 : TEXCOORD4;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
tmpvar_1 = v._glesVertex;
float4 tmpvar_2;
float4 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (tmpvar_1).xyz;
float3x3 tmpvar_5;
tmpvar_5[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_5[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_5[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_2).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_2).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float4 tmpvar_6;
tmpvar_6 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
(tmpvar_3).xyz = (((((tmpvar_6).xyz - (unity_ShadowFadeCenterAndType).xyz)) * (unity_ShadowFadeCenterAndType).w));
float4 tmpvar_7;
(tmpvar_7).w = 1.0;
(tmpvar_7).xyz = (tmpvar_1).xyz;
(tmpvar_3).w = (((-(((mul((mul(tmpvar_7, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).z)) * ((1.0 - (unity_ShadowFadeCenterAndType).w))));
o.position = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_5, v._glesNormal)));
o.xlv_TEXCOORD2 = (tmpvar_6).xyz;
o.xlv_TEXCOORD3 = tmpvar_2;
o.xlv_TEXCOORD4 = tmpvar_3;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed3 tmpvar_1;
tmpvar_1 = i.xlv_TEXCOORD1;
fixed3 tmpvar_2;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_2 = (((tmpvar_3).xyz * (_Color).xyz));
half3 tmpvar_4;
half4 tmpvar_5;
tmpvar_5 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_6;
color_6 = tmpvar_5;
tmpvar_4 = (((unity_Lightmap_HDR).x * (color_6).xyz));
fixed4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
tmpvar_4 = ((tmpvar_4 + pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_7).w)) * (tmpvar_7).xyz)), (unity_DynamicLightmap_HDR).yyy)));
half4 emission_8;
half3 tmpvar_9;
half3 tmpvar_10;
float3 tmpvar_11;
tmpvar_9 = tmpvar_2;
tmpvar_10 = (((((_SpecColor).xyz * (tmpvar_3).w)) * 0.3183099));
tmpvar_11 = tmpvar_1;
half4 outGBuffer2_12;
half4 tmpvar_13;
(tmpvar_13).xyz = tmpvar_9;
(tmpvar_13).w = 1.0;
half4 tmpvar_14;
(tmpvar_14).xyz = tmpvar_10;
(tmpvar_14).w = _Shininess;
float4 tmpvar_15;
(tmpvar_15).w = 1.0;
(tmpvar_15).xyz = ((((tmpvar_11 * 0.5)) + 0.5));
outGBuffer2_12 = tmpvar_15;
fixed4 tmpvar_16;
(tmpvar_16).w = 1.0;
(tmpvar_16).xyz = float3(0.0, 0.0, 0.0);
emission_8 = tmpvar_16;
(emission_8).xyz = (((emission_8).xyz + ((tmpvar_2 * tmpvar_4))));
o.target0 = tmpvar_13;
o.target1 = tmpvar_14;
o.target2 = outGBuffer2_12;
o.target3 = emission_8;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && !defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = float2(0.0, 0.0);
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed3 tmpvar_1;
tmpvar_1 = i.xlv_TEXCOORD1;
fixed3 tmpvar_2;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_2 = (((tmpvar_3).xyz * (_Color).xyz));
half3 normalWorld_4;
normalWorld_4 = tmpvar_1;
half4 realtimeDirTex_5;
fixed4 tmpvar_6;
tmpvar_6 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_7;
tmpvar_7 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_5 = tmpvar_7;
fixed4 dirTex_8;
dirTex_8 = realtimeDirTex_5;
fixed3 y_9;
y_9 = (((dirTex_8).xyz - 0.5));
fixed tmpvar_10;
tmpvar_10 = max(0.0001, (dirTex_8).w);
half4 emission_11;
half3 tmpvar_12;
half3 tmpvar_13;
float3 tmpvar_14;
tmpvar_12 = tmpvar_2;
tmpvar_13 = (((((_SpecColor).xyz * (tmpvar_3).w)) * 0.3183099));
tmpvar_14 = tmpvar_1;
half4 outGBuffer2_15;
half4 tmpvar_16;
(tmpvar_16).xyz = tmpvar_12;
(tmpvar_16).w = 1.0;
half4 tmpvar_17;
(tmpvar_17).xyz = tmpvar_13;
(tmpvar_17).w = _Shininess;
float4 tmpvar_18;
(tmpvar_18).w = 1.0;
(tmpvar_18).xyz = ((((tmpvar_14 * 0.5)) + 0.5));
outGBuffer2_15 = tmpvar_18;
fixed4 tmpvar_19;
(tmpvar_19).w = 1.0;
(tmpvar_19).xyz = float3(0.0, 0.0, 0.0);
emission_11 = tmpvar_19;
(emission_11).xyz = (((emission_11).xyz + ((tmpvar_2 * ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_6).w)) * (tmpvar_6).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_4, y_9) + 0.5)))) / tmpvar_10))))));
o.target0 = tmpvar_16;
o.target1 = tmpvar_17;
o.target2 = outGBuffer2_15;
o.target3 = emission_11;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = float2(0.0, 0.0);
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed3 tmpvar_1;
tmpvar_1 = i.xlv_TEXCOORD1;
fixed3 tmpvar_2;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_2 = (((tmpvar_3).xyz * (_Color).xyz));
half3 normalWorld_4;
normalWorld_4 = tmpvar_1;
fixed4 bakedDirTex_5;
half4 tmpvar_6;
tmpvar_6 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_7;
color_7 = tmpvar_6;
half4 tmpvar_8;
tmpvar_8 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_5 = tmpvar_8;
fixed3 y_9;
y_9 = (((bakedDirTex_5).xyz - 0.5));
fixed tmpvar_10;
tmpvar_10 = max(0.0001, (bakedDirTex_5).w);
half4 emission_11;
half3 tmpvar_12;
half3 tmpvar_13;
float3 tmpvar_14;
tmpvar_12 = tmpvar_2;
tmpvar_13 = (((((_SpecColor).xyz * (tmpvar_3).w)) * 0.3183099));
tmpvar_14 = tmpvar_1;
half4 outGBuffer2_15;
half4 tmpvar_16;
(tmpvar_16).xyz = tmpvar_12;
(tmpvar_16).w = 1.0;
half4 tmpvar_17;
(tmpvar_17).xyz = tmpvar_13;
(tmpvar_17).w = _Shininess;
float4 tmpvar_18;
(tmpvar_18).w = 1.0;
(tmpvar_18).xyz = ((((tmpvar_14 * 0.5)) + 0.5));
outGBuffer2_15 = tmpvar_18;
fixed4 tmpvar_19;
(tmpvar_19).w = 1.0;
(tmpvar_19).xyz = float3(0.0, 0.0, 0.0);
emission_11 = tmpvar_19;
(emission_11).xyz = (((emission_11).xyz + ((tmpvar_2 * (((((((unity_Lightmap_HDR).x * (color_7).xyz)) * ((dot(normalWorld_4, y_9) + 0.5)))) / tmpvar_10))))));
o.target0 = tmpvar_16;
o.target1 = tmpvar_17;
o.target2 = outGBuffer2_15;
o.target3 = emission_11;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && defined(DYNAMICLIGHTMAP_ON) && defined(LIGHTMAP_ON) && !defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed3 tmpvar_1;
tmpvar_1 = i.xlv_TEXCOORD1;
fixed3 tmpvar_2;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_2 = (((tmpvar_3).xyz * (_Color).xyz));
half3 normalWorld_4;
normalWorld_4 = tmpvar_1;
half4 realtimeDirTex_5;
fixed4 bakedDirTex_6;
half3 tmpvar_7;
half4 tmpvar_8;
tmpvar_8 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_9;
color_9 = tmpvar_8;
half4 tmpvar_10;
tmpvar_10 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_6 = tmpvar_10;
fixed3 y_11;
y_11 = (((bakedDirTex_6).xyz - 0.5));
fixed tmpvar_12;
tmpvar_12 = max(0.0001, (bakedDirTex_6).w);
tmpvar_7 = (((((((unity_Lightmap_HDR).x * (color_9).xyz)) * ((dot(normalWorld_4, y_11) + 0.5)))) / tmpvar_12));
fixed4 tmpvar_13;
tmpvar_13 = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
fixed4 tmpvar_14;
tmpvar_14 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, (i.xlv_TEXCOORD3).zw);
realtimeDirTex_5 = tmpvar_14;
fixed4 dirTex_15;
dirTex_15 = realtimeDirTex_5;
fixed3 y_16;
y_16 = (((dirTex_15).xyz - 0.5));
fixed tmpvar_17;
tmpvar_17 = max(0.0001, (dirTex_15).w);
tmpvar_7 = ((tmpvar_7 + ((((pow((((((unity_DynamicLightmap_HDR).x * (tmpvar_13).w)) * (tmpvar_13).xyz)), (unity_DynamicLightmap_HDR).yyy) * ((dot(normalWorld_4, y_16) + 0.5)))) / tmpvar_17))));
half4 emission_18;
half3 tmpvar_19;
half3 tmpvar_20;
float3 tmpvar_21;
tmpvar_19 = tmpvar_2;
tmpvar_20 = (((((_SpecColor).xyz * (tmpvar_3).w)) * 0.3183099));
tmpvar_21 = tmpvar_1;
half4 outGBuffer2_22;
half4 tmpvar_23;
(tmpvar_23).xyz = tmpvar_19;
(tmpvar_23).w = 1.0;
half4 tmpvar_24;
(tmpvar_24).xyz = tmpvar_20;
(tmpvar_24).w = _Shininess;
float4 tmpvar_25;
(tmpvar_25).w = 1.0;
(tmpvar_25).xyz = ((((tmpvar_21 * 0.5)) + 0.5));
outGBuffer2_22 = tmpvar_25;
fixed4 tmpvar_26;
(tmpvar_26).w = 1.0;
(tmpvar_26).xyz = float3(0.0, 0.0, 0.0);
emission_18 = tmpvar_26;
(emission_18).xyz = (((emission_18).xyz + ((tmpvar_2 * tmpvar_7))));
o.target0 = tmpvar_23;
o.target1 = tmpvar_24;
o.target2 = outGBuffer2_22;
o.target3 = emission_18;

return o;
}

#elif (defined(DIRLIGHTMAP_COMBINED) && !defined(DYNAMICLIGHTMAP_ON) && defined(LIGHTMAP_ON) && defined(LIGHTPROBE_SH) && defined(UNITY_HDR_ON))
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Shininess;
fixed4 _SpecColor;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float4 xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
float4 target1 : SV_Target1;
float4 target2 : SV_Target2;
float4 target3 : SV_Target3;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
float3x3 tmpvar_3;
tmpvar_3[0] = (transpose(unity_WorldToObject)[0]).xyz;
tmpvar_3[1] = (transpose(unity_WorldToObject)[1]).xyz;
tmpvar_3[2] = (transpose(unity_WorldToObject)[2]).xyz;
(tmpvar_1).zw = float2(0.0, 0.0);
(tmpvar_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = normalize((mul(tmpvar_3, v._glesNormal)));
o.xlv_TEXCOORD2 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;
o.xlv_TEXCOORD3 = tmpvar_1;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed3 tmpvar_1;
tmpvar_1 = i.xlv_TEXCOORD1;
fixed3 tmpvar_2;
fixed4 tmpvar_3;
tmpvar_3 = tex2D(_MainTex, i.xlv_TEXCOORD0);
tmpvar_2 = (((tmpvar_3).xyz * (_Color).xyz));
half3 normalWorld_4;
normalWorld_4 = tmpvar_1;
fixed4 bakedDirTex_5;
half3 ambient_6;
half4 tmpvar_7;
(tmpvar_7).w = 1.0;
(tmpvar_7).xyz = normalWorld_4;
half3 x_8;
(x_8).x = dot(unity_SHAr, tmpvar_7);
(x_8).y = dot(unity_SHAg, tmpvar_7);
(x_8).z = dot(unity_SHAb, tmpvar_7);
half3 x1_9;
half4 tmpvar_10;
tmpvar_10 = (((normalWorld_4).xyzz * (normalWorld_4).yzzx));
(x1_9).x = dot(unity_SHBr, tmpvar_10);
(x1_9).y = dot(unity_SHBg, tmpvar_10);
(x1_9).z = dot(unity_SHBb, tmpvar_10);
ambient_6 = max(float3(0.0, 0.0, 0.0), ((x_8 + ((x1_9 + (((unity_SHC).xyz * (((((normalWorld_4).x * (normalWorld_4).x)) - (((normalWorld_4).y * (normalWorld_4).y)))))))))));
half3 tmpvar_11;
tmpvar_11 = max(((((1.055 * pow(max(ambient_6, float3(0.0, 0.0, 0.0)), float3(0.4166667, 0.4166667, 0.4166667)))) - 0.055)), float3(0.0, 0.0, 0.0));
ambient_6 = tmpvar_11;
half4 tmpvar_12;
tmpvar_12 = UNITY_SAMPLE_TEX2D(unity_Lightmap, (i.xlv_TEXCOORD3).xy);
fixed4 color_13;
color_13 = tmpvar_12;
half4 tmpvar_14;
tmpvar_14 = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, (i.xlv_TEXCOORD3).xy);
bakedDirTex_5 = tmpvar_14;
fixed3 y_15;
y_15 = (((bakedDirTex_5).xyz - 0.5));
fixed tmpvar_16;
tmpvar_16 = max(0.0001, (bakedDirTex_5).w);
half4 emission_17;
half3 tmpvar_18;
half3 tmpvar_19;
float3 tmpvar_20;
tmpvar_18 = tmpvar_2;
tmpvar_19 = (((((_SpecColor).xyz * (tmpvar_3).w)) * 0.3183099));
tmpvar_20 = tmpvar_1;
half4 outGBuffer2_21;
half4 tmpvar_22;
(tmpvar_22).xyz = tmpvar_18;
(tmpvar_22).w = 1.0;
half4 tmpvar_23;
(tmpvar_23).xyz = tmpvar_19;
(tmpvar_23).w = _Shininess;
float4 tmpvar_24;
(tmpvar_24).w = 1.0;
(tmpvar_24).xyz = ((((tmpvar_20 * 0.5)) + 0.5));
outGBuffer2_21 = tmpvar_24;
fixed4 tmpvar_25;
(tmpvar_25).w = 1.0;
(tmpvar_25).xyz = float3(0.0, 0.0, 0.0);
emission_17 = tmpvar_25;
(emission_17).xyz = (((emission_17).xyz + ((tmpvar_2 * ((tmpvar_11 + (((((((unity_Lightmap_HDR).x * (color_13).xyz)) * ((dot(normalWorld_4, y_15) + 0.5)))) / tmpvar_16))))))));
o.target0 = tmpvar_22;
o.target1 = tmpvar_23;
o.target2 = outGBuffer2_21;
o.target3 = emission_17;

return o;
}

#else
#error Sunny shader keyword combination has no signed donor program
#endif
ENDCG
}
Pass {
Name "META"
Tags { "LightMode"="Meta" "RenderType"="Opaque" }
Blend One Zero, One Zero
BlendOp Add, Add
ColorMask RGBA
ZWrite On
ZTest LEqual
Cull Off
Offset 0.0, 0.0
AlphaToMask Off
Stencil { Ref 0 ReadMask 255 WriteMask 255 Comp Always Pass Keep Fail Keep ZFail Keep }
CGPROGRAM
#pragma target 3.0
#pragma vertex vert
#pragma fragment frag
#if 1
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
float unity_MaxOutputValue;
bool4 unity_MetaFragmentControl;
bool4 unity_MetaVertexControl;
float unity_OneOverOutputBoost;
float unity_UseLinearSpace;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
float4 _glesMultiTexCoord2 : TEXCOORD2;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float3 xlv_TEXCOORD1 : TEXCOORD1;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 vertex_1;
vertex_1 = v._glesVertex;
if ((unity_MetaVertexControl).x) {
(vertex_1).xy = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
float tmpvar_2;
if ((((v._glesVertex).z > 0.0))) {
tmpvar_2 = 0.0001;
} else {
tmpvar_2 = 0.0;
}

(vertex_1).z = tmpvar_2;
}

if ((unity_MetaVertexControl).y) {
(vertex_1).xy = (((((v._glesMultiTexCoord2).xy * (unity_DynamicLightmapST).xy)) + (unity_DynamicLightmapST).zw));
float tmpvar_3;
if ((((vertex_1).z > 0.0))) {
tmpvar_3 = 0.0001;
} else {
tmpvar_3 = 0.0;
}

(vertex_1).z = tmpvar_3;
}

float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (vertex_1).xyz;
o.position = (mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = ((mul(v._glesVertex, transpose(unity_ObjectToWorld)))).xyz;

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 tmpvar_1;
half3 tmpvar_2;
fixed3 tmpvar_3;
tmpvar_3 = (((tex2D(_MainTex, i.xlv_TEXCOORD0)).xyz * (_Color).xyz));
tmpvar_2 = tmpvar_3;
half4 res_4;
res_4 = float4(0.0, 0.0, 0.0, 0.0);
if ((unity_MetaFragmentControl).x) {
half4 tmpvar_5;
(tmpvar_5).w = 1.0;
(tmpvar_5).xyz = tmpvar_2;
(res_4).w = (tmpvar_5).w;
float3 tmpvar_6;
tmpvar_6 = clamp(pow(tmpvar_2, ((float3)(clamp(unity_OneOverOutputBoost, 0.0, 1.0)))), float3(0.0, 0.0, 0.0), ((float3)(unity_MaxOutputValue)));
(res_4).xyz = tmpvar_6;
}

if ((unity_MetaFragmentControl).y) {
half3 emission_7;
if (((bool)(unity_UseLinearSpace))) {
emission_7 = float3(0.0, 0.0, 0.0);
} else {
emission_7 = float3(0.0, 0.0, 0.0);
}

half4 tmpvar_8;
(tmpvar_8).w = 1.0;
(tmpvar_8).xyz = emission_7;
res_4 = tmpvar_8;
}

tmpvar_1 = res_4;
o.target0 = tmpvar_1;

return o;
}

#else
#error Sunny shader keyword combination has no signed donor program
#endif
ENDCG
}
}
Fallback "Classic325/Legacy Shaders/VertexLit"
}
