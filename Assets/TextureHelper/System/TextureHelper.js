//TextureHelper library - Copyright (c) 2012 Paul West/Venus12 LLC

//Version 1.0 - Last Modified 11/03/2012

//114 functions - implemented for easy procedural access
//Just call the functions directly from anywhere, e.g. CreateTexture(512,512,TextureFormat.ARGB32);
//No need for object-oriented class.method() syntax, but you must pass objects as parameters, e.g. SaveTextureJPG(Tex:Texture2D, FilePath:String, Quality:int)


#pragma strict

class TextureHelper extends MonoBehaviour {
	//Add TextureHelper library commands to MonoBehavior so they can be referenced globally without object-oriented syntax
	//Alternative is a global class but requires e.g. TextureHelper.Command() syntax, ie //static class TextureHelper {

	function CreateTexture() {
		//Create a new Texture2D texture the size of the screen
		//The texture will be in ARGB32 format with no mipmaps
		return Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
	}
	
	function CreateTexture(Width:int, Height:int) {
		//Create a new Texture2D texture of a given size
		//The texture will be in ARGB32 format with no mipmaps
		return Texture2D(Width, Height, TextureFormat.ARGB32, false);
	}

	function CreateTexture(Width:int, Height:int, Format:TextureFormat) {
		//Create a new Texture2D texture of a given size and format
		//The texture will have no mipmaps
		return Texture2D(Width, Height, Format, false);
	}

	function CreateTexture(Width:int, Height:int, Format:TextureFormat, MipMap:boolean) {
		//Create a new Texture2D texture of a given size and format with or without mipmaps
		return Texture2D(Width, Height, Format, MipMap);
	}

	function CreateTexture(Width:int, Height:int, Format:TextureFormat, MipMap:boolean, Filter:FilterMode, AnisotropicLevel:int, Wrap:TextureWrapMode, Bias:float) {
		//Create a new Texture2D texture of a given size and format with or without mipmaps
		//Also assign a filter mode, anisotropic level, wrapping mode and mipmap bias
		var Tex:Texture2D = Texture2D(Width, Height, Format, MipMap);			//Make a texture
		Tex.filterMode = Filter;				//Set sampling
		Tex.anisoLevel = AnisotropicLevel;		//Set anisotropic filtering
		Tex.wrapMode = Wrap;					//Set edge wrapping
		Tex.mipMapBias = Bias;					//Set mipmap bias
		return Tex;								//Send the texture back
	}
	
	function FreeTexture(Tex:Texture2D) {
		//Delete a Texture2D
		if (Tex) {
			Destroy(Tex);						//Free it
		}
	}
			
	function ResourceToTexture(ResourcePath:String) {
		//Load an image from a Resources folder into a Texture2D texture
		//The image format must be a JPEG (RGB24) or a PNG (RGB24 or RGBA32)
		//The image resource being loaded must have its extension renamed from .jpg/.png to .bytes
		//JPEG files will return an RGB24 Texture2D without any alpha channel
		//PNG files will return an ARGB32 Texture2D including alpha channel regardless of whether the file has an alpha channel (defaults to 255)
		var File:TextAsset = Resources.Load(ResourcePath) as TextAsset;
		var Tex:Texture2D = new Texture2D(1,1);		//Make a dummy texture
		Tex.LoadImage(File.bytes);					//Load the image into the texture
		return Tex;									//Send the texture back 
	}
	
	function ResourceToTexture(ResourcePath:String, DXTCompress:boolean) {
		//Load an image from a Resources folder into a Texture2D texture
		//The texture will be optionally DXT compressed - JPEGs will be DXT1 compressed without alpha, PNGs will be DXT5 compressed with alpha
		//The texture will not be mipmapped
		//The image format must be a JPEG (RGB24) or a PNG (RGB24 or RGBA32)
		//The image resource being loaded must have its extension renamed from .jpg/.png to .bytes
		//JPEG files will return an RGB24 Texture2D without any alpha channel
		//PNG files will return an ARGB32 Texture2D including alpha channel regardless of whether the file has an alpha channel (defaults to 255)
		var File:TextAsset = Resources.Load(ResourcePath) as TextAsset;
		var Tex:Texture2D;
		if (DXTCompress==true) {
			Tex = new Texture2D(1,1,TextureFormat.DXT5,false);		//Create texture with compression
		} else {
			Tex = new Texture2D(1,1,TextureFormat.ARGB32,false);	//Create texture without compression
		}
		Tex.LoadImage(File.bytes);					//Load the image into the texture
		return Tex;									//Send the texture back 
	}

	function ResourceToTexture(ResourcePath:String, DXTCompress:boolean, MipMap:boolean) {
		//Load an image from a Resources folder into a Texture2D texture
		//The texture will be optionally DXT compressed - JPEGs will be DXT1 compressed without alpha, PNGs will be DXT5 compressed with alpha
		//The texture will be optionally mipmapped
		//The image format must be a JPEG (RGB24) or a PNG (RGB24 or RGBA32)
		//The image resource being loaded must have its extension renamed from .jpg/.png to .bytes
		//JPEG files will return an RGB24 Texture2D without any alpha channel
		//PNG files will return an ARGB32 Texture2D including alpha channel regardless of whether the file has an alpha channel (defaults to 255)
		var File:TextAsset = Resources.Load(ResourcePath) as TextAsset;
		var Tex:Texture2D;
		if (MipMap==true) {
			if (DXTCompress==true) {
				Tex = new Texture2D(1,1,TextureFormat.DXT5,false);		//Create mip-mapped texture with compression
			} else {
				Tex = new Texture2D(1,1,TextureFormat.ARGB32,false);	//Create mip-mapped texture without compression
			}
		}
		Tex.LoadImage(File.bytes);					//Load the image into the texture
		return Tex;									//Send the texture back 
	}

	function FileToTexture(FilePathUrl:String, Tex:Texture2D) {
		//Load an image file from disk into an existing Texture2D texture, e.g. call CreateTexture() first
		//Image file must be a regular .JPG or .PNG
		//JPEG will load as RGB24 format, PNG will load as ARGB32
		//Since `WWW` is used, you can also load from a website url
		//FilePathUrl should begin with http:// or similar for web-based images, or file:// for filesystem/disk images
		//Texture size and format may be modified afterwards
		//If original texture had DXT1 or DXT5 compression the loaded texture will be compressed (JPEG as DXT1, PNG as DXT5)
		var www = new WWW(FilePathUrl);				//Load/download the file
		yield www;									//Wait for it to finish
		www.LoadImageIntoTexture(Tex);				//Load the PNG/JPEG into our texture
	}
	
	function LoadTexture2D(ResourcePath:String) {
		//Load a texture asset from the Resources folder into a Texture2D object
		//The texture should already have been set up in the Unity editor with appropriate import settings - it's a Texture2D not an image file
		return Resources.Load(ResourcePath) as Texture2D;		//Loaded, send it back
	}
	
	function CloneTexture(SourceTexture:Texture2D) {
		//Create an exact copy of a texture including its pixels
		//SourceTexture must be isReadable and in ARGB32 format
		//Works using a pixmap to transfer the data so must be a format compatible with Pixmaps ie ARGB32
		var Tex:Texture2D = CreateTexture(SourceTexture.width,SourceTexture.height);	//Create same-size texture
		var Pix:Pixmap = CreatePixmap(SourceTexture.width,SourceTexture.height,true);	//Create same-size Pixmap in Color32[] format
		TextureToPixmap(SourceTexture,Pix);			//Download the pixels into the Pixmap
		PixmapToTexture(Pix,Tex);					//Upload to the new texture
		return Tex;									//Send it back
	}

	function SaveTexturePNG(Tex:Texture2D, FilePath:String) {
		//Save a readable texture as a PNG file
		//This only works with ARGB32 or RGB24 Texture2D, no other formats
		//Typicall file extension is .png
		if (Tex) {
		    var bytes:byte[] = Tex.EncodeToPNG();		//Encode texture into PNG
			File.WriteAllBytes(FilePath, bytes);		//Write to a file
			return true;								//Success
		} else {
			return false;								//Failed
		}
	}
	
	function SaveTextureJPG(Tex:Texture2D, FilePath:String) {
		//Save a readable texture as a JPG file - 85% quality
		//This only works with ARGB32 or RGB24 Texture2D, no other formats
		//Typical file extension is .jpg
		if (Tex) {
			//Create our JPEG encoder for this texture - utilizes JPEGEncoder.js which is derived from Adobe copyrighted/licensed sourcecode
			var encoder:JPGEncoder = new JPGEncoder(Tex, 85);
			
			//Encoder is threaded; wait for it to finish - the brute force unfriendly hogging way
			while(!encoder.isDone) {}					//Should really do yield WaitForSeconds(0.0001); here to yield for 1 millisecond between checks, but this causes the function to fail to execute, probably because multithreading then clashes with other programming
			var bytes:byte[] = encoder.GetBytes();		//Get the data
			File.WriteAllBytes(FilePath, bytes);		//Write to a file
			return true;								//Success
		} else {
			return false;								//Failed
		}
	}
	
	function SaveTextureJPG(Tex:Texture2D, FilePath:String, Quality:int) {
		//Save a readable texture as a JPG file at a given quality
		//Quality should range from 1 to 100 (as percentage, 100=maximum quality)
		//This only works with ARGB32 or RGB24 Texture2D, no other formats
		//Typical file extension is .jpg
		if (Tex) {
			if (Quality<1) {
				Quality=1;
			} else {
				if (Quality>100) {
					Quality=100;
				}
			}
			
			//Create our JPEG encoder for this texture - utilizes JPEGEncoder.js which is derived from Adobe copyrighted/licensed sourcecode
			var encoder:JPGEncoder = new JPGEncoder(Tex, Quality);
			
			//Encoder is threaded; wait for it to finish - the brute force unfriendly hogging way
			while(!encoder.isDone) {}					//Should really do yield WaitForSeconds(0.0001); here to yield for 1 millisecond between checks, but this causes the function to fail to execute, probably because multithreading then clashes with other programming
			var bytes:byte[] = encoder.GetBytes();		//Get the data
			File.WriteAllBytes(FilePath, bytes);		//Write to a file
			return true;								//Success
		} else {
			return false;								//Failed
		}
	}
	
	function SaveTextureRAW(Tex:Texture2D, FilePath:String) {
		//Save a readable texture as a RAW file (raw byte pixel data, no header)
		//Will include alpha channel even if the texture had none (in which case alpha is 255)
		//Typical file extension is .raw (but not camera raw format)
		if (Tex) {
			var NewPixmap:Pixmap = CreatePixmap(Tex.width, Tex.height, true); 	//Create an RGBA8 Pixmap
			SavePixmapRAW(NewPixmap, FilePath);			//Save the pixmap to a file
			return true;								//Success
		} else {
			return false;								//Failed
		}
	}

	function SaveTextureBMP(Tex:Texture2D, FilePath:String) {
		//Save a readable texture as a BMP file (raw byte pixel data, no header)
		//Will NOT include alpha channel even if the texture had one
		//Typical file extension is .bmp
		if (Tex) {
			var NewPixmap:Pixmap = CreatePixmap(Tex.width, Tex.height, true); 	//Create an RGBA8 Pixmap
			SavePixmapBMP(NewPixmap, FilePath);			//Save the pixmap to a file
			return true;								//Success
		} else {
			return false;								//Failed
		}
	}
	
	function GrabTexture() {
		//Grab the backbuffer into a new ARGB32 texture
		//Returns the new texture
		var Tex:Texture2D = CreateTexture(Screen.width, Screen.height);		//Create a texture
		Tex.ReadPixels(Rect(0,0,Screen.width,Screen.height), 0,0,false);	//Grab to texture
		return Tex;									//Send it back
	}
	
	function GrabTexture(Tex:Texture2D) {
		//Grab the backbuffer into an existing texture
		//Requires an ARGB32 and RGB24 texture format. The texture also has to have Is Readable flag set in the import settings
		Tex.ReadPixels(Rect(0,0,Mathf.Min(Screen.width,Tex.width),Mathf.Min(Screen.height,Tex.height)), 0,0,false);	//Grab to texture as much as will fit
	}
	
	function GrabTexture(X:int, Y:int, Width:int, Height:int) {
		//Grab a portion of the backbuffer into a new ARGB32 texture
		//Returns the new texture
		var Tex:Texture2D = CreateTexture(Width, Height);		//Create a texture
		Tex.ReadPixels(Rect(X,Y,Width,Height), 0,0,false);		//Grab to texture
		return Tex;									//Send it back
	}
	
	function GrabTexture(Tex:Texture2D, X:int, Y:int, Width:int, Height:int) {
		//Grab a portion of the backbuffer into an existing texture
		//Requires an ARGB32 and RGB24 texture format. The texture also has to have Is Readable flag set in the import settings
		Tex.ReadPixels(Rect(X,Y,Width,Height), 0,0,false);	//Grab to texture as much as will fit
	}

	function GrabTexture(X:int, Y:int, Width:int, Height:int, ToX:int, ToY:int, MipMaps:boolean) {
		//Grab a portion of the backbuffer into a new ARGB32 texture at a given offset
		//Optionally generates mipmaps
		//Returns the new texture
		var Tex:Texture2D = CreateTexture(Width, Height, TextureFormat.ARGB32, MipMaps);			//Create a texture
		Tex.ReadPixels(Rect(X,Y,Width,Height), ToX,ToY,MipMaps);	//Grab to texture at offset
		return Tex;									//Send it back
	}
	
	function GrabTexture(Tex:Texture2D, X:int, Y:int, Width:int, Height:int, ToX:int, ToY:int, MipMaps:boolean) {
		//Grab a portion of the backbuffer into an existing texture at a given offset
		//Optionally generates mipmaps
		//Requires an ARGB32 and RGB24 texture format. The texture also has to have Is Readable flag set in the import settings
		Tex.ReadPixels(Rect(X,Y,Width,Height), ToX,ToY,MipMaps);	//Grab to texture as much as will fit at a given offset
	}

	function TextureWidth(Tex:Texture2D) {
		//Get the width of a Texture2D texture in texels
		if (Tex) {
			return Tex.width;					//Get it
		} else {
			return 0;
		}
	}

