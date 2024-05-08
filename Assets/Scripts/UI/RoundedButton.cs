using UnityEngine;

public class RoundedButton : MonoBehaviour
{
    [SerializeField] private float width; // Ancho del botón redondeado
    [SerializeField] private float height; // Alto del botón redondeado
    [SerializeField] private float borderRadius; // Radio del borde del botón redondeado

    private void Start()
    {
        GenerateMesh(); // Método que genera la malla del botón redondeado al iniciar
    }

    private void GenerateMesh()
    {
        var halfWidth = width * 0.5f; // Mitad del ancho
        var halfHeight = height * 0.5f; // Mitad del alto

        int numVertices = 91 * 4; // 91 vértices por cuadrante (4 cuadrantes en total)
        Vector3[] vertices = new Vector3[numVertices]; // Arreglo de vértices

        int vertexIndex = 0; // Índice para llenar el arreglo de vértices

        // Generar vértices para los arcos redondeados en los cuatro cuadrantes
        for (int startAngle = 0; startAngle < 360; startAngle += 90)
        {
            float startX = (startAngle == 0 || startAngle == 270) ? (halfWidth - borderRadius) : -(halfWidth - borderRadius);
            float startY = (startAngle < 180) ? (halfHeight - borderRadius) : -(halfHeight - borderRadius);

            for (int i = startAngle; i <= startAngle + 90; i++)
            {
                float angle = i * Mathf.Deg2Rad;
                float x = startX + Mathf.Cos(angle) * borderRadius;
                float y = startY + Mathf.Sin(angle) * borderRadius;
                vertices[vertexIndex++] = new Vector3(x, y, 0f);
            }
        }

        int numTriangles = 90 * 3 * 4 + 18; // Cantidad de triángulos
        int[] triangles = new int[numTriangles]; // Arreglo de triángulos

        int triangleIndex = 0; // Índice para llenar el arreglo de triángulos

        // Generar triángulos para los arcos redondeados en los cuatro cuadrantes
        for (int o = 0; o < 4; o++) // Cuatro cuadrantes
        {
            int offset = o * 90; // Desplazamiento de índice para cada cuadrante
            int aoff = o * 91; // Desplazamiento de índice para cada cuadrante en el arreglo de vértices

            triangles[offset * 3] = aoff;
            triangles[offset * 3 + 1] = 90 + aoff;
            triangles[offset * 3 + 2] = 89 + aoff;

            for (int i = 3; i < 90 * 3; i += 3)
            {
                triangles[i + offset * 3] = aoff;
                triangles[i + 1 + offset * 3] = triangles[i - 1 + offset * 3];
                triangles[i + 2 + offset * 3] = triangles[i - 1 + offset * 3] - 1;
            }
        }

        // Triángulos adicionales para cerrar la forma del botón redondeado
        int[] remainingTriangles = new int[]
        {
            0, 91, 90,
            91, 182, 181,
            182, 273, 272,
            273, 0, 363,
            273, 91, 0,
            273, 182, 91
        };

        // Agregar triángulos adicionales al arreglo principal de triángulos
        for (int i = 0; i < 18; i++)
        {
            triangles[90 * 3 * 4 + i] = remainingTriangles[i];
        }

        // Crear una nueva malla
        Mesh mesh = new Mesh();
        mesh.vertices = vertices; // Asignar los vértices a la malla
        mesh.triangles = triangles; // Asignar los triángulos a la malla

        // Asignar la malla al objeto actual
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }
}
