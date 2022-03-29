using UnityEngine;

namespace Networking
{
    public class InitializeNormcore : MonoBehaviour
    {
        private Normal.Realtime.Realtime _realtime;
    
        private void Start()
        {
            _realtime = GetComponent<Normal.Realtime.Realtime>();
            _realtime.Connect(Application.isEditor ? "Dev Room" : "Test Room");
        }
    }
}