	function TextureWidth(Tex:Texture2D, NewWidth:int) {
		//Set the width of a Texture2D texture in texels
		//The pixel content of the texture will likely be trashed afterwards
		//The new width must be a texture size allowed by the graphics card e.g. 1..4096
		if (Tex) {
			if (NewWidth<1) {
				NewWidth=1;
			} else {
				if (NewWidth>65536) {
					NewWidth=65536;
				}
			}
			if (Tex.mipmapCount==1) {
				Tex.Resize(NewWidth, Tex.height, Tex.format, false);	//Change the width only, no mipmaps
			} else {
				Tex.Resize(NewWidth, Tex.height, Tex.format, true);		//Change the width only, with mpmaps
			}
		}
	}
		
	function TextureHeight(Tex:Texture2D) {
		//Get the height of a Texture2D texture in texels
		if (Tex) {
			return Tex.height;					//Get it
		} else {
			return 0;
		}
	}

	function TextureHeight(Tex:Texture2D, NewHeight:int) {
		//Set the height of a Texture2D texture in texels
		//The pixel content of the texture will likely be trashed afterwards
		//The new height must be a texture size allowed by the graphics card e.g. 1..4096
		if (Tex) {
			if (NewHeight<1) {
				NewHeight=1;
			} else {
				if (NewHeight>65536) {
					NewHeight=65536;
				}
			}
			if (Tex.mipmapCount==1) {
				Tex.Resize(Tex.width, NewHeight, Tex.format, false);		//Change the height only, no mipmaps
			} else {
				Tex.Resize(Tex.width, NewHeight, Tex.format, false);		//Change the height only, with mipmaps
			}
		}
	}
		
	function TextureFilter(Tex:Texture2D) {
		//Get the filterMode of a Texture2D texture
		//Can be FilterMode.Point, FilterMode.Bilinear, FilterMode.Trilinear
		if (Tex) {
			return Tex.filterMode;				//Get it
		} else {
			return null;
		}
	}

	function TextureFilter(Tex:Texture2D, NewFilter:FilterMode) {
		//Set the filterMode of a Texture2D texture
		//Can be FilterMode.Point, FilterMode.Bilinear, FilterMode.Trilinear
		if (Tex) {
			Tex.filterMode=NewFilter;			//Set it
		}
	}

	function TextureAniso(Tex:Texture2D) {
		//Get the amount of anisotropic filtering for a Texture2D texture
		//Can be in the range 1-9 where 1=none and 9=maximum
		if (Tex) {
			return Tex.anisoLevel;				//Get it
		} else {
			return null;
		}
	}
	
	function TextureAniso(Tex:Texture2D, NewAnisotropic:int) {
		//Set the amount of anisotropic filtering for a Texture2D texture
		//Can be in the range 1-9 where 1=none and 9=maximum
		if (Tex) {
			if (NewAnisotropic<0) {
				NewAnisotropic=0;
			} else {
				if (NewAnisotropic>9) {
					NewAnisotropic=9;
				}
			}
			Tex.anisoLevel=NewAnisotropic;		//Set it
		}
	}
	
	function TextureWrap(Tex:Texture2D) {
		//Get the wrap ode of a Texture2D texture
		//Can be TextureWrapMode.Clamp or TextureWrapMode.Wrap
		if (Tex) {
			return Tex.wrapMode;				//Get it
		} else {
			return null;
		}
	}

	function TextureWrap(Tex:Texture2D, NewWrapMode:TextureWrapMode) {
		//Set the wrap ode of a Texture2D texture
		//Can be TextureWrapMode.Clamp or TextureWrapMode.Wrap
		if (Tex) {
			Tex.wrapMode = NewWrapMode;			//Set it
		}
	}
		
	function TextureBias(Tex:Texture2D) {
		//Get the mipMapBias of a Texture2D texture
		//Returns float in the range -1 to +1
		if (Tex) {
			return Tex.mipMapBias;				//Get it
		} else {
			return null;
		}
	}
	
	function TextureBias(Tex:Texture2D, NewBias:float) {
		//Set the mipMapBias of a Texture2D texture
		//Float value should be in the range -1 to +1, most likely -0.5 to +0.5
		if (Tex) {
			if (NewBias<-1) {
				NewBias=-1;
			} else {
				if (NewBias>1) {
					NewBias=1;
				}
			}
			Tex.mipMapBias=NewBias;				//Set it
		}
	}

	function TextureMipMaps(Tex:Texture2D) {
		//Get the number of Mip Map levels in a Texture2D texture, as an integer
		//Includes the biggest level size, so is always 1 or more
		if (Tex) {
			return Tex.mipmapCount;				//Get it
		} else {
			return null;
		}
	}
	
	function TextureMipMaps(Tex:Texture2D, MipMap:boolean) {
		//Change a Texture2D texture to have mipmaps if it doesn't already
		//This will alter the texture and possibly trash the pixel content!
		if (Tex) {
			if (Tex.mipmapCount==1) {
				//Does not have mipmaps, convert it
				Tex.Resize(Tex.width, Tex.height, Tex.format, true);	//Same size and format, but with mipmaps
			}
		}
	}

	function TextureMode(Tex:Texture2D) {
		//Get the format of an existing Texture2D texture
		//Returns the TextureFormat
		if (Tex) {
			return Tex.format;					//Get it
		} else {
			return null;
		}
	}

	function TextureMode(Tex:Texture2D, NewFormat:TextureFormat) {
		//Set the format of an existing Texture2D texture
		//This will alter the texture and possibly trash the pixel content!
		if (Tex) {
			if (NewFormat != Tex.format) {
				if (Tex.mipmapCount==1) {
					Tex.Resize(Tex.width, Tex.height, NewFormat, false);	//Same size, new format, no mipmaps
				} else {
					Tex.Resize(Tex.width, Tex.height, NewFormat, true);		//Same size, new format, has mipmaps
				}
			}
		}
	}

	function TextureMode(Tex:Texture2D, NewFormat:TextureFormat, NewWidth:int, NewHeight:int) {
		//Set the format of an existing Texture2D texture and also resize it
		//This will alter the texture and possibly trash the pixel content!
		//The new size must be a texture size allowed by the graphics card e.g. 1..4096
		if (Tex) {
			if ((NewFormat != Tex.format) || ((NewWidth != Tex.width) && (NewHeight != Tex.height)) ) {
				if (Tex.mipmapCount==1) {
					Tex.Resize(NewWidth, NewHeight, NewFormat, false);		//New size, new format, no mipmaps
				} else {
					Tex.Resize(NewWidth, NewHeight, NewFormat, true);		//New size, new format, has mipmaps
				}
			}
		}
	}
	
	function TextureMode(Tex:Texture2D, NewFormat:TextureFormat, NewWidth:int, NewHeight:int, MipMaps:boolean) {
		//Set the format of an existing Texture2D texture and also resize it, possibly with mipmaps
		//This will alter the texture and possibly trash the pixel content!
		//The new size must be a texture size allowed by the graphics card e.g. 1..4096
		if (Tex) {
			var Mips:int=Tex.mipmapCount;
			if ((NewFormat != Tex.format) || ((NewWidth != Tex.width) && (NewHeight != Tex.height)) || ((MipMaps==true) && (Mips==1)) || ((MipMaps==false) && (Mips>1)) ) {
				if (MipMaps==true) {
					Tex.Resize(NewWidth, NewHeight, NewFormat, true);		//New size, new format, with mipmaps
				} else {
					Tex.Resize(NewWidth, NewHeight, NewFormat, false);		//New size, new format, without mipmaps
				}
			}
		}
	}
			
	function TextureCompression(Tex:Texture2D) {
		//Get the compressed status of an existing Texture2D texture
		//Typically can only be applied to RGB24 or ARGB32 uncompressed textures
		//RGB24 is compressed with DXT1, ARGB32 is compressed with DXT5
		//Returns true if compressed, false if not
		if (Tex) {
			if ((Tex.format==TextureFormat.DXT1) || (Tex.format==TextureFormat.DXT5)) {
				return true;					//It's compressed
			} else {
				return false;					//It's not compressed
			}
		} else {
			return null;
		}
	}
	
	function TextureCompression(Tex:Texture2D, HighQuality:boolean) {
		//Apply DXT compression to an existing Texture2D texture
		//If HighQuality is true then the texture is dithered first to provide better results, but runs slower
		//Typically can only be applied to RGB24 or ARGB32 uncompressed textures
		//RGB24 is compressed with DXT1, ARGB32 is compressed with DXT5
		if (Tex) {
			Tex.Compress(HighQuality);
		}
	}


	function TexturePixel(Tex:Texture2D, X:int, Y:int, WithColor:Color, ApplyChanges:boolean) {
		//Draw a single pixel to a texture in a given color
		//Pixels will only upload to the actual texture if ApplyChanges=true
		if (Tex) {
			Tex.SetPixel(X,Y,WithColor);				//Draw pixel
			if (ApplyChanges==true) {Tex.Apply();}		//Apply the changes
		}
	}
	
	function TextureRect(Tex:Texture2D, X1:int, Y1:int, X2:int, Y2:int, WithColor:Color, ApplyChanges:boolean) {
		//Draw an axis-aligned rectangle within a texture in a given color
		//Pixels in X and X2 columns are included, pixels in Y and Y2 rows are included
		//Size of the rectangle will be cropped to fit within the texture
		//Pixels will only upload to the actual texture if ApplyChanges=true
		if (Tex) {
			var Wide:int=Tex.width;						//Texture width in pixels (row length)
			var High:int=Tex.height;					//Texture height in pixels
			if ( ((X1<0) && (X2<0)) || ((X1>=Wide) && (X2>=Wide)) || ((Y1<0) && (Y2<0)) || ((Y1>=High) && (Y2>=High)) ) {
				return;									//Nothing to draw
			}
			var Temp:int;
			if (X2<X1) {
				Temp=X1;								//Swap X coords
				X1=X2;
				X2=Temp;
			}
			if (Y2<Y1) {
				Temp=Y1;
				Y1=Y2;
				Y2=Temp;								//Swap Y coords
			}
			if (X1<0) {X1=0;}							//Clamp X
			if (X2>=Wide) {X2=Wide-1;}					//Clamp X2
			if (Y1<0) {Y1=0;}							//Clamp Y
			if (Y2>=High) {Y2=High-1;}					//Clamp Y2
			var Xpos:int;
			var Ypos:int;
			for (Ypos=Y1; Ypos<=Y2; Ypos++) {
				for (Xpos=X1; Xpos<=X2; Xpos++) {
					Tex.SetPixel(Xpos,Ypos,WithColor);	//Write float pixel
				}
			}
			if (ApplyChanges==true) {Tex.Apply();}		//Apply the changes
		}
	}
	
	function TextureBox(Tex:Texture2D, X1:int, Y1:int, X2:int, Y2:int, WithColor:Color, ApplyChanges:boolean) {
		//Draw a hollow box in a Texture in a given color
		//Pixels will only upload to the actual texture if ApplyChanges=true
		if (Tex) {
			var Wide:int=Tex.width;						//Texture width in pixels (row length)
			var High:int=Tex.height;					//Texture height in pixels
			if ( ((X1<0) && (X2<0)) || ((X1>=Wide) && (X2>=Wide)) || ((Y1<0) && (Y2<0)) || ((Y1>=High) && (Y2>=High)) ) {
				return;									//Nothing to draw
			}
			var Temp:int;
			if (X2<X1) {
				Temp=X1;								//Swap X coords
				X1=X2;
				X2=Temp;
			}
			if (Y2<Y1) {
				Temp=Y1;
				Y1=Y2;
				Y2=Temp;								//Swap Y coords
			}
			TextureRect(Tex,X1,Y1,X2,Y1,WithColor,false);		//Draw top
			TextureRect(Tex,X1,Y2,X2,Y2,WithColor,false);		//Draw bottom
			TextureRect(Tex,X1,Y1+1,X1,Y2-1,WithColor,false);	//Draw left
			TextureRect(Tex,X2,Y1+1,X2,Y2-1,WithColor,false);	//Draw right
			if (ApplyChanges==true) {Tex.Apply();}		//Apply the changes
		}
	}
	
	function TextureLine(Tex:Texture2D, X1:int, Y1:int, X2:int, Y2:int, WithColor:Color, ApplyChanges:boolean) {
		//Draw an aliased/pixel line at any angle in a Texture in a given color
		//Based on bresenham integer-math routine from Wikipedia
 		//Pixels will only upload to the actual texture if ApplyChanges=true
		if (Tex) {
 			var Wide:int=Tex.width;						//Width in pixels (row length)
 			var High:int=Tex.height;					//Height in pixels
 			var Steep:boolean;
 			var Temp:int;
 			var DeltaX:int;
 			var DeltaY:int;
 			var Error:int;
 			var DeltaError:int;
 			var X:int;
 			var Y:int;
 			var XStep:int;
 			var YStep:int;
 	 	 	Steep=Mathf.Abs(Y2-Y1) > Mathf.Abs(X2-X1);	//Boolean
			if (Steep) {
				Temp=X1;							//Swap X1,Y1
				X1=Y1;
				Y1=Temp;
				Temp=X2;							//Swap Y1,Y2
				X2=Y2;
				Y2=Temp;
			}
			DeltaX=Mathf.Abs(X2-X1);				//X Difference
			DeltaY=Mathf.Abs(Y2-Y1);				//Y Difference
			Error=0;								//Overflow counter
			DeltaError=DeltaY;						//Counter adder
			X=X1;									//Start at X1,Y1
			Y=Y1;		
			if (X1<X2) {							//Direction
				XStep=1;
			} else {
				XStep=-1;
			}
			if (Y1<Y2) {							//Direction
				YStep=1;
			} else {
				YStep=-1;
			}
			if ((X>=0) && (X<Wide) && (Y>=0) && (Y<High)) {
				if (Steep) {						//Draw
					Tex.SetPixel(Y,X,WithColor);
				} else {
					Tex.SetPixel(X,Y,WithColor);
				}
			}
			while (X!=X2) {
				X+=XStep;							//Move in X
				Error+=DeltaError;					//Add to counter
				if ((Error*2)>DeltaX) {				//Would it overflow?
					Y+=YStep;						//Move in Y
					Error-=DeltaX;					//Overflow/wrap the counter
				}
				if ((X>=0) && (X<Wide) && (Y>=0) && (Y<High)) {
					if (Steep) {						//Draw
						Tex.SetPixel(Y,X,WithColor);
					} else {
						Tex.SetPixel(X,Y,WithColor);
					}
				}
			}
 			if (ApplyChanges==true) {Tex.Apply();}		//Apply the changes
		}
	}

