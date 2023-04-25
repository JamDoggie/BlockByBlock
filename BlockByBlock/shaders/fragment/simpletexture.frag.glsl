#version 460

precision lowp float;

out vec4 outputColor;

in vec2 outTexCoord;
in vec4 outColor;
in vec3 outNormals;
in vec4 outLightColor;
in vec2 outBrightness;

in vec3 vertPos;

uniform sampler2D activeTexture;
uniform sampler2D lightTexture;

// SHADER STATES: 1 = active, 0 = inactive
uniform int TextureState;
uniform int LightmapState;
uniform int ColorState;
uniform int AlphaTestState;
uniform int LightingState;
uniform int OverrideBrightnessState;
uniform int FogState = 0;
uniform int ColorMaterialState;

uniform mat4 projectionMatrix;
uniform mat3 normalMatrix;
uniform mat4 modelViewMatrix;

uniform vec4 GlobalColor;
uniform float AlphaTestThreshold = 0.5; // If the alpha of the current texture is less than or equal to this value -- and AlphaTestState is 1 -- we'll discard the pixel later.

uniform vec2 BrightnessOverride;

// FOG PARAMETERS
uniform int FogMode = 0; // 0 = linear, 1 = exp
uniform float FogDensity = 0.02;
uniform vec4 FogColor = vec4(0.66, 0.73, 1.0, 1.0);
uniform float FogStart = 0.0F;
uniform float FogEnd = 8.0F;

// Simple function that flips a boolean input. Input of 1 returns 0 and vice-versa.
int flipRange(int val)
{
    return (val - 1) * -1;
}

void main()
{
	vec4 colorWhite = vec4(1.0, 1.0, 1.0, 1.0); // White color, mixed in when a given state is disabled instead of the texture or color.
                                                // The color white is used because when multiplied it has no effect on the final output.

	float textureMixValue = TextureState; // 0 means the texture will be multiplied in the final output, 1 means the color white will be.
	float colorMixValue = ColorState; // 0 means the input color will be multiplied in the final output, 1 means the color white will be.
	// The idea is that you can choose to use render the texture, the color, or a mixture of the two.
	
	// Lerp between our input colors and the color white.
	vec4 texFragment = texture(activeTexture, outTexCoord);
    vec4 texColor = mix(colorWhite, texFragment, textureMixValue); 
	vec4 color = mix(colorWhite, outColor, colorMixValue);

	if (AlphaTestState == 1 && TextureState == 1 && texColor.a <= AlphaTestThreshold) // If the alpha test is enabled, and the texture is enabled, and the alpha value is less than AlphaTestThreshold, discard the pixel. 
		discard;
		
	vec2 lightTexCoords = outBrightness;

	if (OverrideBrightnessState == 1)
	{
		lightTexCoords = vec2(((BrightnessOverride.x / 16) / 17) + 0.0625, ((BrightnessOverride.y / 16) / 17) + 0.0625);
	}

	vec4 lightMapColor = mix(colorWhite, texture(lightTexture, lightTexCoords), LightmapState);
	

	// FOG
	float fogFactor;

	// Linear fog
	if (FogMode == 0)
	{
		float fogZ = length(vertPos);
		fogFactor = clamp((FogEnd - fogZ) / (FogEnd - FogStart), 0.0, 1.0);
	}

	// Exponential fog
	if (FogMode == 1)
	{
		const float LOG2 = 1.442695;
		float fogZ = length(vertPos);
	
		fogFactor = exp2( -FogDensity * 
							FogDensity * 
							fogZ * 
							fogZ * 
							LOG2 );

		fogFactor = clamp(fogFactor, 0.0, 1.0);
	}
	
	// Final output color
	outputColor = texColor * color * GlobalColor * lightMapColor; // Final output, texture and color are multiplied together.

	if (LightingState == 1)
	{
		outputColor *= outLightColor;
	}

	outputColor = mix(outputColor, mix(FogColor, outputColor, fogFactor), FogState);
}

