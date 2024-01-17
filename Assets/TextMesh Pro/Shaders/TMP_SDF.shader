Shader stats.TextMeshPro/Distance Fieldstats. {

Properties {
	_FaceTex			(stats.Face Texturestats., 2D) = stats.whitestats. {}
	_FaceUVSpeedX		(stats.Face UV Speed Xstats., Range(-5, 5)) = 0.0
	_FaceUVSpeedY		(stats.Face UV Speed Ystats., Range(-5, 5)) = 0.0
	[HDR]_FaceColor		(stats.Face Colorstats., Color) = (1,1,1,1)
	_FaceDilate			(stats.Face Dilatestats., Range(-1,1)) = 0

	[HDR]_OutlineColor	(stats.Outline Colorstats., Color) = (0,0,0,1)
	_OutlineTex			(stats.Outline Texturestats., 2D) = stats.whitestats. {}
	_OutlineUVSpeedX	(stats.Outline UV Speed Xstats., Range(-5, 5)) = 0.0
	_OutlineUVSpeedY	(stats.Outline UV Speed Ystats., Range(-5, 5)) = 0.0
	_OutlineWidth		(stats.Outline Thicknessstats., Range(0, 1)) = 0
	_OutlineSoftness	(stats.Outline Softnessstats., Range(0,1)) = 0

	_Bevel				(stats.Bevelstats., Range(0,1)) = 0.5
	_BevelOffset		(stats.Bevel Offsetstats., Range(-0.5,0.5)) = 0
	_BevelWidth			(stats.Bevel Widthstats., Range(-.5,0.5)) = 0
	_BevelClamp			(stats.Bevel Clampstats., Range(0,1)) = 0
	_BevelRoundness		(stats.Bevel Roundnessstats., Range(0,1)) = 0

	_LightAngle			(stats.Light Anglestats., Range(0.0, 6.2831853)) = 3.1416
	[HDR]_SpecularColor	(stats.Specularstats., Color) = (1,1,1,1)
	_SpecularPower		(stats.Specularstats., Range(0,4)) = 2.0
	_Reflectivity		(stats.Reflectivitystats., Range(5.0,15.0)) = 10
	_Diffuse			(stats.Diffusestats., Range(0,1)) = 0.5
	_Ambient			(stats.Ambientstats., Range(1,0)) = 0.5

	_BumpMap 			(stats.Normal mapstats., 2D) = stats.bumpstats. {}
	_BumpOutline		(stats.Bump Outlinestats., Range(0,1)) = 0
	_BumpFace			(stats.Bump Facestats., Range(0,1)) = 0

	_ReflectFaceColor	(stats.Reflection Colorstats., Color) = (0,0,0,1)
	_ReflectOutlineColor(stats.Reflection Colorstats., Color) = (0,0,0,1)
	_Cube 				(stats.Reflection Cubemapstats., Cube) = stats.blackstats. { /* TexGen CubeReflect */ }
	_EnvMatrixRotation	(stats.Texture Rotationstats., vector) = (0, 0, 0, 0)


	[HDR]_UnderlayColor	(stats.Border Colorstats., Color) = (0,0,0, 0.5)
	_UnderlayOffsetX	(stats.Border OffsetXstats., Range(-1,1)) = 0
	_UnderlayOffsetY	(stats.Border OffsetYstats., Range(-1,1)) = 0
	_UnderlayDilate		(stats.Border Dilatestats., Range(-1,1)) = 0
	_UnderlaySoftness	(stats.Border Softnessstats., Range(0,1)) = 0

	[HDR]_GlowColor			(stats.Colorstats., Color) = (0, 1, 0, 0.5)
	_GlowOffset			(stats.Offsetstats., Range(-1,1)) = 0
	_GlowInner			(stats.Innerstats., Range(0,1)) = 0.05
	_GlowOuter			(stats.Outerstats., Range(0,1)) = 0.05
	_GlowPower			(stats.Falloffstats., Range(1, 0)) = 0.75

	_WeightNormal		(stats.Weight Normalstats., float) = 0
	_WeightBold			(stats.Weight Boldstats., float) = 0.5

	_ShaderFlags		(stats.Flagsstats., float) = 0
	_ScaleRatioA		(stats.Scale RatioAstats., float) = 1
	_ScaleRatioB		(stats.Scale RatioBstats., float) = 1
	_ScaleRatioC		(stats.Scale RatioCstats., float) = 1

	_MainTex			(stats.Font Atlasstats., 2D) = stats.whitestats. {}
	_TextureWidth		(stats.Texture Widthstats., float) = 512
	_TextureHeight		(stats.Texture Heightstats., float) = 512
	_GradientScale		(stats.Gradient Scalestats., float) = 5.0
	_ScaleX				(stats.Scale Xstats., float) = 1.0
	_ScaleY				(stats.Scale Ystats., float) = 1.0
	_PerspectiveFilter	(stats.Perspective Correctionstats., Range(0, 1)) = 0.875
	_Sharpness			(stats.Sharpnessstats., Range(-1,1)) = 0

	_VertexOffsetX		(stats.Vertex OffsetXstats., float) = 0
	_VertexOffsetY		(stats.Vertex OffsetYstats., float) = 0

	_MaskCoord			(stats.Mask Coordinatesstats., vector) = (0, 0, 32767, 32767)
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
		#pragma target 3.0
		#pragma vertex VertShader
		#pragma fragment PixShader
		#pragma shader_feature __ BEVEL_ON
		#pragma shader_feature __ UNDERLAY_ON UNDERLAY_INNER
		#pragma shader_feature __ GLOW_ON

		#pragma multi_compile __ UNITY_UI_CLIP_RECT
		#pragma multi_compile __ UNITY_UI_ALPHACLIP

		#include stats.UnityCG.cgincstats.
		#include stats.UnityUI.cgincstats.
		#include stats.TMPro_Properties.cgincstats.
		#include stats.TMPro.cgincstats.

		struct vertex_t {
			UNITY_VERTEX_INPUT_INSTANCE_ID
			float4	position		: POSITION;
			float3	normal			: NORMAL;
			fixed4	color			: COLOR;
			float2	texcoord0		: TEXCOORD0;
			float2	texcoord1		: TEXCOORD1;
		};


		struct pixel_t {
			UNITY_VERTEX_INPUT_INSTANCE_ID
			UNITY_VERTEX_OUTPUT_STEREO
			float4	position		: SV_POSITION;
			fixed4	color			: COLOR;
			float2	atlas			: TEXCOORD0;		// Atlas
			float4	param			: TEXCOORD1;		// alphaClip, scale, bias, weight
			float4	mask			: TEXCOORD2;		// Position in object space(xy), pixel Size(zw)
			float3	viewDir			: TEXCOORD3;

		#if (UNDERLAY_ON || UNDERLAY_INNER)
			float4	texcoord2		: TEXCOORD4;		// u,v, scale, bias
			fixed4	underlayColor	: COLOR1;
		#endif
			float4 textures			: TEXCOORD5;
		};

		// Used by Unity internally to handle Texture Tiling and Offset.
		float4 _FaceTex_ST;
		float4 _OutlineTex_ST;

		pixel_t VertShader(vertex_t input)
		{
			pixel_t output;

			UNITY_INITIALIZE_OUTPUT(pixel_t, output);
			UNITY_SETUP_INSTANCE_ID(input);
			UNITY_TRANSFER_INSTANCE_ID(input,output);
			UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

			float bold = step(input.texcoord1.y, 0);

			float4 vert = input.position;
			vert.x += _VertexOffsetX;
			vert.y += _VertexOffsetY;

			float4 vPosition = UnityObjectToClipPos(vert);

			float2 pixelSize = vPosition.w;
			pixelSize /= float2(_ScaleX, _ScaleY) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));
			float scale = rsqrt(dot(pixelSize, pixelSize));
			scale *= abs(input.texcoord1.y) * _GradientScale * (_Sharpness + 1);
			if (UNITY_MATRIX_P[3][3] == 0) scale = lerp(abs(scale) * (1 - _PerspectiveFilter), scale, abs(dot(UnityObjectToWorldNormal(input.normal.xyz), normalize(WorldSpaceViewDir(vert)))));

			float weight = lerp(_WeightNormal, _WeightBold, bold) / 4.0;
			weight = (weight + _FaceDilate) * _ScaleRatioA * 0.5;

			float bias =(.5 - weight) + (.5 / scale);

			float alphaClip = (1.0 - _OutlineWidth * _ScaleRatioA - _OutlineSoftness * _ScaleRatioA);

		#if GLOW_ON
			alphaClip = min(alphaClip, 1.0 - _GlowOffset * _ScaleRatioB - _GlowOuter * _ScaleRatioB);
		#endif

			alphaClip = alphaClip / 2.0 - ( .5 / scale) - weight;

		#if (UNDERLAY_ON || UNDERLAY_INNER)
			float4 underlayColor = _UnderlayColor;
			underlayColor.rgb *= underlayColor.a;

			float bScale = scale;
			bScale /= 1 + ((_UnderlaySoftness*_ScaleRatioC) * bScale);
			float bBias = (0.5 - weight) * bScale - 0.5 - ((_UnderlayDilate * _ScaleRatioC) * 0.5 * bScale);

			float x = -(_UnderlayOffsetX * _ScaleRatioC) * _GradientScale / _TextureWidth;
			float y = -(_UnderlayOffsetY * _ScaleRatioC) * _GradientScale / _TextureHeight;
			float2 bOffset = float2(x, y);
		#endif

			// Generate UV for the Masking Texture
			float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
			float2 maskUV = (vert.xy - clampedRect.xy) / (clampedRect.zw - clampedRect.xy);

			// Support for texture tiling and offset
			float2 textureUV = UnpackUV(input.texcoord1.x);
			float2 faceUV = TRANSFORM_TEX(textureUV, _FaceTex);
			float2 outlineUV = TRANSFORM_TEX(textureUV, _OutlineTex);


			output.position = vPosition;
			output.color = input.color;
			output.atlas =	input.texcoord0;
			output.param =	float4(alphaClip, scale, bias, weight);
			output.mask = half4(vert.xy * 2 - clampedRect.xy - clampedRect.zw, 0.25 / (0.25 * half2(_MaskSoftnessX, _MaskSoftnessY) + pixelSize.xy));
			output.viewDir =	mul((float3x3)_EnvMatrix, _WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, vert).xyz);
			#if (UNDERLAY_ON || UNDERLAY_INNER)
			output.texcoord2 = float4(input.texcoord0 + bOffset, bScale, bBias);
			output.underlayColor =	underlayColor;
			#endif
			output.textures = float4(faceUV, outlineUV);

			return output;
		}


		fixed4 PixShader(pixel_t input) : SV_Target
		{
			UNITY_SETUP_INSTANCE_ID(input);

			float c = tex2D(_MainTex, input.atlas).a;

		#ifndef UNDERLAY_ON
			clip(c - input.param.x);
		#endif

			float	scale	= input.param.y;
			float	bias	= input.param.z;
			float	weight	= input.param.w;
			float	sd = (bias - c) * scale;

			float outline = (_OutlineWidth * _ScaleRatioA) * scale;
			float softness = (_OutlineSoftness * _ScaleRatioA) * scale;

			half4 faceColor = _FaceColor;
			half4 outlineColor = _OutlineColor;

			faceColor.rgb *= input.color.rgb;

			faceColor *= tex2D(_FaceTex, input.textures.xy + float2(_FaceUVSpeedX, _FaceUVSpeedY) * _Time.y);
			outlineColor *= tex2D(_OutlineTex, input.textures.zw + float2(_OutlineUVSpeedX, _OutlineUVSpeedY) * _Time.y);

			faceColor = GetColor(sd, faceColor, outlineColor, outline, softness);

		#if BEVEL_ON
			float3 dxy = float3(0.5 / _TextureWidth, 0.5 / _TextureHeight, 0);
			float3 n = GetSurfaceNormal(input.atlas, weight, dxy);

			float3 bump = UnpackNormal(tex2D(_BumpMap, input.textures.xy + float2(_FaceUVSpeedX, _FaceUVSpeedY) * _Time.y)).xyz;
			bump *= lerp(_BumpFace, _BumpOutline, saturate(sd + outline * 0.5));
			n = normalize(n- bump);

			float3 light = normalize(float3(sin(_LightAngle), cos(_LightAngle), -1.0));

			float3 col = GetSpecular(n, light);
			faceColor.rgb += col*faceColor.a;
			faceColor.rgb *= 1-(dot(n, light)*_Diffuse);
			faceColor.rgb *= lerp(_Ambient, 1, n.z*n.z);

			fixed4 reflcol = texCUBE(_Cube, reflect(input.viewDir, -n));
			faceColor.rgb += reflcol.rgb * lerp(_ReflectFaceColor.rgb, _ReflectOutlineColor.rgb, saturate(sd + outline * 0.5)) * faceColor.a;
		#endif

		#if UNDERLAY_ON
			float d = tex2D(_MainTex, input.texcoord2.xy).a * input.texcoord2.z;
			faceColor += input.underlayColor * saturate(d - input.texcoord2.w) * (1 - faceColor.a);
		#endif

		#if UNDERLAY_INNER
			float d = tex2D(_MainTex, input.texcoord2.xy).a * input.texcoord2.z;
			faceColor += input.underlayColor * (1 - saturate(d - input.texcoord2.w)) * saturate(1 - sd) * (1 - faceColor.a);
		#endif

		#if GLOW_ON
			float4 glowColor = GetGlowColor(sd, scale);
			faceColor.rgb += glowColor.rgb * glowColor.a;
		#endif

		// Alternative implementation to UnityGet2DClipping with support for softness.
		#if UNITY_UI_CLIP_RECT
			half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(input.mask.xy)) * input.mask.zw);
			faceColor *= m.x * m.y;
		#endif

		#if UNITY_UI_ALPHACLIP
			clip(faceColor.a - 0.001);
		#endif

  		return faceColor * input.color.a;
		}

		ENDCG
	}
}

Fallback stats.TextMeshPro/Mobile/Distance Fieldstats.
CustomEditor stats.TMPro.EditorUtilities.TMP_SDFShaderGUIstats.
}
