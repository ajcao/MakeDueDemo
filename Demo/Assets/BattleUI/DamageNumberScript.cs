using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumberScript : MonoBehaviour
{
    public TextMeshPro text;
    // Start is called before the first frame update
    public void Init(int d)
    {
        text.text = "" + d;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-15.0f, 15.0f), 40.0f));
        Object.Destroy(this.gameObject, 1.5f);
    }
}
