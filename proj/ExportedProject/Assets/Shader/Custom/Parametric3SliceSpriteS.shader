// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: upgraded instancing buffer 'UrkiParametric3SliceSpriteTest' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Urki/Parametric3SliceSpriteTest"
{
	Properties
	{
		_Color("Tint Color", Color) = (1,0,0,1)
		_SizeParams("Size Length and Center", Vector) = (0.2,2,0.5,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		ZWrite Off
		Stencil
		{
			Ref 0
			CompFront Always
			PassFront Keep
			FailFront Keep
			ZFailFront Keep
		}
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 5.0
		#pragma multi_compile_instancing
		#pragma surface surf Unlit keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			half filler;
		};

		UNITY_INSTANCING_BUFFER_START(UrkiParametric3SliceSpriteTest)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
#define _Color_arr UrkiParametric3SliceSpriteTest
			UNITY_DEFINE_INSTANCED_PROP(float3, _SizeParams)
#define _SizeParams_arr UrkiParametric3SliceSpriteTest
		UNITY_INSTANCING_BUFFER_END(UrkiParametric3SliceSpriteTest)


		float4 Nodecutthing( float4 worldPos, float3 camForward )
		{
			worldPos.xz += camForward.xz * worldPos.w;
			return UnityObjectToClipPos(worldPos);
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float4 ase_vertex4Pos = v.vertex;
			float3 _SizeParams_Instance = UNITY_ACCESS_INSTANCED_PROP(_SizeParams_arr, _SizeParams);
			float4 worldPos67 = float4( mul( unity_ObjectToWorld, float4( ( ( float3(1,0,0) * ( ase_vertex4Pos.x * _SizeParams_Instance.x ) ) + ( float3(0,1,0) * ( ( ase_vertex4Pos.y - _SizeParams_Instance.z ) * _SizeParams_Instance.y ) ) + ( float3(0,0,1) * ( _SizeParams_Instance.x * ase_vertex4Pos.z ) ) ) , 0.0 ) ).xyz , 0.0 );
			float4 normalizeResult56 = normalize( unity_WorldToCamera[2] );
			float3 camForward67 = normalizeResult56.xyz;
			float4 localNodecutthing67 = Nodecutthing( worldPos67 , camForward67 );
			v.vertex.xyz = localNodecutthing67.xyz;
			v.vertex.w = 1;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 _Color_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color_arr, _Color);
			o.Emission = ( _Color_Instance * _Color_Instance.a ).rgb;
			o.Alpha = ( 0.0 * 0.0 * _Color_Instance.a );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
0;25;1680;942;413.89;462.9951;1;True;True
Node;AmplifyShaderEditor.CommentaryNode;32;-1010.411,242.2914;Inherit;False;1214.404;497.7589;Vertex stuff;10;18;25;26;27;24;23;15;9;5;3;;1,1,1,1;0;0
Node;AmplifyShaderEditor.PosVertexDataNode;5;-955.1518,260.8454;Inherit;False;1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;3;-968.2684,488.2732;Inherit;False;InstancedProperty;_SizeParams;Size Length and Center;1;0;Create;False;0;0;0;False;0;False;0.2,2,0.5;1,1.03,0.5;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;9;-711.8937,406.579;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;14;-1100,-412;Inherit;False;542.8;235.6;Amplify component mask is retarded;3;11;20;19;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector3Node;19;-1060.796,-351.6589;Inherit;False;Constant;_Red;Red;2;0;Create;True;0;0;0;False;0;False;1,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-561.2072,391.7913;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-554.207,293.2913;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;20;-732.796,-348.6589;Inherit;False;Constant;_Blue;Blue;2;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;11;-893,-350;Inherit;False;Constant;_Green;Green;2;0;Create;True;0;0;0;False;0;False;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-555.207,488.2912;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-427.2071,388.2913;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-427.2071,292.2913;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldToCameraMatrix;63;-189.8904,43.00488;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-419.2071,487.2912;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;-234.2072,310.7913;Inherit;True;3;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.VectorFromMatrixNode;57;71.10962,49.00488;Inherit;False;Row;2;1;0;FLOAT4x4;1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TransformVariables;60;-207.8904,-48.99512;Inherit;False;_Object2World;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;71.10962,-50.99512;Inherit;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode;56;276.1096,53.00488;Inherit;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ColorNode;2;-52,-413.5;Inherit;False;InstancedProperty;_Color;Tint Color;0;0;Create;False;0;0;0;True;0;False;1,0,0,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CustomExpressionNode;67;438.1096,33.00488;Inherit;False;worldPos.xz += camForward.xz * worldPos.w@$return UnityObjectToClipPos(worldPos)@;4;Create;2;True;worldPos;FLOAT4;0,0,0,0;In;;Inherit;False;True;camForward;FLOAT3;0,0,0;In;;Inherit;False;Node cut thing;False;True;0;;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;194.204,-228.6589;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;31;-772.796,-567.1589;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;198.9498,-372.2058;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;939.205,-406.3875;Float;False;True;-1;7;ASEMaterialInspector;0;0;Unlit;Urki/Parametric3SliceSpriteTest;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Opaque;;AlphaTest;All;18;all;True;True;True;True;0;False;-1;True;0;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;1;False;-1;1;False;-1;0;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;False;Absolute;0;;2;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;0;5;2
WireConnection;9;1;3;3
WireConnection;15;0;9;0
WireConnection;15;1;3;2
WireConnection;23;0;5;1
WireConnection;23;1;3;1
WireConnection;24;0;3;1
WireConnection;24;1;5;3
WireConnection;26;0;11;0
WireConnection;26;1;15;0
WireConnection;25;0;19;0
WireConnection;25;1;23;0
WireConnection;27;0;20;0
WireConnection;27;1;24;0
WireConnection;18;0;25;0
WireConnection;18;1;26;0
WireConnection;18;2;27;0
WireConnection;57;0;63;0
WireConnection;61;0;60;0
WireConnection;61;1;18;0
WireConnection;56;0;57;0
WireConnection;67;0;61;0
WireConnection;67;1;56;0
WireConnection;29;2;2;4
WireConnection;35;0;2;0
WireConnection;35;1;2;4
WireConnection;0;2;35;0
WireConnection;0;9;29;0
WireConnection;0;11;67;0
ASEEND*/
//CHKSM=A4B5DAA43C74FA206BC774D34F9E0516E9B5498D