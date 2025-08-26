// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Urki/NoteShrinkCutout"
{
	Properties
	{
		_Color("Color", Color) = (0,0,0,0)
		_ColorIntensity("Color Intensity", Float) = 0
		_FogOffset("Fog Offset", Float) = 0
		_FogScale("Fog Scale", Float) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_CullMode("Cull Mode", Float) = 0
		_RimFresnel("Rim Fresnel", Float) = 0
		_RimIntensity("Rim Intensity", Float) = 0
		_Cutout("Cutout", Float) = 0
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_CutPlane("_CutPlane", Vector) = (0,0,0,0)
		[HideInInspector]_SlicePos("_SlicePos", Vector) = (0,0,0,0)
		[HideInInspector]_TransformOffset("_TransformOffset", Vector) = (0,0,0,0)
		[Toggle(_ENABLEPLANECUT_ON)] _EnablePlaneCut("Enable PlaneCut", Float) = 0
		[Toggle(_USEGLOWEDGE_ON)] _UseGlowEdge("Use Glow Edge", Float) = 0
		_SliceEdge("Slice Edge", Float) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull [_CullMode]
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 5.0
		#pragma shader_feature_local _USEGLOWEDGE_ON
		#pragma shader_feature_local _ENABLEPLANECUT_ON
		#include "slice.cginc"
		#pragma surface surf Standard keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float eyeDepth;
			float4 screenPos;
			float3 worldPos;
			float3 worldNormal;
			half ASEVFace : VFACE;
		};

		uniform float _Cutout;
		uniform float _CustomFogAttenuation;
		uniform float _FogOffset;
		uniform float _CustomFogOffset;
		uniform float _FogScale;
		uniform float _ColorIntensity;
		uniform float4 _Color;
		uniform float _CullMode;
		uniform float _CustomFogColorMultiplier;
		uniform float4 _CustomFogColor;
		uniform sampler2D _BloomPrePassTexture;
		uniform float _RimFresnel;
		uniform float _RimIntensity;
		uniform half3 _CutPlane;
		uniform half3 _SlicePos;
		uniform half3 _TransformOffset;
		uniform float _SliceEdge;
		uniform float _Metallic;
		uniform float _Smoothness;
		uniform float _Cutoff = 0.5;


		float2 MyCustomExpression136( float2 input )
		{
			#ifdef UNITY_UV_STARTS_AT_TOP
			{
			}
			#else
			{
				input.y = 1 - input.y;
			}
			#endif
			return input;
		}


		float3 MyCustomExpression122( half3 CutPlane, float3 SlicePos, float3 localpos, float3 TransformOffset, float SliceEdge )
		{
			return slice(CutPlane, SlicePos, localpos, TransformOffset, SliceEdge);
		}


		float3 MyCustomExpression101( half3 CutPlane, float3 SlicePos, float3 localpos, float3 TransformOffset, float SliceEdge )
		{
			return slice(CutPlane, SlicePos, localpos, TransformOffset, SliceEdge);
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			v.vertex.xyz = ( ase_vertex3Pos * ( 1.0 - _Cutout ) );
			v.vertex.w = 1;
			o.eyeDepth = -UnityObjectToViewPos( v.vertex.xyz ).z;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float clampResult84 = clamp( ( ( ( i.eyeDepth * _CustomFogAttenuation ) - ( _FogOffset + _CustomFogOffset ) ) / _FogScale ) , 0.0 , 1.0 );
			float temp_output_61_0 = ( 1.0 - clampResult84 );
			o.Albedo = ( temp_output_61_0 * ( ( _ColorIntensity * _Color ) + ( _CullMode * 0.0 ) ) ).rgb;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 input136 = ase_screenPosNorm.xy;
			float2 localMyCustomExpression136 = MyCustomExpression136( input136 );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV28 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode28 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV28, _RimFresnel ) );
			float3 CutPlane122 = _CutPlane;
			float3 SlicePos122 = _SlicePos;
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float3 localpos122 = ase_vertex3Pos;
			float3 TransformOffset122 = _TransformOffset;
			float SliceEdge122 = _SliceEdge;
			float3 localMyCustomExpression122 = MyCustomExpression122( CutPlane122 , SlicePos122 , localpos122 , TransformOffset122 , SliceEdge122 );
			#ifdef _USEGLOWEDGE_ON
				float3 staticSwitch123 = localMyCustomExpression122;
			#else
				float3 staticSwitch123 = float3( 0,0,0 );
			#endif
			o.Emission = ( ( ( ( _CustomFogColorMultiplier * _CustomFogColor ) + tex2D( _BloomPrePassTexture, localMyCustomExpression136 ) ) * clampResult84 ) + ( temp_output_61_0 * ( ( fresnelNode28 * _RimIntensity * _Color * i.ASEVFace ) + float4( staticSwitch123 , 0.0 ) ) ) ).xyz;
			o.Metallic = _Metallic;
			o.Smoothness = ( temp_output_61_0 * _Smoothness * ( 1.0 - fresnelNode28 ) * i.ASEVFace );
			float fresnelNdotV25 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode25 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV25, 1.0 ) );
			o.Occlusion = ( 1.0 - fresnelNode25 );
			o.Alpha = ( ( 0.0 + 0.0 ) + staticSwitch123 ).x;
			float3 CutPlane101 = _CutPlane;
			float3 SlicePos101 = _SlicePos;
			float3 localpos101 = ase_vertex3Pos;
			float3 TransformOffset101 = _TransformOffset;
			float SliceEdge101 = 0.0;
			float3 localMyCustomExpression101 = MyCustomExpression101( CutPlane101 , SlicePos101 , localpos101 , TransformOffset101 , SliceEdge101 );
			#ifdef _ENABLEPLANECUT_ON
				float3 staticSwitch115 = localMyCustomExpression101;
			#else
				float3 staticSwitch115 = float3( 0,0,0 );
			#endif
			clip( ( 1.0 - staticSwitch115 ).x - _Cutoff );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
