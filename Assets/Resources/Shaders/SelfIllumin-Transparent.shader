Shader "Custom/SelfIllumin-Transparent" {
	Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Alpha ("Alpha", Range(0,1)) = 1
    }

    SubShader {        
	Tags{"Queue" = "Transparent"}
	Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite On
			ZTest Always
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            uniform sampler2D _MainTex;
            uniform float _Alpha;
			uniform fixed4 _Color;
			
			void vert(float4 v:POSITION, out float4 sv:SV_POSITION)
			{
				sv = mul (UNITY_MATRIX_MVP, v);
			}

            float4 frag(v2f_img i) : COLOR 
			{		 
				float4 result = tex2D(_MainTex, i.uv);			
				result.a = _Alpha;

                return result;  
			}
            ENDCG
        }
    }
}
