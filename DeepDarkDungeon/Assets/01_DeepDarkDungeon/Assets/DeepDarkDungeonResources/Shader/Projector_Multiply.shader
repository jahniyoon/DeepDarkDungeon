//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Projector/Multiply" {
Properties {
_ShadowTex ("Cookie", 2D) = "gray" { }
_FalloffTex ("FallOff", 2D) = "white" { }
}
SubShader {
 Tags { "QUEUE" = "Transparent" }
 Pass {
  Tags { "QUEUE" = "Transparent" }
  Blend DstColor Zero, DstColor Zero
  ColorMask RGB 0
  ZClip Off
  ZWrite Off
  Offset -1, -1
  GpuProgramID 17833
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
}