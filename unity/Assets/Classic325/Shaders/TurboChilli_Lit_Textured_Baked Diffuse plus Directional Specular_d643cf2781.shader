// Signed Sunny 3.2.5 GLES -> Cg expression translation, not original authoring source.
// Complete native program table SHA256: c18547102c93e040708d550af6e91f6c6b415aa258a32373abff7765a4c36451
Shader "Classic325/TurboChilli/Lit/Textured/Baked Diffuse plus Directional Specular" {
Properties {
_MainTex ("Baked Diffuse", 2D) = "white" {}
_Specular ("Specular Level", Float) = 2.0
_Gloss ("Gloss", Float) = 0.5
_Color ("Specular Color", Color) = (1.0,1.0,1.0,1.0)
}
SubShader {
LOD 0
Tags {  }
Pass {
Tags { "LightMode"="ForwardBase" }
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
float3 _Color;
float _Gloss;
float4 _LightColor0;
sampler2D _MainTex;
float _Specular;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float4 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
(tmpvar_1).w = 0.0;
(tmpvar_1).xyz = v._glesNormal;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = (v._glesVertex).xyz;
o.xlv_TEXCOORD0 = (v._glesMultiTexCoord0).xy;
o.position = (mul((mul(tmpvar_2, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD1 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = normalize(((mul(transpose(unity_WorldToObject), tmpvar_1))).xyz);

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

float4 tmpvar_1;
fixed4 col_2;
float3 specularReflection_3;
float3 tmpvar_4;
tmpvar_4 = normalize(i.xlv_TEXCOORD2);
float3 tmpvar_5;
tmpvar_5 = normalize(((_WorldSpaceCameraPos - (i.xlv_TEXCOORD1).xyz)));
float3 tmpvar_6;
tmpvar_6 = normalize((_WorldSpaceLightPos0).xyz);
float tmpvar_7;
tmpvar_7 = dot(tmpvar_4, tmpvar_6);
if (((tmpvar_7 < 0.0))) {
specularReflection_3 = float3(0.0, 0.0, 0.0);
} else {
float3 I_8;
I_8 = (-(tmpvar_6));
specularReflection_3 = (((_LightColor0).xyz * pow(max(0.0, dot(((I_8 - ((2.0 * ((dot(tmpvar_4, I_8) * tmpvar_4)))))), tmpvar_5)), _Gloss)));
}

fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
float4 tmpvar_10;
(tmpvar_10).w = 1.0;
(tmpvar_10).xyz = (((tmpvar_9).xyz + ((((specularReflection_3 * _Specular)) * _Color))));
col_2 = tmpvar_10;
tmpvar_1 = col_2;
o.target0 = tmpvar_1;

return o;
}

#elif (defined(FOG_LINEAR))
#include "UnityCG.cginc"
float3 _Color;
float _Gloss;
float4 _LightColor0;
sampler2D _MainTex;
float _Specular;
struct SunnyInput {
float4 _glesVertex : POSITION;
float3 _glesNormal : NORMAL;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float4 xlv_TEXCOORD1 : TEXCOORD1;
float3 xlv_TEXCOORD2 : TEXCOORD2;
float xlv_TEXCOORD3 : TEXCOORD3;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float4 tmpvar_1;
(tmpvar_1).w = 0.0;
(tmpvar_1).xyz = v._glesNormal;
float4 tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
tmpvar_2 = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));
o.xlv_TEXCOORD0 = (v._glesMultiTexCoord0).xy;
o.position = tmpvar_2;
o.xlv_TEXCOORD1 = (mul(v._glesVertex, transpose(unity_ObjectToWorld)));
o.xlv_TEXCOORD2 = normalize(((mul(transpose(unity_WorldToObject), tmpvar_1))).xyz);
o.xlv_TEXCOORD3 = ((((UNITY_Z_0_FAR_FROM_CLIPSPACE((tmpvar_2).z) * (unity_FogParams).z)) + (unity_FogParams).w));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

float4 tmpvar_1;
fixed4 col_2;
float3 specularReflection_3;
float3 tmpvar_4;
tmpvar_4 = normalize(i.xlv_TEXCOORD2);
float3 tmpvar_5;
tmpvar_5 = normalize(((_WorldSpaceCameraPos - (i.xlv_TEXCOORD1).xyz)));
float3 tmpvar_6;
tmpvar_6 = normalize((_WorldSpaceLightPos0).xyz);
float tmpvar_7;
tmpvar_7 = dot(tmpvar_4, tmpvar_6);
if (((tmpvar_7 < 0.0))) {
specularReflection_3 = float3(0.0, 0.0, 0.0);
} else {
float3 I_8;
I_8 = (-(tmpvar_6));
specularReflection_3 = (((_LightColor0).xyz * pow(max(0.0, dot(((I_8 - ((2.0 * ((dot(tmpvar_4, I_8) * tmpvar_4)))))), tmpvar_5)), _Gloss)));
}

fixed4 tmpvar_9;
tmpvar_9 = tex2D(_MainTex, i.xlv_TEXCOORD0);
float4 tmpvar_10;
(tmpvar_10).w = 1.0;
(tmpvar_10).xyz = (((tmpvar_9).xyz + ((((specularReflection_3 * _Specular)) * _Color))));
col_2 = tmpvar_10;
float tmpvar_11;
tmpvar_11 = clamp(i.xlv_TEXCOORD3, 0.0, 1.0);
(col_2).xyz = lerp((unity_FogColor).xyz, (col_2).xyz, ((float3)(tmpvar_11)));
tmpvar_1 = col_2;
o.target0 = tmpvar_1;

return o;
}

#else
#error Sunny shader keyword combination has no signed donor program
#endif
ENDCG
}
}
Fallback "Classic325/Legacy Shaders/Specular"
}
