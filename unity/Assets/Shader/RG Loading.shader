Shader "TurboChilli/RGB/Unlit/Advanced/RG Loading"
{
    Properties
    {
        _MainTex ("Map", 2D) = "white" {}
        _Ammount ("Ammount", Range(0,1)) = 0.5
        _Alpha ("Alpha", Float) = 0.75
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
                float2 uv = i.uv;
                fixed4 texColor = tex2D(_MainTex, uv);

                float vec_y = (uv.x - 0.5) * 2.0;
                float vec_x = (0.5 - uv.y) * 2.0;

                float angle;
                if (abs(vec_x) > (1e-08 * abs(vec_y)))
                {
                    float ratio = vec_y / vec_x;
                    float baseVal = ratio * rsqrt(ratio * ratio + 1.0);
                    float absBase = abs(baseVal);
                    float arc = sign(baseVal) * (1.5708 - sqrt(1.0 - absBase) *
                        (1.5708 + absBase * (-0.214602 + absBase * (0.0865667 + absBase * -0.0310296))));

                    if (vec_x < 0.0)
                    {
                        if (vec_y >= 0.0)
                            arc += 3.14159;
                        else
                            arc -= 3.14159;
                    }

                    angle = arc;
                }
                else
                {
                    angle = sign(vec_y) * 1.5708;
                }

                float normalizedAngle = angle / 6.28318 + 0.5;
                float mask = step(1.0 - _Ammount, 1.0 - normalizedAngle);
                float alphaValue = (texColor.r + texColor.g * mask) * _Alpha;

                return fixed4(1.0, 1.0, 1.0, alphaValue);
            }
            ENDCG
        }
    }

    Fallback "TurboChilli/RGB/Unlit/Advanced/RG Clock Fallback"
}
