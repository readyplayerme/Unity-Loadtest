using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ReadyPlayerMe.Loadtest.UI
{
    public class AvatarLoaderUI : MonoBehaviour
    {
        [SerializeField] private List<AvatarConfig> avatarConfigs;
        [SerializeField] private Dropdown avatarConfigDropdown;
        [SerializeField] private Button btnLoading;
        [SerializeField] private Slider sldAvatarCount;
        [SerializeField] private Text txtAvatarCount;
        
        [SerializeField] private Text txtMeshLod;
        [SerializeField] private Text txtPose;
        [SerializeField] private Text txtTextureAtlas;
        [SerializeField] private Text txtTextureSizeLimit;
        [SerializeField] private Text txtUseHands;
        [SerializeField] private Text txtMorphTargets;
        
        [SerializeField] private GameObject pnlInfo;
        [SerializeField] private Button btnOpenInfo;
        
        [SerializeField] private AvatarLoadingHandler avatarLoadingHandler;

        private int numberOfAvatarsToLoad = 1;
        
        void Start()
        {
            InitUI();
            avatarLoadingHandler.AllAvatarsLoaded += OnAllAvatarsLoaded;
        }

        private void InitUI()
        {
            pnlInfo.SetActive(false);
            btnLoading.enabled = false;
            
            avatarConfigDropdown.onValueChanged.AddListener(delegate
            {
                AvatarConfigSelected(avatarConfigDropdown);
            });
            
            sldAvatarCount.onValueChanged.AddListener(delegate
            {
                AvatarCountChanged(sldAvatarCount);
            });
            
            FillAvatarConfigDropdown();
            btnLoading.onClick.AddListener(OnLoadingButtonClick);
            btnOpenInfo.onClick.AddListener(OnOpenInfoClick);
        }

        private void OnOpenInfoClick()
        {
            pnlInfo.SetActive(!pnlInfo.activeSelf);
        }

        private void AvatarCountChanged(Slider slider)
        {
            txtAvatarCount.text = slider.value.ToString();
            numberOfAvatarsToLoad = (int)slider.value;
        }

        private void AvatarConfigSelected(Dropdown dropdown)
        {
            if (dropdown.value > 0)
            {
                var avatarConfig = GetSelectedAvatarConfig();

                txtMeshLod.text = avatarConfig.MeshLod.ToString();
                txtPose.text = avatarConfig.Pose.ToString();
                txtTextureAtlas.text = avatarConfig.TextureAtlas.ToString();
                txtTextureSizeLimit.text = avatarConfig.TextureSizeLimit.ToString();
                txtUseHands.text = avatarConfig.UseHands.ToString();
                txtMorphTargets.text = avatarConfig.MorphTargets.Count.ToString();

                btnLoading.enabled = true;
            }
            else
            {
                btnLoading.enabled = false;
            }
        }
        
        private void OnAllAvatarsLoaded(object sender, AllAvatarsLoadedEventArgs args)
        {
            btnLoading.enabled = true;
            btnLoading.GetComponentInChildren<Text>().text = "Load Avatars";
        }

        private void FillAvatarConfigDropdown()
        {
            foreach (var avatarConfig in avatarConfigs)
            {
                avatarConfigDropdown.options.Add(new Dropdown.OptionData(avatarConfig.name));
            }
        }

        private void OnLoadingButtonClick()
        {
            btnLoading.enabled = false;
            btnLoading.GetComponentInChildren<Text>().text = "Loading ...";
            var selectedAvatarConfig = GetSelectedAvatarConfig();

            avatarLoadingHandler.LoadAvatars(numberOfAvatarsToLoad, selectedAvatarConfig);
        }
        
        private AvatarConfig GetSelectedAvatarConfig()
        {
            return avatarConfigs.Find((avatarConfig =>
                avatarConfig.name == avatarConfigDropdown.options[avatarConfigDropdown.value].text));
        }
    }
}