//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/BlitCopy" {
Properties {
_MainTex ("Texture", any) = "" { }
_Color ("Multiplicative color", Color) = (1,1,1,1)
}
SubShader {
 Pass {
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 51980
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