#region

using System.Collections;
using System.Text;
using IbrahKit.Debugging;
using IbrahKit.Localization;
using IbrahKit.Manager;
using IbrahKit.ThreeDPlayer;
using IbrahKit.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace IbrahKit.Dialog
{
    public class Dialog_Manager : Manager_Global<Dialog_Manager>
    {
        private const float cooldown = 0.25f;

        private Coroutine routine;

        private Player3D_Input input;

        private UI_Interative_Extension_Text_Setter contentTextSetter;

        private UI_Interative_Extension_Text_Setter nameTextSetter;

        private UI_Interative_Extension_Text_Setter dismissTextSetter;

        [SerializeField] private float defaultCharDelay;

        [SerializeField] private UI_Interactive contentText;

        [SerializeField] private UI_Interactive nameText;

        [SerializeField] private UI_Interactive dismissText;

        [SerializeField] private UI_Menu menu;

        public static Dialog_Element dialog;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            SceneManager.activeSceneChanged += OnSceneChanged;

            input = new();

            input.Enable();
        }

        private void OnDestroy()
        {
            if (GetInstance() == this) SceneManager.activeSceneChanged -= OnSceneChanged;
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

                foreach (var character in rawText)
                {
                    string processed = sub.Process(character.ToString());

                    sb.Append(processed);

                    contentTextSetter.SetText(sb);

                    float time = sub.GetTime(character);

                    float timer = 0;

                    while(timer < time && !skip)
                    {
                        timer += UnityEngine.Time.deltaTime;

                        yield return null;

                        if (input.Player.Interact.IsPressed() && sub.GetMode() == Dialog_Sub_Element.Mode.SKIPABLE) skip = true;
                    }
                }

                dismissTextSetter.SetText("Dismiss");

                yield return new WaitForSeconds(cooldown);

                yield return new WaitUntil(() => input.Player.Interact.IsPressed() || sub.GetMode() == Dialog_Sub_Element.Mode.NOTSKIPABLE);

                yield return new WaitForSeconds(cooldown);
            }
        }
    }
}