using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using System;
using System.Reflection;
using UnityEngine;

//Change `SkinTest` to your project name
namespace AnichanSkin
{
    //Marks mod as client-side
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync)]
    //Adds dependency to `R2API`
    [BepInDependency("com.bepis.r2api")]
    //Definition of a mod
    [BepInPlugin(
        //The GUID should be a unique ID for this plugin, which is human readable (as it is used in places like the config). Java package notation is commonly used, which is "com.[your name here].[your plugin name here]"
        "com.NotArme.AnichanSkin",
        //The name is the name of the plugin that's displayed on load
        "AnichanSkin",
        "1.0.0")]


    public class AnichanSkin : BaseUnityPlugin
    {
        private static AssetBundle skinAssetBundle;
        public void Awake()
        {

            //Loading AssetBundle with a model. String value is "{ProjectName}.{AssetBundleFileName}"
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AnichanSkin.anichan"))
            {
                skinAssetBundle = AssetBundle.LoadFromStream(assetStream);
            }

            On.RoR2.SurvivorCatalog.Init += (orig) =>
            {
                orig();

                AddSkin();
            };

            LanguageAPI.Add("ANICHAN_SKIN", "Ani-chan", "en");
            LanguageAPI.Add("ANICHAN_SKIN", "Ani-chan", "pt");
        }

        private void AddSkin()
        {
            //Getting character's prefab
            GameObject bodyPrefab = BodyCatalog.FindBodyPrefab("CommandoBody");

            //Getting necessary components
            var renderers = bodyPrefab.GetComponentsInChildren<Renderer>(true);
            var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();
            var mdl = skinController.gameObject;

            var skin = new SkinDefInfo
            {
                Icon = Skins.CreateSkinIcon(Color.white, new Color(0f, 0.39f, 0.2f), Color.black, new Color(1f, 0.38f, 0f)),
                Name = "AnichanCommando",
                NameToken = "ANICHAN_SKIN",
                RootObject = mdl,
                BaseSkins = [skinController.skins[0]],
                GameObjectActivations = [],
                RendererInfos =
                [
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = skinAssetBundle.LoadAsset<Material>("Assets/AnichanMat.mat"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        //Should mesh be ignored by overlays. For example shield outline from `Personal Shield Generator`
                        ignoreOverlays = false,
                        //Index list: https://github.com/risk-of-thunder/R2Wiki/wiki/Creating-skin-for-vanilla-characters-with-custom-model#renderers
                        renderer = renderers[6]
                    }
                ],
                //This is used to define which mesh should be used on a specific renderer.
                MeshReplacements =
                [
                    new SkinDef.MeshReplacement
                    {
                        mesh = skinAssetBundle.LoadAsset<Mesh>("Assets/AnichanMesh.mesh"),
                        renderer = renderers[6]
                    }
                ],
                ProjectileGhostReplacements = [],
                MinionSkinReplacements = []
            };

            Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
            skinController.skins[skinController.skins.Length - 1] = Skins.CreateNewSkinDef(skin);

            //Adding new skin into BodyCatalog
            var skinsField = Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins");
            BodyCatalog.skins[(int)BodyCatalog.FindBodyIndex(bodyPrefab)] = skinController.skins;
            Reflection.SetFieldValue(typeof(BodyCatalog), "skins", skinsField);
        }
    }
}