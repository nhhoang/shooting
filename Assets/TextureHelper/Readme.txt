TextureHelper - Copyright (c) 2012 - Paul West/Venus12 LLC

See the sourcecode functions for added documentation about what each function does and what parameters to provide

TextureHelper is a useful set of functions for working with procedurally generated textures and bitmaps.
In TextureHelper, a bitmap stored in main memory is referred to as a Pixmap. A Pixmap is a new type of object.

Features:

- Pixmaps in main memory
TextureHelper supports main-memory images which can be loaded, saved, transferred to/from textures, grabbed from the backbuffer, etc.
A Pixmap is a place to store pixel data in memory. You can read/write pixels in the pixmap, convert to/from textures etc.
You can read/write Pixmap pixels `fast`er using special versions of the functions, which does not errorcheck your inputs.
You can get direct access to the Color[] or Color32[] array of pixel data in a Pixmap using PixmapPixels() for the fastest data manipulation.
A Pixmap supports only RGBA format stored either as floats (ie Color[] array) or bytes (ie Color32[] array).
A few drawing functions are provided to draw pixels, axis-aligned rectanges, lines, axis-aligned boxes, and axis-aligned ellipses, on a Pixmap.
You can load an image resource with a .bytes extension from the Resources folder into a Pixmap, in PNG or JPEG format
You can also load an image file with .jpg or png extension from a disk file or a website url into a Pixmap
You can copy one Pixmap to another or copy an area of one Pixmap to another.
You can clear a Pixmap, clone an exact copy including pixel data, and convert between Byte and Float formats.
You can get the number of pixels or bytes used by a Pixmap.

NB: Y coordinates in Pixmaps are based on top-left corner whereas in a Texture2D they are relative to the bottom-left corner. This is because
texture hardware considers 0,0 to be the bottom left corner, so you may want to vertically flip the pixmap before sending it to the texture
or modify the texture with flipped Y coordinates e.g. YCoord=PixmapHeight-YCoord. See YFlipPixmap().

- Loading/Saving of images
TextureHelper can load images from your resources folder in JPG or PNG format, RGB 24-bit or RGBA 32-bit, provided file extension is .bytes
Textures can also be created by loading or downloading an RGB or RGBA PNG image file, or RGB JPEG file, from a disk file or from a web page url
It can save images straight from the backbuffer, from a texture, or from a Pixmap in PNG, JPEG, BMP and RAW (memory dump) formats..
Also you can load a Texture2D object from the Resources folder.

- Creation of new textures
TextureHelper makes it easier to create new textures and manipulate them, including quick access to modifying or reading their properties.
TextureHelper can also pass data between a Pixmap and a Texture in either direction, provided it is an ARGB32 texture format and isReadable.
RGB24 or ARGB32 textures can be compressed with DXT1/DXT5 but this may limit their modifiability/interopability with Pixmaps.
You can draw pixels, axis-aligned rectangles, lines, axis-aligned boxes, and axis-aligned ellipses directly to a texture.
While TextureHelper can create any format of Texture, most of TextureHelper (Pixmaps etc) is only compatible with ARGB32 format.

- Grabbing of images
Grab images straight from the backbuffer into a texture, to a Pixmap, or directly to a file as PNG, JPG, BMP or RAW (memory dump) formats.

- Easy procedural programming
TextureHelper is written for easy access in a `procedural` style of programming. The functions are immediately accessible anywhere without
the need for long-winded object-oriented Class.Method() syntax. This saves time typing and makes it easier to remember the commands.
As a result, you need to pass objects (e.g. Pixmap, Texture2D etc) to the function as needed.

- Variety of inputs and levels of control
There are multiple versions of various commands allowing reading, writing and different degrees or types of input control.

- JPEG export
TextureHelper uses a JavaScript implementation of JPEGEncoder.js which was originally written by Adobe and converted to
Unity by Matthew Wegner of Flashbang Studios. It is free to use provided you keep the copyright notice intact. Make sure you are comfortable
with the terms (included in a separate file) before using it in your projects.

Enjoy.

Paul West (ImaginaryHuman on the Unity forums)
http://forum.unity3d.com/members/17044-imaginaryhuman
