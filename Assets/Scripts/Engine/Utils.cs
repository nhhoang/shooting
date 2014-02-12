//----------------------------------------
// Virtual Controls Suite for Unity
// © 2012 Bit By Bit Studios, LLC
// Author: sean@bitbybitstudios.com
// Use of this software means you accept the license agreement.	
// Please don't redistribute without permission :)
//---------------------------------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System;
using System.Globalization;
/// <summary>
/// Static utilities class used by Virtual Controls Suite.
/// </summary>
public class Utils : MonoBehaviour {
	private static Component[] renderers;
	private static float haftScreen = 0.0f;
	private static float OFFSET_Y = 200.0f;
	private static Transform mainCameraTransform;
	
	/// <summary>
	/// A wrapper for Mathf.Approximately, which is sometimes buggy on iOS platforms.
	/// </summary>
	public static bool Approximately(float a, float b) {
		#if UNITY_IPHONE
			return (Mathf.Abs(a - b) < 0.0000001f);
		#else
			return Mathf.Approximately(a, b);
		#endif
	}

	/// <summary>
	/// Returns the first camera (but perhaps not the only one!) that draws the specified GameObject.
	/// </summary>
	public static Camera GetCamera(GameObject go) {
		foreach (Camera c in GameObject.FindObjectsOfType(typeof(Camera))) {
			if ((c.cullingMask & (1 << go.layer)) != 0)	{
				return c;
			}
		}
		
		return null;
	}

	public static void SetLayerRecursively(GameObject obj, string newLayer) {
    if (null == obj) {
      return;
    }
    obj.layer = LayerMask.NameToLayer(newLayer);
    foreach (Transform trans in obj.transform) {
      if (null == trans) {
        continue;
      }
      SetLayerRecursively(trans.gameObject, newLayer);
    }
  }
	
	// Return 0-360 degrees
	public static float GetAngle360(Vector3 v1, Vector3 v2) {
		float angle = Vector3.Angle(v1.normalized, v2);
		Vector3 cross = Vector3.Cross(v1.normalized, v2);

		if (cross.y > 0) {
			angle = 360 - angle;
		}
		
		return angle;
	}
	
	// Return 0:180 0:-180 degrees
	public static float GetAngle180(Vector3 v1, Vector3 v2) {
		float angle = Vector3.Angle(v1.normalized, v2);
		Vector3 cross = Vector3.Cross(v1.normalized, v2);

		if (cross.y > 0) {
			angle *= -1;
		}
		
		return angle;
	}
	
	public static void TweenAlphaTo(GameObject obj, float duration, float delay, float to, float from = -1.0f) {
		UIWidget[] widgets = obj.GetComponentsInChildren<UIWidget>();
		TweenAlpha tween;
		foreach (UIWidget widget in widgets) {
			tween = TweenAlpha.Begin(widget.gameObject, duration, to);
			tween.delay = delay;
			if (from != -1.0f) {
				tween.from = from;
			}
		}
	}
	
	public static bool CanGetReward(float percent) {
		float randomValue = UnityEngine.Random.Range(0.0f, 100.0f);
		if (randomValue < percent) {
			return true;
		} else {
			return false;
		}
	}
	
	public static Vector3 GetRandomCirclePointXY(Vector3 centerPoint, float radius, int startDegree = 0, int endDegree = 360) {
		int degree = UnityEngine.Random.Range(startDegree, endDegree);
		return new Vector3(radius * Mathf.Cos(degree * Mathf.Deg2Rad) + centerPoint.x, radius * Mathf.Sin(degree * Mathf.Deg2Rad) + centerPoint.y, 0.0f);
	}
	
	public static Vector3 GetRandomCirclePointXZ(Vector3 centerPoint, float radius, int startDegree = 0, int endDegree = 360) {
		int degree = UnityEngine.Random.Range(startDegree, endDegree);
		return new Vector3(radius * Mathf.Cos(degree * Mathf.Deg2Rad) + centerPoint.x, 0.0f, radius * Mathf.Sin(degree * Mathf.Deg2Rad) + centerPoint.z);
	}
	
