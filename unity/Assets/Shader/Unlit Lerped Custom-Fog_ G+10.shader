Shader "TurboChilli/Unlit/Textured/Offset/Lerped, Custom-Fog, G+10"
{
    Properties
    {
        _MainTex("First Texture", 2D) = "white" {}
        _AltTex("Second Texture", 2D) = "white" {}
        _Ammount("Lerp Weight", Range(0,1)) = 0.5
        _ColorF("Fog Color", Color) = (0,0,0,1)
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" "Queue" = "Geometry+10" "IgnoreProjector" = "true" }
            LOD 200
            Lighting Off
            ZWrite On
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
                fixed4 _ColorF;
                // REMOVED _ProjectionParams declaration — it's built-in

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float3 fogCoord : TEXCOORD1;
                    float4 vertex : SV_POSITION;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    float4 clipPos = UnityObjectToClipPos(v.vertex);
                    float4 tmp = clipPos * 0.5;
                    float2 screenPos;
                    screenPos.x = tmp.x + tmp.w;
                    screenPos.y = tmp.y * _ProjectionParams.x + tmp.w;

                    o.fogCoord.xy = screenPos.xy;
                    o.fogCoord.z = saturate((clipPos.z * 0.075) - 1.25);

                    o.uv = v.uv;
                    o.vertex = clipPos;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col1 = tex2D(_MainTex, i.uv);
                    fixed4 col2 = tex2D(_AltTex, i.uv);
                    fixed4 blended = lerp(col1, col2, _Ammount);
                    return lerp(blended, _ColorF, i.fogCoord.z);
                }
                ENDCG
            }
        }
            Fallback "Diffuse"
}
