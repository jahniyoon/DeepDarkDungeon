//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/LensFlareCreate" {
Properties {
_MainTex ("Base (RGB)", 2D) = "" { }
}
SubShader {
 Pass {
  Blend One One, One One
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 22580
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