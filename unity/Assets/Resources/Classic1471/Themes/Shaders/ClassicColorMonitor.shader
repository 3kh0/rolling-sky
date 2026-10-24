// Translation of bb322b284fc12481481a19ca3753e86d:1's actual GLES program.
// _GreenIntensity is unused; the monitor branch alpha is zero in the donor.
Shader "Hidden/Classic1471/ColorMonitor"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_GreenIntensity ("Green Intensity", Float) = 1
		_MainColor ("Main Color", Color) = (0, 1, 0, 0)
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
			float4 _MainColor;
			float4 frag(v2f_img input) : SV_Target
			{
				fixed4 sampled = tex2D(_MainTex, input.uv);
				float4 color = sampled;
				float2 uv = input.uv * 2.0 - 1.0;
				float4 monitor = float4(color.rgb * _MainColor.rgb + (1.0 - sqrt(dot(uv, uv))) * _BackgroundIntensity, 0.0);
				return lerp(color, monitor, _EffectIntensity);
			}
			ENDCG
		}
	}
	Fallback Off
}
