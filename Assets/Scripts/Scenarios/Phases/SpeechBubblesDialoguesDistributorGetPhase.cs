using UnityEngine;

namespace Assets.Scripts.Scenarios.Phases
{
    public class SpeechBubblesDialoguesDistributorGetPhase : SpeechBubblesDialoguePhaseBase
    {
        [SerializeField]
        private DialoguesDistributorKey _key;

        protected override Dialogue GetDialogue()
        {
            return DialoguesDistributorAddPhase.GetNextDialogue(_key);
        }

        public override string IconName => "DialogueGet2.png";
    }
}
