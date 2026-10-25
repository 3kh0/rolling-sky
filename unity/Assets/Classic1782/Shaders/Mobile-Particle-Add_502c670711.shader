// Native 1.7.8.2 GLES: clamped vertex color * texture, no fog variant.
Shader "Classic1782/Mobile-Particle-Add" {
Properties { _MainTex ("Particle Texture", 2D) = "white" {} }
SubShader { Tags { "Queue"="Transparent" "IgnoreProjector"="true" "RenderType"="Transparent" "PreviewType"="Plane" }
Pass { ZWrite Off Cull Off Blend SrcAlpha One
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
struct appdata { float4 vertex:POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; };
struct v2f { float4 vertex:SV_POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; };
sampler2D _MainTex; float4 _MainTex_ST;
v2f vert(appdata v) { v2f o; o.vertex=UnityObjectToClipPos(v.vertex); o.color=saturate(v.color); o.uv=TRANSFORM_TEX(v.uv,_MainTex); return o; }
fixed4 frag(v2f i):SV_Target { return tex2D(_MainTex,i.uv)*i.color; }
ENDCG
} } }
