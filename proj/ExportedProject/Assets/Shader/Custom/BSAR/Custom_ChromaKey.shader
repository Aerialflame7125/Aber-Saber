Shader "Custom/ChromaKey" {
	Properties {
		[HideInInspector] _MainTex ("Main Texture", 2D) = "white" {}
		_KeyColor ("Keying Color", Vector) = (0,1,1,0)
		_Threshold ("Threshold", Float) = 0.1
		_Smoothness ("Smoothness", Float) = 0.1
		[HideInInspector] _ScreenAspect ("Screen Aspect Ratio", Float) = 1.7777
		[HideInInspector] _Scale ("Scale", Float) = 1
		[KeywordEnum(Default, Left, Right)] [HideInInspector] _Orientation ("Orientation", Float) = 0
	}
	SubShader {
		Tags { "QUEUE" = "Transparent+20" "RenderType" = "Transparent" }
		Pass {
			Tags { "QUEUE" = "Transparent+20" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, One One
			ZWrite Off
			Cull Off
			GpuProgramID 2264
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float4 texcoord : TEXCOORD0;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			float4 _MainTex_TexelSize;
			float4 _KeyColor;
			float _Threshold;
			float _Smoothness;
			float _Scale;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			
			// Keywords: _ORIENTATION_DEFAULT
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp0 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp1 = tmp0.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp1 = unity_MatrixVP._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp0.wwww + tmp1;
                o.position = tmp0;
                tmp0.y = tmp0.y * _ProjectionParams.x;
                tmp1.xzw = tmp0.xwy * float3(0.5, 0.5, 0.5);
                o.texcoord.zw = tmp0.zw;
                o.texcoord.xy = tmp1.zz + tmp1.xw;
                return o;
			}
			// Keywords: _ORIENTATION_DEFAULT
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                float4 tmp4;
                float4 tmp5;
                tmp0.xyz = -_MainTex_TexelSize.xyx;
                tmp1.xy = inp.texcoord.xy / inp.texcoord.ww;
                tmp1.z = -tmp1.y;
                tmp1.xy = tmp1.xz + float2(-0.5, 0.5);
                tmp2 = tmp1.xyxy * _Scale.xxxx + float4(0.5, 0.5, 0.0, 0.5);
                tmp0.xy = tmp0.xy + tmp2.xy;
                tmp3 = tex2D(_MainTex, tmp0.xy);
                tmp0.x = dot(tmp3.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp0.xy = tmp3.zx - tmp0.xx;
                tmp1.z = dot(_KeyColor.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp1.zw = _KeyColor.zx - tmp1.zz;
                tmp1.zw = tmp1.zw * float2(0.5647, 0.7132);
                tmp0.xy = tmp0.xy * float2(0.5647, 0.7132) + -tmp1.zw;
                tmp0.x = dot(tmp0.xy, tmp0.xy);
                tmp3 = tex2D(_MainTex, tmp2.xy);
                tmp0.y = dot(tmp3.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp4.xy = tmp3.zx - tmp0.yy;
                tmp4.xy = tmp4.xy * float2(0.5647, 0.7132) + -tmp1.zw;
                tmp0.y = dot(tmp4.xy, tmp4.xy);
                tmp0.xy = sqrt(tmp0.xy);
                tmp0.x = tmp0.x + tmp0.y;
                tmp4 = _MainTex_TexelSize * float4(-1.0, 1.0, 1.0, -1.0) + tmp2.xyxy;
                tmp5 = tex2D(_MainTex, tmp4.xy);
                tmp4 = tex2D(_MainTex, tmp4.zw);
                tmp0.y = dot(tmp5.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp5.xy = tmp5.zx - tmp0.yy;
                tmp5.xy = tmp5.xy * float2(0.5647, 0.7132) + -tmp1.zw;
                tmp0.y = dot(tmp5.xy, tmp5.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp0.y = dot(tmp4.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp4.xy = tmp4.zx - tmp0.yy;
                tmp4.xy = tmp4.xy * float2(0.5647, 0.7132) + -tmp1.zw;
                tmp0.y = dot(tmp4.xy, tmp4.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp2.xy = tmp2.xy + _MainTex_TexelSize.xy;
                tmp4 = tex2D(_MainTex, tmp2.xy);
                tmp0.y = dot(tmp4.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp2.xy = tmp4.zx - tmp0.yy;
                tmp2.xy = tmp2.xy * float2(0.5647, 0.7132) + -tmp1.zw;
                tmp0.y = dot(tmp2.xy, tmp2.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp4.y = tmp1.y * _Scale;
                tmp4.zw = tmp1.xx * _Scale.xx + float2(0.5, 0.5);
                tmp0.w = 0.5;
                tmp0.yz = tmp0.zw + tmp4.zy;
                tmp5 = tex2D(_MainTex, tmp0.yz);
                tmp0.y = dot(tmp5.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp0.yz = tmp5.zx - tmp0.yy;
                tmp0.yz = tmp0.yz * float2(0.5647, 0.7132) + -tmp1.zw;
                tmp0.y = dot(tmp0.xy, tmp0.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp5.x = _MainTex_TexelSize.x;
                tmp5.yz = float2(0.5, 0.5);
                tmp0.yz = tmp4.wy + tmp5.xy;
                tmp4 = tex2D(_MainTex, tmp0.yz);
                tmp0.y = dot(tmp4.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp0.yz = tmp4.zx - tmp0.yy;
                tmp0.yz = tmp0.yz * float2(0.5647, 0.7132) + -tmp1.zw;
                tmp0.y = dot(tmp0.xy, tmp0.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp5.w = -_MainTex_TexelSize.y;
                tmp0.yz = tmp2.zw + tmp5.zw;
                tmp4 = tex2D(_MainTex, tmp0.yz);
                tmp0.y = dot(tmp4.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp0.yz = tmp4.zx - tmp0.yy;
                tmp0.yz = tmp0.yz * float2(0.5647, 0.7132) + -tmp1.zw;
                tmp0.y = dot(tmp0.xy, tmp0.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp1.x = 0.5;
                tmp1.y = _MainTex_TexelSize.y;
                tmp0.yz = tmp1.xy + tmp2.zw;
                tmp2 = tex2D(_MainTex, tmp0.yz);
                tmp0.y = dot(tmp2.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp0.yz = tmp2.zx - tmp0.yy;
                tmp0.yz = tmp0.yz * float2(0.5647, 0.7132) + -tmp1.zw;
                tmp0.y = dot(tmp0.xy, tmp0.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp0.x = tmp0.x * 0.1111111 + -_Threshold;
                tmp0.y = 1.0 / _Smoothness;
                tmp0.x = saturate(tmp0.y * tmp0.x);
                tmp0.y = tmp0.x * -2.0 + 3.0;
                tmp0.x = tmp0.x * tmp0.x;
                tmp0.x = tmp0.x * tmp0.y;
                o.sv_target.w = tmp0.x * tmp3.w;
                o.sv_target.xyz = tmp3.xyz;
                return o;
			}
			ENDCG
		}
	}
}