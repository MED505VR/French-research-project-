using UnityEngine;

namespace EmojiWall
{
    public class MoodAnimation : MonoBehaviour
    {
        Animator anim;
        private MoodSound _moodSound;

        private void Start()
        {
            anim = GetComponent<Animator>();
            _moodSound = GetComponent<MoodSound>();
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space)) anim.SetTrigger("Press");

            if (_moodSound.play == true) anim.SetTrigger("Press");
        }
    }
}
