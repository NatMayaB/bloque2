using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taxi : MonoBehaviour
{
    [SerializeField] Vector3 displacement;
    [SerializeField] float angle;
    [SerializeField] AXIS rotationAxis;
    Mesh mesh;
    Vector3[] baseVertices;
    Vector3[] newVertices;
    [SerializeField] GameObject frontLeft;
    [SerializeField] GameObject frontRight;
    [SerializeField] GameObject backLeft;
    [SerializeField] GameObject backRight;
    private bool rot = false;
    [SerializeField] bool rotado = false;
    [SerializeField] float forwardSpeed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentInChildren<MeshFilter>().mesh;
        baseVertices = mesh.vertices;


        // Crear una copia de los vértices originales
        newVertices = new Vector3[baseVertices.Length];
        for (int i = 0; i < baseVertices.Length; i++)
        {
            newVertices[i] = baseVertices[i];
        }

        frontLeft.transform.rotation *= Quaternion.Euler(0.0f, 180.0f, 0.0f);
        frontRight.transform.rotation *= Quaternion.Euler(0.0f, 180.0f, 0.0f);
        backLeft.transform.rotation *= Quaternion.Euler(0.0f, 180.0f, 0.0f);
        backRight.transform.rotation *= Quaternion.Euler(0.0f, 180.0f, 0.0f);
        frontLeft.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        frontRight.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        backLeft.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        backRight.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        frontLeft.transform.position = new Vector3(-1.86f, 0.5f, 2.7f);
        frontRight.transform.position = new Vector3(1.86f, 0.5f, -2.7f); //YA
        backLeft.transform.position = new Vector3(-1.86f, 0.5f, 2.1f);
        backRight.transform.position = new Vector3(1.86f, 0.5f, 2.1f);

    }

    // Update is called once per frame
    void Update()
    {
        DoTransform();
    }

    void DoTransform()
    {
        // Matriz de traslación en función del tiempo multiplicado por el desplazamiento
        Matrix4x4 move = HW_Transforms.TranslationMat(displacement.x * Time.time, displacement.y * Time.time, displacement.z * Time.time + forwardSpeed * Time.time);

        // Matriz de rotación en función del tiempo multiplicado por el ángulo y el eje de rotación
        Matrix4x4 rotate = HW_Transforms.RotateMat(angle * Time.time, rotationAxis);

        // Matriz para trasladar al origen
        Matrix4x4 posOrigin = HW_Transforms.TranslationMat(-displacement.x, -displacement.y, -displacement.z);
        Matrix4x4 posObject = HW_Transforms.TranslationMat(displacement.x, displacement.y, displacement.z);

        // Matriz compuesta de traslación y rotación
        Matrix4x4 composite = move;

        // Aplicar la transformación a cada vértice del objeto
        for (int i = 0; i < newVertices.Length; i++)
        {
            // Crear un vector temporal con las coordenadas del vértice original
            Vector4 temp = new Vector4(baseVertices[i].x, baseVertices[i].y, baseVertices[i].z, 1);
            // Aplicar la transformación a cada vértice usando la matriz compuesta
            newVertices[i] = composite * temp;
        }


        // Reemplazar los vértices en el objeto de malla
        mesh.vertices = newVertices;

        // Recalcular las normales de la malla
        mesh.RecalculateNormals();

        if (Time.time >= 3 && rotado == false)
        {
            frontLeft.transform.rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
            frontRight.transform.rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
            backLeft.transform.rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
            backRight.transform.rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
            frontLeft.transform.position = new Vector3(1.86f, 0.5f, 1.225f); //cambie esto para probar
            frontRight.transform.position = new Vector3(-1.86f, 0.5f, 1.225f);
            backLeft.transform.position = new Vector3(1.86f, 0.5f, -1.455f);
            backRight.transform.position = new Vector3(-1.86f, 0.5f, -1.455f);
            transform.rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
            rotado = true;

            for (int i = 0; i < newVertices.Length; i++)
            {
                // Crear un vector temporal con las coordenadas del vértice original
                Vector4 temp = new Vector4(baseVertices[i].x, baseVertices[i].y, baseVertices[i].z, 1);
                // Aplicar la transformación a cada vértice usando la matriz compuesta
                newVertices[i] = -(composite * temp);
            }
        }

        if (Time.time >= 6 && rotado == false)
        {
            frontLeft.transform.rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
            frontRight.transform.rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
            backLeft.transform.rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
            backRight.transform.rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
            frontLeft.transform.position = new Vector3(1.86f, 0.5f, 1.225f);
            frontRight.transform.position = new Vector3(-1.86f, 0.5f, 1.225f);
            backLeft.transform.position = new Vector3(1.86f, 0.5f, -1.455f);
            backRight.transform.position = new Vector3(-1.86f, 0.5f, -1.455f);
            transform.rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
            rotado = true;

            for (int i = 0; i < newVertices.Length; i++)
            {
                // Crear un vector temporal con las coordenadas del vértice original
                Vector4 temp = new Vector4(baseVertices[i].x, baseVertices[i].y, baseVertices[i].z, 1);
                // Aplicar la transformación a cada vértice usando la matriz compuesta
                newVertices[i] = -(composite * temp);
            }
        }


    }
}