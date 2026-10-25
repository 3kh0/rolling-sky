// Native Unity 5.4 Particles/Additive (Soft) GLES contract.
Shader "Classic1782/Particle AddSmooth" {
Properties { _MainTex ("Particle Texture", 2D) = "white" {} _InvFade ("Soft Particles Factor", Range(.01,3)) = 1 }
SubShader { Tags { "Queue"="Transparent" "IgnoreProjector"="true" "RenderType"="Transparent" "PreviewType"="Plane" }
Pass { ZWrite Off Cull Off Blend One OneMinusSrcColor ColorMask RGB
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_particles
#pragma multi_compile __ FOG_LINEAR
#include "UnityCG.cginc"
struct appdata { float4 vertex:POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; };
struct v2f { float4 vertex:SV_POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; float4 projected:TEXCOORD1; float fog:TEXCOORD2; };
sampler2D _MainTex; float4 _MainTex_ST; sampler2D_float _CameraDepthTexture; float _InvFade;
v2f vert(appdata v) { v2f o; o.vertex=UnityObjectToClipPos(v.vertex); o.color=v.color; o.uv=TRANSFORM_TEX(v.uv,_MainTex); o.projected=ComputeScreenPos(o.vertex); o.projected.z=-UnityObjectToViewPos(v.vertex).z; o.fog=o.vertex.z*unity_FogParams.z+unity_FogParams.w; return o; }
fixed4 frag(v2f i):SV_Target {
#ifdef SOFTPARTICLES_ON
i.color.a *= saturate(_InvFade*(LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture,UNITY_PROJ_COORD(i.projected)))-i.projected.z));
#endif
fixed4 c=i.color*tex2D(_MainTex,i.uv); c.rgb*=c.a;
#ifdef FOG_LINEAR
c.rgb*=saturate(i.fog);
#endif
return c; }
ENDCG
} } }
