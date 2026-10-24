Shader "TurboChilli/Unlit/Textured/Offset/Cookie, ZLess Fogless Transparent G+5" {
Properties {
 _MainTex ("Cookie (Black and White)", 2D) = "white" {}
 _Color ("Color", Color) = (0,0,0,1)
}
SubShader { 
 LOD 100
 Tags { "LIGHTMODE"="Always" "QUEUE"="Geometry+5" "IGNOREPROJECTOR"="true" }
 Pass {
  Tags { "LIGHTMODE"="Always" "QUEUE"="Geometry+5" "IGNOREPROJECTOR"="true" }
  BindChannels {
   Bind "vertex", Vertex
   Bind "color", Color
   Bind "texcoord", TexCoord
  }
  ZWrite Off
  Cull Off
  Fog { Mode Off }
  Blend Zero SrcColor
  SetTexture [_MainTex] { combine texture * primary }
  SetTexture [_MainTex] { ConstantColor (1,1,1,1) combine previous lerp(previous) constant }
 }
}
Fallback "Unlit/Transparent Cutout"
}