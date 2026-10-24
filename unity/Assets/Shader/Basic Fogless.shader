Shader "TurboChilli/Unlit/Textured/Basic" {
Properties {
 _MainTex ("Texture", 2D) = "white" {}
}
SubShader { 
 LOD 100
 Tags { "LIGHTMODE"="Always" "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" }
 Pass {
  Tags { "LIGHTMODE"="Always" "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" }
  SetTexture [_MainTex] { combine texture }
 }
}
Fallback "Mobile/Unlit (Supports Lightmap)"
}