	function TextureEllipse(Tex:Texture2D, XCenter:int, YCenter:int, XRadius:int, YRadius:int, WithColor:Color, ApplyChanges:boolean) {
		//Draw a filled axis-aligned ellipse within a Texture using the given color
		//Centered at XCenter,YCenter with radii of XRadius and YRadius
		//Based on mid-point ellipse algorithm, almost entirely integer math
 		//Pixels will only upload to the actual texture if ApplyChanges=true
		if (Tex) {
 			var Wide:int=Tex.width;						//Width in pixels (row length)
 			var High:int=Tex.height;					//Height in pixels
 			var p:int;
 			var px:int;
 			var py:int;
 			var x:int;
 			var y:int;
 			var prevy:int;
			var Rx2:int;
			var Ry2:int;
			var twoRx2:int;
			var twoRy2:int;
			var pFloat:float;
			Rx2=XRadius*XRadius;
			Ry2=YRadius*YRadius;
			twoRx2=Rx2*2;
			twoRy2=Ry2*2;
			//Region 1
			x=0;
			y=YRadius;
			TextureRect(Tex,XCenter-XRadius,YCenter,(XCenter-XRadius)+(XRadius*2),YCenter,WithColor,false);
			pFloat=(Ry2-(Rx2*YRadius))+(0.25*Rx2);
			p=pFloat + (Mathf.Sign(pFloat)*0.5);
			px=0;
			py=twoRx2*y;
			while (px<py-1) {
				prevy=y;
				x+=1;
				px+=twoRy2;
				if (p>=0) {
					y-=1;
					py-=twoRx2;
				}
				if (p<0) {
					p+=Ry2+px;
				} else {
					p+=(Ry2+px-py);
				}
				if ((y<prevy) && (px<py-1)) {
					TextureRect(Tex,XCenter-x,YCenter+y,(XCenter-x)+(x*2),YCenter+y,WithColor,false);
					TextureRect(Tex,XCenter-x,YCenter-y,(XCenter-x)+(x*2),YCenter-y,WithColor,false);
				}
			}
			//Region 2
			pFloat=(Ry2*(x+0.5)*(x+0.5))+(Rx2*(y-1.0)*(y-1.0))-(Rx2*(Ry2));
			p=pFloat + (Mathf.Sign(pFloat)*0.5);
			y+=1;
			while (y>1) {
				y-=1;
				py-=twoRx2;
				if (p<=0) {
					x+=1;
					px+=twoRy2;
				}
				if (p>0) {
					p+=(Rx2-py);
				} else {
					p+=(Rx2-py+px);
				}
				TextureRect(Tex,XCenter-x,YCenter+y,(XCenter-x)+(x*2),YCenter+y,WithColor,false);
				TextureRect(Tex,XCenter-x,YCenter-y,(XCenter-x)+(x*2),YCenter-y,WithColor,false);
			}
 			if (ApplyChanges==true) {Tex.Apply();}		//Apply the changes
		}
	}
	
	function GrabPNG(FilePath:String){
		//Grab a screenshot of the backbuffer and save as a PNG file, exact dimensions
		//Suggested file format extension .png
	 	 Application.CaptureScreenshot(FilePath,0);
	}
	
	function GrabPNG(FilePath:String, Scale:int){
		//Grab a screenshot of the backbuffer and save as a PNG file, scaled to a larger size
		//Suggested file format extension .png
		//If Scale=1, normal size is grabbed. If Scale>1, double/triple/quad etc size is grabbed (if camera complies)
		//If Scale=0 a half-sized image (bilinear filtered) is grabbed
		
	 	if (Scale>0) {
			//Save regular size or larger, the quick way
	 		Application.CaptureScreenshot(FilePath,Scale);
	 		
		} else {
			//Save half-size with bilinear filtering, the slower way
			
			//Create a texture, RGB24 format (no alpha) for grabbing the screen
			var width:int;
			var height:int;
			var leftX:int;
			var topY:int;
			width = Screen.width;
			height = Screen.height;
			leftX = 0;
			topY = 0;
		    var tex = new Texture2D (width, height, TextureFormat.RGB24, false);
		
		    // Read the screen contents into the texture
		    tex.ReadPixels (Rect(leftX, topY, width, height), 0, 0);
			var Pixels:Color[] = tex.GetPixels(0);
			var NewWidth:int = width/2;
			var NewHeight:int = height/2;
			var NewPixels:Color[] = new Color[NewWidth * NewHeight];
			var YPos:int;
			var XPos:int;
			var OldRowStart:int;
			var NewOffset:int = 0;
			//Resize with bilinear filtering, slower but better quality
			var PixelOffset:int;
			for (YPos=0; YPos<NewHeight; YPos++) {
				//Do a row
				OldRowStart = (YPos * 2) * width;
				for (XPos=0; XPos<NewWidth; XPos++) {
					//Do a pixel
					PixelOffset = OldRowStart + (XPos * 2);
					NewPixels[NewOffset] = (Pixels[PixelOffset] + Pixels[PixelOffset +1] + Pixels[PixelOffset + width] + Pixels[PixelOffset + width + 1])/4.0; //Bilinear filtered read
					NewOffset+=1;
				}
			}
			Destroy(tex);		//Release texture memory
			tex = new Texture2D (NewWidth, NewHeight, TextureFormat.RGB24, false); //Create a new texture with the half size
			tex.SetPixels(NewPixels, 0);	//Upload to texture
		
		    // Encode texture into PNG
		    var bytes:byte[] = tex.EncodeToPNG();
		    Destroy (tex);	//release the texture memory
			
			// Write to a file
			File.WriteAllBytes(FilePath, bytes);
		}
	}
	
	function GrabJPG(FilePath:String) {
		//Grab a screen shot of the backbuffer and save it into a JPEG file
		//Suggested file format extension: .jpg
		//Defaults to JPEG quality 85%, normal scale
		GrabJPG(FilePath,85,1);				//Grab JPEG 85% quality, normal scale
	}

	function GrabJPG(FilePath:String, Quality:int) {
		//Grab a screen shot of the backbuffer and save it into a JPEG file
		//Suggested file format extension: .jpg
		//Quality ranges from 0 to 100, 100 is full quality
		GrabJPG(FilePath,Mathf.Clamp(Quality,1,100),1);				//Grab JPEG, normal scale
	}
	
	function GrabJPG(FilePath:String, Quality:int, Scale:int) {
		//Grab a screen shot of the backbuffer and save it into a JPEG file
		//Suggested file format extension: .jpg
		//Quality ranges from 0 to 100, 100 is full quality
		//If Scale=0, the image will be scaled down to half of its original size before being saved
		//If Scale>=1 the image will not be scaled (cannot scale up)
			
		//Create a texture, RGB24 format (no alpha) for grabbing the screen
		var width:int;
		var height:int;
		var leftX:int;
		var topY:int;
		width = Screen.width;
		height = Screen.height;
		leftX = 0;
		topY = 0;
	    var tex = new Texture2D (width, height, TextureFormat.RGB24, false);
	
	    // Read the screen contents into the texture - either full screen or half screen
	    tex.ReadPixels (Rect(leftX, topY, width, height), 0, 0);
	
		//Shrink image to half its dimensions
		var Pixels:Color[] = tex.GetPixels(0);
		var NewWidth:int = width/2;
		var NewHeight:int = height/2;
		var NewPixels:Color[] = new Color[NewWidth * NewHeight];
		var YPos:int;
		var XPos:int;
		var OldRowStart:int;
		var NewOffset:int = 0;
		//Resize with bilinear filtering, slower but better quality
		var PixelOffset:int;
		for (YPos=0; YPos<NewHeight; YPos++) {
			//Do a row
			OldRowStart = (YPos * 2) * width;
			for (XPos=0; XPos<NewWidth; XPos++) {
				//Do a pixel
				PixelOffset = OldRowStart + (XPos * 2);
				NewPixels[NewOffset] = (Pixels[PixelOffset] + Pixels[PixelOffset +1] + Pixels[PixelOffset + width] + Pixels[PixelOffset + width + 1])/4.0; //Bilinear filtered read
				NewOffset+=1;
			}
		}
		Destroy(tex);		//Release texture memory
		tex = new Texture2D (NewWidth, NewHeight, TextureFormat.RGB24, false); //Create a new texture with the half size
		tex.SetPixels(NewPixels, 0);		//Upload to texture
		
		//Create our JPEG encoder for this texture - utilizes JPEGEncoder.js which is derived from Adobe copyrighted/licensed sourcecode
		var encoder:JPGEncoder = new JPGEncoder(tex, Quality);
		
		//Encoder is threaded; wait for it to finish - the brute force unfriendly hogging way
		while(!encoder.isDone) {}			//Should really do yield WaitForSeconds(0.0001); here to yield for 1 millisecond between checks, but this causes the function to fail to execute, probably because multithreading then clashes with other programming
	
	    Destroy(tex);		//release the texture memory
		var bytes:byte[] = encoder.GetBytes();
		
		// Write to a file
		File.WriteAllBytes(FilePath, bytes);
	}
	

	function GrabRAW(FilePath:String) {
		//Grab a screen shot of the backbuffer and save it into a RAW file (raw data, not camera raw format)
		//Suggestion file format extension: .raw
		GrabRAW(FilePath,1);				//Grab RAW, normal scale
	}
	
	function GrabRAW(FilePath:String, Scale:int) {
		//Grab a screen shot of the backbuffer and save it into a RAW file (raw data, not camera raw format)
		//Suggestion file format extension: .raw
		//If Scale=0, the image will be scaled down to half of its original size before being saved
		//If Scale>=1 the image will not be scaled (cannot scale up)
	
		//Create a texture, RGB24 format (no alpha) for grabbing the screen
		var width:int;
		var height:int;
		var leftX:int;
		var topY:int;
		width = Screen.width;
		height = Screen.height;
		leftX = 0;
		topY = 0;
	    var tex = new Texture2D (width, height, TextureFormat.RGB24, false);
	
	    // Read the screen contents into the texture
	    tex.ReadPixels (Rect(leftX, topY, width, height), 0, 0);
	
		//Shrink image to half its dimensions
		var NewPixels:Color[];
		var Pixels:Color[] = tex.GetPixels(0);
		var NewWidth:int = width/2;
		var NewHeight:int = height/2;
		NewPixels = new Color[NewWidth * NewHeight];
		var YPos:int;
		var XPos:int;
		var OldRowStart:int;
		var NewOffset:int = 0;
		//Resize with bilinear filtering, slower but better quality
		var PixelOffset:int;
		for (YPos=0; YPos<NewHeight; YPos++) {
			//Do a row
			OldRowStart = (YPos * 2) * width;
			for (XPos=0; XPos<NewWidth; XPos++) {
				//Do a pixel
				PixelOffset = OldRowStart + (XPos * 2);
				NewPixels[NewOffset] = (Pixels[PixelOffset] + Pixels[PixelOffset +1] + Pixels[PixelOffset + width] + Pixels[PixelOffset + width + 1])/4.0; //Bilinear filtered read
				NewOffset+=1;
			}
		}
		width = NewWidth;				//Update so that below code refers to correct sized source
		height = NewHeight;
		Destroy(tex);					//release the texture memory
		var TotalColors:int = width * height;
		var bytes:byte[] = new byte[TotalColors * 3];
		
		//Convert colors to RGB8 byte stream
		var ColorOffset:int = 0;
		var ColorRowEnd:int;
		var ByteOffset:int = 0;
		var Row:int;
		var ColorsStart:int;
		for (Row=height-1; Row>=0; Row--) {
			//Do one row
			ColorsStart = Row * width;
			ColorRowEnd = ColorsStart + width;
			for (ColorOffset = ColorsStart; ColorOffset < ColorRowEnd; ColorOffset++) {
				//Do all pixels in the row from left to right
				bytes[ByteOffset] = Mathf.Floor(NewPixels[ColorOffset].r*255.0);
				bytes[ByteOffset+1] = Mathf.Floor(NewPixels[ColorOffset].g*255.0);
				bytes[ByteOffset+2] = Mathf.Floor(NewPixels[ColorOffset].b*255.0);
				ByteOffset+=3;
			}
		}
	
		// Write to a file
		File.WriteAllBytes(FilePath, bytes);
	}

	function GrabBMP(FilePath:String) {
		//Grab a screen shot of the backbuffer and save it into a BMP file
		//Suggested file format extension: .bmp
		GrabBMP(FilePath,1);			//Grab BMP, normal scale
	}
	
