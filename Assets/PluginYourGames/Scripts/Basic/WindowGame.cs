using System;
using UnityEditor;
using UnityEngine;

namespace YG
{
    public static partial class YG2
    {
        public static bool isFocusWindowGame { get { return visibilityWindowGame; } }
        private static bool visibilityWindowGame = true;

        public static Action<bool> onFocusWindowGame;
        public static Action onShowWindowGame, onHideWindowGame;

#if UNITY_EDITOR
        private static bool exitingPlayMode;
#endif

#if UNITY_EDITOR || !PLATFORM_WEBGL
        [InitYG]
        private static void InitWindowGame()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
            Application.focusChanged -= SetFocusWindowGame;
            Application.focusChanged += SetFocusWindowGame;
        }
#endif
        public static void SetFocusWindowGame(bool visible)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying || exitingPlayMode)
                return;
#endif
            if (visible)
            {
                visibilityWindowGame = true;
#if UNITY_EDITOR
                if (infoYG.Simulation.pauseOnFocusGame) SetPause();
#elif !YandexGamesPlatform_yg
                SetPause();
#endif
                onFocusWindowGame?.Invoke(true);
                onShowWindowGame?.Invoke();

                void SetPause()
                {
                    if (!nowAdsShow)
                        PauseGame(false);
                }
            }
            else
            {
#if UNITY_EDITOR
                if (infoYG.Simulation.pauseOnFocusGame) SetPause();
#elif !YandexGamesPlatform_yg
                SetPause();
#endif
                visibilityWindowGame = false;

                onFocusWindowGame?.Invoke(false);
                onHideWindowGame?.Invoke();

                void SetPause()
                {
                    if (!nowAdsShow)
                        PauseGame(true);
                }
            }
        }

#if UNITY_EDITOR
        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
                exitingPlayMode = true;
            else if (state == PlayModeStateChange.EnteredPlayMode ||
                     state == PlayModeStateChange.EnteredEditMode)
                exitingPlayMode = false;
        }
#endif
    }
}

#if PLATFORM_WEBGL
namespace YG.Insides
{
    public partial class YGSendMessage
    {
        public void SetFocusWindowGame(string visible) => YG2.SetFocusWindowGame(visible == "true");
    }
}
#endif
