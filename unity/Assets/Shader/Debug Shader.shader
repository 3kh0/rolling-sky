Shader "TurboChilli/Debug/Mobile Unlit Fogless" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader { 
 LOD 100
 Tags { "LIGHTMODE"="Always" }
 Pass {
  Tags { "LIGHTMODE"="Always" }
  Fog { Mode Off }
  SetTexture [_MainTex] { combine texture }
 }
}
Fallback "Mobile/Unlit (Supports Lightmap)"
}