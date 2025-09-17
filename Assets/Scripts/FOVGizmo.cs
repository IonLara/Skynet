using UnityEngine;

[ExecuteAlways]
public class FOVGizmo : MonoBehaviour
{
    [Header("FOV Settings")]
    public float viewRadius = 10f;
    [Range(0,360)]
    public float viewAngle = 120f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 0.2f); // amarillo semitransparente

        // dibujar el radio
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        // dibujar las líneas del cono
        Vector3 forward = transform.forward;
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * forward * viewRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * forward * viewRadius;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);

        // opcional: rayos dentro del cono para ver el área
        int rayCount = 10;
        for(int i = 0; i <= rayCount; i++)
        {
            float lerpAngle = Mathf.Lerp(-viewAngle/2f, viewAngle/2f, i/(float)rayCount);
            Vector3 rayDir = Quaternion.Euler(0, lerpAngle, 0) * forward * viewRadius;
            Gizmos.DrawLine(transform.position, transform.position + rayDir);
        }
    }
}
