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

uniform mat4 modelViewMatrix;
uniform mat3 normalMatrix;
uniform mat4 projectionMatrix;

struct LightSource
{
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    vec3 position;
};

uniform LightSource lightSources[2];

struct LightModel
{
    vec3 ambient;
};

uniform LightModel lightModel;

struct Material 
{
    vec3  emission;
    vec3  ambient;
    vec3  diffuse;
    vec3  specular;
    float shininess;
};
uniform Material material;

// States
uniform int LightingState;
uniform int SmoothLightingState;

void main()
{
    vec3 normal    = normalize(normalMatrix * normals);         // normal vector              
    vec3 position  = vec3(modelViewMatrix * vec4(position, 1)); // vertex pos in eye coords   

    if (LightingState == 1)
    {
        vec3 ambient  = vec3(0.0, 0.0, 0.0);
        vec3 diffuse  = vec3(0.0, 0.0, 0.0);
        vec3 specular = vec3(0.0, 0.0, 0.0);

        vec3 sceneColor = vec3(0.0, 0.0, 0.0);

	    for (int i = 0; i < lightSources.length; i++)
        {
            vec3 halfVector = normalize(lightSources[i].position + vec3(0, 0, 1));          // light half vector          
            float nDotVP    = dot(normal, normalize(lightSources[i].position));             // normal . light direction   
            float nDotHV    = max(0.0, dot(normal,  halfVector));                           // normal . light half vector 
            float pf        = mix(0.0, pow(nDotHV, material.shininess), step(0.0, nDotVP)); // power factor               

            ambient    += lightSources[i].ambient;
            diffuse    += lightSources[i].diffuse * nDotVP;
            specular   += lightSources[i].specular * pf;
            sceneColor += material.emission + material.ambient * lightModel.ambient;
        }
    
        outLightColor = vec4(clamp(sceneColor +                             
                        ambient  * material.ambient +            
                        diffuse  * material.diffuse +            
                        specular * material.specular, 0.0, 1.0), 1.0);
    }
	
    outTexCoord = texCoord;
    outColor = color;
    outNormals = normals;
    outBrightness = brightness;
    
    gl_Position = projectionMatrix * modelViewMatrix * vec4(position, 1);
}