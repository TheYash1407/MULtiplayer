using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FishNet.Object;

public class Health : NetworkBehaviour
{
    public int m_health;
    public int m_maxHealth = 100;
    [SerializeField] private Image m_healthbar;
    private void Awake()
    {
        m_health = m_maxHealth;
    }
   /* [ContextMenu ("Take Damage")]
    public void TestTakeDamage()
    {
        TakeDamage(10);
    }*/
    public void UpdateHealth(int health)
    {
        m_health -= health;
        m_healthbar.fillAmount = (float)m_health / m_maxHealth;
        if (m_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    [ObserversRpc]
    private void ObserversUpdate(int health)
    {
        m_health -= health;
        m_healthbar.fillAmount = (float)m_health / m_maxHealth;
        if (m_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
