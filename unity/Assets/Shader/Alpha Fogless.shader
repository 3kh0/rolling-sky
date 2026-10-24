Shader "TurboChilli/Unlit/Textured/Alpha, Fogless" {
Properties {
 _MainTex ("Texture", 2D) = "white" {}
}
SubShader { 
 LOD 100
 Tags { "LIGHTMODE"="Always" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" }
 Pass {
  Tags { "LIGHTMODE"="Always" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" }
  Fog { Mode Off }
  Blend SrcAlpha OneMinusSrcAlpha
  SetTexture [_MainTex] { combine texture }
 }
}
Fallback "Mobile/Unlit (Supports Lightmap)"
}