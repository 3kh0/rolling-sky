// Cross-platform translation of daf9781cad112c75d0008dfa8d76c639:1's GLES
// subprogram, NOT recovered authoring source. Ramp default behavior must be
// checked in Unity 2017: donor component's serialized textureRamp is null.
Shader "Hidden/Classic1471/Grayscale Effect"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_RampTex ("Base (RGB)", 2D) = "grayscaleRamp" {}
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
			sampler2D _RampTex;
			half _RampOffset;
			fixed4 frag(v2f_img input) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, input.uv);
				fixed grayscale = dot(color.rgb, unity_ColorSpaceLuminance.rgb);
				color.rgb = tex2D(_RampTex, half2(grayscale + _RampOffset, 0.5h)).rgb;
				return color;
			}
			ENDCG
		}
	}
	Fallback Off
}
