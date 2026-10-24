// GLES translation, not recovered authoring source. Evidence: analysis/expansion-import/shader-source.
Shader "Classic1471/Mobile-Lightmap-Unlit" {
Properties { _MainTex ("Texture", 2D) = "white" {} }
SubShader { LOD 100 Tags { "RenderType"="Opaque" }
Pass { Tags { "LightMode"="Vertex" } 
CGPROGRAM
#define LIGHTMAP_MODE 0
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile __ FOG_LINEAR

#include "UnityCG.cginc"
struct appdata { float4 vertex:POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; float2 uv1:TEXCOORD1; };
struct v2f { float4 vertex:SV_POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; float fog:TEXCOORD1; float4 projected:TEXCOORD2; float2 lightmap:TEXCOORD3; };
sampler2D _MainTex; float4 _MainTex_ST; fixed4 _TintColor; float _InvFade; fixed _Cutoff;
sampler2D_float _CameraDepthTexture;
v2f vert(appdata v) { v2f o; o.vertex=UnityObjectToClipPos(v.vertex); o.color=v.color; o.uv=TRANSFORM_TEX(v.uv,_MainTex); o.fog=saturate(length(UnityObjectToViewPos(v.vertex))*unity_FogParams.z+unity_FogParams.w); o.projected=ComputeScreenPos(o.vertex); o.projected.z=-UnityObjectToViewPos(v.vertex).z; o.lightmap=v.uv1*unity_LightmapST.xy+unity_LightmapST.zw; return o; }
fixed4 frag(v2f i):SV_Target {
fixed4 c=tex2D(_MainTex,i.uv);
#if LIGHTMAP_MODE > 0
fixed4 lm=UNITY_SAMPLE_TEX2D(unity_Lightmap,i.lightmap);
#if LIGHTMAP_MODE == 2
c.rgb *= lm.rgb*lm.a*8;
#else
c.rgb *= lm.rgb*2;
#endif
#endif
c.a=1;
#ifdef FOG_LINEAR
c.rgb=lerp(unity_FogColor.rgb,c.rgb,saturate(i.fog));
#endif
return c;
}
ENDCG
}
Pass { Tags { "LightMode"="VertexLM" } 
CGPROGRAM
#define LIGHTMAP_MODE 1
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile __ FOG_LINEAR

#include "UnityCG.cginc"
struct appdata { float4 vertex:POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; float2 uv1:TEXCOORD1; };
struct v2f { float4 vertex:SV_POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; float fog:TEXCOORD1; float4 projected:TEXCOORD2; float2 lightmap:TEXCOORD3; };
sampler2D _MainTex; float4 _MainTex_ST; fixed4 _TintColor; float _InvFade; fixed _Cutoff;
sampler2D_float _CameraDepthTexture;
v2f vert(appdata v) { v2f o; o.vertex=UnityObjectToClipPos(v.vertex); o.color=v.color; o.uv=TRANSFORM_TEX(v.uv,_MainTex); o.fog=saturate(length(UnityObjectToViewPos(v.vertex))*unity_FogParams.z+unity_FogParams.w); o.projected=ComputeScreenPos(o.vertex); o.projected.z=-UnityObjectToViewPos(v.vertex).z; o.lightmap=v.uv1*unity_LightmapST.xy+unity_LightmapST.zw; return o; }
fixed4 frag(v2f i):SV_Target {
fixed4 c=tex2D(_MainTex,i.uv);
#if LIGHTMAP_MODE > 0
fixed4 lm=UNITY_SAMPLE_TEX2D(unity_Lightmap,i.lightmap);
#if LIGHTMAP_MODE == 2
c.rgb *= lm.rgb*lm.a*8;
#else
c.rgb *= lm.rgb*2;
#endif
#endif
c.a=1;
#ifdef FOG_LINEAR
c.rgb=lerp(unity_FogColor.rgb,c.rgb,saturate(i.fog));
#endif
return c;
}
ENDCG
}
Pass { Tags { "LightMode"="VertexLMRGBM" } 
CGPROGRAM
#define LIGHTMAP_MODE 2
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile __ FOG_LINEAR

#include "UnityCG.cginc"
struct appdata { float4 vertex:POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; float2 uv1:TEXCOORD1; };
struct v2f { float4 vertex:SV_POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; float fog:TEXCOORD1; float4 projected:TEXCOORD2; float2 lightmap:TEXCOORD3; };
sampler2D _MainTex; float4 _MainTex_ST; fixed4 _TintColor; float _InvFade; fixed _Cutoff;
sampler2D_float _CameraDepthTexture;
v2f vert(appdata v) { v2f o; o.vertex=UnityObjectToClipPos(v.vertex); o.color=v.color; o.uv=TRANSFORM_TEX(v.uv,_MainTex); o.fog=saturate(length(UnityObjectToViewPos(v.vertex))*unity_FogParams.z+unity_FogParams.w); o.projected=ComputeScreenPos(o.vertex); o.projected.z=-UnityObjectToViewPos(v.vertex).z; o.lightmap=v.uv1*unity_LightmapST.xy+unity_LightmapST.zw; return o; }
fixed4 frag(v2f i):SV_Target {
fixed4 c=tex2D(_MainTex,i.uv);
#if LIGHTMAP_MODE > 0
fixed4 lm=UNITY_SAMPLE_TEX2D(unity_Lightmap,i.lightmap);
#if LIGHTMAP_MODE == 2
c.rgb *= lm.rgb*lm.a*8;
#else
c.rgb *= lm.rgb*2;
#endif
#endif
c.a=1;
#ifdef FOG_LINEAR
c.rgb=lerp(unity_FogColor.rgb,c.rgb,saturate(i.fog));
#endif
return c;
}
ENDCG
}
}

}
