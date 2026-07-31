#region

using System;
using System.Collections;
using System.Text;
using IbrahKit.Debugging;
using IbrahKit.Localization;
using IbrahKit.Manager;
using IbrahKit.UI.Menu;
using IbrahKit.UI.Modifier;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace IbrahKit.Dialog
{
    public class Dialog_Manager : Manager_Global<Dialog_Manager>
    {
        private const float cooldown = 0.25f;

        public static Dialog_Element dialog;

        [SerializeField] private float defaultCharDelay;

        [SerializeField] private UI_Modifier contentText;

        [SerializeField] private UI_Modifier nameText;

        [SerializeField] private UI_Modifier dismissText;

        [SerializeField] private UI_Menu menu;

        private UI_Modifier_Extension_Text_Setter contentTextSetter;

        private UI_Modifier_Extension_Text_Setter dismissTextSetter;

        private UI_Modifier_Extension_Text_Setter nameTextSetter;

        private Action onContinue;

        private Coroutine routine;

        private void OnDestroy()
        {
            if (GetInstance() == this) SceneManager.activeSceneChanged -= OnSceneChanged;
        }

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnSceneChanged(Scene _previousScene, Scene _newScene)
        {
            if (dialog != null && GetInstance() == this)
            {
                menu.GetStateController().Disable();
            }
        }

        public Coroutine StartDialog(Dialog_Element _dialog)
        {
            return StartCoroutine(StartDialogRoutine(_dialog));
        }

        public IEnumerator StartDialogRoutine(Dialog_Element _dialog)
        {
            if (_dialog == null)
            {
                IbrahDebug.LogWarning("No dialog provided");
                yield break;
            }

            if (menu == null)
            {
                IbrahDebug.LogWarning("No menus provided");
                yield break;
            }

            menu.GetStateController().Enable();

            dialog = _dialog;

            routine = StartCoroutine(RunDialog(dialog));

            yield return routine;

            routine = null;

            dialog = null;

            menu.GetStateController().Disable();
        }

        public IEnumerator RunDialog(Dialog_Element dialog)
        {
            foreach (var sub in dialog.GetSubElements())
            {
                if (sub.TryName(out string name)) nameTextSetter.SetText(name);

                contentTextSetter.SetText(string.Empty);
                dismissTextSetter.SetText(string.Empty);

                string key = sub.GetKey();

                string rawText = Local_Manager.GetInstance().GetString(key);

                StringBuilder sb = new();

                bool skip = false;

                onContinue += Skip;

                foreach (var character in rawText)
                {
                    string processed = sub.Process(character.ToString());

                    sb.Append(processed);

                    contentTextSetter.SetText(sb);

                    float time = sub.GetTime(character);

                    float timer = 0;

                    while (timer < time && !skip)
                    {
                        timer += UnityEngine.Time.deltaTime;

                        yield return null;
                    }
                }

                onContinue -= Skip;

                skip = false;

                dismissTextSetter.SetText("Dismiss");

                yield return new WaitForSeconds(cooldown);

                onContinue += Skip;

                yield return new WaitUntil(() => skip || sub.GetMode() == Dialog_Sub_Element.Mode.NOTSKIPABLE);

                yield return new WaitForSeconds(cooldown);

                onContinue -= Skip;
                continue;

                void Skip()
                {
                    if (sub.GetMode() == Dialog_Sub_Element.Mode.SKIPABLE) skip = true;
                }
            }
        }

        public void InvokeContinue()
        {
            onContinue?.Invoke();
        }
    }
}