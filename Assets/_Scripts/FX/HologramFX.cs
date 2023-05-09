using System.Collections;
using UnityEngine;

public class HologramFX : MonoBehaviour
{
    [SerializeField] private float m_maxGlitchStrength = 0.5f;
    [SerializeField] private float m_minGlitchStrength = 0f;
    [SerializeField] private float m_glitchDuration = 1f;
    [SerializeField] private float m_maxTimeBtwGlitches = 1f;
    [SerializeField] private float m_minTimeBtwGlitches = 0f;

    void Start()
    {
        StartCoroutine(Glitch());
    }

    private IEnumerator Glitch()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        yield return new WaitForSeconds(Random.Range(m_minTimeBtwGlitches, m_maxTimeBtwGlitches));

        foreach (Renderer r in renderers)
        {
            r.material.SetFloat("_Glitch_Strength", Random.Range(m_minGlitchStrength, m_maxGlitchStrength));
        }

        yield return new WaitForSeconds(m_glitchDuration);

        foreach (Renderer r in renderers)
        {
            r.material.SetFloat("_Glitch_Strength", 0f);
        }
        StartCoroutine(Glitch());
    }

}