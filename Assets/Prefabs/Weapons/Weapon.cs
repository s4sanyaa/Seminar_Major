using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
   [SerializeField] private string AttachSlotTag;
   [SerializeField] private AnimatorOverrideController _animatorOverrideController;
   [SerializeField] private float AttackRateMult = 1f;
   public abstract void Attack();
   
   public string GetAttachSlotTag()
   {
      return AttachSlotTag;
   }
   public GameObject Owner
   {
      get;
      private set;
   }

   public void Init(GameObject owner)
   {
      Owner = owner;
      Unequip();
   }

   public void Equip()
   {
      gameObject.SetActive(true);
      Owner.GetComponent<Animator>().runtimeAnimatorController = _animatorOverrideController;
      Owner.GetComponent<Animator>().SetFloat("AttackRateMult",AttackRateMult);
   }
   
   public void Unequip()
   {
      gameObject.SetActive(false);
   }
   public void DamageGameObject(GameObject objToDamage, float amt)
   {
     HealthComponent healthComponent= objToDamage.GetComponent<HealthComponent>();
     if (healthComponent != null)
     {
        healthComponent.changeHealth(-amt, Owner);
     }
   }

}
