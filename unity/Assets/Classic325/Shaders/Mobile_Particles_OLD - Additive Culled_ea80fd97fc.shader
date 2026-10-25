// Signed Sunny 3.2.5 GLES -> Cg expression translation, not original authoring source.
// Complete native program table SHA256: b7256cee8c2cde05c2f754a23c4dd4d4e798be2e70ca7911d3bd2d843e685d5f
Shader "Classic325/Mobile/Particles/OLD - Additive Culled" {
Properties {
_MainTex ("Particle Texture", 2D) = "white" {}
_Color ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
}
SubShader {
LOD 0
Tags { "IgnoreProjector"="true" "Queue"="Transparent" "RenderType"="Transparent" }
Pass {
Tags { "IgnoreProjector"="true" "Queue"="Transparent" "RenderType"="Transparent" }
Blend SrcAlpha One, SrcAlpha One
BlendOp Add, Add
ColorMask RGB
ZWrite Off
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
#if 1
#include "UnityCG.cginc"
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesColor : COLOR;
float4 _glesMultiTexCoord0 : TEXCOORD0;
};
struct SunnyVaryings { float4 position : SV_POSITION;
fixed4 xlv_COLOR0 : TEXCOORD0;
float2 xlv_TEXCOORD0 : TEXCOORD1;
float2 xlv_TEXCOORD1 : TEXCOORD2;
};
struct SunnyTargets {
float4 target0 : SV_Target0;
};
SunnyVaryings vert(SunnyInput v) {
SunnyVaryings o = (SunnyVaryings)0;

fixed4 tmpvar_1;
half4 tmpvar_2;
tmpvar_2 = clamp(v._glesColor, 0.0, 1.0);
tmpvar_1 = tmpvar_2;
float4 tmpvar_3;
(tmpvar_3).w = 1.0;
(tmpvar_3).xyz = (v._glesVertex).xyz;
o.xlv_COLOR0 = tmpvar_1;
float2 tmpvar_4;
tmpvar_4 = (((v._glesMultiTexCoord0).xy * (_MainTex_ST).xy));
o.xlv_TEXCOORD0 = ((tmpvar_4 + (_MainTex_ST).zw));
o.xlv_TEXCOORD1 = ((tmpvar_4 + (_MainTex_ST).zw));
o.position = (mul((mul(tmpvar_3, transpose(unity_ObjectToWorld))), transpose(unity_MatrixVP)));

return o;
}
SunnyTargets frag(SunnyVaryings i) {
SunnyTargets o = (SunnyTargets)0;

fixed4 col_1;
col_1 = ((_Color * i.xlv_COLOR0));
col_1 = ((tex2D(_MainTex, i.xlv_TEXCOORD1) * col_1));
col_1 = ((col_1 * 2.0));
if ((((col_1).w <= 0.01))) {
clip(-1);
}

o.target0 = col_1;

return o;
}

#else
#error Sunny shader keyword combination has no signed donor program
#endif
ENDCG
}
}
SubShader {
LOD 0
Tags { "IgnoreProjector"="true" "Queue"="Transparent" "RenderType"="Transparent" }
Pass {
Tags { "IgnoreProjector"="true" "Queue"="Transparent" "RenderType"="Transparent" }
Blend SrcAlpha One, SrcAlpha One
BlendOp Add, Add
ColorMask RGB
ZWrite Off
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
#if 1
#include "UnityCG.cginc"
sampler2D _MainTex;
float4 _MainTex_ST;
struct SunnyInput {
float4 _glesVertex : POSITION;
float4 _glesColor : COLOR;
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
tmpvar_2 = clamp(v._glesColor, 0.0, 1.0);
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
col_1 = ((tex2D(_MainTex, i.xlv_TEXCOORD0) * i.xlv_COLOR0));
if ((((col_1).w <= 0.01))) {
clip(-1);
}

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
