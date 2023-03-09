using System;
using UnityEngine;

namespace HECS.Controllers
{
    [DefaultExecutionOrder(-990)]
    public class InputController : MonoBehaviour
    {
        public static InputController Instance;

        public float VerticalMove;// => verticalMove;
        public float HorizontalMove;// => horizontalMove;

    }
}
