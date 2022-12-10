using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SlimeAttack : MonoBehaviour
{
    public float maxvalue = 2f;

    public Transform sprite;

    public float value;

    private void Update()
    {
       
       // sprite.position = new Vector3( sprite.gameObject.GetComponent<SpriteRenderer>().size.x, sprite.position.y, sprite.position.z);
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            sprite.localScale = new Vector3(value + 0.1f, 0.05f, 1f);
        }
    }


}
