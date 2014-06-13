#ifdef GL_ES
precision highp float;
precision highp int;
#endif

varying vec2 vTexCoord;
uniform sampler2D texture;
uniform float arg;
uniform vec2 size;
const float PI = 3.14159265358979323846264;

vec4 brazil(vec2 coord) {

    float xBlockSize = 0.01*0.1;
    float yBlockSize = xBlockSize * size.x / size.y;  // mutiply ratio
    float xCoord = (floor((coord.x-0.5)/xBlockSize)+0.5) * xBlockSize+0.5;
    float yCoord = (floor((coord.y-0.5)/yBlockSize)+0.5) * yBlockSize+0.5;
    vec4 color = texture2D(texture, vec2(xCoord, yCoord));
    color = vec4(color.rgb+arg*9.0-1.0, color.a);
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

    return color;

}

void main(void) {
  if (arg == 0.5) {
    gl_FragColor = texture2D(texture, vTexCoord);
  } else if (arg == 1.0) {
    gl_FragColor = vec4(0.0, 0.0, 0.0, 1.0);
  } else if (arg > 0.5) {
    vec2 coordOffset = size*0.5;
    float fd = 500.0 / tan((arg-0.5) * PI);

    vec2 v = gl_FragCoord.xy - coordOffset;
    float d = length(v);
    vec2 xy = v/d * fd*tan(clamp(d/fd, -0.5*PI, 0.5*PI)) + coordOffset;
    vec2 tc = xy/size;
    if (all(greaterThanEqual(tc, vec2(0.0))) && all(lessThanEqual(tc, vec2(1.0)))) {
      gl_FragColor = brazil(xy/size);
    } else {
      gl_FragColor = vec4(0.0, 0.0, 0.0, 1.0);
    }
  } else {
    vec2 coordOffset = size*0.5;
    float fd = 500.0 / tan((0.5-arg) * PI);

    vec2 v = gl_FragCoord.xy - coordOffset;
    float d = length(v);
    vec2 xy = v/d * fd*atan(d/fd) + coordOffset;
    gl_FragColor = brazil(xy/size);
  }
}