	function GrabBMP(FilePath:String, Scale:int) {
		//Grab a screen shot of the backbuffer and save it into a BMP file
		//Suggested file format extension: .bmp
		//If Scale=0, the image will be scaled down to half of its original size before being saved
		//If Scale>=1, the image will not be scaled (cannot scale up)
	
		//Create a texture, RGB24 format (no alpha) for grabbing the screen
		var width:int;
		var height:int;
		var leftX:int;
		var topY:int;
		width = Screen.width;
		height = Screen.height;
		leftX = 0;
		topY = 0;
	    var tex = new Texture2D (width, height, TextureFormat.RGB24, false);
	
	    // Read the screen contents into the texture
	    tex.ReadPixels (Rect(leftX, topY, width, height), 0, 0);
	
		//Shrink image to half its dimensions
		var NewPixels:Color[];
		var Pixels:Color[] = tex.GetPixels(0);
		var NewWidth:int = width/2;
		var NewHeight:int = height/2;
		NewPixels = new Color[NewWidth * NewHeight];
		var YPos:int;
		var XPos:int;
		var OldRowStart:int;
		var NewOffset:int = 0;
		//Resize with bilinear filtering, slower but better quality
		var PixelOffset:int;
		for (YPos=0; YPos<NewHeight; YPos++) {
			//Do a row
			OldRowStart = (YPos * 2) * width;
			for (XPos=0; XPos<NewWidth; XPos++) {
				//Do a pixel
				PixelOffset = OldRowStart + (XPos * 2);
				NewPixels[NewOffset] = (Pixels[PixelOffset] + Pixels[PixelOffset +1] + Pixels[PixelOffset + width] + Pixels[PixelOffset + width + 1])/4.0; //Bilinear filtered read
				NewOffset+=1;
				}
			}
		width = NewWidth;					//Update so that below code refers to correct sized source
		height = NewHeight;
		Destroy(tex);						//release the texture memory
		var TotalColors:int = width * height;
		var bytes:byte[] = new byte[TotalColors * 3];
		
		//Convert colors to RGB8 byte stream
		var ColorOffset:int = 0;
		var ColorRowEnd:int;
		var ByteOffset:int = 0;
		var Row:int;
		var ColorsStart:int;
		for (Row=0; Row<height; Row++) {
			//Do one row
			ColorsStart = Row * width;
			ColorRowEnd = ColorsStart + width;
			for (ColorOffset = ColorsStart; ColorOffset < ColorRowEnd; ColorOffset++) {
				//Do all pixels in the row from left to right
				bytes[ByteOffset] = Mathf.Floor(NewPixels[ColorOffset].r*255.0);
				bytes[ByteOffset+1] = Mathf.Floor(NewPixels[ColorOffset].g*255.0);
				bytes[ByteOffset+2] = Mathf.Floor(NewPixels[ColorOffset].b*255.0);
				ByteOffset+=3;
			}
		}
	
		// Write to a file
		//Create a binary stream for writing
		var Stream:FileStream = new FileStream(FilePath,FileMode.CreateNew);
		if (Stream) {
			var BinaryStream:BinaryWriter = new BinaryWriter(Stream);		//Convert to binary writer
			
			//Variables
			var HSize:int;
			var HOffset:int = 54;				//Header offset
			var Size:int = 40;				//Header size?
			var Wdth:int;
			var Hght:int;
			var Planes:int = 1;				//How many bitplanes (only 1 for truecolor)
			var Bits: int = 24;					//Number of bits per pixel
			var Compression:int = 0;			//Compression type, none
			var ISize:int = 40;				//Size?
			var XPels:int = 2834;				//Horizontal pixels per meter
			var YPels:int = 2834;				//Vertical pixels per meter
			var Cols:int = 0;					//Number of color indexes in color table
			var InUse: int = 0;					//Colors in use?
			var W:int;
			var X:int;
			var Y:int;
			
			//Calculate header values for BMP
			Wdth = width;						//Width of the bitmap in pixels
			Hght = height;						//Height of the bitmap in pixels
			W=Wdth*3;							//Number of bytes in a row of pixels, actual pixels only, no padding
			//if ((W % 4)!=0) {W+=(4-(W % 4));}	//Make the width into multiples of 4 bytes for alignment
			W=(W+3) & 0xfffc;
			HSize=(W*Hght)+54;					//Total size of header + pixel data + alignment padding

			//Write the header
			var Temp:byte;
			Temp=66;							//Convert B in ASCII/UTF8 to integer, #66
			BinaryStream.Write(Temp);			//BM means BITMAP
			Temp=77;							//Convert M in ASCII/UTF8 to integer, #77
			BinaryStream.Write(Temp);			//BM means BITMAP
			BinaryStream.Write(HSize);
		 	var Temp2:int;
		 	Temp2=0;
			BinaryStream.Write(Temp2);
			BinaryStream.Write(HOffset);
			BinaryStream.Write(Size);
			BinaryStream.Write(Wdth);
			BinaryStream.Write(Hght);
			Temp=Planes;
			BinaryStream.Write(Temp);
			Temp=0;								//Short not int, little endian
			BinaryStream.Write(Temp);
			Temp=Bits;
			BinaryStream.Write(Temp);
			Temp=0;								//Short not int, little endian
			BinaryStream.Write(Temp);
			BinaryStream.Write(Compression);
			BinaryStream.Write(ISize);
			BinaryStream.Write(XPels);
			BinaryStream.Write(YPels);
			BinaryStream.Write(Cols);
			BinaryStream.Write(InUse);

			//Write the pixel data
			var Offset:int = 0;				//Position in source
			for (Y=Hght-1; Y>=0; Y--) {
				//Process each row from bottom to top
				for (X=0; X<W; X+=3) {
					//Process each pixel - convert RGB to BGR
					BinaryStream.Write(bytes[Offset+X+2]);		//Copy blue to red
					BinaryStream.Write(bytes[Offset+X+1]);		//Copy green to green
					BinaryStream.Write(bytes[Offset+X]);		//Copy red to blue
				}
				Offset+=W;						//Next row of source bytes, no padding
			}
			BinaryStream.Close();
		}
		Stream.Close();		//Done
	}

	function CreatePixmap() {
		//Create a new pixmap measuring Screen.width x Screen.height pixels in RGBA8 format (Color32[] array)
		var NewPixmap:Pixmap = new Pixmap(Screen.width, Screen.height, 1);		//Create a pixmap
		return NewPixmap;				//Created it, return it
	}
	
	function CreatePixmap(RGBA8:boolean) {
		//Create a new pixmap measuring Screen.width x Screen.height pixels
		//If RGBA8 is True then the pixmap will be in RGBA8 format, ie 4 bytes per pixel as a Color32[] array
		//If RGBA8 is False then the pixmap will be in RGBA32 format, ie 4 floats per pixel as a Color[] array
		var Format:int;
		if (RGBA8==true) {
			Format=1;					//Color32[] array
		} else {
			Format=0;					//Color[] array
		}
		var NewPixmap:Pixmap = new Pixmap(Screen.width, Screen.height, Format);		//Create a pixmap
		return NewPixmap;				//Created it, return it
	}
	
	function CreatePixmap(Width:int, Height:int) {
		//Create a new pixmap measuring width x height pixels in RGBA8 format (color32[] array)
		var NewPixmap:Pixmap = new Pixmap(Width, Height, 1);		//Create a pixmap
		return NewPixmap;				//Created it, return it
	}
	
	function CreatePixmap(Width:int, Height:int, RGBA8:boolean) {
		//Create a new pixmap measuring width x height pixels
		//If RGBA8 is True then the pixmap will be in RGBA8 format, ie 4 bytes per pixel as a Color32[] array
		//If RGBA8 is False then the pixmap will be in RGBA32 format, ie 4 floats per pixel as a Color[] array
		var Format:int;
		if (RGBA8==true) {
			Format=1;					//Color32[] array
		} else {
			Format=0;					//Color[] array
		}
		var NewPixmap:Pixmap = new Pixmap(Width, Height, Format);		//Create a pixmap
		return NewPixmap;				//Created it, return it
	}
		
	function FreePixmap(PixmapToFree:Pixmap) {
		//Free a previously created pixmap - frees up the memory
		//To really Free it all references to the Pixmap instance must be nulled so the Garbage Collector can remove it
		if (PixmapToFree) {
			PixmapToFree.Width=0;
			PixmapToFree.Height=0;
			PixmapToFree.Format=0;
			PixmapToFree.TotalPixels=0;
			PixmapToFree.Pixels = null;
			PixmapToFree.Pixels32 = null;
		}
	}
	
	function ClearPixmap(PixmapToClear:Pixmap) {
		//Erase the contents of all pixels in the pixmap - they will be set to zero's (black with no alpha)
		if (PixmapToClear) {
			var pos:int=0;
			var TotalPixels:int = PixmapToClear.TotalPixels;		//For speed
			if (PixmapToClear.Format==0) {
				//Float format
				var Pixels:Color[] = PixmapToClear.Pixels;		//For speed
				var ColorWipe:Color = Color(0.0,0.0,0.0,0.0);
				for (pos=0; pos<TotalPixels; pos++) {
					Pixels[pos]=ColorWipe;			//Clear to black, no alpha
				}
			} else {
				//Byte format
				var Pixels32:Color32[] = PixmapToClear.Pixels32;	//For speed
				var ColorWipe32:Color32 = Color32(0, 0, 0, 0);
				for (pos=0; pos<TotalPixels; pos++) {
					Pixels32[pos]=ColorWipe32;				//Clear to black, no alpha
				}
			}
		}
	}
	
	function ClearPixmap(PixmapToClear:Pixmap, Red:float, Green:float, Blue:float, Alpha:float) {
		//Erase the contents of all pixels in the pixmap - they will be set the color values specified
		//Expects to be used with a Color[] float format pixmap
		if (PixmapToClear) {
			if (PixmapToClear.Format==1) {
				ClearPixmap(PixmapToClear, Red*255, Green*255, Blue*255, Alpha*255);
				return;												//Get out of here, deferred to other function
			}
			var pos:int=0;
			var TotalPixels:int = PixmapToClear.TotalPixels;		//For speed
			//Float format
			var Pixels:Color[] = PixmapToClear.Pixels;		//For speed
			var ColorWipe:Color = Color(Red, Green, Blue, Alpha);
			for (pos=0; pos<TotalPixels; pos++) {
				Pixels[pos]=ColorWipe;		//Clear to color and alpha
			}
		}
	}
	
	function ClearPixmap(PixmapToClear:Pixmap, Red:byte, Green:byte, Blue:byte, Alpha:byte) {
		//Erase the contents of all pixels in the pixmap - they will be set the color values specified
		//Expects to be used with a Color32[] byte format pixmap
		if (PixmapToClear) {
			if (PixmapToClear.Format==0) {
				ClearPixmap(PixmapToClear, Red/255.0, Green/255.0, Blue/255.0, Alpha/255.0);
				return;												//Get out of here, deferred to other function
			}
			var pos:int=0;
			var TotalPixels:int = PixmapToClear.TotalPixels;		//For speed
			//Float format
			var Pixels32:Color32[] = PixmapToClear.Pixels32;		//For speed
			var ColorWipe:Color32 =Color32(Red, Green, Blue, Alpha);
			for (pos=0; pos<TotalPixels; pos++) {
				Pixels32[pos]=ColorWipe;		//Clear to color and alpha
			}
		}
	}
	
	function ConvertPixmap(PixmapToConvert:Pixmap, Format:int) {
		//Convert a pixmap between Color[] and Color32[] formats
		//Format=0 means Color[] float array
		//Format=1 means Color32[] byte array
		//A new array will be created, the data is converted, then the old array is released
		if (PixmapToConvert) {
			var OldFormat:int = PixmapToConvert.Format;				//For speed
			if (OldFormat != Format) {
				//Format is different, need to convert it, otherwise do nothing as it's already in the requested format
				var TotalPixels:int = PixmapToConvert.TotalPixels;	//For speed
				var pos:int = 0;
				var Pixels:Color[];
				var Pixels32:Color32[];
				if (Format==1) {
					//Convert Color[] to Color32[]
					PixmapToConvert.Pixels32 = new Color32[TotalPixels];	//Create new Color32[] array
					Pixels32 = PixmapToConvert.Pixels32;				//For speed
					Pixels = PixmapToConvert.Pixels;					//For speed
					var Pixel:Color;
					for (pos=0; pos<TotalPixels; pos++) {
						Pixel = Pixels[pos];							//For speed
						Pixels32[pos]=Color32(Pixel.r*255, Pixel.g*255, Pixel.b*255, Pixel.a*255);		//Convert it
					}
					PixmapToConvert.Pixels = null;						//Free the old pixels
					PixmapToConvert.Format = 1;							//Color32[] format
				} else {
					//Convert Color32[] to Color[]
					PixmapToConvert.Pixels = new Color[TotalPixels];	//Create new Color[] array
					Pixels = PixmapToConvert.Pixels;					//For speed
					Pixels32 = PixmapToConvert.Pixels32;				//For speed
					var Pixel32:Color32;
					for (pos=0; pos<TotalPixels; pos++) {
						Pixel32 = Pixels32[pos];							//For speed
						Pixels[pos]=Color(Pixel32.r/255.0, Pixel32.g/255.0, Pixel32.b/255.0, Pixel32.a/255.0);		//Convert it
					}
					PixmapToConvert.Pixels32 = null;					//Free the old pixels
					PixmapToConvert.Format = 0;							//Color[] format
				}
			}
		}
	}
	
	function ClonePixmap(SourcePixmap:Pixmap) {
		//Create a duplicate copy of a pixmap including its pixel data in the same format
		//Returns the new pixmap
		if (SourcePixmap) {
			var Width:int = SourcePixmap.Width;
			var Height:int = SourcePixmap.Height;
			var Format:int = SourcePixmap.Format;
			var TotalPixels:int = SourcePixmap.TotalPixels;
			var NewPixmap:Pixmap = new Pixmap(Width, Height, Format);
			var pos:int;
			if (Format==0) {
				//Copy Color[] float pixel data
				var SourceColor:Color[] = SourcePixmap.Pixels;			//For speed
				var DestColor:Color[] = NewPixmap.Pixels;				//For speed
				for (pos=0; pos<TotalPixels; pos++) {
					DestColor[pos]=SourceColor[pos];					//Copy a Color (4 floats)
				}
			} else {
				//Copy Color32[] byte pixel data
				var SourceColor32:Color32[] = SourcePixmap.Pixels32;	//For speed
				var DestColor32:Color32[] = NewPixmap.Pixels32;			//For speed
				for (pos=0; pos<TotalPixels; pos++) {
					DestColor32[pos]=SourceColor32[pos];				//Copy a Color (4 bytes)
				}
			}
			return NewPixmap;				//Done, send it back
		} else {
			return null;
		}
	}

