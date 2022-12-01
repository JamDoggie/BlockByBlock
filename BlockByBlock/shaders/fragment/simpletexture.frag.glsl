#version 460

precision highp float;

out vec4 outputColor;

in vec2 outTexCoord;
in vec4 outColor;

uniform sampler2D texture0;

// SHADER STATES: 1 = active, 0 = inactive
uniform int TextureState;
uniform int ColorState;

// Simple function that flips a boolean input. Input of 1 returns 0 and vice-versa.
int flipRange(int val)
{
    return (val - 1) * -1;
}

void main()
{
    vec4 renderColor = outColor;

	int textureMixValue = ColorState; // 0 means the texture will be multiplied in the final output, 1 means the color white will be.
	int colorMixValue = TextureState; // 0 means the input color will be multiplied in the final output, 1 means the color white will be.
	// The idea is that you can choose to use render the texture, the color, or a mixture of the two.

    vec4 colorWhite = vec4(1.0f, 1.0f, 1.0f, 1.0f); // White color, mixed in when a given state is disabled instead of the texture or color.
                                                    // The color white is used because when multiplied it has no effect on the final output.

	// Lerp between our input colors and the color white.
    vec4 texColor = mix(texture(texture0, outTexCoord), colorWhite, textureMixValue); 
	vec4 color = mix(renderColor, colorWhite, colorMixValue);

    outputColor = texColor * color; // Final output, texture and color are multiplied together.
}

