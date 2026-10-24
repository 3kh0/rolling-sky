Shader "TurboChilli/Lit/Advanced/Diffuse (with Ambient), Specular (with Gloss and Ambient), Emission (core only), BiLit, Fogless +30" {
Properties {
 _ColorD ("Diffuse Color", Color) = (0.5,0,0,1)
 _ColorDa ("Diffuse Ambient Color", Color) = (0,0,0.22,1)
 _ColorS ("Specular Color", Color) = (1,1,1,1)
 _ColorSa ("Specular Ambient Color", Color) = (0,0,0.185,1)
 _Gloss ("Gloss", Range(0,1)) = 0
 _Ammount ("Emission Ammount", Range(0,1)) = 0
 _ColorE ("Core Emission Color", Color) = (1,0.75,0,1)
 _ColorEAlt ("Rim  Emission Color", Color) = (1,0,0,1)
 _ColorEWeight ("Emission Ammount", Range(1,10)) = 1
}
	//DummyShaderTextExporter
	
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard fullforwardshadows
#pragma target 3.0
		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
		}
		ENDCG
	}
}