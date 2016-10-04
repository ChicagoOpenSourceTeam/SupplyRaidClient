using UnityEngine;
using System.Collections;
using System;

public class JSONHelper {
	public static T[] getJsonArray<T>(string json)
	{
		string newJson = "{ \"array\": " + json + "}";
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (newJson);
		return wrapper.array;
	}

	[Serializable]
	private class Wrapper<T>
	{
		public T[] array;
	}
}
