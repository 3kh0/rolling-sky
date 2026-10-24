// Translated from signed 1.4.7.1 GLES program. See analysis/classic-import/shader-source.
Shader "Classic1471/Particle Color" {
Properties {  }
SubShader { LOD 200 Tags { "LIGHTMODE"="Always" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" }
Pass { Name "FORWARD" Tags { "LIGHTMODE"="Always" } Blend SrcAlpha OneMinusSrcAlpha
CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
struct input { float4 vertex : POSITION; fixed4 color : COLOR; float2 uv : TEXCOORD0; };
struct output { float4 vertex : SV_POSITION; fixed4 color : COLOR; float2 uv : TEXCOORD0; float4 projected : TEXCOORD1; float fog : TEXCOORD2; };
sampler2D _MainTex; float4 _MainTex_ST;
output vert(input v) { output o; o.vertex=UnityObjectToClipPos(v.vertex); o.color=v.color; o.uv=TRANSFORM_TEX(v.uv,_MainTex); o.projected=ComputeScreenPos(o.vertex); o.projected.z=-UnityObjectToViewPos(v.vertex).z; o.fog=saturate(length(UnityObjectToViewPos(v.vertex))*unity_FogParams.z+unity_FogParams.w); return o; }

fixed4 frag(output i):SV_Target { return i.color; }
ENDCG
}
}
Fallback "Unlit/Transparent Cutout"
}
