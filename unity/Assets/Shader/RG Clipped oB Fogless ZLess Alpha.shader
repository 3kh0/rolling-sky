Shader "TurboChilli/RGB/Unlit/Advanced/HudBar (Clipped) RGoB Fogless Transparent Alpha ZLess"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorR ("Color (Red)", Color) = (1,1,1,1)
        _ColorG ("Color (Green)", Color) = (1,1,1,1)
        _ColorB ("Color (Blue)", Color) = (1,1,1,1)
        _Alpha ("Alpha", Float) = 0.75
        _Min ("Left Edge", Float) = 0
        _Max ("Right Edge", Float) = 1
        _Ammount ("Ammount", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        LOD 200
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            Name "FORWARD"
            Tags { "LightMode" = "Always" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ColorR, _ColorG, _ColorB;
            float _Alpha, _Min, _Max, _Ammount;

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
                fixed4 texCol = tex2D(_MainTex, i.uv);

                // Apply R, G, B channel tinting
                fixed4 blendedColor = texCol.r * _ColorR +
                                      texCol.g * _ColorG +
                                      texCol.b * _ColorB;

                float clipStart = _Min;
                float clipEnd = lerp(_Min, _Max, _Ammount);

                if (i.uv.x > clipStart && i.uv.x < clipEnd)
                {
                    blendedColor.a = blendedColor.a *
                                     (texCol.r * _ColorR.a +
                                      texCol.g * _ColorG.a +
                                      texCol.b * _ColorB.a);
                }
                else
                {
                    blendedColor.a = texCol.b;
                }

                blendedColor.a *= _Alpha;

                return blendedColor;
            }
            ENDCG
        }
    }

    Fallback "Unlit/Transparent Cutout"
}
