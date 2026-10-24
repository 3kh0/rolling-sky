Shader "TurboChilli/Unlit/Color/Debug" {
Properties {
 _Color ("Main Color", Color) = (0.5,0.5,0.5,0)
}
SubShader { 
 LOD 200
 Tags { "LIGHTMODE"="Always" "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" }
 Pass {
  Tags { "LIGHTMODE"="Always" "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" }
  Color [_Color]
  Fog { Mode Off }
 }
}
}