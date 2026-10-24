Shader "TurboChilli/RGB/Unlit/Advanced/RG Clock"
{
    Properties
    {
        _MainTex ("Map", 2D) = "white" {}
        _Ammount ("Ammount", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Ammount;

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
                float2 centered = (i.uv - 0.5) * 2.0;
                float angle = atan2(-centered.x, -centered.y); // start at 6 o'clock, CCW
                angle = (angle + UNITY_PI) / (2.0 * UNITY_PI); // normalize to 0–1

                float mask = step(angle, _Ammount);

                float4 col = tex2D(_MainTex, i.uv);
                float alpha = col.r + (col.g * mask);
                return float4(1.0, 1.0, 1.0, alpha);
            }
            ENDCG
        }
    }

    Fallback "TurboChilli/RGB/Unlit/Advanced/RG Clock Fallback"
}
