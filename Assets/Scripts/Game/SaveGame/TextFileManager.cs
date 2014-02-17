using UnityEngine;
using System;
using System.IO;

class TextFileManager {	
	private static string filePath = Application.persistentDataPath + "/";

	
	// Save data to text file
	public static void SaveToFile(string fileName, string dataStr, bool isFullPath = false) {
		try {
	    using (StreamWriter writer = new StreamWriter((isFullPath) ? fileName : filePath + fileName)) {
	    	writer.Write(dataStr);
	    	writer.Close();
	    }
		} catch (Exception e) {
			Debug.Log(" write file error----------- " + e);
		} 
	}
	
	// Load data from text file
	public static string LoadFromFile(string fileName) {
		string dataStr = "";
		try {
			using (StreamReader reader = new StreamReader(filePath + fileName)) {
				dataStr = reader.ReadToEnd();
				reader.Close();
			}
		} catch (Exception e) {
			dataStr = "";	
		}
		
		return dataStr;
	}
}