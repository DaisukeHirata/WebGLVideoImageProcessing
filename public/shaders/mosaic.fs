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
    gl_FragColor = texture2D(texture, vec2(xCoord, yCoord));
  } else {
    gl_FragColor = texture2D(texture, vTexCoord);
  }
}