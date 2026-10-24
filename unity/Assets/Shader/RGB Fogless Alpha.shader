Shader "TurboChilli/RGB/Unlit/Fogless Alpha"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorR ("Color (Red)", Color) = (1,1,1,1)
        _ColorG ("Color (Green)", Color) = (1,1,1,1)
        _ColorB ("Color (Blue)", Color) = (1,1,1,1)
        _Alpha ("Alpha", Float) = 0.75
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="true" "LightMode"="Always" }
        LOD 200
        ZWrite Off
        Cull Off
        Lighting Off
        Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "FORWARD"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _ColorR;
            fixed4 _ColorG;
            fixed4 _ColorB;
            float _Alpha;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                fixed3 rgb = tex.r * _ColorR.rgb + tex.g * _ColorG.rgb + tex.b * _ColorB.rgb;
                fixed a = min(1.0, tex.r * _ColorR.a + tex.g * _ColorG.a + tex.b * _ColorB.a) * _Alpha;
                return fixed4(rgb, a);
            }
            ENDCG
        }
    }

    Fallback "Unlit/Transparent Cutout"
}
