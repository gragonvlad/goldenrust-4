using Oxide.Core.Libraries.Covalence;
using System.Collections.Generic;
using System;
using Oxide.Core.Libraries;
using Oxide.Game.Rust.Cui;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("First plugin", "Wasdik", "0.1.0")]
    [Description("Our first pugin for Rust")]
    class WasdikGUI : RustPlugin
    {
        private void Init()
        {
            
        }
		
		
		[ChatCommand("help")]
		private void PostRequest(BasePlayer player)
		{
			#region ChatCommand и Фон
			string Layer = "FirstPage";
			CuiElementContainer Container = new CuiElementContainer();
			Container.Add(new CuiPanel
			{
				CursorEnabled = true,
				RectTransform = { AnchorMin = "0 0", AnchorMax = "1 1", OffsetMax = "0 0" },
				Image = { Color = "0 0 0 0.4", Sprite = "Assets/Content/UI/UI.Background.Tile.psd", Material = "assets/content/ui/uibackgroundblur.mat", FadeIn = 0.2f },
				FadeOut = 0.2f
			}, "Overlay", Layer);
			#endregion
			#region gui
			//Текст
			Container.Add(new CuiButton
			{
				RectTransform = { AnchorMin = "0.3425 0.13", AnchorMax = "0.6393 0.7644", OffsetMin = "0 0", OffsetMax = "0 0" },
				Button = { Color = "0.48 0.41 0.93 1"},
				Text = { Text = "Информация:\n1)Kits - стартовые наборы\n2) Map - игровая карта", Align = TextAnchor.MiddleCenter, FontSize = 40 },
			}, Layer);

			Container.Add(new CuiButton
			{
				RectTransform = { AnchorMin = "0.34375 0.16", AnchorMax = "0.6381 0.2255", OffsetMin = "0 0", OffsetMax = "0 0" },
				Button = { Color = "0.4739 0.4739 0.4739 0.7170817", Command = "close",Close = Layer},
				Text = { Text = "Закрыть", Align = TextAnchor.MiddleCenter,FontSize = 40 },
			}, Layer);
			CuiHelper.AddUi(player, Container);
			#endregion
		}
		

		//Команда
		[ChatCommand("Info")]
		void DrawGui(BasePlayer player)
		{
			#region ChatCommand и Фон
			//Сообщение При открытие меню!!
			PrintToChat("Вы успешно открыли меню!");
			//Добавление Страници!!
			string Layer = "FirstPage";
			//Добавление CuiElement,CuiButton,CuiElementContainer,CuiPanel,Container,container
			CuiElementContainer Container = new CuiElementContainer();
			Container.Add(new CuiPanel
			{
				//Фон
				CursorEnabled = true,
				RectTransform = { AnchorMin = "0 0", AnchorMax = "1 1", OffsetMax = "0 0" },
				Image = { Color = "0 0 0 0.4", Sprite = "Assets/Content/UI/UI.Background.Tile.psd", Material = "assets/content/ui/uibackgroundblur.mat", FadeIn = 0.2f },
				FadeOut = 0.2f
			}, "Overlay", Layer);
			#endregion
			//Меню
			#region Gui and Ui
			Container.Add(new CuiElement 
			{ 
				Parent = Layer, 
				Components = 
				{ 
					new CuiImageComponent {FadeIn = 0.25f, Color = "0 0 0 0.4"}, 
					new CuiRectTransformComponent {AnchorMin = "0.0006250 0.0600000", AnchorMax = " 0.9925 0.9333333"} 
				} 
			}); 
			/*Container.Add(new CuiButton 
			{ 
			RectTransform = { AnchorMin = "0.43749 0.8677778", AnchorMax = "0.5581242 0.93"}, 
			Button = { Color = "0.48 0.41 0.93 1"}, 
			Text = { Text = "название сервера", Align = TextAnchor.MiddleCenter, FontSize = 20 }, 
			}, Layer);*/
			//Кнопка,Кнопки 
			Container.Add(new CuiButton 
			{ 
				RectTransform = { AnchorMin = "0.8725 0.87", AnchorMax = "0.9924 0.93", }, 
				Button = { Color = "0.48 0.41 0.93 1", Command = "Close", Close = Layer}, 
				Text = { Text = "Закрыть", Align = TextAnchor.MiddleCenter,FontSize = 20 }, 
			}, Layer); 
			Container.Add(new CuiButton 
			{ 
				RectTransform = { AnchorMin = "0.0043 0.8688", AnchorMax = "0.1212431 0.9266668", }, 
				Button = { Color = "0.48 0.41 0.93 1", Command = "help"}, 
				Text = { Text = "Вкладка1", Align = TextAnchor.MiddleCenter,FontSize = 20 }, 
			}, Layer); 

			Container.Add(new CuiButton 
			{ 
				RectTransform = { AnchorMin = "0.0031249 0.8011124", AnchorMax = "0.121875 0.857779", }, 
				Button = { Color = "0.48 0.41 0.93 1", Command = "help1"}, 
				Text = { Text = "Вкладка2", Align = TextAnchor.MiddleCenter,FontSize = 20 }, 
			}, Layer); 
			Container.Add(new CuiButton 
			{ 
				RectTransform = { AnchorMin = "0.0025000 0.7322236", AnchorMax = "0.121875 0.7900013", }, 
				Button = { Color = "0.48 0.41 0.93 1", Command = "help2"}, 
				Text = { Text = "Вкладка3", Align = TextAnchor.MiddleCenter,FontSize = 20 }, 
			}, Layer); 
			Container.Add(new CuiButton 
			{ 
				RectTransform = { AnchorMin = "0.0018750 0.6622236", AnchorMax = "0.121875 0.7200013", }, 
				Button = { Color = "0.48 0.41 0.93 1", Command = "help3"}, 
				Text = { Text = "Вкладка4", Align = TextAnchor.MiddleCenter,FontSize = 20 }, 
			}, Layer);
			Container.Add(new CuiButton 
			{ 
				RectTransform = { AnchorMin = "0.0018751 0.5911124", AnchorMax = "0.121875 0.6488903", }, 
				Button = { Color = "0.48 0.41 0.93 1", Command = "help4"}, 
				Text = { Text = "Вкладка5", Align = TextAnchor.MiddleCenter,FontSize = 20 }, 
			}, Layer);
			//Метод,Хук,Хуки,Методы 
			CuiHelper.AddUi(player, Container);
			#endregion
		}
		
    }
}