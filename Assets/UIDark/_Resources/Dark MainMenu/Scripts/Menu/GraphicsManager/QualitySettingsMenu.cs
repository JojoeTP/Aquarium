using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class QualitySettingsMenu : MonoBehaviour {

	[SerializeField] private Dropdown resolutions; // Resolution screen 
	[SerializeField] private Dropdown qualityPreset; // Setting Presets
	[SerializeField] private Dropdown antiAliasing; // Antialiasing
	[SerializeField] private Dropdown shadowQuality; // Shadow Quality
	[SerializeField] private Dropdown shadowRes; // Shadow Resolution 
	[SerializeField] private Dropdown textureRes; // Texture size
	[SerializeField] private Dropdown anisotropic; // Anisotropic
	[SerializeField] private Button applyButton; // Apply Setting
	[SerializeField] private Button revertButton; // Reset Preset
	[SerializeField] private Toggle fullscreenToggle; // Full Sreen of/onn
	[SerializeField] private Toggle vSyncToggle; // vSync

	
	private int[] aliasing = new int[]{0, 2, 4, 8};
	private int[] texRes = new int[]{0, 1, 2, 3};
	private int[] af = new int[]{0, 2, 4, 8, 16};
	private string[] shadowQualityList, shadowResolutionList, qualityNamesList;
	private Resolution[] resolutionsList;
	private int aniso;

	void Awake()
	{	
		
		resolutionsList = Screen.resolutions;
		qualityNamesList = QualitySettings.names;
		shadowQualityList = System.Enum.GetNames(typeof(ShadowQuality));
		shadowResolutionList = System.Enum.GetNames(typeof(ShadowResolution));
		Load();
		BuildMenu();
	}

	
	void Load()
	{
		QualitySettings.shadows = (ShadowQuality) System.Enum.Parse(typeof(ShadowQuality), PlayerPrefs.GetString("shadowQuality", QualitySettings.shadows.ToString()));
		QualitySettings.vSyncCount = PlayerPrefs.GetInt("vSync", QualitySettings.vSyncCount);
		vSyncToggle.isOn = (QualitySettings.vSyncCount > 0) ? true : false;
		QualitySettings.antiAliasing = PlayerPrefs.GetInt("aliasing", QualitySettings.antiAliasing);
		QualitySettings.masterTextureLimit = PlayerPrefs.GetInt("texRes", QualitySettings.masterTextureLimit);
		QualitySettings.shadowResolution = (ShadowResolution) System.Enum.Parse(typeof(ShadowResolution), PlayerPrefs.GetString("shadowRes", QualitySettings.shadowResolution.ToString()));
		aniso = PlayerPrefs.GetInt("aniso", 16);
		AnisoFiltering(aniso);
		fullscreenToggle.isOn = Screen.fullScreen;
	}

	void Save()
	{
		PlayerPrefs.SetInt("vSync", QualitySettings.vSyncCount);
		PlayerPrefs.SetInt("aliasing", aliasing[antiAliasing.value]);
		PlayerPrefs.SetInt("texRes", QualitySettings.masterTextureLimit);
		PlayerPrefs.SetString("shadowRes", QualitySettings.shadowResolution.ToString());
		PlayerPrefs.SetString("shadowQuality", QualitySettings.shadows.ToString());
		PlayerPrefs.SetInt("aniso", af[anisotropic.value]);
		PlayerPrefs.Save();
	}

	string ResToString(Resolution res)
	{
		return res.width + " x " + res.height;
	}

	void RefreshDropdown()
	{
		resolutions.RefreshShownValue();
		qualityPreset.RefreshShownValue();
		antiAliasing.RefreshShownValue();
		shadowRes.RefreshShownValue();
		textureRes.RefreshShownValue();
		anisotropic.RefreshShownValue();
		shadowQuality.RefreshShownValue();
	}

	void BuildMenu()
	{
		resolutions.options = new List<Dropdown.OptionData>();
		qualityPreset.options = new List<Dropdown.OptionData>();
		antiAliasing.options = new List<Dropdown.OptionData>();
		shadowRes.options = new List<Dropdown.OptionData>();
		textureRes.options = new List<Dropdown.OptionData>();
		anisotropic.options = new List<Dropdown.OptionData>();
		shadowQuality.options = new List<Dropdown.OptionData>();

		for(int i = 0; i < resolutionsList.Length; i++)
		{
			Dropdown.OptionData option = new Dropdown.OptionData();
			option.text = ResToString(resolutionsList[i]);
			resolutions.options.Add(option);
			if(resolutionsList[i].height == Screen.height && resolutionsList[i].width == Screen.width) resolutions.value = i;
		}

		for(int i = 0; i < qualityNamesList.Length; i++)
		{
			Dropdown.OptionData option = new Dropdown.OptionData();
			option.text = QualityNames(qualityNamesList[i]);
			qualityPreset.options.Add(option);
		}

		for(int i = 0; i < aliasing.Length; i++)
		{
			Dropdown.OptionData option = new Dropdown.OptionData();
			option.text = (aliasing[i] == 0) ? "Off" : aliasing[i] + "x Multi Sampling";
			antiAliasing.options.Add(option);
			if(aliasing[i] == QualitySettings.antiAliasing) antiAliasing.value = i;
		}

		for(int i = 0; i < texRes.Length; i++)
		{
			Dropdown.OptionData option = new Dropdown.OptionData();
			option.text = TextureResolutionNames(texRes[i]);
			textureRes.options.Add(option);
		}

		for(int i = 0; i < af.Length; i++)
		{
			Dropdown.OptionData option = new Dropdown.OptionData();
			option.text = (af[i] == 0) ? "Off" : af[i] + "x";
			anisotropic.options.Add(option);
			if(af[i] == aniso) anisotropic.value = i;
		}

		for(int i = 0; i < shadowQualityList.Length; i++)
		{
			Dropdown.OptionData option = new Dropdown.OptionData();
			option.text = ShadowQualityNames(shadowQualityList[i]);
			shadowQuality.options.Add(option);
		}

		for(int i = 0; i < shadowResolutionList.Length; i++)
		{
			Dropdown.OptionData option = new Dropdown.OptionData();
			option.text = ShadowResolutionNames(shadowResolutionList[i]);
			shadowRes.options.Add(option);
			if(shadowResolutionList[i].CompareTo(QualitySettings.shadowResolution.ToString()) == 0) shadowRes.value = i;
		}

		shadowQuality.value = (int)QualitySettings.shadows;
		textureRes.value = QualitySettings.masterTextureLimit;
		qualityPreset.value = QualitySettings.GetQualityLevel();
		qualityPreset.onValueChanged.AddListener(delegate{ApplyPresets();});
		applyButton.onClick.AddListener(()=>{ApplySettings();});
		revertButton.onClick.AddListener(()=>{PresetsSettings();});

		RefreshDropdown();
	}

	void PresetsSettings() // Presets Settings default
	{
		switch(qualityPreset.value)
		{
		case 0:
			QualitySettings.vSyncCount = 0;
			QualitySettings.antiAliasing = 0;
			QualitySettings.masterTextureLimit = 3;
			QualitySettings.shadows = ShadowQuality.Disable;
			QualitySettings.shadowResolution = ShadowResolution.Low;
			anisotropic.value = 0;
			AnisoFiltering(af[0]);
			break;

		case 1:
			QualitySettings.vSyncCount = 0;
			QualitySettings.antiAliasing = 0;
			QualitySettings.masterTextureLimit = 2;
			QualitySettings.shadows = ShadowQuality.HardOnly;
			QualitySettings.shadowResolution = ShadowResolution.Low;
			anisotropic.value = 0;
			AnisoFiltering(af[0]);
			break;

		case 2:
			QualitySettings.vSyncCount = 0;
			QualitySettings.antiAliasing = 0;
			QualitySettings.masterTextureLimit = 1;
			QualitySettings.shadows = ShadowQuality.All;
			QualitySettings.shadowResolution = ShadowResolution.Medium;
			anisotropic.value = 1;
			AnisoFiltering(af[1]);
			break;

		case 3:
			QualitySettings.vSyncCount = 0;
			QualitySettings.antiAliasing = 0;
			QualitySettings.masterTextureLimit = 0;
			QualitySettings.shadows = ShadowQuality.All;
			QualitySettings.shadowResolution = ShadowResolution.Medium;
			anisotropic.value = 2;
			AnisoFiltering(af[2]);
			break;

		case 4:
			QualitySettings.vSyncCount = 1;
			QualitySettings.antiAliasing = 2;
			QualitySettings.masterTextureLimit = 0;
			QualitySettings.shadows = ShadowQuality.All;
			QualitySettings.shadowResolution = ShadowResolution.High;
			anisotropic.value = 3;
			AnisoFiltering(af[3]);
			break;

		case 5:
			QualitySettings.vSyncCount = 1;
			QualitySettings.antiAliasing = 4;
			QualitySettings.masterTextureLimit = 0;
			QualitySettings.shadows = ShadowQuality.All;
			QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
			anisotropic.value = 4;
			AnisoFiltering(af[4]);
			break;
		}

		int j = 0;
		foreach (string res in System.Enum.GetNames(typeof(ShadowResolution))) 
		{
			if(res.CompareTo(QualitySettings.shadowResolution.ToString()) == 0) shadowRes.value = j;
			j++;
		}

		vSyncToggle.isOn = (QualitySettings.vSyncCount > 0) ? true : false;
		textureRes.value = QualitySettings.masterTextureLimit;
		shadowQuality.value = (int)QualitySettings.shadows;

		for(int i = 0; i < aliasing.Length; i++)
		{
			if(aliasing[i] == QualitySettings.antiAliasing) antiAliasing.value = i;
		}

		RefreshDropdown();
	}

	void ApplyPresets() // Apply Presets 
	{
		QualitySettings.SetQualityLevel(qualityPreset.value, true);
		PresetsSettings();
	}

	void AnisoFiltering(int value)
	{
		if(value > 0)
		{
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
			Texture.SetGlobalAnisotropicFilteringLimits(value, 16);
		}
		else
		{
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
		}
	}

	void ApplySettings()
	{
		AnisoFiltering(af[anisotropic.value]);
		QualitySettings.vSyncCount = (vSyncToggle.isOn) ? 1 : 0;
		QualitySettings.masterTextureLimit = texRes[textureRes.value];
		QualitySettings.shadows = (ShadowQuality) System.Enum.Parse(typeof(ShadowQuality), shadowQualityList[shadowQuality.value]);
		QualitySettings.shadowResolution = (ShadowResolution) System.Enum.Parse(typeof(ShadowResolution), shadowResolutionList[shadowRes.value]);
		QualitySettings.antiAliasing = aliasing[antiAliasing.value];
		Screen.SetResolution(resolutionsList[resolutions.value].width, resolutionsList[resolutions.value].height, fullscreenToggle.isOn);
		Save();
	}


	
	string TextureResolutionNames(int value)
	{
		if(value == 0)
		{
			return "VeryHigh";
		}
		else if(value == 1)
		{
			return "High";
		}
		else if(value == 2)
		{
			return "Medium";
		}
		else if(value == 3)
		{
			return "Low";
		}

		return "Error";
	}

	string ShadowQualityNames(string value)
	{
		if(value.CompareTo("HardOnly") == 0)
		{
			return "HardOnly";
		}
		else if(value.CompareTo("Disable") == 0)
		{
			return "Disable";
		}
		else if(value.CompareTo("All") == 0)
		{
			return "All";
		}

		return "Error";
	}

	string ShadowResolutionNames(string value)
	{
		if(value.CompareTo("High") == 0)
		{
			return "High";
		}
		else if(value.CompareTo("Low") == 0)
		{
			return "Low";
		}
		else if(value.CompareTo("Medium") == 0)
		{
			return "Medium";
		}
		else if(value.CompareTo("VeryHigh") == 0)
		{
			return "VeryHigh";
		}

		return "Error";
	}

	string QualityNames(string value)
	{
		if(value.CompareTo("Fastest") == 0)
		{
			return "Fastest";
		}
		else if(value.CompareTo("Fast") == 0)
		{
			return "Fast";
		}
		else if(value.CompareTo("Simple") == 0)
		{
			return "Simple";
		}
		else if(value.CompareTo("Good") == 0)
		{
			return "Good";
		}
		else if(value.CompareTo("Beautiful") == 0)
		{
			return "Beautiful";
		}
		else if(value.CompareTo("Fantastic") == 0)
		{
			return "Fantastic";
		}

		return "Error";

		
	}
}
