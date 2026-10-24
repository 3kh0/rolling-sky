Shader "TurboChilli/RGB/Unlit/Advanced/RadialRGB_FoglessTransparentAlpha" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorR ("Color (Red)", Color) = (1,1,1,1)
        _ColorG ("Color (Green)", Color) = (1,1,1,1)
        _ColorB ("Color (Blue)", Color) = (1,1,1,1)
        _Alpha ("Alpha", Float) = 0.75
        _Ammount ("Amount", Range(0,1)) = 0.5
    }

    SubShader {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Lighting Off
        Cull Off
        Fog { Mode Off }

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _ColorR;
            float4 _ColorG;
            float4 _ColorB;
            float _Alpha;
            float _Ammount;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target {
                float2 uv = i.uv;
                float4 map = tex2D(_MainTex, uv);
                float4 col = map.r * _ColorR;

                float2 centeredUV = (uv - 0.5) * 2.0;
                float x = centeredUV.x;
                float y = centeredUV.y;

                float angle;
                if (abs(x) > (1e-8 * abs(y))) {
                    float t = y / x;
                    float approx = t * rsqrt(t * t + 1.0);
                    float signVal = sign(approx);
                    approx = signVal * (1.5708 - sqrt(1.0 - abs(approx)) * (1.5708 + abs(approx) * (-0.214602 + abs(approx) * (0.0865667 + abs(approx) * -0.0310296))));

                    if (x < 0.0) {
                        if (y >= 0.0) approx += 3.14159;
                        else approx -= 3.14159;
                    }
                    angle = approx;
                } else {
                    angle = sign(y) * 1.5708;
                }

                float normalizedAngle = 1.0 - ((angle / 3.14159) + 1.0) / 2.0;
                if (normalizedAngle > 0.25)
                    normalizedAngle -= 0.25;
                else
                    normalizedAngle += 0.75;

                if (normalizedAngle <= _Ammount) {
                    col.rgb = (map.r * _ColorR + map.g * _ColorG).rgb;
                    col.a = min(1.0, (map.r * _ColorR.a + map.g * _ColorG.a + map.b * _ColorB.a));
                } else {
                    col.a = map.r * _ColorR.a;
                }

                col.a *= _Alpha;
                return col;
            }
            ENDCG
        }
    }

    Fallback "Unlit/Transparent Cutout"
}
