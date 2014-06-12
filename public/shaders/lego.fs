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
    float xBlockSize = arg*0.1;
    float yBlockSize = xBlockSize * size.x / size.y;  // mutiply ratio
    float xCoord = (floor((vTexCoord.x-0.5)/xBlockSize)+0.5) * xBlockSize+0.5;
    float yCoord = (floor((vTexCoord.y-0.5)/yBlockSize)+0.5) * yBlockSize+0.5;
    vec4 color = texture2D(texture, vec2(xCoord, yCoord));
    float sum = color.r + color.g + color.b / 3.0;
    vec3 one = vec3(255.0, 255.0, 255.0) / 255.0;
    vec3 two = vec3(242.0, 252.0, 0.0) / 255.0;
    vec3 three = vec3(0.0, 140.0, 0.0) / 255.0;
    vec3 four = vec3(48.0, 19.0, 6.0) / 255.0;
    vec3 five = vec3(0.0, 0.0, 0.0) / 255.0;
/*
黒　緑　黄色　白
1   255 255 255
2   242 252 0
3   0   140 0
4   48  19  6
5   0   0   0
*/
    if      (sum < 0.05) color = vec4(five,   color.a);
    else if (sum < 0.65) color = vec4(four,   color.a);
    else if (sum < 1.40) color = vec4(three, color.a);
    else if (sum < 2.15) color = vec4(two,  color.a);
    else                 color = vec4(one,  color.a);
    gl_FragColor = color;
  } else {
    gl_FragColor = texture2D(texture, vTexCoord);
  }
}