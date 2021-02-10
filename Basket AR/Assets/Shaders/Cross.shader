// Shader created with Shader Forge v1.41 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Enhanced by Antoine Guillon / Arkham Development - http://www.arkham-development.com/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.41;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:7775,x:33912,y:32112,varname:node_7775,prsc:2|diff-5543-OUT,emission-5543-OUT,alpha-5729-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:5311,x:31703,y:32019,varname:node_5311,prsc:2;n:type:ShaderForge.SFN_Append,id:9304,x:31949,y:32076,varname:node_9304,prsc:2|A-5311-X,B-5311-Z;n:type:ShaderForge.SFN_Slider,id:5794,x:31740,y:32401,ptovrint:False,ptlb:Tile,ptin:_Tile,varname:node_5794,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:50;n:type:ShaderForge.SFN_Multiply,id:5761,x:32136,y:32076,varname:node_5761,prsc:2|A-9304-OUT,B-5794-OUT;n:type:ShaderForge.SFN_Tex2d,id:4812,x:32973,y:32114,ptovrint:False,ptlb:Grid,ptin:_Grid,varname:node_4812,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8432-OUT;n:type:ShaderForge.SFN_Color,id:5741,x:32603,y:31770,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5741,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:5543,x:32971,y:31852,varname:node_5543,prsc:2|A-5741-RGB,B-4812-RGB;n:type:ShaderForge.SFN_Tex2d,id:5118,x:32976,y:32390,ptovrint:False,ptlb:Mesh,ptin:_Mesh,varname:node_5118,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3501-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:6633,x:31108,y:32455,varname:node_6633,prsc:2;n:type:ShaderForge.SFN_Append,id:7470,x:31346,y:32494,varname:node_7470,prsc:2|A-6633-X,B-6633-Y,C-6633-Z;n:type:ShaderForge.SFN_Append,id:1879,x:31346,y:32661,varname:node_1879,prsc:2|A-6698-X,B-6698-Y,C-6698-Z;n:type:ShaderForge.SFN_Vector4Property,id:6698,x:31108,y:32647,ptovrint:False,ptlb:worldPos,ptin:_worldPos,varname:node_6698,prsc:2,glob:True,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5,v2:0.5,v3:0.5,v4:1;n:type:ShaderForge.SFN_Distance,id:6638,x:31543,y:32604,varname:node_6638,prsc:2|A-7470-OUT,B-1879-OUT;n:type:ShaderForge.SFN_Multiply,id:8115,x:32035,y:32718,varname:node_8115,prsc:2|A-761-OUT,B-8204-OUT;n:type:ShaderForge.SFN_Clamp01,id:8678,x:32247,y:32718,varname:node_8678,prsc:2|IN-8115-OUT;n:type:ShaderForge.SFN_Smoothstep,id:6627,x:32417,y:32868,varname:node_6627,prsc:2|A-3612-OUT,B-2814-OUT,V-8678-OUT;n:type:ShaderForge.SFN_Slider,id:3612,x:32011,y:32893,ptovrint:False,ptlb:Falloff,ptin:_Falloff,varname:node_3612,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.02538462,max:0.99;n:type:ShaderForge.SFN_OneMinus,id:7260,x:32593,y:32970,varname:node_7260,prsc:2|IN-6627-OUT;n:type:ShaderForge.SFN_Multiply,id:5963,x:33294,y:32591,varname:node_5963,prsc:2|A-5118-A,B-2215-OUT;n:type:ShaderForge.SFN_Add,id:3324,x:33294,y:32379,varname:node_3324,prsc:2|A-4812-A,B-5963-OUT;n:type:ShaderForge.SFN_Clamp01,id:7055,x:33502,y:32379,varname:node_7055,prsc:2|IN-3324-OUT;n:type:ShaderForge.SFN_Set,id:4223,x:32302,y:32076,varname:GridUV,prsc:2|IN-5761-OUT;n:type:ShaderForge.SFN_Get,id:3501,x:32739,y:32390,varname:node_3501,prsc:2|IN-4223-OUT;n:type:ShaderForge.SFN_Get,id:8432,x:32746,y:32114,varname:node_8432,prsc:2|IN-4223-OUT;n:type:ShaderForge.SFN_Set,id:461,x:32791,y:32868,varname:MeshEffect,prsc:2|IN-7260-OUT;n:type:ShaderForge.SFN_Get,id:2215,x:33074,y:32609,varname:node_2215,prsc:2|IN-461-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8204,x:31400,y:33110,ptovrint:False,ptlb:Scale,ptin:_Scale,varname:node_8204,prsc:2,glob:True,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Vector1,id:2814,x:32168,y:32996,varname:node_2814,prsc:2,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:9807,x:33256,y:32206,ptovrint:False,ptlb:_ShowAll,ptin:_ShowAll,varname:node_9807,prsc:2,glob:True,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:7437,x:31346,y:32899,ptovrint:False,ptlb:ShowMesh,ptin:_ShowMesh,varname:node_7437,prsc:2,glob:True,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Clamp01,id:3997,x:33480,y:32206,varname:node_3997,prsc:2|IN-9807-OUT;n:type:ShaderForge.SFN_Multiply,id:5729,x:33706,y:32296,varname:node_5729,prsc:2|A-3997-OUT,B-7055-OUT;n:type:ShaderForge.SFN_Lerp,id:761,x:31791,y:32651,varname:node_761,prsc:2|A-1588-OUT,B-6638-OUT,T-7437-OUT;n:type:ShaderForge.SFN_Multiply,id:1588,x:31584,y:32899,varname:node_1588,prsc:2|A-4647-OUT,B-8204-OUT;n:type:ShaderForge.SFN_Vector1,id:4647,x:31346,y:32963,varname:node_4647,prsc:2,v1:2;proporder:5794-4812-5741-5118-3612;pass:END;sub:END;*/

