//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Mobile/VertexLit" {
Properties {
_MainTex ("Base (RGB)", 2D) = "white" { }
}
SubShader {
 LOD 80
 Tags { "RenderType" = "Opaque" }
 Pass {
  LOD 80
  Tags { "LIGHTMODE" = "Vertex" "RenderType" = "Opaque" }
  ZClip Off
  Fog {
   Mode Off
  }
  GpuProgramID 108856
Program "vp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" }
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
SubProgram "d3d9 " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" }
"// shader disassembly not supported on DXBC"
}
}
}
 Pass {
  LOD 80
  Tags { "LIGHTMODE" = "VertexLM" "RenderType" = "Opaque" }
  ZClip Off
  Fog {
   Mode Off
  }
  GpuProgramID 190774
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
  LOD 80
  Tags { "LIGHTMODE" = "VertexLMRGBM" "RenderType" = "Opaque" }
  ZClip Off
  Fog {
   Mode Off
  }
  GpuProgramID 255612
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
  Name "SHADOWCASTER"
  LOD 80
  Tags { "LIGHTMODE" = "SHADOWCASTER" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  ZClip Off
  Cull Off
  GpuProgramID 44511
Program "vp" {
SubProgram "d3d9 " {
Keywords { "SHADOWS_DEPTH" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SHADOWS_DEPTH" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "SHADOWS_DEPTH" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SHADOWS_DEPTH" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
}
}
}
}