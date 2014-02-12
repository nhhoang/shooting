//Copyright (c) 2012 Paul West/Venus12 LLC

#pragma strict

//Simple demo using TextureHelper to procedurally generate and manipulate new textures and Pixmaps
//The original texture is copied into a pixmap, the pixmap is drawn on, the result is uploaded to a new texture and used

var Cam:Camera;
var Tex:Texture2D;
var Mat:Material;

class Demo extends TextureHelper {
	//Extend the TextureHelper script object to make it's functions available
	
	function Start () {
	
		Cam.orthographicSize = Screen.height/2;					//Adjuast Camera
	
		var Pix:Pixmap = CreatePixmap(512,512,true);			//Create a pixmap
		var Tex2:Texture2D = CreateTexture(512,512);			//Create a texture
		TextureToPixmap(Tex,Pix);								//Copy the original texture to the pixmap
		
		PixmapRect(Pix,50,50,100,200,Color(1.0,1.0,0,1.0));		//Draw a rect
		PixmapEllipse(Pix,256,256,100,70,Color(1.0,0,1.0,1.0));	//Draw an ellipse
		PixmapLine(Pix,70,100,300,500,Color(0,0,1.0,1.0));		//Draw a line
		PixmapBox(Pix,400,400,450,480,Color(0,1.0,0,1.0));		//Draw a box
		
		PixmapToTexture(Pix,Tex2);								//Copy the pixmap to the texture
		Mat.mainTexture=Tex2;									//Set the new texture in the material
	}

}