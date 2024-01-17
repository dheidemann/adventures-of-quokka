Shader stats.TextMeshPro/Spritestats.
{
	Properties
	{
        [PerRendererData] _MainTex (stats.Sprite Texturestats., 2D) = stats.whitestats. {}
		_Color (stats.Tintstats., Color) = (1,1,1,1)
		
		_StencilComp (stats.Stencil Comparisonstats., Float) = 8
		_Stencil (stats.Stencil IDstats., Float) = 0
		_StencilOp (stats.Stencil Operationstats., Float) = 0
		_StencilWriteMask (stats.Stencil Write Maskstats., Float) = 255
		_StencilReadMask (stats.Stencil Read Maskstats., Float) = 255
		
		_CullMode (stats.Cull Modestats., Float) = 0
		_ColorMask (stats.Color Maskstats., Float) = 15
		_ClipRect (stats.Clip Rectstats., vector) = (-32767, -32767, 32767, 32767)

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip (stats.Use Alpha Clipstats., Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			stats.Queuestats.=stats.Transparentstats. 
			stats.IgnoreProjectorstats.=stats.Truestats. 
			stats.RenderTypestats.=stats.Transparentstats. 
			stats.PreviewTypestats.=stats.Planestats.
			stats.CanUseSpriteAtlasstats.=stats.Truestats.
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
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		Pass
		{
            Name stats.Defaultstats.
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma target 2.0

			#include stats.UnityCG.cgincstats.
			#include stats.UnityUI.cgincstats.

            #pragma multi_compile __ UNITY_UI_CLIP_RECT
            #pragma multi_compile __ UNITY_UI_ALPHACLIP
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
			};
			
            sampler2D _MainTex;
			fixed4 _Color;
			fixed4 _TextureSampleAdd;
			float4 _ClipRect;
            float4 _MainTex_ST;

            v2f vert(appdata_t v)
			{
				v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				
                OUT.color = v.color * _Color;
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
				
                #ifdef UNITY_UI_CLIP_RECT
					color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
				#endif

				#ifdef UNITY_UI_ALPHACLIP
					clip (color.a - 0.001);
				#endif

				return color;
			}
		ENDCG
		}
	}
}
