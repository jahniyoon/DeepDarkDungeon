//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/SSAO" {
Properties {
_MainTex ("", 2D) = "" { }
_RandomTexture ("", 2D) = "" { }
_SSAO ("", 2D) = "" { }
}
SubShader {
 Pass {
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 59896
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
 Pass {
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 74590
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
 Pass {
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 181401
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
 Pass {
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 254245
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
 Pass {
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 290493
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