0;0;2560;1379;485.3022;928.6901;1;False;False
Node;AmplifyShaderEditor.RangedFloatNode;76;-563.7146,-363.1763;Inherit;False;Property;_FogOffset;Fog Offset;2;0;Create;True;0;0;0;False;0;False;0;-1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SurfaceDepthNode;93;-622.9256,-523.9626;Inherit;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;85;-620.0281,-289.6024;Inherit;False;Global;_CustomFogOffset;_CustomFogOffset;7;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;77;-645.4195,-440.352;Inherit;False;Global;_CustomFogAttenuation;_CustomFogAttenuation;7;0;Create;True;0;0;0;False;0;False;0;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-310.0634,-504.9951;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;79;-338.0634,-393.9952;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;81;-159.7152,-407.1762;Inherit;False;Property;_FogScale;Fog Scale;3;0;Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;111;605.1307,384.3835;Half;False;Property;_SlicePos;_SlicePos;14;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;109;527.0057,243.8559;Half;False;Property;_CutPlane;_CutPlane;13;0;Create;True;0;0;0;False;0;False;0,0,0;1,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;113;567.1307,651.3835;Half;False;Property;_TransformOffset;_TransformOffset;15;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;82;-162.7152,-508.176;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;124;418.163,532.2213;Inherit;False;Property;_SliceEdge;Slice Edge;18;0;Create;True;0;0;0;False;0;False;0;-0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;112;579.1307,519.3835;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenPosInputsNode;94;-127.5002,-716.7184;Float;True;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;30;-680.0064,-111.1983;Inherit;False;Property;_RimFresnel;Rim Fresnel;7;0;Create;True;0;0;0;False;0;False;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;13;-134,-232.5;Inherit;False;Property;_Color;Color;0;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CustomExpressionNode;122;824.1307,428.3835;Inherit;False;return slice(CutPlane, SlicePos, localpos, TransformOffset, SliceEdge)@;3;Create;5;True;CutPlane;FLOAT3;0,0,0;In;;Half;False;True;SlicePos;FLOAT3;0,0,0;In;;Inherit;False;True;localpos;FLOAT3;0,0,0;In;;Inherit;False;True;TransformOffset;FLOAT3;0,0,0;In;;Inherit;False;True;SliceEdge;FLOAT;0;In;;Inherit;False;My Custom Expression;True;False;0;;False;5;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;83;19.28499,-477.1761;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FaceVariableNode;127;140.7861,78.07098;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;28;-490.755,-186.7006;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;87;453.463,-597.0853;Inherit;False;Global;_CustomFogColor;_CustomFogColor;13;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;88;382.4302,-672.2961;Inherit;False;Global;_CustomFogColorMultiplier;_CustomFogColorMultiplier;13;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CustomExpressionNode;136;191.3338,-594.1972;Inherit;False;#ifdef UNITY_UV_STARTS_AT_TOP${$}$#else${$	input.y = 1 - input.y@$}$#endif$return input@;2;Create;1;True;input;FLOAT2;0,0;In;;Inherit;False;My Custom Expression;True;False;0;;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;31;60.45898,9.048309;Inherit;False;Property;_RimIntensity;Rim Intensity;8;0;Create;True;0;0;0;False;0;False;0;0.25;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;86;681.882,-512.1245;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;59;446.9308,-422.3803;Inherit;True;Global;_BloomPrePassTexture;_BloomPrePassTexture;11;0;Create;True;0;0;0;False;0;False;-1;None;;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;294.459,-81.95169;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-135.541,-309.9517;Inherit;False;Property;_ColorIntensity;Color Intensity;1;0;Create;True;0;0;0;False;0;False;0;0.25;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-60,-62.5;Inherit;False;Property;_CullMode;Cull Mode;6;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;84;197.9641,-487.9978;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;123;1069.906,411.8683;Inherit;False;Property;_UseGlowEdge;Use Glow Edge;17;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;92.95556,-84.93723;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;126,-180.5;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CustomExpressionNode;101;823.7756,255.4483;Inherit;False;return slice(CutPlane, SlicePos, localpos, TransformOffset, SliceEdge)@;3;Create;5;True;CutPlane;FLOAT3;0,0,0;In;;Half;False;True;SlicePos;FLOAT3;0,0,0;In;;Inherit;False;True;localpos;FLOAT3;0,0,0;In;;Inherit;False;True;TransformOffset;FLOAT3;0,0,0;In;;Inherit;False;True;SliceEdge;FLOAT;0;In;;Inherit;False;My Custom Expression;True;False;0;;False;5;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;61;438.3156,-223.3914;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;628,56.5;Inherit;False;Constant;_Glow;Glow;5;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;35;68.459,252.0483;Inherit;False;Property;_Cutout;Cutout;9;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;853.3566,-77.57277;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;89;822.1223,-354.2389;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;1157.943,-114.3973;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;133;-78.81704,344.7915;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;25;-452.541,-7.451691;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;75;-222.2893,-80.82964;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;48;828.459,58.04828;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;128;-586.384,179.7076;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;1157.897,-207.6335;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StaticSwitch;115;1124.839,226.9963;Inherit;False;Property;_EnablePlaneCut;Enable PlaneCut;16;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;9;496.4667,-14.49533;Inherit;False;Property;_Smoothness;Smoothness;5;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;295,-172.5;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;8;493,-82.5;Inherit;False;Property;_Metallic;Metallic;4;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;130;-378.2069,165.1967;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;10,10,10;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;91;184.6527,-354.0413;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;73;48.64909,145.0354;Inherit;False;Simplex3D;True;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;631.3547,-189.2489;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;137;1415.903,189.396;Inherit;False;SliceEdge;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;147;1453.852,279.9886;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldToObjectTransfNode;129;-185.8464,64.04173;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;39;-268.3873,252.4044;Inherit;False;Property;_NoiseScale;Noise Scale;10;0;Create;True;0;0;0;False;0;False;5;2.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;997.2014,-51.99077;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;47;305.459,153.0483;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;132;110.3032,344.7914;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;642.0264,135.5546;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;51;251.6931,258.032;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.05;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;131;-105.1437,241.4774;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;116;1229.77,80.57619;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StepOpNode;52;474.468,131.5612;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;69;1325.094,-124.0133;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.OneMinusNode;26;-216.9782,-14.07725;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;24;1721.6,-133.122;Float;False;True;-1;7;ASEMaterialInspector;0;0;Standard;Urki/NoteShrinkCutout;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Opaque;;AlphaTest;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Absolute;0;;12;-1;-1;-1;0;False;0;0;True;2;-1;0;False;-1;1;Include;slice.cginc;False;;Custom;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;80;0;93;0
WireConnection;80;1;77;0
WireConnection;79;0;76;0
WireConnection;79;1;85;0
WireConnection;82;0;80;0
WireConnection;82;1;79;0
WireConnection;122;0;109;0
WireConnection;122;1;111;0
WireConnection;122;2;112;0
WireConnection;122;3;113;0
WireConnection;122;4;124;0
WireConnection;83;0;82;0
WireConnection;83;1;81;0
WireConnection;28;3;30;0
WireConnection;136;0;94;0
WireConnection;86;0;88;0
WireConnection;86;1;87;0
WireConnection;59;1;136;0
WireConnection;27;0;28;0
WireConnection;27;1;31;0
WireConnection;27;2;13;0
WireConnection;27;3;127;0
WireConnection;84;0;83;0
WireConnection;123;0;122;0
WireConnection;4;0;2;0
WireConnection;14;0;17;0
WireConnection;14;1;13;0
WireConnection;101;0;109;0
WireConnection;101;1;111;0
WireConnection;101;2;112;0
WireConnection;101;3;113;0
WireConnection;61;0;84;0
WireConnection;50;0;27;0
WireConnection;50;1;123;0
WireConnection;89;0;86;0
WireConnection;89;1;59;0
WireConnection;65;0;61;0
WireConnection;65;1;50;0
WireConnection;133;0;35;0
WireConnection;75;0;28;0
WireConnection;48;0;11;0
WireConnection;66;0;89;0
WireConnection;66;1;84;0
WireConnection;115;0;101;0
WireConnection;16;0;14;0
WireConnection;16;1;4;0
WireConnection;130;0;128;0
WireConnection;73;0;129;0
WireConnection;73;1;131;0
WireConnection;64;0;61;0
WireConnection;64;1;16;0
WireConnection;137;0;115;0
WireConnection;147;0;115;0
WireConnection;129;0;130;0
WireConnection;68;0;61;0
WireConnection;68;1;9;0
WireConnection;68;2;75;0
WireConnection;68;3;127;0
WireConnection;47;1;35;0
WireConnection;132;0;128;0
WireConnection;132;1;133;0
WireConnection;49;0;13;0
WireConnection;49;1;47;0
WireConnection;51;0;35;0
WireConnection;131;0;39;0
WireConnection;116;0;48;0
WireConnection;116;1;123;0
WireConnection;52;0;51;0
WireConnection;69;0;66;0
WireConnection;69;1;65;0
WireConnection;26;0;25;0
WireConnection;24;0;64;0
WireConnection;24;2;69;0
WireConnection;24;3;8;0
WireConnection;24;4;68;0
WireConnection;24;5;26;0
WireConnection;24;9;116;0
WireConnection;24;10;147;0
WireConnection;24;11;132;0
ASEEND*/
//CHKSM=E76F2E5EB96EEB5157592C93034A52B5E97D7781