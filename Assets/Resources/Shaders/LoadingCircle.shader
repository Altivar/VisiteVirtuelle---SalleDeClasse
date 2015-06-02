Shader "Custom/CircleShader" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
		_Angle ("Angle (Float)", Float) = 0
		_Color ("Tint", Color) = (1.0, 0.6, 0.6, 1.0)
    }

    SubShader {        
	Tags{"Queue" = "Overlay" "RenderType"="Transparent"}
	Pass {
			Blend SrcAlpha OneMinusSrcAlpha 
//			Tags { "Queue"="Opaque" "RenderType"="Opaque" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            uniform sampler2D _MainTex;
            float4 _MainTex_ST;
			uniform float _Angle;
			uniform fixed4 _Color;

			void vert(float4 v:POSITION, inout float2 uv:TEXCOORD0, out float4 sv:SV_POSITION)
	      	{
	      		uv = TRANSFORM_TEX (uv, _MainTex);
	            sv = mul (UNITY_MATRIX_MVP, v);
	        }

	        void frag(float2 uv:TEXCOORD0 ,out float4 Color:COLOR, out float Depth:DEPTH)
	        {
	        	Color = tex2D(_MainTex, uv);
	        	float angle = atan2(uv.x-0.5f, uv.y-0.5f);
		
				if(angle > _Angle)
				{				
					Color.a = 0;
				}
				Color *= _Color;
				
	        	Depth = 0.0f;
	        }

//            float4 frag(v2f_img i) : COLOR 
//			{		 
//				float4 result = tex2D(_MainTex, i.uv);
//				float angle = atan2(i.uv.x-0.5 , i.uv.y-0.5);
//		
//				if(angle > _Angle)
//				{				
//					result.a = 0;
//				}
//
//                return result*_Color;  
//			}
            ENDCG
        }
    }
}