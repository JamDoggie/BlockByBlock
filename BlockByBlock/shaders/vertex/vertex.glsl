#version 460

precision highp float;

layout(location = 0) in vec3 position;
layout(location = 1) in vec2 texCoord;
layout(location = 2) in vec4 color;
layout(location = 3) in vec3 normals;
layout(location = 4) in vec2 brightness;

out vec2 outTexCoord;
out vec4 outColor;
out vec3 outNormals;
out vec4 outLightColor;
out vec2 outBrightness;

out vec3 vertPos;

uniform mat4 projectionMatrix;
uniform mat3 normalMatrix;
uniform mat4 modelViewMatrix;

uniform int LightingState;
uniform int SmoothLightingState;

// END OF INITIAL DECLARATIONS || START OF LIGHTING \\
vec4 Specular = vec4(1.0, 1.0, 1.0, 1.0);

struct LightSource 
{
    vec4 position;
	vec4 ambient;
	vec4 diffuse;
	vec4 specular;
	float constantAttenuation;
	float linearAttenuation;
	float quadraticAttenuation;
};

uniform LightSource LightSources[2];
uniform float material_shininess;
uniform vec4 material_ambient;
uniform vec4 material_diffuse;
uniform vec4 material_specular;

uniform vec4 LightModelAmbient = vec4(0.0,0.0,0.0,1.0);

void directionalLight(in int i, in vec3 normal, in vec3 ecPosition3, inout vec4 Ambient, inout vec4 Diffuse, inout vec4 Specular)
{
   float nDotVP;         // normal . light direction
   float nDotHV;         // normal . light half vector
   float pf;             // power factor

   vec3 halfVector = normalize(LightSources[i].position.xyz + vec3(0.0, 0.0, 1.0));

   nDotVP = max(0.0, dot(normal, normalize(vec3 (LightSources[i].position))));
   nDotHV = max(0.0, dot(normal, vec3 (halfVector)));

   
    pf = 0.0;

   Ambient  += LightSources[i].ambient;
   Diffuse  += LightSources[i].diffuse * nDotVP;
   Specular += LightSources[i].specular * pf;
}

vec3 fnormal(mat3 normalMatrix, vec3 inNormal)
{
    // Compute the normal 
    vec3 normal = normalMatrix * inNormal;
    normal = normalize(normal);
    return normal;
}

void flight(vec3 normal, vec4 ecPosition, float alphaFade, out vec4 returnColor, inout vec4 Ambient, inout vec4 Diffuse, inout vec4 Specular)
{
    vec4 color;
    vec3 ecPosition3;
    vec3 eye;

    ecPosition3 = (vec3 (ecPosition)) / ecPosition.w;
    eye = vec3 (0.0, 0.0, 1.0);

    // Clear the light intensity accumulators
    Ambient  = vec4 (0.0);
    Diffuse  = vec4 (0.0);
    Specular = vec4 (0.0);

    directionalLight(0, normal, ecPosition3, Ambient, Diffuse, Specular);
    directionalLight(1, normal, ecPosition3, Ambient, Diffuse, Specular);

    color = LightModelAmbient +
            Ambient  * material_ambient +
            Diffuse  * material_diffuse;
    color += Specular * material_specular;
    color = clamp( color, 0.0, 1.0 );
    returnColor = color;

    returnColor.a *= alphaFade;
}

void main()
{
	gl_Position = projectionMatrix * modelViewMatrix * vec4(position, 1.0);

    outTexCoord = texCoord;
	outColor = color;
    outNormals = normals;
    outBrightness = brightness;

    vertPos = (modelViewMatrix * vec4(position, 1.0)).xyz;

    if (LightingState == 1)
    {
        vec4 Ambient  = vec4 (0.0);
        vec4 Diffuse  = vec4 (0.0);
        vec4 Specular = vec4 (0.0);

        vec3  transformedNormal;
        float alphaFade = 1.0;

        transformedNormal = fnormal(normalMatrix, normals);
	
        vec4 lightColor;

        vec4 ecPosition = modelViewMatrix * vec4(position, 1.0);

        // This function outputs the result to lightColor.
        flight(transformedNormal, ecPosition, alphaFade, lightColor, Ambient, Diffuse, Specular);

        outLightColor = lightColor;
    }
}