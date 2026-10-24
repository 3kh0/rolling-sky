Shader "TurboChilli/Unlit/Textured/Offset/Lerped, Custom-Fog, G+10"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AltTex ("Texture", 2D) = "white" {}
        _Ammount ("Lerp Weight", Range(0,1)) = 0.5
        _ColorF ("Fog Color", Color) = (0,0,0,1)
    }

    SubShader
    {
        Tags { "Queue"="Geometry+10" "IgnoreProjector"="True" "RenderType"="Opaque" }
        LOD 200
        ZWrite On
        Cull Off
        Lighting Off
        Fog { Mode Off }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _AltTex;
            float4 _ColorF;
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
                float3 scrPos : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float4 tmp = o.vertex * 0.5;
                tmp.xy = float2(tmp.x, tmp.y * _ProjectionParams.x) + tmp.w;
                o.scrPos.xy = tmp.xy;
                o.scrPos.z = saturate((o.vertex.z * 0.075) - 1.25); // Custom fog factor

                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 baseColor = tex2D(_MainTex, i.uv);
                fixed4 altColor = tex2D(_AltTex, i.uv);
                //fixed4 lerpedColor = lerp(baseColor, altColor, _Ammount);
                fixed4 lerpedColor = lerp(altColor, baseColor, _Ammount); //Invert to fix bug
                fixed4 finalColor = lerp(lerpedColor, _ColorF, i.scrPos.z);
                return finalColor;
            }
            ENDCG
        }
    }

    Fallback "Diffuse"
}
