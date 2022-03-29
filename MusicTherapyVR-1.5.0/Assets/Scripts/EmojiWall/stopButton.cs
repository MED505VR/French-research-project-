using UnityEngine;

namespace EmojiWall
{
    public class stopButton : MonoBehaviour
    {
        [SerializeField]
        public bool play = false;
        private bool prevPlay;
        private GameObject emoji0, emoji1, emoji2;

        private AudioSource source;

        private GameObject leftHand, rightHand;

        private void Start()
        {
            leftHand = GameObject.Find("LeftHandAnchor");
            rightHand = GameObject.Find("RightHandAnchor");
            emoji0 = GameObject.Find("AngryEmoji");
            emoji1 = GameObject.Find("SadEmoji");
            emoji2 = GameObject.Find("NeutralEmoji");
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == leftHand || other.gameObject == rightHand)
            {
                if (emoji0 == true)
                {
                    emoji0.GetComponent<MoodPlay>()._play = false;
                    emoji0.GetComponent<MoodSync>().SetPlay(false);

                    emoji0.GetComponent<MoodPlay>().color();
                    emoji0.GetComponent<MoodSync>().SetColor(emoji0.GetComponent<MoodPlay>()._original);
                    emoji0.GetComponent<MoodSync>().SetLight(emoji0.GetComponent<MoodPlay>()._lightColor);
                    //emoji0.GetComponent<MoodPlay>().Update();
                }

                if (emoji1 == true)
                {
                    emoji1.GetComponent<MoodPlay>()._play = false;
                    emoji1.GetComponent<MoodSync>().SetPlay(false);

                    emoji1.GetComponent<MoodPlay>().color();
                    emoji1.GetComponent<MoodSync>().SetColor(emoji1.GetComponent<MoodPlay>()._original);
                    emoji1.GetComponent<MoodSync>().SetLight(emoji1.GetComponent<MoodPlay>()._lightColor);
                    //emoji1.GetComponent<MoodPlay>().Update();
                }

                if (emoji2 == true)
                {
                    emoji2.GetComponent<MoodPlay>()._play = false;
                    emoji2.GetComponent<MoodSync>().SetPlay(false);

                    emoji2.GetComponent<MoodPlay>().color();
                    emoji2.GetComponent<MoodSync>().SetColor(emoji2.GetComponent<MoodPlay>()._original);
                    emoji2.GetComponent<MoodSync>().SetLight(emoji2.GetComponent<MoodPlay>()._lightColor);
                    //emoji2.GetComponent<MoodPlay>().Update();
                }
            }
        }
    }
}
