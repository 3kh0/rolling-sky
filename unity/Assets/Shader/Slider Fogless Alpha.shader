Shader "TurboChilli/RGB/Unlit/Advanced/Slider Fogless Alpha"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorR ("Color (Red)", Color) = (1,1,1,1)
        _ColorG ("Color (Green)", Color) = (1,1,1,1)
        _Alpha ("Alpha", Float) = 0.75
        _Ammount ("Slide Ammount", Range(-1,1)) = 0
        _Min ("Left Side", Float) = -0.5
        _Max ("Right Side", Float) = 0.5
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="true" "RenderType"="Transparent" }
        LOD 200
        ZWrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
        Lighting Off

        Pass
        {
            Name "FORWARD"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _ColorR;
            float4 _ColorG;
            float _Alpha;
            float _Ammount;
            float _Min;
            float _Max;

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
                float2 uv = i.uv;
                float2 shiftedUV;
                shiftedUV.y = uv.y;

                float lerpX = ((_Max - _Min) * ((_Ammount + 1.0) / 2.0)) + _Min;
                shiftedUV.x = uv.x - lerpX;

                fixed4 map = tex2D(_MainTex, uv);
                fixed4 mapAlt = tex2D(_MainTex, shiftedUV);

                fixed4 colr;
                colr.rgb = ((mapAlt.r * _ColorR.rgb) + (map.g * _ColorG.rgb)) - mapAlt.b;
                colr.a = min(1.0, (mapAlt.r * _ColorR.a + map.g * _ColorG.a - mapAlt.b)) * _Alpha;

                return colr;
            }
            ENDCG
        }
    }

    Fallback "Unlit/Transparent Cutout"
}