	function CopyPixmap(SourcePixmap:Pixmap, DestPixmap:Pixmap) {
		//Copy pixels from one pixmap to another
		//Pixmaps must both be the same size
		if (SourcePixmap && DestPixmap) {
			var SWidth:int = SourcePixmap.Width;
			var SHeight:int = SourcePixmap.Height;
			var DWidth:int = DestPixmap.Width;
			var DHeight:int = DestPixmap.Height;
			if ((SWidth != DWidth) || (SHeight != DHeight)) {return;}
			var SFormat:int = SourcePixmap.Format;
			var STotalPixels:int = SourcePixmap.TotalPixels;
			var DFormat:int = DestPixmap.Format;
			var DTotalPixels:int = DestPixmap.TotalPixels;
			var pos:int;
			var SourceColor:Color[];
			var SourceColor32:Color32[];
			var DestColor:Color[];
			var DestColor32:Color32[];
			if (SFormat==0) {
				//Copy Color[] float pixel data
				if (DFormat==0) {
					//Copy Color[] to Color[]
					SourceColor = SourcePixmap.Pixels;					//For speed
					DestColor = DestPixmap.Pixels;						//For speed
					for (pos=0; pos<STotalPixels; pos++) {
						DestColor[pos]=SourceColor[pos];				//Copy a Color (4 floats)
					}
				} else {
					//Copy Color[] to Color32[]
					SourceColor = SourcePixmap.Pixels;					//For speed
					DestColor32 = DestPixmap.Pixels32;					//For speed
					var Pixel:Color;
					for (pos=0; pos<STotalPixels; pos++) {
						Pixel=SourceColor[pos];
						DestColor32[pos]=Color32(Pixel.r*255,Pixel.g*255,Pixel.b*255,Pixel.a*255);		//Copy a Color (4 floats) to a Color32 (4 bytes)
					}
				}
			} else {
				//Copy Color32[] byte pixel data
				if (DFormat==1) {
					//Copy Color32 to Color32
					SourceColor32 = SourcePixmap.Pixels32;					//For speed
					DestColor32 = DestPixmap.Pixels32;						//For speed
					for (pos=0; pos<STotalPixels; pos++) {
						DestColor32[pos]=SourceColor32[pos];				//Copy a Color32 (4 bytes)
					}	
				} else {
					//Copy Color32 to Color
					SourceColor32 = SourcePixmap.Pixels32;					//For speed
					DestColor = DestPixmap.Pixels;							//For speed
					var Pixel32:Color32;
					for (pos=0; pos<STotalPixels; pos++) {
						Pixel32=SourceColor32[pos];
						DestColor32[pos]=Color(Pixel32.r/255.0,Pixel32.g/255.0,Pixel32.b/255.0,Pixel32.a/255.0);	//Copy a Color32 (4 bytes) to a Color (4 floats)
					}
				}
			}
		}
	}

	function CopyPixmap(SourcePixmap:Pixmap, DestPixmap:Pixmap, SourceX:int, SourceY:int, SourceWidth:int, SourceHeight:int, DestX:int, DestY:int) {
		//Copy an area of pixels from one pixmap to another
		//Does not perform bounds error checking, make sure you only copy from a source rectangle that fits within the source Pixmap,
		//and make sure you have enough room in the destination Pixmap to accept the entire SourceWidth x SourceHeight pixels being copied.
		if (SourcePixmap && DestPixmap) {
			var SWidth:int = SourcePixmap.Width;
			var SHeight:int = SourcePixmap.Height;
			var DWidth:int = DestPixmap.Width;
			var DHeight:int = DestPixmap.Height;
			var SFormat:int = SourcePixmap.Format;
			var STotalPixels:int = SourcePixmap.TotalPixels;
			var DFormat:int = DestPixmap.Format;
			var DTotalPixels:int = DestPixmap.TotalPixels;
			var Xpos:int;
			var Ypos:int;
			var SourceColor:Color[];
			var SourceColor32:Color32[];
			var DestColor:Color[];
			var DestColor32:Color32[];
			if (SFormat==0) {
				//Copy Color[] float pixel data
				if (DFormat==0) {
					//Copy Color[] to Color[]
					SourceColor = SourcePixmap.Pixels;						//For speed
					DestColor = DestPixmap.Pixels;							//For speed
					for (Ypos=0; Ypos<SourceHeight; Ypos++) {
						for (Xpos=0; Xpos<SourceWidth; Xpos++) {
							DestColor[(Ypos*DWidth)+Xpos+DestX]=SourceColor[(Ypos*SWidth)+Xpos+SourceX];	//Copy a Color (4 floats)
						}
					}
				} else {
					//Copy Color[] to Color32[]
					SourceColor = SourcePixmap.Pixels;						//For speed
					DestColor32 = DestPixmap.Pixels32;						//For speed
					var Pixel:Color;
					for (Ypos=0; Ypos<SourceHeight; Ypos++) {
						for (Xpos=0; Xpos<SourceWidth; Xpos++) {
							Pixel=SourceColor[(Ypos*SWidth)+Xpos+SourceX];
							DestColor32[(Ypos*DWidth)+Xpos+DestX]=Color32(Pixel.r*255,Pixel.g*255,Pixel.b*255,Pixel.a*255);	//Copy a Color (4 floats) to a Color32 (4 bytes)
						}
					}

				}
			} else {
				//Copy Color32[] byte pixel data
				if (DFormat==1) {
					//Copy Color32 to Color32
					SourceColor32 = SourcePixmap.Pixels32;					//For speed
					DestColor32 = DestPixmap.Pixels32;						//For speed
					for (Ypos=0; Ypos<SourceHeight; Ypos++) {
						for (Xpos=0; Xpos<SourceWidth; Xpos++) {
							DestColor32[(Ypos*DWidth)+Xpos+DestX]=SourceColor32[(Ypos*SWidth)+Xpos+SourceX];	//Copy a Color32 (4 bytes)
						}
					}
				} else {
					//Copy Color32 to Color
					SourceColor32 = SourcePixmap.Pixels32;					//For speed
					DestColor = DestPixmap.Pixels;							//For speed
					var Pixel32:Color32;	
					for (Ypos=0; Ypos<SourceHeight; Ypos++) {
						for (Xpos=0; Xpos<SourceWidth; Xpos++) {
							Pixel32=SourceColor32[(Ypos*SWidth)+Xpos+SourceX];
							DestColor32[(Ypos*DWidth)+Xpos+DestX]=Color(Pixel32.r/255.0,Pixel32.g/255.0,Pixel32.b/255.0,Pixel32.a/255.0);	//Copy a Color32 (4 bytes) to a Color (4 floats)
						}
					}
				}
			}
		}
	}

	function PixmapWidth(PixmapToGet:Pixmap) {
		//Return the width of a pixmap
		if (PixmapToGet) {
			return PixmapToGet.Width;
		} else {
			return null;
		}
	}
	
	function PixmapHeight(PixmapToGet:Pixmap) {
		//Return the height of a pixmap
		if (PixmapToGet) {
			return PixmapToGet.Height;
		} else {
			return null;
		}
	}
	
	function PixmapFormat(PixmapToGet:Pixmap) {
		//Return the format of a pixmap
		//0=Color[] float format
		//1=Color32[] byte format
		//-1 = no pixmap
		if (PixmapToGet) {
			return PixmapToGet.Format;
		} else {
			return null;
		}
	}
	
	function PixmapFormat(PixmapToChange:Pixmap, NewFormat:int) {
		//Change the format of a pixmap to a new format
		//This is really a wrapper for ConvertPixmap, keeping with consistency of calling
		ConvertPixmap(PixmapToChange, NewFormat);
	}
	
	function PixmapBytes(PixmapToMeasure:Pixmap) {
		//Return how many bytes the pixels in a pixmap consume
		if (PixmapToMeasure) {
			if (PixmapToMeasure.Format==0) {
				return PixmapToMeasure.TotalPixels*16;		//Color[] format, 16 bytes per pixel, 4 floats
			} else {
				return PixmapToMeasure.TotalPixels*4;		//Color32[] format, 4 bytes per pixel, 4 bytes
			}
		} else {
			return null;			//No pixmap
		}
	}
	
	function XFlipPixmap(Pix:Pixmap) {
		//Horizontally flip the columns in a Pixmap, ie flip the image side-to-side
		//Works in place with no extra buffers, does not create a second Pixmap so use ClonePixmap first if you need to preserve the original
		if (Pix) {
			var Wide:int=Pix.Width;						//Width in pixels (row length)
			if (Wide>1) {
				//At least 2 columns to swap
				var Half:int=Wide/2;					//How many columns in half the Pixmap
				var X:int;
				var Y:int;
				var High:int=Pix.Height;				//Height in pixels
				var OppositeX:int;
				var ThisY:int;
				if (Pix.Format==0) {
					//Color[] float format
					var Pixel:Color;
					var Pixels:Color[]=Pix.Pixels;		//For speed
					for (Y=0; Y<High; Y++) {
						ThisY=Y*Wide;					//Start of this row as pixel offset
						for (X=0; X<Half; X++) {
	 						OppositeX=(Wide-1)-X;		//Opposite column
							Pixel=Pixels[ThisY+X];		//Keep this pixel
							Pixels[ThisY+X]=Pixels[ThisY+OppositeX];		//Swap this pixel with opposite pixel
							Pixels[ThisY+OppositeX]=Pixel;	//Restore original pixel
						}
					}
				} else {
					//Color32[] byte format
					var Pixel32:Color32;
					var Pixels32:Color32[]=Pix.Pixels32;//For speed
					for (Y=0; Y<High; Y++) {
						ThisY=Y*Wide;					//Start of this row as pixel offset
						for (X=0; X<Half; X++) {
							OppositeX=(Wide-1)-X;		//Opposite column
							Pixel32=Pixels32[ThisY+X];	//Keep this pixel
							Pixels32[ThisY+X]=Pixels32[ThisY+OppositeX];	//Swap this pixel with opposite pixel
							Pixels32[ThisY+OppositeX]=Pixel32;	//Restore original pixel
						}
					}
				}
			}
		}
	}
	
	function YFlipPixmap(Pix:Pixmap) {
		//Vertically flip the rows in a Pixmap, ie turn the image upside down
		//Works in place with no extra buffers, does not create a second Pixmap so use ClonePixmap first if you need to preserve the original
		//This may be useful for preparing pixmaps for upload to a texture since textures have an opposite Y coordinate system
		if (Pix) {
			var High:int=Pix.Height;					//Height in rows
			if (High>1) {
				//At least 2 rows to swap
				var Half:int=High/2;					//How many rows in half the Pixmap
				var X:int;
				var Y:int;
				var Wide:int=Pix.Width;					//Width in pixels (row length)
				var OppositeY:int;
				var ThisY:int;
				if (Pix.Format==0) {
					//Color[] float format
					var Pixel:Color;
					var Pixels:Color[]=Pix.Pixels;		//For speed
					for (Y=0; Y<Half; Y++) {
						ThisY=Y*Wide;					//Start of this row as pixel offset
						OppositeY=((High-1)-Y)*Wide;	//Opposite row start as pixel offset
						for (X=0; X<Wide; X++) {
							Pixel=Pixels[ThisY+X];		//Keep this pixel
							Pixels[ThisY+X]=Pixels[OppositeY+X];		//Swap this pixel with opposite pixel
							Pixels[OppositeY+X]=Pixel;	//Restore original pixel
						}
					}
				} else {
					//Color32[] byte format
					var Pixel32:Color32;
					var Pixels32:Color32[]=Pix.Pixels32;//For speed
					for (Y=0; Y<Half; Y++) {
						ThisY=Y*Wide;					//Start of this row as pixel offset
						OppositeY=((High-1)-Y)*Wide;	//Opposite row start as pixel offset
						for (X=0; X<Wide; X++) {
							Pixel32=Pixels32[ThisY+X];		//Keep this pixel
							Pixels32[ThisY+X]=Pixels32[OppositeY+X];	//Swap this pixel with opposite pixel
							Pixels32[OppositeY+X]=Pixel32;	//Restore original pixel
						}
					}
				}
			}
		}
	}
	
	function PixmapPixelCount(PixmapToMeasure:Pixmap) {
		//Return how many pixels the pixmap contains
		if (PixmapToMeasure) {
			if (PixmapToMeasure.Format==0) {
				return PixmapToMeasure.TotalPixels;		//Color[] format
			} else {
				return PixmapToMeasure.TotalPixels;		//Color32[] format
			}
		} else {
			return null;			//No pixmap
		}
	}
	
	function PixmapPixels(PixmapToGet:Pixmap) {
		//Return the Color[] or Color32[] array containing pixel data for this pixmap
		if (PixmapToGet) {
			if (PixmapToGet.Format==0) {
				return PixmapToGet.Pixels;				//Return Color[]
			} else {
				return PixmapToGet.Pixels32;			//Return Color32[]
			}
		} else {
			return null;
		}
	}
	
	function PixmapPixel(PixmapToAccess:Pixmap, XPos:int, YPos:int) {
		//Read a pixel from a pixmap
		if (PixmapToAccess) {
			if ((XPos<0) || (YPos<0) || (XPos>=PixmapToAccess.Width) || (YPos>=PixmapToAccess.Height)) {
				return Color(0,0,0,0);					//Outside bounds, return black with no alpha
			} else {
				if (PixmapToAccess.Format==0) {
					return PixmapToAccess.Pixels[XPos+(YPos*PixmapToAccess.Width)];	//Return Color float pixel
				} else {
					return PixmapToAccess.Pixels32[XPos+(YPos*PixmapToAccess.Width)];	//Return Color32 byte pixel
				}
			}
		} else {
			return null;
		}
	}
	
	function PixmapPixel(PixmapToAccess:Pixmap, XPos:int, YPos:int, Pixel:Color) {
		//Write a Color float pixel to a pixmap
		if (PixmapToAccess) {
			if ((XPos<0) || (YPos<0) || (XPos>=PixmapToAccess.Width) || (YPos>=PixmapToAccess.Height)) {
				return;									//Outside bounds, return
			} else {
				if (PixmapToAccess.Format==0) {
					PixmapToAccess.Pixels[XPos+(YPos*PixmapToAccess.Width)]=Pixel;	//Write Color float pixel
				} else {
					PixmapToAccess.Pixels32[XPos+(YPos*PixmapToAccess.Width)]=Color32(Pixel.r*255, Pixel.g*255, Pixel.b*255, Pixel.a*255);	//Write Color32 byte pixel
				}
			}
		}
	}
	
	function PixmapPixel(PixmapToAccess:Pixmap, XPos:int, YPos:int, Pixel:Color32) {
		//Write a Color32 byte pixel to a pixmap
		if (PixmapToAccess) {
			if ((XPos<0) || (YPos<0) || (XPos>=PixmapToAccess.Width) || (YPos>=PixmapToAccess.Height)) {
				return;									//Outside bounds, return
			} else {
				if (PixmapToAccess.Format==0) {
					PixmapToAccess.Pixels[XPos+(YPos*PixmapToAccess.Width)]=Color(Pixel.r/255.0, Pixel.g/255.0, Pixel.b/255.0, Pixel.a/255.0);	//Write Color float pixel
				} else {
					PixmapToAccess.Pixels32[XPos+(YPos*PixmapToAccess.Width)]=Pixel;	//Write Color32 byte pixel
				}
			}
		}
	}
	
