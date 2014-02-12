using UnityEngine;
 
public class CSVReader : MonoBehaviour {
	private static TextAsset txt;
	
	// Create data array
	public static string[,] GetDataReference(string fileName, out int numRows) {
		txt = (TextAsset)Resources.Load("Datafiles/" + fileName, typeof(TextAsset));
		return GenerateArray(txt.text, out numRows);
	}
 
	// Splits a CSV file into a 2D string array
	public static string[,] GenerateArray(string csvText, out int numRows) {	
		//Fix bug when load empty line in *.csv file
		//string[] rows = csvText.Split("\n"[0]); 
		string[] rows = csvText.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries); 
		string[] columns = rows[0].Split("\t"[0]);
		numRows = rows.Length - 1;
		string[,] output = new string[numRows, columns.Length];		
		for (int i = 0; i < numRows; i++) {
			columns = rows[i + 1].Split("\t"[0]); 
			for (int j = 0; j < columns.Length; j++) {
				output[i, j] = columns[j];
			}	
		}
		
		return output;
	}
	
	// Clear text after reading
	public static void ClearText() {
		txt = null;
	}
}