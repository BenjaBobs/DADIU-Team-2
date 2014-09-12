using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Localization : MonoBehaviour {

	private static Localization _instance;
	private LocLanguage language = LocLanguage.English;
	private Dictionary<LocKey, Dictionary<LocLanguage, string>> translations;
	private bool isPopulated;

	// Languages available
	public enum LocLanguage
	{
		English,
		Danish,
	};

	// Localization strings
	public enum LocKey
	{
		English,
		Danish,
		Score,
		Health,
		Count
	};

	// Add dictionary lookups
	void PopulateLocalization()
	{
		if (isPopulated) return;
		isPopulated = true;

		translations = new Dictionary<LocKey, Dictionary<LocLanguage, string>> ();

		for (int i = 0; i < (int)LocKey.Count; i++)
		{
			translations[(LocKey)i] = new Dictionary<LocLanguage, string>();
		}

		translations[LocKey.English][LocLanguage.English] = "English";
		translations[LocKey.English][LocLanguage.Danish] = "Engelsk";

		translations[LocKey.Danish][LocLanguage.English] = "Danish";
		translations[LocKey.Danish][LocLanguage.Danish] = "Dansk";
		
		translations[LocKey.Score][LocLanguage.English] = "Score";
		translations[LocKey.Score][LocLanguage.Danish] = "Points";
		
		translations[LocKey.Health][LocLanguage.English] = "Health";
		translations[LocKey.Health][LocLanguage.Danish] = "Liv";
	}

	// Singleton istancing
	public static Localization Instance
	{
		get
		{
			if (!_instance)
			{
				GameObject singleton = new GameObject();
				_instance = singleton.AddComponent<Localization>();
				singleton.name = "(singleton) "+ typeof(Localization).ToString();
				DontDestroyOnLoad(singleton);
			}
			return _instance;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool SetLanguage(LocLanguage l)
	{
		language = l;
		return true;
	}

	public string GetString(LocKey inputKey)
	{
		PopulateLocalization();

		if (!translations.ContainsKey(inputKey)) return "INVALID KEY FOR ALL LANGUAGES";
		if (!translations[inputKey].ContainsKey(language)) return "INVALID KEY FOR LANGUAGE";

		return translations [inputKey][language];
	}
}
