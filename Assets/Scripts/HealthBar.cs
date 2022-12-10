using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform healthBar;
    public Transform healthBarIndicator;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        StartCoroutine(UpdateHealthBarCoroutine(currentHealth, maxHealth));
    }

    public IEnumerator UpdateHealthBarCoroutine(float currentHealth, float maxHealth)
    {
        yield return UpdateBarScale(healthBar, currentHealth, maxHealth);
        yield return new WaitForSeconds(.1f);

        yield return UpdateBarScale(healthBarIndicator, currentHealth, maxHealth);
        yield return new WaitForSeconds(.1f);
    }
    public IEnumerator UpdateBarScale(Transform bar, float currentHealth, float maxHealth)
    {
        Vector3 targetScale = new Vector3(currentHealth / maxHealth, 1, 1);

        while (bar.localScale != targetScale)
        {
            bar.localScale = Vector3.MoveTowards(bar.localScale, targetScale, 2 * Time.deltaTime);
            yield return null;
        }
    }
}