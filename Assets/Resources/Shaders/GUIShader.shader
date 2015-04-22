Shader "Custom/GUIShader" {
	
	Properties {
      _Color ("Color", Color) = (0,0,0,1)
      _Alpha ("Alpha", Range(0,1)) = 0
   	}
   	SubShader {
   	
   		Tags{"Queue" = "Transparent" }
   	
      Pass {    
         Cull Off // since the front is partially transparent, 
            // we shouldn't cull the back
 
         Blend SrcAlpha OneMinusSrcAlpha 
         CGPROGRAM
         #pragma vertex vert_img
         #pragma fragment frag

         #include "UnityCG.cginc"
 
         uniform fixed4 _Color;    
         uniform half _Alpha;
 
         struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 tex : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
            output.tex = input.texcoord;
            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
            float4 textureColor = float4(_Color.rgb, _Alpha);  
            return textureColor;
         }
 
         ENDCG
      }
   }
 
   // The definition of a fallback shader should be commented out 
   // during development:
   // Fallback "Unlit/Transparent Cutout"
}
