using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Localization {
	
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
        Cows,
        NPB,
        Local,
        Options,
        PlayerName,
        Quit,
        Music,
        Soundfx,
        Language,
        Scoreboard,
        noUpload,
        LocalBest,
        OnlineBest,
        ToMenu,
        Toggle,
        Madeby,
        // ADD ABOVE
		Count,
        
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

        translations[LocKey.Cows][LocLanguage.English] = "Cows";
        translations[LocKey.Cows][LocLanguage.Danish] = "Køer";

        translations[LocKey.NPB][LocLanguage.English] = "New Personal Best";
        translations[LocKey.NPB][LocLanguage.Danish] = "Ny Personlig Rekord";

        translations[LocKey.LocalBest][LocLanguage.English] = "Local Best";
        translations[LocKey.LocalBest][LocLanguage.Danish] = "Lokalt Bedste";

        translations[LocKey.OnlineBest][LocLanguage.English] = "Online Best";
        translations[LocKey.OnlineBest][LocLanguage.Danish] = "Online Bedste";

        translations[LocKey.NPB][LocLanguage.English] = "";
        translations[LocKey.NPB][LocLanguage.Danish] = "Din bedste";

        translations[LocKey.NPB][LocLanguage.English] = "Local Best";
        translations[LocKey.NPB][LocLanguage.Danish] = "Local Best";

        translations[LocKey.Local][LocLanguage.English] = "Local";
        translations[LocKey.Local][LocLanguage.Danish] = "Lokal";

        translations[LocKey.Options][LocLanguage.English] = "Options";
        translations[LocKey.Options][LocLanguage.Danish] = "Indstillinger";

        translations[LocKey.PlayerName][LocLanguage.English] = "Player Name";
        translations[LocKey.PlayerName][LocLanguage.Danish] = "Spillernavn";

        translations[LocKey.Quit][LocLanguage.English] = "Quit";
        translations[LocKey.Quit][LocLanguage.Danish] = "Afslut";

        translations[LocKey.Music][LocLanguage.English] = "Music";
        translations[LocKey.Music][LocLanguage.Danish] = "Musik";

        translations[LocKey.Soundfx][LocLanguage.English] = "Sound effects";
        translations[LocKey.Soundfx][LocLanguage.Danish] = "Lyd effekter";

        translations[LocKey.Scoreboard][LocLanguage.English] = "Scoreboard";
        translations[LocKey.Scoreboard][LocLanguage.Danish] = "Resultater";

        translations[LocKey.noUpload][LocLanguage.English] = "Cannot upload a score of 0";
        translations[LocKey.noUpload][LocLanguage.Danish] = "Kan ikke uploade et resultat på 0";

        translations[LocKey.ToMenu][LocLanguage.English] = "Return to menu";
        translations[LocKey.ToMenu][LocLanguage.Danish] = "Tilbage til menuen";

        translations[LocKey.Toggle][LocLanguage.English] = "Toggle";
        translations[LocKey.Toggle][LocLanguage.Danish] = "Skift";

        translations[LocKey.Madeby][LocLanguage.English] = "Made by DADIU Team 2 @ 2014";
        translations[LocKey.Madeby][LocLanguage.Danish] = "Udviklet af DADIU Team2 @ 2014";

	}
	
	// Singleton istancing
	public static Localization instance
	{
		get
		{
			if (_instance == null)
				_instance = new Localization();
			return _instance;
		}
	}
	
	public bool SetLanguage(LocLanguage l)
	{
		language = l;
		return true;
	}
    public bool GetLanguage()
    {
        if (language == 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
	
	public string GetString(LocKey inputKey)
	{
		PopulateLocalization();
		
		if (!translations.ContainsKey(inputKey)) return "INVALID KEY FOR ALL LANGUAGES";
		if (!translations[inputKey].ContainsKey(language)) return "INVALID KEY FOR LANGUAGE";
		
		return translations [inputKey][language];
	}
}
