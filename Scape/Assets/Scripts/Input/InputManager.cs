using UnityEngine;
using InputActions;

namespace Input
{

    public class InputManager : MonoBehaviour
    {

        public static Player player;

        private static InputManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
            }

            player = new Player();
            player.Enable();
        }

    }

}