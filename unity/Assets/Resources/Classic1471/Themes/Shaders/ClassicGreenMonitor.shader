// Cross-platform translation of 7704f995cbdc2400eb6ff9bc1de9023b:1's GLES
// subprogram. _GreenIntensity is declared but unused in the original program.
Shader "Hidden/Classic1471/GreenMonitor"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_GreenIntensity ("Green Intensity", Float) = 1
		_BackgroundIntensity ("Background Intensity", Float) = 0.5
		_EffectIntensity ("Effect Intensity", Float) = 1
	}
	SubShader
	{
		Pass
		{
			ZTest Always ZWrite Off Cull Off
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"
			sampler2D _MainTex;
			fixed _BackgroundIntensity;
			fixed _EffectIntensity;
			float4 frag(v2f_img input) : SV_Target
			{
				fixed4 sampled = tex2D(_MainTex, input.uv);
				float4 color = sampled;
				float2 uv = input.uv * 2.0 - 1.0;
				float4 monitor = float4(0.0, color.g + (1.0 - sqrt(dot(uv, uv))) * _BackgroundIntensity, 0.0, 0.0);
				return lerp(color, monitor, _EffectIntensity);
			}
			ENDCG
		}
	}
	Fallback Off
}