	function PixmapPixel(PixmapToAccess:Pixmap, XPos:int, YPos:int, Red:float, Green:float, Blue:float, Alpha:float) {
		//Write a Color float pixel to a pixmap
		if (PixmapToAccess) {
			if ((XPos<0) || (YPos<0) || (XPos>=PixmapToAccess.Width) || (YPos>=PixmapToAccess.Height)) {
				return;									//Outside bounds, return
			} else {
				if (PixmapToAccess.Format==0) {
					PixmapToAccess.Pixels[XPos+(YPos*PixmapToAccess.Width)]=Color(Red, Green, Blue, Alpha);	//Write Color float pixel
				} else {
					PixmapToAccess.Pixels32[XPos+(YPos*PixmapToAccess.Width)]=Color32(Red*255, Green*255, Blue*255, Alpha*255);	//Write Color32 byte pixel
				}
			}
		}
	}
	
	function PixmapPixel(PixmapToAccess:Pixmap, XPos:int, YPos:int, Red:byte, Green:byte, Blue:byte, Alpha:byte) {
		//Write a Color32 byte pixel to a pixmap
		if (PixmapToAccess) {
			if ((XPos<0) || (YPos<0) || (XPos>=PixmapToAccess.Width) || (YPos>=PixmapToAccess.Height)) {
				return;									//Outside bounds, return
			} else {
				if (PixmapToAccess.Format==0) {
					PixmapToAccess.Pixels[XPos+(YPos*PixmapToAccess.Width)]=Color(Red/255.0, Green/255.0, Blue/255.0, Alpha/255.0);	//Write Color float pixel
				} else {
					PixmapToAccess.Pixels32[XPos+(YPos*PixmapToAccess.Width)]=Color32(Red, Green, Blue, Alpha);	//Write Color32 byte pixel
				}
			}
		}
	}
	
	function PixmapPixelFast(PixmapToAccess:Pixmap, XPos:int, YPos:int) {
		//Read a pixel from a pixmap without any safety checks, for speed
		//Only works with Color[] float format pixmaps
		//May throw a runtime error if trying to access a Color32[] format pixmap or a pixmap doesn't exist or the coords are out of bounds
		return PixmapToAccess.Pixels[XPos+(YPos*PixmapToAccess.Width)];	//Return Color float pixel
	}
	
	function PixmapPixelFast(PixmapToAccess:Pixmap, XPos:int, YPos:int, Pixel:Color) {
		//Write a Color float pixel to a pixmap without any safety checks, for speed
		//Only works with Color[] float format pixmaps
		//May throw a runtime error if trying to access a Color32[] format pixmap or a pixmap doesn't exist or the coords are out of bounds
		PixmapToAccess.Pixels[XPos+(YPos*PixmapToAccess.Width)]=Pixel;	//Write Color float pixel
	}
	
	function PixmapPixelFast(PixmapToAccess:Pixmap, XPos:int, YPos:int, Pixel:Color32) {
		//Write a Color32 byte pixel to a pixmap without any safety checks, for speed
		//Only works with Color[] float format pixmaps
		//May throw a runtime error if trying to access a Color32[] format pixmap or a pixmap doesn't exist or the coords are out of bounds
		PixmapToAccess.Pixels[XPos+(YPos*PixmapToAccess.Width)]=Color(Pixel.r/255.0, Pixel.g/255.0, Pixel.b/255.0, Pixel.a/255.0);	//Write Color float pixel
	}
	
	function PixmapPixelFast(PixmapToAccess:Pixmap, XPos:int, YPos:int, Red:float, Green:float, Blue:float, Alpha:float) {
		//Write a Color float pixel to a pixmap without any safety checks, for speed
		//Only works with Color[] float format pixmaps
		//May throw a runtime error if trying to access a Color32[] format pixmap or a pixmap doesn't exist or the coords are out of bounds
		PixmapToAccess.Pixels[XPos+(YPos*PixmapToAccess.Width)]=Color(Red, Green, Blue, Alpha);	//Write Color float pixel
	}
	
	function PixmapPixelFast(PixmapToAccess:Pixmap, XPos:int, YPos:int, Red:byte, Green:byte, Blue:byte, Alpha:byte) {
		//Write a Color32 byte pixel to a pixmap without any safety checks, for speed
		//Only works with Color[] float format pixmaps
		//May throw a runtime error if trying to access a Color32[] format pixmap or a pixmap doesn't exist or the coords are out of bounds
		PixmapToAccess.Pixels[XPos+(YPos*PixmapToAccess.Width)]=Color(Red/255.0, Green/255.0, Blue/255.0, Alpha/255.0);	//Write Color float pixel
	}
	
	function PixmapPixelFast32(PixmapToAccess:Pixmap, XPos:int, YPos:int) {
		//Read a pixel from a pixmap without any safety checks, for speed
		//Only works with Color32[] byte format pixmaps
		//May throw a runtime error if trying to access a Color[] format pixmap or a pixmap doesn't exist or the coords are out of bounds
		return PixmapToAccess.Pixels32[XPos+(YPos*PixmapToAccess.Width)];	//Return Color32 byte pixel
	}
	
	function PixmapPixelFast32(PixmapToAccess:Pixmap, XPos:int, YPos:int, Pixel:Color) {
		//Write a Color32 float pixel to a pixmap without any safety checks, for speed
		//Only works with Color32[] byte format pixmaps
		//May throw a runtime error if trying to access a Color[] format pixmap or a pixmap doesn't exist or the coords are out of bounds
		PixmapToAccess.Pixels32[XPos+(YPos*PixmapToAccess.Width)]=Color32(Pixel.r*255, Pixel.g*255, Pixel.b*255, Pixel.a*255);	//Write Color32 byte pixel
	}
	
	function PixmapPixelFast32(PixmapToAccess:Pixmap, XPos:int, YPos:int, Pixel:Color32) {
		//Write a Color32 float pixel to a pixmap without any safety checks, for speed
		//Only works with Color32[] byte format pixmaps
		//May throw a runtime error if trying to access a Color[] format pixmap or a pixmap doesn't exist or the coords are out of bounds
		PixmapToAccess.Pixels32[XPos+(YPos*PixmapToAccess.Width)]=Pixel;	//Write Color32 byte pixel
	}
	
	function PixmapPixelFast32(PixmapToAccess:Pixmap, XPos:int, YPos:int, Red:float, Green:float, Blue:float, Alpha:float) {
		//Write a Color32 float pixel to a pixmap without any safety checks, for speed
		//Only works with Color32[] byte format pixmaps
		//May throw a runtime error if trying to access a Color[] format pixmap or a pixmap doesn't exist or the coords are out of bounds
		PixmapToAccess.Pixels32[XPos+(YPos*PixmapToAccess.Width)]=Color32(Red*255, Green*255, Blue*255, Alpha*255);	//Write Color32 byte pixel
	}
	
	function PixmapPixelFast32(PixmapToAccess:Pixmap, XPos:int, YPos:int, Red:byte, Green:byte, Blue:byte, Alpha:byte) {
		//Write a Color32 float pixel to a pixmap without any safety checks, for speed
		//Only works with Color32[] byte format pixmaps
		//May throw a runtime error if trying to access a Color[] format pixmap or a pixmap doesn't exist or the coords are out of bounds
		PixmapToAccess.Pixels32[XPos+(YPos*PixmapToAccess.Width)]=Color32(Red, Green, Blue, Alpha);	//Write Color32 byte pixel
	}
	
	function PixmapRect(Pix:Pixmap, X1:int, Y1:int, X2:int, Y2:int, WithColor:Color) {
		//Draw an axis-aligned rectangle within a pixmap in a given color
		//Pixels in X and X2 columns are included, pixels in Y and Y2 rows are included
		//Size of the rectangle will be cropped to fit within the Pixmap
		if (Pix) {
			var Wide:int=Pix.Width;						//Pixmap width in pixels (row length)
			var High:int=Pix.Height;					//Pixmap height in pixels
			if ( ((X1<0) && (X2<0)) || ((X1>=Wide) && (X2>=Wide)) || ((Y1<0) && (Y2<0)) || ((Y1>=High) && (Y2>=High)) ) {
				return;									//Nothing to draw
			}
			var Temp:int;
			if (X2<X1) {
				Temp=X1;								//Swap X coords
				X1=X2;
				X2=Temp;
			}
			if (Y2<Y1) {
				Temp=Y1;
				Y1=Y2;
				Y2=Temp;								//Swap Y coords
			}
			if (X1<0) {X1=0;}							//Clamp X
			if (X2>=Wide) {X2=Wide-1;}					//Clamp X2
			if (Y1<0) {Y1=0;}							//Clamp Y
			if (Y2>=High) {Y2=High-1;}					//Clamp Y2
			var Xpos:int;
			var Ypos:int;
			if (Pix.Format==0) {
				//Color[] float format
				var Pixels:Color[]=Pix.Pixels;			//Get pixels array
				for (Ypos=Y1; Ypos<=Y2; Ypos++) {
					for (Xpos=X1; Xpos<=X2; Xpos++) {
						Pixels[(Ypos*Wide)+Xpos]=WithColor;	//Write float pixel
					}
				}
			} else {
				//Color32[] byte format
				var Pixels32:Color32[]=Pix.Pixels32;			//Get pixels32 array
				var WithColor32:Color32=Color32(WithColor.r*255.0, WithColor.g*255.0, WithColor.b*255.0, WithColor.a*255.0);	//Convert float color to byte color
				for (Ypos=Y1; Ypos<=Y2; Ypos++) {
					for (Xpos=X1; Xpos<=X2; Xpos++) {
						Pixels32[(Ypos*Wide)+Xpos]=WithColor32;	//Write byte pixel
					}
				}
			}
		}
	}
	
	function PixmapBox(Pix:Pixmap, X1:int, Y1:int, X2:int, Y2:int, WithColor:Color) {
		//Draw a hollow box in a Pixmap in a given color
		if (Pix) {
			var Wide:int=Pix.Width;						//Pixmap width in pixels (row length)
			var High:int=Pix.Height;					//Pixmap height in pixels
			if ( ((X1<0) && (X2<0)) || ((X1>=Wide) && (X2>=Wide)) || ((Y1<0) && (Y2<0)) || ((Y1>=High) && (Y2>=High)) ) {
				return;									//Nothing to draw
			}
			var Temp:int;
			if (X2<X1) {
				Temp=X1;								//Swap X coords
				X1=X2;
				X2=Temp;
			}
			if (Y2<Y1) {
				Temp=Y1;
				Y1=Y2;
				Y2=Temp;								//Swap Y coords
			}
			PixmapRect(Pix,X1,Y1,X2,Y1,WithColor);		//Draw top
			PixmapRect(Pix,X1,Y2,X2,Y2,WithColor);		//Draw bottom
			PixmapRect(Pix,X1,Y1+1,X1,Y2-1,WithColor);	//Draw left
			PixmapRect(Pix,X2,Y1+1,X2,Y2-1,WithColor);	//Draw right
		}
	}
	
	function PixmapLine(Pix:Pixmap, X1:int, Y1:int, X2:int, Y2:int, WithColor:Color) {
		//Draw an aliased/pixel line at any angle in a Pixmap in a given color
		//Based on bresenham integer-math routine from Wikipedia
 		if (Pix) {
 			var Wide:int=Pix.Width;						//Width in pixels (row length)
 			var High:int=Pix.Height;					//Height in pixels
 			var Steep:boolean;
 			var Temp:int;
 			var DeltaX:int;
 			var DeltaY:int;
 			var Error:int;
 			var DeltaError:int;
 			var X:int;
 			var Y:int;
 			var XStep:int;
 			var YStep:int;
 			if (Pix.Format==0) {
 				//Color[] float format
 				var Pixels:Color[]=Pix.Pixels;			//Get pixels array
	 	 	 	Steep=Mathf.Abs(Y2-Y1) > Mathf.Abs(X2-X1);	//Boolean
				if (Steep) {
					Temp=X1;							//Swap X1,Y1
					X1=Y1;
					Y1=Temp;
					Temp=X2;							//Swap Y1,Y2
					X2=Y2;
					Y2=Temp;
				}
				DeltaX=Mathf.Abs(X2-X1);				//X Difference
				DeltaY=Mathf.Abs(Y2-Y1);				//Y Difference
				Error=0;								//Overflow counter
				DeltaError=DeltaY;						//Counter adder
				X=X1;									//Start at X1,Y1
				Y=Y1;		
				if (X1<X2) {							//Direction
					XStep=1;
				} else {
					XStep=-1;
				}
				if (Y1<Y2) {							//Direction
					YStep=1;
				} else {
					YStep=-1;
				}
				if ((X>=0) && (X<Wide) && (Y>=0) && (Y<High)) {
					if (Steep) {						//Draw
						Pixels[(X*Wide)+Y]=WithColor;
					} else {
						Pixels[(Y*Wide)+X]=WithColor;
					}
				}
				while (X!=X2) {
					X+=XStep;							//Move in X
					Error+=DeltaError;					//Add to counter
					if ((Error*2)>DeltaX) {				//Would it overflow?
						Y+=YStep;						//Move in Y
						Error-=DeltaX;					//Overflow/wrap the counter
					}
					if ((X>=0) && (X<Wide) && (Y>=0) && (Y<High)) {
						if (Steep) {					//Draw
							Pixels[(X*Wide)+Y]=WithColor;
						} else {
							Pixels[(Y*Wide)+X]=WithColor;
						}
					}
				}
 			} else {
 				//Color32[] byte format
 				var WithColor32:Color32=Color32(WithColor.r*255.0, WithColor.g*255.0, WithColor.b*255.0, WithColor.a*255.0);	//Convert float color to byte color
 				var Pixels32:Color32[]=Pix.Pixels32;		//Get pixels32 array
	 	 	 	Steep=Mathf.Abs(Y2-Y1) > Mathf.Abs(X2-X1);	//Boolean
				if (Steep) {
					Temp=X1;							//Swap X1,Y1
					X1=Y1;
					Y1=Temp;
					Temp=X2;							//Swap Y1,Y2
					X2=Y2;
					Y2=Temp;
				}
				DeltaX=Mathf.Abs(X2-X1);				//X Difference
				DeltaY=Mathf.Abs(Y2-Y1);				//Y Difference
				Error=0;								//Overflow counter
				DeltaError=DeltaY;						//Counter adder
				X=X1;									//Start at X1,Y1
				Y=Y1;		
				if (X1<X2) {							//Direction
					XStep=1;
				} else {
					XStep=-1;
				}
				if (Y1<Y2) {							//Direction
					YStep=1;
				} else {
					YStep=-1;
				}
				if ((X>=0) && (X<Wide) && (Y>=0) && (Y<High)) {
					if (Steep) {						//Draw
						Pixels32[(X*Wide)+Y]=WithColor32;
					} else {
						Pixels32[(Y*Wide)+X]=WithColor32;
					}
				}
				while (X!=X2) {
					X+=XStep;							//Move in X
					Error+=DeltaError;					//Add to counter
					if ((Error*2)>DeltaX) {				//Would it overflow?
						Y+=YStep;						//Move in Y
						Error-=DeltaX;					//Overflow/wrap the counter
					}
					if ((X>=0) && (X<Wide) && (Y>=0) && (Y<High)) {
						if (Steep) {					//Draw
							Pixels32[(X*Wide)+Y]=WithColor32;
						} else {
							Pixels32[(Y*Wide)+X]=WithColor32;
						}
					}
				}
 			}
		}
	}

