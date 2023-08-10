//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Standard" {
Properties {
_Color ("Color", Color) = (1,1,1,1)
_MainTex ("Albedo", 2D) = "white" { }
_Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
_Glossiness ("Smoothness", Range(0, 1)) = 0.5
_GlossMapScale ("Smoothness Scale", Range(0, 1)) = 1
[Enum(Metallic Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0
_Metallic ("Metallic", Range(0, 1)) = 0
_MetallicGlossMap ("Metallic", 2D) = "white" { }
[ToggleOff] _SpecularHighlights ("Specular Highlights", Float) = 1
[ToggleOff] _GlossyReflections ("Glossy Reflections", Float) = 1
_BumpScale ("Scale", Float) = 1
_BumpMap ("Normal Map", 2D) = "bump" { }
_Parallax ("Height Scale", Range(0.005, 0.08)) = 0.02
_ParallaxMap ("Height Map", 2D) = "black" { }
_OcclusionStrength ("Strength", Range(0, 1)) = 1
_OcclusionMap ("Occlusion", 2D) = "white" { }
_EmissionColor ("Color", Color) = (0,0,0,1)
_EmissionMap ("Emission", 2D) = "white" { }
_DetailMask ("Detail Mask", 2D) = "white" { }
_DetailAlbedoMap ("Detail Albedo x2", 2D) = "grey" { }
_DetailNormalMapScale ("Scale", Float) = 1
_DetailNormalMap ("Normal Map", 2D) = "bump" { }
[Enum(UV0,0,UV1,1)] _UVSec ("UV Set for secondary textures", Float) = 0
_Mode ("__mode", Float) = 0
_SrcBlend ("__src", Float) = 1
_DstBlend ("__dst", Float) = 0
_ZWrite ("__zw", Float) = 1
}
SubShader {
 LOD 300
 Tags { "PerformanceChecks" = "False" "RenderType" = "Opaque" }
 Pass {
  Name "FORWARD"
  LOD 300
  Tags { "LIGHTMODE" = "FORWARDBASE" "PerformanceChecks" = "False" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  Blend Zero Zero, Zero Zero
  ZClip Off
  ZWrite Off
  GpuProgramID 56268
Program "vp" {
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_EMISSION" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_EMISSION" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_EMISSION" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_EMISSION" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
}
}
 Pass {
  Name "FORWARD_DELTA"
  LOD 300
  Tags { "LIGHTMODE" = "FORWARDADD" "PerformanceChecks" = "False" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  Blend Zero One, Zero One
  ZClip Off
  ZWrite Off
  GpuProgramID 72038
Program "vp" {
SubProgram "d3d9 " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" }
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
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" }
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
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
}
}
 Pass {
  Name "SHADOWCASTER"
  LOD 300
  Tags { "LIGHTMODE" = "SHADOWCASTER" "PerformanceChecks" = "False" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  ZClip Off
  GpuProgramID 162081
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
SubProgram "d3d9 " {
Keywords { "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
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
SubProgram "d3d9 " {
Keywords { "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
}
}
 Pass {
  Name "DEFERRED"
  LOD 300
  Tags { "LIGHTMODE" = "DEFERRED" "PerformanceChecks" = "False" "RenderType" = "Opaque" }
  ZClip Off
  GpuProgramID 244627
Program "vp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "_EMISSION" "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_EMISSION" "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "_EMISSION" "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_EMISSION" "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
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
Keywords { "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "_EMISSION" "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_EMISSION" "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" "UNITY_HDR_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "_EMISSION" "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_EMISSION" "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "LIGHTMAP_ON" "_EMISSION" "UNITY_HDR_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
}
}
 Pass {
  Name "META"
  LOD 300
  Tags { "LIGHTMODE" = "META" "PerformanceChecks" = "False" "RenderType" = "Opaque" }
  ZClip Off
  Cull Off
  GpuProgramID 274847
Program "vp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_EMISSION" }
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
Keywords { "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
}
}
}
SubShader {
 LOD 150
 Tags { "PerformanceChecks" = "False" "RenderType" = "Opaque" }
 Pass {
  Name "FORWARD"
  LOD 150
  Tags { "LIGHTMODE" = "FORWARDBASE" "PerformanceChecks" = "False" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  Blend Zero Zero, Zero Zero
  ZClip Off
  ZWrite Off
  GpuProgramID 360084
Program "vp" {
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_EMISSION" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_EMISSION" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_EMISSION" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_EMISSION" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "VERTEXLIGHT_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "LIGHTMAP_SHADOW_MIXING" "_EMISSION" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
}
}
 Pass {
  Name "FORWARD_DELTA"
  LOD 150
  Tags { "LIGHTMODE" = "FORWARDADD" "PerformanceChecks" = "False" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  Blend Zero One, Zero One
  ZClip Off
  ZWrite Off
  GpuProgramID 429947
Program "vp" {
SubProgram "d3d9 " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" }
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
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" }
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
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
}
}
 Pass {
  Name "SHADOWCASTER"
  LOD 150
  Tags { "LIGHTMODE" = "SHADOWCASTER" "PerformanceChecks" = "False" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  ZClip Off
  GpuProgramID 502921
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
SubProgram "d3d9 " {
Keywords { "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
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
SubProgram "d3d9 " {
Keywords { "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SHADOWS_DEPTH" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "SHADOWS_CUBE" "_ALPHAPREMULTIPLY_ON" }
"// shader disassembly not supported on DXBC"
}
}
}
 Pass {
  Name "META"
  LOD 150
  Tags { "LIGHTMODE" = "META" "PerformanceChecks" = "False" "RenderType" = "Opaque" }
  ZClip Off
  Cull Off
  GpuProgramID 535281
Program "vp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_EMISSION" }
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
Keywords { "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11 " {
Keywords { "_EMISSION" }
"// shader disassembly not supported on DXBC"
}
}
}
}
Fallback "VertexLit"
CustomEditor "StandardShaderGUI"
}