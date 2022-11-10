using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUISetup : MonoBehaviour
{
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _characterNameText;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _purchaseButton;

    [SerializeField] List<Skin> _skins;

    [SerializeField] List<Image> _grayedOutImages;

    [SerializeField] Transform _buttonsParent;

    private DataPersist _dataPersist;
    private List<SkinData> _skinsData = new List<SkinData>();
    private void Start()
    {
        LoadData();
        SetupSkinBackground();
        SelectActiveSkin();
    }

    public void UpdateSkinsData()
    {
        _dataPersist = FindObjectOfType<DataPersist>();
        _skinsData.Clear();
        foreach (var skin in _dataPersist.PlayerData.Skins)
        {
            _skinsData.Add(skin);
        }
    }

    public void SetupSkinBackground()
    {
        foreach(var image in _grayedOutImages)
        {
            SkinData associatedSkin = _dataPersist.PlayerData.Skins.FirstOrDefault(i => i.Name == image.gameObject.name);
            image.color = associatedSkin.IsUnlocked ? Color.white : Color.gray;
        }
    }

    private void LoadData()
    {
        _dataPersist = FindObjectOfType<DataPersist>();
        _skinsData.Clear();
        foreach(var skin in _dataPersist.PlayerData.Skins)
        {
            _skinsData.Add(skin);
        }
    }

    public void UpdateSkins()
    {
        var dataPersist = FindObjectOfType<DataPersist>();
        List<SkinData> skins = new List<SkinData>();
        foreach (var skin in _skins)
            skins.Add(skin.SkinData);
        dataPersist.PlayerData.Skins = skins;
    }

    public void SelectActiveSkin()
    {
        var dataPersist = FindObjectOfType<DataPersist>();
        _buttonsParent.GetChild(dataPersist.PlayerData.SelectedSkin).GetComponent<Button>().onClick.Invoke();
    }

    public void SelectCharacter(string SkinName)
    {
        if (_skinsData == null)
            LoadData();
        SkinData skin = _skinsData.FirstOrDefault(t => t.Name == SkinName);
        if (skin.Cost == 0 || skin.IsUnlocked)
        {
            _descriptionText.text = "To unlock this skin finish " + skin.UnlockingRequirement + " levels";
            _purchaseButton.gameObject.SetActive(false);
        }
        else if (skin.Cost > 0 && skin.IsUnlocked == false)
        {
            _purchaseButton.gameObject.SetActive(true);
        }
        if (skin.IsUnlocked)
            _descriptionText.text = "This skin is already unlocked";

        _playButton.interactable = skin.IsUnlocked ? true : false;

        _characterNameText.text = skin.NameForPlaceholder;
    }
}
