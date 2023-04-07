using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENTER_YOUR_NAME_HERE
{
	public class ExampleInteractable_1 : Interactable
	{
		[Header("My Settings")]
		[SerializeField] private Light _lamp;
		[SerializeField] private Color _disabledColor;
		[SerializeField] private Color _enabledColor;
		[SerializeField] private Material _material;

		private bool _lightEnabled = true;

		//Этот метод вы можете удалить, если он вам не нужен
		//Вызывается, когда курсор входит в поле объекта
		public override void OnInteractorEnter(Interactor interactor)
		{
			base.OnInteractorEnter(interactor);
		}
		
		//Этот метод вы можете удалить, если он вам не нужен
		//Вызывается, когда курсор выходит из поля объекта
		public override void OnInteractorExit(Interactor interactor)
		{
			base.OnInteractorExit(interactor);
		}
		
		//Вызывается, когда пользователь нажимает кнопку E, смотря на объект
		public override void OnInteractDown(Interactor interactor)
		{
			_lightEnabled = !_lightEnabled;

			_lamp.enabled = _lightEnabled;
			_material.color = _lightEnabled ? _enabledColor : _disabledColor;
		}
		
		//Вызывается, когда пользователь отпускает кнопку E, смотря на объект
		//Не вызывается в случае, если во время зажатия кнопки курсор вышел за поле объекта
		public override void OnInteractUp(Interactor interactor)
		{
			
		}
		
		//Вызывается, когда пользователь пытается взаимодействовать с объектом и у объекта стоит CanInteract = false
		public override void OnTryInteract(Interactor interactor)
		{
			
		}
	}
}