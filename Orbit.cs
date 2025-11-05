using UnityEngine;

public class Orbit : MonoBehaviour
{
    // O valor real da constante gravitacional é 6.67408 × 10^-11.
    // Aqui usamos um valor maior para deixar as coisas mais rápidas, em vez de aumentar o timestep do Unity.
    readonly float G = 1000f;
    GameObject[] celestials;

    [SerializeField]
    bool IsElipticalOrbit = false;

    // Start é chamado antes da primeira atualização do frame
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");

        SetInitialVelocity();
    }

    // FixedUpdate é chamado em intervalos fixos de tempo
    void FixedUpdate()
    {
        Gravity();
    }

    void SetInitialVelocity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if(!a.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.transform.LookAt(b.transform);

                    if (IsElipticalOrbit)
                    {
                        // Órbita elíptica: G * M * (2 / r + 1 / a),
                        // onde G é a constante gravitacional, M é a massa do corpo central, r é a distância entre os corpos
                        // e a é o semi-eixo maior (não é o GameObject a).
                        a.GetComponent<Rigidbody>().linearVelocity += a.transform.right * Mathf.Sqrt((G * m2) * ((2 / r) - (1 / (r * 1.5f))));
                    }
                    else
                    {
                        // Órbita circular: velocidade = √((G * M) / r)
                        // Ignoramos a massa do corpo em órbita se ela for desprezível em relação à do corpo central (ex: Terra vs Sol).
                        a.GetComponent<Rigidbody>().linearVelocity += a.transform.right * Mathf.Sqrt((G * m2) / r);
                    }
                }
            }
        }
    }

    void Gravity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r)));
                }
            }
        }
    }
}

