using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Text;

public class JSONHelper
{
    #region LoadJSON
    public static T LoadJSONAsObject<T>(string fileName)
    {
        
        var jsonData = LoadTextAppBundle (fileName);
        if (jsonData != string.Empty) 
        {
            try {

                return (T)JsonConvert.DeserializeObject<T>(jsonData, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                
            }catch (Exception e){
                Debug.LogError(e.Message);
                return default(T);
            }
        }else
            return default(T);
    }

    public static T LoadJSONAsObject<T>(string fileName,JsonConverter converter)
    {
        var jsonData = LoadTextAppBundle (fileName);
        if (jsonData != string.Empty) 
        {
            try {

                return (T)JsonConvert.DeserializeObject<T>(jsonData, converter);

            }catch (Exception e){
                Debug.LogError(e.Message);
                return default(T);
            }
        }else
            return default(T);
    }

	public static T LoadUserJSONAsObject<T>(string fileName)
	{
		var jsonData = LoadTextUserData (fileName);
		if (jsonData != string.Empty) 
		{
			try {
                return (T)JsonConvert.DeserializeObject<T>(jsonData, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
            }catch (Exception e){
                Debug.LogError(e.Message);
				return default(T);
			}
		}else
			return default(T);
	}

	private static string LoadTextAppBundle(string fileName) 
	{
		string filePath;
		if (Application.platform == RuntimePlatform.IPhonePlayer) 
		{
			filePath = Application.dataPath + "/Raw/" + fileName;
			if (File.Exists(filePath)) 
			{
				StreamReader r = File.OpenText(filePath);
				if (r != null) 
				{
					string data = r.ReadToEnd();
					r.Close();
					return data;
				}
			}
		}
		else  if(Application.platform == RuntimePlatform.Android)
		{
			filePath = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
			WWW loadFile = new WWW(filePath);

			while(!loadFile.isDone) 
			{
				//count ++;
				////Logger.LogWarning("XML:LoadAppBundleXML() filePath:"+filePath+ " While(!IsDone)");
			}
			//yield return new WaitForSeconds (0.5f);
			//Logger.LogWarning("XML:LoadAppBundleXML() filePath:"+filePath+ " DONE! Data:"+loadFile.text);
			var resultData = loadFile.text;
			GC.Collect();
			return resultData;
		}
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WSAPlayerX86 || Application.platform == RuntimePlatform.OSXPlayer)
        {
            filePath = Application.streamingAssetsPath + "/" + fileName;
            if (File.Exists(filePath))
            {

                StreamReader r = File.OpenText(filePath);

                if (r != null)
                {
                    string data = r.ReadToEnd();
                    r.Close();
                    return data;
                }
            }
        }
        else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
			filePath = Application.streamingAssetsPath + "/" + fileName;
			if (File.Exists(filePath)) 
			{

				StreamReader r = File.OpenText(filePath);

				if (r != null) 
				{
					string data = r.ReadToEnd();
					r.Close();
					return data;
				}
			}
		}

		return "";

	}

    private static string LoadTextUserData(string fileName) 
    {
        string filePath;
        if (Application.platform == RuntimePlatform.IPhonePlayer) 
        {
            filePath = Application.persistentDataPath + "/JsonData/" + fileName;
            if (File.Exists(filePath)) 
            {
                StreamReader r = File.OpenText(filePath);
                if (r != null) 
                {
                    string data = r.ReadToEnd();
                    r.Close();
                    return data;
                }
            }
        }
        else  if(Application.platform == RuntimePlatform.Android)
        {
            #if UNITY_ANDROID
            var internalPath = GetInternalAndroidPath();
            filePath = internalPath + "/" + fileName;
            WWW loadFile = new WWW(filePath);

            while(!loadFile.isDone) 
            {
                //count ++;
                ////Logger.LogWarning("XML:LoadAppBundleXML() filePath:"+filePath+ " While(!IsDone)");
            }
            //yield return new WaitForSeconds (0.5f);
            //Logger.LogWarning("XML:LoadAppBundleXML() filePath:"+filePath+ " DONE! Data:"+loadFile.text);
            var resultData = loadFile.text;
            GC.Collect();
            return resultData;
            #endif
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WSAPlayerX86 || Application.platform == RuntimePlatform.OSXPlayer)
        {
            filePath = Application.persistentDataPath + "/JsonData/" + fileName;
            if (File.Exists(filePath))
            {

                StreamReader r = File.OpenText(filePath);

                if (r != null)
                {
                    string data = r.ReadToEnd();
                    r.Close();
                    return data;
                }
            }
        }
        else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            filePath = Application.dataPath + "/../../Documents/JsonData/" + fileName;
            if (File.Exists(filePath)) 
            {

                StreamReader r = File.OpenText(filePath);

                if (r != null) 
                {
                    string data = r.ReadToEnd();
                    r.Close();
                    return data;
                }
            }
        }

