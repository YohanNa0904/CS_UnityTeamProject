Shader "TimProject/DragAlpha"
{
    Properties
    {
        _Color("Color",Color) = (1,1,1,1)
        _Alpha ("Alpha", range(0.0, 1.0)) = 1.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

       Pass
       {           
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag        
            
            #include "UnityCG.cginc"           

            struct appdata
            {
                float4 pos : POSITION;
            };

            struct v2f
            {               
                float4 vertex : SV_POSITION;                
            };           

            fixed4 _Color; 
            float _Alpha;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.pos);
                return o;
            }            

            fixed4 frag (v2f i) : SV_Target
            {                
                return fixed4(_Color.xyz,_Alpha);
            }
            ENDCG
        }
    }
}
