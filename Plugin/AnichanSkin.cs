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
    [BepInDependency(LanguageAPI.PluginGUID)]
    [BepInDependency(Skins.PluginGUID)]
    //Definition of a mod
    [BepInPlugin(
        "com.NotArme.AnichanSkin",
        "AnichanCommandoSkin",
        "1.1.1")]


    public class AnichanSkin : BaseUnityPlugin
    {
        private static AssetBundle skinAssetBundle;
        public void Awake()
        {

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
            LanguageAPI.Add("ANICHAN_2023", "Ani-chan 2023 uniform", "en");
            LanguageAPI.Add("ANICHAN_FURRY", "Furry Ani-chan", "en");
            LanguageAPI.Add("ANICHAN_COMMUNIST", "Communist Ani-chan", "en");

            LanguageAPI.Add("ANICHAN_SKIN", "Ani-chan", "pt-br");
            LanguageAPI.Add("ANICHAN_2023", "Ani-chan uniforme de 2023", "pt-br");
            LanguageAPI.Add("ANICHAN_FURRY", "Ani-chan furry", "pt-br");
            LanguageAPI.Add("ANICHAN_COMMUNIST", "Ani-chan comunista", "pt-br");
        }

        private void AddSkin()
        {
            GameObject bodyPrefab = BodyCatalog.FindBodyPrefab("CommandoBody");

            ModelSkinController skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();
            SkinDefinition skinDefinition = new SkinDefinition(bodyPrefab, skinAssetBundle);

            SkinDefInfo[] skinList =
            {
                skinDefinition.DefaultSkin(),
                skinDefinition.UniformSkin(),
                skinDefinition.CommunistSkin(),
                skinDefinition.FurrySkin()
            };

            foreach (var skin in skinList)
            {
                Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
                skinController.skins[skinController.skins.Length - 1] = Skins.CreateNewSkinDef(skin);

                var skinsField = Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins");
                BodyCatalog.skins[(int)BodyCatalog.FindBodyIndex(bodyPrefab)] = skinController.skins;
                Reflection.SetFieldValue(typeof(BodyCatalog), "skins", skinsField);
            }
        }
    }
}