// GLES translation, not recovered authoring source. Evidence: analysis/expansion-import/shader-source.
Shader "Classic1782/Mobile-Particle-Add" {
Properties { _MainTex ("Texture", 2D) = "white" {} }
SubShader { LOD 100 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" }
Pass {  ZWrite Off Cull Off Blend SrcAlpha One
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
v2f vert(appdata v) { v2f o; o.vertex=UnityObjectToClipPos(v.vertex); o.color=saturate(v.color); o.uv=TRANSFORM_TEX(v.uv,_MainTex); o.fog=saturate(length(UnityObjectToViewPos(v.vertex))*unity_FogParams.z+unity_FogParams.w); o.projected=ComputeScreenPos(o.vertex); o.projected.z=-UnityObjectToViewPos(v.vertex).z; o.lightmap=v.uv1*unity_LightmapST.xy+unity_LightmapST.zw; return o; }
fixed4 frag(v2f i):SV_Target {
fixed4 c=tex2D(_MainTex,i.uv); c *= i.color;
#ifdef FOG_LINEAR
c.rgb=lerp(unity_FogColor.rgb,c.rgb,saturate(i.fog));
#endif
return c;
}
ENDCG
}
}

}
