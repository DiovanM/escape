using UnityEngine;
using InputActions;

namespace Input
{

    public class InputManager : Singleton<InputManager>
    {

        public static Character character;
        public static Inventory inventory;

        private new void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(this);

            character = new Character();
            character.Enable();

            inventory = new Inventory();
            inventory.Enable();
        }

    }

}