float4x4 View;
float4x4 Projection;
#define NUM_TEXTURES 8
#define MAX_OBJECTS 81
float4x3 WorldTransforms[MAX_OBJECTS];

texture Colors;

float3 LightDirection1;
float3 DiffuseColor1;
float3 LightDirection2;
float3 DiffuseColor2;
float AmbientAmount;


sampler ColorSampler = sampler_state
{
    Texture = (Colors);
    
    MinFilter = Point;
    MagFilter = Point;
    MipFilter = Point;
    
    AddressU = Clamp;
    AddressV = Clamp;
};


//The texture coordinates aren't actually used in this shader but it makes things marginally simpler outside.  Not exactly optimized!
struct VertexShaderInput
{
    half4 PositionXYZNormalX : POSITION0;
    half2 NormalYZ : POSITION1;
    //float2 TextureCoordinates : TEXCOORD0;
    half2 Index : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float3 Normal : TEXCOORD0;
	float TextureIndex : TEXCOORD1;
};



VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    int index = input.Index.x;

	float4 position = float4(input.PositionXYZNormalX.x, input.PositionXYZNormalX.y, input.PositionXYZNormalX.z, 1);
	float3 normal = float3(input.PositionXYZNormalX.w, input.NormalYZ.x, input.NormalYZ.y);

    float3 worldPosition = mul(position, WorldTransforms[index]);
    output.Position = mul(mul(float4(worldPosition, 1), View), Projection);
    output.Normal = mul(float4(normal, 0), WorldTransforms[index]);
	output.TextureIndex = input.Index.y;

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{

	float halfPixel = .5f / NUM_TEXTURES;
    
	float3 surfaceColor = tex2D(ColorSampler, float2(halfPixel + halfPixel * 2 * input.TextureIndex, halfPixel)).xyz;

		
	float3 normal = normalize(input.Normal);
    float diffuseAmount1 = saturate(-dot(normal, LightDirection1));
    float diffuseAmount2 = saturate(-dot(normal, LightDirection2));
  
    
	surfaceColor =  AmbientAmount * surfaceColor + surfaceColor * (diffuseAmount1 * DiffuseColor1 + diffuseAmount2 * DiffuseColor2);
    return float4(surfaceColor, 1);
}

technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