        return "";

    }
    #endregion

    #region SaveJSON

    public static void SaveJSONObject(string fileName,System.Object saveObject , bool streaming = false)
    {
        string dataString = JsonConvert.SerializeObject(saveObject);
        if(streaming)
            CreateStreamingJSON(fileName, dataString);
        else
            CreateUserJSON(fileName,dataString);
    }

    public static void CreateUserJSON(string fileName, string data) 
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            #if UNITY_IPHONE
            StreamWriter writer;
            FileInfo t = new FileInfo(Application.persistentDataPath + "/JsonData/" + fileName);
            if (!t.Exists)
            {
                t.Directory.Create();
            } else {
                t.Delete();
            }
            writer = t.CreateText();
            writer.Write(data);
            writer.Close();
            #endif
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            #if UNITY_ANDROID
            StreamWriter writer;
            var internalPath = GetInternalAndroidPath();
            FileInfo t = new FileInfo(internalPath + "/" + fileName);
            if (!t.Exists)
            {
                t.Directory.Create();
            } else {
                t.Delete();
            }
            writer = t.CreateText();
            writer.Write(data);
            writer.Close();
            #endif
        }else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WSAPlayerX86 || Application.platform == RuntimePlatform.OSXPlayer)
        {
            StreamWriter writer;
            FileInfo t = new FileInfo(Application.persistentDataPath + "/JsonData/" + fileName);
            if (!t.Exists)
            {
                t.Directory.Create();
            }
            else {
                t.Delete();
            }
            writer = t.CreateText();
            writer.Write(data);
            writer.Close();
        }else if (Application.isEditor){
            StreamWriter writer;
            FileInfo t = new FileInfo(Application.dataPath + "/../../Documents/JsonData/" + fileName);
            if(!t.Exists) {
                t.Directory.Create();
            } else {
                t.Delete();
            }
            writer = t.CreateText();
            writer.Write(data);
            writer.Close();
        }
    }

    public static void CreateStreamingJSON(string fileName, string data)
    {
        if (Application.isEditor)
        {
            StreamWriter writer;
            FileInfo t = new FileInfo(Application.streamingAssetsPath+ "/" + fileName);
            if (!t.Exists)
            {
                t.Directory.Create();
            }
            else
            {
                t.Delete();
            }
            writer = t.CreateText();
            writer.Write(data);
            writer.Close();
        }
    }
    #endregion

    public static void DeleteUserJSON(string fileName) 
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            #if UNITY_IPHONE
            FileInfo t = new FileInfo(Application.persistentDataPath + "/JsonData/" + fileName);
            if(t.Exists) {
                t.Delete();
            }
            #endif
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            #if UNITY_ANDROID
            var internalPath = GetInternalAndroidPath();
            FileInfo t = new FileInfo(internalPath + "/" + fileName);
            if(t.Exists) {
                t.Delete();
            }
            #endif
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WSAPlayerX86 || Application.platform == RuntimePlatform.OSXPlayer)
        {
            FileInfo t = new FileInfo(Application.persistentDataPath + "/JsonData/" + fileName);
            if (t.Exists)
            {
                t.Delete();
            }
        }
        else if (Application.isEditor)
        {
            FileInfo t = new FileInfo(Application.dataPath + "/../../Documents/JsonData/" + fileName);
            if (t.Exists)
            {
                t.Delete();
            }
        }
    }
    #region AndroidInternalChecker
    #if UNITY_ANDROID
    public static string internalAndroidPath;

    private static string GetInternalAndroidPath()
    {
        string retVal = Application.persistentDataPath;
        using (AndroidJavaClass application = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject activity = application.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                try
                {
                    retVal = activity.Call<AndroidJavaObject>("getFilesDir").Call<string>("getAbsolutePath");
                    Debug.Log("LOCAL DIR: " + retVal + " | " + Application.persistentDataPath);
                }
                catch
                {
                    Debug.Log("LOCAL DIR: ERROR GETTING LOCALPATH");
                }
            }
        }
        return retVal;
    }
    #endif
    #endregion
}
