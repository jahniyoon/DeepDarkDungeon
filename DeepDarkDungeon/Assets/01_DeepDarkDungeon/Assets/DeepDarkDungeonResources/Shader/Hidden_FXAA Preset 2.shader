//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/FXAA Preset 2" {
Properties {
_MainTex ("Base (RGB)", 2D) = "white" { }
}
SubShader {
 Pass {
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 3580
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
Fallback "Hidden/FXAA II"
}