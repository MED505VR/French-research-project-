using UnityEngine;

namespace C_scale
{
    public class Echo : MonoBehaviour
    {
        public bool echo = false;
        private bool previousEcho;

        public GameObject table;
        public AudioEchoFilter[] note;

        private MeshRenderer meshRenderer;
        private GameObject button;

        private void Awake()
        {
            button = GameObject.Find("Echo");
            meshRenderer = button.GetComponent<MeshRenderer>();
            table = GameObject.Find("Table");
            note = table.GetComponentsInChildren<AudioEchoFilter>();
            for (var i = 0; i < note.Length; i++)
            {
                note[i].enabled = false;
                echo = false;
            }
        }

        // Start is called before the first frame update
        // Update is called once per frame
        private void Update()
        {
            if (echo != previousEcho)
            {
                if (echo)
                    for (var i = 0; i < note.Length; i++)
                    {
                        meshRenderer.material.color = Color.green;
                        note[i].enabled = true;
                        previousEcho = echo;
                    }

                if (!echo)
                    for (var i = 0; i < note.Length; i++)
                    {
                        meshRenderer.material.color = Color.black;
                        note[i].enabled = false;
                        previousEcho = echo;
                    }
            }
        }
    }
}