Shader "Custom/Unlit/LerpedTextured"
{
    Properties
    {
        _MainTex("First Texture", 2D) = "white" {}
        _AltTex("Second Texture", 2D) = "white" {}
        _Ammount("Lerp Weight", Range(0, 1)) = 0.5
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
            LOD 100
            Lighting Off
            ZWrite On
            Cull Off
            Fog { Mode Off }

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                sampler2D _AltTex;
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

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col1 = tex2D(_MainTex, i.uv);
                    fixed4 col2 = tex2D(_AltTex, i.uv);
                    return lerp(col1, col2, _Ammount);
                }
                ENDCG
            }
        }
            Fallback "Unlit/Texture"
}
