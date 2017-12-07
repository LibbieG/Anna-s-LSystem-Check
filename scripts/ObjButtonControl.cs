using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EasyWiFi.Core;

namespace EasyWiFi.ClientControls
{
    [AddComponentMenu("EasyWiFiController/Client/UserControls/Button")]
    public class ObjButtonControl : MonoBehaviour, IClientController
    {

        public string controlName = "Button1";
//        public Sprite buttonPressedSprite;
		public int islandNum;

        ButtonControllerType button;
//        Image currentImage;
//        Sprite buttonRegularSprite;
        string buttonKey;
//        Rect screenPixelsRect;
        int touchCount;
        bool pressed;
		float verticalSpeed = 1.0f;
		public float threshold = 120.0f;
		GameObject[] islands;
		int[] spawns;

        // Use this for initialization
        void Awake()
        {
            buttonKey = EasyWiFiController.registerControl(EasyWiFiConstants.CONTROLLERTYPE_BUTTON, controlName);
            button = (ButtonControllerType)EasyWiFiController.controllerDataDictionary[buttonKey];
//            currentImage = gameObject.GetComponent<Image>();
//            buttonRegularSprite = currentImage.sprite;
            
        }

        void Start()
        {
//            screenPixelsRect = EasyWiFiUtilities.GetScreenRect(currentImage.rectTransform);
        }

        //here we grab the input and map it to the data list
        void Update()
        {
            mapInputToDataStream();
        }

        public void mapInputToDataStream()
		{

			//reset to default values;
			//touch count is 0
			button.BUTTON_STATE_IS_PRESSED = false;
			pressed = false;

			//touch
//            touchCount = Input.touchCount;

			int fingersOnScreen = 0;
			foreach (Touch touch in Input.touches) {
				fingersOnScreen++; //Count fingers (or rather touches) on screen as you iterate through all screen touches.

				//You need two fingers on screen to pinch.
				if (fingersOnScreen == 2) {
					Touch swipeTouch = Input.GetTouch (1);
					if (swipeTouch.phase == TouchPhase.Moved) {
						float swipeUp = verticalSpeed * swipeTouch.deltaPosition.y;
//						print (swipeUp);
//            if (touchCount > 0)
//            {
//                for (int i = 0; i < touchCount; i++)
//                {
//					Touch touch = Input.GetTouch(i);
//
//                    //touch somewhere on control
//					RaycastHit hit;
//					Ray ray = Camera.main.ScreenPointToRay (touch.position);
//					if (Physics.Raycast (ray, out hit, 100.0f))
						if (swipeUp > threshold) {
							pressed = true;
							break;
						}
					}
				}

			}
				//show the correct image
				if (pressed) {
					button.BUTTON_STATE_IS_PRESSED = true;
//                currentImage.sprite = buttonPressedSprite;
				print ("hello");
				GameObject original = GameObject.FindGameObjectWithTag ("main");
				original.GetComponent<Animator> ().SetBool ("rise", true);

				islands = GameObject.FindGameObjectsWithTag("spawns");
				spawns = new int[islands.Length];
				print (islands.Length);
				for (int i = 0; i < islands.Length; i++) {
					islands[i].GetComponent<Animator>().SetBool ("rise", true);
					foreach (Animator anim in gameObject.GetComponentsInChildren<Animator>())
						anim.SetBool ("rise", true);
				}

				} else {
					button.BUTTON_STATE_IS_PRESSED = false;
//                currentImage.sprite = buttonRegularSprite;

				}
		}
	}
}
