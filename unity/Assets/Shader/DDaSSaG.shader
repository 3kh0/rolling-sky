// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TurboChilli/Lit/Diffuse (with Ambient) Specular (with Gloss and Ambient), Custom Fog" {
    Properties{
     _ColorD("Diffuse Color", Color) = (0.500000,0.000000,0.000000,1.000000)
     _ColorDa("Diffuse Ambient Color", Color) = (0.000000,0.000000,0.220000,1.000000)
     _ColorS("Specular Color", Color) = (1.000000,1.000000,1.000000,1.000000)
     _ColorSa("Specular Ambient Color", Color) = (0.000000,0.000000,0.185000,1.000000)
     _Gloss("Gloss", Range(0.000000,1.000000)) = 0.000000
     _ColorF("Fog Color", Color) = (1.000000,1.000000,1.000000,1.000000)
    [Toggle]  _AlphaAnim("Alpha Anim", Float) = 0.000000
     _Alpha("Alpha", Float) = 1.000000
     _AinmSpeed("Anim Speed", Float) = 1.000000
    }
        SubShader{
         LOD 200
         Tags { "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
         Pass {
          Tags { "LIGHTMODE" = "ForwardBase" "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
          ZWrite Off
          Blend SrcAlpha OneMinusSrcAlpha
          GpuProgramID 13652
        CGPROGRAM
        //#pragma target 4.0

        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"


        #define CODE_BLOCK_VERTEX
        //uniform float4 _ProjectionParams;
        //uniform float4x4 UNITY_MATRIX_MVP;
        //uniform float4x4 unity_ObjectToWorld;
        //uniform float4x4 unity_WorldToObject;
        //uniform float3 _WorldSpaceCameraPos;
        //uniform float4 _WorldSpaceLightPos0;
        //uniform float4 glstate_lightmodel_ambient;
        uniform float4 _LightColor0;
        uniform float3 _ColorD;
        uniform float3 _ColorS;
        uniform float3 _ColorDa;
        uniform float3 _ColorSa;
        uniform float _Gloss;
        uniform float3 _ColorF;
        struct appdata_t
        {
            float4 vertex :POSITION;
            float3 normal :NORMAL;
        };

        struct OUT_Data_Vert
        {
            float4 xlv_TEXCOORD0 :TEXCOORD0;
            float3 xlv_TEXCOORD1 :TEXCOORD1;
            float3 xlv_TEXCOORD2 :TEXCOORD2;
            float4 vertex :SV_POSITION;
        };

        struct v2f
        {
            float4 xlv_TEXCOORD0 :TEXCOORD0;
            float3 xlv_TEXCOORD1 :TEXCOORD1;
            float3 xlv_TEXCOORD2 :TEXCOORD2;
        };

        struct OUT_Data_Frag
        {
            float4 color :SV_Target0;
        };

        OUT_Data_Vert vert(appdata_t in_v)
        {
            OUT_Data_Vert out_v;
            float3 scrPos_1;
            float4 tmpvar_2;
            tmpvar_2.w = 0;
            tmpvar_2.xyz = float3(in_v.normal);
            float4 tmpvar_3;
            float4 tmpvar_4;
            tmpvar_4.w = 1;
            tmpvar_4.xyz = in_v.vertex.xyz;
            tmpvar_3 = UnityObjectToClipPos(tmpvar_4);
            float4 o_5;
            float4 tmpvar_6;
            tmpvar_6 = (tmpvar_3 * 0.5);
            float2 tmpvar_7;
            tmpvar_7.x = tmpvar_6.x;
            tmpvar_7.y = (tmpvar_6.y * _ProjectionParams.x);
            o_5.xy = (tmpvar_7 + tmpvar_6.w);
            o_5.zw = tmpvar_3.zw;
            scrPos_1.xy = o_5.xy;
            scrPos_1.z = clamp(((tmpvar_3.z * 0.075) - 1.25), 0, 1);
            out_v.vertex = tmpvar_3;
            out_v.xlv_TEXCOORD0 = mul(unity_ObjectToWorld, in_v.vertex);
            out_v.xlv_TEXCOORD1 = scrPos_1;
            out_v.xlv_TEXCOORD2 = normalize(mul(tmpvar_2, unity_WorldToObject).xyz);
            return out_v;
        }

        #define CODE_BLOCK_FRAGMENT
        OUT_Data_Frag frag(v2f in_f)
        {
            OUT_Data_Frag out_f;
            float4 col_1;
            float3 tmpvar_2;
            tmpvar_2 = normalize(_WorldSpaceLightPos0.xyz);
            float3 tmpvar_3;
            tmpvar_3 = normalize(in_f.xlv_TEXCOORD2);
            float4 tmpvar_4;
            tmpvar_4.w = 1;
            tmpvar_4.xyz = lerp((((((max(0, dot(tmpvar_3, tmpvar_2)) * _LightColor0.xyz) + (glstate_lightmodel_ambient * 2).xyz) + _ColorDa) * _ColorD) + (((pow(max(0, dot(tmpvar_3, normalize((normalize((_WorldSpaceCameraPos - in_f.xlv_TEXCOORD0.xyz)) + tmpvar_2)))), exp2(((_Gloss * 10) + 1))) * _LightColor0.xyz) + _ColorSa) * _ColorS)), _ColorF, in_f.xlv_TEXCOORD1.zzz);
            col_1 = tmpvar_4;
            out_f.color = col_1;
            return out_f;
        }


        ENDCG

        }
}
Fallback "Diffuse"
}