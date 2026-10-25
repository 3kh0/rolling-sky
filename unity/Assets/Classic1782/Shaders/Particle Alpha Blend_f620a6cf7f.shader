// GLES translation, not recovered authoring source. Evidence: analysis/expansion-import/shader-source.
Shader "Classic1782/Particle Alpha Blend" {
Properties { _MainTex ("Texture", 2D) = "white" {}
_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
_InvFade ("Soft Particles Factor", Range(0.01,3)) = 1 }
SubShader { LOD 100 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" }
Pass {  ZWrite Off Cull Off Blend SrcAlpha OneMinusSrcAlpha ColorMask RGB
CGPROGRAM
#define LIGHTMAP_MODE 0
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile __ FOG_LINEAR
#pragma multi_compile_particles
#include "UnityCG.cginc"
struct appdata { float4 vertex:POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; float2 uv1:TEXCOORD1; };
struct v2f { float4 vertex:SV_POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; float fog:TEXCOORD1; float4 projected:TEXCOORD2; float2 lightmap:TEXCOORD3; };
sampler2D _MainTex; float4 _MainTex_ST; fixed4 _TintColor; float _InvFade; fixed _Cutoff;
sampler2D_float _CameraDepthTexture;
v2f vert(appdata v) { v2f o; o.vertex=UnityObjectToClipPos(v.vertex); o.color=v.color; o.uv=TRANSFORM_TEX(v.uv,_MainTex); o.fog=o.vertex.z*unity_FogParams.z+unity_FogParams.w; o.projected=ComputeScreenPos(o.vertex); o.projected.z=-UnityObjectToViewPos(v.vertex).z; o.lightmap=v.uv1*unity_LightmapST.xy+unity_LightmapST.zw; return o; }
fixed4 frag(v2f i):SV_Target {
#ifdef SOFTPARTICLES_ON
i.color.a *= saturate(_InvFade*(LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture,UNITY_PROJ_COORD(i.projected)))-i.projected.z));
#endif
fixed4 c=2*i.color*_TintColor*tex2D(_MainTex,i.uv);
#ifdef FOG_LINEAR
c.rgb=lerp(unity_FogColor.rgb,c.rgb,saturate(i.fog));
#endif
return c;
}
ENDCG
}
}

}
