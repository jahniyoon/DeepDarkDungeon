//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "VacuumShaders/Subsurface Scattering/Diffuse" {
Properties {
[V_MaterialTag] _V_MaterialTag ("", Float) = 0
[V_MaterialTitle] _V_MaterialTitle_Default ("Default Options", Float) = 0
_Color ("Main Color", Color) = (1,1,1,1)
_MainTex ("Base (RGB)", 2D) = "white" { }
[V_MaterialTitle] _V_MaterialTitle_SSS ("Translucency Options", Float) = 0
_TransMap ("", 2D) = "white" { }
_TransColor ("", Color) = (1,1,1,1)
_TransDistortion ("", Range(0, 0.5)) = 0.1
_TransPower ("", Range(1, 16)) = 1
_TransScale ("", Float) = 1
_TransBackfaceIntensity ("", Float) = 0.15
_TransDirectianalLightStrength ("", Range(0, 1)) = 0.2
_TransOtherLightsStrength ("", Range(0, 1)) = 0.5
_V_SSS_Emission ("", Float) = 0
_V_SSS_Rim_Color ("", Color) = (1,1,1,1)
_V_SSS_Rim_Pow ("", Range(0.5, 8)) = 2
}
SubShader {
 LOD 200
 Tags { "RenderType" = "Opaque" "SSSType" = "Legacy/PixelLit" }
 Pass {
  Name "FORWARD"
  LOD 200
  Tags { "LIGHTMODE" = "FORWARDBASE" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" "SSSType" = "Legacy/PixelLit" }
  ZClip Off
  GpuProgramID 58593
Program "vp" {
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "VERTEXLIGHT_ON" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "VERTEXLIGHT_ON" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
}
}
 Pass {
  Name "FORWARD"
  LOD 200
  Tags { "LIGHTMODE" = "FORWARDADD" "RenderType" = "Opaque" "SSSType" = "Legacy/PixelLit" }
  Blend One One, One One
  ZClip Off
  ZWrite Off
  GpuProgramID 80119
Program "vp" {
SubProgram "d3d9 " {
Keywords { "POINT" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "POINT" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
}
}
 Pass {
  Name "META"
  LOD 200
  Tags { "LIGHTMODE" = "META" "RenderType" = "Opaque" "SSSType" = "Legacy/PixelLit" }
  ZClip Off
  Cull Off
  GpuProgramID 172537
Program "vp" {
SubProgram "d3d9 " {
Keywords { "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "V_SSS_NORMALIZE_DIFFUSE_COEF_ON" "V_SSS_ADVANCED_TRANSLUSENCY_OFF" "V_SSS_RIM_OFF" }
"// shader disassembly not supported on DXBC"
}
}
}
}
Fallback "Legacy Shaders/Diffuse"
CustomEditor "SubsurfaceScatteringMaterial_Editor"
}