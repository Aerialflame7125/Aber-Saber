Shader "Custom/Urki/Parametric3SliceSprite" {
	Properties {
		_FogStartOffset ("Fog Start Offset", Float) = 1
		_FogScale ("Fog Scale", Float) = 1
		_CapUVSize ("Cap UV Size", Float) = 0.25
		_Offset ("Z Offset", Float) = 0
		[Space] _MainTex ("Main Texture", 2D) = "white" {}
		[Space] [Toggle(ENABLE_ANGLE_DISAPPEAR)] _AngleDisappear ("Angle Disappear", Float) = 10
		[Toggle(ENABLE_FOG)] _EnableFog ("Enable Fog", Float) = 1
		[Toggle(ENABLE_NOISE_DITHERING)] _EnableNoiseDithering ("Noise Dithering", Float) = 0
		[Toggle(ENABLE_Y_AXIS_BILLBOARD)] _EnableYAxisBillboard ("Y Axis Billboard", Float) = 1
		[Toggle(ENABLE_MAIN_EFFECT_WHITE_BOOST)] _EnableMainEffectWhiteBoost ("White Boost", Float) = 1
		[Space] [Enum(UnityEngine.Rendering.BlendMode)] _BlendSrcFactor ("Blend Src", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendDstFactor ("Blend Dst", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendSrcFactorA ("Blend Src Factor A", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendDstFactorA ("Blend Dst Factor A", Float) = 1
	}
	SubShader {
		Tags { "DisableBatching" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Tags { "DisableBatching" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend [_BlendSrcFactor] [_BlendDstFactor] ,[_BlendSrcFactorA] [_BlendDstFactorA]
			ZWrite Off
			Cull Off
			GpuProgramID 63089
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float2 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float _CapUVSize;
			// $Globals ConstantBuffers for Fragment Shader
			// Custom ConstantBuffers for Vertex Shader
			CBUFFER_START(Props)
				float4 _SizeParams;
			CBUFFER_END
			// Custom ConstantBuffers for Fragment Shader
			CBUFFER_START(Props)
				float4 _Color;
			CBUFFER_END
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
				v2f o;
				float4 tmp0; //u_xlat0
				float4 tmp1; //u_xlat1
				float4 tmp2; //u_xlat2
				float tmp0Dot; //u_xlat6
				float negativeZee; //u_xlat3
				float posY; //u_xlat9
				bool u_xlatb; //u_xlatb
				float uxlatButGood; //u_xlat7

				tmp0.xy = _WorldSpaceCameraPos.yy * unity_WorldToObject[1].xz;
				tmp0.xy = unity_WorldToObject[0].xz * _WorldSpaceCameraPos.xx + tmp0.xy;
				tmp0.xy = unity_WorldToObject[2].xz * _WorldSpaceCameraPos.zz + tmp0.xy;
				tmp0.xy = tmp0.xy + unity_ObjectToWorld[3].xz;
				tmp0Dot = dot(tmp0.xy, tmp0.xy);
				tmp0Dot = sqrt(tmp0Dot);
				tmp0.xy = tmp0.xy / tmp0Dot;
				tmp0.z = (-tmp0.y);
				tmp1.x = v.vertex.x * _SizeParams.x;
				tmp1.y = v.vertex.z;
				tmp0Dot = dot(tmp0.xz, tmp1.xy);
				tmp0.x = dot((-tmp0.yx), tmp1.xy);
				negativeZee = v.vertex.y + (-_SizeParams.z);
				posY = v.vertex.y - 0.5;
				tmp1.x = v.texcoord.y - 0.5;
				u_xlatb = abs(tmp1.x) > 0.49;
				uxlatButGood = u_xlatb ? 1.0 : 0.0;;
				posY *= uxlatButGood;
				posY *= _SizeParams.w;
				negativeZee = negativeZee * _SizeParams.y + posY;
				tmp2 = negativeZee + unity_ObjectToWorld[1];
				tmp2 = unity_ObjectToWorld[0] * tmp0.xxxx + tmp2;
				tmp0 = unity_ObjectToWorld[2] * tmp0Dot + tmp2;
				tmp2 = tmp0 + unity_ObjectToWorld[3];
				tmp0 = tmp2.yyyy * unity_MatrixVP[1];
				tmp0 = unity_MatrixVP[0] * tmp2.xxxx + tmp0;
				tmp0 = unity_MatrixVP[2] * tmp2.zzzz + tmp0;
				o.position = unity_MatrixVP[3] * tmp2.wwww + tmp0;
				o.texcoord1 = v.texcoord.xy;
				return o;

			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                tmp0 = tex2D(_MainTex, inp.texcoord1.xy);
                o.sv_target = tmp0 * _Color * tmp0.a;
                return o;
			}
			ENDCG
		}
	}
}