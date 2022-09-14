using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace  ReadyPlayerMe.Loadtest
{
    public class AvatarLoaderUI : MonoBehaviour
    {
        [SerializeField] private List<AvatarConfig> avatarConfigs;
        [SerializeField] private Dropdown avatarConfigDropdown;
        [SerializeField] private Button btnLoading;
        [SerializeField] private Slider sldAvatarCount;
        [SerializeField] private Text txtAvatarCount;

        [SerializeField] private Text txtAvgLoadingTime;
        [SerializeField] private Text txtTotalLoadingTime;
        [SerializeField] private Text txtLastLoadingTime;
        [SerializeField] private Text txtCountAvatars;

        [SerializeField] private Text txtMeshLod;
        [SerializeField] private Text txtPose;
        [SerializeField] private Text txtTextureAtlas;
        [SerializeField] private Text txtTextureSizeLimit;
        [SerializeField] private Text txtUseHands;
        [SerializeField] private Text txtMorphTargets;
        
        [SerializeField] private AvatarLoadingHandler avatarLoadingHandler;

        private int numberOfAvatarsToLoad = 1;
        private int numberOfAvatarsLoaded = 0;
        
        void Start()
        {
            InitUI();
            
            avatarLoadingHandler.AvatarLoaded += OnAvatarLoaded;
            avatarLoadingHandler.AllAvatarsLoaded += OnAllAvatarsLoaded;
        }

        private void InitUI()
        {
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
            var selectedAvatarConfig = GetSelectedAvatarConfig();

            avatarLoadingHandler.LoadAvatars(numberOfAvatarsToLoad, selectedAvatarConfig);
        }

        private void OnAllAvatarsLoaded(object sender, AllAvatarsLoadedEventArgs args)
        {
            btnLoading.enabled = true;
            txtTotalLoadingTime.text = args.SumLoadingTime.ToString();
            Debug.Log("Finished loading");
        }

        private void OnAvatarLoaded(object sender, AvatarLoadedEventArgs args)
        {
            txtAvgLoadingTime.text = args.AverageLoadingTime + " s";
            txtLastLoadingTime.text = args.LoadingTime + " s";
            
            numberOfAvatarsLoaded++;
            txtCountAvatars.text = numberOfAvatarsLoaded.ToString();
        }
        
        private AvatarConfig GetSelectedAvatarConfig()
        {
            return avatarConfigs.Find((avatarConfig =>
                avatarConfig.name == avatarConfigDropdown.options[avatarConfigDropdown.value].text));
        }

        private void OnDestroy()
        {
            if (avatarLoadingHandler == null) return;
            avatarLoadingHandler.AvatarLoaded -= OnAvatarLoaded;
            avatarLoadingHandler.AllAvatarsLoaded -= OnAllAvatarsLoaded;
        }
    }
}