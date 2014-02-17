using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LZWCompression {
  public static string Compress(string uncompressed) {
    // Build the dictionary
    Dictionary<string, int> dictionary = new Dictionary<string, int>();
    for (int i = 0; i < 256; i++) {
      dictionary.Add(((char)i).ToString(), i);
    }
  
    string w = string.Empty;
    List<int> compressed = new List<int>();
    StringBuilder compressedStr = new StringBuilder();
  
    foreach (char c in uncompressed) {
      string wc = w + c;
      if (dictionary.ContainsKey(wc)) {
        w = wc;
      }
      else {
        // Write w to output
        compressed.Add(dictionary[w]);
        compressedStr.Append(dictionary[w] + ",");
        // wc is a new sequence; add it to the dictionary
        dictionary.Add(wc, dictionary.Count);
        w = c.ToString();
      }
    }
  
    // Write remaining output if necessary
    if (!string.IsNullOrEmpty(w)) {
    	compressedStr.Append(dictionary[w] + ",");
    }
  
    return compressedStr.ToString().TrimEnd(","[0]);
  }
  
  public static string Decompress(string compressedStr) {
    // Build the dictionary
    ArrayList compressed = new ArrayList(compressedStr.Split(","[0]));
    Dictionary<int, string> dictionary = new Dictionary<int, string>();
    for (int i = 0; i < 256; i++) {
      dictionary.Add(i, ((char)i).ToString());
    }
    string w = dictionary[int.Parse(compressed[0].ToString())];
    compressed.RemoveAt(0);
    StringBuilder decompressed = new StringBuilder(w);
  	int k;
    foreach (string s in compressed) {
    	k = int.Parse(s);
      string entry = null;
      if (dictionary.ContainsKey(k)) {
        entry = dictionary[k];
      } else if (k == dictionary.Count) {
        entry = w + w[0];
      }
  
      decompressed.Append(entry);  
      // New sequence; add it to the dictionary
      dictionary.Add(dictionary.Count, w + entry[0]);
  
      w = entry;
    }
    return decompressed.ToString();
  }
}
