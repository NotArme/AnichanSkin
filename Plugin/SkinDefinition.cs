using R2API;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AnichanSkin
{
    public class SkinDefinition
    {
        private GameObject bodyPrefab;
        private AssetBundle skinAssetBundle;

        private Renderer[] renderers;
        private ModelSkinController skinController;
        private GameObject mdl;
        public SkinDefinition(GameObject body, AssetBundle assetBundle)
        {
            bodyPrefab = body;
            skinAssetBundle = assetBundle;

            renderers = bodyPrefab.GetComponentsInChildren<Renderer>(true);
            skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();
            mdl = skinController.gameObject;
        }

        public SkinDefInfo DefaultSkin()
        {
            return new SkinDefInfo
            {
                Icon = Skins.CreateSkinIcon(Color.white, new Color(0f, 0.39f, 0.2f), Color.black, new Color(1f, 0.38f, 0f)),
                Name = "AnichanDefault",
                NameToken = "ANICHAN_SKIN",
                RootObject = mdl,
                BaseSkins = [skinController.skins[0]],
                GameObjectActivations = [],
                RendererInfos =
                    [
                        new CharacterModel.RendererInfo
                        {
                            defaultMaterial = skinAssetBundle.LoadAsset<Material>("Assets/AnichanDefaultMat.mat"),
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

        }

        public SkinDefInfo UniformSkin()
        {
            return new SkinDefInfo
            {
                Icon = Skins.CreateSkinIcon(Color.black, new Color(0f, 0.39f, 0.2f), Color.black, new Color(1f, 0.38f, 0f)),
                Name = "Anichan2023",
                NameToken = "ANICHAN_2023",
                RootObject = mdl,
                BaseSkins = [skinController.skins[0]],
                GameObjectActivations = [],
                RendererInfos =
                [
                    new CharacterModel.RendererInfo
                        {
                            defaultMaterial = skinAssetBundle.LoadAsset<Material>("Assets/Anichan2023Mat.mat"),
                            defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                            ignoreOverlays = false,
                            renderer = renderers[6]
                        }
                ],
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
        }

        public SkinDefInfo CommunistSkin()
        {
            return new SkinDefInfo
            {
                Icon = Skins.CreateSkinIcon(Color.yellow, Color.red, Color.red, Color.red),
                Name = "AnichanCommunist",
                NameToken = "ANICHAN_COMMUNIST",
                RootObject = mdl,
                BaseSkins = [skinController.skins[0]],
                GameObjectActivations = [],
                RendererInfos =
                [
                    new CharacterModel.RendererInfo
                        {
                            defaultMaterial = skinAssetBundle.LoadAsset<Material>("Assets/AnichanCommunistMat.mat"),
                            defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                            ignoreOverlays = false,
                            renderer = renderers[6]
                        }
                ],
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

        }

        public SkinDefInfo FurrySkin()
        {
            return new SkinDefInfo
            {
                Icon = Skins.CreateSkinIcon(Color.white, new Color(1f, 0.75f, 0.60f), new Color(0f, 0.39f, 0.2f), new Color(1f, 0.38f, 0f)),
                Name = "AnichanFurry",
                NameToken = "ANICHAN_FURRY",
                RootObject = mdl,
                BaseSkins = [skinController.skins[0]],
                GameObjectActivations = [],
                RendererInfos =
                [
                    new CharacterModel.RendererInfo
                        {
                            defaultMaterial = skinAssetBundle.LoadAsset<Material>("Assets/AnichanFurryMat.mat"),
                            defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                            ignoreOverlays = false,
                            renderer = renderers[6]
                        }
                ],
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
        }
    }
}
