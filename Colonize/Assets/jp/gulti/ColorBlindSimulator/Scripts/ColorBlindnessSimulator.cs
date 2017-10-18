using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace jp.gulti.ColorBlind
{

    public class ColorBlindnessSimulator : jp.gulti.PostEffectsBase
    {
        public enum ColorBlindMode
        {
            Protanope,
            Deuteranope,
            Tritanope,
            Monochromacy
        }
        [SerializeField]
        public ColorBlindMode BlindMode = ColorBlindMode.Protanope;

        [SerializeField]
        public float BlindIntensity = 0f;

        [SerializeField]
        public Shader ColorBlindShader;
        private Material ColorBlindMat;

        private readonly Material[] materials = new Material[2];
        private bool off = false;

        public static readonly string ColorBlindShaderName = "Hidden/GULTI/ColorBlindSimulator";

        #region Overrides

        protected override bool CheckResources()
        {
            CheckSupport(false);
            ColorBlindShader = Shader.Find(ColorBlindShaderName);
            ColorBlindMat = CreateMaterial(ColorBlindShader, ColorBlindMat);
            return ColorBlindMat != null;
        }

        #endregion

        #region Monobehavior

        private void Awake()
        {
            materials[0] = new Material(Shader.Find("Hidden/Tritanopia"));
            materials[1] = new Material(Shader.Find("Hidden/Monochromacy"));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                BlindMode = ColorBlindMode.Protanope;
                BlindIntensity = 0f;
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                BlindMode = ColorBlindMode.Protanope;
                BlindIntensity = 1.0f;
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                BlindMode = ColorBlindMode.Deuteranope;
                BlindIntensity = 1.0f;
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                BlindMode = ColorBlindMode.Tritanope;
                BlindIntensity = 1.0f;
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                BlindMode = ColorBlindMode.Monochromacy;
                BlindIntensity = 1.0f;
            }
        }

        void OnDisable()
        {
            if (ColorBlindMat != null)
            {
#if UNITY_EDITOR
                if (!UnityEditor.EditorApplication.isPlaying)
                    DestroyImmediate(ColorBlindMat, true);
                else
#endif
                    Destroy(ColorBlindMat);
            }
        }

        void OnRenderImage(RenderTexture _src, RenderTexture _dst)
        {
            if (ColorBlindMat == null)
            {
                if (!CheckResources())
                {
                    NotSupported();
                    return;
                }
            }
            Material temp;
            switch (BlindMode)
            {
                case ColorBlindMode.Protanope:
                    ColorBlindMat.shaderKeywords = new string[] { "CB_TYPE_ONE" };
                    break;
                case ColorBlindMode.Deuteranope:
                    ColorBlindMat.shaderKeywords = new string[] { "CB_TYPE_TWO" };
                    break;
                case ColorBlindMode.Tritanope:
                    temp = materials[0];
                    temp.SetFloat("_BlindIntensity", BlindIntensity);
                    Graphics.Blit(_src, _dst, materials[0]);
                    return;
                case ColorBlindMode.Monochromacy:
                    temp = materials[1];
                    temp.SetFloat("_BlindIntensity", BlindIntensity);
                    Graphics.Blit(_src, _dst, materials[1]);
                    return;
            }

            //Intensity Set
            ColorBlindMat.SetFloat("_BlindIntensity", BlindIntensity);

            Graphics.Blit(_src, _dst, ColorBlindMat);
        }

        #endregion
    }
}