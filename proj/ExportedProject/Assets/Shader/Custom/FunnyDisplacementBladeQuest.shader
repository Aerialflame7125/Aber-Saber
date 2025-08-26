Shader "Custom/Urki/SaberBladeQuest" {
	Properties {
		_MainTex ("Displacement Texture", 2D) = "white" {}
		_DisplacementStrength ("Displacement Strength", Float) = 0.01
		[Space] [Enum(UnityEngine.Rendering.CullMode)] _CullMode ("CullMode", Float) = 0
		[Space] [Toggle(_ENABLE_INSTANCED_TintColor)] _EnableInstancedColor ("Enable Instanced Color", Float) = 0
		_TintColor ("Tint Color", Vector) = (1,1,1,1)
[MaterialToggle] _CustomColors("Custom Colors", Float) = 0
		_AddColorIntensity ("Add Color Intensity", Float) = 0
		[Space] [Toggle(_SCROLL_UV)] _ScrollUV ("Scroll UV", Float) = 0
		_ScrollUVVelocity ("Scroll UV Velocity", Vector) = (0,0,0,0)
		[Space] [Toggle(_ZWRITE)] _ZWrite ("Z Write", Float) = 0
		[Space] [Toggle(_USE_MONO_SCREEN_TEXTURE)] _UseMonoScreenTexture ("Use Mono Screen Texture", Float) = 0
		_LightRimScale ("Light Rim Scale", Float) = 0
		_LightRimOffset ("Light Rim Offset", Float) = 0
	}
	SubShader {
		Tags { "DisableBatching" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent+1" "RenderType" = "Transparent" }
		Pass {
			Tags { "DisableBatching" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent+1" "RenderType" = "Transparent" }
			ZWrite On
			Cull Back
			GpuProgramID 12770
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ ENABLE_BLOOM_FOG
			struct VertexInput {
                float4 vertex : POSITION;
            };
			#include "UnityCG.cginc"
			#include "BloomFog.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float2 texcoord : TEXCOORD0;
				float texcoord4 : TEXCOORD4;
				float4 projPos : TEXCOORD3;
				float4 texcoord2 : TEXCOORD2;
				BLOOM_FOG_COORDS(5, 6)

			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _MainTex_ST;
			float4 _ScrollUVVelocity;
			float _LightRimScale;
			float _LightRimOffset;
			// $Globals ConstantBuffers for Fragment Shader
			float4 _GrabTexture1_TexelSize;
			float4 _BloomPrePassTexture_TexelSize;
			float _DisplacementStrength;
			float4 _TintColor;
                        float _AddColorIntensity;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp0 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp1 = tmp0.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp1 = unity_MatrixVP._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp0.wwww + tmp1;
                o.position = UnityObjectToClipPos( v.vertex );
				o.projPos = ComputeScreenPos (o.position);
				COMPUTE_EYEDEPTH(o.projPos.z);
                tmp1.x = unity_WorldToObject._m10;
                tmp1.y = unity_WorldToObject._m11;
                tmp1.z = unity_WorldToObject._m12;
                tmp1.w = dot(tmp1.xyz, tmp1.xyz);
                tmp1.w = rsqrt(tmp1.w);
                tmp1.xyz = tmp1.www * tmp1.xyz;
                tmp1.w = dot(tmp1.xyz, unity_ObjectToWorld._m03_m13_m23);
                tmp2.x = dot(tmp1.xyz, _WorldSpaceCameraPos);
                tmp1.w = tmp2.x - tmp1.w;
                tmp1.xyz = -tmp1.www * tmp1.xyz + _WorldSpaceCameraPos;
                tmp1.xyz = tmp1.xyz - unity_ObjectToWorld._m03_m13_m23;
                tmp1.w = dot(tmp1.xyz, tmp1.xyz);
                tmp1.w = rsqrt(tmp1.w);
                tmp1.xyz = tmp1.www * tmp1.xyz;
                tmp2.x = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp2.y = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp2.z = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp1.w = dot(tmp2.xyz, tmp2.xyz);
                tmp1.w = rsqrt(tmp1.w);
                tmp2.xyz = tmp1.www * tmp2.xyz;
                tmp1.x = dot(tmp1.xyz, tmp2.xyz);
                tmp1.y = _LightRimOffset + 1.0;
                tmp1.x = tmp1.y - tmp1.x;
                o.texcoord4.x = saturate(tmp1.x * _LightRimScale);
                o.texcoord.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                tmp1.xyz = tmp0.xwy * float3(0.5, 0.5, -0.5);
                o.texcoord2.zw = tmp0.zw;
                o.texcoord2.xy = tmp1.yy + tmp1.xz;
				BLOOM_FOG_INITIALIZE(o, v.vertex);
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
				float4 tmp2;
				float2 UVTime;
				float2 sceneUVs = (inp.projPos.xy / inp.projPos.w);
				UVTime.x = _ScrollUVVelocity.x * _Time;
				UVTime.y = _ScrollUVVelocity.y * _Time;
                tmp2 = tex2D(_MainTex, (inp.texcoord.xy + UVTime));
                tmp2.xy = tmp2.xy - float2(0.5, 0.5);
				tmp2.xy = tmp2.xy * _BloomPrePassTexture_TexelSize.xy;
                tmp2.zw = inp.texcoord2.xy / inp.texcoord2.ww;
				//sceneUVs.y = 1 - sceneUVs.y;
                tmp0 = BLOOM_FOG_SAMPLE(inp, 0.0);
				tmp0.xyz = (tmp0.x + tmp0.y + tmp0.z) / 3;
				tmp0.w = 0;
                tmp0 = tmp0 * _TintColor + (_AddColorIntensity * _TintColor);
                tmp1 = _TintColor - tmp0;
                o.sv_target = inp.texcoord4.xxxx * tmp1 + tmp0;
                return o;
			}
			ENDCG
		}
	}
}