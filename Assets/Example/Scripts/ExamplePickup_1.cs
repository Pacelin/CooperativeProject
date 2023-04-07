using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENTER_YOUR_NAME_HERE
{
	public class ExamplePickup_1 : Pickup
	{
		//Этот метод вы можете удалить, если он вам не нужен
		//Вызывается, когда игрок поднимает предмет
		public override void OnTake(Inventory inventory)
		{
			base.OnTake(inventory);
			Debug.Log("Молодец, взял предмет 1");
		}
		
		//Этот метод вы можете удалить, если он вам не нужен
		//Вызывается, когда игрок выбрасывает предмет
		public override void OnDrop(Inventory inventory)
		{
			base.OnDrop(inventory);
			Debug.Log("Молодец, выбросил предмет 1");
		}
		
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
		
		//Вызывается, когда пользователь пытается взаимодействовать с объектом и у объекта стоит CanInteract = false
		public override void OnTryInteract(Interactor interactor)
		{
			
		}
	}
}