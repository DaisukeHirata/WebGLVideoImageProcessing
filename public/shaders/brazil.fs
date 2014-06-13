#ifdef GL_ES
precision highp float;
precision highp int;
#endif

varying vec2 vTexCoord;
uniform sampler2D texture;
uniform float arg;
uniform vec2 size;

void main(void) {
  if (arg > 0.0) {
    float xBlockSize = 0.01*0.1;
    float yBlockSize = xBlockSize * size.x / size.y;  // mutiply ratio
    float xCoord = (floor((vTexCoord.x-0.5)/xBlockSize)+0.5) * xBlockSize+0.5;
    float yCoord = (floor((vTexCoord.y-0.5)/yBlockSize)+0.5) * yBlockSize+0.5;
    vec4 color = texture2D(texture, vec2(xCoord, yCoord));
    color = vec4(color.rgb+arg*2.0-1.0, color.a);
    float sum = color.r + color.g + color.b / 3.0;
    vec3 white  = vec3(255.0, 255.0, 255.0) / 255.0;
    vec3 yellow = vec3(242.0, 252.0,   0.0) / 255.0;
    vec3 green  = vec3(  0.0, 140.0,   0.0) / 255.0;
    vec3 brown  = vec3( 48.0,  19.0,   6.0) / 255.0;
    vec3 black  = vec3(  0.0,   0.0,   0.0) / 255.0;

    if      (sum < 0.25) color = vec4(black, color.a);
    else if (sum < 0.75) color = vec4(brown,   color.a);
    else if (sum < 1.21) color = vec4(green, color.a);
    else if (sum < 2.30) color = vec4(yellow,  color.a);
    else                 color = vec4(white,  color.a);
    gl_FragColor = color;
  } else {
    gl_FragColor = texture2D(texture, vTexCoord);
  }
}