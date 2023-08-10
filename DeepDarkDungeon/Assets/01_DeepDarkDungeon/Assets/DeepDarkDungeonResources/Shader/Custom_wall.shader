//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Custom/wall" {
Properties {
_Color1 ("MonsterColor", Color) = (1,1,1,1)
_Color2 ("PlayerColor", Color) = (1,1,1,1)
}
SubShader {
 Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent+1" "RenderType" = "Transparent" }
 Pass {
  Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent+1" "RenderType" = "Transparent" }
  Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
  ZClip Off
  ZWrite Off
  Stencil {
   Ref 2
   Comp Equal
   Pass Keep
   Fail Keep
   ZFail Keep
  }
  GpuProgramID 44044
Program "vp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
"// shader disassembly not supported on DXBC"
}
}
}
}
Fallback "Diffuse"
}