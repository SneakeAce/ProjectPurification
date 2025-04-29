using UnityEngine;
using Zenject;

public class PlaceableWoodBarrier : PlaceableObject
{
    private PlaceableObjectAttack _placeableObjectAttack;

    //[Inject]
    //private void Construct(PlaceableObjectAttack placeableObjectAttack)
    //{
    //    _placeableObjectAttack = placeableObjectAttack;
    //}

    //private void OnTriggerStay(Collider collider)
    //{
    //    _placeableObjectAttack.OnTriggerStay(collider);
    //}
}