	function PixmapEllipse(Pix:Pixmap, XCenter:int, YCenter:int, XRadius:int, YRadius:int, WithColor:Color) {
		//Draw a filled axis-aligned ellipse within a Pixmap using the given color
		//Centered at XCenter,YCenter with radii of XRadius and YRadius
		//Based on mid-point ellipse algorithm, almost entirely integer math
		if (Pix) {
 			var Wide:int=Pix.Width;						//Width in pixels (row length)
 			var High:int=Pix.Height;					//Height in pixels
 			//Doesn't matter what Pixmap format is, PixmapRect function takes care of it
 			var p:int;
 			var px:int;
 			var py:int;
 			var x:int;
 			var y:int;
 			var prevy:int;
			var Rx2:int;
			var Ry2:int;
			var twoRx2:int;
			var twoRy2:int;
			var pFloat:float;
			Rx2=XRadius*XRadius;
			Ry2=YRadius*YRadius;
			twoRx2=Rx2*2;
			twoRy2=Ry2*2;
			//Region 1
			x=0;
			y=YRadius;
			PixmapRect(Pix,XCenter-XRadius,YCenter,(XCenter-XRadius)+(XRadius*2),YCenter,WithColor);
			pFloat=(Ry2-(Rx2*YRadius))+(0.25*Rx2);
			p=pFloat + (Mathf.Sign(pFloat)*0.5);
			px=0;
			py=twoRx2*y;
			while (px<py-1) {
				prevy=y;
				x+=1;
				px+=twoRy2;
				if (p>=0) {
					y-=1;
					py-=twoRx2;
				}
				if (p<0) {
					p+=Ry2+px;
				} else {
					p+=(Ry2+px-py);
				}
				if ((y<prevy) && (px<py-1)) {
					PixmapRect(Pix,XCenter-x,YCenter+y,(XCenter-x)+(x*2),YCenter+y,WithColor);
					PixmapRect(Pix,XCenter-x,YCenter-y,(XCenter-x)+(x*2),YCenter-y,WithColor);
				}
			}
			//Region 2
			pFloat=(Ry2*(x+0.5)*(x+0.5))+(Rx2*(y-1.0)*(y-1.0))-(Rx2*(Ry2));
			p=pFloat + (Mathf.Sign(pFloat)*0.5);
			y+=1;
			while (y>1) {
				y-=1;
				py-=twoRx2;
				if (p<=0) {
					x+=1;
					px+=twoRy2;
				}
				if (p>0) {
					p+=(Rx2-py);
				} else {
					p+=(Rx2-py+px);
				}
				PixmapRect(Pix,XCenter-x,YCenter+y,(XCenter-x)+(x*2),YCenter+y,WithColor);
				PixmapRect(Pix,XCenter-x,YCenter-y,(XCenter-x)+(x*2),YCenter-y,WithColor);
			}
		}
	}

	function ResourceToPixmap(ResourcePath:String, RGBA8:boolean) {
		//Load an image from a Resources folder into a Pixmap
		//The image format must be a JPEG (RGB24) or a PNG (RGB24 or RGBA32)
		//The image resource being loaded must have its extension renamed from .jpg/.png to .bytes
		//RGBA8 determines whether to load in Color[] float format or Color32[] byte format
		var File:TextAsset = Resources.Load(ResourcePath) as TextAsset;
		var Tex:Texture2D = new Texture2D(1,1);		//Make a dummy texture
		Tex.LoadImage(File.bytes);					//Load the image into the texture
		var Pix:Pixmap = CreatePixmap(Tex.width,Tex.height,RGBA8);	//Make a Pixmap
		TextureToPixmap(Tex,Pix);					//Download the texture into the Pixmap
		Destroy(Tex);								//Free the texture
		return Pix;									//Send back the Pixmap
	}
	
	function FileToPixmap(FilePathUrl:String, RGBA8:boolean) {
		//Load an image file from disk into an new Pixmap
		//You do not need to know the size of the image before loading it, but more cpu time will be blocked while waiting for the load
		//Image file must be a regular .JPG or .PNG
		//JPEG will load as RGB24 format, PNG will load as ARGB32
		//Since `WWW` is used, you can also load from a website url
		//FilePathUrl should begin with http:// or similar for web-based images, or file:// for filesystem/disk images
		//RGBA8 determines whether to load in Color[] float format or Color32[] byte format
		//Recommended not to use this in a Unity webplayer because empty wait loop will block/hog all cpu time preventing the download
		var Tex:Texture2D = CreateTexture(1,1,TextureFormat.ARGB32,false);	//Dummy texture without compression
		var www = new WWW(FilePathUrl);				//Load/download the file
		while (!www.isDone) {FileToPixmap_Wait(www);}	//Wait for it to finish in an external coroutine
		www.LoadImageIntoTexture(Tex);				//Load the PNG/JPEG into our texture
		var Pix:Pixmap=CreatePixmap(Tex.width,Tex.height,RGBA8);	//Create new Pixmap
		TextureToPixmap(Tex,Pix);					//Download the texture into the Pixmap
		Destroy(Tex);								//Free the texture
		return Pix;									//Send it back
	}
	function FileToPixmap_Wait(www:WWW) {
		//Corouting used by FileToPixmap to wait until the file is fully loaded/downloaded
		//This allows FileToPixmap() to return a new Pixmap object which can't be returned from a function that has a coroutine in it
		//This is an internal function, not for direct use in your scripts
		yield www;									//Wait for file to finish loading then go back
	}
	
	function FileToPixmap(FilePathUrl:String, Pix:Pixmap, RGBA8:boolean) {
		//Load an image file from disk into an existing Pixmap, e.g. call CreatePixmap() first with same size as the image being loaded
		//You will need to know the size of the image before loading it
		//Image file must be a regular .JPG or .PNG
		//JPEG will load as RGB24 format, PNG will load as ARGB32
		//Since `WWW` is used, you can also load from a website url
		//FilePathUrl should begin with http:// or similar for web-based images, or file:// for filesystem/disk images
		//RGBA8 determines whether to load in Color[] float format or Color32[] byte format
		//Texture size and Pixmap size must be the same
		var Tex:Texture2D = CreateTexture(1,1,TextureFormat.ARGB32,false);	//Dummy texture without compression
		var www = new WWW(FilePathUrl);				//Load/download the file
		yield www;									//Wait for it to finish
		if ((Tex.width != Pix.Width) || (Tex.height != Pix.Height)) {return;}	//Size doesn't match, quit
		www.LoadImageIntoTexture(Tex);				//Load the PNG/JPEG into our texture
		TextureToPixmap(Tex,Pix);					//Download the texture into the Pixmap
		Destroy(Tex);								//Free the texture
	}
	
	function SavePixmapPNG(PixmapToSave:Pixmap, FilePath:String){
		//Save a pixmap as a PNG file
		//Suggested file format extension .png
		//Pixmap has to be converted to a texture first then encoded to PNG and saved
		//Returns true if successful, otherwise false
		
		if (PixmapToSave) {			
			//Create a texture, ARGB32 format for grabbing the screen
		    var tex = new Texture2D (PixmapToSave.Width, PixmapToSave.Height, TextureFormat.ARGB32, false);
		
		    // Read the pixmap contents into the texture
		    if (PixmapToSave.Format==0) {
		    	//Color[] float format
		    	tex.SetPixels(PixmapToSave.Pixels,0);		//Upload to texture
		    } else {
		    	//Color32[] byte format
		    	tex.SetPixels32(PixmapToSave.Pixels32,0);	//Upload to texture
		    }
		    tex.Apply(false);
		
		    // Encode texture into PNG
		    var bytes:byte[] = tex.EncodeToPNG();
		    Destroy (tex);	//release the texture memory
			
			// Write to a file
			File.WriteAllBytes(FilePath, bytes);
			return true;		//Success
		} else {
			return null;
		}
	}
	
	function SavePixmapJPG(PixmapToSave:Pixmap, FilePath:String) {
		//Save a pixmap into a JPEG file
		//Suggested file format extension: .jpg
		//Saves with 85% JPEG quality
		//Pixmap has to be converted to a texture first then encoded to JPG and saved
		//Returns true if successful, otherwise false

		if (PixmapToSave) {
			//Create a texture, ARGB32 format for grabbing the screen
		    var tex = new Texture2D (PixmapToSave.Width, PixmapToSave.Height, TextureFormat.ARGB32, false);
		
		    // Read the pixmap contents into the texture
		    if (PixmapToSave.Format==0) {
		    	//Color[] float format
		    	tex.SetPixels(PixmapToSave.Pixels,0);		//Upload to texture
		    } else {
		    	//Color32[] byte format
		    	tex.SetPixels32(PixmapToSave.Pixels32,0);	//Upload to texture
		    }
		    tex.Apply(false);
			
			//Create our JPEG encoder for this texture - utilizes JPEGEncoder.js which is derived from Adobe copyrighted/licensed sourcecode
			var encoder:JPGEncoder = new JPGEncoder(tex, 85);
			
			//Encoder is threaded; wait for it to finish - the brute force unfriendly hogging way
			while(!encoder.isDone) {}			//Should really do yield WaitForSeconds(0.0001); here to yield for 1 millisecond between checks, but this causes the function to fail to execute, probably because multithreading then clashes with other programming
		
		    Destroy(tex);		//release the texture memory
			var bytes:byte[] = encoder.GetBytes();
			
			// Write to a file
			File.WriteAllBytes(FilePath, bytes);
			return true;		//Success
		} else {
			return null;
		}
	}
	
	function SavePixmapJPG(PixmapToSave:Pixmap, FilePath:String, Quality:int) {
		//Save a pixmap into a JPEG file
		//Suggested file format extension: .jpg
		//Quality ranges from 1 to 100%
		//Pixmap has to be converted to a texture first then encoded to JPG and saved
		//Returns true if successful, otherwise false

		if (PixmapToSave) {
			//Create a texture, ARGB32 format for grabbing the screen
		    var tex = new Texture2D (PixmapToSave.Width, PixmapToSave.Height, TextureFormat.ARGB32, false);
		
		    // Read the pixmap contents into the texture
		    if (PixmapToSave.Format==0) {
		    	//Color[] float format
		    	tex.SetPixels(PixmapToSave.Pixels,0);		//Upload to texture
		    } else {
		    	//Color32[] byte format
		    	tex.SetPixels32(PixmapToSave.Pixels32,0);	//Upload to texture
		    }
		    tex.Apply(false);
			
			//Create our JPEG encoder for this texture - utilizes JPEGEncoder.js which is derived from Adobe copyrighted/licensed sourcecode
			var encoder:JPGEncoder = new JPGEncoder(tex, Quality);
			
			//Encoder is threaded; wait for it to finish - the brute force unfriendly hogging way
			while(!encoder.isDone) {}			//Should really do yield WaitForSeconds(0.0001); here to yield for 1 millisecond between checks, but this causes the function to fail to execute, probably because multithreading then clashes with other programming
		
		    Destroy(tex);		//release the texture memory
			var bytes:byte[] = encoder.GetBytes();
			
			// Write to a file
			File.WriteAllBytes(FilePath, bytes);
			return true;		//Success
		} else {
			return null;
		}
	}
	
	function SavePixmapRAW(PixmapToSave:Pixmap, FilePath:String) {
		//Save a pixmap into a RAW file (raw data, not camera raw format)
		//Suggestion file format extension: .raw
		//Returns true if successful, otherwise false
	
		if (PixmapToSave) {
			//Convert colors to RGBA8 byte stream
			var ColorOffset:int = 0;
			var ColorRowEnd:int;
			var ByteOffset:int = 0;
			var Row:int;
			var ColorsStart:int;
			var Width:int = PixmapToSave.Width;				//For speed
			var Height:int = PixmapToSave.Height;				//For speed
			var TotalColors:int = Width * Height;
			var bytes:byte[] = new byte[TotalColors * 4];		//For RGBA8
			if (PixmapToSave.Format==0) {
				//Color[] float format
				var Pixels:Color[] = PixmapToSave.Pixels;		//For speed
				for (Row=Height-1; Row>=0; Row--) {
					//Do one row
					ColorsStart = Row * Width;
					ColorRowEnd = ColorsStart + Width;
					for (ColorOffset = ColorsStart; ColorOffset < ColorRowEnd; ColorOffset++) {
						//Do all pixels in the row from left to right
						bytes[ByteOffset] = Mathf.Floor(Pixels[ColorOffset].r*255.0);		//Convert Color to Color32
						bytes[ByteOffset+1] = Mathf.Floor(Pixels[ColorOffset].g*255.0);
						bytes[ByteOffset+2] = Mathf.Floor(Pixels[ColorOffset].b*255.0);
						bytes[ByteOffset+3] = Mathf.Floor(Pixels[ColorOffset].a*255.0);
						ByteOffset+=4;
					}
				}
			} else {
				//Color32[] byte format
				var Pixels32:Color32[] = PixmapToSave.Pixels32;		//For speed
				for (Row=Height-1; Row>=0; Row--) {
					//Do one row
					ColorsStart = Row * Width;
					ColorRowEnd = ColorsStart + Width;
					for (ColorOffset = ColorsStart; ColorOffset < ColorRowEnd; ColorOffset++) {
						//Do all pixels in the row from left to right
						bytes[ByteOffset] = Pixels[ColorOffset].r;		//Get Color32
						bytes[ByteOffset+1] = Pixels[ColorOffset].g;
						bytes[ByteOffset+2] = Pixels[ColorOffset].b;
						bytes[ByteOffset+3] = Pixels[ColorOffset].a;
						ByteOffset+=4;
					}
				}
			}
		
			// Write to a file
			File.WriteAllBytes(FilePath, bytes);
			return true;			//Success
		} else {
			return null;
		}
	}
	
