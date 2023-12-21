Shader "shader1"
{
	Properties
	{
		_Color("Color", color) = (1,1,0,1)
		_BaseMap("Base Map", 2D) = "while" {}
	}

	SubShader
	{
				
		Pass
		{
			HLSLPROGRAM
			
			#pragma vertex Vertex
			#pragma fragment Pixel

			half4 _Color;
			sampler2D _BaseMap;

			//#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			//#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

			struct Attributes
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD;
				float3 normal : NORMAL;
				
			};

			struct Varyings
			{
				float4 positionCS : SV_POSITION;
				float2 uv : TEXCOORD;
				float3 normalWS : NORMAL;
				float3 positionWS : TEXCOORD1;
			};


			Varyings Vertex(Attributes IN)
			{
				Varyings OUT;
				OUT.positionCS = TransformObjectToHClip(IN.vertex.xyz);
				OUT.normalWS = TransformObjectToWorldNormal(IN.normal);
				OUT.uv = IN.uv;
				OUT.positionWS = mul(UNITY_MATRIX_M, float4(IN.vertex.xyz, 1.0));
				// float4 positionCS = mul(UNITY_MATRIX_VP,float4(positionWS.xyz, 1.0));
				// return positionCS;
				return OUT;
			}
			half4 Pixel(Varyings IN) : SV_TARGET
			{
				half4 color;
				// float3 normalWS = normalize(IN.normalWS);
				float3 viewDir = -normalize(_WorldSpaceCameraPos.xyz - IN.positionWS);
				Light light = GetMainLight();
				float3 refDir = reflect(light.direction, IN.normalWS);
				float NOL = max(0, dot(IN.normalWS, light.direction));
				float sepc = max(0,dot(viewDir, refDir));														
				sepc = pow(sepc, 32);
				
				half3 gi = SampleSH(IN.normalWS) * 0.02;
				color.rgb = tex2D(_BaseMap, IN.uv).rgb * _Color * NOL * light.color + gi;
				color.a = 1.0;
				return color;
			}

			ENDHLSL
		}
	}

}
