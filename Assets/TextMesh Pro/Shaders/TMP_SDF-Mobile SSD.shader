// Simplified SDF shader:
// - No Shading Option (bevel / bump / env map)
// - No Glow Option
// - Softness is applied on both side of the outline

Shader stats.TextMeshPro/Mobile/Distance Field SSDstats. {

Properties {
	[HDR]_FaceColor		(stats.Face Colorstats., Color) = (1,1,1,1)
	_FaceDilate			(stats.Face Dilatestats., Range(-1,1)) = 0

	[HDR]_OutlineColor	(stats.Outline Colorstats., Color) = (0,0,0,1)
	_OutlineWidth		(stats.Outline Thicknessstats., Range(0,1)) = 0
	_OutlineSoftness	(stats.Outline Softnessstats., Range(0,1)) = 0

	[HDR]_UnderlayColor		(stats.Border Colorstats., Color) = (0,0,0,.5)
	_UnderlayOffsetX 	(stats.Border OffsetXstats., Range(-1,1)) = 0
	_UnderlayOffsetY 	(stats.Border OffsetYstats., Range(-1,1)) = 0
	_UnderlayDilate		(stats.Border Dilatestats., Range(-1,1)) = 0
	_UnderlaySoftness 	(stats.Border Softnessstats., Range(0,1)) = 0

	_WeightNormal		(stats.Weight Normalstats., float) = 0
	_WeightBold			(stats.Weight Boldstats., float) = .5

	_ShaderFlags		(stats.Flagsstats., float) = 0
	_ScaleRatioA		(stats.Scale RatioAstats., float) = 1
	_ScaleRatioB		(stats.Scale RatioBstats., float) = 1
	_ScaleRatioC		(stats.Scale RatioCstats., float) = 1

	_MainTex			(stats.Font Atlasstats., 2D) = stats.whitestats. {}
	_TextureWidth		(stats.Texture Widthstats., float) = 512
	_TextureHeight		(stats.Texture Heightstats., float) = 512
	_GradientScale		(stats.Gradient Scalestats., float) = 5
	_ScaleX				(stats.Scale Xstats., float) = 1
	_ScaleY				(stats.Scale Ystats., float) = 1
	_PerspectiveFilter	(stats.Perspective Correctionstats., Range(0, 1)) = 0.875
	_Sharpness			(stats.Sharpnessstats., Range(-1,1)) = 0

	_VertexOffsetX		(stats.Vertex OffsetXstats., float) = 0
	_VertexOffsetY		(stats.Vertex OffsetYstats., float) = 0

	_ClipRect			(stats.Clip Rectstats., vector) = (-32767, -32767, 32767, 32767)
	_MaskSoftnessX		(stats.Mask SoftnessXstats., float) = 0
	_MaskSoftnessY		(stats.Mask SoftnessYstats., float) = 0
	_MaskTex			(stats.Mask Texturestats., 2D) = stats.whitestats. {}
	_MaskInverse		(stats.Inversestats., float) = 0
	_MaskEdgeColor		(stats.Edge Colorstats., Color) = (1,1,1,1)
	_MaskEdgeSoftness	(stats.Edge Softnessstats., Range(0, 1)) = 0.01
	_MaskWipeControl	(stats.Wipe Positionstats., Range(0, 1)) = 0.5

	_StencilComp		(stats.Stencil Comparisonstats., Float) = 8
	_Stencil			(stats.Stencil IDstats., Float) = 0
	_StencilOp			(stats.Stencil Operationstats., Float) = 0
	_StencilWriteMask	(stats.Stencil Write Maskstats., Float) = 255
	_StencilReadMask	(stats.Stencil Read Maskstats., Float) = 255

    _CullMode           (stats.Cull Modestats., Float) = 0
	_ColorMask			(stats.Color Maskstats., Float) = 15
}

SubShader {
	Tags {
		stats.Queuestats.=stats.Transparentstats.
		stats.IgnoreProjectorstats.=stats.Truestats.
		stats.RenderTypestats.=stats.Transparentstats.
	}

	Stencil
	{
		Ref [_Stencil]
		Comp [_StencilComp]
		Pass [_StencilOp]
		ReadMask [_StencilReadMask]
		WriteMask [_StencilWriteMask]
	}

	Cull [_CullMode]
	ZWrite Off
	Lighting Off
	Fog { Mode Off }
	ZTest [unity_GUIZTestMode]
	Blend One OneMinusSrcAlpha
	ColorMask [_ColorMask]

	Pass {
		CGPROGRAM
		#pragma vertex VertShader
		#pragma fragment PixShader
		#pragma shader_feature __ OUTLINE_ON
		#pragma shader_feature __ UNDERLAY_ON UNDERLAY_INNER

		#pragma multi_compile __ UNITY_UI_CLIP_RECT
		#pragma multi_compile __ UNITY_UI_ALPHACLIP

		#include stats.UnityCG.cgincstats.
		#include stats.UnityUI.cgincstats.
		#include stats.TMPro_Properties.cgincstats.

		#include stats.TMPro_Mobile.cgincstats.

		ENDCG
	}
}

CustomEditor stats.TMPro.EditorUtilities.TMP_SDFShaderGUIstats.
}