	function SavePixmapBMP(PixmapToSave:Pixmap, FilePath:String) {
		//Save a pixmap into a BMP file
		//Suggested file format extension: .bmp
		//BMP does not support Alpha channel, it is ignored, RGB only
		//Returns true if successful, otherwise false
		
		if (PixmapToSave) {
			var Width:int = PixmapToSave.Width;			//For Speed
			var Height:int = PixmapToSave.Height;			//For Speed
			var TotalColors:int = Width * Height;
			var bytes:byte[] = new byte[TotalColors * 3];
			
			//Convert colors to RGB8 byte stream
			var ColorOffset:int = 0;
			var ColorRowEnd:int;
			var ByteOffset:int = 0;
			var Row:int;
			var ColorsStart:int;
			if (PixmapToSave.Format==0) {
				//Color float format
				var Pixels:Color[] = PixmapToSave.Pixels;		//For speed
				for (Row=0; Row<Height; Row++) {
					//Do one row
					ColorsStart = Row * Width;
					ColorRowEnd = ColorsStart + Width;
					for (ColorOffset = ColorsStart; ColorOffset < ColorRowEnd; ColorOffset++) {
						//Do all pixels in the row from left to right
						bytes[ByteOffset] = Mathf.Floor(Pixels[ColorOffset].r*255.0);		//Convert Color
						bytes[ByteOffset+1] = Mathf.Floor(Pixels[ColorOffset].g*255.0);
						bytes[ByteOffset+2] = Mathf.Floor(Pixels[ColorOffset].b*255.0);
						ByteOffset+=3;
					}
				}
			} else {
				//Color32 byte format
				var Pixels32:Color32[] = PixmapToSave.Pixels32;		//For speed
				for (Row=0; Row<Height; Row++) {
					//Do one row
					ColorsStart = Row * Width;
					ColorRowEnd = ColorsStart + Width;
					for (ColorOffset = ColorsStart; ColorOffset < ColorRowEnd; ColorOffset++) {
						//Do all pixels in the row from left to right
						bytes[ByteOffset] = Pixels32[ColorOffset].r;		//Get Color32
						bytes[ByteOffset+1] = Pixels32[ColorOffset].g;
						bytes[ByteOffset+2] = Pixels32[ColorOffset].b;
						ByteOffset+=3;
					}
				}
			}
		
			// Write to a file
			//Create a binary stream for writing
			var Stream:FileStream = new FileStream(FilePath,FileMode.CreateNew);
			if (Stream) {
				var BinaryStream:BinaryWriter = new BinaryWriter(Stream);		//Convert to binary writer
				
				//Variables
				var HSize:int;
				var HOffset:int = 54;				//Header offset
				var Size:int = 40;				//Header size?
				var Wdth:int;
				var Hght:int;
				var Planes:int = 1;				//How many bitplanes (only 1 for truecolor)
				var Bits: int = 24;					//Number of bits per pixel
				var Compression:int = 0;			//Compression type, none
				var ISize:int = 40;				//Size?
				var XPels:int = 2834;				//Horizontal pixels per meter
				var YPels:int = 2834;				//Vertical pixels per meter
				var Cols:int = 0;					//Number of color indexes in color table
				var InUse: int = 0;					//Colors in use?
				var W:int;
				var X:int;
				var Y:int;
				
				//Calculate header values for BMP
				Wdth = Width;						//Width of the bitmap in pixels
				Hght = Height;						//Height of the bitmap in pixels
				W=Wdth*3;							//Number of bytes in a row of pixels, actual pixels only, no padding
				//if ((W % 4)!=0) {W+=(4-(W % 4));}	//Make the width into multiples of 4 bytes for alignment
				W=(W+3) & 0xfffc;
				HSize=(W*Hght)+54;					//Total size of header + pixel data + alignment padding
	
				//Write the header
				var Temp:byte;
				Temp=66;							//Convert B in ASCII/UTF8 to integer, #66
				BinaryStream.Write(Temp);			//BM means BITMAP
				Temp=77;							//Convert M in ASCII/UTF8 to integer, #77
				BinaryStream.Write(Temp);			//BM means BITMAP
				BinaryStream.Write(HSize);
			 	var Temp2:int;
			 	Temp2=0;
				BinaryStream.Write(Temp2);
				BinaryStream.Write(HOffset);
				BinaryStream.Write(Size);
				BinaryStream.Write(Wdth);
				BinaryStream.Write(Hght);
				Temp=Planes;
				BinaryStream.Write(Temp);
				Temp=0;								//Short not int, little endian
				BinaryStream.Write(Temp);
				Temp=Bits;
				BinaryStream.Write(Temp);
				Temp=0;								//Short not int, little endian
				BinaryStream.Write(Temp);
				BinaryStream.Write(Compression);
				BinaryStream.Write(ISize);
				BinaryStream.Write(XPels);
				BinaryStream.Write(YPels);
				BinaryStream.Write(Cols);
				BinaryStream.Write(InUse);
	
				//Write the pixel data
				var Offset:int = 0;				//Position in source
				for (Y=Hght-1; Y>=0; Y--) {
					//Process each row from bottom to top
					for (X=0; X<W; X+=3) {
						//Process each pixel - convert RGB to BGR
						BinaryStream.Write(bytes[Offset+X+2]);		//Copy blue to red
						BinaryStream.Write(bytes[Offset+X+1]);		//Copy green to green
						BinaryStream.Write(bytes[Offset+X]);		//Copy red to blue
					}
					Offset+=W;						//Next row of source bytes, no padding
				}
				BinaryStream.Close();
				Stream.Close();				//Success
				return true;
			}
			Stream.Close();		//Done
			return null;
		} else {
			return null;
		}
	}

	function TextureToPixmap(TextureToRead:Texture2D, PixmapToWrite:Pixmap) {
		//Download pixels from a Texture2D into a pixmap
		//Texture2D must have isReadable flag set and be a compatible format ARGB32, RGB or Alpha8
		//The whole texture will be imported, so pixmap size and texture size must match
		//MipMap level 0 only will be read
		if (TextureToRead && PixmapToWrite) {
			if ((TextureToRead.width==PixmapToWrite.Width) && (TextureToRead.height==PixmapToWrite.Height)) {
				if (PixmapToWrite.Format==0) {
					//Color float format
					//Since format is Color[] and GetPixels returns a Color[] array, replace existing array in Pixmap
					PixmapToWrite.Pixels=TextureToRead.GetPixels(0);		//Get Color[] array, release old one
				} else {
					//Color32 byte format
					//Since format is Color32[] and GetPixels32 returns a Color32[] array, replace existing array in Pixmap
					PixmapToWrite.Pixels32=TextureToRead.GetPixels32(0);	//Get Colors32[] array, release old one
				}
			}
		}
	}

	function TextureToPixmap(TextureToRead:Texture2D, PixmapToWrite:Pixmap, MipLevel:int) {
		//Download pixels from a Texture2D into a pixmap
		//Texture2D must have isReadable flag set and be a compatible format ARGB32, RGB or Alpha8
		//The whole texture will be downloaded, so pixmap size and texture size must match
		//MipMap level specified will be read
		if (TextureToRead && PixmapToWrite) {
			if ((TextureToRead.width==PixmapToWrite.Width) && (TextureToRead.height==PixmapToWrite.Height)) {
				if (PixmapToWrite.Format==0) {
					//Color float format
					//Since format is Color[] and GetPixels returns a Color[] array, replace existing array in Pixmap
					PixmapToWrite.Pixels=TextureToRead.GetPixels(MipLevel);		//Get Color[] array, release old one
				} else {
					//Color32 byte format
					//Since format is Color32[] and GetPixels32 returns a Color32[] array, replace existing array in Pixmap
					PixmapToWrite.Pixels32=TextureToRead.GetPixels32(MipLevel);	//Get Colors32[] array, release old one
				}
			}
		}
	}
	
	function PixmapToTexture(PixmapToRead:Pixmap, TextureToWrite:Texture2D) {
		//Upload pixels from a pixmap into a Texture2D
		//Texture2D must have isReadable flag set and be a compatible format ARGB32, RGB or Alpha8
		//The whole texture will be uploaded, so pixmap size and texture size must match
		//MipMap level 0 only will be written
		if (PixmapToRead && TextureToWrite) {
			if ((TextureToWrite.width==PixmapToRead.Width) && (TextureToWrite.height==PixmapToRead.Height)) {
				if (PixmapToRead.Format==0) {
					//Color float format
					TextureToWrite.SetPixels(PixmapToRead.Pixels,0);		//Upload Color[] array
				} else {
					//Color32 byte format
					TextureToWrite.SetPixels32(PixmapToRead.Pixels32,0);	//Upload Color32[] array
				}
				TextureToWrite.Apply();			//Apply changes
			}
		}
	}

	function PixmapToTexture(PixmapToRead:Pixmap, TextureToWrite:Texture2D, MipLevel:int) {
		//Upload pixels from a pixmap into a Texture2D
		//Texture2D must have isReadable flag set and be a compatible format ARGB32, RGB or Alpha8
		//The whole texture will be uploaded, so pixmap size and texture size must match
		//MipMap level 0 only will be written
		if (PixmapToRead && TextureToWrite) {
			if ((TextureToWrite.width==PixmapToRead.Width) && (TextureToWrite.height==PixmapToRead.Height)) {
				if (PixmapToRead.Format==0) {
					//Color float format
					TextureToWrite.SetPixels(PixmapToRead.Pixels,MipLevel);		//Upload Color[] array
				} else {
					//Color32 byte format
					TextureToWrite.SetPixels32(PixmapToRead.Pixels32,MipLevel);	//Upload Color32[] array
				}
				TextureToWrite.Apply();			//Apply changes
			}
		}
	}
	
	function GrabPixmap() {
		//Grab the backbuffer into a temporary ARGB32 texture and transfer it to a new Pixmap in RGBA8 format
		//Returns the new Pixmap
		var Tex:Texture2D = CreateTexture(Screen.width, Screen.height);		//Create a texture, no mipmaps
		Tex.ReadPixels(Rect(0,0,Screen.width,Screen.height), 0,0,false);	//Grab to texture, no mipmaps
		var Pix:Pixmap = CreatePixmap(Screen.width,Screen.height,true);		//Make new RGBA8 pixmap
		TextureToPixmap(Tex,Pix);					//Grab to pixmap
		Destroy(Tex);								//Free the texture
		return Pix;									//Send it back
	}
	
	function GrabPixmap(Pix:Pixmap) {
		//Grab the backbuffer into an existing pixmap
		var Tex:Texture2D = CreateTexture(Screen.width, Screen.height);		//Create a texture, no mipmaps
		Tex.ReadPixels(Rect(0,0,Screen.width,Screen.height),0,0,false);		//Grab to texture as much as will fit
		TextureToPixmap(Tex,Pix);					//Grab to pixmap
		Destroy(Tex);								//Free the texture
	}
	
	function GrabPixmap(X:int, Y:int, Width:int, Height:int) {
		//Grab a portion of the backbuffer into a new ARGB32 texture and transfer it to a new Pixmap in RGBA8 format
		//Returns the new Pixmap
		var Tex:Texture2D = CreateTexture(Width,Height);		//Create a texture, no mipmaps
		Tex.ReadPixels(Rect(X,Y,Width,Height), 0,0,false);		//Grab to texture
		var Pix:Pixmap = CreatePixmap(Width,Height,true);		//Make new RGBA8 pixmap
		TextureToPixmap(Tex,Pix);					//Grab to pixmap
		Destroy(Tex);								//Free the texture
		return Pix;									//Send it back
	}
	
	function GrabPixmap(Pix:Pixmap, X:int, Y:int, Width:int, Height:int) {
		//Grab a portion of the backbuffer into an existing pixmap
		var Tex:Texture2D = CreateTexture(Width, Height);		//Create a texture, no mipmaps
		Tex.ReadPixels(Rect(X,Y,Width,Height),0,0,false);		//Grab to texture as much as will fit
		TextureToPixmap(Tex,Pix);					//Grab to pixmap
		Destroy(Tex);								//Free the texture
	}

	function GrabPixmap(X:int, Y:int, Width:int, Height:int, ToX:int, ToY:int) {
		//Grab a portion of the backbuffer into a new ARGB32 texture at a given offset and transfer it to a new Pixmap in RGBA8 format
		//Returns the new Pixmap
		var Tex:Texture2D = CreateTexture(Width, Height, TextureFormat.ARGB32, false);	//Create a texture
		Tex.ReadPixels(Rect(X,Y,Width,Height), ToX,ToY,false);	//Grab to texture at offset
		var Pix:Pixmap = CreatePixmap(Width,Height,true);		//Make new RGBA8 pixmap
		TextureToPixmap(Tex,Pix);					//Grab to pixmap
		Destroy(Tex);								//Free the texture
		return Pix;									//Send it back
	}

	function GrabPixmap(Pix:Pixmap, X:int, Y:int, Width:int, Height:int, ToX:int, ToY:int) {
		//Grab a portion of the backbuffer into an existing Pixmap at a given offset
		var Tex:Texture2D = CreateTexture(Width, Height);		//Create a texture, no mipmaps
		Tex.ReadPixels(Rect(X,Y,Width,Height),ToX,ToY,false);		//Grab to texture as much as will fit
		TextureToPixmap(Tex,Pix);					//Grab to pixmap
		Destroy(Tex);								//Free the texture
		return Pix;									//Send it back
	}


}	//End of TextureHelper Class