	public static Vector3 GetRandomCirclePointYZ(Vector3 centerPoint, float radius, int startDegree = 0, int endDegree = 360) {
		int degree = UnityEngine.Random.Range(startDegree, endDegree);
		return new Vector3(0.0f, radius * Mathf.Cos(degree * Mathf.Deg2Rad) + centerPoint.y, radius * Mathf.Sin(degree * Mathf.Deg2Rad) + centerPoint.z);
	}
	
	// add value to localPosition
	public static void AddToLocalPositionX(Transform obj, float value) {
		obj.localPosition += new Vector3(value, 0.0f, 0.0f);
	}
	
	public static void AddToLocalPositionY(Transform obj, float value) {
		obj.localPosition += new Vector3(0.0f, value, 0.0f);
	}
	
	public static void AddToLocalPositionZ(Transform obj, float value) {
		obj.localPosition += new Vector3(0.0f, 0.0f, value);
	}
	
	public static void SetLocalPositionX(Transform obj, float value) {
		obj.localPosition = new Vector3(value, obj.localPosition.y, obj.localPosition.z);
	}
	
	public static void SetLocalPositionY(Transform obj, float value) {
		obj.localPosition = new Vector3(obj.localPosition.x, value, obj.localPosition.z);
	}
	
	public static void SetLocalPositionZ(Transform obj, float value) {
		obj.localPosition = new Vector3(obj.localPosition.x, obj.localPosition.y, value);
	}
	
	public static void MultiplyToLocalPositionX(Transform obj, float value) {
		obj.localPosition = new Vector3(obj.localPosition.x * value, obj.localPosition.y, obj.localPosition.z);
	}
	
	public static void MultiplyToLocalPositionY(Transform obj, float value) {
		obj.localPosition = new Vector3(obj.localPosition.x, obj.localPosition.y * value, obj.localPosition.z);
	}
	
	public static void MultiplyToLocalPositionZ(Transform obj, float value) {
		obj.localPosition = new Vector3(obj.localPosition.x, obj.localPosition.y, obj.localPosition.z * value);
	}
	
	// Set value to position
	public static void AddToPositionX(Transform obj, float value) {
		obj.position += new Vector3(value, 0.0f, 0.0f);
	}
	
	public static void AddToPositionY(Transform obj, float value) {
		obj.position += new Vector3(0.0f, value, 0.0f);
	}
	
	public static void AddToPositionZ(Transform obj, float value) {
		obj.position += new Vector3(0.0f, 0.0f, value);
	}
	
	public static void SetPositionX(Transform obj, float value) {
		obj.position = new Vector3(value, obj.position.y, obj.position.z);
	}
	
	public static void SetPositionY(Transform obj, float value) {
		obj.position = new Vector3(obj.position.x, value, obj.position.z);
	}
	
	public static void SetPositionZ(Transform obj, float value) {
		obj.position = new Vector3(obj.position.x, obj.position.y, value);
	}
	
	public static void MultiplyToPositionX(Transform obj, float value) {
		obj.position = new Vector3(obj.position.x * value, obj.position.y, obj.position.z);
	}
	
	public static void MultiplyToPositionY(Transform obj, float value) {
		obj.position = new Vector3(obj.position.x, obj.position.y * value, obj.position.z);
	}
	
	public static void MultiplyToPositionZ(Transform obj, float value) {
		obj.position = new Vector3(obj.position.x, obj.position.y, obj.position.z * value);
	}
	
	// Set value to localScale
	public static void AddToLocalScaleX(Transform obj, float value) {
		obj.localScale += new Vector3(value, 0.0f, 0.0f);
	}
	
	public static void AddToLocalScaleY(Transform obj, float value) {
		obj.localScale += new Vector3(0.0f, value, 0.0f);
	}
	
