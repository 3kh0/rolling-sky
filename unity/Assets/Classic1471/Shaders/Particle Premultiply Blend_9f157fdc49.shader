// Translated from signed 1.4.7.1 GLES program. See analysis/classic-import/shader-source.
Shader "Classic1471/Particle Premultiply Blend" {
Properties { _MainTex ("Particle Texture", 2D) = "white" {} _InvFade ("Soft Particles Factor", Range(0.01,3)) = 1 }
SubShader { Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" }
Pass { ZWrite Off Cull Off Blend One OneMinusSrcAlpha ColorMask RGB
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_particles
#include "UnityCG.cginc"
struct input { float4 vertex : POSITION; fixed4 color : COLOR; float2 uv : TEXCOORD0; };
struct output { float4 vertex : SV_POSITION; fixed4 color : COLOR; float2 uv : TEXCOORD0; float4 projected : TEXCOORD1; float fog : TEXCOORD2; };
sampler2D _MainTex; float4 _MainTex_ST;
output vert(input v) { output o; o.vertex=UnityObjectToClipPos(v.vertex); o.color=v.color; o.uv=TRANSFORM_TEX(v.uv,_MainTex); o.projected=ComputeScreenPos(o.vertex); o.projected.z=-UnityObjectToViewPos(v.vertex).z; o.fog=saturate(length(UnityObjectToViewPos(v.vertex))*unity_FogParams.z+unity_FogParams.w); return o; }

sampler2D_float _CameraDepthTexture; float _InvFade;
fixed4 frag(output i):SV_Target {
#ifdef SOFTPARTICLES_ON
i.color.a *= saturate(_InvFade*(LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projected)))-i.projected.z));
#endif
return i.color*tex2D(_MainTex,i.uv)*i.color.a;
}
ENDCG
}
}

}
