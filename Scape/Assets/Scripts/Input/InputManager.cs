using UnityEngine;
using InputActions;

namespace Input
{

    public class InputManager : MonoBehaviour
    {

        public static Character character;
        public static Inventory inventory;

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

            character = new Character();
            character.Enable();

            inventory = new Inventory();
            inventory.Enable();
        }

    }

}