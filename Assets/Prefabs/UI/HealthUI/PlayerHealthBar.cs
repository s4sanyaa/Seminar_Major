using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
   [SerializeField] private Image AmtImage;
   [SerializeField] private TextMeshProUGUI AmtText;

   internal void UpdateHealth(float health, float delta, float maxhealth)
   {
      AmtImage.fillAmount = health/maxhealth;
      AmtText.SetText(health.ToString());
   }
}
