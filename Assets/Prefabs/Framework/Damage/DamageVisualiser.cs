using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisualiser : MonoBehaviour
{

    [SerializeField] Renderer mesh;
    [SerializeField] Color DamageEmissionColor;
    [SerializeField] float BlinkSpeed = 2f;
    [SerializeField] string EmmissionColorPropertyName = "_Addition";
    [SerializeField] HealthComponent healthComponent;

    Color OriginalEmissionColor;

    // Start is called before the first frame update
    void Start()
    {
        Material mat = mesh.material;
        mesh.material = new Material(mat);

        OriginalEmissionColor = mesh.material.GetColor(EmmissionColorPropertyName);
        healthComponent.onTakeDamage += TookDamage;
    }

    protected virtual void TookDamage(float health, float delta, float maxHealth, GameObject Instigator)
    {
        Color currentEmissionColor = mesh.material.GetColor(EmmissionColorPropertyName);
        if (Mathf.Abs(currentEmissionColor.grayscale - OriginalEmissionColor.grayscale) < 0.1f)
        {
            mesh.material.SetColor(EmmissionColorPropertyName, DamageEmissionColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Color currentEmissionColor = mesh.material.GetColor(EmmissionColorPropertyName);
        Color newEmissionColor = Color.Lerp(currentEmissionColor, OriginalEmissionColor, Time.deltaTime * BlinkSpeed);
        mesh.material.SetColor(EmmissionColorPropertyName, newEmissionColor);
    }
}


