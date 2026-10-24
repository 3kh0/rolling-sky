Shader "TurboChilli/Unlit/Textured/OLD - Y-Clipped, Fogless ZLess Transparent"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaxV ("Top Edge", Range(0,1)) = 1
        _MinV ("Bottom Edge", Range(0,1)) = 0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="true" "RenderType"="Transparent" "LightMode"="Always" }
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
            float4 _MainTex_ST;
            float _MinV;
            float _MaxV;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                if ((_MinV != 0.0 && i.uv.y < _MinV) || (_MaxV != 1.0 && i.uv.y > _MaxV))
                {
                    col.a = 0.0;
                }
                return col;
            }
            ENDCG
        }
    }

    Fallback "Unlit/Transparent Cutout"
}
