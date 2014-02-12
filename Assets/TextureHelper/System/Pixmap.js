//Copyright (c) 2012 - Paul West/Venus12 LLC

#pragma strict

class Pixmap {
	//Pixel-map class for storing bitmaps of pixels in main memory
	//Unity currently only allows use of Color[] (4 floats ie RGBA32) or Color32[] (4 bytes ie RGBA8) formats with SetPixels() so these are the two formats we support
	//Other formats such as Alpha8 could be supported but they won't be uploadable to video ram. Yet you can upload a Color[] array to an Alpha8 or RGB texture as well as RGBA
	//The pixmap will always be in multiples of 4 bytes, so has no padding bytes at the end of each row
	//Remember that a texture will need the `isReadable` flag set to True in order to be able to upload a pixmap to it
	//Since SetPixels/SetPixel32 takes a Color or Color32 array as a separate step, any pixmap can be uploaded to any texture if the size matches, format is correct and the texture isReadable

	var Width:int;					//Width of the pixmap in pixels
	var Height:int;					//Height of the pixmap in pixels
	var Format:int;					//The format, 0=Color[], 1=Color32[]
	var TotalPixels:int;				//The total number of pixels in the pixmap
	var Pixels:Color[];				//Pixel data in 4-float RGBA color format
	var Pixels32:Color32[];			//Pixel data in 4-byte RGBA color format

	function Pixmap(NewWidth:int, NewHeight:int, NewFormat:int) {
		//Initialize storage space for pixels in the requested format
		Width = NewWidth;
		Height = NewHeight;
		Format = NewFormat;
		TotalPixels = Width * Height;		//Total number of pixels in the pixmap
		if (Format==0) {
			Pixels = new Color[TotalPixels];		//Create a Color[] array to store float components
		} else {
			Pixels32 = new Color32[TotalPixels];	//Create a Color32[] array to store byte components
		}
	}

}	//End of Pixmap Class
