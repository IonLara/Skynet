using UnityEngine;
using UnityEngine.InputSystem;

public class DamageAI : MonoBehaviour
{
    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (gameObject.TryGetComponent(out BobSPA bob))
            {
                bob.health -= 9;
                Debug.Log("Damaged " + bob.gameObject.name + ". Remaining HP is: " + bob.health);
            }
        }
    }
}
