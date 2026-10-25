// Signed Sunny 3.2.5 GLES -> Cg expression translation, not original authoring source.
// Complete native program table SHA256: 45410612b0bf16831da0d8063e944fd1e588bb97713049bfd8201c0afea799eb
Shader "Classic325/Mobile/Unlit (Supports Lightmap)" {
Properties {
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
#pragma multi_compile __ FOG_LINEAR
#if (!defined(FOG_LINEAR))
#include "UnityCG.cginc"
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
fixed4 xlv_COLOR0 : TEXCOORD0;
float2 xlv_TEXCOORD0 : TEXCOORD1;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed4 tmpvar_1;
half4 tmpvar_2;
tmpvar_2 = clamp(float4(0.0, 0.0, 0.0, 1.1), 0.0, 1.0);
tmpvar_1 = tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
o.xlv_COLOR0 = tmpvar_1;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.position = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 col_1;
(col_1).xyz = (tex2D(_MainTex, i.xlv_TEXCOORD0)).xyz;
(col_1).w = 1.0;
o.target0 = col_1;

return o;
}

#elif (defined(FOG_LINEAR))
#include "UnityCG.cginc"
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
fixed4 xlv_COLOR0 : TEXCOORD0;
float2 xlv_TEXCOORD0 : TEXCOORD1;
fixed xlv_TEXCOORD1 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float3 tmpvar_1;
tmpvar_1 = (v._glesVertex).xyz;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = tmpvar_1;
float3 tmpvar_3;
tmpvar_3 = ((mul(tmpvar_2, (mul(transpose(unity_ObjectToWorld), transpose(unity_MatrixV)))))).xyz;
fixed4 tmpvar_4;
half4 tmpvar_5;
tmpvar_5 = clamp(float4(0.0, 0.0, 0.0, 1.1), 0.0, 1.0);
tmpvar_4 = tmpvar_5;
fixed tmpvar_6;
float tmpvar_7;
tmpvar_7 = clamp(((((sqrt(dot(tmpvar_3, tmpvar_3)) * (unity_FogParams).z)) + (unity_FogParams).w)), 0.0, 1.0);
tmpvar_6 = tmpvar_7;
float4 tmpvar_8;
(tmpvar_8).w = 1.0;
(tmpvar_8).xyz = tmpvar_1;
o.xlv_COLOR0 = tmpvar_4;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = tmpvar_6;
o.position = (mul((mul(tmpvar_8, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 col_1;
(col_1).xyz = (tex2D(_MainTex, i.xlv_TEXCOORD0)).xyz;
(col_1).w = 1.0;
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
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float2 xlv_TEXCOORD1 : TEXCOORD1;
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
o.xlv_TEXCOORD1 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.position = (mul((mul(tmpvar_1, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 col_1;
half4 tmpvar_2;
tmpvar_2 = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.xlv_TEXCOORD0);
fixed4 color_3;
color_3 = tmpvar_2;
half3 tmpvar_4;
tmpvar_4 = (((unity_Lightmap_HDR).x * (color_3).xyz));
(col_1).xyz = tmpvar_4;
(col_1).xyz = (((tex2D(_MainTex, i.xlv_TEXCOORD1)).xyz * (col_1).xyz));
(col_1).w = 1.0;
o.target0 = col_1;

return o;
}

#elif (defined(FOG_LINEAR))
#include "UnityCG.cginc"
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesMultiTexCoord0 : TEXCOORD0;
float4 _glesMultiTexCoord1 : TEXCOORD1;
};
struct SunnyVaryings { float4 position : SV_POSITION;
float2 xlv_TEXCOORD0 : TEXCOORD0;
float2 xlv_TEXCOORD1 : TEXCOORD1;
fixed xlv_TEXCOORD2 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

float3 tmpvar_1;
tmpvar_1 = (v._glesVertex).xyz;
float4 tmpvar_2;
(tmpvar_2).w = 1.0;
(tmpvar_2).xyz = tmpvar_1;
float3 tmpvar_3;
float4 tmpvar_4;
(tmpvar_4).w = 1.0;
(tmpvar_4).xyz = (tmpvar_2).xyz;
tmpvar_3 = ((mul((mul(tmpvar_4, transpose(unity_ObjectToWorld))), transpose(unity_MatrixV)))).xyz;
fixed tmpvar_5;
float tmpvar_6;
tmpvar_6 = clamp(((((sqrt(dot(tmpvar_3, tmpvar_3)) * (unity_FogParams).z)) + (unity_FogParams).w)), 0.0, 1.0);
tmpvar_5 = tmpvar_6;
float4 tmpvar_7;
(tmpvar_7).w = 1.0;
(tmpvar_7).xyz = tmpvar_1;
o.xlv_TEXCOORD0 = (((((v._glesMultiTexCoord1).xy * (unity_LightmapST).xy)) + (unity_LightmapST).zw));
o.xlv_TEXCOORD1 = (((((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy)) + (_MainTex_ST).zw));
o.xlv_TEXCOORD2 = tmpvar_5;
o.position = (mul((mul(tmpvar_7, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 col_1;
half4 tmpvar_2;
tmpvar_2 = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.xlv_TEXCOORD0);
fixed4 color_3;
color_3 = tmpvar_2;
half3 tmpvar_4;
tmpvar_4 = (((unity_Lightmap_HDR).x * (color_3).xyz));
(col_1).xyz = tmpvar_4;
(col_1).xyz = (((tex2D(_MainTex, i.xlv_TEXCOORD1)).xyz * (col_1).xyz));
(col_1).w = 1.0;
(col_1).xyz = lerp((unity_FogColor).xyz, (col_1).xyz, ((float3)(i.xlv_TEXCOORD2)));
o.target0 = col_1;

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
