namespace Oculus.Platform.Samples.VrHoops
{
    using UnityEngine;
    using Platform;
    using Models;

    public class PlatformManager : MonoBehaviour
    {
        private static PlatformManager s_instance;
        private MatchmakingManager m_matchmaking;
        private P2PManager m_p2p;
        private LeaderboardManager m_leaderboards;
        private AchievementsManager m_achievements;
        private State m_currentState;

        // my Application-scoped Oculus ID
        private ulong m_myID;

        // my Oculus user name
        private string m_myOculusID;

        private void Update()
        {
            m_p2p.UpdateNetwork();
            m_leaderboards.CheckForUpdates();
        }

        #region Initialization and Shutdown

        private void Awake()
        {
            // make sure only one instance of this manager ever exists
            if (s_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            s_instance = this;
            DontDestroyOnLoad(gameObject);

            Core.Initialize();
            m_matchmaking = new MatchmakingManager();
            m_p2p = new P2PManager();
            m_leaderboards = new LeaderboardManager();
            m_achievements = new AchievementsManager();
        }


        private void Start()
        {
            // First thing we should do is perform an entitlement check to make sure
            // we successfully connected to the Oculus Platform Service.
            Entitlements.IsUserEntitledToApplication().OnComplete(IsEntitledCallback);
        }

        private void IsEntitledCallback(Message msg)
        {
            if (msg.IsError)
            {
                TerminateWithError(msg);
                return;
            }

            // Next get the identity of the user that launched the Application.
            Users.GetLoggedInUser().OnComplete(GetLoggedInUserCallback);
        }

        private void GetLoggedInUserCallback(Message<User> msg)
        {
            if (msg.IsError)
            {
                TerminateWithError(msg);
                return;
            }

            m_myID = msg.Data.ID;
            m_myOculusID = msg.Data.OculusID;

            TransitionToState(State.WAITING_TO_PRACTICE_OR_MATCHMAKE);
            Achievements.CheckForAchievmentUpdates();
        }

        // In this example, for most errors, we terminate the Application.  A full App would do
        // something more graceful.
        public static void TerminateWithError(Message msg)
        {
            Debug.Log("Error: " + msg.GetError().Message);
            UnityEngine.Application.Quit();
        }

        public void QuitButtonPressed()
        {
            UnityEngine.Application.Quit();
        }

        private void OnApplicationQuit()
        {
            // be a good matchmaking citizen and leave any queue immediately
            Matchmaking.LeaveQueue();
        }

        #endregion

        #region Properties

        public static MatchmakingManager Matchmaking => s_instance.m_matchmaking;

        public static P2PManager P2P => s_instance.m_p2p;

        public static LeaderboardManager Leaderboards => s_instance.m_leaderboards;

        public static AchievementsManager Achievements => s_instance.m_achievements;

        public static State CurrentState => s_instance.m_currentState;

        public static ulong MyID
        {
            get
            {
                if (s_instance != null)
                    return s_instance.m_myID;
                else
                    return 0;
            }
        }

        public static string MyOculusID
        {
            get
            {
                if (s_instance != null && s_instance.m_myOculusID != null)
                    return s_instance.m_myOculusID;
                else
                    return string.Empty;
            }
        }

        #endregion

        #region State Management

        public enum State
        {
            // loading platform library, checking application entitlement,
            // getting the local user info
            INITIALIZING,

            // waiting on the user to join a matchmaking queue or play a practice game
            WAITING_TO_PRACTICE_OR_MATCHMAKE,

            // waiting for the match to start or viewing results
            MATCH_TRANSITION,

            // actively playing a practice match
            PLAYING_A_LOCAL_MATCH,

            // actively playing an online match
            PLAYING_A_NETWORKED_MATCH
        };

        public static void TransitionToState(State newState)
        {
            if (s_instance && s_instance.m_currentState != newState) s_instance.m_currentState = newState;
        }

        #endregion
    }
}