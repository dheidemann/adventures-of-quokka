using UnityEngine;

public class enemyHealthBar : MonoBehaviour
{
    private SpriteRenderer healthBarFill;

    void Start()
    {
        // You can initialize healthBarFill here or assign it through the Unity Editor.
         healthBarFill = this.GetComponent<SpriteRenderer>();
    }

    public void SetHealth(float healthPercentage)
    {
        // Clamp the value between 0 and 1
        healthPercentage = Mathf.Clamp01(healthPercentage);

        // Set the local scale of the fill sprite based on the health percentage
        Vector3 newScale = healthBarFill.transform.localScale;
        newScale.x = healthPercentage;
        healthBarFill.transform.localScale = newScale;
    }
}
