using BepInEx;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.SceneManagement;
using BepInEx.Logging;
using RoR2;

namespace AntlersDecay
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;

        public const string PluginAuthor = "HIFU";
        public const string PluginName = "AntlersDecay";
        public const string PluginVersion = "1.1.0";

        public static ManualLogSource adLogger;

        public static GameObject ppHolder;
        public static GameObject ppHolderUI;

        public static Color32 rallypointDeltaColor = new(187, 236, 255, 255);
        public static Color32 titanicPlainsColor = new(238, 238, 238, 255);
        public static Color32 commencementStartColor = new(70, 83, 120, 255);
        public static Color32 commencementMidColor = new(37, 43, 52, 125);
        public static Color32 grayColor = new(174, 174, 174, 255);

        public void Awake()
        {
            adLogger = base.Logger;
            ppHolder ??= InitPP("AntlersDecay");
            // ppHolderUI ??= InitPP("AntlersDecayUI");
            On.RoR2.Highlight.Awake += Highlight_Awake;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            var name = scene.name;
            SwitchPP(name);
        }

        private void Highlight_Awake(On.RoR2.Highlight.orig_Awake orig, RoR2.Highlight self)
        {
            orig(self);
            self.strength = 0.75f;
        }

        public static void SwitchPP(string sceneName)
        {
            var profile = ppHolder.GetComponent<PostProcessVolume>().profile;
            profile.name = sceneName + " | Antler's Decay";
            var colorGrading = profile.GetSetting<ColorGrading>();
            adLogger.LogDebug("color grading is " + colorGrading);
            switch (sceneName)
            {
                case "dampcavesimple":
                    colorGrading.saturation.value = -64f;
                    colorGrading.contrast.value = 20f;
                    colorGrading.colorFilter.value = Color.white;
                    break;

                case "wispgraveyard":
                    colorGrading.saturation.value = -30f;
                    colorGrading.contrast.value = 5f;
                    colorGrading.colorFilter.value = Color.white;
                    break;

                case "rootjungle":
                    colorGrading.saturation.value = -40f;
                    colorGrading.contrast.value = 0f;
                    colorGrading.colorFilter.value = Color.white;
                    break;

                case "frozenwall":
                    colorGrading.saturation.value = 0f;
                    colorGrading.contrast.value = 15f;
                    colorGrading.colorFilter.value = rallypointDeltaColor;
                    break;

                case "golemplains":
                    colorGrading.contrast.value = 25f;
                    colorGrading.saturation.value = -30f;
                    colorGrading.colorFilter.value = titanicPlainsColor;
                    break;

                case "golemplains2":
                    colorGrading.contrast.value = 25f;
                    colorGrading.saturation.value = -30f;
                    colorGrading.colorFilter.value = titanicPlainsColor;
                    break;

                case "moon2":
                    var pp = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).GetChild(0).Find("FX").GetChild(0);
                    var bruh = pp.GetComponent<PostProcessVolume>();
                    var rampFog = bruh.profile.GetSetting<RampFog>();
                    rampFog.fogColorStart.value = commencementStartColor;
                    rampFog.fogColorMid.value = commencementMidColor;
                    bruh.weight = 0.68f;
                    var enbyent = pp.GetComponent<HookLightingIntoPostProcessVolume>();
                    enbyent.overrideAmbientColor = new Color32(71, 71, 81, 255);
                    enbyent.overrideDirectionalColor = new Color32(41, 34, 41, 255);
                    colorGrading.saturation.value = -10f;
                    colorGrading.contrast.value = 15f;
                    colorGrading.colorFilter.value = Color.white;
                    break;

                case "arena":
                    colorGrading.saturation.value = -30f;
                    colorGrading.contrast.value = 15f;
                    colorGrading.colorFilter.value = grayColor;
                    break;

                case "goolake":
                    colorGrading.contrast.value = 15f;
                    colorGrading.saturation.value = -20f;
                    colorGrading.colorFilter.value = titanicPlainsColor;
                    break;

                case "voidraid":
                    colorGrading.saturation.value = -30f;
                    colorGrading.contrast.value = 15f;
                    colorGrading.colorFilter.value = Color.white;
                    break;

                case "voidstage":
                    colorGrading.saturation.value = -20f;
                    colorGrading.contrast.value = 15f;
                    colorGrading.colorFilter.value = Color.white;
                    break;

                case "skymeadow":
                    colorGrading.saturation.value = -20f;
                    colorGrading.contrast.value = 5f;
                    colorGrading.colorFilter.value = Color.white;
                    break;

                case "foggyswamp":
                    colorGrading.contrast.value = 5f;
                    colorGrading.saturation.value = -30f;
                    colorGrading.colorFilter.value = titanicPlainsColor;
                    break;

                case "blackbeach":
                    colorGrading.saturation.value = -30f;
                    colorGrading.contrast.value = 10f;
                    colorGrading.colorFilter.value = Color.white;
                    break;

                case "blackbeach2":
                    colorGrading.saturation.value = -30f;
                    colorGrading.contrast.value = 10f;
                    colorGrading.colorFilter.value = Color.white;
                    break;

                case "sulfurpools":
                    colorGrading.saturation.value = -30f;
                    colorGrading.contrast.value = 5f;
                    colorGrading.colorFilter.value = Color.white;
                    break;

                case "shipgraveyard":
                    colorGrading.saturation.value = -20f;
                    colorGrading.contrast.value = 10f;
                    colorGrading.colorFilter.value = Color.white;
                    break;

                default:
                    colorGrading.saturation.value = -15f;
                    colorGrading.contrast.value = 15f;
                    colorGrading.colorFilter.value = Color.white;
                    break;
            }
        }

        public static GameObject InitPP(string name)
        {
            GameObject holder = new(name);
            DontDestroyOnLoad(holder);
            holder.layer = RoR2.LayerIndex.postProcess.intVal;
            holder.AddComponent<AntlersDecayController>();
            PostProcessVolume pp = holder.AddComponent<PostProcessVolume>();
            DontDestroyOnLoad(pp);
            pp.isGlobal = true;
            pp.weight = 1f;
            pp.priority = float.MaxValue;
            var ppProfile = ScriptableObject.CreateInstance<PostProcessProfile>();
            DontDestroyOnLoad(ppProfile);
            ppProfile.name = "pp" + name;
            var colorGrading = ppProfile.AddSettings<ColorGrading>();
            colorGrading.SetAllOverridesTo(true);

            pp.sharedProfile = ppProfile;

            colorGrading.enabled.value = true;
            colorGrading.saturation.value = -20f;
            colorGrading.contrast.value = 30f;
            colorGrading.tonemapper.value = Tonemapper.Custom;
            colorGrading.toneCurveToeStrength.value = 1f;
            colorGrading.toneCurveToeLength.value = 0.15f;
            colorGrading.lift.value = new Vector4(1f, 1f, 1.03f, 0f);
            colorGrading.gamma.value = new Vector4(1.15f, 1f, 1f, 0f);

            var grain = ppProfile.AddSettings<Grain>();
            grain.SetAllOverridesTo(true);

            grain.intensity.value = 0.13f;
            var bloom = ppProfile.AddSettings<Bloom>();
            bloom.SetAllOverridesTo(true);

            var ambientOcclusion = ppProfile.AddSettings<AmbientOcclusion>();
            ambientOcclusion.SetAllOverridesTo(true);
            var vignette = ppProfile.AddSettings<Vignette>();
            vignette.SetAllOverridesTo(true);

            ambientOcclusion.intensity.value = 0.3f;
            ambientOcclusion.radius.value = 0.25f;
            ambientOcclusion.thicknessModifier.value = 100f;
            ambientOcclusion.upsampleTolerance.value = 0f;

            vignette.intensity.value = 0.2f;

            return holder;
        }
    }

    public class AntlersDecayController : MonoBehaviour
    {
        public PostProcessVolume volume;

        public void Start()
        {
            volume = GetComponent<PostProcessVolume>();
        }
    }
}