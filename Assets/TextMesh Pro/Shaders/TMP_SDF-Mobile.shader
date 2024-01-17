﻿// Simplified SDF shader:
// - No Shading Option (bevel / bump / env map)
// - No Glow Option
// - Softness is applied on both side of the outline

Shader stats.TextMeshPro/Mobile/Distance Fieldstats. {

Properties {
	[HDR]_FaceColor     (stats.Face Colorstats., Color) = (1,1,1,1)
	_FaceDilate			(stats.Face Dilatestats., Range(-1,1)) = 0

	[HDR]_OutlineColor	(stats.Outline Colorstats., Color) = (0,0,0,1)
	_OutlineWidth		(stats.Outline Thicknessstats., Range(0,1)) = 0
	_OutlineSoftness	(stats.Outline Softnessstats., Range(0,1)) = 0

	[HDR]_UnderlayColor	(stats.Border Colorstats., Color) = (0,0,0,.5)
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

	_StencilComp		(stats.Stencil Comparisonstats., Float) = 8
	_Stencil			(stats.Stencil IDstats., Float) = 0
	_StencilOp			(stats.Stencil Operationstats., Float) = 0
	_StencilWriteMask	(stats.Stencil Write Maskstats., Float) = 255
	_StencilReadMask	(stats.Stencil Read Maskstats., Float) = 255

	_CullMode			(stats.Cull Modestats., Float) = 0
	_ColorMask			(stats.Color Maskstats., Float) = 15
}

SubShader {
	Tags
	{
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

		struct vertex_t {
			UNITY_VERTEX_INPUT_INSTANCE_ID
			float4	vertex			: POSITION;
			float3	normal			: NORMAL;
			fixed4	color			: COLOR;
			float2	texcoord0		: TEXCOORD0;
			float2	texcoord1		: TEXCOORD1;
		};

		struct pixel_t {
			UNITY_VERTEX_INPUT_INSTANCE_ID
			UNITY_VERTEX_OUTPUT_STEREO
			float4	vertex			: SV_POSITION;
			fixed4	faceColor		: COLOR;
			fixed4	outlineColor	: COLOR1;
			float4	texcoord0		: TEXCOORD0;			// Texture UV, Mask UV
			half4	param			: TEXCOORD1;			// Scale(x), BiasIn(y), BiasOut(z), Bias(w)
			half4	mask			: TEXCOORD2;			// Position in clip space(xy), Softness(zw)
			#if (UNDERLAY_ON | UNDERLAY_INNER)
			float4	texcoord1		: TEXCOORD3;			// Texture UV, alpha, reserved
			half2	underlayParam	: TEXCOORD4;			// Scale(x), Bias(y)
			#endif
		};


		pixel_t VertShader(vertex_t input)
		{
			pixel_t output;

			UNITY_INITIALIZE_OUTPUT(pixel_t, output);
			UNITY_SETUP_INSTANCE_ID(input);
			UNITY_TRANSFER_INSTANCE_ID(input, output);
			UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

			float bold = step(input.texcoord1.y, 0);

			float4 vert = input.vertex;
			vert.x += _VertexOffsetX;
			vert.y += _VertexOffsetY;
			float4 vPosition = UnityObjectToClipPos(vert);

			float2 pixelSize = vPosition.w;
			pixelSize /= float2(_ScaleX, _ScaleY) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));

			float scale = rsqrt(dot(pixelSize, pixelSize));
			scale *= abs(input.texcoord1.y) * _GradientScale * (_Sharpness + 1);
			if(UNITY_MATRIX_P[3][3] == 0) scale = lerp(abs(scale) * (1 - _PerspectiveFilter), scale, abs(dot(UnityObjectToWorldNormal(input.normal.xyz), normalize(WorldSpaceViewDir(vert)))));

			float weight = lerp(_WeightNormal, _WeightBold, bold) / 4.0;
			weight = (weight + _FaceDilate) * _ScaleRatioA * 0.5;

			float layerScale = scale;

			scale /= 1 + (_OutlineSoftness * _ScaleRatioA * scale);
			float bias = (0.5 - weight) * scale - 0.5;
			float outline = _OutlineWidth * _ScaleRatioA * 0.5 * scale;

			float opacity = input.color.a;
			#if (UNDERLAY_ON | UNDERLAY_INNER)
			opacity = 1.0;
			#endif

			fixed4 faceColor = fixed4(input.color.rgb, opacity) * _FaceColor;
			faceColor.rgb *= faceColor.a;

			fixed4 outlineColor = _OutlineColor;
			outlineColor.a *= opacity;
			outlineColor.rgb *= outlineColor.a;
			outlineColor = lerp(faceColor, outlineColor, sqrt(min(1.0, (outline * 2))));

			#if (UNDERLAY_ON | UNDERLAY_INNER)
			layerScale /= 1 + ((_UnderlaySoftness * _ScaleRatioC) * layerScale);
			float layerBias = (.5 - weight) * layerScale - .5 - ((_UnderlayDilate * _ScaleRatioC) * .5 * layerScale);

			float x = -(_UnderlayOffsetX * _ScaleRatioC) * _GradientScale / _TextureWidth;
			float y = -(_UnderlayOffsetY * _ScaleRatioC) * _GradientScale / _TextureHeight;
			float2 layerOffset = float2(x, y);
			#endif

			// Generate UV for the Masking Texture
			float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
			float2 maskUV = (vert.xy - clampedRect.xy) / (clampedRect.zw - clampedRect.xy);

			// Populate structure for pixel shader
			output.vertex = vPosition;
			output.faceColor = faceColor;
			output.outlineColor = outlineColor;
			output.texcoord0 = float4(input.texcoord0.x, input.texcoord0.y, maskUV.x, maskUV.y);
			output.param = half4(scale, bias - outline, bias + outline, bias);
			output.mask = half4(vert.xy * 2 - clampedRect.xy - clampedRect.zw, 0.25 / (0.25 * half2(_MaskSoftnessX, _MaskSoftnessY) + pixelSize.xy));
			#if (UNDERLAY_ON || UNDERLAY_INNER)
			output.texcoord1 = float4(input.texcoord0 + layerOffset, input.color.a, 0);
			output.underlayParam = half2(layerScale, layerBias);
			#endif

			return output;
		}


		// PIXEL SHADER
		fixed4 PixShader(pixel_t input) : SV_Target
		{
			UNITY_SETUP_INSTANCE_ID(input);

			half d = tex2D(_MainTex, input.texcoord0.xy).a * input.param.x;
			half4 c = input.faceColor * saturate(d - input.param.w);

			#ifdef OUTLINE_ON
			c = lerp(input.outlineColor, input.faceColor, saturate(d - input.param.z));
			c *= saturate(d - input.param.y);
			#endif

			#if UNDERLAY_ON
			d = tex2D(_MainTex, input.texcoord1.xy).a * input.underlayParam.x;
			c += float4(_UnderlayColor.rgb * _UnderlayColor.a, _UnderlayColor.a) * saturate(d - input.underlayParam.y) * (1 - c.a);
			#endif

			#if UNDERLAY_INNER
			half sd = saturate(d - input.param.z);
			d = tex2D(_MainTex, input.texcoord1.xy).a * input.underlayParam.x;
			c += float4(_UnderlayColor.rgb * _UnderlayColor.a, _UnderlayColor.a) * (1 - saturate(d - input.underlayParam.y)) * sd * (1 - c.a);
			#endif

			// Alternative implementation to UnityGet2DClipping with support for softness.
			#if UNITY_UI_CLIP_RECT
			half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(input.mask.xy)) * input.mask.zw);
			c *= m.x * m.y;
			#endif

			#if (UNDERLAY_ON | UNDERLAY_INNER)
			c *= input.texcoord1.z;
			#endif

			#if UNITY_UI_ALPHACLIP
			clip(c.a - 0.001);
			#endif

			return c;
		}
		ENDCG
	}
}

CustomEditor stats.TMPro.EditorUtilities.TMP_SDFShaderGUIstats.
}
