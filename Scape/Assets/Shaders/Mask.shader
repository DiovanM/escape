Shader "Custom/3DMask" 
{
    Properties 
	{
		[IntRange] _StencilID ("Stencil ID", Range(0, 255)) = 0
    }

	SubShader 
	{
        Tags 
		{ 
			"RenderType" = "Opaque"
			"Queue" = "Geometry+1"
			"RenderPipeline" = "UniversalPipeline"
		}
        ZWrite Off

		Stencil 
		{
			Ref [_StencilID]
			Comp Always
			Pass Replace
			//Fail Keep
			//ZFail Keep
		}

		ColorMask 0

		Pass {}
        Pass
		{
			Tags 
			{
				"LightMode"="ShadowCaster"
			} 
		}
	}
}