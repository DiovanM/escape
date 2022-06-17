using UnityEngine;
using InputActions;

namespace Input
{

    public class InputManager : MonoBehaviour
    {

        public static Player player;

        private static InputManager _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }
            else
            {
                DontDestroyOnLoad(this);
            }


            player = new Player();
            player.Enable();
        }

    }

}