	public static void AddToLocalScaleZ(Transform obj, float value) {
		obj.localScale += new Vector3(0.0f, 0.0f, value);
	}
	
	public static void SetLocalScaleX(Transform obj, float value) {
		obj.localScale = new Vector3(value, obj.localScale.y, obj.localScale.z);
	}
	
	public static void SetLocalScaleY(Transform obj, float value) {
		obj.localScale = new Vector3(obj.localScale.x, value, obj.localScale.z);
	}
	
	public static void SetLocalScaleZ(Transform obj, float value) {
		obj.localScale = new Vector3(obj.localScale.x, obj.localScale.y, value);
	}
	
	// add value to localPosition 
	public static void AddToLocalPositionX(GameObject obj, float value) {
		obj.transform.localPosition += new Vector3(value, 0.0f, 0.0f);
	}
	
	public static void AddToLocalPositionY(GameObject obj, float value) {
		obj.transform.localPosition += new Vector3(0.0f, value, 0.0f);
	}
	
	public static void AddToLocalPositionZ(GameObject obj, float value) {
		obj.transform.localPosition += new Vector3(0.0f, 0.0f, value);
	}
	
	public static void SetLocalPositionX(GameObject obj, float value) {
		obj.transform.localPosition = new Vector3(value, obj.transform.localPosition.y, obj.transform.localPosition.z);
	}
	
