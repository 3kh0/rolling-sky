// Translated from signed 1.4.7.1 GLES program. See analysis/classic-import/shader-source.
Shader "Classic1782/Mobile-Particle-Alpha" {
Properties { _MainTex ("Particle Texture", 2D) = "white" {} }
SubShader { Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" }
Pass { ZWrite Off Cull Off Blend SrcAlpha OneMinusSrcAlpha
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile __ FOG_LINEAR
#include "UnityCG.cginc"
struct input { float4 vertex : POSITION; fixed4 color : COLOR; float2 uv : TEXCOORD0; };
struct output { float4 vertex : SV_POSITION; fixed4 color : COLOR; float2 uv : TEXCOORD0; float4 projected : TEXCOORD1; float fog : TEXCOORD2; };
sampler2D _MainTex; float4 _MainTex_ST;
output vert(input v) { output o; o.vertex=UnityObjectToClipPos(v.vertex); o.color=saturate(v.color); o.uv=TRANSFORM_TEX(v.uv,_MainTex); o.projected=ComputeScreenPos(o.vertex); o.projected.z=-UnityObjectToViewPos(v.vertex).z; o.fog=saturate(length(UnityObjectToViewPos(v.vertex))*unity_FogParams.z+unity_FogParams.w); return o; }

fixed4 frag(output i):SV_Target { fixed4 c=tex2D(_MainTex,i.uv)*i.color;
#ifdef FOG_LINEAR
c.rgb=lerp(unity_FogColor.rgb,c.rgb,i.fog);
#endif
return c; }
ENDCG
}
}

}