Shader "Shader Forge/Cross" {
    Properties {
        _Tile ("Tile", Range(0, 50)) = 0
        _Grid ("Grid", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Mesh ("Mesh", 2D) = "white" {}
        _Falloff ("Falloff", Range(0, 0.99)) = 0.02538462
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #ifndef UNITY_PASS_FORWARDBASE
            #define UNITY_PASS_FORWARDBASE
            #endif //UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 2.0
            uniform float4 _LightColor0;
            uniform float _Tile;
            uniform sampler2D _Grid; uniform float4 _Grid_ST;
            uniform float4 _Color;
            uniform sampler2D _Mesh; uniform float4 _Mesh_ST;
            uniform float4 _worldPos;
            uniform float _Falloff;
            uniform float _Scale;
            uniform float _ShowAll;
            uniform float _ShowMesh;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float2 GridUV = (float2(i.posWorld.r,i.posWorld.b)*_Tile);
                float2 node_8432 = GridUV;
                float4 _Grid_var = tex2D(_Grid,TRANSFORM_TEX(node_8432, _Grid));
                float3 node_5543 = (_Color.rgb*_Grid_var.rgb);
                float3 diffuseColor = node_5543;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = node_5543;
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float2 node_3501 = GridUV;
                float4 _Mesh_var = tex2D(_Mesh,TRANSFORM_TEX(node_3501, _Mesh));
                float MeshEffect = (1.0 - smoothstep( _Falloff, 1.0, saturate((lerp((2.0*_Scale),distance(float3(i.posWorld.r,i.posWorld.g,i.posWorld.b),float3(_worldPos.r,_worldPos.g,_worldPos.b)),_ShowMesh)*_Scale)) ));
                fixed4 finalRGBA = fixed4(finalColor,(saturate(_ShowAll)*saturate((_Grid_var.a+(_Mesh_var.a*MeshEffect)))));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #ifndef UNITY_PASS_FORWARDADD
            #define UNITY_PASS_FORWARDADD
            #endif //UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 2.0
            uniform float4 _LightColor0;
            uniform float _Tile;
            uniform sampler2D _Grid; uniform float4 _Grid_ST;
            uniform float4 _Color;
            uniform sampler2D _Mesh; uniform float4 _Mesh_ST;
            uniform float4 _worldPos;
            uniform float _Falloff;
            uniform float _Scale;
            uniform float _ShowAll;
            uniform float _ShowMesh;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                UNITY_LIGHT_ATTENUATION(attenuation, i, i.posWorld.xyz);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float2 GridUV = (float2(i.posWorld.r,i.posWorld.b)*_Tile);
                float2 node_8432 = GridUV;
                float4 _Grid_var = tex2D(_Grid,TRANSFORM_TEX(node_8432, _Grid));
                float3 node_5543 = (_Color.rgb*_Grid_var.rgb);
                float3 diffuseColor = node_5543;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                float2 node_3501 = GridUV;
                float4 _Mesh_var = tex2D(_Mesh,TRANSFORM_TEX(node_3501, _Mesh));
                float MeshEffect = (1.0 - smoothstep( _Falloff, 1.0, saturate((lerp((2.0*_Scale),distance(float3(i.posWorld.r,i.posWorld.g,i.posWorld.b),float3(_worldPos.r,_worldPos.g,_worldPos.b)),_ShowMesh)*_Scale)) ));
                fixed4 finalRGBA = fixed4(finalColor * (saturate(_ShowAll)*saturate((_Grid_var.a+(_Mesh_var.a*MeshEffect)))),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
