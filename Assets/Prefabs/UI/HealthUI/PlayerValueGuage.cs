using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValueGuage : MonoBehaviour
{
   [SerializeField] private Image AmtImage;
   [SerializeField] private TextMeshProUGUI AmtText;

   internal void UpdateValue(float health, float delta, float maxhealth)
   {
      AmtImage.fillAmount = health / maxhealth;
      int healthAsInt = (int)health;
      AmtText.SetText(healthAsInt.ToString());
   }
}
