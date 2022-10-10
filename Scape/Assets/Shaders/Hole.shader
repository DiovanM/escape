Shader "Custom/3DHoleable" 
{
    Properties 
	{
        _BaseColor("Base Color", Color) = (1, 1, 1, 1)
		[IntRange] _StencilID ("Stencil ID", Range(0, 255)) = 0
    }

	SubShader 
	{
        Tags 
		{ 
			"RenderType" = "Transparent"
			"Queue" = "Geometry+1"
			"RenderPipeline" = "UniversalPipeline"
		}

		//ZWrite On	
		//ZTest Always

		Stencil 
		{
			Ref [_StencilID]
			Comp NotEqual
			Pass Keep
			Fail Keep
			ZFail Keep
		}

		Pass 
		{		
			
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"            

            struct Attributes
            {
                float4 positionOS   : POSITION;                 
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
            };

            // To make the Unity shader SRP Batcher compatible, declare all
            // properties related to a Material in a a single CBUFFER block with 
            // the name UnityPerMaterial.
            CBUFFER_START(UnityPerMaterial)
                // The following line declares the _BaseColor variable, so that you
                // can use it in the fragment shader.
                half4 _BaseColor;            
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                return OUT;
            }

            half4 frag() : SV_Target
            {
                // Returning the _BaseColor value.                
                return _BaseColor;
            }
            ENDHLSL
			
		}
	}
}