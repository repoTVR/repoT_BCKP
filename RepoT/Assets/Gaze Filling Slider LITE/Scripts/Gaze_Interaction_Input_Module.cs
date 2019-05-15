using UnityEngine.UI;

/// <summary>
/// Add this script to the EventSystem object in your scene.
/// This script creates a custom input module based on the one provided
/// by the OVR package.
/// </summary>
namespace UnityEngine.EventSystems
{
    public class Gaze_Interaction_Input_Module : OVRInputModule
    {
        //Public Variables.
        [Tooltip("How long to wait until a gazed control is activated")]
        [Header("How long you will wait until a gazed control is activated?")]
        public float activationDwellTime = 1.0f;

        //Private Variables.
        private GameObject currentlyGazedObject;
        private float lastTimeGazedObjectChanged;

        override public void Process()
        {
            base.Process();

            //Handle selection.
            if (!currentlyGazedObject || lastTimeGazedObjectChanged > Time.time)
            {
                OVRGazePointer.instance.SelectionProgress = 0f;
            }
            else
            {
                float currentDwellTime = Time.time - lastTimeGazedObjectChanged;
                OVRGazePointer.instance.SelectionProgress = currentDwellTime / activationDwellTime;
            }
        }

        /// <summary>
        /// Overwritten from the base version so we can use our custom. 
        /// </summary>
        /// <returns></returns>
        override protected MouseState GetGazePointerData()
        {
            MouseState state = base.GetGazePointerData();

            var raycast = state.GetButtonState(PointerEventData.InputButton.Left).eventData.buttonData.pointerCurrentRaycast;

            //Get custom press state.
            PointerEventData.FramePressState pressState = GetGazeButtonState(raycast.gameObject);
            //Set custom press state.
            state.SetButtonState(PointerEventData.InputButton.Left, pressState, state.GetButtonState(PointerEventData.InputButton.Left).eventData.buttonData);

            return state;
        }

        /// <summary>
        /// Modified version of the base class that takes into account when which object got hit.
        /// </summary>
        /// <returns></returns>
        protected PointerEventData.FramePressState GetGazeButtonState(GameObject rayCastHit)
        {
            var pressed = Input.GetKeyDown(gazeClickKey) || OVRInput.GetDown(joyPadClickButton);
            var released = Input.GetKeyUp(gazeClickKey) || OVRInput.GetUp(joyPadClickButton);

            bool shouldImmediatelyRelease;
            GameObject newGazedObject = GetCurrentlyGazedGameObject(rayCastHit, out shouldImmediatelyRelease);
            if (currentlyGazedObject != newGazedObject)
            {
                released |= true;
                currentlyGazedObject = newGazedObject;
                lastTimeGazedObjectChanged = Time.time;
            }

            float currentDwellTime = Time.time - lastTimeGazedObjectChanged;
            if (currentlyGazedObject && currentDwellTime >= activationDwellTime)
            {
                pressed |= true;
                if (shouldImmediatelyRelease)
                {
                    //Reset the time so this doesn't get activated again.
                    lastTimeGazedObjectChanged = float.MaxValue;
                    released |= true; //Simulate click.
                }
            }

            if (pressed && released)
                return PointerEventData.FramePressState.PressedAndReleased;
            if (pressed)
                return PointerEventData.FramePressState.Pressed;
            if (released)
                return PointerEventData.FramePressState.Released;
            return PointerEventData.FramePressState.NotChanged;
        }

        //Method for receiving information about the currently gazed object.
        private GameObject GetCurrentlyGazedGameObject(GameObject go, out bool shouldImmediatelyRelease)
        {
            shouldImmediatelyRelease = true;
            if (!go)
            {
                return null;
            }

            Slider slider = go.GetComponentInParent<Slider>();
            if (slider)
            {
                shouldImmediatelyRelease = false;
                return slider.gameObject;
            }
            Button button = go.GetComponentInParent<Button>();
            if (button)
            {
                return button.gameObject;
            }
            Toggle toggle = go.GetComponentInParent<Toggle>();
            if (toggle)
            {
                return toggle.gameObject;
            }

            return null;
        }

        public void SetActivationDwellTime(float v)
        {
            activationDwellTime = v;
        }
    }
}