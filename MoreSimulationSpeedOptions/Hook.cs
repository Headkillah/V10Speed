/* 
 * Copyright(c) 2015 Alexander Dzhoganov
 * Copyright(c) 2018 Thomas Rohloff <v10lator@myway.de>
 * 
 * Licensed under the MIT
 */

using System;
using ColossalFramework.UI;
using UnityEngine;

namespace MoreSimulationSpeedOptions
{
    public class Hook : MonoBehaviour
    {
        private UIButton speedButton;
        private UIMultiStateButton speedBar;

        private Color32 white = new Color32(255, 255, 255, 255);
        private Color32 red = new Color32(255, 0, 0, 255);

		private int oldSpeed;

        void OnDestroy()
        {
            speedBar.isVisible = true;
            Destroy(speedButton.gameObject);
        }

        void Awake()
        {
            var multiStateButtons = GameObject.FindObjectsOfType<UIMultiStateButton>();
            foreach (var button in multiStateButtons)
            {
                if (button.name == "Speed")
                {
                    speedBar = button;
                    break;
                }
            }

            speedBar.isVisible = false;

            // Create a GameObject with a ColossalFramework.UI.UIButton component.
            var buttonObject = new GameObject("MoreSimulationSpeedOptionsButton", typeof(UIButton));

            // Make the buttonObject a child of the uiView.
            buttonObject.transform.parent = speedBar.transform.parent.transform;

            // Get the button component.
            speedButton = buttonObject.GetComponent<UIButton>();

            // Set the text to show on the button.
			SimulationManager.instance.SelectedSimulationSpeed = oldSpeed = 1;
            speedButton.text = "x1";

            // Set the button dimensions.
            speedButton.width = speedBar.width;
            speedButton.height = speedBar.height;

            // Style the button to look like a menu button.
            speedButton.normalBgSprite = "ButtonMenu";
            speedButton.disabledBgSprite = "ButtonMenuDisabled";
            speedButton.hoveredBgSprite = "ButtonMenuHovered";
            speedButton.focusedBgSprite = "ButtonMenu";
            speedButton.pressedBgSprite = "ButtonMenuPressed";
            speedButton.textColor = new Color32(255, 255, 255, 255);
            speedButton.disabledTextColor = new Color32(7, 7, 7, 255);
            speedButton.hoveredTextColor = new Color32(255, 255, 255, 255);
            speedButton.focusedTextColor = new Color32(255, 255, 255, 255);
            speedButton.pressedTextColor = new Color32(30, 30, 44, 255);

            // Place the button.
            speedButton.transformPosition = speedBar.transformPosition;

            // Respond to button click.
            speedButton.eventMouseDown += (component, param) =>
            {
				int speed = SimulationManager.instance.SelectedSimulationSpeed;

                if ((param.buttons & UIMouseButton.Left) != 0)
                {
                    switch (speed)
                    {
                        case 1:
                            speed = 2;
                            break;
                        case 2:
                            speed =  4;
                            break;
                        case 4:
                            speed = 6;
                            break;
                        case 6:
                            speed = 9;
                            break;
                        case 9:
                        case 99:
                            speed = 1;
                            break;
                    }
					SimulationManager.instance.SelectedSimulationSpeed = speed;
                }
                else if ((param.buttons & UIMouseButton.Right) != 0)
                {
                    switch (speed)
                    {
                        case 1:
                            speed = 9;
                            break;
                        case 2:
                            speed = 1;
                            break;
                        case 4:
                            speed = 2;
                            break;
                        case 6:
                            speed = 4;
                            break;
                        case 9:
							speed = 6;
							break;
                        case 99:
                            speed = 9;
                            break;
                    }
					SimulationManager.instance.SelectedSimulationSpeed = speed;
                }

                if (Input.GetKey(KeyCode.LeftControl))
                    SimulationManager.instance.SelectedSimulationSpeed = 99;
            };
        }

		void Update()
        {
			speedButton.transformPosition = speedBar.transformPosition;
			speedButton.transform.position = speedBar.transform.position;

            int speed = SimulationManager.instance.SelectedSimulationSpeed;
			if (speed == oldSpeed)
				return;
			
            speedButton.text = "x" + speed.ToString();
			speedButton.textColor = speedButton.focusedTextColor = speedButton.hoveredTextColor = speed > 3 ? red : white;
			oldSpeed = speed;
        }

    }
}
