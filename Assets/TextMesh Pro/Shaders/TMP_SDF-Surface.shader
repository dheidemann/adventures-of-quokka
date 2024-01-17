Shader stats.TextMeshPro/Distance Field (Surface)stats. {

Properties {
	_FaceTex			(stats.Fill Texturestats., 2D) = stats.whitestats. {}
	_FaceUVSpeedX		(stats.Face UV Speed Xstats., Range(-5, 5)) = 0.0
	_FaceUVSpeedY		(stats.Face UV Speed Ystats., Range(-5, 5)) = 0.0
	[HDR]_FaceColor		(stats.Fill Colorstats., Color) = (1,1,1,1)
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

	_BumpMap 			(stats.Normalmapstats., 2D) = stats.bumpstats. {}
	_BumpOutline		(stats.Bump Outlinestats., Range(0,1)) = 0.5
	_BumpFace			(stats.Bump Facestats., Range(0,1)) = 0.5

	_ReflectFaceColor	    (stats.Face Colorstats., Color) = (0,0,0,1)
	_ReflectOutlineColor	(stats.Outline Colorstats., Color) = (0,0,0,1)
	_Cube 					(stats.Reflection Cubemapstats., Cube) = stats.blackstats. { /* TexGen CubeReflect */ }
	_EnvMatrixRotation  	(stats.Texture Rotationstats., vector) = (0, 0, 0, 0)
	[HDR]_SpecColor		    (stats.Specular Colorstats., Color) = (0,0,0,1)

	_FaceShininess		(stats.Face Shininessstats., Range(0,1)) = 0
	_OutlineShininess	(stats.Outline Shininessstats., Range(0,1)) = 0

	[HDR]_GlowColor		(stats.Colorstats., Color) = (0, 1, 0, 0.5)
	_GlowOffset			(stats.Offsetstats., Range(-1,1)) = 0
	_GlowInner			(stats.Innerstats., Range(0,1)) = 0.05
	_GlowOuter			(stats.Outerstats., Range(0,1)) = 0.05
	_GlowPower			(stats.Falloffstats., Range(1, 0)) = 0.75

	_WeightNormal		(stats.Weight Normalstats., float) = 0
	_WeightBold			(stats.Weight Boldstats., float) = 0.5

	// Should not be directly exposed to the user
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

	_CullMode			(stats.Cull Modestats., Float) = 0
	//_MaskCoord		(stats.Mask Coordsstats., vector) = (0,0,0,0)
	//_MaskSoftness		(stats.Mask Softnessstats., float) = 0
}

SubShader {

	Tags { stats.Queuestats.=stats.Transparentstats. stats.IgnoreProjectorstats.=stats.Truestats. stats.RenderTypestats.=stats.Transparentstats. }

	LOD 300
	Cull [_CullMode]

	CGPROGRAM
	#pragma surface PixShader BlinnPhong alpha:blend vertex:VertShader nolightmap nodirlightmap
	#pragma target 3.0
	#pragma shader_feature __ GLOW_ON
	#pragma glsl

	#include stats.TMPro_Properties.cgincstats.
	#include stats.TMPro.cgincstats.

	half _FaceShininess;
	half _OutlineShininess;

	struct Input
	{
		fixed4	color			: COLOR;
		float2	uv_MainTex;
		float2	uv2_FaceTex;
		float2  uv2_OutlineTex;
		float2	param;						// Weight, Scale
		float3	viewDirEnv;
	};


	#define BEVEL_ON 1
	#include stats.TMPro_Surface.cgincstats.

	ENDCG

	// Pass to render object as a shadow caster
	Pass
	{
		Name stats.Casterstats.
		Tags { stats.LightModestats. = stats.ShadowCasterstats. }
		Offset 1, 1

		Fog {Mode Off}
		ZWrite On
		ZTest LEqual
		Cull Off

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile_shadowcaster
		#include stats.UnityCG.cgincstats.

		struct v2f {
			V2F_SHADOW_CASTER;
			float2	uv			: TEXCOORD1;
			float2	uv2			: TEXCOORD3;
			float	alphaClip	: TEXCOORD2;
		};

		uniform float4 _MainTex_ST;
		uniform float4 _OutlineTex_ST;
		float _OutlineWidth;
		float _FaceDilate;
		float _ScaleRatioA;

		v2f vert( appdata_base v )
		{
			v2f o;
			TRANSFER_SHADOW_CASTER(o)
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.uv2 = TRANSFORM_TEX(v.texcoord, _OutlineTex);
			o.alphaClip = (1.0 - _OutlineWidth * _ScaleRatioA - _FaceDilate * _ScaleRatioA) / 2;
			return o;
		}

		uniform sampler2D _MainTex;

		float4 frag(v2f i) : COLOR
		{
			fixed4 texcol = tex2D(_MainTex, i.uv).a;
			clip(texcol.a - i.alphaClip);
			SHADOW_CASTER_FRAGMENT(i)
		}
		ENDCG
	}
}

CustomEditor stats.TMPro.EditorUtilities.TMP_SDFShaderGUIstats.
}

