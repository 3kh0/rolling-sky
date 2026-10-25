// Unity legacy Specular surface contract, not the baseline dummy Standard shader.
Shader "Classic1782/Normal-Glossy" {
Properties { _Color ("Main Color", Color) = (1,1,1,1) _SpecColor ("Specular Color", Color) = (.5,.5,.5,1)
_Shininess ("Shininess", Range(.01,1)) = .078125 _MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {} }
SubShader { Tags { "RenderType"="Opaque" } LOD 300
CGPROGRAM
#pragma surface surf BlinnPhong
sampler2D _MainTex; fixed4 _Color; half _Shininess;
struct Input { float2 uv_MainTex; };
void surf(Input i, inout SurfaceOutput o) { fixed4 c=tex2D(_MainTex,i.uv_MainTex); o.Albedo=c.rgb*_Color.rgb; o.Gloss=c.a; o.Alpha=c.a*_Color.a; o.Specular=_Shininess; }
ENDCG
}
Fallback "Legacy Shaders/VertexLit"
}
