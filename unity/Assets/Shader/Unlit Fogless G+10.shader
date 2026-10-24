Shader "TurboChilli/Unlit/Textured/Offset/Fogless, G+10" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader { 
 LOD 100
 Tags { "LIGHTMODE"="Always" "QUEUE"="Geometry+10" "IGNOREPROJECTOR"="true" }
 Pass {
  Tags { "LIGHTMODE"="Always" "QUEUE"="Geometry+10" "IGNOREPROJECTOR"="true" }
  Fog { Mode Off }
  SetTexture [_MainTex] { combine texture }
 }
}
Fallback "Mobile/Unlit (Supports Lightmap)"
}