	public static void SetLocalPositionY(GameObject obj, float value) {
		obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, value, obj.transform.localPosition.z);
	}
	
	public static void SetLocalPositionZ(GameObject obj, float value) {
		obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, value);
	}
	
	public static void MultiplyToLocalPositionX(GameObject obj, float value) {
		obj.transform.localPosition = new Vector3(obj.transform.localPosition.x * value, obj.transform.localPosition.y, obj.transform.localPosition.z);
	}
	
	public static void MultiplyToLocalPositionY(GameObject obj, float value) {
		obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y * value, obj.transform.localPosition.z);
	}
	
	public static void MultiplyToLocalPositionZ(GameObject obj, float value) {
		obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, obj.transform.localPosition.z * value);
	}
	
	// Set value to position
	public static void AddToPositionX(GameObject obj, float value) {
		obj.transform.position += new Vector3(value, 0.0f, 0.0f);
	}
	
	public static void AddToPositionY(GameObject obj, float value) {
		obj.transform.position += new Vector3(0.0f, value, 0.0f);
	}
	
	public static void AddToPositionZ(GameObject obj, float value) {
		obj.transform.position += new Vector3(0.0f, 0.0f, value);
	}
	
	public static void SetPositionX(GameObject obj, float value) {
		obj.transform.position = new Vector3(value, obj.transform.position.y, obj.transform.position.z);
	}
	
	public static void SetPositionY(GameObject obj, float value) {
		obj.transform.position = new Vector3(obj.transform.position.x, value, obj.transform.position.z);
	}
	
	public static void SetPositionZ(GameObject obj, float value) {
		obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, value);
	}
	
	public static void MultiplyToPositionX(GameObject obj, float value) {
		obj.transform.position = new Vector3(obj.transform.position.x * value, obj.transform.position.y, obj.transform.position.z);
	}
	
	public static void MultiplyToPositionY(GameObject obj, float value) {
		obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y * value, obj.transform.position.z);
	}
	
	public static void MultiplyToPositionZ(GameObject obj, float value) {
		obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z * value);
	}
	
	// Set value to localScale
	public static void AddToLocalScaleX(GameObject obj, float value) {
		obj.transform.localScale += new Vector3(value, 0.0f, 0.0f);
	}
	
	public static void AddToLocalScaleY(GameObject obj, float value) {
		obj.transform.localScale += new Vector3(0.0f, value, 0.0f);
	}
	
	public static void AddToLocalScaleZ(GameObject obj, float value) {
		obj.transform.localScale += new Vector3(0.0f, 0.0f, value);
	}
	
	public static void SetLocalScaleX(GameObject obj, float value) {
		obj.transform.localScale = new Vector3(value, obj.transform.localScale.y, obj.transform.localScale.z);
	}
	
	public static void SetLocalScaleY(GameObject obj, float value) {
		obj.transform.localScale = new Vector3(obj.transform.localScale.x, value, obj.transform.localScale.z);
	}
	
	public static void SetLocalScaleZ(GameObject obj, float value) {
		obj.transform.localScale = new Vector3(obj.transform.localScale.x, obj.transform.localScale.y, value);
	}
	
	public static Vector3 GetTopPostionOfObj(GameObject obj) {
		Vector3 pos = new Vector3(obj.transform.position.x, -1000.0f, obj.transform.position.z);
		
		renderers = obj.GetComponentsInChildren<Renderer>(true);
		foreach (Component render in renderers) {
			pos = new Vector3(pos.x, Mathf.Max(pos.y, render.renderer.bounds.max.y), pos.z);
		}
		
		return pos;
	}
	
	public static Vector3 GetCenterPostionOfObj(GameObject obj) {
		Vector3 pos = new Vector3(obj.transform.position.x, -1000.0f, obj.transform.position.z);
		Vector3 pos2 = new Vector3(obj.transform.position.x, 1000.0f, obj.transform.position.z);
		
		renderers = obj.GetComponentsInChildren<Renderer>(true);
		foreach (Component render in renderers) {
			if (render.renderer.bounds.max.y != 0.0f || render.renderer.bounds.min.y != 0.0f) {
				pos = new Vector3(pos.x, Mathf.Max(pos.y, render.renderer.bounds.max.y), pos.z);
				pos2 = new Vector3(pos2.x, Mathf.Min(pos2.y, render.renderer.bounds.min.y), pos2.z);
			}
		}
		
		pos.y = (pos.y + pos2.y) / 2.0f;
		return pos;
	}
	
	public static string ArrayToString(object[] arr) {
		string str = "";
		for (int i = 0; i < arr.Length; i++) {
			str += arr[i] + "";
			if (i < arr.Length - 1) {
				str += ",";
			}
		}
		
		return str;
	}
	
	public static string ArrayIntToString(int[] arr) {
		string str = "";
		for (int i = 0; i < arr.Length; i++) {
			str += arr[i] + "";
			if (i < arr.Length - 1) {
				str += ",";
			}
		}
		
		return str;
	}
	
	public static string ArrayStringToString(string[] arr) {
		string str = "";
		for (int i = 0; i < arr.Length; i++) {
			str += arr[i] + "";
			if (i < arr.Length - 1) {
				str += ",";
			}
		}
		
		return str;
	}
	
	// Convert ex: 1234.5 => 1234, 123456 => 123k...
	public static string RoundTo4Digits(float num) {
		int even = (int)num;
		string evenStr = "" + even;
		string numStr = "" + num;
		string returnStr = "";
		
		if (evenStr.Length <= 4) {
			if (evenStr.Length < 4) {
				returnStr = numStr.Length >= 5 ? numStr.Substring(0, 5) : numStr;
			} else {
				returnStr = numStr.Substring(0, 4);
			}
		} else if (evenStr.Length >= 5 && evenStr.Length <= 6) {
			returnStr = "" + ((even - (even % Mathf.Pow(10.0f, evenStr.Length - 3))) / Mathf.Pow(10.0f, 3)) + "k";
		} else if (evenStr.Length >= 7 && evenStr.Length <= 9) {
			returnStr = "" + ((even - (even % Mathf.Pow(10.0f, evenStr.Length - 3))) / Mathf.Pow(10.0f, 6)) + "m";
		} else if (evenStr.Length >= 10 && evenStr.Length <= 12) {
			returnStr = "" + ((even - (even % Mathf.Pow(10.0f, evenStr.Length - 3))) / Mathf.Pow(10.0f, 9)) + "b";
		}
		
		return returnStr;
	}
	
	// Get time in format 1h2m3s
	public static string GetTime(int timer) {
		if (timer <= 0) {
			return "";
		}
		
		string timeString = "";
    int count = 0;
		
		if (timer >= 86400) {
			timeString += Mathf.Ceil(timer / 86400) + "d";
			timer -= (int)Mathf.Ceil(timer / 86400) * 86400;
			count += 1;
		}
		 
		if (timer >= 3600) {
			timeString += Mathf.Ceil(timer / 3600) + "h";
			timer -= (int)Mathf.Ceil(timer / 3600) * 3600;
			count += 1;
			if (count == 2) {
			  return timeString;
			}
		}
		
		if (timer >= 60) {
			timeString += Mathf.Ceil(timer / 60) + "m";
			timer -= (int)Mathf.Ceil(timer / 60) * 60;
			count += 1;
			if (count == 2) {
			  return timeString;
			}
		}
		
		if (timer > 0) {
			timeString += timer + "s";
			count += 1;
			if (count == 2) {
			  return timeString;
			}
		}

		return timeString;
	}
	
	// Get time in format 1:20:30
	public static string GetTimeString(int timer) {
		if (timer <= 0) {
			return "00:00:00";
		}
		
		string timeString = "";
		if (timer > 3600) {
			timeString += Mathf.Ceil(timer / 3600) + ":";
			timer -= (int)Mathf.Ceil(timer / 3600) * 3600;
		} else {
			timeString += "00:";
		}
		
		if (timer >= 60) {
			timeString += Mathf.Ceil(timer / 60) + ":";
			timer -= (int)Mathf.Ceil(timer / 60) * 60;
		} else {
			timeString += "00:";
		}
		
		if (timer < 10) {
			timeString += "0" + timer;
		} else {
			timeString += timer;
		}
			
		return timeString;
	}
	
	// Convert time UTC string to datetime (string format: yyyy/MM/dd HH:mm:ss)
	public static DateTime? ConvertStringToTime(string timer) {
	  if (timer != null && timer.IndexOf("null") == -1) {
      // return DateTime.ParseExact(timer, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
      return DateTime.Parse(timer);
	  } else {
	    return null;
	  }
	}
	
	// Display short abbreviation for long text
	public static string SimpleLongText(string inputText) {
		if (inputText.Length > 20) {
			inputText = inputText.Substring(0, 18) + "...";
		}
		
		return inputText;
	}
	
	public static int[] SplitStringToIntArray(string st, string delimiter) {
		if (st == "") {
			return new int[0];
		}
		
		string[] arr = st.Split(delimiter[0]);
		int[] intArr = new int[arr.Length];
		for (int i = 0; i < arr.Length; i++) {
			intArr[i] = int.Parse(arr[i]);
		}
		return intArr;
	}
	
	public static List<int> SplitStringToListInt(string st, string delimiter) {
		if (st == "") {
			return new List<int>();
		}
		
		string[] arr = st.Split(delimiter[0]);
		List<int> listInt = new List<int>();
		for (int i = 0; i < arr.Length; i++) {
			listInt.Add(int.Parse(arr[i]));
		}
		return listInt;
	}
	
	public static void SetParent(Transform parentObj, Transform childObj) {
		childObj.parent = parentObj;
		childObj.localPosition = Vector3.zero;
	}
	
	
	// Only apply for object rotate 90 or not around x
	public static void SetBoxColliderPosition(Transform obj, Vector3 pos) {
		if (obj.GetComponent<BoxCollider>() == null) {
			return;
		}

		BoxCollider boxCollider = obj.GetComponent<BoxCollider>();
		if (Mathf.Approximately(obj.eulerAngles.x, 90.0f)) {
			boxCollider.center = new Vector3((pos.x - obj.position.x) / obj.lossyScale.x,
																	 (pos.z - obj.position.z) / obj.lossyScale.z,
																	 -(pos.y - obj.position.y) / obj.lossyScale.y);
		} else {
			boxCollider.center = new Vector3((pos.x - obj.position.x) / obj.lossyScale.x,
																	 (pos.y - obj.position.y) / obj.lossyScale.y, 
																	 (pos.z - obj.position.z) / obj.lossyScale.z);
		}
	}
	
	public static bool IsOnScreenY(float posY) {
		return posY < mainCameraTransform.position.y + haftScreen && posY > mainCameraTransform.position.y - haftScreen;
	}
	
	public static object[] RandomizeBuiltinArray(object[] arr1) {
		object[] arr = new object[arr1.Length];
		arr1.CopyTo(arr, 0);
		for (int i = arr.Length - 1; i > 0; i--) {
			var r = UnityEngine.Random.Range(0,i);
			var tmp = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
		}
		
		return arr;
	}
	
	public static string CalcItemPos(int pos, int col) {
	  int returnPos = 0;
	  string itemPos = "";
	  if (pos % 2 == 0) {
	    returnPos = pos / 2;
	  } else {
	    returnPos = pos + col;
	  }
	  if (returnPos < 10) {
      itemPos = "0" + returnPos;
    } else {
      itemPos = returnPos.ToString();
    }
	  return itemPos;
	}
	
	public static string RemoveSortCharacters(string str) {
	  return str.Remove(0, str.IndexOf("_") + 1);
	}
	
	public static string GetSortCharacters(int num) {
	  if (num < 10) {
	    return "0" + num;
	  } else {
	    return num.ToString();
	  }
	}
	
	// Calculate parabol through 2 points with predefined shoot angle
	public static void CalcParabolaVertex(float x1, float y1, float x2, float y2, float shootAngle, out float moveDelta, out int sign, 
																				out float AX, out float BX, out float CX, bool IsCorssCorner, float timeToFly, float maxHigh) {
  	if ((x2 - x1) > 0) {
  		sign = 1;
  	} else {
  		sign = -1;
  	}
  	
  	moveDelta = Mathf.Abs(x2 - x1) / timeToFly;
  	float x3 = (x2 - x1) / 2 + x1;
  	float highDistance = Mathf.Tan(shootAngle * Mathf.PI / 180) * Mathf.Abs(x3 - x1);
//		Debug.Log("IsCorssCorner " + IsCorssCorner + " highDistance " + highDistance + " distance " + Mathf.Abs(x3 - x1) + " sign " + sign + " start " + x1 + "," + y1 + " end " + x2 + "," + y2);
		if (IsCorssCorner || Mathf.Abs(x3 - x1) < 1) {
  		highDistance = Mathf.Max(Mathf.Min(highDistance, Mathf.Abs(x3 - x1)), shootAngle / 3);
		}
		
		highDistance = Mathf.Min(highDistance, maxHigh);
//		Debug.Log("after, highDistance -- " + highDistance);
		
  	float y3 = Mathf.Min(y1, y2)  + Mathf.Abs(y2 - y1) / 2 + highDistance * Mathf.Cos((90 - shootAngle) * Mathf.PI / 180);
  	float denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
  	
  	AX = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
  	BX = (x3 * x3 * (y1 - y2) + x2 * x2 * (y3 - y1) + x1 * x1 * (y2 - y3)) / denom;
  	CX = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;  
//  	Debug.Log(" AX " + AX + " BX " + BX + " CX " + CX + " denom " + denom);
  }
	
	public static float GetBarWidth(int cValue, int maxValue, float maxWidth) {
		return (float)cValue * maxWidth / (float)maxValue;
	}
	
	public static string Md5Sum(string strToEncrypt) {
  	System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
  	byte[] bytes = ue.GetBytes(strToEncrypt);
   
  	// encrypt bytes
  	System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
  	byte[] hashBytes = md5.ComputeHash(bytes);
   
  	// Convert the encrypted bytes back to a string (base 16)
  	string hashString = "";
   
  	for (int i = 0; i < hashBytes.Length; i++) {
  		hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
  	}
   
  	return hashString.PadLeft(32, '0');
	}
}

